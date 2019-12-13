using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using relert_sharp.Utils;

namespace relert_sharp.FileSystem
{
    public class PalFile
    {
        private string fileName;
        private Dictionary<byte, RGBColor> palDict = new Dictionary<byte, RGBColor>();
        public PalFile(Stream baseStream, string _fullName)
        {
            fileName = _fullName;
            BinaryReader br = new BinaryReader(baseStream);
            byte index = 0;
            for (; !palDict.Keys.Contains(index); index++)
            {
                palDict[index] = new RGBColor((byte)(br.ReadByte() << 2), (byte)(br.ReadByte() << 2), (byte)(br.ReadByte() << 2));
            }
            br.Dispose();
        }
    }
}
