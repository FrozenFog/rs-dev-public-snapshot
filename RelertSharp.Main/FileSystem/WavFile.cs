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


        #region Constructor - WavFile
        public WavFile(byte[] _sample, int _sampleRate, int _chanel)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            SampleRate = _sampleRate;
            Chanel = _chanel;
            SampleSize = _sample.Length;
            bw.Write(0x46464952);//RIFF
            bw.Write(_sample.Length + 36);//4+24+4+4
            bw.Write(0x45564157);//WAVE
            bw.Write(0x20746D66);//fmt
            bw.Write(16);//chunksize
            bw.Write((short)1);//formatTag
            bw.Write((ushort)_chanel);//chanel
            bw.Write(_sampleRate);//samples per sec
            bw.Write(44100);//sample rate
            bw.Write((ushort)2);//block align
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
            br.ReadInt32();
            SampleSize = br.ReadInt32() - 36;
            br.ReadBytes(14);
            Chanel = br.ReadUInt16();
            br.ReadInt32();
            SampleRate = br.ReadInt32();
            data = fulldata;
        }
        public WavFile() { }
        #endregion


        #region Private Methods - WavFile
        #endregion


        #region Public Methods - WavFile
        #endregion


        #region Public Calls - WavFile
        public byte[] ByteArray { get { return data; } }
        public bool IsEmpty { get { return ByteArray.Length == 0; } }
        public int SampleRate { get; set; }
        public int SampleSize { get; set; }
        public int Chanel { get; set; }
        public int TimeMilSec { get { return SampleSize * 1000 / Chanel / SampleRate; } }
        #endregion
    }
}
