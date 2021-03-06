using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure.Logic;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;
using RelertSharp.Common;

namespace RelertSharp.MapStructure
{
    public partial class Map
    {
        #region Locigs
        #region Triggers
        public TriggerItem AddTrigger(string triggerName)
        {
            TriggerItem trigger = Triggers.AddItem(NewID, triggerName);
            TagItem tag = Tags.AddItem(NewID, trigger);
            return trigger;
        }
        public TriggerItem AddTrigger(TriggerItem src)
        {
            TriggerItem trigger = Triggers.CopyItem(src, NewID);
            TagItem tag = Tags.AddItem(NewID, trigger);
            return trigger;
        }
        public bool RemoveTrigger(TriggerItem trigger, bool removeTag = true)
        {
            DelID(trigger.Id);
            if (Triggers.RemoveItem(trigger))
            {
                if (removeTag)
                {
                    IEnumerable<string> removedTags = Tags.RemoveItem(trigger);
                    removedTags.Foreach(x => DelID(x));
                }
                return true;
            }
            return false;
        }
        #endregion


        #region Taskforces
        public TaskforceItem AddTaskforce(string name)
        {
            TaskforceItem tf = Taskforces.AddItem(NewID, name);
            return tf;
        }
        public TaskforceItem AddTaskforce(TaskforceItem src)
        {
            TaskforceItem tf = Taskforces.CopyItem(src, NewID);
            return tf;
        }
        public bool RemoveTaskforce(TaskforceItem item)
        {
            DelID(item.Id);
            return Taskforces.RemoveItem(item);
        }
        #endregion


        #region Scripts
        public TeamScriptGroup AddScript(string name)
        {
            TeamScriptGroup script = Scripts.AddItem(NewID, name);
            return script;
        }
        public TeamScriptGroup AddScript(TeamScriptGroup src)
        {
            TeamScriptGroup copy = Scripts.CopyItem(src, NewID);
            return copy;
        }
        public bool RemoveScript(TeamScriptGroup item)
        {
            DelID(item.Id);
            return Scripts.RemoveItem(item);
        }
        #endregion

        #region AiTrigger
        public AITriggerItem AddAiTrigger(string name)
        {
            AITriggerItem trg = AiTriggers.AddItem(NewID, name);
            return trg;
        }
        public AITriggerItem AddAiTrigger(AITriggerItem src)
        {
            AITriggerItem copy = AiTriggers.CopyItem(src, NewID);
            return copy;
        }
        public bool RemoveAiTrigger(AITriggerItem item)
        {
            DelID(item.Id);
            return AiTriggers.RemoveItem(item);
        }
        #endregion

        #region Teams
        public TeamItem AddTeam(string name)
        {
            TeamItem team = Teams.AddItem(NewID, name);
            return team;
        }
        public TeamItem AddTeam(TeamItem src)
        {
            TeamItem copy = Teams.CopyItem(src, NewID);
            return copy;
        }
        public bool RemoveTeam(TeamItem item)
        {
            DelID(item.Id);
            return Teams.RemoveItem(item);
        }
        #endregion


        #region House & Country
        public void AddHouse(string houseName, out HouseItem house, out CountryItem country)
        {
            CountryItem c = new CountryItem(houseName);
            HouseItem h = HouseItem.FromCountry(c);
            bool bCountry = false, bHouse = false;
            for (int i = 0; !bCountry || !bHouse; i++)
            {
                string id = i.ToString();
                if (!bCountry && !Countries.AllId.Contains(id))
                {
                    Countries[id] = c;
                    bCountry = true;
                }
                if (!bHouse && Houses.AllId.Contains(id))
                {
                    Houses[id] = h;
                    bHouse = true;
                }
            }
            house = h;
            country = c;
        }
        public bool RemoveHouse(HouseItem house)
        {
            Houses.Remove(house.Id);
            CountryItem c = Countries.GetCountry(house.Country);
            Countries.Remove(c.Id);
            return true;
        }
        /// <summary>
        /// Only used by listener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="prev"></param>
        /// <param name="now"></param>
        private void HouseNameChanged(object sender, string prev, string now)
        {
            // player house
            if (Info.PlayerHouseName == prev) Info.PlayerHouseName = now;

            // all map objects
            foreach (ICombatObject obj in AllCombatObjects)
            {
                if (obj.Owner == prev) obj.Owner = now;
            }

            // all allies
            foreach (HouseItem house in Houses)
            {
                for (int i = 0; i< house.AlliesWith.Count; i++)
                {
                    if (house.AlliesWith[i] == prev)
                    {
                        house.AlliesWith[i] = now;
                        house.OnAllInfoUpdate();
                    }
                }
            }
        }
        /// <summary>
        /// Only used by listener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="prev"></param>
        /// <param name="now"></param>
        private void CountryNameChanged(object sender, string prev, string now)
        {
            // all triggers
            foreach (TriggerItem trg in Triggers)
            {
                if (trg.Owner == prev) trg.Owner = now;
            }

            // all teams
            foreach (TeamItem team in Teams)
            {
                if (team.Owner == prev) team.Owner = now;
            }

            // all houses
            foreach (HouseItem house in Houses)
            {
                if (house.Country == prev) house.Country = now;
            }
        }
        #endregion


        #region Misc
        public LocalVarItem AddLocalVar(string name)
        {
            LocalVarItem local = LocalVariables.AddItem(null, name);
            return local;
        }
        public bool RemoveVariable(LocalVarItem local)
        {
            return LocalVariables.RemoveItem(local);
        }
        #endregion
        #endregion
    }
}
