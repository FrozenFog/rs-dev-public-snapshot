using RelertSharp.Common;
using RelertSharp.IniSystem;
using System;
using System.Collections.Generic;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure.Logic
{
    [IniEntitySerialize(Constant.MapStructure.ENT_AITRG)]
    public class AITriggerCollection : IndexableItemCollection<AITriggerItem>, ICurdContainer<AITriggerItem>, IIniEntitySerializable
    {
        private Dictionary<string, bool> globalEnables = new Dictionary<string, bool>();


        #region Ctor - AITriggerCollection
        public AITriggerCollection() { }
        public void ReadFromIni(INIEntity src)
        {
            foreach (INIPair p in src)
            {
                AITriggerItem item = new AITriggerItem();
                item.ReadFromIni(p);
                this[p.Name] = item;
            }
        }

        public INIEntity SaveAsIni()
        {
            INIEntity ent = this.GetNamedEnt();
            foreach (AITriggerItem item in this) ent.AddPair(item.SaveAsIni());
            return ent;
        }

        internal void SetEnables(INIEntity globalEnable)
        {
            foreach (INIPair p in globalEnable)
            {
                GlobalEnables[p.Name] = p.ParseBool();
            }
        }
        internal INIEntity DumpEnables()
        {
            INIEntity ent = new INIEntity(Constant.MapStructure.ENT_AI_ENABLE);
            foreach (AITriggerItem item in this)
            {
                INIPair p = new INIPair(item.Name)
                {
                    Value = item.Enabled.YesNo()
                };
                ent.AddPair(p);
            }
            return ent;
        }
        #endregion

        #region Public Methods - AITriggerCollection
        internal void Clear()
        {
            data.Clear();
        }

        public AITriggerItem AddItem(string id, string name)
        {
            AITriggerItem item = new AITriggerItem(id, name);
            this[id] = item;
            GlobalEnables[id] = true;
            return item;
        }

        public AITriggerItem CopyItem(AITriggerItem src, string id)
        {
            AITriggerItem copy = new AITriggerItem(src, id);
            this[id] = copy;
            GlobalEnables[id] = true;
            return copy;
        }

        public bool ContainsItem(AITriggerItem look)
        {
            return data.ContainsKey(look.Id);
        }

        public bool ContainsItem(string id, string param2)
        {
            return data.ContainsKey(id);
        }

        public bool RemoveItem(AITriggerItem target)
        {
            if (ContainsItem(target))
            {
                data.Remove(target.Id);
                globalEnables.Remove(target.Id);
                return true;
            }
            return false;
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


    public class AITriggerItem : IndexableItem, ILogicItem, IIniPairSerializable
    {
        #region Ctor - AITriggerItem
        public AITriggerItem()
        {

        }
        public AITriggerItem(string _id, string _name)
        {
            Id = _id;
            Name = _name;
        }
        public AITriggerItem(AITriggerItem src, string id)
        {
            Id = id;
            Name = src.Name + Constant.CLONE_SUFFIX;
            Team1ID = src.Team1ID;
            Team2ID = src.Team2ID;
            OwnerHouse = src.OwnerHouse;
            TechLevel = src.TechLevel;
            ConditionObjID = src.ConditionObjID;
            ConditionType = src.ConditionType;
            Comparator = new AITriggerComparator(src.Comparator);
            StartingWeight = src.StartingWeight;
            MinimumWeight = src.MinimumWeight;
            MaximumWeight = src.MaximumWeight;
            IsForSkirmish = src.IsForSkirmish;
            Residual = "0";
            SideIndex = src.SideIndex;
            IsBaseDefense = src.IsBaseDefense;
            EasyOn = src.EasyOn;
            NormalOn = src.NormalOn;
            HardOn = src.HardOn;
            Enabled = true;
        }
        public void ReadFromIni(INIPair src)
        {
            Id = src.Name;
            ParameterReader reader = new ParameterReader(src.ParseStringList());
            Name = reader.ReadString();
            Team1ID = reader.ReadString();
            OwnerHouse = reader.ReadString();
            TechLevel = reader.ReadInt();
            ConditionType = (AITriggerConditionType)reader.ReadInt();
            ConditionObjID = reader.ReadString();
            Comparator = new AITriggerComparator(reader.ReadString());
            StartingWeight = reader.ReadFloat();
            MinimumWeight = reader.ReadFloat();
            MaximumWeight = reader.ReadFloat();
            IsForSkirmish = reader.ReadBool();
            reader.Skip();
            SideIndex = reader.ReadInt();
            IsBaseDefense = reader.ReadBool();
            Team2ID = reader.ReadString();
            EasyOn = reader.ReadBool();
            NormalOn = reader.ReadBool();
            HardOn = reader.ReadBool();
            Enabled = false;
            if (reader.ReadError) GlobalVar.Monitor.LogCritical(Id, Name, LogicType.AiTrigger, this);
        }
        public INIPair SaveAsIni()
        {
            INIPair p = new INIPair(Id);
            ParameterWriter writer = new ParameterWriter();
            writer.Write(Name);
            writer.Write(Team1ID);
            writer.Write(OwnerHouse);
            writer.Write(TechLevel);
            writer.Write((int)ConditionType);
            writer.Write(ConditionObjID);
            writer.Write(Comparator.ToCmpString);
            writer.Write(StartingWeight);
            writer.Write(MinimumWeight);
            writer.Write(MaximumWeight);
            writer.Write(IsForSkirmish);
            writer.Write(Residual);
            writer.Write(SideIndex);
            writer.Write(IsBaseDefense);
            writer.Write(Team2ID);
            writer.Write(EasyOn);
            writer.Write(NormalOn);
            writer.Write(HardOn);
            p.Value = writer.ToString();
            return p;
        }
        #endregion


        #region Public Methods - AITriggerItem
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
        public string OwnerHouse { get; set; } = Constant.ITEM_ALL;
        public int TechLevel { get; set; }
        public LogicType ItemType { get { return LogicType.AiTrigger; } }
        public AITriggerConditionType ConditionType { get; set; } = AITriggerConditionType.ConditionTrue;
        public string ConditionObjID { get; set; } = Constant.ITEM_NONE;
        public AITriggerConditionOperator Operator { get { return Comparator.Operator; } set { Comparator.Operator = value; } }
        public AITriggerComparator Comparator { get; set; } = new AITriggerComparator();
        public float StartingWeight { get; set; }
        public float MinimumWeight { get; set; }
        public float MaximumWeight { get; set; }
        public bool IsForSkirmish { get; set; }
        public string Residual { get; private set; } = "0";
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
        public AITriggerComparator(AITriggerComparator src)
        {
            Num1 = src.Num1;
            Operator = src.Operator;
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