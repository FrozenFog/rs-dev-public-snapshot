using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;

namespace RelertSharp.Wpf.ViewModel
{
    internal class AiTriggerVm : BaseVm<AITriggerItem>
    {
        public GlobalTeamVm TeamCollections { get { return GlobalCollectionVm.Teams; } }
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
        public int SideIndex
        {
            get { return data.SideIndex; }
            set
            {
                data.SideIndex = value;
                SetProperty();
            }
        }
        public string Owner
        {
            get { return data.OwnerHouse; }
            set
            {
                data.OwnerHouse = value;
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
        public AITriggerConditionType Condition
        {
            get { return data.ConditionType; }
            set
            {
                data.ConditionType = value;
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
        public AITriggerConditionOperator Operator
        {
            get { return data.Comparator.Operator; }
            set
            {
                data.Comparator.Operator = value;
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
        public string Team1String
        {
            get
            {
                TeamItem team = MapTeam[data.Team1ID];
                if (team == null) return data.Team1ID;
                return team.ToString();
            }
            set { }
        }
        public string Team2String
        {
            get
            {
                TeamItem team = MapTeam[data.Team2ID];
                if (team == null) return data.Team2ID;
                return team.ToString();
            }
            set { }
        }
        public object Team1Item
        {
            get
            {
                TeamItem team = MapTeam[data.Team1ID];
                if (team == null) return data.Team1ID;
                return team;
            }
            set
            {
                if (value == null) data.Team1ID = Constant.ITEM_NONE;
                else if (value is TeamItem item) data.Team1ID = item.Id;
                else if (value is string s)
                {
                    if (string.IsNullOrEmpty(s)) data.Team1ID = Constant.ITEM_NONE;
                    else data.Team1ID = s;
                }
                SetProperty();
            }
        }
        public object Team2Item
        {
            get
            {
                TeamItem team = MapTeam[data.Team2ID];
                if (team == null) return data.Team2ID;
                return team;
            }
            set
            {
                if (value == null) data.Team2ID = Constant.ITEM_NONE;
                else if (value is TeamItem item) data.Team2ID = item.Id;
                else if (value is string s)
                {
                    if (string.IsNullOrEmpty(s)) data.Team2ID = Constant.ITEM_NONE;
                    else data.Team2ID = s;
                }
                SetProperty();
            }
        }
    }
}
