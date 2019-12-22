using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.FileSystem;

namespace relert_sharp.MapStructure.Logic
{
    public class TaskforceCollection : TeamLogicCollection
    {
        public TaskforceCollection() : base() { }
    }

    public class TaskforceItem : TeamLogicItem
    {
        private Dictionary<string, int> memberData = new Dictionary<string, int>();
        public TaskforceItem(INIEntity ent) : base(ent)
        {
            Name = ent.PopPair("Name").Value;
            Group = int.Parse(ent.PopPair("Group").Value);
            foreach (INIPair p in ent.DataList)
            {
                string[] tmp = p.ParseStringList();
                memberData[tmp[1]] = int.Parse(tmp[0]);
            }
        }
        public TaskforceItem() : base() { }


        #region Public Calls - TaskforceItem
        public Dictionary<string, int> MemberData { get { return memberData; } set { memberData = value; } }
        public int Group { get; set; }
        public string Name { get; set; }
        #endregion
    }
}
