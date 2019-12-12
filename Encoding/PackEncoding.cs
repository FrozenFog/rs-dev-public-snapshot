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
        public static byte[] DecodePack(byte[] source)
        {
            List<byte> output = new List<byte>();
            MemoryStream ms = new MemoryStream(source);
            BinaryReader br = new BinaryReader(ms);
            while (br.BaseStream.Position != br.BaseStream.Length)
            {
                ushort inputSize = br.ReadUInt16();
                ushort outputSize = br.ReadUInt16();
                byte[] buffer = br.ReadBytes(inputSize);
                byte[] result = new byte[outputSize];
                result = MiniLZO.Decompress(buffer, result);
                output = output.Concat(result).ToList();
            }
            br.Dispose();
            ms.Dispose();
            return output.ToArray();
        }
    }
}
