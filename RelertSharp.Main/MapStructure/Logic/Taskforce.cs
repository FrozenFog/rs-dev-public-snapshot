using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            Name = GlobalVar.GlobalRules.GetCsfUIName(RegName);
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
        private int memberid = 0;


        #region Ctor - TaskforceItem
        public TaskforceItem(INIEntity ent) : base(ent)
        {
            Name = ent.PopPair("Name").Value;
            Group = int.Parse(ent.PopPair("Group").Value);
            foreach (INIPair p in ent.DataList)
            {
                string[] tmp = p.ParseStringList();
                Members[p.Name] = new TaskforceUnit(p);
            }
            memberid = Members.Count;
        }
        public TaskforceItem() : base() { }
        #endregion


        #region Public Methods - TaskforceItem
        public static IEnumerable<ListViewItem> ToListViewItems(IEnumerable<TaskforceUnit> src)
        {
            List<ListViewItem> dest = new List<ListViewItem>();
            foreach (TaskforceUnit unit in src)
            {
                dest.Add(new ListViewItem(string.Format("{0}({1}):{2}", unit.UiName, unit.RegName, unit.UnitNum), unit.PcxName.ToLower()));
            }
            return dest;
        }
        public TaskforceUnit NewUnitItem(string regname, string num)
        {
            TaskforceUnit unit = new TaskforceUnit(memberid++.ToString(), regname, int.Parse(num));
            Members[unit.ID] = unit;
            return unit;
        }
        #endregion


        #region Public Calls - TaskforceItem
        public Dictionary<string, TaskforceUnit> Members { get; private set; } = new Dictionary<string, TaskforceUnit>();
        public override string ToString() { return ID + " " + Name; }
        public List<Tuple<string, int>> MemberData { get { return memberData; } set { memberData = value; } }
        public IEnumerable<string> MemberPcxNames
        {
            get
            {
                List<string> s = new List<string>();
                foreach (TaskforceUnit u in Members.Values)
                {
                    s.Add(GlobalVar.GlobalRules.GetPcxName(u.RegName));
                }
                return s;
            }
        }
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


    public class TaskforceUnit
    {
        #region Ctor - TaskforceUnit
        public TaskforceUnit(INIPair p)
        {
            ID = p.Name;
            string[] tmp = p.ParseStringList();
            RegName = tmp[1];
            UnitNum = int.Parse(tmp[0]);
        }
        public TaskforceUnit(string id, string regname, int num)
        {
            ID = id;
            RegName = regname;
            UnitNum = num;
        }
        #endregion


        #region Public Methods - TaskforceUnit
        public override string ToString()
        {
            return string.Format("{0}({1}):{2}", UiName, RegName, UnitNum);
        }
        #endregion


        #region Public Calls - TaskforceUnit
        public string ID { get; set; }
        public string RegName { get; set; }
        public int UnitNum { get; set; }
        public string UiName { get { return GlobalVar.GlobalRules.GetCsfUIName(RegName); } }
        public string PcxName { get { return GlobalVar.GlobalRules.GetPcxName(RegName); } }
        #endregion
    }
}
