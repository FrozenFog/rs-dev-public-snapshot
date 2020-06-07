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
        public RSConfig(string configFileName) : base(configFileName, INIFileType.DefaultINI, true)
        {
            GetMixNameList();
            GetExpandMixList();
            GetTheaterMixList();
            GetCiphedMixNameList();
            GetOldMixList();
            GetStringtable();
            LoadAttribute();
        }
        #endregion


        #region Private Methods - RSConfig
        private void LoadAttribute()
        {
            BagNameList = this["SoundConfigs"].GetPair("Bags").ParseStringList();
            RulesName = this["INI"]["RulesFileName"];
            ArtName = this["INI"]["ArtFileName"];
            SoundName = this["INI"]["SoundFileName"];
            ThemeName = this["INI"]["ThemeFileName"];
            AiName = this["INI"]["AIFileName"];
            EvaName = this["INI"]["EvaFileName"];
            ConfigName = this["General"]["ConfigName"];
            ConfigVersion = this["General"]["ConfigVersion"];
            IgnoreBuildingTheaterArt = this["DrawingConfig"].GetPair("IgnoreBuildingTheaterArt").ParseBool();

            DeactiveAnimList = this["DrawingConfig"].GetPair("DeactivateAnim").ParseStringList().ToList();
            DeactiveBibList = this["DrawingConfig"].GetPair("DeactivateBib").ParseStringList().ToList();
            DeactiveShadow = this["DrawingConfig"].GetPair("DeactivateShadow").ParseStringList().ToList();

            BridgeOffsetFrames = this["DrawingConfig"].GetPair("OffsetBridgeFrames").ParseIntList().ToList();
            if (this["General"].GetPair("PreloadTheaterMix").ParseBool())
            {
                PreloadMixes = new List<string>();
                foreach (INIPair p in this["TheaterMixList"])
                {
                    string[] l = p.ParseStringList();
                    if (l.Length > 0) PreloadMixes.AddRange(l);
                }
            }
        }
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
        public bool IgnoreBuildingTheaterArt { get; private set; }
        public string RulesName { get; private set; }
        public string ArtName { get; private set; }
        public string SoundName { get; private set; }
        public string ThemeName { get; private set; }
        public string AiName { get; private set; }
        public string EvaName { get; private set; }
        public string GamePath
        {
            get
            {
                string p = this["General"]["GamePath"];
                if (!p.EndsWith("\\")) return p + "\\";
                return p;
            }
        }
        public string ConfigName { get; private set; }
        public string ConfigVersion { get; private set; }
        public string LastPath { get; set; }
        public string[] BagNameList { get; private set; }
        public List<string> DeactiveAnimList { get; private set; }
        public List<string> DeactiveBibList { get; private set; }
        public List<string> DeactiveShadow { get; private set; }
        public List<string> StringtableList { get; private set; }
        public List<string> MixNameList { get; private set; }
        public List<string> CiphedMix { get; private set; }
        public List<string> OldMix { get; private set; }
        public List<string> ExpandMixList { get; private set; }
        public List<string> TheaterMixList { get; private set; }
        public List<int> BridgeOffsetFrames { get; private set; }
        public List<string> PreloadMixes { get; private set; }
        #endregion
    }
}
