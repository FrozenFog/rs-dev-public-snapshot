using RelertSharp.Common;
using RelertSharp.IniSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static RelertSharp.Utils.Misc;

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
            //    GlobalVar.CurrentMapDocument.Map.Taskforces.Count() > 0 ?
            //    GlobalVar.CurrentMapDocument.Map.Taskforces.ElementAt(0).Id :
            //    string.Empty;
            //item.ScriptID =
            //    GlobalVar.CurrentMapDocument.Map.Scripts.Count() > 0 ?
            //    GlobalVar.CurrentMapDocument.Map.Scripts.ElementAt(0).Id :
            //    string.Empty;
            //item.TagID = string.Empty;
            //item.Waypoint = 0;
            //item.House =
            //    GlobalVar.CurrentMapDocument.Map.Countries.Count() > 0 ?
            //    GlobalVar.CurrentMapDocument.Map.Countries.ElementAt(0).Name :
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
        public IEnumerable<TechnoPair> ToTechno()
        {
            List<TechnoPair> result = new List<TechnoPair>();
            foreach (TeamItem team in this)
            {
                result.Add(new TechnoPair(team.Id, team.Name));
            }
            return result;
        }
        #endregion

    }

    /// <summary>
    /// use yes/no for bool
    /// </summary>
    public class TeamItem : TeamLogicItem
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
                this[Constant.KEY_NAME] = new INIPair(Constant.KEY_NAME, Value);
            }
        }
        public INIEntity GetSaveData()
        {
            INIEntity result = new INIEntity(Id);
            //result.AddPair("Name", Name);
            //result.AddPair("House", House);
            //result.AddPair("Script", ScriptID);
            //result.AddPair("TaskForce", TaskforceID);
            //result.AddPair("Max", TeamCapacity);
            //result.AddPair("Waypoint", WaypointString(Waypoint));
            //result.AddPair("VeteranLevel", (int)VeteranLevel);
            //result.AddPair("MindControlDecision", (int)MCDecision);
            //result.AddPair("Priority", Priority);
            //result.AddPair("TechLevel", TechLevel);
            //result.AddPair("Group", Group);
            //for (int i = 0; i < Constant.TeamBoolIndex.Count; i++)
            //{
            //    result.AddPair(Constant.TeamBoolIndex[i], YesNo(Attributes.Get(i)));
            //}
            result.AddPair(Residue.Values);
            return result;
        }
        //public void SetAttribute(string index, bool value)
        //{
        //    Attributes.Set(Constant.TeamBoolIndex.IndexOf(index), value);
        //}
        //public bool GetAttribute(string index)
        //{
        //    return Attributes.Get(Constant.TeamBoolIndex.IndexOf(index));
        //}
        //public void SetFromUnit(TeamUnit teamUnit)
        //{
        //    Name = (string)teamUnit.Data["TeamName"].Value;
        //    TaskforceID = (string)teamUnit.Data["TaskforceID"].Value;
        //    ScriptID = (string)teamUnit.Data["ScriptID"].Value;
        //    TagID = (string)teamUnit.Data["TagID"].Value;
        //    Waypoint = (int)teamUnit.Data["Waypoint"].Value;
        //    House = (string)teamUnit.Data["House"].Value;
        //    VeteranLevel = (TeamVeteranLevel)teamUnit.Data["VeteranLevel"].Value;
        //    MCDecision = (TeamMCDecision)teamUnit.Data["VeteranLevel"].Value;
        //    TeamCapacity = (int)teamUnit.Data["TeamCapacity"].Value;
        //    Priority = (int)teamUnit.Data["Priority"].Value;
        //    TechLevel = (int)teamUnit.Data["TechLevel"].Value;
        //    Group = (int)teamUnit.Data["Group"].Value;
        //    foreach (string idx in Constant.TeamBoolIndex) SetAttribute(idx, (bool)teamUnit.Data[idx].Value);
        //}
        //public TeamItem Copy(string newid)
        //{
        //    TeamItem item = new TeamItem();
        //    item.Id = newid;
        //    item.Name = Name += " Clone";
        //    item.TaskforceID = TaskforceID;
        //    item.ScriptID = ScriptID;
        //    item.TagID = TagID;
        //    item.Waypoint = Waypoint;
        //    item.House = House;
        //    item.VeteranLevel = VeteranLevel;
        //    item.MCDecision = MCDecision;
        //    item.TeamCapacity = TeamCapacity;
        //    item.Priority = Priority;
        //    item.TechLevel = TechLevel;
        //    item.Group = Group;
        //    item.Attributes = Attributes;
        //    item.GetToUnit = new TeamUnit(item);
        //    return item;
        //}
        #endregion


        #region Public Calls - TeamItem
        public int Group { get; set; }
        public int TechLevel { get; set; }
        /// <summary>
        /// Recruitable rank of this team, aka priority
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// Capacity of this team (in this map)
        /// </summary>
        public int TeamCapacity { get; set; }
        public TeamMCDecision MCDecision { get; set; }
        public TeamVeteranLevel VeteranLevel { get; set; }
        public string TaskforceID { get; set; }
        public string ScriptID { get; set; }
        public string TagID { get; set; }
        public string House { get; set; }
        public int Waypoint { get; set; }
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
