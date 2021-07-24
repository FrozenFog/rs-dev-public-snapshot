using RelertSharp.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RelertSharp.MapStructure.Logic
{
    public class LocalVarCollection : IEnumerable<LocalVarItem>, ICurdContainer<LocalVarItem>
    {
        private Dictionary<string, LocalVarItem> data = new Dictionary<string, LocalVarItem>();
        private string NewId
        {
            get
            {
                for (int i = 0; i < Constant.MapStructure.CAPACITY_LOCALVAR; i++)
                {
                    if (!data.ContainsKey(i.ToString())) return i.ToString();
                }
                return string.Empty;
            }
        }

        #region Ctor - LocalVarCollection
        public LocalVarCollection() { }
        #endregion


        #region Public Methods - LocalVarCollection
        /// <summary>
        /// id is obsoleted
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public LocalVarItem AddItem(string id, string name)
        {
            id = NewId;
            if (id.IsNullOrEmpty()) return null;
            LocalVarItem local = new LocalVarItem(name, false, id);
            this[id] = local;
            return local;
        }

        public LocalVarItem CopyItem(LocalVarItem src, string id)
        {
            id = NewId;
            if (id.IsNullOrEmpty()) return null;
            LocalVarItem local = new LocalVarItem(src, id);
            this[id] = local;
            return local;
        }

        public bool ContainsItem(LocalVarItem look)
        {
            return data.ContainsKey(look.Id);
        }

        public bool ContainsItem(string id, string param2)
        {
            return data.ContainsKey(id);
        }

        public bool RemoveItem(LocalVarItem target)
        {
            if (ContainsItem(target))
            {
                return data.Remove(target.Id);
            }
            return false;
        }
        #region Enumerator
        public IEnumerator<LocalVarItem> GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }
        #endregion
        #endregion


        #region Public Calls - LocalValCollection
        /// <summary>
        /// index,name
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LocalVarItem this[string id]
        {
            get { return data.Keys.Contains(id) ? data[id] : null; }
            set { data[id] = value; }
        }

        #endregion
    }


    public class LocalVarItem : IndexableItem, ILogicItem
    {
        #region Ctor - LocalVarItem
        public LocalVarItem(string name, bool init, string index) : this(name, init, int.Parse(index)) { }
        public LocalVarItem(string name, bool init, int index)
        {
            Name = name;
            InitState = init;
            Id = index.ToString();
        }
        public LocalVarItem()
        {
            Name = Constant.VALUE_NONE;
            Id = Constant.ID_INVALID;
        }
        public LocalVarItem(LocalVarItem src, string id)
        {
            Name = src.Name + Constant.CLONE_SUFFIX;
            Id = id;
        }
        #endregion


        #region Public Calls - LocalVarItem
        public override string ToString() { return string.Format("{0}: {1} ({2})", Id, Name, InitState); }
        public bool InitState { get; set; }
        public LogicType ItemType { get { return LogicType.LocalVariable; } }
        #endregion
    }
}
