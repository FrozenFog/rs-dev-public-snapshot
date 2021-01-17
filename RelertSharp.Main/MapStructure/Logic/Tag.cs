using RelertSharp.Common;
using RelertSharp.IniSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RelertSharp.MapStructure.Logic
{
    public class TagCollection : IEnumerable<TagItem>, IGlobalIdContainer
    {
        private Dictionary<string, TagItem> data = new Dictionary<string, TagItem>();
        private Dictionary<string, List<string>> trigger_tag = new Dictionary<string, List<string>>();


        #region Ctor - TagCollection
        public TagCollection(INIEntity entTag)
        {
            foreach (INIPair p in entTag.DataList)
            {
                if (!data.Keys.Contains(p.Name))
                {
                    this[p.Name] = new TagItem(p.Name, p.ParseStringList());
                }
            }
        }
        #endregion


        #region Public Methods - TagCollection
        public TagItem NewTag(TriggerItem trg, string id)
        {
            TagItem t = new TagItem(trg, id);
            this[id] = t;
            return t;
        }
        public TagItem NewTag(TagItem src, string id)
        {
            TagItem t = new TagItem(src, id);
            this[id] = t;
            return t;
        }
        public void Remove(TagItem t, string triggerid)
        {
            if (data.Keys.Contains(t.Id)) data.Remove(t.Id);
            if (trigger_tag.Keys.Contains(triggerid)) trigger_tag.Remove(triggerid);
        }
        public bool HasId(string id)
        {
            return data.Keys.Contains(id);
        }
        public void RemoveRelation(string triggerid)
        {
            if (trigger_tag.ContainsKey(triggerid))
            {
                if (trigger_tag[triggerid].Count == 1) data.Remove(trigger_tag[triggerid].First());
                trigger_tag.Remove(triggerid);
            }
        }
        /// <summary>
        /// return a tag with certain trigger id, return null tag if not found
        /// </summary>
        /// <param name="triggerID"></param>
        /// <returns></returns>
        public List<TagItem> GetTagFromTrigger(string triggerID, TriggerItem item = null)
        {
            if (triggerID == "TEMPLATE") return new List<TagItem>(new TagItem[] { new TagItem(item, "TGMPLATE") });
            List<TagItem> ret = new List<TagItem>();
            if (trigger_tag.ContainsKey(triggerID))
                foreach (var i in trigger_tag[triggerID])
                    ret.Add(data[i]);
            return
                ret.Count > 0
                ? ret
                : new List<TagItem>(new TagItem[] { new TagItem("xxxxxxxx", new string[3] { "0", "!NO AVAIABLE TAG!", "<none>" }) });
        }
        public IEnumerable<TechnoPair> ToTechno()
        {
            List<TechnoPair> result = new List<TechnoPair>();
            foreach (TagItem tag in this)
            {
                result.Add(new TechnoPair(tag.Id, tag.Name));
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
                if (!trigger_tag.ContainsKey(value.AssoTrigger)) trigger_tag[value.AssoTrigger] = new List<string>();
                trigger_tag[value.AssoTrigger].Add(value.Id);
            }
        }
        public IEnumerable<string> AllId { get { return data.Keys; } }
        public TagItem TemplateTag { get; set; }
        #endregion
    }


    public class TagItem : IndexableItem
    {
        #region Ctor - TagItem
        public TagItem(string _id, string[] dataList)
        {
            Id = _id;
            if (dataList.Length != 3)
            {
                //logger
                return;
            }
            Repeating = (TriggerRepeatingType)(int.Parse(dataList[0]));
            Name = dataList[1];
            AssoTrigger = dataList[2];
        }
        public TagItem(TagItem src, string id)
        {
            Id = id;
            Repeating = src.Repeating;
            Name = src.Name + " - Clone";
            AssoTrigger = src.AssoTrigger;
        }
        public TagItem(TriggerItem trg, string _id)
        {
            Id = _id;
            Repeating = trg.Repeating;
            Name = trg.Name + " - Tag";
            AssoTrigger = trg.Id;
        }
        public TagItem(string id, string name)
        {
            Id = id;
            Name = name;
        }
        #endregion


        #region Public Calls - TagItem
        public IEnumerable<object> SaveData
        {
            get
            {
                return new List<object>()
                {
                    (int)Repeating, Name, AssoTrigger
                };
            }
        }
        public override string ToString() { return Id + ' ' + Name; }
        public TriggerRepeatingType Repeating { get; set; }
        public string AssoTrigger { get; set; }
        public static TagItem NullTag { get { return new TagItem("None", ""); } }
        public bool Binded { get; set; } = true;
        #endregion
    }
}
