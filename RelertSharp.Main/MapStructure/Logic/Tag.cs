using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.IniSystem;
using relert_sharp.Common;
using System.Collections;

namespace relert_sharp.MapStructure.Logic
{
    public class TagCollection : IEnumerable<TagItem>
    {
        private Dictionary<string, TagItem> data = new Dictionary<string, TagItem>();
        private Dictionary<string, string> trigger_tag = new Dictionary<string, string>();


        #region Constructor - TagCollection
        public TagCollection(INIEntity entTag)
        {
            foreach (INIPair p in entTag.DataList)
            {
                if (!data.Keys.Contains(p.Name))
                {
                    data[p.Name] = new TagItem(p.Name, p.ParseStringList());
                    trigger_tag[data[p.Name].AssoTrigger] = p.Name;
                }
            }
        }
        #endregion


        #region Public Methods - TagCollection
        /// <summary>
        /// return a tag with certain trigger id, return null tag if not found
        /// </summary>
        /// <param name="triggerID"></param>
        /// <returns></returns>
        public TagItem GetTagFromTrigger(string triggerID)
        {
            if (trigger_tag.Keys.Contains(triggerID))
            {
                return data[trigger_tag[triggerID]];
            }
            TagItem nullitem = new TagItem("xxxxxxxx", new string[3]{ "0","!NO AVAIABLE TAG!","<none>"});
            return nullitem;
        }
        public IEnumerable<TechnoPair> ToTechno()
        {
            List<TechnoPair> result = new List<TechnoPair>();
            foreach (TagItem tag in this)
            {
                result.Add(new TechnoPair(tag.ID, tag.Name));
            }
            return result;
        }

        #region Enumerator
        public IEnumerator<TagItem> GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }
        #endregion
        #endregion


        #region Public Calls - TagCollection
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
        #endregion
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
