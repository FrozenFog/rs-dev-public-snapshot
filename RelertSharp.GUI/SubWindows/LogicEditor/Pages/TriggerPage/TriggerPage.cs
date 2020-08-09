using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.GUI;
using RelertSharp.MapStructure.Logic;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    internal partial class LogicEditor
    {
        internal event I2dLocateableHandler JumpToWaypoint;


        private void InitialTriggerPage()
        {
            pnlTriggerTag.Initialize(lbxTriggerList);
            pnlTriggerTag.NewTriggerFired += PnlTriggerTag_NewTriggerFired;
            pnlTriggerTag.TriggerDeleted += PnlTriggerTag_TriggerDeleted;
            pnlTriggerTag.TriggerUpdated += PnlTriggerTag_TriggerUpdated;
            pnlTriggerTag.TemplateStatChanged += PnlTriggerTag_TemplateStatChanged;
            pnlTriggerTag.TraceFired += PnlTriggerTag_TraceFired;
            pnlEvent.Initialize(lbxTriggerList, true, descriptCollection);
            pnlEvent.NeedPlayingSound += PlayingSound;
            pnlEvent.TriggerTracing += PnlTriggerTag_TraceFired;
            pnlEvent.TriggerRefreshing += TriggerRefreshing;
            pnlEvent.JumpToWaypoint += Pnl_JumpToWaypoint;
            pnlAction.Initialize(lbxTriggerList, false, descriptCollection);
            pnlAction.NeedPlayingSound += PlayingSound;
            pnlAction.TriggerTracing += PnlTriggerTag_TraceFired;
            pnlAction.TriggerRefreshing += TriggerRefreshing;
            pnlAction.JumpToWaypoint += Pnl_JumpToWaypoint;
            UpdateTrgList(TriggerItem.DisplayingType.IDandName);
        }

        private void Pnl_JumpToWaypoint(object sender, I2dLocateable pos)
        {
            OnJumpToWaypoint(pos);
        }
        protected virtual void OnJumpToWaypoint(I2dLocateable cell)
        {
            JumpToWaypoint?.Invoke(this, cell);
        }
        private void TriggerRefreshing(object sender, TriggerItem trigger)
        {
            UpdateAt(lbxTriggerList, trigger, ref updatingLbxTriggerList);
            lbxTriggerList_SelectedValueChanged(null, null);
        }

        private void PlayingSound(object sender, TriggerParam param, TechnoPair p)
        {
            ManageSound(param, p);
        }

        private void PnlTriggerTag_TraceFired(object sender, TriggerItem trigger)
        {
            lbxTriggerList.SelectedItem = trigger;
        }

        private void PnlTriggerTag_TemplateStatChanged(object sender, bool isTemplate)
        {
            lbxTriggerList.Enabled = !isTemplate;
        }

        private void PnlTriggerTag_TriggerUpdated(object sender, TriggerItem trigger)
        {
            UpdateAt(lbxTriggerList, trigger, ref updatingLbxTriggerList);
        }

        private void PnlTriggerTag_TriggerDeleted(object sender, EventArgs e)
        {
            RemoveAt(lbxTriggerList, ref updatingLbxTriggerList);
        }

        private void PnlTriggerTag_NewTriggerFired(object sender, TriggerItem trigger)
        {
            AddTo(lbxTriggerList, trigger, ref updatingLbxTriggerList);
        }


        private bool updatingLbxTriggerList = false;
        private void lbxTriggerList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!updatingLbxTriggerList)
            {
                ChangeSaved = false;
                TriggerItem item = lbxTriggerList.SelectedItem as TriggerItem;
                if (item != null)
                {
                    IEnumerable<TagItem> tags = Map.Tags.GetTagFromTrigger(item.ID);
                    pnlTriggerTag.Reload(item, tags);
                    pnlEvent.Reload(item);
                    pnlAction.Reload(item);
                }
            }
        }
        private void splitEA_Resize(object sender, EventArgs e)
        {
            int width = splitEA.Width / 2;
            if (splitEA.Panel1MinSize < width && width < splitEA.Width - splitEA.Panel2MinSize) splitEA.SplitterDistance = width;
        }
        private void tsmiTrgLstAscending_Click(object sender, EventArgs e)
        {
            map.Triggers.AscendingSort();
            UpdateTrgList();
        }
        private void tsmiTrgLstDecending_Click(object sender, EventArgs e)
        {
            map.Triggers.DecendingSort();
            UpdateTrgList();
        }
        private void tsmiTrgLstID_Click(object sender, EventArgs e)
        {
            UpdateTrgList(TriggerItem.DisplayingType.OnlyID);
        }
        private void tsmiTrgLstName_Click(object sender, EventArgs e)
        {
            UpdateTrgList(TriggerItem.DisplayingType.OnlyName);
        }
        private void tsmiTrgLstIDName_Click(object sender, EventArgs e)
        {
            UpdateTrgList(TriggerItem.DisplayingType.IDandName);
        }
    }
}
