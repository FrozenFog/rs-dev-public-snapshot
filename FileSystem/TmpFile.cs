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

    public class TMPFile
    {
        private string fileName;
        private int WidthCount, HeightCount, blockWidthPX, blockHeightPX;
        private List<TmpImage> Images;
        private TheaterType theaterType;

        public TMPFile(string path)
        {
            File f = new File(path, FileMode.Open, FileAccess.Read);
            fileName = f.FullName;
            BinaryReader br = new BinaryReader(f.ReadStream);
            Read(br);
            GetTheater();
            br.Dispose();
            f.Close();
        }
        public TMPFile(Stream baseStream, string fullName)
        {
            fileName = fullName;
            BinaryReader br = new BinaryReader(baseStream);
            Read(br);
            br.Dispose();
        }
        private void GetTheater()
        {
            switch (fileName.Split(new char[] { '.' })[1].ToLower())
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
        private void Read(BinaryReader br)
        {
            WidthCount = br.ReadInt32();
            HeightCount = br.ReadInt32();
            blockWidthPX = br.ReadInt32();
            blockHeightPX = br.ReadInt32();
            byte[] indexs = br.ReadBytes(WidthCount * HeightCount * 4);
            Images = new List<TmpImage>(WidthCount * HeightCount);
            for (int i = 0; i < WidthCount * HeightCount; i++)
            {
                int imageData = BitConverter.ToInt32(indexs, i * 4);
                br.BaseStream.Seek(imageData, SeekOrigin.Begin);
                TmpImage img = new TmpImage();
                img.Read(br, blockWidthPX, blockHeightPX);
                Images.Add(img);
            }
            br.Dispose();
        }
        #region Public Calls - TMPFile
        public string FullName
        {
            get { return fileName; }
        }
        public List<TmpImage> Imgs
        {
            get { return Images; }
        }
        #endregion
    }
    public class TmpImage
    {
        public int X, Y, Height, exX, exY;
        private int exOffset, zOffset, exzOffset;
        private int exWidth, exHeight;
        private StatusFlag _flag;
        public byte TerrainType, RampType;
        public RGBColor ColorRadarLeft, ColorRadarRight;
        public byte[] TileData;
        public byte[] ExtraData;
        public byte[] ZData;
        public byte[] ExtraZData;

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
        private enum StatusFlag : uint
        {
            ExtraData = 0x01,
            ZData = 0x02,
            DamagedData = 0x04,
        }

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
    }
}