using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using RelertSharp.MapStructure.Points;

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
        private List<BaseNode> baseNodes = new List<BaseNode>();
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
                baseNodes.Add(new BaseNode(tmp[0], int.Parse(tmp[1]), int.Parse(tmp[2])));
            }
            residual = ent.DictData;
        }
        #endregion


        #region Public Methods - HouseItem
        public override string ToString()
        {
            return Name;
        }
        #endregion


        #region Public Calls - HouseItem
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
        #endregion
    }
}
