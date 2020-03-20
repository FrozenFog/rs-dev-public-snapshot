using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.IniSystem;

namespace RelertSharp.Common
{
    public class RSConfig : INIFile
    {


        #region ContruCtor - RSConfig
        public RSConfig() : base("config.rsc", INIFileType.DefaultINI)
        {
            GetMixNameList();
            GetExpandMixList();
            GetTheaterMixList();
            GetCiphedMixNameList();
            GetOldMixList();
            GetStringtable();
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
        private void GetStringtable()
        {
            StringtableList = new List<string>();
            foreach (string stb in this["StringTable"].TakeValuesToList()) StringtableList.Add(stb);
        }
        #endregion


        #region Public Methods - RSConfig
        public TheaterType GetTheater(string theaterName)
        {
            theaterName = theaterName.ToLower();
            if (theaterName == this["CustomTheater"]["Custom1Name"]) return TheaterType.Custom1;
            else if (theaterName == this["CustomTheater"]["Custom2Name"]) return TheaterType.Custom2;
            else if (theaterName == this["CustomTheater"]["Custom3Name"]) return TheaterType.Custom3;
            switch (theaterName)
            {
                case "temperate":
                    return TheaterType.Temprate;
                case "desert":
                    return TheaterType.Desert;
                case "urban":
                    return TheaterType.Urban;
                case "newurban":
                    return TheaterType.NewUrban;
                case "lunar":
                    return TheaterType.Lunar;
                case "snow":
                    return TheaterType.Snow;
                default:
                    return TheaterType.Unknown;
            }
        }
        public string GetTheaterPalName(TheaterType type)
        {
            switch (type)
            {
                case TheaterType.Custom1:
                    return this["TheaterPals"]["Custom1"];
                case TheaterType.Custom2:
                    return this["TheaterPals"]["Custom2"];
                case TheaterType.Custom3:
                    return this["TheaterPals"]["Custom3"];
                case TheaterType.Desert:
                    return this["TheaterPals"]["Desert"];
                case TheaterType.Lunar:
                    return this["TheaterPals"]["Lunar"];
                case TheaterType.NewUrban:
                    return this["TheaterPals"]["NewUrban"];
                case TheaterType.Snow:
                    return this["TheaterPals"]["Snow"];
                case TheaterType.Temprate:
                    return this["TheaterPals"]["Temperate"];
                case TheaterType.Urban:
                    return this["TheaterPals"]["Urban"];
                default:
                    return null;
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
        public List<string> StringtableList { get; private set; }
        public List<string> MixNameList { get; private set; }
        public List<string> CiphedMix { get; private set; }
        public List<string> OldMix { get; private set; }
        public List<string> ExpandMixList { get; private set; }
        public List<string> TheaterMixList { get; private set; }
        #endregion
    }
}
