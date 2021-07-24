using RelertSharp.Common;
using RelertSharp.Common.Config.Model;
using RelertSharp.IniSystem;
using System;
using System.Collections.Generic;

namespace RelertSharp.MapStructure.Logic
{
    public class TeamScriptCollection : TeamLogicCollection<TeamScriptGroup>, ICurdContainer<TeamScriptGroup>
    {
        public TeamScriptCollection() : base()
        {

        }


        #region Public Methods - TeamScriptCollection


        #endregion

        #region Curd
        public TeamScriptGroup AddItem(string id, string name)
        {
            TeamScriptGroup s = new TeamScriptGroup(id, name);
            this[id] = s;
            return s;
        }

        public TeamScriptGroup CopyItem(TeamScriptGroup src, string id)
        {
            TeamScriptGroup s = new TeamScriptGroup(src, id);
            this[id] = s;
            return s;
        }

        public bool ContainsItem(TeamScriptGroup look)
        {
            return data.ContainsKey(look.Id);
        }

        public bool ContainsItem(string id, string obsolete = null)
        {
            return data.ContainsKey(id);
        }

        public bool RemoveItem(TeamScriptGroup target)
        {
            if (ContainsItem(target))
            {
                return data.Remove(target.Id);
            }
            return false;
        }
        #endregion
    }

    public class TeamScriptGroup : TeamLogicItem, IIndexableItem, ISubCurdContainer<TeamScriptItem>, ILogicItem
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
            Name = src.Name + Constant.CLONE_SUFFIX;
            Id = id;
            data = new List<TeamScriptItem>(src.data);
        }
        public TeamScriptGroup(string id, string name)
        {
            Id = id;
            Name = name;
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
        public TeamScriptItem AddItemAt(int pos)
        {
            TeamScriptItem item = new TeamScriptItem();
            data.Insert(pos, item);
            return item;
        }
        public void RemoveItemAt(int pos)
        {
            data.RemoveAt(pos);
        }
        public void RemoveAll()
        {
            data.Clear();
        }
        public void MoveItemTo(int from, int to)
        {
            TeamScriptItem item = data[from];
            data.RemoveAt(from);
            data.Insert(to, item);
        }
        public TeamScriptItem CopyItemAt(int pos)
        {
            TeamScriptItem src = data[pos];
            TeamScriptItem copy = new TeamScriptItem(src);
            data.Insert(pos, copy);
            return copy;
        }
        //public TeamScriptItem NewScript(int insertIndex)
        //{
        //    TeamScriptItem item = new TeamScriptItem(0, "0");
        //    if (insertIndex == -1) data.Add(item);
        //    else data.Insert(insertIndex + 1, item);
        //    return item;
        //}
        //public TeamScriptItem NewScript(int insertIndex, TeamScriptItem src)
        //{
        //    TeamScriptItem item = new TeamScriptItem(src);
        //    if (insertIndex == -1) data.Add(item);
        //    else data.Insert(insertIndex + 1, item);
        //    return item;
        //}
        //public void RemoveScript(int index)
        //{
        //    data.RemoveAt(index);
        //}
        #endregion


        #region Public Calls - TeamScriptGroup
        public bool IsEmpty { get { return data.Count == 0; } }
        public List<TeamScriptItem> Data { get { return data; } set { data = value; } }
        public LogicType ItemType { get { return LogicType.Script; } }
        #endregion
    }

    public class TeamScriptItem
    {
        private bool _initialized = false;
        private List<LogicInfo> cfg { get { return GlobalVar.GlobalConfig.ModConfig.TriggerInfo.ScriptActions; } }
        private LogicInfo info;
        public event EventHandler InfoUpdated;
        #region Ctor
        public TeamScriptItem(int _actionType, string _value)
        {
            ScriptActionIndex = _actionType;
            ActionValue = _value;
            _initialized = true;
        }
        public TeamScriptItem(TeamScriptItem src)
        {
            ScriptActionIndex = src.ScriptActionIndex;
            ActionValue = src.ActionValue;
            _initialized = true;
        }
        public TeamScriptItem()
        {
            ScriptActionIndex = 0;
            ActionValue = "0";
            _initialized = true;
        }
        #endregion


        #region Public
        public override string ToString()
        {
            List<string> param = new List<string>();
            foreach (LogicInfoParameter parameter in info.Parameters)
            {
                string raw = ActionValue;
                string parsed = LogicInfoParameter.GetFormatParam(raw, parameter);
                param.Add(parsed);
            }
            string result = string.Format(info.FormatString, param.ToArray());
            return string.Format("{0:D2}: {1}", ScriptActionIndex, result);
        }
        public void SetScriptTypeTo(int id)
        {
            ScriptActionIndex = id;
            OnInfoUpdated();
        }
        public void OnInfoUpdated()
        {
            InfoUpdated?.Invoke(null, null);
        }
        #region Parameter IO
        public string GetParameter(LogicInfoParameter param)
        {
            string raw = ActionValue;
            return LogicInfoParameter.GetParameter(raw, param);
        }
        public void SetParameter(LogicInfoParameter param, string value)
        {
            string write = LogicInfoParameter.WriteParameter(value, param, ActionValue);
            ActionValue = write;
            OnInfoUpdated();
        }
        public void SetParameter(LogicInfoParameter param, bool value)
        {
            string write = LogicInfoParameter.WriteParameter(value.ZeroOne(), param, ActionValue); 
            ActionValue = write;
            OnInfoUpdated();
        }
        #endregion
        #endregion


        #region Private
        private void InitializeParameter()
        {
            ActionValue = info.DefaultParameters;
        }
        #endregion


        #region Public Calls - TeamScriptItem
        public string SaveData { get { return string.Format("{0},{1}", ScriptActionIndex, ActionValue); } }
        private int _index;
        public int ScriptActionIndex
        {
            get { return _index; }
            private set
            {
                _index = value;
                info = cfg[_index];
                if (_initialized) InitializeParameter();
            }
        }
        public string ActionValue { get; private set; }
        public LogicInfo Info { get { return info; } }
        #endregion
    }
}
