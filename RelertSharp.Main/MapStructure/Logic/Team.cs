using RelertSharp.Common;
using RelertSharp.IniSystem;
using System.Collections.Generic;

namespace RelertSharp.MapStructure.Logic
{
    public class TeamCollection : TeamLogicCollection<TeamItem>
    {
        #region Ctor - TeamCollection
        public TeamCollection() : base() { }
        #endregion


        #region Public Methods - TeamCollection
        public TeamItem NewTeam(string id)
        {
            TeamItem item = new TeamItem();
            item.Id = id;
            item.Name = "New Team";
            //item.TaskforceID =
            //    GlobalVar.GlobalMap.Taskforces.Count() > 0 ?
            //    GlobalVar.GlobalMap.Taskforces.ElementAt(0).Id :
            //    string.Empty;
            //item.ScriptID =
            //    GlobalVar.GlobalMap.Scripts.Count() > 0 ?
            //    GlobalVar.GlobalMap.Scripts.ElementAt(0).Id :
            //    string.Empty;
            //item.TagID = string.Empty;
            //item.Waypoint = 0;
            //item.House =
            //    GlobalVar.GlobalMap.Countries.Count() > 0 ?
            //    GlobalVar.GlobalMap.Countries.ElementAt(0).Name :
            //    string.Empty;
            //item.VeteranLevel = TeamVeteranLevel.Rookie;
            //item.MCDecision = TeamMCDecision.Default;
            //item.TeamCapacity = 5;
            //item.Priority = 5;
            //item.TechLevel = 0;
            //item.Group = -1;
            //item.Attributes = new BitArray(21);
            //item.GetToUnit = new TeamUnit(item);
            //item.Residue = new Dictionary<string, INIPair>();
            return item;
        }
        #endregion

    }

    /// <summary>
    /// use yes/no for bool
    /// </summary>
    public class TeamItem : TeamLogicItem, IGroupable, IOwnableObject, ILogicItem
    {
        #region Ctor - TeamItem
        public TeamItem(INIEntity ent) : base(ent)
        {
            //TaskforceID = ent.PopPair("TaskForce").Value;
            //ScriptID = ent.PopPair("Script").Value;
            //TagID = ent.PopPair("Tag").Value;
            //Waypoint = WaypointInt(ent.PopPair("Waypoint").Value);
            //House = ent.PopPair("House").Value;
            //VeteranLevel = (TeamVeteranLevel)ent.PopPair("VeteranLevel").ParseInt(1);
            //MCDecision = (TeamMCDecision)ent.PopPair("MindControlDecision").ParseInt(0);
            //TeamCapacity = ent.PopPair("Max").ParseInt(5);
            //Priority = ent.PopPair("Priority").ParseInt(5);
            //TechLevel = ent.PopPair("TechLevel").ParseInt();
            //Group = ent.PopPair("Group").ParseInt(-1);
            Residue = ent.DictData;
            //LoadAttributes();
            //GetToUnit = new TeamUnit(this);
        }
        public TeamItem() : base()
        {
            Residue = new Dictionary<string, INIPair>();
        }
        #endregion


        #region Private Methods - TeamItem
        //private void LoadAttributes()
        //{
        //    Dictionary<string, INIPair> d = new Dictionary<string, INIPair>();
        //    foreach (string key in residual.Keys)
        //    {
        //        if (Constant.TeamBoolIndex.Contains(key)) LoadAttribute(key);
        //        else d[key] = residual[key];
        //    }
        //    residual = d;
        //}
        //private void LoadAttribute(string index)
        //{
        //    SetAttribute(index, ParseBool(residual[index].Value));
        //}
        #endregion


        #region Public Methods - TeamItem
        public override string Name
        {
            get { return this[Constant.KEY_NAME].Value; }
            set
            {
                this[Constant.KEY_NAME] = new INIPair(Constant.KEY_NAME, value);
            }
        }
        public INIEntity GetSaveData()
        {
            INIEntity result = new INIEntity(Id);
            if (GlobalVar.GlobalConfig?.ModConfig != null)
            {
                ModConfig cfg = GlobalVar.GlobalConfig.ModConfig;
                foreach (INIPair p in Residue.Values)
                {
                    var attr = cfg.TeamItems[p.Name];
                    if (attr.DefaultValue == null || attr.DefaultValue != p.Value) result.AddPair(p);
                }
            }
            else result.AddPair(Residue.Values);
            return result;
        }
        public string[] ExtractParameter()
        {
            return new string[]
            {
                Id,
                Name,
                TaskforceID,
                ScriptID,
                Owner,
                Group
            };
        }
        #endregion


        #region Public Calls - TeamItem
        public string TaskforceID
        {
            get { return this[Constant.MapStructure.KEY_TASKFORCE].Value; }
            set { this[Constant.MapStructure.KEY_TASKFORCE] = new INIPair(Constant.MapStructure.KEY_TASKFORCE, value); }
        }
        public string ScriptID
        {
            get { return this[Constant.MapStructure.KEY_SCRIPT].Value; }
            set { this[Constant.MapStructure.KEY_SCRIPT] = new INIPair(Constant.MapStructure.KEY_SCRIPT, value); }
        }
        /// <summary>
        /// House
        /// </summary>
        public string Owner
        {
            get { return this[Constant.MapStructure.KEY_HOUSE].Value; }
            set { this[Constant.MapStructure.KEY_HOUSE] = new INIPair(Constant.MapStructure.KEY_HOUSE, value); }
        }
        public string Group
        {
            get { return this[Constant.MapStructure.KEY_GROUP].GetString(Constant.ID_INVALID); }
            set { this[Constant.MapStructure.KEY_GROUP] = new INIPair(Constant.MapStructure.KEY_GROUP, value); }
        }
        //public BitArray Attributes { get; set; } = new BitArray(21);
        public Dictionary<string, INIPair> Residue { get; set; }
        public INIPair this[string key]
        {
            get
            {
                if (Residue.ContainsKey(key)) return Residue[key];
                else return new INIPair(key);
            }
            set
            {
                Residue[key] = value;
            }
        }
        public LogicType ItemType { get { return LogicType.Team; } }
        //public TeamUnit GetToUnit { get; set; }
        #endregion
    }

    //public class TeamUnit
    //{
    //    public struct TeamUnitNode
    //    {
    //        public object Value;
    //        public string ShowName;
    //        public TeamUnitNode(string showName, object value) { Value = value; ShowName = showName; }
    //    }

    //    #region Ctor - TeamUnit
    //    public TeamUnit(TeamItem item)
    //    {
    //        // A new config could be added to decide the name displayed on the screen.
    //        Data["TeamName"] = new TeamUnitNode(Language.DICT["LGColvRowTeamName"], item.Name);
    //        Data["TaskforceID"] = new TeamUnitNode(Language.DICT["LGColvRowTeamTaskforce"], item.TaskforceID);
    //        Data["ScriptID"] = new TeamUnitNode(Language.DICT["LGColvRowTeamScript"], item.ScriptID);
    //        Data["TagID"] = new TeamUnitNode(Language.DICT["LGColvRowTeamTag"], item.TagID);
    //        Data["Waypoint"] = new TeamUnitNode(Language.DICT["LGColvRowTeamWaypoint"], item.Waypoint);
    //        Data["House"] = new TeamUnitNode(Language.DICT["LGColvRowTeamHouse"], item.House);
    //        Data["VeteranLevel"] = new TeamUnitNode(Language.DICT["LGColvRowTeamVeteran"], item.VeteranLevel);
    //        Data["MCDecision"] = new TeamUnitNode(Language.DICT["LGColvRowTeamMCD"], item.MCDecision);
    //        Data["TeamCapacity"] = new TeamUnitNode(Language.DICT["LGColvRowTeamCapacity"], item.TeamCapacity);
    //        Data["Priority"] = new TeamUnitNode(Language.DICT["LGColvRowTeamPriority"], item.Priority);
    //        Data["TechLevel"] = new TeamUnitNode(Language.DICT["LGColvRowTeamTechLevel"], item.TechLevel);
    //        Data["Group"] = new TeamUnitNode(Language.DICT["LGColvRowTeamGroup"], item.Group);
    //        for (int i = 0; i < Constant.TeamBoolIndex.Count; ++i) // For this one, a List<string> to save each bool's display name.
    //            Data[Constant.TeamBoolIndex[i]] = new TeamUnitNode(Language.DICT["LGColvRowTeamBool" + i.ToString()], item.Attributes[i]);
    //    }
    //    public TeamUnit() { }
    //    #endregion

    //    #region Public Calls - TeamUnit
    //    public Dictionary<string, TeamUnitNode> Data { get; set; } = new Dictionary<string, TeamUnitNode>();
    //    #endregion
    //}
}
