using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using RelertSharp.Common;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Wpf.Common;

namespace RelertSharp.Wpf.ViewModel
{
    internal class ObjectAttributeApplierVm : BaseVm<ObjectBrushConfig>
    {
        private ObjectBrushFilter filter;
        private MapObjectType Type { get; set; }
        private RelertSharp.MapStructure.Map Map { get { return GlobalVar.CurrentMapDocument?.Map; } }
        internal event EventHandler ObjectRefreshRequest;
        internal event EventHandler AttributeRefreshRequest;

        public ObjectAttributeApplierVm()
        {
            data = new ObjectBrushConfig();
            filter = new ObjectBrushFilter();
        }
        public ObjectAttributeApplierVm(object obj) : base(obj)
        {
            data = new ObjectBrushConfig();
            filter = new ObjectBrushFilter();
        }
        public ObjectAttributeApplierVm(ObjectBrushConfig cfg, ObjectBrushFilter filter, MapObjectType type = MapObjectType.Undefined)
        {
            data = cfg;
            Type = type;
            this.filter = filter;
        }



        #region Binds
        public Visibility IsOwnerHouseEnable
        {
            get { return (Type & MapObjectType.CombatObject) != 0 ? Visibility.Visible : Visibility.Collapsed; }
        }
        public bool ApplyOwnerHouse
        {
            get { return filter.OwnerHouse; }
            set
            {
                filter.OwnerHouse = value;
                SetProperty();
            }
        }
        public HouseItem OwnerHouseItem
        {
            get { return GlobalVar.CurrentMapDocument?.Map.Houses.GetHouse(data.OwnerHouse); }
            set
            {
                data.OwnerHouse = value.Name;
                SetProperty();
                ObjectRefreshRequest?.Invoke(null, null);
            }
        }
        public Visibility IsHpEnable
        {
            get { return (Type & MapObjectType.CombatObject) != 0 ? Visibility.Visible : Visibility.Collapsed; }
        }
        public bool ApplyHp
        {
            get { return filter.HealthPoint; }
            set
            {
                filter.HealthPoint = value;
                SetProperty();
            }
        }
        public int HealthPoint
        {
            get { return data.HealthPoint; }
            set
            {
                data.HealthPoint = value;
                SetProperty();
            }
        }
        public Visibility IsFacingEnable
        {
            get { return (Type & MapObjectType.CombatObject) != 0 ? Visibility.Visible : Visibility.Collapsed; }
        }
        public bool ApplyFacing
        {
            get { return filter.Facing; }
            set
            {
                filter.Facing = value;
                SetProperty();
            }
        }
        public int Facing
        {
            get { return data.FacingRotation; }
            set
            {
                data.FacingRotation = value;
                SetProperty();
                ObjectRefreshRequest?.Invoke(null, null);
            }
        }
        public Visibility IsTagEnable
        {
            get { return (Type == MapObjectType.Celltag || (Type & MapObjectType.CombatObject) != 0) ? Visibility.Visible : Visibility.Collapsed; }
        }
        public bool ApplyTag
        {
            get { return filter.Tag; }
            set
            {
                filter.Tag = value;
                SetProperty();
            }
        }
        public TagItem TagItem
        {
            get { return Map?.Tags[data.AttatchedTag]; }
            set
            {
                data.AttatchedTag = value.Id;
                SetProperty();
            }
        }
        public string TagId
        {
            get { return data.AttatchedTag; }
            set
            {
                data.AttatchedTag = value;
                SetProperty();
            }
        }
        public Visibility IsMissionEnable
        {
            get { return (Type & MapObjectType.MovableCombanant) != 0 ? Visibility.Visible : Visibility.Collapsed; }
        }
        public bool ApplyMission
        {
            get { return filter.MissionStatus; }
            set
            {
                filter.MissionStatus = value;
                SetProperty();
            }
        }
        public IIndexableItem MissionStatusItem
        {
            get { return new ComboItem(data.MissionStatus); }
            set
            {
                data.MissionStatus = value.Id;
                SetProperty();
            }
        }
        public Visibility IsVeterancyEnable
        {
            get { return (Type & MapObjectType.MovableCombanant) != 0 ? Visibility.Visible : Visibility.Collapsed; }
        }
        public bool ApplyVeterancy
        {
            get { return filter.Veteran; }
            set
            {
                filter.Veteran = value;
                SetProperty();
            }
        }
        public int Veterancy
        {
            get { return data.VeterancyPercentage; }
            set
            {
                data.VeterancyPercentage = value;
                SetProperty();
            }
        }
        public Visibility IsGroupEnable
        {
            get { return (Type & MapObjectType.MovableCombanant) != 0 ? Visibility.Visible : Visibility.Collapsed; }
        }
        public bool ApplyGroup
        {
            get { return filter.Group; }
            set
            {
                filter.Group = value;
                SetProperty();
            }
        }
        public string Group
        {
            get { return data.Group; }
            set
            {
                if (string.IsNullOrEmpty(value)) data.Group = Constant.ID_INVALID;
                else data.Group = value;
                SetProperty();
            }
        }
        public Visibility IsAboveGroundEnable
        {
            get { return (Type & MapObjectType.GroundMovable) != 0 ? Visibility.Visible : Visibility.Collapsed; }
        }
        public bool ApplyAboveGround
        {
            get { return filter.AboveGround; }
            set
            {
                filter.AboveGround = value;
                SetProperty();
            }
        }
        public bool AboveGround
        {
            get { return data.AboveGround; }
            set
            {
                data.AboveGround = value;
                SetProperty();
            }
        }
        public Visibility IsRecruitYEnable
        {
            get { return (Type & MapObjectType.MovableCombanant) != 0 ? Visibility.Visible : Visibility.Collapsed; }
        }
        public bool ApplyRecruitYEnable
        {
            get { return filter.RecruitYes; }
            set
            {
                filter.RecruitYes = value;
                SetProperty();
            }
        }
        public bool RecruitYes
        {
            get { return data.AutoRecruitYes; }
            set
            {
                data.AutoRecruitYes = value;
                SetProperty();
            }
        }
        public Visibility IsRecruitNEnable
        {
            get { return (Type & MapObjectType.MovableCombanant) != 0 ? Visibility.Visible : Visibility.Collapsed; }
        }
        public bool ApplyRecruitNEnable
        {
            get { return filter.RecruitNo; }
            set
            {
                filter.RecruitNo = value;
                SetProperty();
            }
        }
        public bool RecruitNo
        {
            get { return data.AutoRecruitNo; }
            set
            {
                data.AutoRecruitNo = value;
                SetProperty();
            }
        }
        public Visibility IsFollowsEnable
        {
            get { return Type == MapObjectType.Vehicle ? Visibility.Visible : Visibility.Collapsed; }
        }
        public bool ApplyFollows
        {
            get { return filter.Follows; }
            set
            {
                filter.Follows = value;
                SetProperty();
            }
        }
        public string FollowsIndex
        {
            get { return data.FollowsIndex; }
            set
            {
                data.FollowsIndex = value;
                SetProperty();
            }
        }
        public Visibility IsSellableEnable
        {
            get { return Type == MapObjectType.Building ? Visibility.Visible : Visibility.Collapsed; }
        }
        public bool ApplySellable
        {
            get { return filter.Sellable; }
            set
            {
                filter.Sellable = value;
                SetProperty();
            }
        }
        public bool Sellable
        {
            get { return data.IsSellable; }
            set
            {
                data.IsSellable = value;
                SetProperty();
            }
        }
        public Visibility IsRebuildEnable
        {
            get { return Type == MapObjectType.Building ? Visibility.Visible : Visibility.Collapsed; }
        }
        public bool ApplyRebuild
        {
            get { return filter.Rebuild; }
            set
            {
                filter.Rebuild = value;
                SetProperty();
            }
        }
        public bool AiRebuild
        {
            get { return data.AiRebuildable; }
            set
            {
                data.AiRebuildable = value;
                SetProperty();
            }
        }
        public Visibility IsPoweredEnable
        {
            get { return Type == MapObjectType.Building ? Visibility.Visible : Visibility.Collapsed; }
        }
        public bool ApplyPowered
        {
            get { return filter.Powered; }
            set
            {
                filter.Powered = value;
                SetProperty();
            }
        }
        public bool Powered
        {
            get { return data.Powered; }
            set
            {
                data.Powered = value;
                SetProperty();
            }
        }
        public Visibility IsUpgNumEnable
        {
            get { return Type == MapObjectType.Building ? Visibility.Visible : Visibility.Collapsed; }
        }
        public bool ApplyUpgNum
        {
            get { return filter.UpgradeNum; }
            set
            {
                filter.UpgradeNum = value;
                SetProperty();
            }
        }
        public int UpgradeNum
        {
            get { return data.UpgradeNum; }
            set
            {
                int r = value.TrimTo(0, 3);
                data.UpgradeNum = r;
                SetProperty();
                AttributeRefreshRequest?.Invoke(null, null);
                if (r == 0)
                {
                    data.Upg1 = Constant.VALUE_NONE;
                    data.Upg2 = Constant.VALUE_NONE;
                    data.Upg3 = Constant.VALUE_NONE;
                    ObjectRefreshRequest?.Invoke(null, null);
                }
            }
        }
        public Visibility IsUpg1Enable
        {
            get { return (Type == MapObjectType.Building && data.UpgradeNum >= 1) ? Visibility.Visible : Visibility.Collapsed; }
        }
        public bool ApplyUpg1
        {
            get { return filter.Upg1; }
            set
            {
                filter.Upg1 = value;
                SetProperty();
            }
        }
        public IIndexableItem Upg1Item
        {
            get { return new ComboItem(data.Upg1); }
            set
            {
                if (value == null) data.Upg1 = Constant.VALUE_NONE;
                else data.Upg1 = value.Id;
                SetProperty();
                ObjectRefreshRequest?.Invoke(null, null);
            }
        }
        public Visibility IsUpg2Enable
        {
            get { return (Type == MapObjectType.Building && data.UpgradeNum >= 2) ? Visibility.Visible : Visibility.Collapsed; }
        }
        public bool ApplyUpg2
        {
            get { return filter.Upg2; }
            set
            {
                filter.Upg2 = value;
                SetProperty();
            }
        }
        public IIndexableItem Upg2Item
        {
            get { return new ComboItem(data.Upg2); }
            set
            {
                if (value == null) data.Upg2 = Constant.VALUE_NONE;
                else data.Upg2 = value.Id;
                SetProperty();
                ObjectRefreshRequest?.Invoke(null, null);
            }
        }
        public Visibility IsUpg3Enable
        {
            get { return (Type == MapObjectType.Building && data.UpgradeNum == 3) ? Visibility.Visible : Visibility.Collapsed; }
        }
        public bool ApplyUpg3
        {
            get { return filter.Upg3; }
            set
            {
                filter.Upg3 = value;
                SetProperty();
            }
        }
        public IIndexableItem Upg3Item
        {
            get { return new ComboItem(data.Upg3); }
            set
            {
                if (value == null) data.Upg3 = Constant.VALUE_NONE;
                else data.Upg3 = value.Id;
                SetProperty();
                ObjectRefreshRequest?.Invoke(null, null);
            }
        }
        public Visibility IsRepairEnable
        {
            get { return Type == MapObjectType.Building ? Visibility.Visible : Visibility.Collapsed; }
        }
        public bool ApplyRepair
        {
            get { return filter.AiRepairable; }
            set
            {
                filter.AiRepairable = value;
                SetProperty();
            }
        }
        public bool AiRepair
        {
            get { return data.AiRepairable; }
            set
            {
                data.AiRepairable = value;
                SetProperty();
            }
        }
        public Visibility IsSpotlightEnable
        {
            get { return Type == MapObjectType.Building ? Visibility.Visible : Visibility.Collapsed; }
        }
        public bool ApplySpotlight
        {
            get { return filter.Spotlight; }
            set
            {
                filter.Spotlight = value;
                SetProperty();
            }
        }
        public IIndexableItem SpotlightItem
        {
            get { return new ComboItem(((int)data.SpotlightType).ToString(), data.SpotlightType.ToString()); }
            set
            {
                int.TryParse(value.Id, out int id);
                data.SpotlightType = (BuildingSpotlightType)id;
                SetProperty();
            }
        }
        #endregion


        #region Public
        public void SetObjectType(MapObjectType type)
        {
            Type = type;
        }
        #endregion
    }
}
