using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using relert_sharp.Utils;
using relert_sharp.Common;

namespace relert_sharp.FileSystem
{

    public class TmpFile : BaseFile
    {
        private int WidthCount, HeightCount, blockWidthPX, blockHeightPX;
        private List<TmpImage> images;
        private TheaterType theaterType;


        #region Ctor - TmpFile
        public TmpFile(string path) : base(path, FileMode.Open, FileAccess.Read)
        {
            Read();
            GetTheater();
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
        private void GetTheater()
        {
            switch (NameExt.ToLower())
            {
                case "tem":
                    theaterType = TheaterType.Template;
                    break;
                case "des":
                    theaterType = TheaterType.Desert;
                    break;
                case "sno":
                    theaterType = TheaterType.Snow;
                    break;
                case "lun":
                    theaterType = TheaterType.Lunar;
                    break;
                case "urb":
                    theaterType = TheaterType.Urban;
                    break;
                case "ubn":
                    theaterType = TheaterType.NewUrban;
                    break;
                default:
                    theaterType = TheaterType.Unknown;
                    break;
            }
        }
        private void Read()
        {
            WidthCount = ReadInt32();
            HeightCount = ReadInt32();
            blockWidthPX = ReadInt32();
            blockHeightPX = ReadInt32();
            byte[] indexs = ReadBytes(WidthCount * HeightCount * 4);
            images = new List<TmpImage>();
            for (int i = 0; i < WidthCount * HeightCount; i++)
            {
                int imageOffset = BitConverter.ToInt32(indexs, i * 4);
                if (imageOffset == 0) continue;
                ReadSeek(imageOffset, SeekOrigin.Begin);
                TmpImage img = new TmpImage();
                img.Read(BReader, blockWidthPX, blockHeightPX);
                images.Add(img);
            }
            Dispose();
        }
        #endregion


        #region Public Methods - TmpFile
        public void LoadColor(PalFile _pal)
        {
            foreach (TmpImage img in images)
            {
                img.LoadColor(_pal, blockWidthPX, blockHeightPX);
            }
        }
        #endregion


        #region Public Calls - TmpFile
        public TmpImage this[int index]
        {
            get { return images[index]; }
            set { images[index] = value; }
        }
        public List<TmpImage> Images { get { return images; } }
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
        public RGBColor ColorRadarLeft, ColorRadarRight;

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
            ColorRadarLeft = new RGBColor(br.ReadByte(), br.ReadByte(), br.ReadByte());
            ColorRadarRight = new RGBColor(br.ReadByte(), br.ReadByte(), br.ReadByte());
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
        public void LoadColor(PalFile _pal, int blockWidthPX = 60, int blockHeightPX = 30)
        {
            Bitmap bmp = new Bitmap(blockWidthPX, blockHeightPX, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            int count = 0;
            for (int j = 0; j < blockHeightPX - 1; j++)
            {
                int len_line = blockWidthPX - Math.Abs(blockHeightPX / 2 - j - 1) * 4;
                int x_start = (blockWidthPX - len_line) / 2;
                for (int i = x_start; i < len_line + x_start; i++)
                {
                    if (TileByte(count) != 0) bmp.SetPixel(i, j, Color.FromArgb(_pal[TileByte(count)]));
                    count++;
                }
            }
            TileBitmap = bmp;
            DrawingPos = new Point(0, 0);
            if (HasExtraData)
            {
                Bitmap extra = new Bitmap(ExtraWidth, ExtraHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                int excount = 0;
                for (int j = 0; j < ExtraHeight; j++)
                {
                    for (int i = 0; i < ExtraWidth; i++)
                    {
                        if (ExtraByte(excount) != 0) extra.SetPixel(i, j, Color.FromArgb(_pal[ExtraByte(excount)]));
                        excount++;
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
        #endregion
    }
}