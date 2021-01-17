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
    public class TriggerCollection : IEnumerable<TriggerItem>, IListSource, IGlobalIdContainer
    {
        private Dictionary<string, TriggerItem> data = new Dictionary<string, TriggerItem>();


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
        public bool HasId(string id)
        {
            return data.Keys.Contains(id);
        }
        public void SetToString(TriggerItem.DisplayingType type)
        {
            foreach (TriggerItem t in this)
            {
                t.SetDisplayingString(type);
            }
        }
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
            if (data.Keys.Contains(trigger.ID)) data.Remove(trigger.ID);
        }
        public TriggerItem NewTrigger(string id, TriggerItem.DisplayingType _type = TriggerItem.DisplayingType.IDandName)
        {
            TriggerItem t = new TriggerItem(TemplateTrigger, id);
            t.Name = TemplateTrigger.Name;
            this[id] = t;
            return t;
        }
        public TriggerItem NewTrigger(string id, TriggerItem src, TriggerItem.DisplayingType type = TriggerItem.DisplayingType.IDandName)
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
                TechnoPair p = new TechnoPair(trigger.ID, trigger.Name);
                result.Add(p);
            }
            return result;
        }
        public void AscendingSort()
        {
            Dictionary<string, TriggerItem> tmp = new Dictionary<string, TriggerItem>();
            foreach (TriggerItem item in data.Values)
            {
                tmp[item.ToString()] = item;
            }
            data.Clear();
            tmp = tmp.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
            foreach (TriggerItem item in tmp.Values)
            {
                data[item.ID] = item;
            }
        }
        public void DecendingSort()
        {
            Dictionary<string, TriggerItem> tmp = new Dictionary<string, TriggerItem>();
            foreach (TriggerItem item in data.Values)
            {
                tmp[item.ToString()] = item;
            }
            data.Clear();
            tmp = tmp.OrderByDescending(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
            foreach (TriggerItem item in tmp.Values)
            {
                data[item.ID] = item;
            }
        }
        #endregion


        #region Public Calls - TriggerCollection
        public TriggerItem TemplateTrigger { get; set; }
        public TriggerItem this[string _id]
        {
            get
            {
                if (data.Keys.Contains(_id)) return data[_id];
                if (_id == "TEMPLATE") return TemplateTrigger;
                return null;
            }
            set
            {
                data[_id] = value;
            }
        }
        public IEnumerable<string> AllId { get { return data.Keys; } }

        #endregion


        #region Enumerator
        public bool ContainsListCollection => data.Values.Count > 0;

        public IEnumerator<TriggerItem> GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }

        public IList GetList()
        {
            return data.Values.ToList();
        }
        #endregion
    }


    public class TriggerItem : ILogicItem
    {
        private DisplayingType displayingType;
        private string innerString;
        [Flags]
        public enum DisplayingType { OnlyID = 0x1, OnlyName = 0x2, IDandName = OnlyID | OnlyName, Remain = 0x4 }


        #region Ctor - TriggerItem
        public TriggerItem(string id, string house, string linkedTriggerID, string name, bool disabled, bool e, bool n, bool h, int repeating)
        {
            ID = id;
            House = house;
            LinkedWith = linkedTriggerID;
            Name = name;
            Disabled = disabled;
            EasyOn = e;
            NormalOn = n;
            HardOn = h;
            Repeating = (TriggerRepeatingType)repeating;
            SetDisplayingString(DisplayingType.IDandName);
        }
        public TriggerItem(TriggerItem src, string id)
        {
            ID = id;
            House = src.House;
            LinkedWith = src.LinkedWith;
            Name = src.Name + " - Clone";
            Disabled = src.Disabled;
            EasyOn = src.EasyOn;
            NormalOn = src.NormalOn;
            HardOn = src.HardOn;
            Repeating = src.Repeating;
            SetDisplayingString(src.displayingType);
            Events = new LogicGroup(src.Events);
            Actions = new LogicGroup(src.Actions);

        }
        public TriggerItem() { }
        #endregion


        #region Public Methods - TriggerItem
        public override string ToString()
        {
            switch (displayingType)
            {
                case DisplayingType.IDandName:
                    return string.Format("{0}:{1}", ID, Name);
                case DisplayingType.OnlyID:
                    return ID;
                case DisplayingType.OnlyName:
                    return Name;
                default:
                    return innerString;
            }
        }
        public void SetDisplayingString(object type)
        {
            int t = int.Parse(type.ToString());
            SetDisplayingString((DisplayingType)t);
        }
        public void SetDisplayingString(DisplayingType type)
        {
            displayingType = type;
            switch (type)
            {
                case DisplayingType.OnlyID:
                    innerString = ID;
                    break;
                case DisplayingType.OnlyName:
                    innerString = Name;
                    break;
                case DisplayingType.IDandName:
                    innerString = ID + ":" + Name;
                    break;
                case DisplayingType.Remain:
                    break;
            }
        }
        #endregion


        #region Public Calls - TriggerItem
        public LogicGroup Events { get; set; }
        public LogicGroup Actions { get; set; }
        public static TriggerItem NullTrigger
        {
            get
            {
                TriggerItem item = new TriggerItem("<none>", "", "", "<none>", true, false, false, false, 0);
                item.SetDisplayingString(DisplayingType.OnlyID);
                return item;
            }
        }
        public string ID { get; set; }
        public string House { get; set; }
        public string LinkedWith { get; set; }
        public string Name { get; set; }
        public bool Disabled { get; set; }
        public bool EasyOn { get; set; }
        public bool NormalOn { get; set; }
        public bool HardOn { get; set; }
        public TriggerRepeatingType Repeating { get; set; }
        public string IDName { get { return ID + ":" + Name; } }

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
