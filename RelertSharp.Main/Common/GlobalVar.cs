using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace relert_sharp.Common
{
    public static class GlobalVar
    {
        private static ELanguage currentlanguage = ELanguage.EnglishUS;
        public static ELanguage CurrentLanguage
        {
            get { return currentlanguage; }
            set { currentlanguage = value; }
        }
        private static RSConfig config;
        public static RSConfig GlobalConfig
        {
            get { return config; }
            set { config = value; }
        }
        private static FileSystem.VirtualDir globaldir;
        public static FileSystem.VirtualDir GlobalDir
        {
            get { return globaldir; }
            set { globaldir = value; }
        }
        private static IniSystem.Rules globalrules;
        public static IniSystem.Rules GlobalRules
        {
            get { return globalrules; }
            set { globalrules = value; }
        }
    }
}
