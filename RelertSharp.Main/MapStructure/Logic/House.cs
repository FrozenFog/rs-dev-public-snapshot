using RelertSharp.Common;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure.Points;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        /// <summary>
        /// Return -1 if not found
        /// </summary>
        /// <param name="houseName"></param>
        /// <returns></returns>
        public int IndexOf(string houseName)
        {
            for (int i = 0; i < data.Count; i++)
            {
                if (this[i.ToString()].Name == houseName) return i;
            }
            return -1;
        }
        #endregion
    }


    public class HouseItem : TeamLogicItem, IIndexableItem, ILogicItem
    {
        private List<string> alliesWith;
        private Dictionary<string, INIPair> residual;
        public event EventHandler ColorUpdated;
        public event EventHandler AllInfoUpdate;
        public event NameChangedHandler HouseNameChanged;
        private bool initialized = false;


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
                BaseNode node = new BaseNode(tmp[0], int.Parse(tmp[1]), int.Parse(tmp[2]), this);
                BaseNodes.Add(node);
            }
            residual = ent.DictData;
            initialized = true;
            //GetToUnit = new HouseUnit(this);
        }
        public HouseItem() { initialized = true; }
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
        public string[] ExtractParameter()
        {
            return new string[]
            {
                Name,
                Country,
                ColorName,
                TechLevel.ToString(),
                PercentBuilt.ToString(),
                PlayerControl.ZeroOne(),
                Edge.ToString(),
                BaseNodes.Count.ToString()
            };
        }
        public override string ToString()
        {
            return Name;
        }
        public BaseNode AddNewNode()
        {
            BaseNode node = new BaseNode(this);
            BaseNodes.Add(node);
            return node;
        }
        public void AddNode(BaseNode node)
        {
            BaseNodes.Add(node);
        }
        public void RemoveNode(BaseNode node)
        {
            BaseNodes.Remove(node);
        }
        internal void OnAllInfoUpdate()
        {
            AllInfoUpdate?.Invoke(null, null);
        }
        #endregion


        #region Public Calls - HouseItem
        public List<BaseNode> BaseNodes { get; private set; } = new List<BaseNode>();
        public int IQ { get; set; }
        public HouseEdges Edge { get; set; }
        private string _colorname;
        public string ColorName
        {
            get { return _colorname; }
            set
            {
                _colorname = value;
                if (GlobalVar.GlobalRules != null)
                {
                    INIPair p = GlobalVar.GlobalRules.GetColorInfo(_colorname);
                    if (p.Name == "") DrawingColor = Color.Red;
                    else
                    {
                        string[] hsb = p.ParseStringList();
                        DrawingColor = Utils.HSBColor.FromHSB(hsb);
                    }
                }
                ColorUpdated?.Invoke(null, null);
            }
        }
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
        public System.Drawing.Color DrawingColor { get; internal set; }
        public override string Value { get { return Id; } }
        public override string Name { get { return Id; } set { Id = value; } }
        public override string Id
        {
            get { return base.Id; }
            set
            {
                string prev = base.Id;
                base.Id = value;
                if (initialized) HouseNameChanged?.Invoke(this, prev, value);
            }
        }
        public LogicType ItemType { get { return LogicType.House; } }
        #endregion
    }
}
