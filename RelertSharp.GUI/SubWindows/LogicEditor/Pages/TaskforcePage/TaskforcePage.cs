﻿using System;
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
            pnlTaskforce.Initialize(lbxTaskList);
            pnlTaskforce.TaskforceNameUpdated += PnlTaskforce_TaskforceNameUpdated;
            pnlTaskforce.TaskforceAdded += PnlTaskforce_TaskforceAdded;
            pnlTaskforce.TaskforceDeleted += PnlTaskforce_TaskforceDeleted;
            if (lbxTaskList.Items.Count > 0) lbxTaskList.SelectedIndex = 0;
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
                TaskforceItem item = lbxTaskList.SelectedItem as TaskforceItem;
                pnlTaskforce.Reload(item);
            }
        }
    }
}
