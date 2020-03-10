﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.IniSystem;
using static relert_sharp.Language;

namespace relert_sharp.MapStructure.Logic
{
    public class DescriptCollection
    {
        private Dictionary<int, TriggerDescription> events = new Dictionary<int, TriggerDescription>();
        private Dictionary<int, TriggerDescription> actions = new Dictionary<int, TriggerDescription>();


        #region Constructor - DescriptCollection
        public DescriptCollection()
        {
            INIFile f = new INIFile(@"Triggers.tgc");
            INIEntity ev = f["EventParams"];
            INIEntity ac = f["ActionParams"];
            Load(ev, events);
            Load(ac, actions);
        }
        #endregion


        #region Private Methods - DescriptCollection
        private void Load(INIEntity ent, Dictionary<int, TriggerDescription> _data)
        {
            foreach (INIPair p in ent)
            {
                int i = 0;
                int id = int.Parse(p.Name);
                string[] param = p.ParseStringList();
                int paramNum = int.Parse(param[i++]);
                string[] initParams = param.Skip(1).Take(paramNum).ToArray();
                i += paramNum;
                string abst = DICT[param[i++]];
                string desc = DICT[param[i++]];
                int settingNum = int.Parse(param[i++]);
                TriggerDescription description = new TriggerDescription(id, abst, desc, initParams);
                while (settingNum-- > 0)
                {
                    description.AddParam(param.Skip(i).Take(5).ToArray());
                    i += 5;
                }
                _data[id] = description;
            }
        }
        #endregion


        #region Public Methods - DescriptCollection
        public TriggerDescription Event(int eventID)
        {
            if (events.Keys.Contains(eventID)) return events[eventID];
            else return null;
        }
        public TriggerDescription Action(int actionID)
        {
            if (actions.Keys.Contains(actionID)) return actions[actionID];
            else return null;
        }
        #endregion


        #region Public Calls - DescriptCollection
        public Dictionary<int, TriggerDescription>.ValueCollection Events { get { return events.Values; } }
        public Dictionary<int, TriggerDescription>.ValueCollection Actions { get { return actions.Values; } }
        #endregion
    }


    public class TriggerDescription
    {
        #region Constructor - TriggerDescription
        public TriggerDescription(int id, string abst, string desc, string[] param)
        {
            ID = id;
            Abstract = abst;
            Description = desc;
            Parameters = new List<TriggerParam>();
            InitParams = param;
        }
        #endregion


        #region Public Methods - TriggerDescription
        public override string ToString()
        {
            return string.Format("{0:D2} ", ID) + Abstract;
        }
        #endregion


        #region Internal - TriggerDescription
        internal void AddParam(string[] param)
        {
            Parameters.Add(new TriggerParam(
                int.Parse(param[0]),
                param[1] == "1",
                int.Parse(param[2]),
                param[3],
                int.Parse(param[4])
                ));
        }
        #endregion


        #region Public Calls - TriggerDescription
        public List<TriggerParam> Parameters { get; private set; }
        public int ID { get; set; }
        public string Abstract { get; private set; }
        public string Description { get; private set; }
        public string[] InitParams { get; private set; }
        #endregion
    }


    public class TriggerParam
    {
        public enum ParamType
        {
            PlainString = 0,
            SelectableString = 1,
            Bool = 2
        }
        public enum ComboContent
        {
            NonCombo = 0,
            Houses = 1,
            Buildings = 2,
            Units = 3,
            Infantries = 4,
            Aircrafts = 5,
            TechnoType = 6,
            Teams = 7,
            GlobalVar = 8,
            LocalVar = 9,
            Movies = 10,
            CsfLabel = 11,
            Triggers = 12,
            Tags = 13,
            SoundNames = 14,
            EvaNames = 15,
            ThemeNames = 20,
            SuperWeapons = 16,
            Animations = 17,
            Weapons = 18,
            ParticalAnim = 19,
            VoxelAnim = 21
        }
        #region Constructor - TriggerParam
        public TriggerParam(int paramType, bool traceable, int pos, string name, int comboType)
        {
            Type = (ParamType)paramType;
            ParamPos = pos;
            Traceable = traceable;
            Name = DICT[name];
            ComboType = (ComboContent)comboType;
        }
        #endregion


        #region Public Methods - TriggerParam
        public dynamic GetParameter(string[] paramSrc, bool isBool = false)
        {
            if (isBool) return paramSrc[ParamPos] == "1";
            else
            {
                if (ParamPos == 6 && Traceable) return Utils.Misc.WaypointInt(paramSrc[ParamPos]).ToString();
                return paramSrc[ParamPos];
            }
        }
        #endregion


        #region Public Calls - TriggerParam
        public ParamType Type { get; private set; }
        public ComboContent ComboType { get; private set; }
        public bool Traceable { get; private set; }
        public string Name { get; private set; }
        public int ParamPos { get; private set; }
        #endregion
    }
}