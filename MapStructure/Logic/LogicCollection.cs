using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.IniSystem;
using relert_sharp.Common;
using System.Collections;

namespace relert_sharp.MapStructure.Logic
{
    public class LogicCollection
    {
        private Dictionary<string, LogicGroup> data = new Dictionary<string, LogicGroup>();


        #region Constructor - LogicCollection
        public LogicCollection(INIEntity ent, LogicType type)
        {
            foreach (INIPair p in ent.DataList)
            {
                if (!data.Keys.Contains(p.Name))
                {
                    string[] l = p.ParseStringList();
                    data[p.Name] = new LogicGroup(p.Name, int.Parse(l[0]), l.Skip(1).ToArray(), type);
                }
            }
        }
        #endregion


        #region Public Calls - LogicCollection
        public LogicGroup this[string _id]
        {
            get
            {
                if (data.Keys.Contains(_id)) return data[_id];
                return null;
            }
        }
        #endregion
    }


    public class LogicGroup : IEnumerable<LogicItem>
    {
        private List<LogicItem> data = new List<LogicItem>();


        #region Constructor - LogicGroup
        public LogicGroup(string _id, int _num, string[] _paramData, LogicType type)
        {
            ID = _id;
            Num = _num;
            Params = _paramData;
            LogicType = type;
            int window = 0;
            int count = 1;
            for (int i = 0; i < _paramData.Length; i += window)
            {
                int logicID = int.Parse(_paramData[i]);
                if (type == LogicType.ActionLogic) window = 8;
                else if (type == LogicType.EventLogic)
                {
                    if (logicID == 60 || logicID == 61) window = 4;
                    else window = 3;
                }
                Add(new LogicItem(logicID, _paramData.Skip(i + 1).Take(window - 1).ToArray(), type, count++));
            }
        }
        public LogicGroup() { }
        #endregion


        #region Public Methods - LogicGroup
        public void Add(LogicItem item)
        {
            data.Add(item);
        }
        public void Remove(int index)
        {
            data.RemoveAt(index);
        }
        public void Remove(LogicItem item)
        {
            data.Remove(item);
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
        internal int Num { get; set; }
        internal string[] Params { get; set; }
        internal LogicType LogicType { get; set; }
        #endregion


        #region Public Calls - LogicGroup
        public string ID { get; set; }
        #endregion
    }


    public class LogicItem
    {
        private LogicType type;
        private int count;


        #region Constructor - LogicItem
        public LogicItem(int _typeID, string[] _param, LogicType _type, int num, string _comment = "")
        {
            ID = _typeID;
            type = _type;
            Parameters = _param;
            Comment = _comment;
            count = num;
        }
        #endregion


        #region Public Methods - LogicItem
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Comment)) return Comment;
            else
            {
                if (type == LogicType.ActionLogic) return string.Format("Action{0:D2}-ID:{1:D2}", count, ID);
                else return string.Format("Event{0:D2}-ID:{1:D2}", count, ID);
            }

        }
        #endregion


        #region Public Calls - LogicItem
        public int ID { get; set; }
        public string Comment { get; set; }
        public string[] Parameters { get; set; }
        #endregion
    }
}
