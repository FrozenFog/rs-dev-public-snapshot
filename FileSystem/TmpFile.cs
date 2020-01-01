using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using relert_sharp.Utils;
using relert_sharp.Common;

namespace relert_sharp.FileSystem
{

    public class TmpFile : BaseFile
    {
        private int WidthCount, HeightCount, blockWidthPX, blockHeightPX;
        private List<TmpImage> Images;
        private TheaterType theaterType;


        #region Constructor - TmpFile
        public TmpFile(string path) : base(path, FileMode.Open, FileAccess.Read)
        {
            Read();
            GetTheater();
        }
        public TmpFile(Stream stream, string fileName) : base(stream, fileName)
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
            Images = new List<TmpImage>(WidthCount * HeightCount);
            for (int i = 0; i < WidthCount * HeightCount; i++)
            {
                int imageData = BitConverter.ToInt32(indexs, i * 4);
                ReadSeek(imageData, SeekOrigin.Begin);
                TmpImage img = new TmpImage();
                img.Read(BReader, blockWidthPX, blockHeightPX);
                Images.Add(img);
            }
            Dispose();
        }
        #endregion


        #region Public Calls - TmpFile
        public TmpImage this[int index]
        {
            get { return Images[index]; }
            set { Images[index] = value; }
        }
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
        private int exWidth, exHeight;
        private StatusFlag _flag;
        public int X, Y, Height, exX, exY;
        public byte TerrainType, RampType;
        public RGBColor ColorRadarLeft, ColorRadarRight;
        public byte[] TileData, ExtraData, ZData, ExtraZData;


        #region Constructor - TmpImage
        public TmpImage() { }
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
            exWidth = br.ReadInt32();
            exHeight = br.ReadInt32();
            _flag = (StatusFlag)br.ReadUInt32();
            Height = br.ReadByte();
            TerrainType = br.ReadByte();
            RampType = br.ReadByte();
            ColorRadarLeft = new RGBColor(br.ReadByte(), br.ReadByte(), br.ReadByte());
            ColorRadarRight = new RGBColor(br.ReadByte(), br.ReadByte(), br.ReadByte());
            br.ReadBytes(3);//cdcd
            
            TileData = br.ReadBytes(width * height / 2);
            if (HasZData)
                ZData = br.ReadBytes(width * height / 2);

            if (HasExtraData)
                ExtraData = br.ReadBytes(Math.Abs(exWidth * exHeight));

            if (HasZData && HasExtraData && 0 < exzOffset && exzOffset < br.BaseStream.Length)
                ExtraZData = br.ReadBytes(Math.Abs(exWidth * exHeight));
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
        #endregion
    }
}