using RelertSharp.Common;
using RelertSharp.IniSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure.Logic
{
    public class TriggerCollection : IndexableItemCollection<TriggerItem>, IListSource, ICurdContainer<TriggerItem>
    {
        #region Ctor - TriggerCollection
        public TriggerCollection()
        {

        }
        #endregion


        #region Private Methods - TriggerCollection

        #endregion


        #region Public Methods - TriggerCollection
        public void ReadTriggerFromIni(INIPair p)
        {
            if (!data.Keys.Contains(p.Name))
            {
                string[] l = p.ParseStringList();
                data[p.Name] = new TriggerItem(p.Name, l[0], l[1], l[2], IniParseBool(l[3]), IniParseBool(l[4]), IniParseBool(l[5]), IniParseBool(l[6]), int.Parse(l[7]));
            }
        }
        public IEnumerable<TechnoPair> ToTechno()
        {
            List<TechnoPair> result = new List<TechnoPair>();
            foreach (TriggerItem trigger in data.Values)
            {
                TechnoPair p = new TechnoPair(trigger.Id, trigger.Name);
                result.Add(p);
            }
            return result;
        }
        #endregion


        #region Public Calls - TriggerCollection
        //public TriggerItem this[string _id]
        //{
        //    get
        //    {
        //        if (data.Keys.Contains(_id)) return data[_id];
        //        if (_id == "TEMPLATE") return TemplateTrigger;
        //        return null;
        //    }
        //    set
        //    {
        //        data[_id] = value;
        //    }
        //}

        #endregion


        #region Enumerator
        public bool ContainsListCollection => data.Values.Count > 0;

        public IList GetList()
        {
            return data.Values.ToList();
        }
        #endregion


        #region Curd
        public TriggerItem AddItem(string id, string name)
        {
            TriggerItem t = new TriggerItem()
            {
                Id = id,
                Name = name
            };
            t.Events = new LogicGroup()
            {
                ParentID = t.Id,
                LogicType = TriggerSubType.EventLogic
            };
            t.Actions = new LogicGroup()
            {
                ParentID = t.Id,
                LogicType = TriggerSubType.ActionLogic
            };
            this[id] = t;
            return t;
        }

        public TriggerItem CopyItem(TriggerItem src, string id)
        {
            TriggerItem t = new TriggerItem(src, id);
            this[id] = t;
            return t;
        }

        public bool RemoveItem(TriggerItem target)
        {
            if (data.ContainsKey(target.Id))
            {
                return data.Remove(target.Id);
            }
            return false;
        }

        public bool ContainsItem(TriggerItem item)
        {
            return data.ContainsKey(item.Id);
        }

        public bool ContainsItem(string id, string obsolete = "")
        {
            return data.ContainsKey(id);
        }
        #endregion
    }


    public class TriggerItem : IndexableItem
    {
        #region Ctor - TriggerItem
        public TriggerItem(string id, string house, string linkedTriggerID, string name, bool disabled, bool e, bool n, bool h, int repeating)
        {
            Id = id;
            OwnerCountry = house;
            LinkedWith = linkedTriggerID;
            Name = name;
            Disabled = disabled;
            EasyOn = e;
            NormalOn = n;
            HardOn = h;
            Repeating = (TriggerRepeatingType)repeating;
        }
        public TriggerItem(TriggerItem src, string id)
        {
            Id = id;
            OwnerCountry = src.OwnerCountry;
            LinkedWith = src.LinkedWith;
            Name = src.Name + Constant.CLONE_SUFFIX;
            Disabled = src.Disabled;
            EasyOn = src.EasyOn;
            NormalOn = src.NormalOn;
            HardOn = src.HardOn;
            Repeating = src.Repeating;
            Events = new LogicGroup(src.Events);
            Actions = new LogicGroup(src.Actions);

        }
        public TriggerItem() { }
        #endregion


        #region Public Methods - TriggerItem
        public void InvokeChildInfoRefreshAll()
        {
            foreach (LogicItem lg in Events) lg.OnInfoRefreshInvoked();
            foreach (LogicItem lg in Actions) lg.OnInfoRefreshInvoked();
        }
        #endregion


        #region Private Methods
        /// <summary>
        /// modify all tag repeating type that related to this trigger
        /// </summary>
        private void ModifyRepeating()
        {
            GlobalVar.CurrentMapDocument?.Map?.Tags?.Foreach(x =>
            {
                if (x.AssoTrigger == this.Id) x.Repeating = repeating;
            });
        }
        #endregion

        #region Public Calls - TriggerItem
        public LogicGroup Events { get; set; }
        public LogicGroup Actions { get; set; }
        public string OwnerCountry { get; set; }
        public string LinkedWith { get; set; } = Constant.ITEM_NONE;
        public bool Disabled { get; set; }
        public bool EasyOn { get; set; } = true;
        public bool NormalOn { get; set; } = true;
        public bool HardOn { get; set; } = true;
        private TriggerRepeatingType repeating = TriggerRepeatingType.NoRepeating;
        public TriggerRepeatingType Repeating
        {
            get { return repeating; }
            set
            {
                repeating = value;
                ModifyRepeating();
            }
        }
        public string IDName { get { return Id + ":" + Name; } }

        public IEnumerable<object> SaveData
        {
            get
            {
                return new List<object>()
                {
                    OwnerCountry, LinkedWith, Name, Disabled.ZeroOne(), EasyOn.ZeroOne(), NormalOn.ZeroOne(), HardOn.ZeroOne(), (int)Repeating
                };
            }
        }
        #endregion
    }
}
