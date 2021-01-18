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
    public class TriggerCollection : IndexableItemCollection<TriggerItem>, IListSource
    {
        #region Ctor - TriggerCollection
        public TriggerCollection()
        {
            TemplateTrigger = new TriggerItem("TEMPLATE", "<none>", "<none>", "DefaultTrigger", false, true, true, true, 0);
            TemplateTrigger.Events = new LogicGroup();
            TemplateTrigger.Actions = new LogicGroup();
        }
        #endregion


        #region Private Methods - TriggerCollection

        #endregion


        #region Public Methods - TriggerCollection
        public void LoadTriggerCommand(INIPair p)
        {
            if (!data.Keys.Contains(p.Name))
            {
                string[] l = p.ParseStringList();
                data[p.Name] = new TriggerItem(p.Name, l[0], l[1], l[2], ParseBool(l[3]), ParseBool(l[4]), ParseBool(l[5]), ParseBool(l[6]), int.Parse(l[7]));
            }
        }
        public void RemoveTrigger(TriggerItem trigger)
        {
            if (data.Keys.Contains(trigger.Id)) data.Remove(trigger.Id);
        }
        public TriggerItem NewTrigger(string id)
        {
            TriggerItem t = new TriggerItem(TemplateTrigger, id);
            t.Name = TemplateTrigger.Name;
            this[id] = t;
            return t;
        }
        public TriggerItem NewTrigger(string id, TriggerItem src)
        {
            TriggerItem t = new TriggerItem(src, id);
            this[id] = t;
            return t;
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
        public TriggerItem TemplateTrigger { get; set; }
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
    }


    public class TriggerItem : IndexableItem
    {
        #region Ctor - TriggerItem
        public TriggerItem(string id, string house, string linkedTriggerID, string name, bool disabled, bool e, bool n, bool h, int repeating)
        {
            Id = id;
            House = house;
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
            House = src.House;
            LinkedWith = src.LinkedWith;
            Name = src.Name + " - Clone";
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

        #endregion


        #region Public Calls - TriggerItem
        public LogicGroup Events { get; set; }
        public LogicGroup Actions { get; set; }
        public static TriggerItem NullTrigger
        {
            get
            {
                TriggerItem item = new TriggerItem("<none>", "", "", "<none>", true, false, false, false, 0);
                return item;
            }
        }
        public string House { get; set; }
        public string LinkedWith { get; set; }
        public bool Disabled { get; set; }
        public bool EasyOn { get; set; }
        public bool NormalOn { get; set; }
        public bool HardOn { get; set; }
        public TriggerRepeatingType Repeating { get; set; }
        public string IDName { get { return Id + ":" + Name; } }

        public IEnumerable<object> SaveData
        {
            get
            {
                return new List<object>()
                {
                    House, LinkedWith, Name, Disabled, EasyOn, NormalOn, HardOn, (int)Repeating
                };
            }
        }
        #endregion
    }
}
