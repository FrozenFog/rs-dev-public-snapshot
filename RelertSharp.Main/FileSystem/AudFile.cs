using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using relert_sharp.Encoding;
using relert_sharp.Common;
using relert_sharp.Utils;

namespace relert_sharp.FileSystem
{
    public class AudFile : BaseFile
    {
        internal enum CompressionType
        {
            WestwoodCompression = 0x1,
            ImaAdpcmCompression = 0x63
        }
        private ushort sampleRate;
        private int dataSize, outputSize;
        private byte[] fullData;
        private AudType flag;
        private CompressionType compressionType;
        private List<AudBlock> data = new List<AudBlock>();


        #region Constructor - AudFile
        public AudFile(byte[] _filedata, string _filename) : base(_filedata, _filename)
        {
            ReadHeader();
            ReadContent();
        }
        public AudFile(string _filename, ushort _sampleRate) : base(_filename)
        {
            sampleRate = _sampleRate;
        }
        public AudFile(byte[] _compressedData, string _filename, ushort _sampleRate, uint _flag, int _chunkSize) : base(_compressedData,_filename)
        {
            sampleRate = _sampleRate;
            //dataSize = _compressedData.Length;
            outputSize = 0;
            dataSize = 0;
            flag = (AudType)_flag;
            compressionType = CompressionType.ImaAdpcmCompression;
            if (_flag == 13)
            {
                return;
            }
            for (int i = _compressedData.Length; i > 0;)
            {
                ushort num = (ushort)Math.Min(508, i);
                outputSize += num * 4;
                dataSize += num + 8;
                AudBlock b = new AudBlock(num, (ushort)(num * 4), ReadInt32(), ReadBytes(num));
                data.Add(b);
                i -= num + 4;
            }
        }
        #endregion


        #region Private Methods - AudFile
        private void ReadContent()
        {
            if (compressionType != CompressionType.ImaAdpcmCompression) return;
            for (int i = dataSize; i > 0;)
            {
                AudBlock block = ReadBlock();
                data.Add(block);
                i -= block.blockSize + 8;
            }
        }
        private AudBlock ReadBlock()
        {
            ushort _sz = ReadUInt16();
            ushort _oz = ReadUInt16();
            int id = ReadInt32();
            byte[] _d = ReadBytes(_sz);
            return new AudBlock(_sz, _oz, id, _d);
        }
        private void ReadHeader()
        {
            sampleRate = ReadUInt16();
            dataSize = ReadInt32();
            outputSize = ReadInt32();
            flag = (AudType)ReadByte();
            compressionType = (CompressionType)ReadByte();
        }
        private void DecodeStrero()
        {
            ReadSeek(0, SeekOrigin.Begin);
            AudioBytes = AudEncoding.DecodeAcmWav(ReadAll(), 1024, false);
        }
        #endregion


        #region Public Methods - AudFile
        public void DecodeBlocks()
        {
            short sample = 0;
            int index = 0;
            foreach (AudBlock b in data)
            {
                if (b.ID == 57007) b.Decode(flag, ref sample, ref index);
                else b.Decode(flag, (short)(b.ID & 0xFFFF), (int)(b.ID & 0xFFFF0000) >> 16);
            }
        }
        public WavFile ToWav()
        {
            WavFile wav;
            if ((int)flag == 13)
            {
                DecodeStrero();
                wav = new WavFile(AudioBytes, SampleRate, 2);
            }
            else
            {
                DecodeBlocks();
                wav = new WavFile(AudioBytes, SampleRate, 1);
            }
            return wav;
        }
        #endregion


        #region Public Calls - AudFile
        public byte[] AudioBytes
        {
            get
            {
                if (fullData == null)
                {
                    byte[] result = new byte[outputSize];
                    int i = 0;
                    foreach (AudBlock b in data)
                    {
                        Array.Copy(b.DecompressedData, 0, result, i, b.DecompressedData.Length);
                        i += b.DecompressedData.Length;
                    }
                    fullData = result;
                }
                return fullData;
            }
            set
            {
                fullData = value;
            }
        }
        public int SampleRate { get { return sampleRate; } }
        #endregion
    }


    public class AudBlock
    {
        internal ushort blockSize;
        private ushort outputSize;
        private int id;
        private byte[] compressedData, decompressedData;


        #region Constructor - AudBlock
        public AudBlock(ushort _size, ushort _outSize, int _id, byte[] _data)
        {
            blockSize = _size;
            outputSize = _outSize;
            id = _id;
            compressedData = _data;
        }
        #endregion


        #region Private Methods - AudBlock
        #endregion


        #region Public Methods - AudBlock
        public void Decode(AudType type, ref short _sample, ref int _index)
        {
            if (IsDecoded) return;
            decompressedData = AudEncoding.DecodeBlock(compressedData, ref _sample, ref _index);
            //short[] cmp = new short[1024];
            //unsafe
            //{
            //    fixed (short* w = &cmp[0])
            //    fixed (byte* r = &compressedData[0])
            //    {
            //        AudDecode.Decode(w, r, 512, 1);
            //    }
            //}
            //decompressedData = Misc.GetBytes(cmp);
        }
        public void Decode(AudType type, short _sample, int _index)
        {
            if (IsDecoded) return;
            decompressedData = AudEncoding.DecodeBlock(compressedData, ref _sample, ref _index);
        }
        #endregion


        #region Public Calls - AudBlock
        public bool IsDecoded { get { return decompressedData != null; } }
        public bool IsEncoded { get { return compressedData != null; } }
        public byte[] CompressedData { get { return compressedData; } }
        public byte[] DecompressedData { get { return decompressedData; } }
        public int ID { get { return id; } }
        #endregion
    }
}
