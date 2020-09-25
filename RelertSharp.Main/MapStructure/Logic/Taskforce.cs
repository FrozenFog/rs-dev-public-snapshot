using RelertSharp.Common;
using RelertSharp.IniSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RelertSharp.MapStructure.Logic
{
    public class TaskforceShowItem
    {
        public override string ToString() { return Number + " " + Name; }
        public TaskforceShowItem(Tuple<string, int> pair)
        {
            Number = pair.Item2;
            RegName = pair.Item1;
            Name = GlobalVar.GlobalRules.GetCsfUIName(RegName);
        }
        public int Number { get; set; }
        public string RegName { get; set; }
        public string Name { get; set; }
    }

    public class TaskforceCollection : TeamLogicCollection<TaskforceItem>
    {
        public TaskforceCollection() : base() { }


        #region Public Methods - TaskforceCollection
        public void RemoveTaskforce(string id)
        {
            if (Keys.Contains(id)) Remove(id);
        }
        public TaskforceItem NewTaskforce(string id, string name = "New Taskforce")
        {
            TaskforceItem t = new TaskforceItem(id, name, -1);
            this[id] = t;
            return t;
        }
        #endregion
    }

    public class TaskforceItem : TeamLogicItem, ILogicItem
    {
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
                Members.Add(new TaskforceUnit(p));
            }
            memberid = Members.Count;
        }
        public TaskforceItem(string id, string name, int group)
        {
            Name = name;
            Group = group;
            ID = id;
        }
        public TaskforceItem() : base() { }
        #endregion


        #region Public Methods - TaskforceItem
        public INIEntity GetSaveData()
        {
            INIEntity result = new INIEntity(ID);
            result.AddPair(new INIPair("Name", Name));
            result.AddPair(new INIPair("Group", Group.ToString()));
            for (int i = 0; i < Members.Count; i++)
            {
                result.AddPair(new INIPair(i.ToString(), Members[i].SaveData));
            }
            return result;
        }
        public TaskforceItem Copy(string newid)
        {
            TaskforceItem cp = new TaskforceItem();
            cp.ID = newid;
            cp.Name = Name + " Copy";
            cp.Members = Members;
            cp.Group = group;
            return cp;
        }
        public static IEnumerable<ListViewItem> ToListViewItems(IEnumerable<TaskforceUnit> src)
        {
            List<ListViewItem> dest = new List<ListViewItem>();
            foreach (TaskforceUnit unit in src)
            {
                dest.Add(unit.ToListviewItem());
            }
            return dest;
        }
        public TaskforceUnit NewUnitItem(string regname, int num)
        {
            TaskforceUnit unit = new TaskforceUnit(memberid++.ToString(), regname, num);
            Members.Add(unit);
            return unit;
        }
        #endregion


        #region Public Calls - TaskforceItem
        public bool IsEmpty { get { return Members.Count == 0; } }
        public List<TaskforceUnit> Members { get; private set; } = new List<TaskforceUnit>();
        public override string ToString() { return ID + " " + Name; }
        public IEnumerable<string> MemberPcxNames
        {
            get
            {
                List<string> s = new List<string>();
                foreach (TaskforceUnit u in Members)
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
            if (tmp.Length != 2)
            {
                //logger
                return;
            }
            RegName = tmp[1];
            UnitNum = int.Parse(tmp[0]);
        }
        public TaskforceUnit(string id, string regname, int num)
        {
            ID = id;
            RegName = regname;
            UnitNum = num;
        }
        public TaskforceUnit(string regname)
        {
            RegName = regname;
            UnitNum = 1;
        }
        #endregion


        #region Public Methods - TaskforceUnit
        public ListViewItem ToListviewItem()
        {
            ListViewItem item = new ListViewItem(string.Format("{0}\n({1}):\n{2}", UiName, RegName, UnitNum), PcxName.ToLower());
            item.Name = RegName;
            return item;
        }
        public static TaskforceUnit FromListviewItem(ListViewItem src)
        {
            TaskforceUnit dest = new TaskforceUnit(src.Name);
            string count = src.Text.Split(':').Last();
            try
            {
                dest.UnitNum = int.Parse(count);
            }
            catch
            {
                dest.UnitNum = 1;
            }
            return dest;
        }
        public override string ToString()
        {
            return string.Format("{0}({1}):{2}", UiName, RegName, UnitNum);
        }
        #endregion


        #region Public Calls - TaskforceUnit
        public string SaveData { get { return string.Format("{0},{1}", UnitNum, RegName); } }
        /// <summary>
        /// 0, 1, 2... etc
        /// </summary>
        public string ID { get; set; }
        public string RegName { get; set; }
        public int UnitNum { get; set; }
        public string UiName { get { return GlobalVar.GlobalRules.GetCsfUIName(RegName); } }
        public string PcxName { get { return GlobalVar.GlobalRules.GetPcxName(RegName); } }
        #endregion
    }
}
