using RelertSharp.Common;
using RelertSharp.FileSystem;
using RelertSharp.IniSystem;
using System.Collections.Generic;
using System.Drawing;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure
{
    public class MapInfo : IChecksum
    {
        private static int iniformat = 4;
        private INIEntity _basic, _map, _specialFlags;
        public string MapName, ThemeName, PostScoreFilmName, AltNextScene, TheaterName, PlayerHouseName;
        public string[] GameModes;
        public Rectangle Size, LocalSize;


        #region Ctor - MapInfo
        public MapInfo(INIEntity Basic, INIEntity MapSize, INIEntity SpecialFlags)
        {
            _basic = Basic;
            _map = MapSize;
            _specialFlags = SpecialFlags;
            //string value
            MapName = Basic.PopPair("Name").Value;
            ThemeName = Basic.PopPair("Theme").Value;
            PostScoreFilmName = Basic.PopPair("PostScore").Value;
            AltNextScene = Basic.PopPair("AltNextScenario").Value;
            TheaterName = MapSize.PopPair("Theater").Value;
            PlayerHouseName = Basic.PopPair("Player").Value;
            GameModes = Basic.PopPair("GameMode").ParseStringList();

            int[] buf = MapSize.PopPair("Size").ParseIntList();
            Size = RectFromIntList(buf);
            buf = MapSize.PopPair("LocalSize").ParseIntList();
            LocalSize = RectFromIntList(buf);
            BasicResidue = Basic;
            SpecialFlagsResidue = SpecialFlags;
        }
        public MapInfo(IMapCreationConfig cfg)
        {
            MapName = cfg.MapName;
            Size = new Rectangle(0, 0, cfg.Width, cfg.Height);
            LocalSize = new Rectangle(2, 4, cfg.Width - 4, cfg.Height - 6);
            TheaterName = cfg.TheaterKey.ToUpper();
            if (cfg.IsSinglePlayer)
            {
                PlayerHouseName = string.Format(Constant.FMT_HOUSE, cfg.PlayerHouseName);
            }
            Basic = new INIEntity("Basic");
            Basic.AddPair(new INIPair("NewINIFormat", 4));
            Map = new INIEntity("Map");
            SpecialFlags = new INIEntity("SpecialFlags");
            BasicResidue = Basic;
            SpecialFlagsResidue = SpecialFlags;
        }
        #endregion


        #region Public Methods - MapInfo
        public int GetChecksum()
        {
            unchecked
            {
                int hash = GetMapEnt().GetChecksum() * 17 + GetBasicEnt().GetChecksum();
                hash = hash * 31 + Size.GetHashCode();
                hash = hash * 31 + LocalSize.GetHashCode();
                hash = hash * 31 + SpecialFlags.GetChecksum();
                return hash;
            }
        }
        public INIEntity GetMapEnt()
        {
            INIEntity map = new INIEntity("Map");
            map.AddPair("Size", Size.ParseString());
            map.AddPair("LocalSize", LocalSize.ParseString());
            map.AddPair("Theater", TheaterName);
            return map;
        }
        public INIEntity GetBasicEnt()
        {
            INIEntity basic = new INIEntity("Basic");
            basic.AddPair("Name", MapName);
            basic.AddPair("Theme", ThemeName);
            basic.AddPair("PostScore", PostScoreFilmName);
            basic.AddPair("AltNextScenario", AltNextScene);
            basic.AddPair("Player", PlayerHouseName);
            basic.AddPair("GameMode", GameModes.JoinBy());
            basic.AddPair(BasicResidue);
            return basic;
        }
        //public void AddInfo(INIEntity ent)
        //{
        //    if (InfoResidue.Keys.Contains(ent.Name)) return;
        //    InfoResidue[ent.Name] = ent;
        //}
        #endregion


        #region Public Calls - MapInfo
        public INIEntity Basic
        {
            get { return _basic; }
            set { _basic = value; }
        }
        public INIEntity Map
        {
            get { return _map; }
            set { _map = value; }
        }
        public INIEntity SpecialFlags
        {
            get { return _specialFlags; }
            set { _specialFlags = value; }
        }
        public int INIFormat
        {
            get { return iniformat; }
        }
        public INIEntity BasicResidue { get; private set; }
        public INIEntity SpecialFlagsResidue { get; private set; }
        public TheaterType TheaterType { get { return GlobalVar.GlobalConfig.GetTheater(TheaterName); } }
        #endregion
    }


    public class Lightning : IChecksum
    {



        #region Ctor - Lightning
        public Lightning(INIEntity ent)
        {
            Normal = new LightningItem(ent["Red"], ent["Green"], ent["Blue"],
                ent["Level"], ent["Ground"], ent["Ambient"]);
            Ion = new LightningItem(ent["IonRed"], ent["IonGreen"], ent["IonBlue"],
                ent["IonLevel"], ent["IonGround"], ent["IonAmbient"]);
            Dominator = new LightningItem(ent["DominatorRed"], ent["DominatorGreen"], ent["DominatorBlue"],
                ent["DominatorLevel"], ent["DominatorGround"], ent["DominatorAmbient"]);
            DominatorChangeRate = ParseDouble(ent["DominatorAmbientChangeRate"]);
        }
        public Lightning()
        {
            Normal = new LightningItem();
            Ion = new LightningItem();
            Dominator = new LightningItem();
            DominatorChangeRate = 0.009;
        }
        #endregion


        #region Public Methods
        public INIEntity GetSaveData()
        {
            INIEntity ent = new INIEntity("Lighting");
            Normal.LoadToSaveData(ref ent);
            Ion.LoadToSaveData(ref ent, "Ion");
            Dominator.LoadToSaveData(ref ent, "Dominator");
            ent.AddPair("DominatorAmbientChangeRate", DominatorChangeRate);
            return ent;
        }
        public int GetChecksum()
        {
            unchecked
            {
                int hash = Constant.BASE_HASH;
                hash = hash * 67 + Normal.GetChecksum();
                hash = hash * 67 + Ion.GetChecksum();
                hash = hash * 67 + Dominator.GetChecksum();
                return hash;
            }
        }
        #endregion


        #region Public Calls - Lightning
        public LightningItem Normal { get; set; }
        public LightningItem Ion { get; set; }
        public LightningItem Dominator { get; set; }
        public double DominatorChangeRate { get; set; }
        #endregion
    }


    public class LightningItem : IChecksum
    {



        #region Ctor - LightningItem
        public LightningItem(string _R, string _G, string _B, string _level, string _ground, string _ambient)
        {
            Red = ParseFloat(_R, 1);
            Green = ParseFloat(_G, 1);
            Blue = ParseFloat(_B, 1);
            Level = ParseFloat(_level);
            Ground = ParseFloat(_ground);
            Ambient = ParseFloat(_ambient, 1);
        }
        public LightningItem(float r, float g, float b, float level, float ground, float ambient)
        {
            Red = r;
            Green = g;
            Blue = b;
            Level = level;
            Ground = ground;
            Ambient = ambient;
        }
        public LightningItem()
        {
            Red = 1;
            Green = 1;
            Blue = 1;
            Level = 0;
            Ground = 0;
            Ambient = 1;
        }
        #endregion


        #region Public Methods - LightningItem
        public void LoadToSaveData(ref INIEntity dest, string prefix = "")
        {
            dest.AddPair(prefix + "Red", Red);
            dest.AddPair(prefix + "Green", Green);
            dest.AddPair(prefix + "Blue", Blue);
            dest.AddPair(prefix + "Ambient", Ambient);
            dest.AddPair(prefix + "Level", Level);
            dest.AddPair(prefix + "Ground", Ground);
        }
        public int GetChecksum()
        {
            unsafe
            {
                int cast(float src) { return *(int*)&src; }
                int hash = Constant.BASE_HASH;
                hash = hash * 11 + cast(Red);
                hash = hash * 11 + cast(Green);
                hash = hash * 11 + cast(Blue);
                hash = hash * 11 + cast(Level);
                hash = hash * 11 + cast(Ambient);
                hash = hash * 11 + cast(Ground);
                return hash;
            }
        }
        #endregion


        #region Public Calls - LightningItem
        public float Red { get; set; } = 1;
        public float Green { get; set; } = 1;
        public float Blue { get; set; } = 1;
        public float Level { get; set; } = 0;
        public float Ground { get; set; } = 0;
        public float Ambient { get; set; } = 1;
        public static LightningItem None { get { return new LightningItem(); } }
        #endregion
    }


    public class RankInfo : IChecksum
    {



        #region Ctor - RankInfo
        public RankInfo(INIEntity rankEnt)
        {
            ETime = TimeInt(rankEnt["ParTimeEasy"]);
            MTime = TimeInt(rankEnt["ParTimeMedium"]);
            HTime = TimeInt(rankEnt["ParTimeHard"]);
            TitleUnder = new CsfString(rankEnt["UnderParTitle"]);
            TitleOver = new CsfString(rankEnt["OverParTitle"]);
            MsgUnder = new CsfString(rankEnt["UnderParMessage"]);
            MsgOver = new CsfString(rankEnt["OverParMessage"]);
        }
        public RankInfo()
        {
            TitleUnder = new CsfString(Constant.VALUE_NONE);
            TitleOver = new CsfString(Constant.VALUE_NONE);
            MsgUnder = new CsfString(Constant.VALUE_NONE);
            MsgOver = new CsfString(Constant.VALUE_NONE);
        }
        #endregion



        public int GetChecksum()
        {
            unchecked
            {
                int hash = Constant.BASE_HASH;
                hash = hash * 11 + ETime;
                hash = hash * 11 + MTime;
                hash = hash * 11 + HTime;
                hash = hash * 11 + TitleUnder.Id.GetHashCode();
                hash = hash * 11 + TitleOver.Id.GetHashCode();
                hash = hash * 11 + MsgUnder.Id.GetHashCode();
                hash = hash * 11 + MsgOver.Id.GetHashCode();
                return hash;
            }
        }


        #region Public Calls - RankInfo
        public int ETime { get; set; }
        public int MTime { get; set; }
        public int HTime { get; set; }
        public CsfString TitleUnder { get; set; }
        public CsfString TitleOver { get; set; }
        public CsfString MsgUnder { get; set; }
        public CsfString MsgOver { get; set; }
        #endregion
    }


    public class HeaderInfo : IChecksum
    {
        public HeaderInfo(INIEntity entHeader)
        {
            Data = entHeader;
        }
        public HeaderInfo()
        {
            Data = new INIEntity("Header");
        }

        public int GetChecksum()
        {
            return Data.GetChecksum();
        }

        public INIEntity Data { get; set; }
    }
}
