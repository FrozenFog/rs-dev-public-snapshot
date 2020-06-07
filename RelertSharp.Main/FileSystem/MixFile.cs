using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RelertSharp.Common;
using RelertSharp.Encoding;
using RelertSharp.Utils;

namespace RelertSharp.FileSystem
{
    public class MixFile : BaseFile
    {
        private MixHeader index;
        private bool ciphed = false;


        #region Ctor - MixFile
        public MixFile(string path, MixTatics tatics, bool ciphed = false) : base(path, FileMode.Open, FileAccess.Read, true, ciphed)
        {
            this.ciphed = ciphed;
            Initialize(tatics);
        }
        public MixFile(byte[] rawData, string fileName, MixTatics tatics) :base(rawData,fileName)
        {
            Initialize(tatics);
        }
        public MixFile(string path): base(path, FileMode.Open, FileAccess.Read, false, true)
        {

        }
        #endregion


        #region Private Methods - MixFile
        private void Initialize(MixTatics tatics)
        {
            Tatics = tatics;
            BReader.BaseStream.Seek(4, SeekOrigin.Begin);
            switch (tatics)
            {
                case MixTatics.Plain:
                    NumOfFiles = ReadUInt16();
                    ReadSeek(4, SeekOrigin.Current);
                    index = new MixHeader(ReadBytes(NumOfFiles * 12), NumOfFiles, ciphed);
                    BodyPos = 10 + NumOfFiles * 12;
                    break;
                case MixTatics.Ciphed:
                    byte[] keySource = ReadBytes(80);
                    byte[] header = DecryptHeader(keySource);
                    index = new MixHeader(header.Skip(6).ToArray(), NumOfFiles);
                    break;
                case MixTatics.Old:
                    ReadSeek(0, SeekOrigin.Begin);
                    NumOfFiles = ReadUInt16();
                    ReadSeek(4, SeekOrigin.Current);
                    index = new MixHeader(ReadBytes(NumOfFiles * 12), NumOfFiles);
                    BodyPos = 6 + NumOfFiles * 12;
                    break;
                default:
                    break;
            }
            ReadSeek(BodyPos, SeekOrigin.Begin);
        }
        private byte[] DecryptHeader(byte[] keySource)
        {
            List<uint> buffer = new List<uint>();
            byte[] blowfishKey = new WSKeyCalc().DecryptKey(keySource);
            Blowfish b = new Blowfish(blowfishKey);
            byte[] headerBlock = ReadBytes(8);
            uint[] firstBlock = b.Decrypt(Misc.ToUintArray(headerBlock));
            buffer = buffer.Concat(firstBlock).ToList();
            NumOfFiles = (ushort)firstBlock[0];
            double indexSize = NumOfFiles * 12 + 6;
            int i = (int)Math.Ceiling(indexSize / 8F) - 1;
            BodyPos = (i + 1) * 8 + 84;
            for (; i > 0; i--)
            {
                uint[] block = b.Decrypt(Misc.ToUintArray(ReadBytes(8)));
                buffer = buffer.Concat(block).ToList();
            }
            return Misc.ToByteArray(buffer.ToArray());
        }
        #endregion


        #region Public Methods - MixFile
        public MemoryStream GetMemFile(string filefullname)
        {
            uint fileID = CRC.GetCRC(filefullname);
            if (index.Entries.Keys.Contains(fileID))
            {
                ReadSeek(index.GetOffset(fileID), SeekOrigin.Current);
                byte[] buffer = ReadBytes(index.GetSize(fileID));
                ReadSeek(BodyPos, SeekOrigin.Begin);
                return new MemoryStream(buffer);
            }
            else
            {
                throw new RSException.MixEntityNotFoundException(FullName, filefullname);
            }
        }
        public byte[] GetByte(VirtualFileInfo info)
        {
            ReadSeek(info.FileOffset, SeekOrigin.Begin);
            return ReadBytes(info.FileSize);
        }
        public bool HasFile(string fileName)
        {
            return index.Entries.Keys.Contains(CRC.GetCRC(fileName));
        }
        #endregion


        #region Public Calls - MixFile
        public MixHeader Index { get { return index; } }
        public int BodyPos { get; private set; }
        public ushort NumOfFiles { get; private set; }
        public MixTatics Tatics { get; private set; }
        #endregion
    }


    public class MixHeader
    {
        private ushort numOfFiles;
        private Dictionary<uint, MixEntry> entries = new Dictionary<uint, MixEntry>();


        #region Ctor - MixHeader
        public MixHeader(byte[] rawData, ushort num, bool ciphed = false)
        {
            numOfFiles = num;
            BinaryReader br = new BinaryReader(new MemoryStream(rawData));
            for(int i = 0; i < num; i++)
            {
                uint id = br.ReadUInt32();
                entries[id] = new MixEntry(id, br.ReadInt32(), br.ReadInt32(), ciphed);
            }
            br.Dispose();
        }
        #endregion


        #region Public Methods - MixHeader
        public int GetOffset(uint fileID)
        {
            return entries[fileID].offset;
        }
        public int GetSize(uint fileID)
        {
            return entries[fileID].size;
        }
        #endregion


        #region Public Calls - MixHeader
        public Dictionary<uint, MixEntry> Entries
        {
            get { return entries; }
        }
        public MixEntry this[uint crcID]
        {
            get { return entries[crcID]; }
        }
        #endregion
    }


    public class MixEntry
    {
        public uint fileID;
        public int offset, size;
        public bool hostCiphed;


        #region Ctor - MixEntry
        public MixEntry(uint id, int _offset, int _size, bool ciphed = false)
        {
            fileID = id;
            offset = _offset;
            size = _size;
            hostCiphed = ciphed;
        }
        #endregion
    }
}
