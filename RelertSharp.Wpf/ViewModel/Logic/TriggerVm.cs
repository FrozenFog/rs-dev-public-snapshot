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
        private Map map { get { return GlobalVar.CurrentMapDocument.Map; } }
        public TriggerVm()
        {
            data = new TriggerItem();
        }
        public TriggerVm(object obj) : base(obj) { }

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
            get { return data.Repeating == TriggerRepeatingType.NoRepeating; }
            set
            {
                if (value)
                {
                    data.Repeating = TriggerRepeatingType.NoRepeating;
                    SetProperty();
                }
            }
        }
        public bool Rep1Checked
        {
            get { return data.Repeating == TriggerRepeatingType.OneTimeLogicAND; }
            set
            {
                if (value)
                {
                    data.Repeating = TriggerRepeatingType.OneTimeLogicAND;
                    SetProperty();
                }
            }
        }
        public bool Rep2Checked
        {
            get { return data.Repeating == TriggerRepeatingType.RepeatLogicOR; }
            set
            {
                if (value)
                {
                    data.Repeating = TriggerRepeatingType.RepeatLogicOR;
                    SetProperty();
                }
            }
        }
    }
}
