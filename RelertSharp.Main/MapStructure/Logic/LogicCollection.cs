using RelertSharp.Common;
using RelertSharp.Common.Config.Model;
using RelertSharp.IniSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RelertSharp.MapStructure.Logic
{
    public class LogicGroup : IEnumerable<LogicItem>, ISubCurdContainer<LogicItem>
    {
        private List<LogicItem> data = new List<LogicItem>();
        private TriggerInfo cfg { get { return GlobalVar.GlobalConfig.ModConfig.TriggerInfo; } }


        #region Ctor - LogicGroup
        public LogicGroup(LogicGroup src)
        {
            src.data.ForEach(x => data.Add(new LogicItem(x)));
            ParentID = src.ParentID;
            LogicType = src.LogicType;
        }
        public LogicGroup(INIPair p, TriggerSubType type)
        {
            LogicType = type;
            ParentID = p.Name;
            List<LogicInfo> infoList;
            if (type == TriggerSubType.ActionLogic) infoList = cfg.TriggerActions;
            else infoList = cfg.TriggerEvents;
            string[] ls = p.ParseStringList();
            if (int.TryParse(ls[0], out int num))
            {
                int pos = 1;
                while (num-- > 0)
                {
                    int id = 0;
                    try
                    {
                        int.TryParse(ls[pos++], out int i);
                        id = i;
                    }
                    catch { }
                    LogicInfo info = infoList[id];
                    string[] param;
                    try
                    {
                        param = ls.Skip(pos).Take(info.ParamLength).ToArray();
                    }
                    catch
                    {
                        param = info.DefaultParameters.Split(',');
                    }
                    pos += info.ParamLength;
                    LogicItem item = new LogicItem(id, param, type)
                    {
                        Parent = this
                    };
                    data.Add(item);
                }
            }
        }
        public LogicGroup()
        {
            data = new List<LogicItem>();
        }
        #endregion


        #region Private Methods - LogicGroup
        #endregion


        #region Public Methods - LogicGroup
        public LogicItem AddItemAt(int pos)
        {
            LogicItem item = new LogicItem(LogicType, this);
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
            LogicItem item = data[from];
            data.RemoveAt(from);
            data.Insert(to, item);
        }
        public LogicItem CopyItemAt(int pos)
        {
            LogicItem src = data[pos];
            LogicItem copy = new LogicItem(src);
            data.Insert(pos, copy);
            return copy;
        }
        /// <summary>
        /// count \t id1 \t param1 \t param2 \t id2...
        /// </summary>
        /// <param name="cmd"></param>
        public void ApplyCommandLine(string cmd)
        {
            List<LogicItem> results = new List<LogicItem>();
            List<LogicInfo> group = LogicType == TriggerSubType.ActionLogic ? cfg.TriggerActions : cfg.TriggerEvents;
            try
            {
                ParameterReader reader = new ParameterReader(cmd, '\t');
                int count = reader.ReadInt();
                for (;count > 0; count--)
                {
                    int id = reader.ReadInt();
                    LogicInfo info = group[id];
                    LogicItem item = new LogicItem(LogicType, this);
                    item.SetIdTo(id);
                    foreach (var param in info.Parameters)
                    {
                        item.SetParameter(param, reader.ReadString());
                    }
                    results.Add(item);
                }
            }
            catch
            {
                results.Clear();
            }
            data.AddRange(results);
        }
        public string GetSaveData()
        {
            List<object> obj = new List<object>();
            obj.Add(data.Count);
            foreach (LogicItem lg in data)
            {
                obj.Add(lg.ID);
                string[] param = lg.Parameters;
                if (LogicType == TriggerSubType.EventLogic && param[0] != "2") param = param.Take(2).ToArray();
                obj.AddRange(param);
            }
            return obj.JoinBy();
        }
        #region Enumerator
        public IEnumerator<LogicItem> GetEnumerator()
        {
            return data.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }
        #endregion
        #endregion


        #region Internal - LogicGroup
        #endregion


        #region Public Calls - LogicGroup
        public TriggerSubType LogicType { get; internal set; }
        public LogicItem this[int index] { get { return data[index]; } set { data[index] = value; } }
        public string ParentID { get; set; }
        #endregion
    }


    public class LogicItem
    {
        private bool initialized = false;
        public event EventHandler InfoUpdated;
        private TriggerInfo cfg { get { return GlobalVar.GlobalConfig.ModConfig.TriggerInfo; } }
        private List<LogicInfo> group { get { if (Type == TriggerSubType.ActionLogic) return cfg.TriggerActions; return cfg.TriggerEvents; } }
        private LogicInfo info;
        //public int idx;


        #region Ctor - LogicItem
        public LogicItem(int _typeID, string[] _param, TriggerSubType _type, string _comment = "")
        {
            Type = _type;
            Parameters = _param;
            Comment = _comment;
            ID = _typeID;
            initialized = true;
        }
        public LogicItem(TriggerSubType _type, LogicGroup parent)
        {
            Type = _type;
            Parent = parent;
            //idx = num;
            //if (_type == TriggerSubType.EventLogic) Parameters = new string[] { "0", "0", "0" };
            //else Parameters = new string[] { "0", "0", "0", "0", "0", "0", "A" };
            ID = 0;
            initialized = true;
        }
        public LogicItem(LogicItem src)
        {
            Comment = src.Comment;
            Type = src.Type;
            Parent = src.Parent;
            //idx = num;
            Parameters = new string[src.Parameters.Length];
            Array.Copy(src.Parameters, Parameters, Parameters.Length);
            ID = src.ID;
            initialized = true;
        }
        #endregion


        #region Public Methods - LogicItem
        public void SetIdTo(int id)
        {
            ID = id;
            OnInfoRefreshInvoked();
        }
        public override string ToString()
        {
            List<string> param = new List<string>();
            foreach (LogicInfoParameter parameter in info.Parameters)
            {
                string raw = Parameters[parameter.ParamPos];
                string parsed = LogicInfoParameter.GetFormatParam(raw, parameter);
                param.Add(parsed);
            }
            //if (!string.IsNullOrEmpty(Comment)) return Comment;
            //else
            //{
            //    if (type == TriggerSubType.ActionLogic) return string.Format("Action{0:D2}-ID:{1:D2}", idx, ID);
            //    else return string.Format("Event{0:D2}-ID:{1:D2}", idx, ID);
            //}
            string result = string.Format(info.FormatString, param.ToArray());
            if (result.Contains(Constant.FMT_OWNER) && !Parent.ParentID.IsNullOrEmpty())
            {
                string owner = GlobalVar.GlobalMap.Triggers[Parent.ParentID].Owner;
                result = result.Replace(Constant.FMT_OWNER, owner.CoverWith("[", "]"));
            }
            return string.Format("{0:D3}: {1}", ID, result);
        }
        public virtual void OnInfoRefreshInvoked()
        {
            InfoUpdated?.Invoke(null, null);
        }
        #region Parameter IO
        public string GetParameter(LogicInfoParameter param)
        {
            string raw = Parameters[param.ParamPos];
            return LogicInfoParameter.GetParameter(raw, param);
        }
        public void SetParameter(LogicInfoParameter paramInfo, string value)
        {
            string write = LogicInfoParameter.WriteParameter(value, paramInfo);
            Parameters[paramInfo.ParamPos] = write;
            OnInfoRefreshInvoked();
        }
        public void SetParameter(LogicInfoParameter paramInfo, bool value)
        {
            string write = value.ZeroOne();
            Parameters[paramInfo.ParamPos] = write;
            OnInfoRefreshInvoked();
        }
        #endregion
        #endregion


        #region Private
        private void InitializeParameter()
        {
            Parameters = info.DefaultParameters.Split(',');
        }
        #endregion


        #region Public Calls - LogicItem
        public TriggerSubType Type { get; internal set; }
        public LogicInfo Info { get { return info; } }
        public LogicGroup Parent { get; internal set; }
        private int id;
        public int ID
        {
            get { return id; }
            private set
            {
                id = value;
                info = group[id];
                if (initialized) InitializeParameter();
            }
        }
        public string Comment { get; set; }
        public string[] Parameters { get; private set; }
        #endregion
    }
}
