using RelertSharp.Common;
using RelertSharp.Common.Config.Model;
using RelertSharp.IniSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RelertSharp.MapStructure.Logic
{
    public class LogicGroup : IEnumerable<LogicItem>
    {
        private List<LogicItem> data = new List<LogicItem>();
        private TriggerInfo cfg { get { return GlobalVar.GlobalConfig.ModConfig.TriggerInfo; } }


        #region Ctor - LogicGroup
        public LogicGroup(LogicGroup src)
        {
            ID = src.ID;
            data = new List<LogicItem>(src.data);
            ParentID = src.ParentID;
        }
        public LogicGroup(INIPair p, TriggerSubType type)
        {
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
            //try
            //{
            //    int num = 1;
            //    LogicType = type;
            //    string[] l = p.ParseStringList();
            //    if (l.Contains(""))
            //    {
            //        int end = l.ToList().IndexOf("");
            //        l = l.Take(end).ToArray();
            //    }
            //    ID = p.Name;
            //    Num = int.Parse(l[0]);
            //    int steplen = 0, llen = l.Length;
            //    for (int i = 1; i < llen; i += steplen)
            //    {
            //        int logicID = int.Parse(l[i]);
            //        switch (type)
            //        {
            //            case TriggerSubType.ActionLogic:
            //                steplen = 8;
            //                Add(new LogicItem(
            //                    logicID,
            //                    new string[] { l[i + 1], l[i + 2], l[i + 3], l[i + 4], l[i + 5], l[i + 6], l[i + 7] },
            //                    type,
            //                    num++
            //                ));
            //                break;
            //            case TriggerSubType.EventLogic:
            //                string flag = l[i + 1];
            //                steplen = flag == "2" ? 4 : 3;
            //                Add(new LogicItem(
            //                    logicID,
            //                    new string[] { l[i + 1], l[i + 2], steplen == 4 ? l[i + 3] : "0" },
            //                    type,
            //                    num++
            //                    ));
            //                break;
            //        }
            //    }
            //}
            //catch
            //{
            //    string sType = type == TriggerSubType.EventLogic ? "Event" : "Action";
            //    GlobalVar.Log.Critical(string.Format("{1} item id: {0} has unreadable data, please verify in map file!", p.Name, sType));
            //}
            /*
            IEnumerable<string> paramEnm = l.Skip(1);
            int end = paramEnm.ToList().IndexOf("");
            Params = paramEnm.Take(end).ToArray();
            LogicType = type;
            int window = 0;
            for (int i = 0; i < Params.Length; i += window)
            {
                int logicID = int.Parse(Params[i]);
                if (type == LogicType.ActionLogic) window = 8;
                else if (type == LogicType.EventLogic)
                {
                    if (logicID == 60 || logicID == 61)
                        window = 4;
                    else window = 3;
                }
                List<string> parameters = Params.Skip(i + 1).Take(window - 1).ToList();
                if (type == LogicType.EventLogic && parameters.Count != 3) parameters.Add("0");
                Add(new LogicItem(logicID, parameters.ToArray(), type, count++));
            }
            */
        }
        public LogicGroup() { }
        #endregion


        #region Private Methods - LogicGroup
        #endregion


        #region Public Methods - LogicGroup
        //public LogicItem NewEvent()
        //{
        //    LogicItem item = new LogicItem(TriggerSubType.EventLogic, GetCount());
        //    Add(item);
        //    return item;
        //}
        //public LogicItem NewAction()
        //{
        //    LogicItem item = new LogicItem(TriggerSubType.ActionLogic, GetCount());
        //    Add(item);
        //    return item;
        //}
        //public void Multiply(string[] command, LogicItem template, List<TriggerParam> lookups)
        //{
        //    foreach (string s in command)
        //    {
        //        if (string.IsNullOrEmpty(s)) continue;
        //        string[] sl = s.Split(new char[] { ',' });
        //        if (sl.Length != lookups.Count) return;
        //        LogicItem item = new LogicItem(template, GetCount());
        //        for (int i = 0; i < sl.Length; i++)
        //        {
        //            item.Parameters[lookups[i].ParamPos] = sl[i];
        //        }
        //        Add(item);
        //    }
        //}
        //public void Add(LogicItem item)
        //{
        //    data[item.idx] = item;
        //}
        //public void Remove(LogicItem item)
        //{
        //    if (item == null) return;
        //    data.Remove(item.idx);
        //}
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
        public string ID { get; set; }
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
        public LogicItem(TriggerSubType _type)
        {
            Type = _type;
            //idx = num;
            //if (_type == TriggerSubType.EventLogic) Parameters = new string[] { "0", "0", "0" };
            //else Parameters = new string[] { "0", "0", "0", "0", "0", "0", "A" };
            ID = 0;
            initialized = true;
        }
        public LogicItem(LogicItem src, int num)
        {
            Comment = src.Comment;
            Type = src.Type;
            //idx = num;
            if (src.Type == TriggerSubType.EventLogic) Parameters = new string[] { "0", "0", "0" };
            else Parameters = new string[] { "0", "0", "0", "0", "0", "0", "A" };
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
            if (result.Contains(Constant.FMT_OWNER))
            {
                string owner = GlobalVar.CurrentMapDocument.Map.Triggers[Parent.ParentID].OwnerCountry;
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
