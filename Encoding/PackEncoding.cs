using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static relert_sharp.Utils.Misc;

namespace relert_sharp.Encoding
{
    public static class PackEncoding
    {
        public static byte[] DecodePack(byte[] source, int maxTileNum)
        {
            byte[] output = new byte[maxTileNum * 11];
            MemoryStream ms = new MemoryStream(source);
            BinaryReader br = new BinaryReader(ms);
            int i = 0;
            while (br.BaseStream.Position != br.BaseStream.Length)
            {
                ushort inputSize = br.ReadUInt16();
                ushort outputSize = br.ReadUInt16();
                byte[] buffer = br.ReadBytes(inputSize);
                byte[] result = new byte[outputSize];
                result = MiniLZO.Decompress(buffer, result);
                WriteToArray(output, result, i);
                i += result.Length;
            }
            br.Dispose();
            ms.Dispose();
            return output;
        }
        public static byte[] EncodeToPack(byte[] source)
        {
            //return MiniLZO.Compress(source);
            int remain = source.Length;
            int outputSize = 0;
            byte[] output = new byte[source.Length];
            MemoryStream msOut = new MemoryStream(output);
            BinaryWriter bwOut = new BinaryWriter(msOut);
            MemoryStream msIn = new MemoryStream(source);
            BinaryReader brIn = new BinaryReader(msIn);
            while (remain > 0)
            {
                byte[] buffer = brIn.ReadBytes(Math.Min(8192, remain));
                byte[] lzoResult = MiniLZO.Compress(buffer);
                ushort resultSize = (ushort)lzoResult.Length;
                outputSize += resultSize + 4;
                bwOut.Write(resultSize);
                bwOut.Write((ushort)8192);
                bwOut.Write(lzoResult);
                remain -= 8192;
                bwOut.Flush();
            }
            bwOut.Close();
            brIn.Close();
            msOut.Close();
            msIn.Close();
            return output.Take(outputSize).ToArray();
        }
    }
}
