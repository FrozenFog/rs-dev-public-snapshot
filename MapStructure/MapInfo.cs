using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using relert_sharp.FileSystem;

namespace relert_sharp.MapStructure
{
    public class MapInfo
    {
        private static int iniformat = 4;
        private INIEntity _basic, _map, _specialFlags;
        private Dictionary<string, INIEntity> _residual = new Dictionary<string, INIEntity>();
        public string MapName, ThemeName, PostScoreFilmName, AltNextScene, TheaterName, PlayerHouseName;
        public string[] GameModes;
        //public bool Official, IceGrowth, VeinGrowth, TibGrowth, IgnoreGlobalAI, DeathToVisc, FreeRadar, EndOfGame, SkipScore, OneTime, SkipSelect,
        //    TruckCrate, TrainCrate, MultiOnly, Inert, FogOfWar, IonStorms, MCVDeploy, Meteories, Visc, FixAlliance, InitVeteran, HarvesterImmune, TibSpread, TibExplode,
        //    DestroyableBridge, SP_TibGrowth;
        //public int MaxPlayer, MinPlayer, InitTime, AltHomeCell, CarryOverCap, Percent, HomeCell;
        //public float CarryOverMoney;
        public Rectangle Size, LocalSize;
        public MapInfo(INIEntity Basic, INIEntity MapSize, INIEntity SpecialFlags)
        {
            _basic = Basic;
            _map = MapSize;
            _specialFlags = SpecialFlags;
            //string value
            MapName = Basic.GetPair("Name").Value;
            ThemeName = Basic.GetPair("Theme").Value;
            PostScoreFilmName = Basic.GetPair("PostScore").Value;
            AltNextScene = Basic.GetPair("AltNextScenario").Value;
            TheaterName = MapSize.GetPair("Theater").Value;
            PlayerHouseName = Basic.GetPair("Player").Value;
            GameModes = Basic.GetPair("GameMode").ParseStringList();
            ////bool value
            //Official = Basic.GetPair("Official").ParseBool();
            //IceGrowth = Basic.GetPair("IceGrowthEnabled").ParseBool();
            //VeinGrowth = Basic.GetPair("VeinGrowthEnabled").ParseBool();
            //TibGrowth = Basic.GetPair("TiberiumGrowthEnabled").ParseBool(true);
            //IgnoreGlobalAI = Basic.GetPair("IgnoreGlobalAITriggers").ParseBool();
            //DeathToVisc = Basic.GetPair("TiberiumDeathToVisceroid").ParseBool();
            //FreeRadar = Basic.GetPair("FreeRadar").ParseBool();
            //EndOfGame = Basic.GetPair("EndOfGame").ParseBool();
            //SkipScore = Basic.GetPair("SkipScore").ParseBool();
            //OneTime = Basic.GetPair("OneTimeOnly").ParseBool();
            //SkipSelect = Basic.GetPair("SkipMapSelect").ParseBool();
            //TruckCrate = Basic.GetPair("TruckCrate").ParseBool(true);
            //TrainCrate = Basic.GetPair("TrainCrate").ParseBool(true);
            //MultiOnly = Basic.GetPair("MultiplayerOnly").ParseBool(true);
            //Inert = SpecialFlags.GetPair("Inert").ParseBool();
            //FogOfWar = SpecialFlags.GetPair("FogOfWar").ParseBool();
            //IonStorms = SpecialFlags.GetPair("IonStorms").ParseBool();
            //MCVDeploy = SpecialFlags.GetPair("MCVDeploy").ParseBool();
            //Meteories = SpecialFlags.GetPair("Meteorites").ParseBool();
            //Visc = SpecialFlags.GetPair("Visceroids").ParseBool();
            //FixAlliance = SpecialFlags.GetPair("FixedAlliance").ParseBool();
            //SP_TibGrowth = SpecialFlags.GetPair("TiberiumGrows").ParseBool(true);
            //InitVeteran = SpecialFlags.GetPair("InitialVeteran").ParseBool();
            //HarvesterImmune = SpecialFlags.GetPair("HarvesterImmune").ParseBool();
            //TibSpread = SpecialFlags.GetPair("TiberiumSpreads").ParseBool(true);
            //TibExplode = SpecialFlags.GetPair("TiberiumExplosive").ParseBool();
            //DestroyableBridge = SpecialFlags.GetPair("DestroyableBridges").ParseBool(true);
            ////int value
            //AltHomeCell = Basic.GetPair("AltHomeCell").ParseInt(99);
            //HomeCell = Basic.GetPair("HomeCell").ParseInt(98);
            //Percent = Basic.GetPair("Percent").ParseInt();
            //InitTime = Basic.GetPair("InitTime").ParseInt(10000);
            //MaxPlayer = Basic.GetPair("MaxPlayer").ParseInt(2);
            //MinPlayer = Basic.GetPair("MinPlayer").ParseInt(2);
            //CarryOverCap = Basic.GetPair("CarryOverCap").ParseInt();
            //CarryOverMoney = Basic.GetPair("CarryOverMoney").ParseFloat();
            //size data
            int[] buf = MapSize.GetPair("Size").ParseIntList();
            Size = new Rectangle(buf[0], buf[1], buf[2], buf[3]);
            buf = MapSize.GetPair("LocalSize").ParseIntList();
            LocalSize = new Rectangle(buf[0], buf[1], buf[2], buf[3]);
        }
        public void AddInfo(INIEntity ent)
        {
            if (_residual.Keys.Contains(ent.Name)) return;
            _residual[ent.Name] = ent;
        }
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
    }


    public class Lightning
    {
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


        #region Public Calls - Lightning
        public LightningItem Normal { get; set; }
        public LightningItem Ion { get; set; }
        public LightningItem Dominator { get; set; }
        public double DominatorChangeRate { get; set; }
        #endregion
    }


    public class LightningItem
    {
        public LightningItem(string _R, string _G, string _B, string _level, string _ground, string _ambient)
        {
            Red = double.Parse(_R);
            Green = double.Parse(_G);
            Blue = double.Parse(_B);
            Level = double.Parse(_level);
            Ground = double.Parse(_ground);
            Ambient = double.Parse(_ambient);
        }


        #region Public Calls - LightningItem
        public double Red { get; set; }
        public double Green { get; set; }
        public double Blue { get; set; }
        public double Level { get; set; }
        public double Ground { get; set; }
        public double Ambient { get; set; }
        #endregion
    }
}
