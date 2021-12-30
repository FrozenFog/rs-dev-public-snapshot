using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure.Logic;
using RelertSharp.MapStructure;
using RelertSharp.Common;
using static RelertSharp.Common.Constant;

namespace RelertSharp.Wpf.ViewModel
{
    internal class TriggerVm : BaseVm<TriggerItem>
    {
        public GlobalTriggerVm Triggers { get { return GlobalCollectionVm.Triggers; } }
        public GlobalCountryVm Owners { get { return GlobalCollectionVm.Countries; } }
        private Map map { get { return GlobalVar.GlobalMap; } }
        private TagCollection Tags { get { return map?.Tags; } }
        public TriggerVm()
        {
            data = new TriggerItem();
        }
        public TriggerVm(object obj) : base(obj) { }


        private bool HasItem { get { return map != null && Tags != null && data != null; } }

        public string Name
        {
            get { return data.Name; }
            set
            {
                data.Name = value;
                data.OnNameUpdated();
                SetProperty();
            }
        }
        public string Id
        {
            get { return data.Id; }
            set
            {
                data.Id = value;
                SetProperty();
            }
        }

        public object AssoTriggerItem
        {
            get
            {
                string linked = data.LinkedWith;
                if (string.IsNullOrEmpty(linked)) return null;
                if (data.LinkedWith == ITEM_NONE) return ComboItem.NoneItem;
                TriggerItem trigger = map.Triggers[linked];
                if (trigger == null) return linked;
                return trigger;
            }
            set
            {
                if (value == null) data.LinkedWith = ITEM_NONE;
                else if (value is IIndexableItem item) data.LinkedWith = item.Id;
                SetProperty();
            }
        }
        public object TriggerOwner
        {
            get
            {
                string owner = data.Owner;
                if (owner.IsNullOrEmpty()) return null;
                CountryItem c = map.Countries.GetCountry(owner);
                return c;
            }
            set
            {
                if (value is CountryItem c)
                {
                    data.Owner = c.Name;
                    data.InvokeChildInfoRefreshAll();
                }
            }
        }
        public bool IsDisabled
        {
            get { return data.Disabled; }
            set
            {
                data.Disabled = value;
                SetProperty();
            }
        }
        public bool Easy
        {
            get { return data.EasyOn; }
            set
            {
                data.EasyOn = value;
                SetProperty();
            }
        }
        public bool Normal
        {
            get { return data.NormalOn; }
            set
            {
                data.NormalOn = value;
                SetProperty();
            }
        }
        public bool Hard
        {
            get { return data.HardOn; }
            set
            {
                data.HardOn = value;
                SetProperty();
            }
        }
        public bool Rep0Checked
        {
            get
            {
                if (Tags != null && Tags.FirstOrDefault(x => x.AssoTrigger == data.Id) is TagItem tg)
                {
                    return tg.Repeating == TriggerRepeatingType.NoRepeating;
                }
                return true;
            }
            set
            {
                if (value && HasItem)
                {
                    Tags.ModifyTagRepeating(data.Id, TriggerRepeatingType.NoRepeating);
                    SetProperty(nameof(Rep0Checked));
                    SetProperty(nameof(Rep1Checked));
                    SetProperty(nameof(Rep2Checked));
                }
            }
        }
        public bool Rep1Checked
        {
            get
            {
                if (Tags != null && Tags.FirstOrDefault(x => x.AssoTrigger == data.Id) is TagItem tg)
                {
                    return tg.Repeating == TriggerRepeatingType.OneTimeLogicAND;
                }
                return false;
            }
            set
            {
                if (value && HasItem)
                {
                    Tags.ModifyTagRepeating(data.Id, TriggerRepeatingType.OneTimeLogicAND);
                    SetProperty(nameof(Rep0Checked));
                    SetProperty(nameof(Rep1Checked));
                    SetProperty(nameof(Rep2Checked));
                }
            }
        }
        public bool Rep2Checked
        {
            get
            {
                if (Tags != null && Tags.FirstOrDefault(x => x.AssoTrigger == data.Id) is TagItem tg)
                {
                    return tg.Repeating == TriggerRepeatingType.RepeatLogicOR;
                }
                return false;
            }
            set
            {
                if (value && HasItem)
                {
                    Tags.ModifyTagRepeating(data.Id, TriggerRepeatingType.RepeatLogicOR);
                    SetProperty(nameof(Rep0Checked));
                    SetProperty(nameof(Rep1Checked));
                    SetProperty(nameof(Rep2Checked));
                }
            }
        }
    }
}
