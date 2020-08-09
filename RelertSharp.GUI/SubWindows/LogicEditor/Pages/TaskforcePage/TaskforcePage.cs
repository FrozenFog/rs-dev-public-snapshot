using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    internal partial class LogicEditor
    {
        private void InitialTaskforcePage()
        {
            LoadToObjectCollection(lbxTaskList, map.TaskForces);
            LoadToObjectCollection(lbxScriptList, map.Scripts);
            pnlTaskforce.Initialize(lbxTaskList);
            pnlTaskforce.TaskforceNameUpdated += PnlTaskforce_TaskforceNameUpdated;
            pnlTaskforce.TaskforceAdded += PnlTaskforce_TaskforceAdded;
            pnlTaskforce.TaskforceDeleted += PnlTaskforce_TaskforceDeleted;
            pnlScript.Initialize(descriptCollection.Scripts);
            pnlScript.NeedPlayingSound += PnlScript_NeedPlayingSound;
            pnlScript.JumpToWaypoint += Pnl_JumpToWaypoint;
            pnlScript.ScriptNameUpdated += PnlScript_ScriptNameUpdated;
            pnlScript.NewScriptAdded += PnlScript_NewScriptAdded;
            pnlScript.ScriptRemoved += PnlScript_ScriptRemoved;
            if (lbxTaskList.Items.Count > 0) lbxTaskList.SelectedIndex = 0;
            if (lbxScriptList.Items.Count > 0) lbxScriptList.SelectedIndex = 0;
        }

        private void PnlScript_ScriptRemoved(object sender, EventArgs e)
        {
            RemoveAt(lbxScriptList, ref updatingLbxScriptList);
        }

        private void PnlScript_NewScriptAdded(object sender, TeamScriptGroup scripts)
        {
            AddTo(lbxScriptList, scripts, ref updatingLbxScriptList);
        }

        private void PnlScript_ScriptNameUpdated(object sender, TeamScriptGroup scripts)
        {
            UpdateAt(lbxScriptList, scripts, ref updatingLbxScriptList);
        }

        private void PnlScript_NeedPlayingSound(object sender, TriggerParam param, TechnoPair p)
        {
            ManageSound(param, p, true);
        }

        private void PnlTaskforce_TaskforceDeleted(object sender, TaskforceItem taskforce)
        {
            RemoveAt(lbxTaskList, ref updatingLbxTaskList);
        }

        private void PnlTaskforce_TaskforceAdded(object sender, TaskforceItem taskforce)
        {
            AddTo(lbxTaskList, taskforce, ref updatingLbxTaskList);
        }

        private void PnlTaskforce_TaskforceNameUpdated(object sender, TaskforceItem taskforce)
        {
            UpdateAt(lbxTaskList, taskforce, ref updatingLbxTaskList);
        }
        private bool updatingLbxTaskList = false;
        private void lbxTaskList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!updatingLbxTaskList)
            {
                ChangeSaved = false;
                TaskforceItem item = lbxTaskList.SelectedItem as TaskforceItem;
                pnlTaskforce.Reload(item);
            }
        }
        private bool updatingLbxScriptList = false;
        private void lbxScriptList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!updatingLbxScriptList)
            {
                ChangeSaved = false;
                TeamScriptGroup item = lbxScriptList.SelectedItem as TeamScriptGroup;
                pnlScript.Reload(item);
            }
        }
        private void splitTaskScript_Resize(object sender, EventArgs e)
        {
            int width = splitTaskScript.Width / 2;
            if (splitTaskScript.Panel1MinSize < width && width < splitTaskScript.Width - splitTaskScript.Panel2MinSize) splitTaskScript.SplitterDistance = width;
        }
    }
}
