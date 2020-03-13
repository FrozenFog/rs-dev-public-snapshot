﻿using System;
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
    }

    public class TeamScriptGroup : TeamLogicItem
    {
        private List<TeamScriptItem> data = new List<TeamScriptItem>();


        public TeamScriptGroup(INIEntity ent) : base(ent)
        {
            Name = ent.PopPair("Name").Value;
            foreach (INIPair p in ent.DataList)
            {
                string[] tmp = p.ParseStringList();
                data.Add(new TeamScriptItem(int.Parse(tmp[0]), tmp[1]));
            }
        }
        public TeamScriptGroup() : base() { }


        #region Public Calls - TeamScriptGroup
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
        public int ScriptActionIndex { get; set; }
        public string ActionValue { get; set; }
        #endregion
    }
}
