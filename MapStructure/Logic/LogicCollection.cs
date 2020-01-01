using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.FileSystem;
using relert_sharp.Common;

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
    }
    public class LogicGroup
    {
        private List<LogicItem> data = new List<LogicItem>();


        #region Constructor - LogicGroup
        public LogicGroup(string _id, int _num, string[] _paramData, LogicType type)
        {
            ID = _id;
            int window = 0;
            for (int i = 0; i < _paramData.Length; i += window)
            {
                int logicID = int.Parse(_paramData[i]);
                if (type == LogicType.ActionLogic) window = 8;
                else if (type == LogicType.EventLogic)
                {
                    if (logicID == 60 || logicID == 61) window = 4;
                    else window = 3;
                }
                Add(new LogicItem(logicID, _paramData.Skip(i + 1).Take(window - 1).ToArray(), type));
            }
        }
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
        #endregion


        #region Public Calls - LogicGroup
        public string ID { get; set; }
        #endregion
    }


    public class LogicItem
    {
        private LogicType type;
        private string[] parameters;


        #region Constructor - LogicItem
        public LogicItem(int _typeID, string[] _param, LogicType _type, string _comment = "")
        {
            ID = _typeID;
            type = _type;
            parameters = _param;
            Comment = _comment;
        }
        #endregion


        #region Public Calls - LogicItem
        public int ID { get; set; }
        public string Comment { get; set; }
        #endregion
    }
}
