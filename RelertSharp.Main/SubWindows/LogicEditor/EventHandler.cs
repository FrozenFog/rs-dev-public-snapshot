using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using relert_sharp.MapStructure.Logic;
using relert_sharp.FileSystem;
using relert_sharp.Common;
using relert_sharp.IniSystem;

namespace relert_sharp.SubWindows.LogicEditor
{
    public partial class LogicEditor : Form
    {
        #region Trigger Page

        #region MenuStrip
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
        #endregion

        private void lklParams_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int tagid = int.Parse(((LinkLabel)sender).Tag.ToString());
            TechnoPair p;
            TriggerDescription desc;
            if (((LinkLabel)sender).Parent == gpbEventParam)
            {
                p = cbbEP[tagid].SelectedItem as TechnoPair;
                desc = cbbEventAbst.SelectedItem as TriggerDescription;
            }
            else
            {
                p = cbbAP[tagid].SelectedItem as TechnoPair;
                desc = cbbActionAbst.SelectedItem as TriggerDescription;
            }
            TriggerParam param = desc.Parameters[tagid];
            switch (param.ComboType)
            {
                case TriggerParam.ComboContent.SoundNames:
                case TriggerParam.ComboContent.ThemeNames:
                case TriggerParam.ComboContent.EvaNames:

                    if (soundPlayer.IsPlaying)
                    {
                        soundPlayer.Stop();
                    }
                    else
                    {
                        string name = soundPlayer.GetSoundName(p, (SoundType)param.ComboType);
                        soundPlayer.LoadWav(GlobalVar.GlobalSoundBank.GetSound(name));
                        soundPlayer.Play();
                    }
                    break;
                case TriggerParam.ComboContent.Triggers:
                    //TODO: Convert TechnoPair into trigger item
                    break;
            }
        }
        private void SelectTextboxContent_MouseClicked(object sender, MouseEventArgs e)
        {
            ((TextBoxBase)sender).SelectAll();
        }
        private void mtxbEventID_Validated(object sender, EventArgs e)
        {
            int id = int.Parse(mtxbEventID.Text);
            if (id > cbbEventAbst.Items.Count) return;
            if (id != cbbEventAbst.SelectedIndex) cbbEventAbst.SelectedIndex = id;
        }
        private void mtxbActionID_Validated(object sender, EventArgs e)
        {
            int id = int.Parse(mtxbActionID.Text);
            if (id > cbbActionAbst.Items.Count) return;
            if (id != cbbActionAbst.SelectedIndex) cbbActionAbst.SelectedIndex = id;
        }
        private void cbbEventAbst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.Parse(mtxbEventID.Text) != cbbEventAbst.SelectedIndex) mtxbEventID.Text = cbbEventAbst.SelectedIndex.ToString();
            TriggerDescription description = cbbEventAbst.SelectedItem as TriggerDescription;
            LogicItem eventItem = lbxEventList.SelectedItem as LogicItem;
            if (eventItem != null) UpdateEventParams(description, eventItem.Parameters);
        }
        private void cbbActionAbst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.Parse(mtxbActionID.Text) != cbbActionAbst.SelectedIndex) mtxbActionID.Text = cbbActionAbst.SelectedIndex.ToString();
            TriggerDescription description = cbbActionAbst.SelectedItem as TriggerDescription;
            LogicItem actionItem = lbxActionList.SelectedItem as LogicItem;
            if (actionItem != null) UpdateActionParams(description, actionItem.Parameters);
        }
        private void lklTraceTrigger_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lbxTriggerList.SelectedItem = cbbAttatchedTrg.SelectedItem;
        }
        private void lbxTriggerList_SelectedValueChanged(object sender, EventArgs e)
        {
            TriggerItem item = ((ListBox)sender).SelectedItem as TriggerItem;
            UpdateContent(item.ID);
        }
        private void lbxEventList_SelectedValueChanged(object sender, EventArgs e)
        {
            LogicItem item = lbxEventList.SelectedItem as LogicItem;
            UpdateEventContent(item);
            cbbEventAbst_SelectedIndexChanged(cbbEventAbst, e);
        }
        private void lbxActionList_SelectedValueChanged(object sender, EventArgs e)
        {
            LogicItem item = lbxActionList.SelectedItem as LogicItem;
            UpdateActionContent(item);
            cbbActionAbst_SelectedIndexChanged(cbbActionAbst, e);
        }
        #endregion
    }
}
