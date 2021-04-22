using RelertSharp.Common;
using RelertSharp.Encoding;
using RelertSharp.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace RelertSharp.FileSystem
{
    public class ShpFile : BaseFile
    {
        private List<ShpFrame> data = new List<ShpFrame>();
        #region Ctor - ShpFile
        public ShpFile(string _path) : base(_path, FileMode.Open, FileAccess.Read, false)
        {
            Read();
        }
        public ShpFile(byte[] _rawData, string _fullName) : base(_rawData, _fullName)
        {
            try
            {
                Read();
            }
            catch
            {
                throw new RSException.InvalidFileException(_fullName);
            }
        }
        #endregion


        #region Public Methods - ShpFile
        public void ApplyAlphaShp(ShpFile alpha, Color transparent)
        {
            int left = (this.Width - alpha.Width) / 2;
            int top = (this.Height - alpha.Height) / 2;
            foreach (ShpFrame img in data)
            {
                img.ApplyAlpha(alpha, left, top, transparent);
            }
        }
        public void ApplyAlphaShp(ShpFile alpha, int frame, Color transparentColor)
        {
            int left = (this.Width - alpha.Width) / 2;
            int top = (this.Height - alpha.Height) / 2;
            data[frame].ApplyAlpha(alpha, left, top, transparentColor);
        }
        public void LoadColor(PalFile _pal)
        {
            foreach (ShpFrame img in data)
            {
                img.SetBitmap(_pal);
            }
        }
        public void ResetPal()
        {
            foreach (ShpFrame img in data)
            {
                img.DeleteBitmap();
            }
        }
        public Size GetMaxSize()
        {
            Size sz = new Size(0, 0);
            foreach (ShpFrame img in data)
            {
                if (img.Image != null)
                {
                    sz = sz.Union(img.Image.Size);
                }
            }
            return sz;
        }
        #endregion


        #region Private Methods - ShpFile
        private void Read()
        {
            //header
            ReadUInt16();//0x0000
            Width = ReadUInt16();
            Height = ReadUInt16();
            Count = ReadUInt16();
            //indexes
            for (int i = 0; i < Count; i++)
            {
                ushort x = ReadUInt16();
                ushort y = ReadUInt16();
                ushort w = ReadUInt16();
                ushort h = ReadUInt16();
                byte flag = ReadByte();
                ReadBytes(11);
                int offset = ReadInt32();
                ShpFrame frame = new ShpFrame(x, y, w, h, flag, offset);
                data.Add(frame);
            }
            //body
            for (int i = 0; i < Count; i++)
            {
                int j = 1;
                if (data[i].Offset == 0) continue;
                if (i == Count - 1)
                {
                    ReadFrame(data[i], (int)FileLength);
                }
                else
                {
                    while (j + i < Count && data[i + j].Offset == 0) j++;
                    if (j + i == Count)
                    {
                        ReadFrame(data[i], (int)FileLength);
                        continue;
                    }
                    ReadFrame(data[i], data[i + j].Offset);
                }
            }
        }
        private void ReadFrame(ShpFrame _destFrame, int _nextOffset)
        {
            //asshole
            ReadSeek(_destFrame.Offset, SeekOrigin.Begin);
            int _pxCount = _destFrame.Width * _destFrame.Height;
            byte[] buffer = new byte[_pxCount];
            switch (_destFrame.CompressType)
            {
                case ShpFrameCompressionType.RawData:
                    buffer = ReadBytes(_pxCount);
                    break;
                case ShpFrameCompressionType.ScanLineRaw:
                    int _destOffset = 0;
                    for (int j = 0; j < _destFrame.Height; j++)
                    {
                        int _lineLength = ReadUInt16() - 2;
                        ReadBytes(buffer, _destOffset, _lineLength);
                        _destOffset += _lineLength;
                    }
                    break;
                case ShpFrameCompressionType.Compressed:
                    byte[] tmp = ReadBytes(_nextOffset - _destFrame.Offset);
                    buffer = ShpEncoding.Decode(tmp, _destFrame.Width, _destFrame.Height);
                    break;
            }
            _destFrame.SetRawData(buffer);
        }
        #endregion


        #region Public Calls - ShpFile
        public ushort Count { get; private set; }
        public ushort Width { get; private set; }
        public ushort Height { get; private set; }
        public List<ShpFrame> Frames { get { return data; } }
        public bool IsEmpty
        {
            get
            {
                foreach (ShpFrame frm in data)
                {
                    if (!frm.IsNullFrame) return false;
                }
                return true;
            }
        }
        #endregion
    }


    public class ShpFrame
    {
        private bool alphaApplied = false;
        #region Ctor - ShpFrame
        public ShpFrame(ushort _x, ushort _y, ushort _width, ushort _height, byte _flag, int _offset)
        {
            X = _x;
            Y = _y;
            Width = _width;
            Height = _height;
            CompressType = (ShpFrameCompressionType)_flag;
            Offset = _offset;
        }
        #endregion


        #region Public Methods - ShpFrame
        public void ApplyAlpha(ShpFile alpha, int alphaLeft, int alphaTop, Color transparentColor)
        {
            if (!alphaApplied)
            {
                ShpFrame alphaImg = alpha.Frames[0];
                using (FastBitmap fast = new FastBitmap(Image))
                {
                    int yAlpha = this.Y - alphaTop;
                    for (int j = 0; j < Height; j++)
                    {
                        if (yAlpha >= alphaImg.Height) break;
                        if (yAlpha < 0)
                        {
                            yAlpha++;
                            continue;
                        }
                        int xAlpha = this.X - alphaLeft;
                        for (int i = 0; i < Width; i++)
                        {
                            if (xAlpha >= alphaImg.Width) break;
                            if (xAlpha < 0)
                            {
                                xAlpha++;
                                continue;
                            }
                            int iAlpha = xAlpha + yAlpha * alphaImg.Width;
                            if (alphaImg.Data[iAlpha] != 0)
                            {
                                double scale = alphaImg.Data[iAlpha] / 127d;
                                int c = fast.GetPixel(i, j);
                                if (c != transparentColor.ToArgb()) fast.SetPixel(i, j, ScalePixelByAlpha(c, scale));
                            }
                            xAlpha++;
                        }
                        yAlpha++;
                    }
                }
                alphaApplied = true;
            }
        }
        public void DeleteBitmap()
        {
            Image?.Dispose();
            alphaApplied = false;
            Image = null;
        }
        public unsafe void SetBitmap(PalFile _pal)
        {
            if (Image == null)
            {
                if (IsNullFrame)
                {
                    Image = new Bitmap(1, 1);
                    return;
                }
                Bitmap bmp = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
                using (FastBitmap shpImage = new FastBitmap(bmp))
                {
                    int count = 0;
                    for (int j = 0; j < Height; j++)
                    {
                        for (int i = 0; i < Width; i++)
                        {
                            if (Data[count] != 0) shpImage.SetPixel(i, j, _pal[Data[count]]);
                            else shpImage.SetPixel(i, j, Color.Transparent);
                            count++;
                        }
                    }
                }
                Image = bmp;
            }
        }
        public void ResetBitmap(PalFile pal, Color transparentColor)
        {
            Image.Dispose();
            Image = null;
            SetBitmap(pal, transparentColor);
        }
        public unsafe void SetBitmap(PalFile _pal, Color transparentColor)
        {
            if (Image == null)
            {
                if (IsNullFrame)
                {
                    Image = new Bitmap(1, 1);
                    return;
                }
                Bitmap bmp = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
                using (FastBitmap shpImage = new FastBitmap(bmp))
                {
                    int count = 0;
                    for (int j = 0; j < Height; j++)
                    {
                        for (int i = 0; i < Width; i++)
                        {
                            if (Data[count] != 0) shpImage.SetPixel(i, j, _pal[Data[count]]);
                            else shpImage.SetPixel(i, j, transparentColor);
                            count++;
                        }
                    }
                }
                Image = bmp;
            }
        }
        public void SetRawData(byte[] _data)
        {
            Data = _data;
        }
        #endregion


        #region Private Methods - ShpFrame
        private int ScalePixelByAlpha(int src, double alpha)
        {
            int func(int color)
            {
                int result = (int)(color * alpha);
                return Math.Min(result, 0xFF);
            }
            int r = func((src & 0x00ff0000) >> 16);
            int g = func((src & 0x0000ff00) >> 8);
            int b = func(src & 0x000000ff);
            return (0xff << 24) | (r << 16) | (g << 8) | b;
        }
        #endregion


        #region Public Calls - ShpFrame
        public bool IsNullFrame { get { return Offset == 0 || Width == 0 || Height == 0; } }
        public ushort X { get; private set; }
        public ushort Y { get; private set; }
        public ushort Width { get; private set; }
        public ushort Height { get; private set; }
        public ShpFrameCompressionType CompressType { get; private set; }
        public int Offset { get; private set; }
        public byte[] Data { get; private set; }
        public Bitmap Image { get; private set; }
        #endregion
    }
}
