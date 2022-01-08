using RelertSharp.Common;
using RelertSharp.IniSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RelertSharp.MapStructure.Logic
{
    [IniEntitySerialize(Constant.MapStructure.ENT_VAR)]
    public class LocalVarCollection : IEnumerable<LocalVarItem>, ICurdContainer<LocalVarItem>, IIniEntitySerializable
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
        public void ReadFromIni(INIEntity src)
        {
            foreach (INIPair p in src)
            {
                LocalVarItem var = new LocalVarItem();
                var.ReadFromIni(p);
                this[p.Name] = var;
            }
        }

        public INIEntity SaveAsIni()
        {
            INIEntity dest = this.GetNamedEnt();
            foreach (var item in this)
            {
                dest.AddPair(item.SaveAsIni());
            }
            return dest;
        }
        #endregion


        #region Public Methods - LocalVarCollection
        internal void Clear()
        {
            data.Clear();
        }
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


    public class LocalVarItem : IndexableItem, ILogicItem, IIniPairSerializable
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

        public void ReadFromIni(INIPair src)
        {
            Id = src.Name;
            ParameterReader reader = new ParameterReader(src.ParseStringList());
            Name = reader.ReadString();
            InitState = reader.ReadBool();
            if (reader.ReadError) GlobalVar.Monitor.LogCritical(Id, Name, LogicType.LocalVariable, this);
        }

        public INIPair SaveAsIni()
        {
            INIPair dest = new INIPair(Id);
            ParameterWriter writer = new ParameterWriter();
            writer.Write(Name);
            writer.Write(InitState);
            dest.Value = writer.ToString();
            return dest;
        }
        #endregion


        #region Public methods
        public string[] ExtractParameter()
        {
            return new string[]
            {
                Id,
                Name,
                InitState.ZeroOne()
            };
        }
        public int GetChecksum()
        {
            return ExtractParameter().GetHashCode();
        }
        #endregion


        #region Public Calls - LocalVarItem
        public override string ToString() { return string.Format("{0}: {1} ({2})", Id, Name, InitState); }
        public bool InitState { get; set; }
        public LogicType ItemType { get { return LogicType.LocalVariable; } }
        #endregion
    }
}
