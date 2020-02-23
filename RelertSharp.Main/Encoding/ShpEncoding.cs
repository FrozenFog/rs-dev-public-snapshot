using System.IO;

namespace relert_sharp.Encoding
{
    public static class ShpEncoding
    {
        public static byte[] Decode(byte[] _src, int _frameWidth, int _frameHeight)
        {
            MemoryStream ms = new MemoryStream();
            BinaryReader br = new BinaryReader(new MemoryStream(_src));
            BinaryWriter bw = new BinaryWriter(ms);
            for (int j = 0; j < _frameHeight; j++)
            {
                int scanlineLength = br.ReadUInt16() - 2;
                int xPos = 0;
                while (scanlineLength > 0)
                {
                    byte b = br.ReadByte();
                    scanlineLength--;
                    if (b == 0)
                    {
                        byte numZero = br.ReadByte();
                        if (xPos + numZero > _frameWidth)
                        {
                            numZero = (byte)(_frameWidth - xPos);
                        }
                        xPos += numZero;
                        scanlineLength--;
                        for (; numZero > 0; numZero--)
                        {
                            bw.Write((byte)0x00);
                        }
                    }
                    else
                    {
                        bw.Write(b);
                        xPos++;
                    }
                }
            }
            bw.Flush();
            return ms.ToArray();
        }
    }
}
