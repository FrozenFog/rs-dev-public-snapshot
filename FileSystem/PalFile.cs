using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using relert_sharp.Utils;

namespace relert_sharp.FileSystem
{
    public class PalFile : BaseFile
    {
        private List<RGBColor> data = new List<RGBColor>();


        #region Constructor - PalFile
        public PalFile(Stream baseStream, string _fullName) : base(baseStream, _fullName)
        {
            for(int i = 0; i < 256; i++)
            {
                data.Add(new RGBColor(ReadByte() << 2, ReadByte() << 2, ReadByte() << 2));
            }
        }
        #endregion


        #region Public Calls - PalFile
        public RGBColor this[byte index]
        {
            get { return data[index]; }
        }
        #endregion
    }
}
