using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.IniSystem;

namespace relert_sharp.Common
{
    public class RSConfig : INIFile
    {


        #region Contructor - RSConfig
        public RSConfig() : base("config.rsc", INIFileType.DefaultINI)
        {
            GetMixNameList();
            GetExpandMixList();
            GetTheaterMixList();
            GetCiphedMixNameList();
            GetOldMixList();
        }
        #endregion


        #region Private Methods - RSConfig
        
        private void LoadMixName(string type, List<string> _host)
        {
            foreach (string mixname in this[type].TakeValuesToList())
            {
                if (!mixname.Contains("#")) _host.Add(mixname);
                else
                {
                    for (int i = 1; i < 100; i++)
                    {
                        int numlen = mixname.Count(c => c == '#');
                        string tmp = mixname.Substring(0, mixname.Length - numlen) + string.Format("{0:D" + numlen.ToString() + "}", i);
                        _host.Add(tmp);
                    }
                }
            }
        }
        private void GetOldMixList()
        {
            OldMix = new List<string>();
            foreach (string name in this["OldMix"].TakeValuesToList()) OldMix.Add(name);
        }
        private void GetMixNameList()
        {
            MixNameList = new List<string>();
            LoadMixName("MixList", MixNameList);
        }
        private void GetCiphedMixNameList()
        {
            CiphedMix = new List<string>();
            foreach (string ciphed in this["CiphedMix"].TakeValuesToList()) CiphedMix.Add(ciphed);
        }
        private void GetExpandMixList()
        {
            ExpandMixList = new List<string>();
            LoadMixName("ExpandMix", ExpandMixList);
        }
        private void GetTheaterMixList()
        {
            TheaterMixList = new List<string>();
            foreach (INIPair p in this["TheaterMixList"].DataList)
            {
                foreach (string mixname in p.ParseStringList())
                {
                    TheaterMixList.Add(mixname);
                }
            }
        }
        #endregion


        #region Public Calls - RSConfig
        public string RulesName { get { return this["INI"]["RulesFileName"]; } }
        public string ArtName { get { return this["INI"]["ArtFileName"]; } }
        public string SoundName { get { return this["INI"]["SoundFileName"]; } }
        public string ThemeName { get { return this["INI"]["ThemeFileName"]; } }
        public string AiName { get { return this["INI"]["AIFileName"]; } }
        public string EvaName { get { return this["INI"]["EvaFileName"]; } }
        public string GamePath { get { return this["General"]["GamePath"]; } }
        public string ConfigName { get { return this["General"]["ConfigName"]; } }
        public string[] BagNameList { get { return this["SoundConfigs"].GetPair("Bags").ParseStringList(); } }
        public List<string> MixNameList { get; private set; }
        public List<string> CiphedMix { get; private set; }
        public List<string> OldMix { get; private set; }
        public List<string> ExpandMixList { get; private set; }
        public List<string> TheaterMixList { get; private set; }
        #endregion
    }
}
