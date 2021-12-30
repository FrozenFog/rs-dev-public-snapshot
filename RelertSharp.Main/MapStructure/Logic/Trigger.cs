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
        internal void Clear()
        {
            data.Clear();
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
                LogicType = TriggerSubType.Event
            };
            t.Actions = new LogicGroup()
            {
                ParentID = t.Id,
                LogicType = TriggerSubType.Action
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


    public class TriggerItem : IndexableItem, IOwnableObject, ILogicItem
    {
        public LogicType ItemType { get { return LogicType.Trigger; } }
        #region Ctor - TriggerItem
        public TriggerItem(string id, string house, string linkedTriggerID, string name, bool disabled, bool e, bool n, bool h, int repeating)
        {
            Id = id;
            Owner = house;
            LinkedWith = linkedTriggerID;
            Name = name;
            Disabled = disabled;
            EasyOn = e;
            NormalOn = n;
            HardOn = h;
        }
        public TriggerItem(TriggerItem src, string id)
        {
            Id = id;
            Owner = src.Owner;
            LinkedWith = src.LinkedWith;
            Name = src.Name + Constant.CLONE_SUFFIX;
            Disabled = src.Disabled;
            EasyOn = src.EasyOn;
            NormalOn = src.NormalOn;
            HardOn = src.HardOn;
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
        public string[] ExtractParameter()
        {
            List<string> r = new List<string>()
            {
                Id,
                Name,
                Owner,
                LinkedWith,
                Disabled.ZeroOne(),
                EasyOn.ZeroOne(),
                NormalOn.ZeroOne(),
                HardOn.ZeroOne(),
                ((int)Repeating).ToString(),
                Events.Count().ToString(),
                Actions.Count().ToString()
            };
            foreach (var e in Events)
            {
                r.Add(e.ID.ToString());
                foreach (var param in e.Info.Parameters)
                {
                    r.Add(e.GetParameter(param));
                }
            }
            foreach (var a in Actions)
            {
                r.Add(a.ID.ToString());
                foreach (var param in a.Info.Parameters)
                {
                    r.Add(a.GetParameter(param));
                }
            }
            return r.ToArray();
        }
        public int GetChecksum()
        {
            return ExtractParameter().GetHashCode();
        }
        #endregion


        #region Private Methods
        #endregion

        #region Public Calls - TriggerItem
        private string compileName;
        public string CompileName
        {
            get { if (compileName.IsNullOrEmpty()) return Name; return compileName; }
            set { compileName = value; }
        }
        public LogicGroup Events { get; set; }
        public LogicGroup Actions { get; set; }
        /// <summary>
        /// Country
        /// </summary>
        public string Owner { get; set; }
        public string LinkedWith { get; set; } = Constant.ITEM_NONE;
        public bool Disabled { get; set; }
        public bool EasyOn { get; set; } = true;
        public bool NormalOn { get; set; } = true;
        public bool HardOn { get; set; } = true;
        /// <summary>
        /// Obsolete
        /// </summary>
        public TriggerRepeatingType Repeating { get { return TriggerRepeatingType.NoRepeating; } }
        public string IDName { get { return Id + ":" + Name; } }

        public IEnumerable<object> SaveData
        {
            get
            {
                return new List<object>()
                {
                    Owner, LinkedWith, CompileName, Disabled.ZeroOne(), EasyOn.ZeroOne(), NormalOn.ZeroOne(), HardOn.ZeroOne(), (int)Repeating
                };
            }
        }
        #endregion
    }
}
