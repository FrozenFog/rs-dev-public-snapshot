using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace relert_sharp.FileSystem
{
    public class WavFile
    {
        private byte[] data;
        private int samplePos = 44;
        private bool acmDecoded = true;


        #region Constructor - WavFile
        public WavFile(byte[] _sample, int _sampleRate, int _chanel)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            SamplesPerSec = _sampleRate;
            Chanel = _chanel;
            SampleSize = _sample.Length;
            bw.Write(0x46464952);//RIFF
            bw.Write(_sample.Length + 36);//4+24+4+4
            bw.Write(0x45564157);//WAVE
            bw.Write(0x20746D66);//fmt
            bw.Write(16);//chunksize
            bw.Write((short)1);//formatTag
            bw.Write((ushort)_chanel);//chanel
            bw.Write(SamplesPerSec);//samples per sec
            bw.Write(2 * SamplesPerSec);//sample rate
            bw.Write((ushort)(2 * Chanel));//block align
            bw.Write((ushort)16);//bits per sec
            bw.Write(0x61746164);//data
            bw.Write(_sample.Length);
            bw.Write(_sample);
            bw.Flush();
            data = ms.ToArray();
            ms.Dispose();
            bw.Dispose();
        }
        public WavFile(byte[] fulldata)
        {
            MemoryStream ms = new MemoryStream(fulldata);
            BinaryReader br = new BinaryReader(ms);
            br.ReadInt32(); // RIFF
            uint remainSize = br.ReadUInt32() - 4;
            br.ReadBytes(8);//WAVE, fmt ;

            int fmtSize = br.ReadInt32();
            WavTypeFlag = br.ReadInt16();
            if (WavTypeFlag != 1) acmDecoded = false;
            Chanel = br.ReadUInt16();
            SamplesPerSec = br.ReadInt32();
            AverageSampleRate = br.ReadUInt32();
            BlockAlign = br.ReadInt16();
            BitsPerSample = br.ReadInt16();
            fmtSize -= 16;
            if (fmtSize > 0)
            {
                br.ReadBytes(fmtSize);
                samplePos += 4;
            }

            while (br.BaseStream.CanRead)
            {
                int chunkname = br.ReadInt32();
                if (chunkname == 0x61746164) // data
                {
                    SampleSize = br.ReadInt32();
                    data = fulldata;
                    break;
                }
                else
                {
                    int size = br.ReadInt32();
                    samplePos += 8 + size;
                    br.ReadBytes(size);
                }
            }
        }
        public WavFile() { }
        #endregion


        #region Private Methods - WavFile
        #endregion


        #region Public Methods - WavFile
        #endregion


        #region Public Calls - WavFile
        public byte[] SampleBytes { get { return ByteArray.Skip(samplePos).Take(SampleSize).ToArray(); } }
        public short BitsPerSample { get; private set; }
        public short BlockAlign { get; private set; }
        public uint AverageSampleRate { get; private set; }
        public int SamplesPerSec { get; private set; }
        public short WavTypeFlag { get; private set; }
        public byte[] ByteArray { get { return data; } }
        public bool IsEmpty { get { return ByteArray.Length == 0; } }
        public int SampleSize { get; set; }
        public int Chanel { get; set; }
        public int TimeMilSec { get { return SampleSize / Chanel / SamplesPerSec * 1000; } }
        #endregion
    }
}
