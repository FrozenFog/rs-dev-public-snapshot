using RelertSharp.Common;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure.Points;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RelertSharp.MapStructure.Logic
{
    public class HouseCollection : TeamLogicCollection<HouseItem>
    {
        #region Ctor - HouseCollection
        public HouseCollection() { }
        #endregion


        #region Public Methods - HouseCollection
        //public IEnumerable<TechnoPair> ToTechno()
        //{
        //    List<TechnoPair> result = new List<TechnoPair>();
        //    foreach (string id in AllId)
        //    {
        //        result.Add(new TechnoPair(id, this[id].Id));
        //    }
        //    return result;
        //}
        public HouseItem GetHouse(string name)
        {
            foreach (HouseItem h in this)
            {
                if (h.Name == name) return h;
            }
            return null;
        }
        public int NewIndex()
        {
            for (int i = 0; i < int.MaxValue; ++i)
                if (!Exists(i.ToString())) return i;
            return -1;
        }
        #endregion
    }


    public class HouseItem : TeamLogicItem, IIndexableItem
    {
        private List<string> alliesWith;
        private Dictionary<string, INIPair> residual;


        #region Ctor - HouseItem
        public HouseItem(INIEntity ent) : base(ent)
        {
            IQ = ent.PopPair("IQ").ParseInt();
            try
            {
                Edge = (HouseEdges)Enum.Parse(typeof(HouseEdges), ent.PopPair("Edge").Value, true);
            }
            catch
            {
                Edge = HouseEdges.North;
            }
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
                BaseNode node = new BaseNode(tmp[0], int.Parse(tmp[1]), int.Parse(tmp[2]));
                BaseNodes.Add(node);
            }
            residual = ent.DictData;
            //GetToUnit = new HouseUnit(this);
        }
        public HouseItem() { }
        #endregion


        #region Public Methods - HouseItem
        public INIEntity GetSaveData()
        {
            INIEntity data = new INIEntity(Name);
            data.AddPair("IQ", IQ);
            data.AddPair("Color", ColorName);
            data.AddPair("Country", Country);
            data.AddPair("Credits", Credits);
            data.AddPair("TechLevel", TechLevel);
            data.AddPair("PercentBuilt", PercentBuilt);
            data.AddPair("PlayerControl", PlayerControl.YesNo());
            string allies = AlliesWith.JoinBy();
            data.AddPair("Allies", allies);
            data.AddPair("Edge", Edge);
            data.AddPair(residual.Values);
            if (BaseNodes.Count > 0)
            {
                data.AddPair("NodeCount", BaseNodes.Count);
                for (int i = 0; i < BaseNodes.Count; i++)
                {
                    data.AddPair(string.Format("{0:D3}", i), BaseNodes[i].SaveData);
                }
            }
            return data;
        }
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
        public string Index { get; set; }
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
        public HouseUnit GetToUnit { get; set; }
        public override string Value { get { return Id; } }
        public override string Name { get { return Id; } }
        #endregion
    }


    public class HouseUnit
    {
        public struct HouseShowUnit
        {
            public object Value;
            public string ShowName;
            public HouseShowUnit(string displayName, object value) { Value = value; ShowName = displayName; }
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
