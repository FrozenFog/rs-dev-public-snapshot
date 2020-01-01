using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.Common;
using static relert_sharp.Utils.Misc;

namespace relert_sharp.MapStructure.Logic
{
    public class AITriggerCollection
    {
        private Dictionary<string, AITriggerItem> localTriggers = new Dictionary<string, AITriggerItem>();
        private Dictionary<string, bool> globalEnables = new Dictionary<string, bool>();


        #region Constructor - AITriggerCollection
        public AITriggerCollection() { }
        #endregion


        #region Public Calls - AITriggerCollection
        public AITriggerItem this[string id]
        {
            get
            {
                if (localTriggers.Keys.Contains(id)) return localTriggers[id];
                return null;
            }
            set { localTriggers[id] = value; }
        }
        public Dictionary<string, bool> GlobalEnables
        {
            get { return globalEnables; }
            set { globalEnables = value; }
        }
        #endregion
    }


    public class AITriggerItem
    {


        #region Constructor - AITriggerItem
        public AITriggerItem() { }
        public AITriggerItem(string _id, string[] _args)
        {
            ID = _id;
            Name = _args[0];
            Team1ID = _args[1];
            OwnerHouse = _args[2];
            TechLevel = int.Parse(_args[3]);
            ConditionType = (AITriggerConditionType)(int.Parse(_args[4]));
            ConditionObjID = _args[5];
            Comparator = new AITriggerComparator(_args[6]);
            StartingWeight = double.Parse(_args[7]);
            MinimumWeight = double.Parse(_args[8]);
            MaximumWeight = double.Parse(_args[9]);
            IsForSkirmish = ParseBool(_args[10]);
            residual = "0";
            SideIndex = int.Parse(_args[12]);
            IsBaseDefense = false;
            Team2ID = _args[14];
            EasyOn = ParseBool(_args[15]);
            NormalOn = ParseBool(_args[16]);
            HardOn = ParseBool(_args[17]);
            Enabled = false;
        }
        #endregion


        #region Public Calls - AITriggerItem
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }
        public string Team1ID { get; set; }
        public string OwnerHouse { get; set; }
        public int TechLevel { get; set; }
        public AITriggerConditionType ConditionType { get; set; }
        public string ConditionObjID { get; set; }
        public AITriggerComparator Comparator { get; set; }
        public double StartingWeight { get; set; }
        public double MinimumWeight { get; set; }
        public double MaximumWeight { get; set; }
        public bool IsForSkirmish { get; set; }
        public string residual { get; private set; }
        public int SideIndex { get; set; }
        public bool IsBaseDefense { get; private set; }
        public string Team2ID { get; set; }
        public bool EasyOn { get; set; }
        public bool NormalOn { get; set; }
        public bool HardOn { get; set; }
        #endregion
    }


    public class AITriggerComparator
    {


        #region Constructor AITriggerComparator
        public AITriggerComparator(string cmpString)
        {
            Num1 = FromLEByteString(cmpString.Substring(0, 8));
            Operator = (AITriggerConditionOperator)(FromLEByteString(cmpString.Substring(8, 8)));
        }
        #endregion


        #region Public Calls - AITriggerComparator
        public int Num1 { get; set; }
        public AITriggerConditionOperator Operator { get; set; }
        public static string Zeros { get { return @"000000000000000000000000000000000000000000000000"; } }
        #endregion
    }
}
