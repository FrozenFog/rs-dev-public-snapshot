using RelertSharp.Common;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Logic;
using RelertSharp.MapStructure.Objects;
using RelertSharp.Wpf.MapEngine.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RelertSharp.Wpf.ViewModel
{
    internal class SelectedInspectorVm : BaseVm<IMapObject>
    {
        public GlobalTagVm GlobalTags { get { return GlobalCollectionVm.Tags; } }
        private const string NULL_VALUE = "------";
        /// <summary>
        /// Sender will be structure regname
        /// </summary>
        public event EventHandler LoadedWithBuilding;
        public event Action RedrawRequested;
        private List<IMapObject> selectedObjects = new List<IMapObject>();
        private List<Tile> selectedTile = new List<Tile>();
        private ObservableCollection<SearchResultVm> listviewVm = new ObservableCollection<SearchResultVm>();
        private ObservableCollection<TileVm> tileVm = new ObservableCollection<TileVm>();
        private bool isCombat, isTag, isOwnable, isBuding;
        public SelectedInspectorVm()
        {
            data = new VirtualMapObject();
            Selector.SelectionChanged += SelectionChangedHandler;
            TileSelector.SelectedTileChanged += SelectedTileChangedHandler;
        }

        private void SelectedTileChangedHandler()
        {
            selectedTile = TileSelector.SelectedTile.ToList();

            tileVm.Clear();
            selectedTile.ForEach(x => tileVm.Add(new TileVm(x)));
            SetProperty(nameof(SelectedTiles));
        }

        private void SelectionChangedHandler()
        {
            selectedObjects = Selector.SelectedObjects.ToList();

            isCombat = IsAllObjectIsType<ICombatObject>();
            isTag = IsAllObjectIsType<ITaggableObject>();
            isOwnable = IsAllObjectIsType<IOwnableObject>();
            isBuding = IsAllObjectInType(MapObjectType.Building);

            listviewVm.Clear();
            selectedObjects.ForEach(x => listviewVm.Add(new SearchResultVm(x)));

            // building extension
            string regname = GetValue<StructureItem>(x => x.RegName);
            if (!regname.IsNullOrEmpty()) LoadedWithBuilding?.Invoke(regname, null);
            else LoadedWithBuilding?.Invoke(null, null);

            OnAllPropertyChanged();
            SetProperty(nameof(SelectedObjects));
        }


        #region Public
        public bool CanChangeFacing()
        {
            return isCombat;
        }
        public int GetFacing()
        {
            return Facing;
        }
        public void SetFacing(int facing)
        {
            Facing = facing;
        }
        #endregion


        #region Private
        private bool IsAllObjectInType(MapObjectType type)
        {
            if (selectedObjects.Count == 0) return false;
            return selectedObjects.All(x => (x.ObjectType & type) != 0);
        }
        private bool IsAllObjectIsType<TType>()
        {
            if (selectedObjects.Count == 0) return false;
            return selectedObjects.All(x => x is TType);
        }
        private TValue GetValue<TType, TValue>(Func<TType, TValue> selector, out bool isValid) where TValue : struct
        {
            TValue value = default;
            isValid = selectedObjects.Count != 0;
            foreach (IMapObject obj in selectedObjects)
            {
                if (obj is TType item)
                {
                    TValue itemValue = selector(item);
                    if (value.Equals(default)) value = selector(item);
                    else if (!value.Equals(itemValue))
                    {
                        isValid = false;
                        return default;
                    }
                    value = itemValue;
                }
                else
                {
                    isValid = false;
                    return default;
                }
            }
            return value;
        }
        private string GetValue<TType>(Func<TType, string> selector)
        {
            string value = null;
            foreach (IMapObject obj in selectedObjects)
            {
                if (obj is TType item)
                {
                    string itemValue = selector(item);
                    if (value == null) value = selector(item);
                    else if (value != itemValue) return null;
                    value = itemValue;
                }
                else return null;
            }
            return value;
        }
        private bool? GetValue<TType>(Func<TType, bool> selector)
        {
            bool? value = null;
            foreach (IMapObject obj in selectedObjects)
            {
                if (obj is TType item)
                {
                    bool itemValue = selector(item);
                    if (value == null) value = selector(item);
                    else if (value != itemValue) return null;
                    value = itemValue;
                }
                else return null;
            }
            return value;
        }
        private int GetValue<TType>(Func<TType, int> selector, int def = -1)
        {
            int value = def;
            foreach (IMapObject obj in selectedObjects)
            {
                if (obj is TType item)
                {
                    int itemValue = selector(item);
                    if (value == def) value = selector(item);
                    else if (value != itemValue) return def;
                    value = itemValue;
                }
                else return def;
            }
            return value;
        }
        private void SetValue<TType, TValue>(Action<TType, TValue> action, TValue value)
        {
            foreach (TType item in selectedObjects)
            {
                action(item, value);
            }
        }
        #endregion


        #region Calls
        #region Bind Call
        public ObservableCollection<SearchResultVm> SelectedObjects
        {
            get
            {
                return listviewVm;
            }
        }
        public ObservableCollection<TileVm> SelectedTiles
        {
            get { return tileVm; }
        }
        public Visibility IsOwnerHouseEnable
        {
            get { return isOwnable ? Visibility.Visible : Visibility.Collapsed; }
        }
        //public bool ApplyOwnerHouse
        //{
        //    get { return filter.Owner; }
        //    set
        //    {
        //        filter.Owner = value;
        //        SetProperty();
        //    }
        //}
        public HouseItem OwnerHouseItem
        {
            get
            {
                if (isOwnable)
                {
                    string owner = GetValue<IOwnableObject>(x => x.Owner);
                    if (!owner.IsNullOrEmpty()) return GlobalVar.GlobalMap?.Houses.GetHouse(owner);
                }
                return null;
            }
            set
            {
                if (isOwnable)
                {
                    string owner = value.Name;
                    SetValue<IOwnableObject, string>((x, v) => x.Owner = v, owner);
                    SetProperty();
                    RedrawRequested?.Invoke();
                }
            }
        }
        public string OwnerText
        {
            get
            {
                HouseItem owner = OwnerHouseItem;
                if (owner != null) return owner.ToString();
                return NULL_VALUE;
            }
        }
        public Visibility IsHpEnable
        {
            get { return isCombat ? Visibility.Visible : Visibility.Collapsed; }
        }
        //public bool ApplyHp
        //{
        //    get { return filter.HealthPoint; }
        //    set
        //    {
        //        filter.HealthPoint = value;
        //        SetProperty();
        //    }
        //}
        public int HealthPoint
        {
            get
            {
                if (isCombat) return GetValue<ICombatObject>(x => x.HealthPoint, 0);
                return 0;
            }
            set
            {
                if (isCombat)
                {
                    SetValue<ICombatObject, int>((x, v) => x.HealthPoint = v, value);
                    SetProperty();
                }
            }
        }
        public Visibility IsFacingEnable
        {
            get { return isCombat ? Visibility.Visible : Visibility.Collapsed; }
        }
        //public bool ApplyFacing
        //{
        //    get { return filter.Facing; }
        //    set
        //    {
        //        filter.Facing = value;
        //        SetProperty();
        //    }
        //}
        public int Facing
        {
            get
            {
                if (isCombat) return GetValue<ICombatObject>(x => x.Rotation, 0);
                return 0;
            }
            set
            {
                if (isCombat)
                {
                    SetValue<ICombatObject, int>((x, v) => x.Rotation = v, value);
                    SetProperty();
                    RedrawRequested?.Invoke();
                }
            }
        }
        public Visibility IsTagEnable
        {
            get { return isTag ? Visibility.Visible : Visibility.Collapsed; }
        }
        //public bool ApplyTag
        //{
        //    get { return filter.Tag; }
        //    set
        //    {
        //        filter.Tag = value;
        //        SetProperty();
        //    }
        //}
        public IIndexableItem TagItem
        {
            get
            {
                if (isTag)
                {
                    string tag = GetValue<ITaggableObject>(x => x.TagId);
                    if (!tag.IsNullOrEmpty())
                    {
                        if (tag == Constant.VALUE_NONE) return new ComboItem(tag);
                        return GlobalVar.GlobalMap?.Tags[tag];
                    }
                }
                return null;
            }
            set
            {
                if (isTag)
                {
                    string id = value.Id;
                    SetValue<ITaggableObject, string>((x, v) => x.TagId = v, id);
                    SetProperty();
                }
            }
        }
        public string TagText
        {
            get
            {
                IIndexableItem item = TagItem;
                if (item != null) return item.ToString();
                return NULL_VALUE;
            }
        }
        //public string TagId
        //{
        //    get { return data.AttatchedTag; }
        //    set
        //    {
        //        data.AttatchedTag = value;
        //        SetProperty();
        //    }
        //}
        public Visibility IsMissionEnable
        {
            get { return isCombat ? Visibility.Visible : Visibility.Collapsed; }
        }
        //public bool ApplyMission
        //{
        //    get { return filter.MissionStatus; }
        //    set
        //    {
        //        filter.MissionStatus = value;
        //        SetProperty();
        //    }
        //}
        public IIndexableItem MissionStatusItem
        {
            get
            {
                if (isCombat)
                {
                    string stat = GetValue<ICombatObject>(x => x.Status);
                    if (!stat.IsNullOrEmpty()) return new ComboItem(stat);
                }
                return null;
            }
            set
            {
                if (isCombat)
                {
                    SetValue<ICombatObject, string>((x, v) => x.Status = v, value.Id);
                    SetProperty();
                }
            }
        }
        public string StatusText
        {
            get
            {
                IIndexableItem item = MissionStatusItem;
                if (item != null) return item.ToString();
                return NULL_VALUE;
            }
        }
        public Visibility IsVeterancyEnable
        {
            get { return isCombat ? Visibility.Visible : Visibility.Collapsed; }
        }
        //public bool ApplyVeterancy
        //{
        //    get { return filter.Veteran; }
        //    set
        //    {
        //        filter.Veteran = value;
        //        SetProperty();
        //    }
        //}
        public int Veterancy
        {
            get
            {
                if (isCombat) return GetValue<ICombatObject>(x => x.VeterancyPercentage, 0);
                return 0;
            }
            set
            {
                if (isCombat)
                {
                    SetValue<ICombatObject, int>((x, v) => x.VeterancyPercentage = v, value);
                    SetProperty();
                }
            }
        }
        public Visibility IsGroupEnable
        {
            get { return isCombat ? Visibility.Visible : Visibility.Collapsed; }
        }
        //public bool ApplyGroup
        //{
        //    get { return filter.Group; }
        //    set
        //    {
        //        filter.Group = value;
        //        SetProperty();
        //    }
        //}
        public string Group
        {
            get
            {
                if (isCombat)
                {
                    string value = GetValue<ICombatObject>(x => x.Group);
                    if (value.IsNullOrEmpty()) return NULL_VALUE;
                    return value;
                }
                return NULL_VALUE;
            }
            set
            {
                if (isCombat)
                {
                    SetValue<ICombatObject, string>((x, v) => x.Group = v, value);
                    SetProperty();
                }
            }
        }
        public Visibility IsAboveGroundEnable
        {
            get { return IsAllObjectInType(MapObjectType.GroundMovable) ? Visibility.Visible : Visibility.Collapsed; }
        }
        //public bool ApplyAboveGround
        //{
        //    get { return filter.AboveGround; }
        //    set
        //    {
        //        filter.AboveGround = value;
        //        SetProperty();
        //    }
        //}
        public bool? AboveGround
        {
            get
            {
                if (IsAllObjectInType(MapObjectType.GroundMovable))
                {
                    return GetValue<ObjectItemBase>(x => x.IsAboveGround);
                }
                return null;
            }
            set
            {
                if (IsAllObjectInType(MapObjectType.GroundMovable) && value.HasValue)
                {
                    SetValue<ObjectItemBase, bool>((x, v) => x.IsAboveGround = v, value.Value);
                    SetProperty();
                }
            }
        }
        public Visibility IsRecruitYEnable
        {
            get { return IsAllObjectInType(MapObjectType.MovableCombanant) ? Visibility.Visible : Visibility.Collapsed; }
        }
        //public bool ApplyRecruitYEnable
        //{
        //    get { return filter.RecruitYes; }
        //    set
        //    {
        //        filter.RecruitYes = value;
        //        SetProperty();
        //    }
        //}
        public bool? RecruitYes
        {
            get
            {
                if (IsAllObjectInType(MapObjectType.MovableCombanant))
                {
                    return GetValue<ObjectItemBase>(x => x.AutoYESRecruitType);
                }
                return null;
            }
            set
            {
                if (IsAllObjectInType(MapObjectType.MovableCombanant) && value.HasValue)
                {
                    SetValue<ObjectItemBase, bool>((x, v) => x.AutoYESRecruitType = v, value.Value);
                    SetProperty();
                }
            }
        }
        public Visibility IsRecruitNEnable
        {
            get { return IsAllObjectInType(MapObjectType.MovableCombanant) ? Visibility.Visible : Visibility.Collapsed; }
        }
        //public bool ApplyRecruitNEnable
        //{
        //    get { return filter.RecruitNo; }
        //    set
        //    {
        //        filter.RecruitNo = value;
        //        SetProperty();
        //    }
        //}
        public bool? RecruitNo
        {
            get
            {
                if (IsAllObjectInType(MapObjectType.MovableCombanant))
                {
                    return GetValue<ObjectItemBase>(x => x.AutoNORecruitType);
                }
                return null;
            }
            set
            {
                if (IsAllObjectInType(MapObjectType.MovableCombanant) && value.HasValue)
                {
                    SetValue<ObjectItemBase, bool>((x, v) => x.AutoNORecruitType = v, value.Value);
                    SetProperty();
                }
            }
        }
        public Visibility IsFollowsEnable
        {
            get { return IsAllObjectInType(MapObjectType.Vehicle) ? Visibility.Visible : Visibility.Collapsed; }
        }
        //public bool ApplyFollows
        //{
        //    get { return filter.Follows; }
        //    set
        //    {
        //        filter.Follows = value;
        //        SetProperty();
        //    }
        //}
        public string FollowsIndex
        {
            get
            {
                if (IsAllObjectInType(MapObjectType.Vehicle))
                {
                    string value = GetValue<UnitItem>(x => x.FollowsIndex);
                    if (value.IsNullOrEmpty()) return NULL_VALUE;
                    return value;
                }
                return NULL_VALUE;
            }
            set
            {
                if (IsAllObjectInType(MapObjectType.Vehicle))
                {
                    SetValue<UnitItem, string>((x, v) => x.FollowsIndex = v, value);
                    SetProperty();
                }
            }
        }
        public Visibility IsSellableEnable
        {
            get { return isBuding ? Visibility.Visible : Visibility.Collapsed; }
        }
        //public bool ApplySellable
        //{
        //    get { return filter.Sellable; }
        //    set
        //    {
        //        filter.Sellable = value;
        //        SetProperty();
        //    }
        //}
        public bool? Sellable
        {
            get
            {
                if (isBuding)
                {
                    return GetValue<StructureItem>(x => x.AISellable);
                }
                return null;
            }
            set
            {
                if (isBuding && value.HasValue)
                {
                    SetValue<StructureItem, bool>((x, v) => x.AISellable = v, value.Value);
                    SetProperty();
                }
            }
        }
        public Visibility IsRebuildEnable
        {
            get { return isBuding ? Visibility.Visible : Visibility.Collapsed; }
        }
        //public bool ApplyRebuild
        //{
        //    get { return filter.Rebuild; }
        //    set
        //    {
        //        filter.Rebuild = value;
        //        SetProperty();
        //    }
        //}
        public bool? AiRebuild
        {
            get
            {
                if (isBuding)
                {
                    return GetValue<StructureItem>(x => x.AIRebuildable);
                }
                return null;
            }
            set
            {
                if (isBuding && value.HasValue)
                {
                    SetValue<StructureItem, bool>((x, v) => x.AIRebuildable = v, value.Value);
                    SetProperty();
                }
            }
        }
        public Visibility IsPoweredEnable
        {
            get { return isBuding ? Visibility.Visible : Visibility.Collapsed; }
        }
        //public bool ApplyPowered
        //{
        //    get { return filter.Powered; }
        //    set
        //    {
        //        filter.Powered = value;
        //        SetProperty();
        //    }
        //}
        public bool? Powered
        {
            get
            {
                if (isBuding)
                {
                    return GetValue<StructureItem>(x => x.IsPowered);
                }
                return null;
            }
            set
            {
                if (isBuding && value.HasValue)
                {
                    SetValue<StructureItem, bool>((x, v) => x.IsPowered = v, value.Value);
                    SetProperty();
                }
            }
        }
        public Visibility IsUpgNumEnable
        {
            get { return isBuding ? Visibility.Visible : Visibility.Collapsed; }
        }
        //public bool ApplyUpgNum
        //{
        //    get { return filter.UpgradeNum; }
        //    set
        //    {
        //        filter.UpgradeNum = value;
        //        SetProperty();
        //    }
        //}
        public int UpgradeNum
        {
            get
            {
                if (isBuding) return GetValue<StructureItem>(x => x.UpgradeNum, 0);
                return 0;
            }
            set
            {
                if (isBuding)
                {
                    int r = value.TrimTo(0, 3);
                    SetValue<StructureItem, int>((x, v) => x.UpgradeNum = v, r);
                    if (r < 3)
                    {
                        SetValue<StructureItem, string>((x, v) => x.Upgrade3 = v, Constant.VALUE_NONE);
                        SetProperty(nameof(Upg3Item));
                    }
                    if (r < 2)
                    {
                        SetValue<StructureItem, string>((x, v) => x.Upgrade2 = v, Constant.VALUE_NONE);
                        SetProperty(nameof(Upg2Item));

                    }
                    if (r < 1)
                    {
                        SetValue<StructureItem, string>((x, v) => x.Upgrade1 = v, Constant.VALUE_NONE);
                        SetProperty(nameof(Upg1Item));

                    }
                    SetProperty();
                    RedrawRequested?.Invoke();
                }
            }
        }
        public Visibility IsUpg1Enable
        {
            get { return isBuding ? Visibility.Visible : Visibility.Collapsed; }
        }
        //public bool ApplyUpg1
        //{
        //    get { return filter.Upg1; }
        //    set
        //    {
        //        filter.Upg1 = value;
        //        SetProperty();
        //    }
        //}
        public IIndexableItem Upg1Item
        {
            get
            {
                if (isBuding)
                {
                    string upg = GetValue<StructureItem>(x => x.Upgrade1);
                    if (!upg.IsNullOrEmpty()) return new ComboItem(upg);
                }
                return null;
            }
            set
            {
                if (isBuding)
                {
                    if (value == null) SetValue<StructureItem, string>((x, v) => x.Upgrade1 = v, Constant.VALUE_NONE);
                    else SetValue<StructureItem, string>((x, v) => x.Upgrade1 = v, value.Id);
                    SetProperty();
                    RedrawRequested?.Invoke();
                }
            }
        }
        public string Upg1Text
        {
            get
            {
                IIndexableItem item = Upg1Item;
                if (item != null) return item.ToString();
                return NULL_VALUE;
            }
        }
        public Visibility IsUpg2Enable
        {
            get { return isBuding ? Visibility.Visible : Visibility.Collapsed; }
        }
        //public bool ApplyUpg2
        //{
        //    get { return filter.Upg2; }
        //    set
        //    {
        //        filter.Upg2 = value;
        //        SetProperty();
        //    }
        //}
        public IIndexableItem Upg2Item
        {
            get
            {
                if (isBuding)
                {
                    string upg = GetValue<StructureItem>(x => x.Upgrade2);
                    if (!upg.IsNullOrEmpty()) return new ComboItem(upg);
                }
                return null;
            }
            set
            {
                if (isBuding)
                {
                    if (value == null) SetValue<StructureItem, string>((x, v) => x.Upgrade2 = v, Constant.VALUE_NONE);
                    else SetValue<StructureItem, string>((x, v) => x.Upgrade2 = v, value.Id);
                    SetProperty();
                    RedrawRequested?.Invoke();
                }
            }
        }
        public string Upg2Text
        {
            get
            {
                IIndexableItem item = Upg2Item;
                if (item != null) return item.ToString();
                return NULL_VALUE;
            }
        }
        public Visibility IsUpg3Enable
        {
            get { return isBuding ? Visibility.Visible : Visibility.Collapsed; }
        }
        //public bool ApplyUpg3
        //{
        //    get { return filter.Upg3; }
        //    set
        //    {
        //        filter.Upg3 = value;
        //        SetProperty();
        //    }
        //}
        public IIndexableItem Upg3Item
        {
            get
            {
                if (isBuding)
                {
                    string upg = GetValue<StructureItem>(x => x.Upgrade3);
                    if (!upg.IsNullOrEmpty()) return new ComboItem(upg);
                }
                return null;
            }
            set
            {
                if (isBuding)
                {
                    if (value == null) SetValue<StructureItem, string>((x, v) => x.Upgrade3 = v, Constant.VALUE_NONE);
                    else SetValue<StructureItem, string>((x, v) => x.Upgrade3 = v, value.Id);
                    SetProperty();
                    RedrawRequested?.Invoke();
                }
            }
        }
        public string Upg3Text
        {
            get
            {
                IIndexableItem item = Upg3Item;
                if (item != null) return item.ToString();
                return NULL_VALUE;
            }
        }
        public Visibility IsRepairEnable
        {
            get { return isBuding ? Visibility.Visible : Visibility.Collapsed; }
        }
        //public bool ApplyRepair
        //{
        //    get { return filter.AiRepairable; }
        //    set
        //    {
        //        filter.AiRepairable = value;
        //        SetProperty();
        //    }
        //}
        public bool? AiRepair
        {
            get
            {
                if (isBuding)
                {
                    return GetValue<StructureItem>(x => x.AIRepairable);
                }
                return null;
            }
            set
            {
                if (isBuding && value.HasValue)
                {
                    SetValue<StructureItem, bool>((x, v) => x.AIRepairable = v, value.Value);
                    SetProperty();
                }
            }
        }
        public Visibility IsSpotlightEnable
        {
            get { return isBuding ? Visibility.Visible : Visibility.Collapsed; }
        }
        //public bool ApplySpotlight
        //{
        //    get { return filter.Spotlight; }
        //    set
        //    {
        //        filter.Spotlight = value;
        //        SetProperty();
        //    }
        //}
        public IIndexableItem SpotlightItem
        {
            get
            {
                if (isBuding)
                {
                    BuildingSpotlightType spot = GetValue<StructureItem, BuildingSpotlightType>(x => x.SpotlightType, out bool isValid);
                    if (!isValid) return null;
                    return new ComboItem(((int)spot).ToString(), spot.ToString());
                }
                return null;
            }
            set
            {
                if (isBuding)
                {
                    int.TryParse(value.Id, out int id);
                    SetValue<StructureItem, BuildingSpotlightType>((x, v) => x.SpotlightType = v, (BuildingSpotlightType)id);
                    SetProperty();
                }
            }
        }
        public string SpotlightText
        {
            get
            {
                IIndexableItem item = SpotlightItem;
                if (item != null) return item.ToString();
                return NULL_VALUE;
            }
        }
        #endregion
        #endregion
    }
}
