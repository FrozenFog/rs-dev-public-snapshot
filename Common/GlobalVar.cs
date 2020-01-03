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
    }
}
