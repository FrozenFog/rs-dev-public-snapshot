﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using relert_sharp.MapStructure;
using relert_sharp.MapStructure.Logic;
using relert_sharp.FileSystem;
using relert_sharp.Common;
using static relert_sharp.Language;

namespace relert_sharp.SubWindows.LogicEditor
{
    public partial class LogicEditor : Form
    {
        private Map map;
        private DescriptCollection descriptCollection = new DescriptCollection();
        private LinkLabel[] lklEP, lklAP;
        private TextBox[] txbEP, txbAP;
        private CheckBox[] ckbEP, ckbAP;
        private ComboBox[] cbbEP, cbbAP;


        #region Constructor - LogicEditor
        public LogicEditor(Map m)
        {
            InitializeComponent();
            SetLanguage();
            SetGroup();
            map = m;
            UpdateTrgList(TriggerItem.DisplayingType.IDandName);
            LoadHouseList();
            LoadEventComboBox();
            LoadActionComboBox();
            lbxTriggerList.SelectedIndex = 0;
        }
        #endregion


        #region Private Methods - LogicEditor
        private void SetGroup()
        {
            lklEP = new LinkLabel[4] { lklEP1, lklEP2, lklEP3, lklEP4 };
            lklAP = new LinkLabel[4] { lklAP1, lklAP2, lklAP3, lklAP4 };
            txbEP = new TextBox[4] { txbEP1, txbEP2, txbEP3, txbEP4 };
            txbAP = new TextBox[4] { txbAP1, txbAP2, txbAP3, txbAP4 };
            ckbEP = new CheckBox[4] { ckbAP1, ckbAP2, ckbAP3, ckbAP4 };
            ckbAP = new CheckBox[4] { ckbAP1, ckbAP2, ckbAP3, ckbAP4 };
            cbbEP = new ComboBox[4] { cbbEP1, cbbEP2, cbbEP3, cbbEP4 };
            cbbAP = new ComboBox[4] { cbbAP1, cbbAP2, cbbAP3, cbbAP4 };
        }
        private void LoadEventComboBox()
        {
            Utils.Misc.LoadToObjectCollection(ref cbbEventAbst, descriptCollection.Events);
        }
        private void LoadActionComboBox()
        {
            Utils.Misc.LoadToObjectCollection(ref cbbActionAbst, descriptCollection.Actions);
        }
        private void LoadHouseList()
        {
            map.Countries.AscendingSort();
            Utils.Misc.LoadToObjectCollection(ref lbxTriggerHouses, map.Countries);
        }
        private void UpdateTrgList(TriggerItem.DisplayingType type = TriggerItem.DisplayingType.Remain)
        {
            lbxTriggerList.Items.Clear();
            cbbAttatchedTrg.Items.Clear();
            cbbAttatchedTrg.Items.Add(TriggerItem.NullTrigger);
            foreach(TriggerItem trigger in map.Triggers)
            {
                trigger.SetDisplayingString(type);
                lbxTriggerList.Items.Add(trigger);
                cbbAttatchedTrg.Items.Add(trigger);
            }
        }
        private void UpdateEventList(LogicGroup eg)
        {
            Utils.Misc.LoadToObjectCollection(ref lbxEventList, eg);
        }
        private void UpdateActionList(LogicGroup ag)
        {
            Utils.Misc.LoadToObjectCollection(ref lbxActionList, ag);
        }
        private void UpdateTags(string triggerID)
        {
            TriggerItem trg = map.Triggers[triggerID];
            TagItem tg = map.Tags.GetTagFromTrigger(triggerID);

            txbTrgID.Text = triggerID;
            txbTrgName.Text = trg.Name;
            txbTagID.Text = tg.ID;
            txbTagName.Text = tg.Name;
            rdbRepeat0.Checked = false;
            rdbRepeat1.Checked = false;
            rdbRepeat2.Checked = false;
            switch (tg.Repeating)
            {
                case TriggerRepeatingType.NoRepeating:
                    rdbRepeat0.Checked = true;
                    break;
                case TriggerRepeatingType.OneTimeLogicAND:
                    rdbRepeat1.Checked = true;
                    break;
                case TriggerRepeatingType.RepeatLogicOR:
                    rdbRepeat2.Checked = true;
                    break;
            }
            ckbDisabled.Checked = trg.Disabled;
            lbxTriggerHouses.SelectedItem = map.Countries[trg.House];
            if (trg.LinkedWith != "<none>")
            {
                cbbAttatchedTrg.SelectedItem = map.Triggers[trg.LinkedWith];
                lklTraceTrigger.Visible = true;
            }
            else
            {
                cbbAttatchedTrg.Text = "<none>";
                lklTraceTrigger.Visible = false;
            }
            ckbEasy.Checked = trg.EasyOn;
            ckbNormal.Checked = trg.NormalOn;
            ckbHard.Checked = trg.HardOn;
        }
        private void UpdateEvents(string triggerID)
        {
            LogicGroup lg = map.Events[triggerID];
            UpdateEventList(lg);
        }
        private void UpdateEventContent(LogicItem item)
        {
            mtxbEventID.Text = item.ID.ToString();
            txbEventAnno.Text = item.Comment;
            cbbEventAbst.SelectedIndex = item.ID;
        }
        private void UpdateEventParams(TriggerDescription description, string[] eventParams)
        {
            rtxbEventDetail.Refresh();
            rtxbEventDetail.Text = description.Description;
            foreach (Control c in gpbEventParam.Controls)
            {
                c.Visible = false;
                c.Enabled = true;
            }
            if (description.Parameters.Count == 0)
            {
                lblNoParamE.Visible = true;
            }
            else
            {
                int i = 0;
                foreach (TriggerParam param in description.Parameters)
                {
                    switch (param.Type)
                    {
                        case TriggerParam.ParamType.PlainString:
                            SetParamControls(txbEP, param, eventParams, i);
                            break;
                        case TriggerParam.ParamType.SelectableString:
                            SetParamControls(cbbEP, param, eventParams, i);
                            break;
                        case TriggerParam.ParamType.Bool:
                            SetParamControls(ckbEP, param, eventParams, i);
                            break;
                    }
                    SetParamControls(lklEP, param, eventParams, i);
                    i++;
                }
            }
        }
        private void UpdateActionParams(TriggerDescription description, string[] actionParams)
        {
            rtxbActionDetail.Refresh();
            rtxbActionDetail.Text = description.Description;
            foreach (Control c in gpbActionParam.Controls)
            {
                c.Visible = false;
                c.Enabled = true;
            }
            if (description.Parameters.Count == 0)
            {
                lblNoParamA.Visible = true;
            }
            else
            {
                int i = 0;
                foreach (TriggerParam param in description.Parameters)
                {
                    switch (param.Type)
                    {
                        case TriggerParam.ParamType.PlainString:
                            SetParamControls(txbAP, param, actionParams, i);
                            break;
                        case TriggerParam.ParamType.SelectableString:
                            SetParamControls(cbbAP, param, actionParams, i);
                            break;
                        case TriggerParam.ParamType.Bool:
                            SetParamControls(ckbAP, param, actionParams, i);
                            break;
                    }
                    SetParamControls(lklAP, param, actionParams, i);
                    i++;
                }
            }
        }
        private void SetParamControls(Control[] controls, TriggerParam param, string[] paramData, int controlIndex)
        {
            if (controls.GetType() == typeof(LinkLabel[]))
            {
                ((LinkLabel)controls[controlIndex]).Text = param.Name;
                ((LinkLabel)controls[controlIndex]).Visible = true;
                if (!param.Traceable) ((LinkLabel)controls[controlIndex]).Enabled = false;
            }
            else
            {
                if (param.Type == TriggerParam.ParamType.Bool)
                {
                    controls[controlIndex].Visible = true;
                    ((CheckBox)controls[controlIndex]).Checked = param.GetParameter(paramData, true);
                    controls[controlIndex].Tag = param.ParamPos;
                }
                else
                {
                    controls[controlIndex].Visible = true;
                    controls[controlIndex].Text = param.GetParameter(paramData);
                    controls[controlIndex].Tag = param.ParamPos;
                }
            }
        }
        private void UpdateActionContent(LogicItem item)
        {
            mtxbActionID.Text = item.ID.ToString();
            txbActionAnno.Text = item.Comment;
            cbbActionAbst.SelectedIndex = item.ID;
        }
        private void UpdateActions(string triggerID)
        {
            LogicGroup ag = map.Actions[triggerID] as LogicGroup;
            UpdateActionList(ag);
        }
        private void ClearContent(GroupBox gpb)
        {
            foreach (Control c in gpb.Controls)
            {
                var t = c.GetType();
                if (t == typeof(TextBox) ||
                    t == typeof(ComboBox) ||
                    t == typeof(MaskedTextBox) ||
                    t == typeof(RichTextBox)) c.Text = "";
                if (t == typeof(GroupBox)) ClearContent(c as GroupBox);
            }
        }
        private void UpdateContent(string triggerID)
        {
            UpdateTags(triggerID);
            UpdateEvents(triggerID);
            if (lbxEventList.Items.Count > 0) lbxEventList.SelectedIndex = 0;
            else
            {
                ClearContent(gpbEvents);
                mtxbEventID.Text = "00";
            }
            UpdateActions(triggerID);
            if (lbxActionList.Items.Count > 0) lbxActionList.SelectedIndex = 0;
            else
            {
                ClearContent(gpbActions);
                mtxbActionID.Text = "00";
            }
        }

        #region Languages
        private void SetLanguage()
        {
            foreach (TabPage p in tbcMain.TabPages)
            {
                foreach (Control c in p.Controls)
                {
                    SetControlLanguage(c);
                }
                p.Text = DICT[p.Text];
            }
            Text = DICT[Text];
            lblNoParamE.Location = new Point(gpbEventParam.Size.Width / 2 - lblNoParamE.Size.Width / 2, gpbEventParam.Size.Height / 2 - lblNoParamE.Size.Height);
            lblNoParamA.Location = new Point(gpbActionParam.Size.Width / 2 - lblNoParamA.Size.Width / 2, gpbActionParam.Size.Height / 2 - lblNoParamA.Size.Height);
        }
        private void SetControlLanguage(Control parent)
        {
            var t = parent.GetType();
            if (t == typeof(TextBox)) return;
            if (t == typeof(GroupBox))
            {
                foreach (Control c in ((GroupBox)parent).Controls)
                {
                    SetControlLanguage(c);
                }
            }
            if (parent.ContextMenuStrip != null)
            {
                foreach (ToolStripItem item in parent.ContextMenuStrip.Items)
                {
                    item.Text = DICT[item.Text];
                }
            }
            if (!string.IsNullOrEmpty(ttTrg.GetToolTip(parent)))
            {
                ttTrg.SetToolTip(parent, DICT[ttTrg.GetToolTip(parent)]);
            }
            parent.Text = DICT[parent.Text];
        }


        #endregion

        #endregion
    }
}
