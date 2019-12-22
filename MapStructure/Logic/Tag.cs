using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.FileSystem;
using relert_sharp.Common;

namespace relert_sharp.MapStructure.Logic
{
    public class TagCollection
    {
        private Dictionary<string, TagItem> data = new Dictionary<string, TagItem>();
        public TagCollection(INIEntity entTag)
        {
            foreach (INIPair p in entTag.DataList)
            {
                if (!data.Keys.Contains(p.Name)) data[p.Name] = new TagItem(p.Name, p.ParseStringList());
            }
        }
        public TagItem this[string _id]
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
    public class TagItem
    {
        public TagItem(string _id, string[] dataList)
        {
            ID = _id;
            Repeating = (TriggerRepeatingType)(int.Parse(dataList[0]));
            Name = dataList[1];
            AssoTrigger = dataList[2];
        }
        public TriggerRepeatingType Repeating { get; set; }
        public string Name { get; set; }
        public string AssoTrigger { get; set; }
        public string ID { get; set; }
    }
}
