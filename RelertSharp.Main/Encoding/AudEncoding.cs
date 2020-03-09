using System;
using System.IO;

namespace relert_sharp.Encoding
{
    public static class AudEncoding
    {
        private static readonly int[] tb_step = 
        {
            7, 8, 9, 10, 11, 12, 13, 14, 16, 17,
            19, 21, 23, 25, 28, 31, 34, 37, 41, 45,
            50, 55, 60, 66, 73, 80, 88, 97, 107, 118,
            130, 143, 157, 173, 190, 209, 230, 253, 279, 307,
            337, 371, 408, 449, 494, 544, 598, 658, 724, 796,
            876, 963, 1060, 1166, 1282, 1411, 1552, 1707, 1878, 2066,
            2272, 2499, 2749, 3024, 3327, 3660, 4026, 4428, 4871, 5358,
            5894, 6484, 7132, 7845, 8630, 9493, 10442, 11487, 12635, 13899,
            15289, 16818, 18500, 20350, 22385, 24623, 27086, 29794, 32767
        };
        private static readonly int[] tb_adjust =
        {
            -1, -1, -1, -1, 2, 4, 6, 8
        };
        public static byte[] DecodeAcmWav(byte[] src, int chunkSize, bool isMono)
        {
            MemoryStream ms = new MemoryStream(src);
            BinaryReader br = new BinaryReader(ms);
            MemoryStream result = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(result);
            for (int i = src.Length; i > 0;)
            {
                if (isMono)
                {
                    short predict = br.ReadInt16();
                    int index = br.ReadByte();
                    br.ReadByte();
                    int num = Math.Min(chunkSize - 4, i);
                    byte[] block = br.ReadBytes(num);
                    bw.Write(DecodeBlock(block, ref predict, ref index));
                    bw.Flush();
                    i -= (num + 4);
                }
                else
                {
                    short spL = br.ReadInt16();
                    int idL = br.ReadByte(); br.ReadByte();
                    short spR = br.ReadInt16();
                    int idR = br.ReadByte(); br.ReadByte();
                    int num = Math.Min(chunkSize - 8, i);
                    byte[] block = br.ReadBytes(num);
                    bw.Write(DecodeStereo(block, 8, ref spL, ref idL, ref spR, ref idR));
                    bw.Flush();
                    i -= (num + 8);
                }
            }
            byte[] arr = result.ToArray();
            result.Dispose();
            br.Dispose();
            ms.Dispose();
            bw.Dispose();
            return arr;
        }
        public static byte[] DecodeBlock(byte[] _compressedData, ref short _sample, ref int _index)
        {
            int code;
            bool _low = true;
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            int j = _compressedData.Length * 2;
            for (int i = 0; i < j; i++)
            {
                code = GetCode(_compressedData[i / 2], ref _low);
                DecodeCode(code, ref _sample, ref _index);
                bw.Write(_sample);
            }
            bw.Flush();
            byte[] result = ms.ToArray();
            bw.Dispose();
            ms.Dispose();
            return result;
        }
        public static byte[] DecodeStereo(byte[] _compressedData, int switchSize, ref short _sampleL, ref int _indexL, ref short _sampleR, ref int _indexR)
        {
            int code;
            bool _low = true;
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            int j = _compressedData.Length * 2;
            for (int i = 0; i < j;)
            {
                short[] left = new short[switchSize];
                short[] right = new short[switchSize];
                for (int l = 0; l < switchSize; l++)
                {
                    code = GetCode(_compressedData[i++ / 2], ref _low);
                    DecodeCode(code, ref _sampleL, ref _indexL);
                    left[l] = _sampleL;
                }
                for (int r = 0; r < switchSize; r++)
                {
                    code = GetCode(_compressedData[i++ / 2], ref _low);
                    DecodeCode(code, ref _sampleR, ref _indexR);
                    right[r] = _sampleR;
                }
                WriteStereo(left, right, bw);
            }
            byte[] result = ms.ToArray();
            bw.Dispose();
            ms.Dispose();
            return result;
        }
        private static void WriteStereo(short[] left, short[] right, BinaryWriter dest)
        {
            for (int i = 0; i < left.Length; i++)
            {
                dest.Write(left[i]);
                dest.Write(right[i]);
            }
            dest.Flush();
        }
        private static void DecodeCode(int code, ref short sample, ref int index)
        {
            byte signingbit = (byte)(code >> 3);
            code &= 0x7;
            short delta = (short)(tb_step[index] * code / 4 + (tb_step[index] / 8));
            if (signingbit == 1) delta *= -1;
            sample = (short)Region(-32768, 32767, sample + delta);
            index = Region(0, 88, index + tb_adjust[code]);
        }
        private static int GetCode(byte _src, ref bool _isLow)
        {
            byte result = _src;
            if (_isLow)
            {
                result &= 0xF;
            }
            else
            {
                result >>= 4;
            }
            _isLow = !_isLow;
            return result;
        }
        public static byte[] EncodeBlock(byte[] _rawData, int _outputSize, Type T)
        {
            BinaryReader br = new BinaryReader(new MemoryStream(_rawData));
            int previousSample = 0;
            int index = 0;
            byte signingBit;
            int currentSample;
            byte[] result = new byte[_outputSize];
            bool low = true;
            for (int i = 0; i < _rawData.Length;)
            {
                if (T == typeof(short)) currentSample = br.ReadUInt16();
                else currentSample = br.ReadByte();
                int delta = currentSample - previousSample;
                if (delta < 0)
                {
                    delta *= -1;
                    signingBit = 1;
                }
                else signingBit = 0;
                int code = Math.Min(4 * delta / tb_step[index], 7);
                index += tb_adjust[code];
                index = Region(0, 88, index);
                int predictDelta = (tb_step[index] * code) / 4 + (tb_step[index] / 8);
                previousSample = Region(-32768, 32767, previousSample + predictDelta);
                Write4Bit(code + signingBit << 3, ref result, ref low, ref i);
            }
            br.Dispose();
            return result;
        }
        private static void Write4Bit(int _data, ref byte[] _dest, ref bool _isLow, ref int _index)
        {
            _data &= 0xF;
            if (!_isLow) _data <<= 4;
            _dest[_index / 2] += (byte)_data;
            _isLow = !_isLow;
            _index++;
        }
        private static int Region(int _floor, int _ceil, int _src)
        {
            if (_src >= _ceil) return _ceil;
            if (_src <= _floor) return _floor;
            return _src;
        }
    }
}
