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


        #region House & Country
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
                if (obj.OwnerHouse == prev) obj.OwnerHouse = now;
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
                if (trg.OwnerCountry == prev) trg.OwnerCountry = now;
            }

            // all teams
            foreach (TeamItem team in Teams)
            {
                if (team.House == prev) team.House = now;
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
