﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using relert_sharp.FileSystem;
using relert_sharp.Common;
using static relert_sharp.Utils.Misc;

namespace relert_sharp.MapStructure
{
    public class MapInfo
    {
        private static int iniformat = 4;
        private INIEntity _basic, _map, _specialFlags;
        private Dictionary<string, INIEntity> _residual = new Dictionary<string, INIEntity>();
        public string MapName, ThemeName, PostScoreFilmName, AltNextScene, TheaterName, PlayerHouseName;
        public string[] GameModes;
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


    public class RankInfo
    {
        public RankInfo(INIEntity rankEnt)
        {
            ETime = TimeInt(rankEnt["ParTimeEasy"]);
            MTime = TimeInt(rankEnt["ParTimeMedium"]);
            HTime = TimeInt(rankEnt["ParTimeHard"]);
            TitleUnder = new CSFString(rankEnt["UnderParTitle"]);
            TitleOver = new CSFString(rankEnt["OverParTitle"]);
            MsgUnder = new CSFString(rankEnt["UnderParMessage"]);
            MsgOver = new CSFString(rankEnt["OverParMessage"]);
        }


        #region Public Calls - RankInfo
        public int ETime { get; set; }
        public int MTime { get; set; }
        public int HTime { get; set; }
        public CSFString TitleUnder { get; set; }
        public CSFString TitleOver { get; set; }
        public CSFString MsgUnder { get; set; }
        public CSFString MsgOver { get; set; }
        #endregion
    }


    public class HeaderInfo
    {
        public HeaderInfo(INIEntity entHeader)
        {

        }
    }
}
