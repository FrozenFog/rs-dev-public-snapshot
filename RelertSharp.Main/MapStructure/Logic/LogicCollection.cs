using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using RelertSharp.Model;
using System.Collections;

namespace RelertSharp.MapStructure.Logic
{
    //public class LogicCollection
    //{
    //    private Dictionary<string, LogicGroup> data = new Dictionary<string, LogicGroup>();


    //    #region Ctor - LogicCollection
    //    public LogicCollection(INIEntity ent, LogicType type)
    //    {
    //        foreach (INIPair p in ent.DataList)
    //        {
    //            if (!data.Keys.Contains(p.Name))
    //            {
    //                string[] l = p.ParseStringList();
    //                data[p.Name] = new LogicGroup(p.Name, int.Parse(l[0]), l.Skip(1).ToArray(), type);
    //            }
    //        }
    //    }
    //    #endregion


    //    #region Public Calls - LogicCollection
    //    public LogicGroup this[string _id]
    //    {
    //        get
    //        {
    //            if (data.Keys.Contains(_id)) return data[_id];
    //            return null;
    //        }
    //    }
    //    #endregion
    //}


    public class LogicGroup : IEnumerable<LogicItem>
    {
        private Dictionary<int, LogicItem> data = new Dictionary<int, LogicItem>();


        #region Ctor - LogicGroup
        public LogicGroup(LogicGroup src)
        {
            ID = src.ID;
            data = new Dictionary<int, LogicItem>(src.data);
            ParentID = src.ParentID;
        }
        public LogicGroup(INIPair p, LogicType type)
        {
            int num = 1;
            LogicType = type;
            string[] l = p.ParseStringList();
            if (l.Contains(""))
            {
                int end = l.ToList().IndexOf("");
                l = l.Take(end).ToArray();
            }
            ID = p.Name;
            Num = int.Parse(l[0]);
            int steplen = 0, llen = l.Length;
            for(int i = 1; i < llen; i += steplen)
            {
                int logicID = int.Parse(l[i]);
                switch (type)
                {
                    case LogicType.ActionLogic:
                        steplen = 8;
                        Add(new LogicItem(
                            logicID,
                            new string[] { l[i + 1], l[i + 2], l[i + 3], l[i + 4], l[i + 5], l[i + 6], l[i + 7] },
                            type,
                            num++
                        ));
                        break;
                    case LogicType.EventLogic:
                        steplen = (logicID == 60 || logicID == 61) ? 4 : 3;
                        Add(new LogicItem(
                            logicID,
                            new string[] { l[i + 1], l[i + 2], steplen == 4 ? l[i + 3] : "0" },
                            type,
                            num++
                            ));
                        break;
                }
            }

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
        public int GetCount()
        {
            for (int i = 1; i < 50; i++)
            {
                if (!data.Keys.Contains(i)) return i;
            }
            return 50;
        }
        public LogicItem NewEvent()
        {
            LogicItem item = new LogicItem(LogicType.EventLogic, GetCount());
            Add(item);
            return item;
        }
        public LogicItem NewAction()
        {
            LogicItem item = new LogicItem(LogicType.ActionLogic, GetCount());
            Add(item);
            return item;
        }
        public void Multiply(string[] command, LogicItem template, List<TriggerParam> lookups)
        {
            foreach(string s in command)
            {
                if (string.IsNullOrEmpty(s)) continue;
                string[] sl = s.Split(new char[] { ',' });
                if (sl.Length != lookups.Count) return;
                LogicItem item = new LogicItem(template, GetCount());
                for (int i = 0; i < sl.Length; i++)
                {
                    item.Parameters[lookups[i].ParamPos] = sl[i];
                }
                Add(item);
            }
        }
        public void Add(LogicItem item)
        {
            data[item.idx] = item;
        }
        public void Remove(LogicItem item)
        {
            if (item == null) return;
            data.Remove(item.idx);
        }
        #region Enumerator
        public IEnumerator<LogicItem> GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }
        #endregion
        #endregion


        #region Internal - LogicGroup
        internal int Num { get; set; }
        internal string[] Params { get; set; }
        internal LogicType LogicType { get; set; }
        #endregion


        #region Public Calls - LogicGroup
        public string ID { get; set; }
        public LogicItem this[int index] { get { return data[index]; } set { data[index] = value; } }
        public string ParentID { get; set; }
        #endregion
    }


    public class LogicItem : BindableBase
    {
        private LogicType type;
        public int idx;
        private int id;
        private string comment;
        private string[] parameters;


        #region Ctor - LogicItem
        public LogicItem(int _typeID, string[] _param, LogicType _type, int num, string _comment = "")
        {
            ID = _typeID;
            type = _type;
            Parameters = _param;
            Comment = _comment;
            idx = num;
        }
        public LogicItem(LogicType _type, int num)
        {
            ID = 0;
            type = _type;
            idx = num;
            if (_type == LogicType.EventLogic) Parameters = new string[] { "0", "0", "0" };
            else Parameters = new string[] { "0", "0", "0", "0", "0", "0", "A" };
        }
        public LogicItem(LogicItem src, int num)
        {
            id = src.id;
            comment = src.comment;
            type = src.type;
            idx = num;
            if (src.type == LogicType.EventLogic) Parameters = new string[] { "0", "0", "0" };
            else Parameters = new string[] { "0", "0", "0", "0", "0", "0", "A" };
            Array.Copy(src.parameters, parameters, parameters.Length);
        }
        #endregion


        #region Public Methods - LogicItem
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Comment)) return Comment;
            else
            {
                if (type == LogicType.ActionLogic) return string.Format("Action{0:D2}-ID:{1:D2}", idx, ID);
                else return string.Format("Event{0:D2}-ID:{1:D2}", idx, ID);
            }

        }
        #endregion


        #region Public Calls - LogicItem
        public int ID
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }
        public string Comment
        {
            get { return comment; }
            set { SetProperty(ref comment, value); }
        }
        public string[] Parameters
        {
            get { return parameters; }
            set { SetProperty(ref parameters, value); }
        }
        #endregion
    }
}
