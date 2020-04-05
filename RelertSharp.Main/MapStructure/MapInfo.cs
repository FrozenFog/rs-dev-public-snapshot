using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using RelertSharp.FileSystem;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure
{
    public class MapInfo
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
            Size = new Rectangle(buf[0], buf[1], buf[2], buf[3]);
            buf = MapSize.PopPair("LocalSize").ParseIntList();
            LocalSize = new Rectangle(buf[0], buf[1], buf[2], buf[3]);
            BasicResidue = Basic;
            SpecialFlagsResidue = SpecialFlags;
        }
        #endregion


        #region Public Methods - MapInfo
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
        #endregion
    }


    public class Lightning
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
            DominatorChangeRate = double.Parse(ent["DominatorAmbientChangeRate"]);
        }
        #endregion


        #region Public Calls - Lightning
        public LightningItem Normal { get; set; }
        public LightningItem Ion { get; set; }
        public LightningItem Dominator { get; set; }
        public double DominatorChangeRate { get; set; }
        #endregion
    }


    public class LightningItem
    {



        #region Ctor - LightningItem
        public LightningItem(string _R, string _G, string _B, string _level, string _ground, string _ambient)
        {
            Red = double.Parse(_R);
            Green = double.Parse(_G);
            Blue = double.Parse(_B);
            Level = double.Parse(_level);
            Ground = double.Parse(_ground);
            Ambient = double.Parse(_ambient);
        }
        #endregion


        #region Public Calls - LightningItem
        public double Red { get; set; }
        public double Green { get; set; }
        public double Blue { get; set; }
        public double Level { get; set; }
        public double Ground { get; set; }
        public double Ambient { get; set; }
        #endregion
    }


    public class RankInfo
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
        #endregion


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


    public class HeaderInfo
    {
        public HeaderInfo(INIEntity entHeader)
        {

        }
    }
}
