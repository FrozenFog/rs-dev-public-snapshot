using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.IniSystem;

namespace relert_sharp.MapStructure.Logic
{
    public class TeamLogicCollection : IEnumerable<TeamLogicItem>
    {
        private Dictionary<string, TeamLogicItem> data = new Dictionary<string, TeamLogicItem>();


        #region Constructor - TeamLogicCollection
        public TeamLogicCollection() { }
        #endregion


        #region Public Methods - TeamLogicCollection
        public void AscendingSort()
        {
            data = data.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
        }
        public void DescendingSort()
        {
            data = data.OrderByDescending(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
        }
        #region Enumerator
        public IEnumerator<TeamLogicItem> GetEnumerator()
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
        public TeamLogicItem this[string _ID]
        {
            get
            {
                if (data.Keys.Contains(_ID)) return data[_ID];
                return TeamLogicItem.Empty;
            }
            set
            {
                data[_ID] = value;
            }
        }
        public Dictionary<string, TeamLogicItem>.KeyCollection Keys { get { return data.Keys; } }
        #endregion
    }
    public class TeamLogicItem
    {
        public TeamLogicItem(INIEntity ent)
        {
            ID = ent.Name;
        }
        public TeamLogicItem() { }
        public string ID { get; set; }
        public static TeamLogicItem Empty
        {
            get { return new TeamLogicItem(); }
        }
    }
}
