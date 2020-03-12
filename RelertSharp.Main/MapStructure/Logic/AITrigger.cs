using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.Common;
using relert_sharp.Model;
using static relert_sharp.Utils.Misc;

namespace relert_sharp.MapStructure.Logic
{
    public class AITriggerCollection
    {
        private Dictionary<string, AITriggerItem> localTriggers = new Dictionary<string, AITriggerItem>();
        private Dictionary<string, bool> globalEnables = new Dictionary<string, bool>();


        #region Ctor - AITriggerCollection
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


    public class AITriggerItem : BindableBase
    {
        private string name, id, team1id, team2id, ownerhouse, condObjID;
        private int tech, sideindex;
        private double startwg, minwg, maxwg;
        private bool enabled, skirmish, ez, nm, hd;
        private AITriggerComparator comparator;
        private AITriggerConditionType condtype;


        #region Ctor - AITriggerItem
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
            Residual = "0";
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
        public bool Enabled
        {
            get { return enabled; }
            set { SetProperty(ref enabled, value); }
        }
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        public string ID
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }
        public string Team1ID
        {
            get { return team1id; }
            set { SetProperty(ref team1id, value); }
        }
        public string OwnerHouse
        {
            get { return ownerhouse; }
            set { SetProperty(ref ownerhouse, value); }
        }
        public int TechLevel
        {
            get { return tech; }
            set { SetProperty(ref tech, value); }
        }
        public AITriggerConditionType ConditionType
        {
            get { return condtype; }
            set { SetProperty(ref condtype, value); }
        }
        public string ConditionObjID
        {
            get { return condObjID; }
            set { SetProperty(ref condObjID, value); }
        }
        public AITriggerComparator Comparator
        {
            get { return comparator; }
            set { SetProperty(ref comparator, value); }
        }
        public double StartingWeight
        {
            get { return startwg; }
            set { SetProperty(ref startwg, value); }
        }
        public double MinimumWeight
        {
            get { return minwg; }
            set { SetProperty(ref minwg, value); }
        }
        public double MaximumWeight
        {
            get { return maxwg; }
            set { SetProperty(ref maxwg, value); }
        }
        public bool IsForSkirmish
        {
            get { return skirmish; }
            set { SetProperty(ref skirmish, value); }
        }
        public string Residual { get; private set; }
        public int SideIndex
        {
            get { return sideindex; }
            set { SetProperty(ref sideindex, value); }
        }
        public bool IsBaseDefense { get; private set; }
        public string Team2ID
        {
            get { return team2id; }
            set { SetProperty(ref team2id, value); }
        }
        public bool EasyOn
        {
            get { return ez; }
            set { SetProperty(ref ez, value); }
        }
        public bool NormalOn
        {
            get { return nm; }
            set { SetProperty(ref nm, value); }
        }
        public bool HardOn
        {
            get { return hd; }
            set { SetProperty(ref hd, value); }
        }
        #endregion
    }


    public class AITriggerComparator : BindableBase
    {
        private int num1;
        private AITriggerConditionOperator oper;


        #region Ctor AITriggerComparator
        public AITriggerComparator(string cmpString)
        {
            Num1 = FromLEByteString(cmpString.Substring(0, 8));
            Operator = (AITriggerConditionOperator)(FromLEByteString(cmpString.Substring(8, 8)));
        }
        #endregion


        #region Public Calls - AITriggerComparator
        public int Num1
        {
            get { return num1; }
            set { SetProperty(ref num1, value); }
        }
        public AITriggerConditionOperator Operator
        {
            get { return oper; }
            set { SetProperty(ref oper, value); }
        }
        public static string Zeros { get { return @"000000000000000000000000000000000000000000000000"; } }
        #endregion
    }
}
