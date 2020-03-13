using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using RelertSharp.Model;
using static RelertSharp.Utils.Misc;
using System.Collections;

namespace RelertSharp.MapStructure.Logic
{
    public class TriggerCollection : IEnumerable<TriggerItem>
    {
        private Dictionary<string, TriggerItem> data = new Dictionary<string, TriggerItem>();


        #region Ctor - TriggerCollection
        public TriggerCollection(INIEntity entTrigger)
        {
            foreach (INIPair p in entTrigger.DataList)
            {
                if (!data.Keys.Contains(p.Name))
                {
                    string[] l = p.ParseStringList();
                    data[p.Name] = new TriggerItem(p.Name, l[0], l[1], l[2], ParseBool(l[3]), ParseBool(l[4]), ParseBool(l[5]), ParseBool(l[6]), int.Parse(l[7]));
                }
            }
        }
        #endregion


        #region Private Methods - TriggerCollection

        #endregion


        #region Public Methods - TriggerCollection
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
        public TriggerItem this[string _id]
        {
            get
            {
                if (data.Keys.Contains(_id)) return data[_id];
                return null;
            }
            set
            {
                data[_id] = value;
            }
        }
        public Dictionary<string, TriggerItem>.KeyCollection Keys { get { return data.Keys; } }
        #endregion


        #region Enumerator
        public IEnumerator<TriggerItem> GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }
        #endregion
    }


    public class TriggerItem : BindableBase
    {
        private string innerString;
        private string id, house, linkedwith, name;
        private bool disabled, ez, nm, hd;
        private TriggerRepeatingType repeattype;
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
        #endregion


        #region Public Methods - TriggerItem
        public override string ToString()
        {
            return innerString;
        }
        public void SetDisplayingString(DisplayingType type)
        {
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
        public static TriggerItem NullTrigger
        {
            get
            {
                TriggerItem item = new TriggerItem("<none>", "", "", "<none>", true, false, false, false, 0);
                item.SetDisplayingString(DisplayingType.OnlyID);
                return item;
            }
        }
        public string ID
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }
        public string House
        {
            get { return house; }
            set { SetProperty(ref house, value); }
        }
        public string LinkedWith
        {
            get { return linkedwith; }
            set { SetProperty(ref linkedwith, value); }
        }
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        public bool Disabled
        {
            get { return disabled; }
            set { SetProperty(ref disabled, value); }
        }
        public bool EasyOn
        {
            get { return ez; }
            set { SetProperty(ref ez, value); }
        }
        public bool NormalOn
        {
            get { return nm; }
            set { SetProperty(ref nm, value); }
        }
        public bool HardOn
        {
            get { return hd; }
            set { SetProperty(ref hd, value); }
        }
        public TriggerRepeatingType Repeating
        {
            get { return repeattype; }
            set { SetProperty(ref repeattype, value); }
        }
        public string IDName { get { return ID + ":" + Name; } }
        #endregion
    }
}
