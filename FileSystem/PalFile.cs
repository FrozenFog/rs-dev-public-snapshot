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
        private List<int> data = new List<int>();


        #region Constructor - PalFile
        public PalFile(Stream baseStream, string _fullName) : base(baseStream, _fullName)
        {
            for(int i = 0; i < 256; i++)
            {
                int tmp = (ReadByte() << 18) + (ReadByte() << 10) + (ReadByte() << 2);
                data.Add(tmp);
            }
        }
        #endregion


        #region Public Calls - PalFile
        public int this[byte index]
        {
            get { return data[index]; }
        }
        public int TransparentColor { get { return data[0]; } }
        #endregion
    }
}
