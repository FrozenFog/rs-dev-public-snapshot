using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.IniSystem;
using RelertSharp.Common;

namespace RelertSharp.MapStructure.Logic
{
    public class TaskforceShowItem
    {
        public override string ToString() { return Number + " " + Name; }
        public TaskforceShowItem(Tuple<string,int> pair)
        {
            Number = pair.Item2;
            RegName= pair.Item1;
            Name = Utils.Misc.FindUIName(RegName);
        }
        public int Number { get; set; }
        public string RegName { get; set; }
        public string Name { get; set; }
    }

    public class TaskforceCollection : TeamLogicCollection<TaskforceItem>
    {
        public TaskforceCollection() : base() { }
    }

    public class TaskforceItem : TeamLogicItem, IRegistable
    {
        //private Dictionary<string, int> memberData = new Dictionary<string, int>();
        private List<Tuple<string, int>> memberData = new List<Tuple<string, int>>();
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
                memberData.Add(new Tuple<string, int>(tmp[1], int.Parse(tmp[0])));
            }
        }
        public TaskforceItem() : base() { }
        #endregion


        #region Public Calls - TaskforceItem
        public override string ToString() { return Name; }
        public  List<Tuple<string, int>> MemberData { get { return memberData; } set { memberData = value; } }
        //public Dictionary<string, int> MemberData { get { return memberData; } set { memberData = value; } }
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
