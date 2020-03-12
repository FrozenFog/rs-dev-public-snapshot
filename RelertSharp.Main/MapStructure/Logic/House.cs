using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.IniSystem;
using relert_sharp.Common;
using relert_sharp.MapStructure.Points;

namespace relert_sharp.MapStructure.Logic
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
        #endregion
    }
}
