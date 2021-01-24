using RelertSharp.Common;
using RelertSharp.IniSystem;
using System.Collections.Generic;

namespace RelertSharp.MapStructure.Logic
{
    public class TeamScriptCollection : TeamLogicCollection<TeamScriptGroup>
    {
        public TeamScriptCollection() : base()
        {
            this[Constant.ITEM_NONE] = new TeamScriptGroup()
            {
                Id = Constant.ITEM_NONE,
                Name = Constant.ITEM_NONE
            };
        }


        #region Public Methods - TeamScriptCollection
        public List<TechnoPair> ToTechnoFrom0()
        {
            List<TechnoPair> result = new List<TechnoPair>();
            int i = 0;
            foreach (TeamScriptGroup g in this)
            {
                TechnoPair p = new TechnoPair(i.ToString(), string.Format("{0} - {1}", g.Id, g.Name));
                result.Add(p);
                i++;
            }
            return result;
        }
        public TeamScriptGroup NewScript(string id, string name = "New Script")
        {
            TeamScriptGroup g = new TeamScriptGroup()
            {
                Id = id,
                Name = name
            };
            this[id] = g;
            return g;
        }
        public TeamScriptGroup NewScript(TeamScriptGroup src, string id)
        {
            TeamScriptGroup g = new TeamScriptGroup(src, id);
            this[id] = g;
            return g;
        }
        #endregion
    }

    public class TeamScriptGroup : TeamLogicItem, IIndexableItem
    {
        private List<TeamScriptItem> data = new List<TeamScriptItem>();


        #region Ctor
        public TeamScriptGroup(INIEntity ent) : base(ent)
        {
            Name = ent.PopPair("Name").Value;
            foreach (INIPair p in ent.DataList)
            {
                string[] tmp = p.ParseStringList();
                TeamScriptItem item;
                if (tmp.Length != 2) item = new TeamScriptItem(0, "0");
                else item = new TeamScriptItem(int.Parse(tmp[0]), tmp[1]);
                data.Add(item);
            }
        }
        public TeamScriptGroup(TeamScriptGroup src, string id)
        {
            Name = src.Name + " - Clone";
            Id = id;
            data = new List<TeamScriptItem>(src.data);
        }
        public TeamScriptGroup() : base() { }
        #endregion


        #region Public Methods - TeamScriptGroup
        public INIEntity GetSaveData()
        {
            INIEntity result = new INIEntity(Id);
            result.AddPair(new INIPair("Name", Name));
            for (int i = 0; i < data.Count; i++)
            {
                result.AddPair(new INIPair(i.ToString(), data[i].SaveData));
            }
            return result;
        }
        public TeamScriptItem NewScript(int insertIndex)
        {
            TeamScriptItem item = new TeamScriptItem(0, "0");
            if (insertIndex == -1) data.Add(item);
            else data.Insert(insertIndex + 1, item);
            return item;
        }
        public TeamScriptItem NewScript(int insertIndex, TeamScriptItem src)
        {
            TeamScriptItem item = new TeamScriptItem(src);
            if (insertIndex == -1) data.Add(item);
            else data.Insert(insertIndex + 1, item);
            return item;
        }
        public void RemoveScript(int index)
        {
            data.RemoveAt(index);
        }
        #endregion


        #region Public Calls - TeamScriptGroup
        public bool IsEmpty { get { return data.Count == 0; } }
        public List<TeamScriptItem> Data { get { return data; } set { data = value; } }
        #endregion
    }

    public class TeamScriptItem
    {
        #region Ctor
        public TeamScriptItem(int _actionType, string _value)
        {
            ScriptActionIndex = _actionType;
            ActionValue = _value;
        }
        public TeamScriptItem(TeamScriptItem src)
        {
            ScriptActionIndex = src.ScriptActionIndex;
            ActionValue = src.ActionValue;
        }
        #endregion

        #region Public Calls - TeamScriptItem
        public string SaveData { get { return string.Format("{0},{1}", ScriptActionIndex, ActionValue); } }
        public override string ToString() { return string.Format("ScriptType-{0:D2}", ScriptActionIndex); }
        public int ScriptActionIndex { get; set; }
        public string ActionValue { get; set; }
        #endregion
    }
}
