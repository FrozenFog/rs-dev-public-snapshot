using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Model;
using RelertSharp.IniSystem;

namespace RelertSharp.MapStructure.Logic
{
    public class TeamLogicCollection<T> : IEnumerable<T>
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
                return default(T);
            }
            set
            {
                data[_ID] = value;
            }
        }
        public Dictionary<string, T>.KeyCollection Keys { get { return data.Keys; } }
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
        public string ID
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
