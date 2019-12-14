using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace relert_sharp.Encoding
{
    public static class PackEncoding
    {
        public static byte[] DecodePack(byte[] source, int tileNum)
        {
            byte[] output = new byte[tileNum * 11];
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
                Utils.Misc.WriteToArray(output, result, i);
                i += result.Length;
            }
            br.Dispose();
            ms.Dispose();
            return output;
        }
        public static byte[] EncodeToPack(byte[] source)
        {
            return MiniLZO.Compress(source);
        }
    }
}
