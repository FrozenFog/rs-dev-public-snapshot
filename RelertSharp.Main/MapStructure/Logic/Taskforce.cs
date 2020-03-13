using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.IniSystem;

namespace RelertSharp.MapStructure.Logic
{
    public class TaskforceCollection : TeamLogicCollection<TaskforceItem>
    {
        public TaskforceCollection() : base() { }
    }

    public class TaskforceItem : TeamLogicItem
    {
        private Dictionary<string, int> memberData = new Dictionary<string, int>();
        private int group;
        private string name;


        #region Ctor - TaskforceItem
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
        #endregion


        #region Public Calls - TaskforceItem
        public Dictionary<string, int> MemberData { get { return memberData; } set { memberData = value; } }
        public int Group
        {
            get { return group; }
            set { SetProperty(ref group, value); }
        }
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        #endregion
    }
}
