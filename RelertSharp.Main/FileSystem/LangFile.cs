using RelertSharp.Common;
using RelertSharp.IniSystem;

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
