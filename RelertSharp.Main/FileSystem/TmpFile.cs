using RelertSharp.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace RelertSharp.FileSystem
{

    public class TmpFile : BaseFile
    {
        private int blockWidthPX, blockHeightPX;
        private List<TmpImage> images;


        #region Ctor - TmpFile
        public TmpFile(string path) : base(path, FileMode.Open, FileAccess.Read)
        {
            Read();
        }
        public TmpFile(Stream stream, string fileName) : base(stream, fileName)
        {
            Read();
        }
        public TmpFile(byte[] _rawbyte, string _filename) : base(_rawbyte, _filename)
        {
            Read();
        }
        #endregion


        #region Private Methods - TmpFile
        private void Read()
        {
            Width = ReadInt32();
            Height = ReadInt32();
            blockWidthPX = ReadInt32();
            blockHeightPX = ReadInt32();
            byte[] indexs = ReadBytes(Width * Height * 4);
            images = new List<TmpImage>();
            for (int i = 0; i < Width * Height; i++)
            {
                int imageOffset = BitConverter.ToInt32(indexs, i * 4);
                if (imageOffset == 0)
                {
                    images.Add(new TmpImage());
                    continue;
                }
                ReadSeek(imageOffset, SeekOrigin.Begin);
                TmpImage img = new TmpImage();
                img.Read(BReader, blockWidthPX, blockHeightPX);
                images.Add(img);
            }
            Dispose();
        }
        private void Assemble()
        {
            if (AssembleImage == null)
            {
                // Get graphic size
                Rectangle total = new Rectangle(0, 0, blockWidthPX, blockHeightPX);
                int sourceHeight = images.First().Height;
                foreach (TmpImage img in images)
                {
                    int deltaHeight = sourceHeight - img.Height;
                    Point src = new Point(img.X, img.Y);
                    Point offset = new Point(src.X - img.DrawingPos.X, src.Y - img.DrawingPos.Y + deltaHeight * 15);
                    Rectangle pic = new Rectangle(offset, img.TileBitmap.Size);
                    total = Rectangle.Union(total, pic);
                    //total = Misc.UnionRectangle(total, pic, zero, out Point newZero);
                    //zero = newZero;
                }

                // Assemble
                Bitmap result = new Bitmap(total.Width, total.Height);
                Point zero = new Point(-total.X, -total.Y);
                Graphics g = Graphics.FromImage(result);
                foreach (TmpImage img in images)
                {
                    int deltaDrawHeight = sourceHeight - img.Height;
                    Point pos = new Point(zero.X + img.X - img.DrawingPos.X, zero.Y + img.Y - img.DrawingPos.Y + deltaDrawHeight * 15);
                    g.DrawImage(img.TileBitmap, pos);
                }
                g.Dispose();
                AssembleImage = result;
            }
        }
        #endregion


        #region Public Methods - TmpFile
        public void LoadColor(PalFile _pal)
        {
            foreach (TmpImage img in images)
            {
                img.LoadColor(_pal, blockWidthPX, blockHeightPX);
            }
            Assemble();
        }
        #endregion


        #region Public Calls - TmpFile
        public int Width { get; private set; }
        public int Height { get; private set; }
        public TmpImage this[int index]
        {
            get { return images[index]; }
            set { images[index] = value; }
        }
        public List<TmpImage> Images { get { return images; } }
        public Bitmap AssembleImage { get; private set; }
        #endregion
    }


    public class TmpImage
    {
        private enum StatusFlag : uint
        {
            ExtraData = 0x01,
            ZData = 0x02,
            DamagedData = 0x04,
        }
        private int exOffset, zOffset, exzOffset;
        private StatusFlag _flag;
        private byte[] TileData, ExtraData, ZData, ExtraZData, pixelData;
        public int X, Y, Height, exX, exY;
        public byte TerrainType, RampType;
        public Color ColorRadarLeft, ColorRadarRight;

        #region Ctor - TmpImage
        public TmpImage() { }
        #endregion


        #region Private Methods - TmpImage
        #endregion


        #region Public Methods - TmpImage
        public void Read(BinaryReader br, int width, int height)
        {
            X = br.ReadInt32();
            Y = br.ReadInt32();
            exOffset = br.ReadInt32();
            zOffset = br.ReadInt32();
            exzOffset = br.ReadInt32();
            exX = br.ReadInt32();
            exY = br.ReadInt32();
            ExtraWidth = br.ReadInt32();
            ExtraHeight = br.ReadInt32();
            _flag = (StatusFlag)br.ReadUInt32();
            Height = br.ReadByte();
            TerrainType = br.ReadByte();
            RampType = br.ReadByte();
            ColorRadarLeft = Color.FromArgb(br.ReadByte(), br.ReadByte(), br.ReadByte());
            ColorRadarRight = Color.FromArgb(br.ReadByte(), br.ReadByte(), br.ReadByte());
            br.ReadBytes(3);//cdcd

            TileByteCount = width * height / 2;
            TileData = br.ReadBytes(TileByteCount);
            if (HasZData)
                ZData = br.ReadBytes(TileByteCount);

            if (HasExtraData)
                ExtraData = br.ReadBytes(Math.Abs(ExtraWidth * ExtraHeight));

            if (HasZData && HasExtraData && 0 < exzOffset && exzOffset < br.BaseStream.Length)
                ExtraZData = br.ReadBytes(Math.Abs(ExtraWidth * ExtraHeight));
        }
        public unsafe void LoadColor(PalFile _pal, int blockWidthPX = 60, int blockHeightPX = 30)
        {
            Bitmap bmp = new Bitmap(blockWidthPX, blockHeightPX, PixelFormat.Format32bppArgb);
            using (FastBitmap normalImage = new FastBitmap(bmp))
            {
                int count = 0;
                for (int j = 0; j < blockHeightPX - 1; j++)
                {
                    int len_line = blockWidthPX - Math.Abs(blockHeightPX / 2 - j - 1) * 4;
                    int x_start = (blockWidthPX - len_line) / 2;
                    for (int i = x_start; i < len_line + x_start; i++)
                    {
                        if (TileByte(count) != 0)
                        {
                            normalImage.SetPixel(i, j, _pal[TileByte(count)]); ;
                        }
                        count++;
                    }
                }
            }

            TileBitmap = bmp;
            DrawingPos = new Point(0, 0);
            if (HasExtraData)
            {
                Bitmap extra = new Bitmap(ExtraWidth, ExtraHeight, PixelFormat.Format32bppArgb);
                using (FastBitmap extraImage = new FastBitmap(extra))
                {
                    int excount = 0;
                    for (int j = 0; j < ExtraHeight; j++)
                    {
                        for (int i = 0; i < ExtraWidth; i++)
                        {
                            if (ExtraByte(excount) != 0)
                            {
                                extraImage.SetPixel(i, j, _pal[ExtraByte(excount)]);
                            }
                            excount++;
                        }
                    }
                }

                Rectangle _rctTile = new Rectangle(0, 0, blockWidthPX, blockHeightPX);
                Rectangle _rctEx = new Rectangle(exX - X, exY - Y, ExtraWidth, ExtraHeight);
                Rectangle _region = Misc.UnionRectangle(ref _rctTile, ref _rctEx);
                DrawingPos = _rctTile.Location;
                TileBitmap = new Bitmap(_region.Width, _region.Height);
                Graphics g = Graphics.FromImage(TileBitmap);
                g.DrawImage(bmp, _rctTile.Location);
                g.DrawImage(extra, _rctEx.Location);
                g.Dispose();
                extra.Dispose();
            }
        }
        /// <summary>
        /// byte list, RGBA, start from bottom-left
        /// </summary>
        /// <param name="pal"></param>
        /// <returns></returns>
        public void LoadPixelData(PalFile pal)
        {
            if (!PixelByteLoaded)
            {
                if (!IsLoaded) LoadColor(pal);
                byte[] result = new byte[TileBitmap.Width * TileBitmap.Height * 4];
                int c = 0;
                for (int j = TileBitmap.Height - 1; j >= 0; j--)
                {
                    for (int i = 0; i < TileBitmap.Width; i++)
                    {
                        Color px = TileBitmap.GetPixel(i, j);
                        result[c++] = px.R;
                        result[c++] = px.G;
                        result[c++] = px.B;
                        result[c++] = px.A;
                    }
                }
                pixelData = result;
            }
        }
        public byte TileByte(int _index)
        {
            if (TileData == null || TileData.Count() == 0) return 0;
            return TileData[_index];
        }
        public byte ExtraByte(int _index)
        {
            return ExtraData[_index];
        }
        public byte ZByte(int _index)
        {
            return ZData[_index];
        }
        public byte ExtraZByte(int _index)
        {
            return ExtraZData[_index];
        }
        #endregion


        #region Public Calls - TmpImage
        public bool HasExtraData
        {
            get { return (_flag & StatusFlag.ExtraData) == StatusFlag.ExtraData; }
        }
        public bool HasZData
        {
            get { return (_flag & StatusFlag.ZData) == StatusFlag.ZData; }
        }
        public bool HasDamagedData
        {
            get { return (_flag & StatusFlag.DamagedData) == StatusFlag.DamagedData; }
        }
        public bool IsLoaded { get { return TileBitmap != null; } }
        public bool PixelByteLoaded { get { return pixelData != null; } }
        public byte[] PixelRGBA { get { return pixelData; } }
        public Bitmap TileBitmap { get; set; }
        public int TileByteCount { get; private set; }
        public int ExtraWidth { get; set; }
        public int ExtraHeight { get; set; }
        public Point DrawingPos { get; private set; }
        public int WidthPX { get { return TileBitmap.Width; } }
        public int HeightPX { get { return TileBitmap.Height; } }
        public bool IsNullTile { get { return TileData == null || TileData.Length == 0; } }
        #endregion
    }
}