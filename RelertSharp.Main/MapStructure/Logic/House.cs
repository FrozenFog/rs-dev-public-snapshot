﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using RelertSharp.MapStructure.Points;
using RelertSharp.Utils;

namespace RelertSharp.MapStructure.Logic
{
    public class HouseCollection : TeamLogicCollection<HouseItem>
    {
        #region Ctor - HouseCollection
        public HouseCollection() { }
        #endregion


        #region Public Methods - HouseCollection
        public IEnumerable<TechnoPair> ToTechno()
        {
            List<TechnoPair> result = new List<TechnoPair>();
            foreach (string id in Keys)
            {
                result.Add(new TechnoPair(id, this[id].ID));
            }
            return result;
        }
        public HouseItem GetHouse(string name)
        {
            foreach (HouseItem h in this)
            {
                if (h.Name == name) return h;
            }
            return null;
        }
        #endregion
    }


    public class HouseItem : TeamLogicItem
    {
        private List<string> alliesWith;
        private Dictionary<string, INIPair> residual;


        #region Ctor - HouseItem
        public HouseItem(INIEntity ent) : base(ent)
        {
            IQ = ent.PopPair("IQ").ParseInt();
            Edge = (HouseEdges)Enum.Parse(typeof(HouseEdges), ent.PopPair("Edge").Value, true);
            ColorName = ent.PopPair("Color").Value;
            alliesWith = ent.PopPair("Allies").ParseStringList().ToList();
            Country = ent.PopPair("Country").Value;
            Credits = ent.PopPair("Credits").ParseInt();
            TechLevel = ent.PopPair("TechLevel").ParseInt();
            PlayerControl = ent.PopPair("PlayerControl").ParseBool();
            PercentBuilt = ent.PopPair("PercentBuilt").ParseFloat();
            NodeCounts = ent.PopPair("NodeCount").ParseInt();
            for (int i = 0; i < NodeCounts; i++)
            {
                string num = string.Format("{0:D3}", i);
                string[] tmp = ent.PopPair(num).ParseStringList();
                BaseNodes.Add(new BaseNode(tmp[0], int.Parse(tmp[1]), int.Parse(tmp[2])));
            }
            residual = ent.DictData;
            GetToUnit = new HouseUnit(this);
        }
        public HouseItem() { }
        #endregion


        #region Public Methods - HouseItem
        public override string ToString()
        {
            return Name;
        }
        public void SetFromUnit(HouseUnit unit)
        {
            IQ = (int)unit.Data["IQ"].Value;
            Edge = (HouseEdges)unit.Data["Edge"].Value;
            PercentBuilt = (double)unit.Data["PercentBuilt"].Value;
            ColorName = (string)unit.Data["ColorName"].Value;
            Country = (string)unit.Data["Country"].Value;
            Credits = (int)unit.Data["Credits"].Value;
            TechLevel = (int)unit.Data["TechLevel"].Value;
            PlayerControl = (bool)unit.Data["PlayerControl"].Value;
            NodeCounts = (int)unit.Data["NodeCounts"].Value;
            GetToUnit = new HouseUnit(this);
        }
        #endregion


        #region Public Calls - HouseItem
        public List<BaseNode> BaseNodes { get; private set; } = new List<BaseNode>();
        public int IQ { get; set; }
        public HouseEdges Edge { get; set; }
        public string ColorName { get; set; }
        public List<string> AlliesWith
        {
            get { return alliesWith; }
            set { alliesWith = value; }
        }
        public string Country { get; set; }
        public int Credits { get; set; }
        public int TechLevel { get; set; }
        public bool PlayerControl { get; set; }
        public int NodeCounts { get; set; }
        public double PercentBuilt { get; set; }
        public System.Drawing.Color DrawingColor { get; set; }
        public string Name { get { return ID; } set { ID = value; } }
        public HouseUnit GetToUnit { get; set; }
        #endregion
    }


    public class HouseUnit
    {
        public struct HouseShowUnit
        {
            public object Value;
            public string ShowName;
            public HouseShowUnit(string displayName, object value) { Value = value;ShowName = displayName; }
        }

        #region Ctor - HouseUnit
        public HouseUnit(HouseItem item)
        {
            Data.Add("IQ", new HouseShowUnit(Language.DICT["LGColvRowHouseIQ"], item.IQ));
            Data.Add("Edge", new HouseShowUnit(Language.DICT["LGColvRowHouseEdge"], item.Edge));
            Data.Add("PercentBuilt", new HouseShowUnit(Language.DICT["LGColvRowHousePercentBuilt"], item.PercentBuilt));
            Data.Add("ColorName", new HouseShowUnit(Language.DICT["LGColvRowHouseColorName"], item.ColorName));
            Data.Add("Country", new HouseShowUnit(Language.DICT["LGColvRowHouseCountry"], item.Country));
            Data.Add("Credits", new HouseShowUnit(Language.DICT["LGColvRowHouseCredits"], item.Credits));
            Data.Add("TechLevel", new HouseShowUnit(Language.DICT["LGColvRowHouseTechLevel"], item.TechLevel));
            Data.Add("PlayerControl", new HouseShowUnit(Language.DICT["LGColvRowHousePlayerControl"], item.PlayerControl));
            Data.Add("NodeCounts", new HouseShowUnit(Language.DICT["LGColvRowHouseNodeCounts"], item.NodeCounts));
        }
        #endregion

        #region Public Calls - HouseUnit
        public Dictionary<string, HouseShowUnit> Data { get; set; } = new Dictionary<string, HouseShowUnit>();
        #endregion
    }
}
