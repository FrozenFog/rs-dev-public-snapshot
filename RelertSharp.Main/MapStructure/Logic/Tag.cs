using RelertSharp.Common;
using RelertSharp.IniSystem;
using System.Collections.Generic;
using System.Linq;

namespace RelertSharp.MapStructure.Logic
{
    public class TagCollection : IndexableItemCollection<TagItem>, ICurdContainer<TagItem>
    {
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
        ///// <summary>
        ///// return a tag with certain trigger id, return null tag if not found
        ///// </summary>
        ///// <param name="triggerID"></param>
        ///// <returns></returns>
        //public List<TagItem> GetTagFromTrigger(string triggerID, TriggerItem item = null)
        //{
        //    if (triggerID == "TEMPLATE") return new List<TagItem>(new TagItem[] { new TagItem(item, "TGMPLATE") });
        //    List<TagItem> ret = new List<TagItem>();
        //    if (trigger_tag.ContainsKey(triggerID))
        //        foreach (var i in trigger_tag[triggerID])
        //            ret.Add(data[i]);
        //    return
        //        ret.Count > 0
        //        ? ret
        //        : new List<TagItem>(new TagItem[] { new TagItem("xxxxxxxx", new string[3] { "0", "!NO AVAIABLE TAG!", "<none>" }) });
        //}
        public IEnumerable<TechnoPair> ToTechno()
        {
            List<TechnoPair> result = new List<TechnoPair>();
            foreach (TagItem tag in this)
            {
                result.Add(new TechnoPair(tag.Id, tag.Name));
            }
            return result;
        }

        #region Curd
        //public void Remove(TagItem t, string triggerid)
        //{
        //    if (data.Keys.Contains(t.Id)) data.Remove(t.Id);
        //    if (trigger_tag.Keys.Contains(triggerid)) trigger_tag.Remove(triggerid);
        //}
        public TagItem AddItem(string id, string name)
        {
            TagItem t = new TagItem(id, name);
            this[id] = t;
            return t;
        }

        public TagItem AddItem(string id, TriggerItem src)
        {
            TagItem t = new TagItem(src, id);
            this[id] = t;
            return t;
        }

        public TagItem CopyItem(TagItem src, string id)
        {
            TagItem t = new TagItem(src, id);
            this[id] = t;
            return t;
        }

        public bool ContainsItem(TagItem target)
        {
            return data.ContainsKey(target.Id);
        }

        public bool ContainsItem(string id, string obsolete = "")
        {
            return data.ContainsKey(id);
        }

        public bool RemoveItem(TagItem target)
        {
            if (data.ContainsKey(target.Id))
            {
                return data.Remove(target.Id);
            }
            return false;
        }

        /// <summary>
        /// Returns all tag id that removed by trigger
        /// </summary>
        /// <param name="trg"></param>
        /// <returns></returns>
        public IEnumerable<string> RemoveItem(TriggerItem trg)
        {
            HashSet<TagItem> target = new HashSet<TagItem>();
            foreach (TagItem tag in this)
            {
                if (tag.AssoTrigger == trg.Id) target.Add(tag);
            }
            target.Foreach(x => RemoveItem(x));
            return target.Select((x) => { return x.Id; });
        }
        #endregion
        #endregion


        #region Public Calls - TagCollection
        public new TagItem this[string _id]
        {
            get
            {
                if (data.Keys.Contains(_id)) return data[_id];
                return null;
            }
            private set
            {
                data[_id] = value;
            }
        }
        #endregion
    }


    public class TagItem : IndexableItem, ILogicItem
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
            Name = src.Name + Constant.CLONE_SUFFIX;
            AssoTrigger = src.AssoTrigger;
        }
        public TagItem(TriggerItem trg, string _id)
        {
            Id = _id;
            Repeating = trg.Repeating;
            Name = trg.Name + Constant.TAG_SUFFIX;
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
        public LogicType ItemType { get { return LogicType.Tag; } }
        #endregion
    }
}
