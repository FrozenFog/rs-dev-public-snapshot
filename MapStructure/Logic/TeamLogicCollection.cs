using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.FileSystem;

namespace relert_sharp.MapStructure.Logic
{
    public class TeamLogicCollection
    {
        private Dictionary<string, TeamLogicItem> data = new Dictionary<string, TeamLogicItem>();
        public TeamLogicCollection() { }
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
