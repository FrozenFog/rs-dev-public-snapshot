using RelertSharp.Common;
using RelertSharp.IniSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RelertSharp.MapStructure.Logic
{
    public class TaskforceCollection : TeamLogicCollection<TaskforceItem>, ICurdContainer<TaskforceItem>
    {
        public TaskforceCollection() : base()
        {

        }


        #region Public Methods - TaskforceCollection

        #endregion


        #region Curd
        public TaskforceItem AddItem(string id, string name)
        {
            TaskforceItem t = new TaskforceItem(id, name);
            this[id] = t;
            return t;
        }

        public bool ContainsItem(TaskforceItem look)
        {
            return data.ContainsKey(look.Id);
        }

        public bool ContainsItem(string id, string obsolete = null)
        {
            return data.ContainsKey(id);
        }

        public TaskforceItem CopyItem(TaskforceItem src, string id)
        {
            TaskforceItem t = new TaskforceItem(src, id);
            this[id] = t;
            return t;
        }

        public bool RemoveItem(TaskforceItem target)
        {
            if (data.ContainsKey(target.Id))
            {
                return data.Remove(target.Id);
            }
            return false;
        }
        #endregion
    }

    public class TaskforceItem : TeamLogicItem, IIndexableItem, ISubCurdContainer<TaskforceUnit>, IGroupable, ILogicItem
    {
        #region Ctor - TaskforceItem
        public TaskforceItem(INIEntity ent) : base(ent)
        {
            Name = ent.PopPair("Name").Value;
            Group = ent.PopPair("Group").Value;
            foreach (INIPair p in ent.DataList)
            {
                Members.Add(new TaskforceUnit(p));
            }
        }
        public TaskforceItem(string id, string name)
        {
            Id = id;
            Name = name;
        }
        public TaskforceItem(TaskforceItem src, string id)
        {
            Name = src.Name + Constant.CLONE_SUFFIX;
            Id = id;
            foreach (TaskforceUnit u in src.Members)
            {
                Members.Add(new TaskforceUnit(u));
            }
        }
        public TaskforceItem() : base() { }
        #endregion


        #region Public Methods - TaskforceItem
        public INIEntity GetSaveData()
        {
            INIEntity result = new INIEntity(Id);
            result.AddPair(new INIPair("Name", Name));
            result.AddPair(new INIPair("Group", Group));
            for (int i = 0; i < Members.Count; i++)
            {
                result.AddPair(new INIPair(i.ToString(), Members[i].SaveData));
            }
            return result;
        }
        public int GetChecksum()
        {
            return GetSaveData().GetChecksum();
        }
        public string[] ExtractParameter()
        {
            List<string> r = new List<string>()
            {
                Id,
                Name,
                Group,
                Members.Count.ToString()
            };
            foreach (TaskforceUnit unit in Members)
            {
                string[] sub = new string[]
                {
                    unit.RegName,
                    unit.Count.ToString()
                };
                r.AddRange(sub);
            }
            return r.ToArray();
        }
        #region Curd
        public TaskforceUnit AddItemAt(int pos)
        {
            TaskforceUnit u = new TaskforceUnit(Constant.ITEM_NONE, 1);
            Members.Insert(pos, u);
            return u;
        }
        public void RemoveItemAt(int pos)
        {
            Members.RemoveAt(pos);
        }
        public void RemoveAll()
        {
            Members.Clear();
        }
        public void MoveItemTo(int from, int to)
        {
            TaskforceUnit unit = Members[from];
            Members.RemoveAt(from);
            Members.Insert(to, unit);
        }
        public TaskforceUnit CopyItemAt(int pos)
        {
            TaskforceUnit src = Members[pos];
            TaskforceUnit copy = new TaskforceUnit(src);
            Members.Insert(pos, copy);
            return copy;
        }
        #endregion
        #endregion


        #region Public Calls - TaskforceItem
        public bool IsEmpty { get { return Members.Count == 0; } }
        public List<TaskforceUnit> Members { get; private set; } = new List<TaskforceUnit>();
        //public IEnumerable<string> MemberPcxNames
        //{
        //    get
        //    {
        //        List<string> s = new List<string>();
        //        foreach (TaskforceUnit u in Members)
        //        {
        //            s.Add(GlobalVar.GlobalRules.GetPcxName(u.RegName));
        //        }
        //        return s;
        //    }
        //}
        //public Dictionary<string, int> MemberData { get { return memberData; } set { memberData = value; } }
        public string Group { get; set; } = Constant.ID_INVALID;
        public LogicType ItemType { get { return LogicType.Taskforce; } }
        #endregion
    }


    public class TaskforceUnit
    {
        public event EventHandler InfoUpdated;
        private bool initialized;
        #region Ctor - TaskforceUnit
        public TaskforceUnit(INIPair p)
        {
            string[] tmp = p.ParseStringList();
            if (tmp.Length != 2)
            {
                //logger
                return;
            }
            RegName = tmp[1];
            int.TryParse(tmp[0], out int num);
            Count = num;
            initialized = true;
        }
        public TaskforceUnit(string regname, int num)
        {
            RegName = regname;
            Count = num;
            initialized = true;
        }
        public TaskforceUnit(string regname)
        {
            RegName = regname;
            Count = 1;
            initialized = true;
        }
        public TaskforceUnit(TaskforceUnit src)
        {
            RegName = src.RegName;
            Count = src.Count;
            initialized = true;
        }
        #endregion


        #region Public Methods - TaskforceUnit
        //public ListViewItem ToListviewItem()
        //{
        //    ListViewItem item = new ListViewItem(string.Format("{0}\n({1}):\n{2}", UiName, RegName, UnitNum), PcxName.ToLower());
        //    item.Name = RegName;
        //    return item;
        //}
        //public static TaskforceUnit FromListviewItem(ListViewItem src)
        //{
        //    TaskforceUnit dest = new TaskforceUnit(src.Name);
        //    string count = src.Text.Split(':').Last();
        //    try
        //    {
        //        dest.UnitNum = int.Parse(count);
        //    }
        //    catch
        //    {
        //        dest.UnitNum = 1;
        //    }
        //    return dest;
        //}
        public override string ToString()
        {
            return string.Format("{0}({1}):{2}", UiName, RegName, Count);
        }
        #endregion


        #region Public Calls - TaskforceUnit
        public string SaveData { get { return string.Format("{0},{1}", Count, RegName); } }
        private string name = Constant.ITEM_NONE;
        public string RegName
        {
            get { return name; }
            set
            {
                name = value;
                if (initialized) InfoUpdated?.Invoke(null, null);
            }
        }
        private int num;
        public int Count
        {
            get { return num; }
            set
            {
                num = value;
                if (initialized) InfoUpdated?.Invoke(null, null);
            }
        }
        public string UiName { get { return GlobalVar.GlobalRules.GetCsfUIName(RegName); } }
        public string PcxName { get { return GlobalVar.GlobalRules.GetPcxName(RegName); } }
        #endregion
    }
}
