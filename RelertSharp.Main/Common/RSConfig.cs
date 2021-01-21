using RelertSharp.IniSystem;
using System.Collections.Generic;
using System.Linq;

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

            LoadTemplate();
            ModConfig = new ModConfig(Constant.Config.Path);
        }
        #endregion


        #region Private Methods - RSConfig
        private void LoadTemplate()
        {
            LightPostTemplate = this["LightPostTemplate"];
        }
        private void LoadAttribute()
        {
            BagNameList = this["SoundConfigs"].ParseStringList("Bags");
            TriggerConfig = this["INI"]["TriggerConfigFile"];
            RulesName = this["INI"]["RulesFileName"];
            ArtName = this["INI"]["ArtFileName"];
            SoundName = this["INI"]["SoundFileName"];
            ThemeName = this["INI"]["ThemeFileName"];
            AiName = this["INI"]["AIFileName"];
            EvaName = this["INI"]["EvaFileName"];
            Ra2xxName = this["INI"]["Ra2xxName"];
            ConfigName = this["General"]["ConfigName"];
            ConfigVersion = this["General"]["ConfigVersion"];
            IgnoreBuildingTheaterArt = this["DrawingConfig"].ParseBool("IgnoreBuildingTheaterArt");

            DeactiveAnimList = this["DrawingConfig"].ParseStringList("DeactivateAnim").ToList();
            DeactiveBibList = this["DrawingConfig"].ParseStringList("DeactivateBib").ToList();
            DeactiveShadow = this["DrawingConfig"].ParseStringList("DeactivateShadow").ToList();

            BridgeOffsetFrames = this["DrawingConfig"].ParseIntList("OffsetBridgeFrames").ToList();
            if (this["General"].ParseBool("PreloadTheaterMix"))
            {
                PreloadMixes = new List<string>();
                foreach (INIPair p in this["TheaterMixList"])
                {
                    string[] l = p.ParseStringList();
                    if (l.Length > 0) PreloadMixes.AddRange(l);
                }
                foreach (INIPair p in this["PreloadMixExtra"])
                {
                    PreloadMixes.Add(p.Value);
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
        public string GetTheaterPalName(string theaterName)
        {
            TheaterType type = GetTheater(theaterName);
            return GetTheaterPalName(type);
        }
        #endregion


        #region Public Calls - RSConfig
        public LocalConfig Local { get; set; }
        public ModConfig ModConfig { get; set; }
        public bool IgnoreBuildingTheaterArt { get; private set; }
        public string TriggerConfig { get; private set; }
        public string RulesName { get; private set; }
        public string ArtName { get; private set; }
        public string SoundName { get; private set; }
        public string ThemeName { get; private set; }
        public string AiName { get; private set; }
        public string EvaName { get; private set; }
        public string Ra2xxName { get; private set; }
        public string GamePath
        {
            get
            {
                string p = Local["General"]["GamePath"];
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
        public INIEntity LightPostTemplate { get; private set; }
        #endregion
    }
}
