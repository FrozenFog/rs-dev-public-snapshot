using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.Common;

namespace relert_sharp.FileSystem
{
    public class LangFile : INIFile
    {



        #region Constructor - LangFile
        public LangFile(string path) : base(path)
        {
            INIType = INIFileType.Language;
        }
        #endregion
    }
}
