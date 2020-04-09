using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using RelertSharp.MapStructure.Logic;
using RelertSharp.FileSystem;
using RelertSharp.Common;
using RelertSharp.IniSystem;
using static RelertSharp.Language;

namespace RelertSharp.SubWindows.LogicEditor
{
    public partial class LogicEditor : Form
    {
        #region Trigger Page

        #region MenuStrip
        private void tsmiEditTemp_Click(object sender, EventArgs e)
        {
            lbxTriggerList.Enabled = false;
            btnNewTrigger.Enabled = false;
            btnDelTrigger.Enabled = false;
            btnCopyTrigger.Enabled = false;
            cbbAttatchedTrg.Enabled = false;
            cbbCustomGroup.Enabled = false;
            btnSaveTemp.Visible = true;
            UpdateContent(map.Triggers.TemplateTrigger);
        }
        private void tsmiCopyEventAdv_Click(object sender, EventArgs e)
        {
            dlgCopy d = new dlgCopy();
            if (d.ShowDialog() == DialogResult.OK)
            {
                _CurrentTrigger.Events.Multiply(d.Result.Split(new char[] { '\n' }), _CurrentEvent, _CurrentEventParameters);
                UpdateAt(lbxTriggerList, _CurrentTrigger);
                UpdateContent(_CurrentBoxTrigger);
            }
        }
        private void tsmiCopyActionAdv_Click(object sender, EventArgs e)
        {
            dlgCopy d = new dlgCopy();
            if (d.ShowDialog() == DialogResult.OK)
            {
                _CurrentTrigger.Actions.Multiply(d.Result.Split(new char[] { '\n' }), _CurrentAction, _CurrentActionParameters);
                UpdateAt(lbxTriggerList, _CurrentTrigger);
                UpdateContent(_CurrentBoxTrigger);
            }
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
        #endregion

        #region btn
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txbSearchName.Text;
            lvSearchResult.Items.Clear();
            if (keyword == DICT["LGClblFakeSearch"]) return;
            _SearchResult.SetKeyword(keyword);

            if (ckbTrigger.Checked) LoadSearch(map.Triggers, SearchItem.SearchType.LGCckbTrig);
            if (ckbTag.Checked) LoadSearch(map.Tags, SearchItem.SearchType.LGCckbTag);
            if (ckbLocal.Checked) LoadSearch(map.LocalVariables, SearchItem.SearchType.LGCckbLocal);
            if (ckbTeam.Checked) LoadSearch(map.Teams, SearchItem.SearchType.LGCckbTeam);
            if (ckbTaskForce.Checked) LoadSearch(map.TaskForces, SearchItem.SearchType.LGCckbTF);
            if (ckbScript.Checked) LoadSearch(map.Scripts, SearchItem.SearchType.LGCckbTScp);
            if (ckbAiTrigger.Checked) LoadSearch(map.AiTriggers, SearchItem.SearchType.LGCckbAiTrg);
            if (ckbHouse.Checked) LoadSearch(map.Countries, SearchItem.SearchType.LGCckbHouse);
            if (ckbCsf.Checked) LoadSearch(GlobalVar.GlobalCsf, SearchItem.SearchType.LGCckbCsf);
            if (ckbTechno.Checked) LoadSearch(GlobalVar.GlobalRules.TechnoList, SearchItem.SearchType.LGCckbTechno);
            if (ckbSound.Checked) LoadSearch(GlobalVar.GlobalSound.SoundList, SearchItem.SearchType.LGCckbSnd);
            if (ckbEva.Checked) LoadSearch(GlobalVar.GlobalSound.EvaList, SearchItem.SearchType.LGCckbEva);
            if (ckbTheme.Checked) LoadSearch(GlobalVar.GlobalSound.ThemeList, SearchItem.SearchType.LGCckbMus);
            if (ckbAnim.Checked) LoadSearch(GlobalVar.GlobalRules.AnimationList, SearchItem.SearchType.LGCckbAnim);
            if (ckbSuper.Checked) LoadSearch(GlobalVar.GlobalRules.SuperWeaponList, SearchItem.SearchType.LGCckbSuper);
            if (ckbGlobal.Checked) LoadSearch(GlobalVar.GlobalRules.GlobalVar, SearchItem.SearchType.LGCckbGlobal);
            lblSearchResult.Text = string.Format(DICT["LGClblSearchNum"], _SearchResult.Length);
        }
        private void LoadSearch(IEnumerable<IRegistable> src, SearchItem.SearchType type)
        {
            StaticHelper.LoadToObjectCollection(lvSearchResult, _SearchResult.SearchIn(src, type));
        }

        private void btnNewEvent_Click(object sender, EventArgs e)
        {
            LogicItem ev = _CurrentTrigger.Events.NewEvent();
            lbxEventList.Items.Add(ev);
            lbxEventList.SelectedItem = ev;
            mtxbEventID.Focus();
            mtxbEventID.SelectAll();
        }

        private void btnNewAction_Click(object sender, EventArgs e)
        {
            string tid = txbTrgID.Text;
            LogicItem ac = map.Triggers[tid].Actions.NewAction();
            lbxActionList.Items.Add(ac);
            lbxActionList.SelectedItem = ac;
            mtxbActionID.Focus();
            mtxbActionID.SelectAll();
        }

        private void btnSaveTemp_Click(object sender, EventArgs e)
        {
            UpdateContent(lbxTriggerList.SelectedItem as TriggerItem);
            lbxTriggerList.Enabled = true;
            btnNewTrigger.Enabled = true;
            btnDelTrigger.Enabled = true;
            btnCopyTrigger.Enabled = true;
            cbbAttatchedTrg.Enabled = true;
            cbbCustomGroup.Enabled = true;
            btnSaveTemp.Visible = false;
        }

        private void btnNewTrigger_Click(object sender, EventArgs e)
        {
            TriggerItem newtrigger = map.NewTrigger();
            lbxTriggerList.Items.Add(newtrigger);
            cbbAttatchedTrg.Items.Add(newtrigger);
            lbxTriggerList.SelectedItem = newtrigger;
        }

        private void btnCopyTrigger_Click(object sender, EventArgs e)
        {
            TriggerItem copytrigger = Utils.Misc.MemCpy(_CurrentTrigger);
            copytrigger.ID = map.NewID;
            copytrigger.Name += " - Copy";
            copytrigger.SetDisplayingString(lbxTriggerList.Tag);
            TagItem newtag = new TagItem(copytrigger, map.NewID);
            map.Triggers[copytrigger.ID] = copytrigger;
            map.Tags[newtag.ID] = newtag;
            lbxTriggerList.Items.Add(copytrigger);
            cbbAttatchedTrg.Items.Add(copytrigger);
            lbxTriggerList.SelectedItem = copytrigger;
        }

        private void btnDelTrigger_Click(object sender, EventArgs e)
        {
            int _i = lbxTriggerList.SelectedIndex;
            TriggerItem t = _CurrentBoxTrigger;
            map.Triggers.Remove(t);
            map.Tags.Remove(map.Tags.GetTagFromTrigger(t.ID), t.ID);
            lbxTriggerList.Items.Remove(t);
            cbbAttatchedTrg.Items.Remove(t);
            lbxTriggerList.SelectedIndex = _i == 0 ? 0 : _i - 1;
        }
        private void btnDeleteEvent_Click(object sender, EventArgs e)
        {
            int _i = lbxEventList.SelectedIndex;
            _CurrentTrigger.Events.Remove(_i);
            RemoveAt(lbxEventList, _i);
        }
        private void btnDeleteAction_Click(object sender, EventArgs e)
        {
            int _i = lbxActionList.SelectedIndex;
            _CurrentTrigger.Actions.Remove(_i);
            RemoveAt(lbxActionList, _i);
        }
        #endregion
        #region txb
        private void txbSearchName_KeyDown(object sender, KeyEventArgs e)
        {
            GoEnter(e, () => { btnSearch_Click(null, null); });
        }

        private void txbSearchName_Enter(object sender, EventArgs e)
        {
            if (txbSearchName.Text == DICT["LGClblFakeSearch"])
            {
                txbSearchName.Text = "";
                txbSearchName.ForeColor = Color.Black;
            }
        }

        private void txbSearchName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txbSearchName.Text))
            {
                txbSearchName.Text = DICT["LGClblFakeSearch"];
                txbSearchName.ForeColor = SystemColors.GrayText;
            }
        }

        private void SelectTextboxContent_MouseClicked(object sender, MouseEventArgs e)
        {
            ((TextBoxBase)sender).SelectAll();
        }

        private void mtxbEventID_TextChanged(object sender, EventArgs e)
        {
            if (!cbbEventAbstChanging)
            {
                if (string.IsNullOrEmpty(mtxbEventID.Text)) return;
                cbbEventAbst.SelectedIndex = int.Parse(mtxbEventID.Text);
            }
        }

        private void mtxbActionID_TextChanged(object sender, EventArgs e)
        {
            if (!cbbActionAbstChanging)
            {
                if (string.IsNullOrEmpty(mtxbActionID.Text)) return;
                cbbActionAbst.SelectedIndex = int.Parse(mtxbActionID.Text);
            }
        }

        private void txbTrgName_Validated(object sender, EventArgs e)
        {
            _CurrentTrigger.Name = txbTrgName.Text;
            if (btnSaveTemp.Visible) return;
            _CurrentTrigger.SetDisplayingString(lbxTriggerList.Tag);
            if (_CurrentTag != null && _CurrentTag.Binded)
            {
                _CurrentTag.Name = _CurrentTrigger.Name + " - Tag";
                txbTagName.Text = _CurrentTag.Name;
            }
            UpdateAt(lbxTriggerList, _CurrentTrigger);
        }
        #endregion
        #region cbb
        private void cbbAttatchedTrg_SelectedValueChanged(object sender, EventArgs e)
        {
            TriggerItem asso = cbbAttatchedTrg.SelectedItem as TriggerItem;
            if (asso != null && asso.ID != _CurrentTrigger.LinkedWith)
            {
                _CurrentTrigger.LinkedWith = asso.ID;
                UpdateAt(lbxTriggerList, _CurrentTrigger);
            }
        }

        private bool cbbEventAbstChanging = false;
        private void cbbEventAbst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxEventList.Items.Count == 0) return;
            cbbEventAbstChanging = true;
            int evid = cbbEventAbst.SelectedIndex;
            if (evid != int.Parse(mtxbEventID.Text)) mtxbEventID.Text = cbbEventAbst.SelectedIndex.ToString();
            TriggerDescription description = cbbEventAbst.SelectedItem as TriggerDescription;
            LogicItem eventItem = lbxEventList.SelectedItem as LogicItem;
            if (_CurrentEvent.ID != evid)
            {
                _CurrentEvent.ID = evid;
                _CurrentEvent.Parameters = description.InitParams;
                UpdateAt(lbxEventList, _CurrentEvent);
            }
            if (eventItem != null) UpdateEventParams(description, eventItem.Parameters);
            cbbEventAbstChanging = false;
        }

        private bool cbbActionAbstChanging = false;
        private void cbbActionAbst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxActionList.Items.Count == 0) return;
            cbbActionAbstChanging = true;
            int acid = cbbActionAbst.SelectedIndex;
            if (acid != int.Parse(mtxbActionID.Text)) mtxbActionID.Text = cbbActionAbst.SelectedIndex.ToString();
            TriggerDescription description = cbbActionAbst.SelectedItem as TriggerDescription;
            LogicItem actionItem = lbxActionList.SelectedItem as LogicItem;
            if (_CurrentAction.ID != acid)
            {
                _CurrentAction.ID = acid;
                _CurrentAction.Parameters = description.InitParams;
                UpdateAt(lbxActionList, _CurrentAction);
            }
            if (actionItem != null) UpdateActionParams(description, actionItem.Parameters);
            cbbActionAbstChanging = false;
        }

        #endregion
        #region lbx
        private void lbxTriggerHouses_SelectedIndexChanged(object sender, EventArgs e)
        {
            string house = (lbxTriggerHouses.SelectedItem as CountryItem)?.Name;
            _CurrentTrigger.House = house;
        }

        private void lbxTriggerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!listUpdating)
            {
                TriggerItem item = ((ListBox)sender).SelectedItem as TriggerItem;
                if (item != null) UpdateContent(item);
            }
        }

        private void lbxEventList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!cbbEventAbstChanging && !listUpdating)
            {
                LogicItem item = lbxEventList.SelectedItem as LogicItem;
                UpdateEventContent(item);
                cbbEventAbst_SelectedIndexChanged(cbbEventAbst, e);
            }
        }

        private void lbxActionList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!cbbActionAbstChanging && !listUpdating)
            {
                LogicItem item = lbxActionList.SelectedItem as LogicItem;
                UpdateActionContent(item);
                cbbActionAbst_SelectedIndexChanged(cbbActionAbst, e);
            }
        }
        #endregion
        private void lvSearchResult_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvSearchResult.BeginUpdate();
            lvSearchResult.Items.Clear();
            lvSearchResult.Items.AddRange(_SearchResult.SortBy(e.Column));
            lvSearchResult.EndUpdate();
        }
        private void EventParamChanged(object sender, EventArgs e)
        {
            if (cbbEventAbstChanging) return;
            ParamChanged(sender, LogicType.EventLogic);
        }
        private void ActionParamChanged(object sender, EventArgs e)
        {
            if (cbbActionAbstChanging) return;
            ParamChanged(sender, LogicType.ActionLogic);
        }

        private void ckb_CheckedChanged(object sender, EventArgs e)
        {
            bool stat = sender.GetType() == typeof(CheckBox) ? ((CheckBox)sender).Checked : ((RadioButton)sender).Checked;
            string tag = ((Control)sender).Tag.ToString();
            switch (tag)
            {
                case "e":
                    _CurrentTrigger.EasyOn = stat;
                    break;
                case "n":
                    _CurrentTrigger.NormalOn = stat;
                    break;
                case "h":
                    _CurrentTrigger.HardOn = stat;
                    break;
                case "d":
                    _CurrentTrigger.Disabled = stat;
                    break;
                case "0":
                case "1":
                case "2":
                    if (stat)
                        _CurrentTrigger.Repeating = (TriggerRepeatingType)int.Parse(tag);
                    break;
            }
        }

        private void lklParams_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ((LinkLabel)sender).UseWaitCursor = true;
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
                    UseWaitCursor = true;
                    ManageSound(param, p);
                    break;
                case TriggerParam.ComboContent.Triggers:
                    string triggerid = p.Index;
                    TriggerItem trigger = map.Triggers[triggerid];
                    lbxTriggerList.SelectedItem = trigger;
                    break;
            }
        }

        private void lklTraceTrigger_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lbxTriggerList.SelectedItem = cbbAttatchedTrg.SelectedItem;
        }

        #endregion

        #region Team Page
        #region btn
        private void btnAddTaskMem_Click(object sender, EventArgs e)
        {

        }

        private void btnDelTaskMem_Click(object sender, EventArgs e)
        {

        }

        private void btnCopyTaskMem_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region cbb
        private void cbbTaskCurType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxTaskList.SelectedItem == null || cbbTaskCurType.SelectedItem == null) return;
            if (lbxTaskList.SelectedItem == null || lbxTaskMemList.SelectedItem == null) return;
            TaskforceShowItem showItem = lbxTaskMemList.SelectedItem as TaskforceShowItem;
            TaskforceItem taskforce = lbxTaskList.SelectedItem as TaskforceItem;
            TechnoPair techno = cbbTaskCurType.SelectedItem as TechnoPair;
            int index = lbxTaskMemList.SelectedIndex;
            taskforce.MemberData[index] = new Tuple<string, int>(techno.RegName, showItem.Number);
            lbxTaskList.BeginUpdate();
            List<TaskforceShowItem> memList = new List<TaskforceShowItem>();
            foreach (var i in taskforce.MemberData) memList.Add(new TaskforceShowItem(i));
            StaticHelper.LoadToObjectCollection(lbxTaskMemList, memList);
            lbxTaskMemList.SelectedIndex = index;
            lbxTaskList.EndUpdate();
        }
        #endregion
        #region txb
        private void txbTaskGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex
                (
                    txbTaskGroup.SelectionStart==0&&txbTaskGroup.SelectionLength>0
                    ? @"^[0-9-\b]*$" 
                    : @"^[0-9\b]*$"
                );
            e.Handled = !re.IsMatch(e.KeyChar.ToString());
        }
        private void txbTaskCurNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(@"^[0-9-\b]*$");
            e.Handled = !re.IsMatch(e.KeyChar.ToString());
        }
        private void txbTaskCurNum_TextChanged(object sender, EventArgs e)
        {
            if (lbxTaskList.SelectedItem == null || lbxTaskMemList.SelectedItem == null) return;
            TaskforceShowItem showItem = lbxTaskMemList.SelectedItem as TaskforceShowItem;
            TaskforceItem taskforce = lbxTaskList.SelectedItem as TaskforceItem;
            int findIndex = taskforce.MemberData.FindIndex(s => s.Item1 == showItem.RegName);
            taskforce.MemberData[findIndex]
                = new Tuple<string, int>(taskforce.MemberData[findIndex].Item1, int.Parse(txbTaskCurNum.Text));
            lbxTaskList.BeginUpdate();
            int index = lbxTaskMemList.SelectedIndex;
            List<TaskforceShowItem> memList = new List<TaskforceShowItem>();
            foreach (var i in taskforce.MemberData) memList.Add(new TaskforceShowItem(i));
            StaticHelper.LoadToObjectCollection(lbxTaskMemList, memList);
            lbxTaskMemList.SelectedIndex = index;
            lbxTaskList.EndUpdate();
        }

        private void txbTaskName_TextChanged(object sender, EventArgs e)
        {
            if (lbxTaskList.SelectedItem == null) return;
            TaskforceItem taskforce = lbxTaskList.SelectedItem as TaskforceItem;
            if (!string.IsNullOrEmpty(txbTaskGroup.Text))
            {
                taskforce.Name = txbTaskName.Text;
                lbxTaskList.BeginUpdate();
                int index = lbxTaskList.SelectedIndex;
                StaticHelper.LoadToObjectCollection(lbxTaskList, map.TaskForces);
                lbxTaskList.SelectedIndex = index;
                lbxTaskList.EndUpdate();
            }
                
        }

        private void txbTaskGroup_TextChanged(object sender, EventArgs e)
        {
            if (lbxTaskList.SelectedItem == null) return;
            TaskforceItem taskforce = lbxTaskList.SelectedItem as TaskforceItem;
            if (!string.IsNullOrEmpty(txbTaskGroup.Text) && txbTaskGroup.Text.Trim() != "-") 
                taskforce.Group = int.Parse(txbTaskGroup.Text);
        }
        #endregion
        #region lbx
        private void lbxTaskList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxTaskList.SelectedItem == null) return;
            UpdateTaskforceContent();
        }
        private void lbxTaskMemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((lbxTaskList.SelectedItem == null || lbxTaskMemList.SelectedItem == null)) return;
            TaskforceShowItem curItem = lbxTaskMemList.SelectedItem as TaskforceShowItem;
            cbbTaskCurType.Text = curItem.RegName;
            txbTaskCurNum.Text = curItem.Number.ToString();
        }
        #endregion
        #endregion

        #region Misc Page

        #region Button
        private void btnNewLocalVar_Click(object sender, EventArgs e)
        {
            LocalVarItem localVar = new LocalVarItem("New Local Variable", false, localVarList.Count);
            localVarList.Add(localVar);
            localVarSource.DataSource = null;
            localVarSource.DataSource = localVarList; 
            chklbxLocalVar.SelectedIndex = chklbxLocalVar.Items.Count - 1;
            for (int idx = 0, count = localVarList.Count; idx < count; ++idx)
                chklbxLocalVar.SetItemChecked(idx, localVarList[idx].InitState);
        }
        #endregion

        private void chklbxLocalVar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (localVarList == null) return;
            txbLocalName.Text = localVarList[chklbxLocalVar.SelectedIndex].Name;
            return;
        }

        private void txbLocalName_TextChanged(object sender, EventArgs e)
        {
            localVarList[chklbxLocalVar.SelectedIndex].Name = txbLocalName.Text;
            return;
        }

        private void chklbxLocalVar_Leave(object sender, EventArgs e)
        {
            for (int idx = 0, count = localVarList.Count; idx < count; ++idx)
                localVarList[idx].InitState = chklbxLocalVar.GetItemChecked(idx);
            map.LocalVariables.UpdateData(localVarList);
            ;
        }

        #endregion
    }
}
