using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using relert_sharp.Common;
using relert_sharp.Encoding;

namespace relert_sharp.FileSystem
{
    public class ShpFile : BaseFile
    {
        private List<ShpFrame> data = new List<ShpFrame>();
        #region Constructor - ShpFile
        public ShpFile(string _path) : base(_path, FileMode.Open, FileAccess.Read, false)
        {
            Read();
        }
        public ShpFile(byte[] _rawData, string _fullName) : base(_rawData, _fullName)
        {
            Read();
        }
        #endregion


        #region Public Methods - ShpFile
        public void LoadColor(PalFile _pal)
        {
            foreach (ShpFrame img in data)
            {
                img.SetBitmap(_pal);
            }
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
                    if (j+i == Count)
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
        #endregion
    }


    public class ShpFrame
    {
        #region Constructor - ShpFrame
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
        public void SetBitmap(PalFile _pal)
        {
            if (IsNullFrame) return;
            Bitmap bmp = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            int count = 0;
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    if (Data[count] != 0) bmp.SetPixel(i, j, Color.FromArgb(_pal[Data[count]]));
                    count++;
                }
            }
            Image = bmp;
        }
        public void SetRawData(byte[] _data)
        {
            Data = _data;
        }
        #endregion


        #region Private Methods - ShpFrame
        #endregion


        #region Public Calls - ShpFrame
        public bool IsNullFrame { get { return Offset == 0 || X == 0 || Y == 0; } }
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
