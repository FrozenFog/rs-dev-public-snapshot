﻿using RelertSharp.IniSystem;
using RelertSharp.Model;
using RelertSharp.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RelertSharp.MapStructure.Logic
{
    public class TeamLogicCollection<T> : IEnumerable<T>, IGlobalIdContainer where T : class
    {
        private Dictionary<string, T> data = new Dictionary<string, T>();


        #region Ctor - TeamLogicCollection
        public TeamLogicCollection() { }
        #endregion


        #region Public Methods - TeamLogicCollection
        public void AscendingSort()
        {
            data = data.OrderBy(x => int.Parse(x.Key)).ToDictionary(x => x.Key, y => y.Value);
        }
        public void DescendingSort()
        {
            data = data.OrderByDescending(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
        }
        public bool Remove(string ID)
        {
            if (!data.ContainsKey(ID)) return false;
            return data.Remove(ID);
        }
        public bool Exists(string ID)
        {
            return data.ContainsKey(ID);
        }
        public bool ValueExists(T Value)
        {
            return data.ContainsValue(Value);
        }
        public bool HasId(string id)
        {
            return data.Keys.Contains(id);
        }
        #region Enumerator
        public IEnumerator<T> GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }
        #endregion
        #endregion


        #region Public Calls - TeamLogicCollection
        public T this[string _ID]
        {
            get
            {
                if (data.Keys.Contains(_ID)) return data[_ID];
                return null;
            }
            set
            {
                data[_ID] = value;
            }
        }
        public IEnumerable<string> AllId { get { return data.Keys; } }
        public int Length { get { return data.Count; } }
        #endregion
    }
    public class TeamLogicItem : BindableBase
    {
        private string id;


        #region Ctor - TeamLogicItem
        public TeamLogicItem(INIEntity ent)
        {
            ID = ent.Name;
        }
        public TeamLogicItem() { }
        #endregion


        #region Public Calls - TeamLogicItem
        public virtual string ID
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }
        public static TeamLogicItem Empty
        {
            get { return new TeamLogicItem(); }
        }
        #endregion
    }
}
