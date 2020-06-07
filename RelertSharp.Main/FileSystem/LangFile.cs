using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.IniSystem;
using RelertSharp.Common;

namespace RelertSharp.FileSystem
{
    public class LangFile : INIFile
    {



        #region Ctor - LangFile
        public LangFile(string path) : base(path, INIFileType.Language, true)
        {
            INIType = INIFileType.Language;
        }
        #endregion
    }
}
