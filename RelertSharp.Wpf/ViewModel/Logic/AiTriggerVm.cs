using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;
using static RelertSharp.Common.Constant;

namespace RelertSharp.Wpf.ViewModel
{
    internal class AiTriggerVm : BaseVm<AITriggerItem>
    {
        private ModConfig config { get { return GlobalVar.GlobalConfig.ModConfig; } }
        public GlobalTeamVm TeamCollections { get { return GlobalCollectionVm.Teams; } }
        public StaticCollectionVm StaticCollections { get { return GlobalCollectionVm.StaticCollections; } }
        public GlobalCountryVm OwnerCountries { get { return GlobalCollectionVm.Countries; } }
        private TeamCollection MapTeam { get { return GlobalVar.CurrentMapDocument.Map.Teams; } }
        public AiTriggerVm()
        {
            data = new AITriggerItem();
        }
        public AiTriggerVm(object obj) : base(obj) { }
        public string Name
        {
            get { return data.Name; }
            set
            {
                data.Name = value;
                SetProperty();
                GlobalCollectionVm.AiTriggers.UpdateDesignatedName();
            }
        }
        public string Id
        {
            get { return data.Id; }
        }
        public string PrimaryTeamId
        {
            get { return data.Team1ID; }
            set
            {
                data.Team1ID = value;
                SetProperty();
            }
        }
        public string SupportTeamId
        {
            get { return data.Team2ID; }
            set
            {
                data.Team2ID = value;
                SetProperty();
            }
        }
        public int TechLevel
        {
            get { return data.TechLevel; }
            set
            {
                data.TechLevel = value;
                SetProperty();
            }
        }
        public string TechType
        {
            get { return data.ConditionObjID; }
            set
            {
                data.ConditionObjID = value;
                SetProperty();
            }
        }
        public int ConditionAmount
        {
            get { return data.Comparator.Num1; }
            set
            {
                data.Comparator.Num1 = value;
                SetProperty();
            }
        }
        public int WeightInit
        {
            get { return data.StartingWeight; }
            set
            {
                data.StartingWeight = value;
                SetProperty();
            }
        }
        public int WeightMin
        {
            get { return data.MinimumWeight; }
            set
            {
                data.MinimumWeight = value;
                SetProperty();
            }
        }
        public int WeightMax
        {
            get { return data.MaximumWeight; }
            set
            {
                data.MaximumWeight = value;
                SetProperty();
            }
        }
        public bool EasyOn
        {
            get { return data.EasyOn; }
            set
            {
                data.EasyOn = value;
                SetProperty();
            }
        }
        public bool NormalOn
        {
            get { return data.NormalOn; }
            set
            {
                data.NormalOn = value;
                SetProperty();
            }
        }
        public bool HardOn
        {
            get { return data.HardOn; }
            set
            {
                data.HardOn = value;
                SetProperty();
            }
        }
        public bool Skirmish
        {
            get { return data.IsForSkirmish; }
            set
            {
                data.IsForSkirmish = value;
                SetProperty();
            }
        }
        public bool BaseDefense
        {
            get { return data.IsBaseDefense; }
            set
            {
                data.IsBaseDefense = value;
                SetProperty();
            }
        }
        public object ConditionItem
        {
            get
            {
                return StaticCollections.AiTriggerConditions.ValueEqual((int)data.ConditionType);
            }
            set
            {
                if (value == null) data.ConditionType = AITriggerConditionType.ConditionTrue;
                else if (value is IIndexableItem item)
                {
                    data.ConditionType = (AITriggerConditionType)item.Value.ParseInt();
                }
                SetProperty();
            }
        }
        public object OperatorItem
        {
            get
            {
                return StaticCollections.AiTriggerOperators.ValueEqual((int)data.Operator);
            }
            set
            {
                if (value is IIndexableItem item) data.Operator = (AITriggerConditionOperator)item.Value.ParseInt();
                else data.Operator = AITriggerConditionOperator.LessThan;
                SetProperty();
            }
        }
        public object TechTypeItem
        {
            get
            {
                return StaticCollections.TechTypes.IndexEqual(data.ConditionObjID);
            }
            set
            {
                if (value is IIndexableItem item) data.ConditionObjID = item.Value;
                else if (value is string s)
                {
                    if (string.IsNullOrEmpty(s)) data.ConditionObjID = ITEM_NONE;
                    else data.ConditionObjID = s;
                }
                else data.ConditionObjID = ITEM_NONE;
                SetProperty();
            }
        }
        public object SideItem
        {
            get { return StaticCollections.AiSideIndexes.ValueEqual(data.SideIndex); }
            set
            {
                if (value is IIndexableItem item) data.SideIndex = item.Value.ParseInt();
                else data.SideIndex = 0;
                SetProperty();
            }
        }
        public object OwnerItem
        {
            get { return GlobalVar.CurrentMapDocument?.Map.Countries[data.OwnerHouse]; }
            set
            {
                if (value is IIndexableItem item) data.OwnerHouse = item.Value;
                else data.OwnerHouse = ITEM_ALL;
                SetProperty();
            }
        }
        public object Team1Item
        {
            get
            {
                if (data.Team1ID == ITEM_NONE) return ComboItem.NoneItem;
                TeamItem team = MapTeam[data.Team1ID];
                if (team == null) return data.Team1ID;
                return team;
            }
            set
            {
                if (value == null) data.Team1ID = ITEM_NONE;
                else if (value is IIndexableItem item) data.Team1ID = item.Id;
                else if (value is string s)
                {
                    if (string.IsNullOrEmpty(s)) data.Team1ID = ITEM_NONE;
                    else data.Team1ID = s;
                }
                SetProperty();
            }
        }
        public object Team2Item
        {
            get
            {
                if (data.Team2ID == ITEM_NONE) return ComboItem.NoneItem;
                TeamItem team = MapTeam[data.Team2ID];
                if (team == null) return data.Team2ID;
                return team;
            }
            set
            {
                if (value == null) data.Team2ID = ITEM_NONE;
                else if (value is IIndexableItem item) data.Team2ID = item.Id;
                else if (value is string s)
                {
                    if (string.IsNullOrEmpty(s)) data.Team2ID = ITEM_NONE;
                    else data.Team2ID = s;
                }
                SetProperty();
            }
        }
    }
}
