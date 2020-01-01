using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.FileSystem;
using relert_sharp.Common;
using static relert_sharp.Utils.Misc;

namespace relert_sharp.MapStructure.Logic
{
    public class TriggerCollection
    {
        private Dictionary<string, TriggerItem> data = new Dictionary<string, TriggerItem>();
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
    }


    public class TriggerItem
    {


        #region Constructor - TriggerItem
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
        }
        #endregion


        #region Public Calls - TriggerItem
        public string ID { get; set; }
        public string House { get; set; }
        public string LinkedWith { get; set; }
        public string Name { get; set; }
        public bool Disabled { get; set; }
        public bool EasyOn { get; set; }
        public bool NormalOn { get; set; }
        public bool HardOn { get; set; }
        public TriggerRepeatingType Repeating { get; set; }
        #endregion
    }
}
