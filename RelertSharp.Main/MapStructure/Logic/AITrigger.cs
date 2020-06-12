using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.Model;
using RelertSharp.Utils;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure.Logic
{
    public class AITriggerCollection : IEnumerable<AITriggerItem>
    {
        private Dictionary<string, AITriggerItem> localTriggers = new Dictionary<string, AITriggerItem>();
        private Dictionary<string, bool> globalEnables = new Dictionary<string, bool>();


        #region Ctor - AITriggerCollection
        public AITriggerCollection() { }
        #endregion

        #region Public Methods - AITriggerCollection
        public AITriggerItem NewAITrigger(string id)
        {
            AITriggerItem item = new AITriggerItem();
            item.ID = id;
            item.Name = "New AI Trigger";
            item.Team1ID = item.Team2ID = "<none>";
            item.OwnerHouse = "<all>";
            item.ConditionObjID = "<none>";
            item.TechLevel = 0;
            item.SideIndex = 0;
            item.StartingWeight = 50D;
            item.MinimumWeight = 30D;
            item.MaximumWeight = 50D;
            item.Enabled = item.IsForSkirmish = item.EasyOn = item.NormalOn = item.HardOn = true;
            item.Comparator = new AITriggerComparator(@"0000000000000000000000000000000000000000000000000000000000000000");
            item.ConditionType = AITriggerConditionType.ConditionTrue;
            item.GetToUnit = new AITriggerUnit(item);
            return item;
        }
        public bool Remove(string ID)
        {
            if (!localTriggers.ContainsKey(ID)) return false;
            return localTriggers.Remove(ID);
        }
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
        #region Enumerator
        public IEnumerator<AITriggerItem> GetEnumerator()
        {
            return localTriggers.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return localTriggers.Values.GetEnumerator();
        }
        #endregion
        #endregion
    }


    public class AITriggerItem : BindableBase, ILogicItem
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
            try
            {
                if (_args.Length != 18)
                {
                    throw new Exception();
                }
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
                GetToUnit = new AITriggerUnit(this);
            }
            catch
            {
                GlobalVar.Log.Critical(string.Format("AITrigger item id: {0} has unreadable data, please verify in map file!", _id));
            }
        }
        #endregion

        #region Public Methods - AITriggerItem
        public AITriggerItem Copy(string newID)
        {
            AITriggerItem item = new AITriggerItem();
            item.ID = newID;
            item.name = name + " Clone";
            item.team1id = team1id;
            item.team2id = team2id;
            item.ownerhouse = ownerhouse;
            item.condObjID = condObjID;
            item.tech = tech;
            item.sideindex = sideindex;
            item.startwg = startwg;
            item.minwg = minwg; 
            item.maxwg = maxwg;
            item.enabled = enabled;
            item.skirmish = skirmish;
            item.hd = hd;
            item.ez = ez;
            item.nm = nm;
            item.comparator = comparator;
            item.condtype = condtype;
            item.GetToUnit = new AITriggerUnit(item);
            return item;
        }
        public void SetFromUnit(AITriggerUnit unit)
        {
            name = (string)unit.Data["Name"].Value;
            team1id = (string)unit.Data["Team1"].Value;
            team2id = (string)unit.Data["Team2"].Value;
            ownerhouse = (string)unit.Data["Owner"].Value;
            tech = (int)unit.Data["TechLevel"].Value;
            sideindex = (int)unit.Data["SideIndex"].Value;
            startwg = (double)unit.Data["StartingWeight"].Value;
            minwg = (double)unit.Data["MinimumWeight"].Value;
            maxwg = (double)unit.Data["MaximumWeight"].Value;
            enabled = (bool)unit.Data["Enabled"].Value;
            ez = (bool)unit.Data["Easy"].Value;
            nm= (bool)unit.Data["Normal"].Value;
            hd = (bool)unit.Data["Hard"].Value;
            skirmish= (bool)unit.Data["Skirmish"].Value;
            condObjID = (string)unit.Data["CondObj"].Value;
            condtype = (AITriggerConditionType)unit.Data["Condition"].Value;
            comparator.Num1 = (int)unit.Data["OperNum"].Value;
            comparator.Operator = (AITriggerConditionOperator)unit.Data["Operator"].Value;
        }
        #endregion

        #region Public Calls - AITriggerItem
        public override string ToString()
        {
            return ID + " " + Name;
        }
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
        public AITriggerUnit GetToUnit { get; set; }
        #endregion
    }


    public class AITriggerUnit
    {
        public struct AITriggerShowItem
        {
            public string ShowName;
            public object Value;
            public AITriggerShowItem(string displayName,object value) { ShowName = displayName;Value = value; }
        }

        #region Ctor - AITriggerUnit
        public AITriggerUnit(AITriggerItem item)
        {
            Data["Name"] = new AITriggerShowItem(Language.DICT["LGColvRowAIName"], item.Name);
            Data["Team1"] = new AITriggerShowItem(Language.DICT["LGColvRowAITeam1"], item.Team1ID);
            Data["Team2"] = new AITriggerShowItem(Language.DICT["LGColvRowAITeam2"], item.Team2ID);
            Data["Owner"] = new AITriggerShowItem(Language.DICT["LGColvRowAIOwner"], item.OwnerHouse);
            Data["TechLevel"] = new AITriggerShowItem(Language.DICT["LGColvRowAITechLevel"], item.TechLevel);
            Data["SideIndex"]= new AITriggerShowItem(Language.DICT["LGColvRowAISideIndex"], item.SideIndex);
            Data["StartingWeight"] = new AITriggerShowItem(Language.DICT["LGColvRowAIStartingWeight"], item.StartingWeight);
            Data["MinimumWeight"] = new AITriggerShowItem(Language.DICT["LGColvRowAIMinimumWeight"], item.MinimumWeight);
            Data["MaximumWeight"] = new AITriggerShowItem(Language.DICT["LGColvRowAIMaximumWeight"], item.MaximumWeight);
            Data["Enabled"] = new AITriggerShowItem(Language.DICT["LGColvRowAIEnabled"], item.Enabled);
            Data["Skirmish"] = new AITriggerShowItem(Language.DICT["LGColvRowAISkirmish"], item.IsForSkirmish);
            Data["Easy"] = new AITriggerShowItem(Language.DICT["LGColvRowAIEasy"], item.EasyOn);
            Data["Normal"] = new AITriggerShowItem(Language.DICT["LGColvRowAINormal"], item.NormalOn);
            Data["Hard"] = new AITriggerShowItem(Language.DICT["LGColvRowAIHard"], item.HardOn);
            Data["OperNum"] = new AITriggerShowItem(Language.DICT["LGColvRowAIOperNum"], item.Comparator.Num1);
            Data["Operator"] = new AITriggerShowItem(Language.DICT["LGColvRowAIOperator"], item.Comparator.Operator);
            Data["Condition"] = new AITriggerShowItem(Language.DICT["LGColvRowAICondition"], item.ConditionType);
            Data["CondObj"] = new AITriggerShowItem(Language.DICT["LGColvRowAICondObj"], item.ConditionObjID);
        }
        #endregion

        #region Public Calls - AITriggerUnit
        public Dictionary<string, AITriggerShowItem> Data { get; set; } = new Dictionary<string, AITriggerShowItem>();
        #endregion
    }

    public class AITriggerComparator : BindableBase
    {
        private int num1;
        private AITriggerConditionOperator oper;


        #region Ctor - AITriggerComparator
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
        public string ToCmpString { get { return ToLEByteString(Num1) + ToLEByteString((int)Operator) + Zeros; } }
        public static string Zeros { get { return @"000000000000000000000000000000000000000000000000"; } }
        #endregion
    }
}