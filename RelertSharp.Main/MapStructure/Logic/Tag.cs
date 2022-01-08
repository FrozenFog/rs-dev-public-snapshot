using RelertSharp.Common;
using RelertSharp.IniSystem;
using System.Collections.Generic;
using System.Linq;

namespace RelertSharp.MapStructure.Logic
{
    [IniEntitySerialize(Constant.MapStructure.ENT_TAG)]
    public class TagCollection : IndexableItemCollection<TagItem>, ICurdContainer<TagItem>, IIniEntitySerializable
    {
        #region Ctor - TagCollection
        public TagCollection()
        {

        }


        public void ReadFromIni(INIEntity src)
        {
            foreach (INIPair p in src)
            {
                TagItem t = new TagItem();
                t.ReadFromIni(p);
                this[p.Name] = t;
            }
        }

        public INIEntity SaveAsIni()
        {
            INIEntity ent = this.GetNamedEnt();
            foreach (TagItem t in this)
            {
                ent.AddPair(t.SaveAsIni());
            }
            return ent;
        }
        #endregion


        #region Public Methods - TagCollection
        internal void Clear()
        {
            data.Clear();
        }
        public void ModifyTagRepeating(string triggerId, TriggerRepeatingType repeating)
        {
            foreach (var tag in this.Where(x => x.AssoTrigger == triggerId))
            {
                tag.Repeating = repeating;
            }
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


    public class TagItem : IndexableItem, ILogicItem, IIniPairSerializable
    {
        #region Ctor - TagItem
        internal TagItem() { }
        internal TagItem(TagItem src, string id)
        {
            Id = id;
            Repeating = src.Repeating;
            Name = src.Name + Constant.CLONE_SUFFIX;
            AssoTrigger = src.AssoTrigger;
        }
        internal TagItem(TriggerItem trg, string _id)
        {
            Id = _id;
            Repeating = trg.Repeating;
            Name = trg.Name + Constant.TAG_SUFFIX;
            AssoTrigger = trg.Id;
        }
        internal TagItem(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public void ReadFromIni(INIPair src)
        {
            Id = src.Name;
            ParameterReader reader = new ParameterReader(src.ParseStringList());
            Repeating = (TriggerRepeatingType)reader.ReadInt();
            Name = reader.ReadString();
            AssoTrigger = reader.ReadString();
            if (reader.ReadError) GlobalVar.Monitor.LogCritical(Id, Name, LogicType.Tag, this);
        }

        public INIPair SaveAsIni()
        {
            ParameterWriter writer = new ParameterWriter();
            INIPair dest = new INIPair(Id);
            writer.Write((int)Repeating);
            writer.Write(Name);
            writer.Write(AssoTrigger);
            dest.Value = writer.ToString();
            return dest;
        }
        #endregion


        #region Public Methods
        public string[] ExtractParameter()
        {
            return new string[]
            {
                Id,
                Name,
                AssoTrigger,
                Repeating.ToString()
            };
        }
        public int GetChecksum()
        {
            return ExtractParameter().GetHashCode();
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
