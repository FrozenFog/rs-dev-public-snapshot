using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DmitryBrant.ImageFormats;
using System.Drawing;

namespace RelertSharp.Encoding
{
    public static class PcxDecoder
    {
        public static Bitmap Decode(byte[] src)
        {
            MemoryStream ms = new MemoryStream(src);
            Bitmap result = PcxReader.Load(ms);
            return result;
        }
    }
}
