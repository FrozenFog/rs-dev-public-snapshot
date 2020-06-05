using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.IniSystem;
using RelertSharp.Common;

namespace RelertSharp.MapStructure.Logic
{
    public class TeamScriptCollection : TeamLogicCollection<TeamScriptGroup>
    {
        public TeamScriptCollection() : base() { }

        public List<TechnoPair> ToTechnoFrom0()
        {
            List<TechnoPair> result = new List<TechnoPair>();
            int i = 0;
            foreach(TeamScriptGroup g in this)
            {
                TechnoPair p = new TechnoPair(i.ToString(), string.Format("{0} - {1}", g.ID, g.Name));
                result.Add(p);
                i++;
            }
            return result;
        }
    }

    public class TeamScriptGroup : TeamLogicItem, IRegistable
    {
        private List<TeamScriptItem> data = new List<TeamScriptItem>();


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
        public TeamScriptGroup() : base() { }


        #region Public Calls - TeamScriptGroup
        public List<TeamScriptItem> Data { get { return data; } set { data = value; } }
        public override string ToString() { return ID + " " + Name; }
        public string Name { get; set; }
        #endregion
    }

    public class TeamScriptItem
    {
        public TeamScriptItem(int _actionType, string _value)
        {
            ScriptActionIndex = _actionType;
            ActionValue = _value;
        }

        #region Public Calls - TeamScriptItem
        public override string ToString() { return string.Format("{0:D2}", ScriptActionIndex) + " " + ActionValue; }
        public int ScriptActionIndex { get; set; }
        public string ActionValue { get; set; }
        #endregion
    }
}
