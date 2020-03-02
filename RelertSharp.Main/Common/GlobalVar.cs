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
        private static IniSystem.SoundRules globalsound;
        public static IniSystem.SoundRules GlobalSound
        {
            get { return globalsound; }
            set { globalsound = value; }
        }
        private static FileSystem.SoundBank globalsoundbank;
        public static FileSystem.SoundBank GlobalSoundBank
        {
            get { return globalsoundbank; }
            set { globalsoundbank = value; }
        }
        private static FileSystem.CsfFile globalcsf;
        public static FileSystem.CsfFile GlobalCsf
        {
            get { return globalcsf; }
            set { globalcsf = value; }
        }
        private static string playerside;
        public static string PlayerSide
        {
            get { return playerside; }
            set { playerside = value; }
        }
    }
}
