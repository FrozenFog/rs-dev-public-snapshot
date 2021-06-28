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

        #endregion
        //public TeamScriptGroup NewScript()
        //{
        //    TeamScriptGroup group = Scripts.NewScript(NewID);
        //    return group;
        //}
        //public TeamScriptGroup NewScript(TeamScriptGroup src)
        //{
        //    TeamScriptGroup g = Scripts.NewScript(src, NewID);
        //    return g;
        //}
        //public void RemoveScript(TeamScriptGroup group)
        //{
        //    Scripts.Remove(group.Id);
        //    DelID(group.Id);
        //}
        //public TaskforceItem NewTaskforce()
        //{
        //    TaskforceItem t = Taskforces.NewTaskforce(NewID);
        //    return t;
        //}
        //public void RemoveTaskforce(TaskforceItem item)
        //{
        //    Taskforces.Remove(item.Id);
        //    DelID(item.Id);
        //}
        //public TeamItem NewTeam()
        //{
        //    TeamItem t = Teams.NewTeam(NewID);
        //    return t;
        //}
        //public void RemoveTeam(TeamItem item)
        //{
        //    Teams.Remove(item.Id);
        //    DelID(item.Id);
        //}
        //public AITriggerItem NewAITrigger()
        //{
        //    AITriggerItem t = AiTriggers.NewAITrigger(NewID);
        //    return t;
        //}
        //public void RemoveAITrigger(AITriggerItem item)
        //{
        //    AiTriggers.Remove(item.Id);
        //    DelID(item.Id);
        //}
    }
}
