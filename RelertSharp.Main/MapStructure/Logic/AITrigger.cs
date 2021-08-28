using RelertSharp.Common;
using System;
using System.Collections.Generic;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure.Logic
{
    public class AITriggerCollection : IndexableItemCollection<AITriggerItem>
    {
        private Dictionary<string, bool> globalEnables = new Dictionary<string, bool>();


        #region Ctor - AITriggerCollection
        public AITriggerCollection() { }
        #endregion

        #region Public Methods - AITriggerCollection
        public AITriggerItem NewAITrigger(string id)
        {
            AITriggerItem item = new AITriggerItem();
            item.Id = id;
            item.Name = "New AI Trigger";
            item.Team1ID = item.Team2ID = "<none>";
            item.OwnerHouse = "<all>";
            item.ConditionObjID = "<none>";
            item.TechLevel = 0;
            item.SideIndex = 0;
            item.StartingWeight = 50;
            item.MinimumWeight = 30;
            item.MaximumWeight = 50;
            item.Enabled = item.IsForSkirmish = item.EasyOn = item.NormalOn = item.HardOn = true;
            item.Comparator = new AITriggerComparator(@"0000000000000000000000000000000000000000000000000000000000000000");
            item.ConditionType = AITriggerConditionType.ConditionTrue;
            return item;
        }
        public bool Remove(string ID)
        {
            if (!data.ContainsKey(ID)) return false;
            return data.Remove(ID);
        }
        #endregion

        #region Public Calls - AITriggerCollection
        public Dictionary<string, bool> GlobalEnables
        {
            get { return globalEnables; }
            set { globalEnables = value; }
        }
        #endregion
    }


    public class AITriggerItem : IndexableItem, ILogicItem
    {
        #region Ctor - AITriggerItem
        public AITriggerItem()
        {

        }
        public AITriggerItem(string _id, string[] _args)
        {
            try
            {
                if (_args.Length != Constant.MapStructure.ArgLenAiTrigger)
                {
                    throw new Exception();
                }
                Id = _id;
                Name = _args[0];
                Team1ID = _args[1];
                OwnerHouse = _args[2];
                TechLevel = int.Parse(_args[3]);
                ConditionType = (AITriggerConditionType)(int.Parse(_args[4]));
                ConditionObjID = _args[5];
                Comparator = new AITriggerComparator(_args[6]);
                StartingWeight = (int)double.Parse(_args[7]);
                MinimumWeight = (int)double.Parse(_args[8]);
                MaximumWeight = (int)double.Parse(_args[9]);
                IsForSkirmish = IniParseBool(_args[10]);
                Residual = "0";
                SideIndex = int.Parse(_args[12]);
                IsBaseDefense = true;
                Team2ID = _args[14];
                EasyOn = IniParseBool(_args[15]);
                NormalOn = IniParseBool(_args[16]);
                HardOn = IniParseBool(_args[17]);
                Enabled = false;
            }
            catch
            {
                GlobalVar.Log.Critical(string.Format("AITrigger item id: {0} has unreadable data, please verify in map file!", _id));
            }
        }
        #endregion


        #region Public Methods - AITriggerItem
        public string GetSaveData()
        {
            List<object> objs = new List<object>();
            objs.AddRange(new object[] { Name, Team1ID, OwnerHouse, TechLevel, (int)ConditionType, ConditionObjID });
            string oper = Comparator.ToCmpString;
            objs.AddRange(new object[] { oper, (double)StartingWeight, (double)MinimumWeight, (double)MaximumWeight, IsForSkirmish, 0, SideIndex, IsBaseDefense, Team2ID, EasyOn, NormalOn, HardOn });
            return objs.JoinBy();
        }
        public string[] ExtractParameter()
        {
            return new string[]
            {
                Name,
                Team1ID,
                OwnerHouse,
                TechLevel.ToString(),
                ((int)ConditionType).ToString(),
                ConditionObjID,
                Comparator.ToCmpString,
                StartingWeight.ToString(),
                MinimumWeight.ToString(),
                IsForSkirmish.ZeroOne(),
                "0",
                SideIndex.ToString(),
                IsBaseDefense.ZeroOne(),
                Team2ID,
                EasyOn.ZeroOne(),
                NormalOn.ZeroOne(),
                HardOn.ZeroOne()
            };

        }
        public int GetChecksum()
        {
            return ExtractParameter().GetHashCode();
        }
        #endregion


        #region Public Calls - AITriggerItem

        public bool Enabled { get; set; }
        public string Team1ID { get; set; } = Constant.ITEM_NONE;
        public string OwnerHouse { get; set; }
        public int TechLevel { get; set; }
        public LogicType ItemType { get { return LogicType.AiTrigger; } }
        public AITriggerConditionType ConditionType { get; set; } = AITriggerConditionType.ConditionTrue;
        public string ConditionObjID { get; set; }
        public AITriggerConditionOperator Operator { get { return Comparator.Operator; } set { Comparator.Operator = value; } }
        public AITriggerComparator Comparator { get; set; } = new AITriggerComparator();
        public int StartingWeight { get; set; }
        public int MinimumWeight { get; set; }
        public int MaximumWeight { get; set; }
        public bool IsForSkirmish { get; set; }
        public string Residual { get; private set; }
        public int SideIndex { get; set; }
        public bool IsBaseDefense { get; set; }
        public string Team2ID { get; set; } = Constant.ITEM_NONE;
        public bool EasyOn { get; set; }
        public bool NormalOn { get; set; }
        public bool HardOn { get; set; }
        #endregion
    }

    public class AITriggerComparator
    {


        #region Ctor - AITriggerComparator
        public AITriggerComparator(string cmpString)
        {
            Num1 = FromLEByteString(cmpString.Substring(0, 8));
            Operator = (AITriggerConditionOperator)(FromLEByteString(cmpString.Substring(8, 8)));
        }
        public AITriggerComparator()
        {
            Num1 = 0;
            Operator = AITriggerConditionOperator.LessThan;
        }
        #endregion


        #region Public Calls - AITriggerComparator
        public int Num1 { get; set; } = 0;
        public AITriggerConditionOperator Operator { get; set; } = AITriggerConditionOperator.LessThan;
        public string ToCmpString { get { return ToLEByteString(Num1) + ToLEByteString((int)Operator) + Zeros; } }
        public static string Zeros { get { return @"000000000000000000000000000000000000000000000000"; } }
        #endregion
    }
}