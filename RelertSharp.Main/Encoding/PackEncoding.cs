using System;
using System.Linq;
using System.IO;
using relert_sharp.Common;
using static relert_sharp.Utils.Misc;

namespace relert_sharp.Encoding
{
    public static class PackEncoding
    {
        public static byte[] DecodePack(byte[] source, int maxTileNum, PackType type)
        {
            byte[] output;
            if (type == PackType.IsoMapPack) output = new byte[maxTileNum * 11];
            else output = new byte[262144];
            MemoryStream ms = new MemoryStream(source);
            BinaryReader br = new BinaryReader(ms);
            int i = 0;
            while (br.BaseStream.Position != br.BaseStream.Length)
            {
                ushort inputSize = br.ReadUInt16();
                ushort outputSize = br.ReadUInt16();
                byte[] buffer = br.ReadBytes(inputSize);
                byte[] result = new byte[outputSize];
                if (type == PackType.IsoMapPack) result = MiniLZO.Decompress(buffer, result);
                else Format80.DecodeInto(buffer, result);
                WriteToArray(output, result, i);
                i += result.Length;
            }
            br.Dispose();
            ms.Dispose();
            return output;
        }
        public static byte[] EncodeToPack(byte[] source, PackType type)
        {
            int remain = source.Length;
            int outputSize = 0;
            byte[] output = new byte[source.Length];
            byte[] compressResult;
            MemoryStream msOut = new MemoryStream(output);
            BinaryWriter bwOut = new BinaryWriter(msOut);
            MemoryStream msIn = new MemoryStream(source);
            BinaryReader brIn = new BinaryReader(msIn);
            while (remain > 0)
            {
                byte[] buffer = brIn.ReadBytes(Math.Min(8192, remain));
                if (type == PackType.IsoMapPack) compressResult = MiniLZO.Compress(buffer);
                else compressResult = Format80.Encode(buffer);
                ushort resultSize = (ushort)compressResult.Length;
                outputSize += resultSize + 4;
                bwOut.Write(resultSize);
                bwOut.Write((ushort)buffer.Length);
                bwOut.Write(compressResult);
                remain -= buffer.Length;
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
