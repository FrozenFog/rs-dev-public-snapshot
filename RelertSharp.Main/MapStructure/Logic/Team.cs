using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.IniSystem;
using relert_sharp.Common;
using static relert_sharp.Utils.Misc;

namespace relert_sharp.MapStructure.Logic
{
    public class TeamCollection : TeamLogicCollection<TeamItem>
    {
        #region Ctor - TeamCollection
        public TeamCollection() : base() { }
        #endregion


        #region Public Methods - TeamCollection
        public IEnumerable<TechnoPair> ToTechno()
        {
            List<TechnoPair> result = new List<TechnoPair>();
            foreach (TeamItem team in this)
            {
                result.Add(new TechnoPair(team.ID, team.TeamName));
            }
            return result;
        }
        #endregion

    }
    public class TeamItem : TeamLogicItem
    {
        private Dictionary<string, INIPair> residual;
        public TeamItem(INIEntity ent) : base(ent)
        {
            TeamName = ent.PopPair("Name").Value;
            TaskforceID = ent.PopPair("TaskForce").Value;
            ScriptID = ent.PopPair("Script").Value;
            Waypoint = WaypointInt(ent.PopPair("Waypoint").Value);
            House = ent.PopPair("House").Value;
            VeteranLevel = (TeamVeteranLevel)(int.Parse(ent.PopPair("VeteranLevel").Value));
            MCDecision = (TeamMCDecision)(int.Parse(ent.PopPair("MindControlDecision").Value));
            TeamCapacity = int.Parse(ent.PopPair("Max").Value);
            Priority = int.Parse(ent.PopPair("Priority").Value);
            TechLevel = int.Parse(ent.PopPair("TechLevel").Value);
            Group = int.Parse(ent.PopPair("Group").Value);
            residual = ent.DictData;
        }
        public TeamItem() : base() { }

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
        public string TeamName { get; set; }
        public string TaskforceID { get; set; }
        public string ScriptID { get; set; }
        public string House { get; set; }
        public int Waypoint { get; set; }
        #endregion
    }
}
