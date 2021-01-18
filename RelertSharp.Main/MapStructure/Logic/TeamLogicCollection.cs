using RelertSharp.IniSystem;
using RelertSharp.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RelertSharp.MapStructure.Logic
{
    public class TeamLogicCollection<T> : IndexableItemCollection<T> where T : IndexableItem
    {
        #region Ctor - TeamLogicCollection
        public TeamLogicCollection() { }
        #endregion


        #region Public Methods - TeamLogicCollection
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
        #endregion


        #region Public Calls - TeamLogicCollection
        #endregion
    }
    public class TeamLogicItem : IndexableItem
    {
        #region Ctor - TeamLogicItem
        public TeamLogicItem(INIEntity ent)
        {
            Id = ent.Name;
        }
        public TeamLogicItem() { }
        #endregion


        #region Public Calls - TeamLogicItem
        public static TeamLogicItem Empty
        {
            get { return new TeamLogicItem(); }
        }
        #endregion
    }
}
