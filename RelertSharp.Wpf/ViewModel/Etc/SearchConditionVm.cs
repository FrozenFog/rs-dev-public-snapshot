using RelertSharp.Common;
using RelertSharp.MapStructure.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace RelertSharp.Wpf.ViewModel
{
    internal class SearchConditionVm : BaseTreeVm<SearchConditionVm.SearchConditionModel>
    {
        public IEnumerable<LogicOperator> LogicOperators { get { return Enum.GetValues(typeof(LogicOperator)).Cast<LogicOperator>(); } }
        public IEnumerable<Operator> ValueOperators { get { return Enum.GetValues(typeof(Operator)).Cast<Operator>(); } }
        public IEnumerable<ConditionType> Conditions { get { return Enum.GetValues(typeof(ConditionType)).Cast<ConditionType>(); } }
        public SearchConditionVm.SearchConditionModel Data { get { return data; } }
        public SearchConditionVm()
        {
            data = new SearchConditionModel();
        }
        public SearchConditionVm(bool isGroup)
        {
            _istree = isGroup;
            data = new SearchConditionModel();
        }



        #region Public
        public override void AddItem(IBaseTreeVm<SearchConditionModel> iitem)
        {
            base.AddItem(iitem);
            foreach (SearchConditionVm vm in Items)
            {
                vm.SetProperty(nameof(IsLogicOperEnable));
                if (!vm.IsLogicOperEnable) vm.LogicOper = LogicOperator.None;
            }
        }
        public override void RemoveItem(IBaseTreeVm<SearchConditionModel> item)
        {
            base.RemoveItem(item);
            foreach (SearchConditionVm vm in Items)
            {
                vm.SetProperty(nameof(IsLogicOperEnable));
                if (!vm.IsLogicOperEnable) vm.LogicOper = LogicOperator.None;
            }
        }
        public bool IsValidObject(object validate)
        {
            bool r = true;
            if (_istree)
            {
                if (Items.Count == 0) return r;
                foreach (SearchConditionVm vm in Items)
                {
                    // if it's first, use this result
                    if (!vm.IsLogicOperEnable) r = vm.IsValidObject(validate);
                    else
                    {
                        switch (vm.LogicOper)
                        {
                            case LogicOperator.And:
                                r = r && vm.IsValidObject(validate);
                                break;
                            case LogicOperator.Or:
                                r = r || vm.IsValidObject(validate);
                                break;
                            case LogicOperator.AndNot:
                                r = r && (!vm.IsValidObject(validate));
                                break;
                            case LogicOperator.OrNot:
                                r = r || (!vm.IsValidObject(validate));
                                break;
                        }
                    }
                }
            }
            else
            {
                string objValue = GetStringValue(validate, out bool isEnumerable, out List<string> extraInfo);
                if (isEnumerable)
                {
                    bool sub = false;
                    foreach (string subValue in extraInfo)
                    {
                        sub = sub || Validate(subValue);
                    }
                    r = sub;
                }
                else
                {
                    if (objValue.IsNullOrEmpty()) return false;
                    r = Validate(objValue);
                }
            }
            return r;
        }
        #endregion


        #region Private
        private bool Convert(string s1, string s2, out double value1, out double value2)
        {
            bool r = double.TryParse(s1, out double d1);
            bool r2 = double.TryParse(s2, out double d2);
            value1 = d1; value2 = d2;
            return r && r2;
        }
        private string GetStringValue(object validate, out bool isEnumerable, out List<string> extraInfo)
        {
            var map = GlobalVar.GlobalMap;
            isEnumerable = false;
            List<string> extra = new List<string>();
            extraInfo = extra;
            switch (data.ConditionType)
            {
                case ConditionType.AttachedTagId:
                    if (validate is ITaggableObject taggable) return taggable.TagId;
                    break;
                case ConditionType.CoordinateX:
                    if (validate is I2dLocateable pos) return pos.X.ToString();
                    break;
                case ConditionType.CoordinateY:
                    if (validate is I2dLocateable p2) return p2.Y.ToString();
                    break;
                case ConditionType.HealthPoint:
                    if (validate is ICombatObject com) return com.HealthPoint.ToString();
                    break;
                case ConditionType.FacingDirection:
                    if (validate is ICombatObject combat) return combat.Rotation.ToString();
                    break;
                case ConditionType.Height:
                    if (validate is I2dLocateable p3) return map.GetHeightFromTile(p3).ToString();
                    break;
                case ConditionType.Id:
                    if (validate is IIndexableItem indexable) return indexable.Id;
                    break;
                case ConditionType.Name:
                    if (validate is IIndexableItem indexable1) return indexable1.Name;
                    break;
                case ConditionType.RegistName:
                    if (validate is IRegistable registable) return registable.RegName;
                    break;
                case ConditionType.GroupId:
                    if (validate is IGroupable groupable) return groupable.Group;
                    break;
                case ConditionType.InfantrySubcell:
                    if (validate is IPosition inf) return inf.SubCell.ToString();
                    break;
                case ConditionType.TeamUseScript:
                    if (validate is TeamItem team) return team.ScriptID;
                    break;
                case ConditionType.TeamUseTaskforce:
                    if (validate is TeamItem team1) return team1.TaskforceID;
                    break;
                case ConditionType.ScriptParameter:
                    if (validate is TeamScriptItem scriptItem)
                    {
                        isEnumerable = true;
                        foreach (var param in scriptItem.Info.Parameters)
                        {
                            extra.Add(scriptItem.GetParameter(param));
                        }
                    }
                    break;
                case ConditionType.ScriptType:
                    if (validate is TeamScriptItem scriptItem1) return scriptItem1.ScriptActionIndex.ToString();
                    break;
                case ConditionType.TaskforceMemberRegistName:
                    if (validate is TaskforceItem taskforce)
                    {
                        isEnumerable = true;
                        foreach (var unit in taskforce.Members)
                        {
                            extra.Add(unit.RegName);
                        }
                    }
                    break;
                case ConditionType.TriggerActionType:
                    if (validate is LogicItem act && act.Type == TriggerSubType.ActionLogic) return act.ID.ToString();
                    break;
                case ConditionType.TriggerEventType:
                    if (validate is LogicItem evnt && evnt.Type == TriggerSubType.EventLogic) return evnt.ID.ToString();
                    break;
                case ConditionType.TriggerParameter:
                    if (validate is LogicItem lg)
                    {
                        isEnumerable = true;
                        foreach (var param in lg.Info.Parameters)
                        {
                            extra.Add(lg.GetParameter(param));
                        }
                    }
                    break;
            }
            return string.Empty;
        }
        private bool Validate(string objValue)
        {
            bool r = false;
            bool b = Convert(objValue, StringValue, out double d1, out double d2);
            switch (data.ValueOperator)
            {
                case Operator.Contains:
                    r = objValue.Contains(StringValue);
                    break;
                case Operator.EqualThan:
                    r = objValue == StringValue;
                    break;
                case Operator.GreaterOrEqualThan:
                    if (b) r = d1 >= d2;
                    break;
                case Operator.GreaterThan:
                    if (b) r = d1 > d2;
                    break;
                case Operator.LessOrEqualThan:
                    if (b) r = d1 <= d2;
                    break;
                case Operator.LessThan:
                    if (b) r = d1 < d2;
                    break;
                case Operator.NotEqualThan:
                    if (b) r = d1 != d2;
                    else r = objValue != StringValue;
                    break;
                case Operator.RegExMatch:
                    Regex regex = new Regex(StringValue);
                    r = regex.IsMatch(objValue);
                    break;
            }
            return r;
        }
        #endregion


        #region Call
        private bool _istree;
        public override bool IsTree { get { return _istree; } }
        #region Bind Call
        public LogicOperator LogicOper
        {
            get { return data.LogicOperator; }
            set
            {
                data.LogicOperator = value;
                SetProperty();
            }
        }
        public Operator ValueOper
        {
            get { return data.ValueOperator; }
            set
            {
                data.ValueOperator = value;
                SetProperty();
            }
        }
        public ConditionType Type
        {
            get { return data.ConditionType; }
            set
            {
                data.ConditionType = value;
                SetProperty();
            }
        }
        public string StringValue
        {
            get { return data.StringValue; }
            set
            {
                data.StringValue = value;
                SetProperty();
            }
        }
        /// <summary>
        /// False if it is the first condition
        /// </summary>
        public bool IsLogicOperEnable
        {
            get
            {
                if (Ancestor == null) return false;
                return Ancestor.IndexOf(this) > 0;
            }
        }
        public Visibility VisIsTree
        {
            get
            {
                return IsTree ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        #endregion
        #endregion



        #region Components
        public enum LogicOperator
        {
            None = -1,
            And,
            Or,
            AndNot,
            OrNot
        }
        public enum Operator
        {
            None = -1,
            GreaterThan,
            LessThan,
            GreaterOrEqualThan,
            LessOrEqualThan,
            EqualThan,
            NotEqualThan,
            Contains,
            RegExMatch
        }
        public enum ConditionType
        {
            NoCondition = -1,
            Id,
            Name,
            HealthPoint,
            CoordinateX,
            CoordinateY,
            Height,
            InfantrySubcell,
            RegistName,
            AttachedTagId,
            TriggerEventType,
            TriggerActionType,
            TriggerParameter,
            TeamUseScript,
            TeamUseTaskforce,
            ScriptParameter,
            ScriptType,
            TaskforceMemberRegistName,
            GroupId,
            FacingDirection
        }
        public class SearchConditionModel
        {
            public ConditionType ConditionType = ConditionType.NoCondition;
            public Operator ValueOperator = Operator.None;
            public LogicOperator LogicOperator = LogicOperator.None;
            public string StringValue;
        }
        #endregion
    }
}
