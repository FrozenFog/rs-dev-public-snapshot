using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    internal partial class LogicEditor
    {
        private void TraceLogic(int tabIndex, object obj, ListBox dest)
        {
            if (obj != null)
            {
                tbcMain.SelectedIndex = tabIndex;
                dest.SelectedItem = obj;
            }
        }
        public void TraceTag(TagItem tag)
        {
            TriggerItem trigger = lbxTriggerList.Items.Cast<TriggerItem>().Where(x => x.ID == tag.AssoTrigger).First();
            if (trigger != null)
            {
                tbcMain.SelectedIndex = 0;
                lbxTriggerList.SelectedItem = trigger;
            }
        }

        public void TraceLogicItem(LogicType type, string keyId)
        {
            switch (type)
            {
                case LogicType.Trigger:
                    TriggerItem trg = Map.Triggers[keyId];
                    TraceLogic(0, trg, lbxTriggerList);
                    break;
                case LogicType.Tag:
                    TagItem tag = Map.Tags[keyId];
                    if (tag != null)
                    {
                        TraceLogic(0, Map.Triggers[tag.AssoTrigger], lbxTriggerList);
                    }
                    break;
                case LogicType.Team:
                    TeamItem team = Map.Teams[keyId];
                    TraceLogic(2, team, lbxTeamList);
                    break;
                case LogicType.Taskforce:
                    TaskforceItem task = Map.TaskForces[keyId];
                    TraceLogic(1, task, lbxTaskList);
                    break;
                case LogicType.Script:
                    TeamScriptGroup script = Map.Scripts[keyId];
                    TraceLogic(1, script, lbxScriptList);
                    break;
            }
        }
    }
}
