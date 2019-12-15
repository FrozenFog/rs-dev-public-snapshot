using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using relert_sharp.Common;
using relert_sharp.Encoding;
using relert_sharp.Utils;

namespace relert_sharp.FileSystem
{
    public class MixFile : BaseFile
    {
        private int bodyPos;
        private ushort numOfFiles;
        private MixHeader index;
        //private BinaryReader reader;
        public MixFile(string path, MixTatics tatics) : base(path, FileMode.Open, FileAccess.Read)
        {
            Initialize(tatics);
        }
        public MixFile(byte[] rawData, string fileName, MixTatics tatics):base(rawData,fileName)
        {
            Initialize(tatics);
        }
        private void Initialize(MixTatics tatics)
        {
            BReader.BaseStream.Seek(4, SeekOrigin.Begin);
            switch (tatics)
            {
                case MixTatics.Plain:
                    numOfFiles = ReadUInt16();
                    ReadSeek(4, SeekOrigin.Current);
                    index = new MixHeader(ReadBytes(numOfFiles * 12), numOfFiles);
                    bodyPos = 10 + numOfFiles * 12;
                    break;
                case MixTatics.Ciphed:
                    byte[] keySource = ReadBytes(80);
                    byte[] header = DecryptHeader(keySource);
                    index = new MixHeader(header.Skip(6).ToArray(), numOfFiles);
                    break;
                default:
                    break;
            }
            ReadSeek(bodyPos, SeekOrigin.Begin);
        }
        public MemoryStream GetMemFile(string filefullname)
        {
            uint fileID = CRC.GetCRC(filefullname);
            if (index.Entries.Keys.Contains(fileID))
            {
                ReadSeek(index.GetOffset(fileID), SeekOrigin.Current);
                byte[] buffer = ReadBytes(index.GetSize(fileID));
                ReadSeek(bodyPos, SeekOrigin.Begin);
                return new MemoryStream(buffer);
            }
            else
            {
                throw new RSException.MixEntityNotFoundException(FullName, filefullname);
            }
        }
        public bool HasFile(string fileName)
        {
            return index.Entries.Keys.Contains(CRC.GetCRC(fileName));
        }
        private byte[] DecryptHeader(byte[] keySource)
        {
            List<uint> buffer = new List<uint>();
            byte[] blowfishKey = new WSKeyCalc().DecryptKey(keySource);
            Blowfish b = new Blowfish(blowfishKey);
            byte[] headerBlock = ReadBytes(8);
            uint[] firstBlock = b.Decrypt(Misc.ToUintArray(headerBlock));
            buffer = buffer.Concat(firstBlock).ToList();
            numOfFiles = (ushort)firstBlock[0];
            double indexSize = numOfFiles * 12 + 6;
            int i = (int)Math.Ceiling(indexSize / 8F) - 1;
            bodyPos = (i + 1) * 8 + 84;
            for (; i > 0; i--)
            {
                uint[] block = b.Decrypt(Misc.ToUintArray(ReadBytes(8)));
                buffer = buffer.Concat(block).ToList();
            }
            return Misc.ToByteArray(buffer.ToArray());
        }
    }
    public class MixHeader
    {
        private ushort numOfFiles;
        private Dictionary<uint, MixEntry> entries = new Dictionary<uint, MixEntry>();
        public MixHeader(byte[] rawData, ushort num)
        {
            numOfFiles = num;
            BinaryReader br = new BinaryReader(new MemoryStream(rawData));
            for(int i = 0; i < num; i++)
            {
                uint id = br.ReadUInt32();
                entries[id] = new MixEntry(id, br.ReadInt32(), br.ReadInt32());
            }
            br.Dispose();
        }
        public int GetOffset(uint fileID)
        {
            return entries[fileID].offset;
        }
        public int GetSize(uint fileID)
        {
            return entries[fileID].size;
        }
        #region Public Calls - MixHeader
        public Dictionary<uint, MixEntry> Entries
        {
            get { return entries; }
        }
        #endregion
    }
    public class MixEntry
    {
        public uint fileID;
        public int offset, size;
        
        public MixEntry(uint id, int _offset, int _size)
        {
            fileID = id;
            offset = _offset;
            size = _size;
        }
    }
}
