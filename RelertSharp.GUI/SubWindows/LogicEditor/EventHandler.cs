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
using BrightIdeasSoftware;
using RelertSharp.SubWindows.LogicEditor;

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
                UpdateAt(lbxTriggerList, _CurrentTrigger, ref updatingLbxTriggerList);
                UpdateContent(_CurrentBoxTrigger);
            }
        }
        private void tsmiCopyActionAdv_Click(object sender, EventArgs e)
        {
            dlgCopy d = new dlgCopy();
            if (d.ShowDialog() == DialogResult.OK)
            {
                _CurrentTrigger.Actions.Multiply(d.Result.Split(new char[] { '\n' }), _CurrentAction, _CurrentActionParameters);
                UpdateAt(lbxTriggerList, _CurrentTrigger, ref updatingLbxTriggerList);
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
            UpdateAt(lbxTriggerList, _CurrentTrigger, ref updatingLbxTriggerList);
        }

        private void btnNewAction_Click(object sender, EventArgs e)
        {
            string tid = txbTrgID.Text;
            LogicItem ac = map.Triggers[tid].Actions.NewAction();
            lbxActionList.Items.Add(ac);
            lbxActionList.SelectedItem = ac;
            mtxbActionID.Focus();
            mtxbActionID.SelectAll();
            UpdateAt(lbxTriggerList, _CurrentTrigger, ref updatingLbxTriggerList);
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
            copytrigger.Name += " Clone";
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
            var tags = map.Tags.GetTagFromTrigger(t.ID);
            foreach (var i in tags)
                map.DelID(i.ID);
            map.Triggers.RemoveTrigger(t);
            foreach (var i in tags)
                map.Tags.Remove(i, t.ID);
            lbxTriggerList.Items.Remove(t);
            cbbAttatchedTrg.Items.Remove(t);
            lbxTriggerList.SelectedIndex = _i == 0 ? 0 : _i - 1;
        }
        private void btnDeleteEvent_Click(object sender, EventArgs e)
        {
            LogicItem ev = lbxEventList.SelectedItem as LogicItem;
            _CurrentTrigger.Events.Remove(ev);
            RemoveAt(lbxEventList, lbxEventList.SelectedIndex, ref updatingLbxEventList);
        }
        private void btnDeleteAction_Click(object sender, EventArgs e)
        {
            LogicItem ac = lbxActionList.SelectedItem as LogicItem;
            _CurrentTrigger.Actions.Remove(ac);
            RemoveAt(lbxActionList, lbxActionList.SelectedIndex, ref updatingLbxActionList);
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
            if (!updatingCbbEventAbst)
            {
                if (string.IsNullOrEmpty(mtxbEventID.Text)) return;
                cbbEventAbst.SelectedIndex = int.Parse(mtxbEventID.Text);
            }
        }

        private void mtxbActionID_TextChanged(object sender, EventArgs e)
        {
            if (!updatingCbbActionAbst)
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
            UpdateAt(lbxTriggerList, _CurrentTrigger, ref updatingLbxTriggerList);
        }
        #endregion
        #region cbb
        private void cbbTagID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbTagID.SelectedItem == null) return;
            string id = cbbTagID.SelectedItem as string;
            TagItem tg = GlobalVar.CurrentMapDocument.Map.Tags[id];
            txbTagName.Text = tg.Name;
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
        }

        private bool updatingCbbAttatchedTrg = false;
        private void cbbAttatchedTrg_SelectedValueChanged(object sender, EventArgs e)
        {
            TriggerItem asso = cbbAttatchedTrg.SelectedItem as TriggerItem;
            if (asso != null && asso.ID != _CurrentTrigger.LinkedWith)
            {
                _CurrentTrigger.LinkedWith = asso.ID;
                UpdateAt(lbxTriggerList, _CurrentTrigger, ref updatingLbxTriggerList);
            }
        }

        private bool updatingCbbEventAbst = false;
        private void cbbEventAbst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxEventList.Items.Count == 0) return;
            int evid = cbbEventAbst.SelectedIndex;
            if (evid != int.Parse(mtxbEventID.Text)) mtxbEventID.Text = cbbEventAbst.SelectedIndex.ToString();
            TriggerDescription description = cbbEventAbst.SelectedItem as TriggerDescription;
            LogicItem eventItem = lbxEventList.SelectedItem as LogicItem;
            if (_CurrentEvent.ID != evid)
            {
                _CurrentEvent.ID = evid;
                _CurrentEvent.Parameters = description.InitParams;
                UpdateAt(lbxEventList, _CurrentEvent, ref updatingLbxEventList);
            }
            if (eventItem != null) UpdateEventParams(description, eventItem.Parameters);
        }

        private bool updatingCbbActionAbst = false;
        private void cbbActionAbst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxActionList.Items.Count == 0) return;
            int acid = cbbActionAbst.SelectedIndex;
            if (acid != int.Parse(mtxbActionID.Text)) mtxbActionID.Text = cbbActionAbst.SelectedIndex.ToString();
            TriggerDescription description = cbbActionAbst.SelectedItem as TriggerDescription;
            LogicItem actionItem = lbxActionList.SelectedItem as LogicItem;
            if (_CurrentAction.ID != acid)
            {
                _CurrentAction.ID = acid;
                _CurrentAction.Parameters = description.InitParams;
                UpdateAt(lbxActionList, _CurrentAction, ref updatingLbxActionList);
            }
            if (actionItem != null) UpdateActionParams(description, actionItem.Parameters);
        }

        #endregion
        #region lbx
        private void lbxTriggerHouses_SelectedIndexChanged(object sender, EventArgs e)
        {
            string house = (lbxTriggerHouses.SelectedItem as CountryItem)?.Name;
            _CurrentTrigger.House = house;
        }

        private bool updatingLbxTriggerList = false;
        private void lbxTriggerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!updatingLbxTriggerList)
            {
                TriggerItem item = ((ListBox)sender).SelectedItem as TriggerItem;
                if (item != null) UpdateContent(item);
            }
        }

        private bool updatingLbxEventList = false;
        private void lbxEventList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!updatingLbxEventList)
            {
                LogicItem item = lbxEventList.SelectedItem as LogicItem;
                UpdateEventContent(item);
                cbbEventAbst_SelectedIndexChanged(cbbEventAbst, e);
            }
        }

        private bool updatingLbxActionList = false;
        private void lbxActionList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!updatingLbxActionList)
            {
                LogicItem item = lbxActionList.SelectedItem as LogicItem;
                UpdateActionContent(item);
                cbbActionAbst_SelectedIndexChanged(cbbActionAbst, e);
            }
        }
        #endregion

        #region etc
        private void lvSearchResult_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvSearchResult.BeginUpdate();
            lvSearchResult.Items.Clear();
            lvSearchResult.Items.AddRange(_SearchResult.SortBy(e.Column));
            lvSearchResult.EndUpdate();
        }
        private void EventParamChanged(object sender, EventArgs e)
        {
            ParamChanged(sender, LogicType.EventLogic, ref updatingLbxEventList);
            UpdateAt(lbxTriggerList, _CurrentTrigger, ref updatingLbxTriggerList);
        }
        private void ActionParamChanged(object sender, EventArgs e)
        {
            ParamChanged(sender, LogicType.ActionLogic, ref updatingLbxActionList);
            UpdateAt(lbxTriggerList, _CurrentTrigger, ref updatingLbxTriggerList);
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

        #endregion

        #region Team Page
        #region Taskforce
        #region btn
        private void btnNewTask_Click(object sender, EventArgs e)
        {
            TaskforceItem item = map.NewTaskforce();
            AddTo(lbxTaskList, item, ref updatingLbxTaskList);
        }

        private void btnDelTask_Click(object sender, EventArgs e)
        {
            if (lbxTaskList.SelectedItem == null) return;
            int index = lbxTaskList.SelectedIndex;
            TaskforceItem item = lbxTaskList.SelectedItem as TaskforceItem;
            map.RemoveTaskforce(item);
            RemoveAt(lbxTaskList, index, ref updatingLbxTaskList);
        }

        private void btnCopyTask_Click(object sender, EventArgs e)
        {
            if (lbxTaskList.SelectedItem == null) return;
            TaskforceItem copied = _CurrentTaskforce.Copy(map.NewID);
            map.TaskForces[copied.ID] = copied;
            AddTo(lbxTaskList, copied, ref updatingLbxTaskList);
        }

        private void btnAddTaskMem_Click(object sender, EventArgs e)
        {
            if (lbxTaskList.SelectedItem == null) return;
            TechnoPair p = cbbTaskType.Items[0] as TechnoPair;
            TaskforceUnit u = _CurrentTaskforce.NewUnitItem(p.RegName, 1);
            UpdateTaskforceContent(_CurrentTaskforce.Members.Count - 1);
        }

        private void btnDelTaskMem_Click(object sender, EventArgs e)
        {
            if (lbxTaskList.SelectedItem == null || lvTaskforceUnits.SelectedIndices.Count < 1) return;
            int index = lvTaskforceUnits.SelectedIndices[0];
            _CurrentTaskforce.Members.RemoveAt(lvTaskforceUnits.SelectedIndices[0]);
            RemoveAt(lvTaskforceUnits, index, ref updatingLvTaskforceUnits);
        }

        private void btnCopyTaskMem_Click(object sender, EventArgs e)
        {
            if (lbxTaskList.SelectedItem == null || lvTaskforceUnits.SelectedIndices.Count < 1) return;
            TechnoPair p = cbbTaskType.SelectedItem as TechnoPair;
            TaskforceUnit u = TaskforceUnit.FromListviewItem(lvTaskforceUnits.SelectedItems[0]);
            TaskforceUnit newunit = _CurrentTaskforce.NewUnitItem(u.RegName, u.UnitNum);
            int index = lvTaskforceUnits.Items.Count;
            UpdateTaskforceContent(index);
        }
        #endregion
        #region cbb
        private void cbbTaskCurType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvTaskforceUnits.SelectedItems.Count < 1) return;
            int index = lvTaskforceUnits.SelectedIndices[0];
            TechnoPair selectedunit = cbbTaskType.SelectedItem as TechnoPair;
            TaskforceUnit unit = _CurrentBoxTaskforceUnit;
            unit.RegName = selectedunit.RegName;
            _CurrentTaskforce.Members[index] = unit;
            UpdateTaskforceContent(index);
        }
        #endregion
        #region txb
        private void txbTaskName_Validated(object sender, EventArgs e)
        {
            _CurrentTaskforce.Name = txbTaskName.Text;
            UpdateAt(lbxTaskList, _CurrentTaskforce, ref updatingLbxTaskList);
        }
        private void mtxbTaskGroup_Validated(object sender, EventArgs e)
        {
            if (lbxTaskList.SelectedItem == null) return;
            int group;
            try
            {
                group = int.Parse(mtxbTaskGroup.Text);
            }
            catch
            {
                group = -1;
                mtxbTaskGroup.Text = "-1";
            }
            _CurrentTaskforce.Group = group;
        }
        private void mtxbTaskNum_Validated(object sender, EventArgs e)
        {
            _CurrentTaskforceUnit.UnitNum = int.Parse(mtxbTaskNum.Text);
            UpdateTaskforceContent(lvTaskforceUnits.SelectedIndices[0]);
        }
        #endregion
        #region lbx
        private bool updatingLbxTaskList = false;
        private void lbxTaskList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxTaskList.SelectedItem == null) return;
            if (!updatingLbxTaskList)
            {
                UpdateTaskforceContent(0);
            }
        }
        private bool updatingLvTaskforceUnits = false;
        private void lvTaskforceUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvTaskforceUnits.SelectedItems.Count < 1) return;
            if (!updatingLvTaskforceUnits)
            {
                TaskforceUnit unit = _CurrentBoxTaskforceUnit;
                cbbTaskType.Text = unit.RegName;
                mtxbTaskNum.Text = unit.UnitNum.ToString();
            }
        }
        #endregion
        #endregion
        #region Script
        #region lbx
        private void lbxScriptList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxScriptList.SelectedItem == null) return;
            TeamScriptGroup scriptGroup = lbxScriptList.SelectedItem as TeamScriptGroup;
            txbScriptName.Text = scriptGroup.Name;
            StaticHelper.LoadToObjectCollection(lbxScriptMemList, scriptGroup.Data);
        }

        private void lbxScriptMemList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion
        #endregion
        #region Team
        #region btn
        private void btnNewTeam_Click(object sender, EventArgs e)
        {
            TeamItem item = map.NewTeam();
            AddTo(lbxTeamList, item, ref updatingLbxTeamList);
        }

        private void btnDelTeam_Click(object sender, EventArgs e)
        {
            if (lbxTeamList.SelectedItem == null) return;
            int index = lbxTeamList.SelectedIndex;
            TeamItem item = lbxTeamList.SelectedItem as TeamItem;
            map.RemoveTeam(item);
            RemoveAt(lbxTeamList, index, ref updatingLbxTeamList);
        }

        private void btnCopyTeam_Click(object sender, EventArgs e)
        {
            if (lbxTeamList.SelectedItem == null) return;
            TeamItem copied = _CurrentTeam.Copy(map.NewID);
            map.Teams[copied.ID] = copied;
            AddTo(lbxTeamList, copied, ref updatingLbxTeamList);
        }
        #endregion
        #region lbx
        private bool updatingLbxTeamList = false;
        private void lbxTeamList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxTeamList.SelectedItem == null) return;
            if (!updatingLbxTeamList)
            {
                olvTeamConfig.ClearObjects();
                olvTeamConfig.SetObjects(_CurrentTeamUnit.Data);
            }
        }
        #endregion
        #region olv
        private void olvTeamConfig_CellEditStarting(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            if (e.SubItemIndex != 1) return;
            KeyValuePair<string, TeamUnit.TeamUnitNode> valuePair = (KeyValuePair<string, TeamUnit.TeamUnitNode>)e.RowObject;
            ComboBox cmb = new ComboBox();
            if (e.Control.GetType().FullName == "BrightIdeasSoftware.BooleanCellEditor2")
            {
                cmb.FlatStyle = FlatStyle.Flat;
                cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                cmb.Bounds = e.CellBounds;
                cmb.Items.Add("0 False");
                cmb.Items.Add("1 True");
                cmb.SelectedIndex = ((bool)e.Value == false) ? 0 : 1;
                e.Control = cmb;
            }
            else if (e.Control.GetType().FullName == "BrightIdeasSoftware.EnumCellEditor")
            {
                cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                cmb.FlatStyle = FlatStyle.Flat;
                cmb.Bounds = e.CellBounds;
                switch (valuePair.Key)
                {
                    case "VeteranLevel":
                        cmb.Items.Add(DICT["LGColvEnumTeamVeteran1"]);
                        cmb.Items.Add(DICT["LGColvEnumTeamVeteran2"]);
                        cmb.Items.Add(DICT["LGColvEnumTeamVeteran3"]);
                        cmb.SelectedIndex = (int)valuePair.Value.Value - 1;
                        break;
                    case "MCDecision":
                        cmb.Items.Add(DICT["LGColvEnumTeamMCD0"]);
                        cmb.Items.Add(DICT["LGColvEnumTeamMCD1"]);
                        cmb.Items.Add(DICT["LGColvEnumTeamMCD2"]);
                        cmb.Items.Add(DICT["LGColvEnumTeamMCD3"]);
                        cmb.Items.Add(DICT["LGColvEnumTeamMCD4"]);
                        cmb.Items.Add(DICT["LGColvEnumTeamMCD5"]);
                        cmb.SelectedIndex = (int)valuePair.Value.Value;
                        break;
                }
                e.Control = cmb;
            }
            else if (valuePair.Key == "Waypoint")
            {
                cmb.FlatStyle = FlatStyle.Flat;
                cmb.DropDownStyle = ComboBoxStyle.DropDown;
                cmb.Bounds = e.CellBounds;
                cmb.Items.AddRange(GlobalVar.CurrentMapDocument.Map.Waypoints.ToArray());
                cmb.Text = e.Control.Text;
                e.Control = cmb;
            }
            else switch (valuePair.Key) 
                {
                    case "TechLevel":
                            cmb.FlatStyle = FlatStyle.Flat;
                            cmb.DropDownStyle = ComboBoxStyle.DropDown;
                            cmb.Bounds = e.CellBounds;
                            cmb.Items.Add("0");
                            cmb.Items.Add("1");
                            cmb.Items.Add("2");
                            cmb.Items.Add("3");
                            cmb.Items.Add("4");
                            cmb.Items.Add("5");
                            cmb.Items.Add("6");
                            cmb.Items.Add("7");
                            cmb.Items.Add("8");
                            cmb.Items.Add("9");
                            cmb.Items.Add("10");
                            cmb.Text = e.Control.Text;
                            e.Control = cmb;
                            break;
                    case "TaskforceID":
                        cmb.FlatStyle = FlatStyle.Flat;
                        cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                        cmb.Bounds = e.CellBounds;
                        cmb.Items.AddRange(GlobalVar.CurrentMapDocument.Map.TaskForces.ToArray());
                        cmb.SelectedItem = GlobalVar.CurrentMapDocument.Map.TaskForces.ToList().Find(s => s.ID == e.Control.Text);
                        e.Control = cmb;
                        break;
                    case "ScriptID":
                        cmb.FlatStyle = FlatStyle.Flat;
                        cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                        cmb.Bounds = e.CellBounds;
                        cmb.Items.AddRange(GlobalVar.CurrentMapDocument.Map.Scripts.ToArray());
                        cmb.SelectedItem = GlobalVar.CurrentMapDocument.Map.Scripts.ToList().Find(s => s.ID == e.Control.Text);
                        e.Control = cmb;
                        break;
                    case "TagID":
                        cmb.FlatStyle = FlatStyle.Flat;
                        cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                        cmb.Bounds = e.CellBounds;
                        cmb.Items.Add("None");
                        cmb.Items.AddRange(GlobalVar.CurrentMapDocument.Map.Tags.ToArray());
                        if (string.IsNullOrEmpty(e.Control.Text)) cmb.SelectedIndex = 0;
                        else cmb.SelectedItem = GlobalVar.CurrentMapDocument.Map.Tags.ToList().Find(s => s.ID == e.Control.Text);
                        e.Control = cmb;
                        break;
                    case "House":
                        cmb.FlatStyle = FlatStyle.Flat;
                        cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                        cmb.Bounds = e.CellBounds;
                        cmb.Items.AddRange(GlobalVar.CurrentMapDocument.Map.Countries.ToArray());
                        cmb.SelectedItem = GlobalVar.CurrentMapDocument.Map.Countries.ToList().Find(s => s.ID == e.Control.Text);
                        e.Control = cmb;
                        break;
                    default:
                        e.Control.Bounds = e.CellBounds;
                        break;
                }
        }
        private void olvTeamConfig_CellEditFinishing(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            if(!e.Cancel && e.SubItemIndex == 1)
            {
                // Do save works
                KeyValuePair<string, TeamUnit.TeamUnitNode> valuePair = (KeyValuePair<string, TeamUnit.TeamUnitNode>)e.RowObject;
                TeamUnit.TeamUnitNode ret = _CurrentTeamUnit.Data[valuePair.Key] = new TeamUnit.TeamUnitNode(valuePair.Value.ShowName, valuePair.Value.Value);
                // _CurrentTeamUnit.Data[valuePair.Key].Value;
                if (Constant.TeamBoolIndex.Contains(valuePair.Key))
                {
                    ComboBox cmb = e.Control as ComboBox;
                    ret.Value = cmb.SelectedIndex == 0 ? false : true;
                    _CurrentTeamUnit.Data[valuePair.Key] = ret;
                }
                else if (valuePair.Key == "Waypoint")
                {
                    ComboBox cmb = e.Control as ComboBox;
                    string text = cmb.Text;
                    int val = 0;
                    try
                    {
                        val = int.Parse(text);
                    }
                    catch
                    {
                        val = 0;
                    }
                    if (val < 0 || val > 701) val = 0;
                    if (GlobalVar.CurrentMapDocument.Map.Waypoints.ToList().Find(s => s.Num == val.ToString()) == null) val = 0;
                    ret.Value = val;
                    _CurrentTeamUnit.Data[valuePair.Key] = ret;
                }
                else if (valuePair.Key == "TechLevel")
                {
                    ComboBox cmb = e.Control as ComboBox;
                    string text = cmb.Text;
                    int val = 0;
                    try
                    {
                        val = int.Parse(text);
                    }
                    catch
                    {
                        val = 0;
                    }
                    ret.Value = val;
                    _CurrentTeamUnit.Data[valuePair.Key] = ret;
                }
                else if (valuePair.Key == "Group")
                {
                    string text = e.Control.Text;
                    int val = -1;
                    try
                    {
                        val = int.Parse(text);
                    }
                    catch
                    {
                        val = -1;
                    }
                    ret.Value = val;
                    _CurrentTeamUnit.Data[valuePair.Key] = ret;
                }
                else if (valuePair.Key == "Priority")
                {
                    string text = e.Control.Text;
                    int val = 0;
                    try
                    {
                        val = int.Parse(text);
                    }
                    catch
                    {
                        val = 0;
                    }
                    ret.Value = val;
                    _CurrentTeamUnit.Data[valuePair.Key] = ret;
                }
                else if (valuePair.Key == "TeamCapacity")
                {
                    string text = e.Control.Text;
                    int val = 0;
                    try
                    {
                        val = int.Parse(text);
                    }
                    catch
                    {
                        val = 0;
                    }
                    ret.Value = val;
                    _CurrentTeamUnit.Data[valuePair.Key] = ret;
                }
                else if (valuePair.Key == "VeteranLevel")
                {
                    ComboBox cmb = e.Control as ComboBox;
                    ret.Value = (TeamVeteranLevel)(cmb.SelectedIndex + 1);
                    _CurrentTeamUnit.Data[valuePair.Key] = ret;
                }
                else if (valuePair.Key == "MCDecision")
                {
                    ComboBox cmb = e.Control as ComboBox;
                    ret.Value = (TeamMCDecision)(cmb.SelectedIndex);
                    _CurrentTeamUnit.Data[valuePair.Key] = ret;
                }
                else if (valuePair.Key == "TaskforceID")
                {
                    ComboBox cmb = e.Control as ComboBox;
                    ret.Value = ((TaskforceItem)cmb.SelectedItem).ID;
                    _CurrentTeamUnit.Data[valuePair.Key] = ret;
                }
                else if (valuePair.Key == "ScriptID")
                {
                    ComboBox cmb = e.Control as ComboBox;
                    ret.Value = ((TeamScriptGroup)cmb.SelectedItem).ID;
                    _CurrentTeamUnit.Data[valuePair.Key] = ret;
                }
                else if (valuePair.Key == "TagID")
                {
                    ComboBox cmb = e.Control as ComboBox;
                    ret.Value = cmb.Text == "None" ? string.Empty : ((TagItem)cmb.SelectedItem).ID;
                    _CurrentTeamUnit.Data[valuePair.Key] = ret;
                }
                else ret.Value = e.Control.Text;
                _CurrentTeamUnit.Data[valuePair.Key] = ret;
            }
        }
        private void olvTeamConfig_CellEditFinished(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            olvTeamConfig.ClearObjects();
            olvTeamConfig.AddObjects(_CurrentTeamUnit.Data);
            _CurrentTeam.SetFromUnit(_CurrentTeamUnit);
            UpdateAt(lbxTeamList, _CurrentTeam, ref updatingLbxTeamList);
        }
        #endregion
        #endregion
        #endregion

        #region Misc Page
        #region Local Variables

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

        #region Houses
        #region lbx
        private bool updatingLbxHousesList = false;
        private void lbxHouses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxHouses.SelectedItem == null) return;
            if (!updatingLbxHousesList)
            {
                olvHouse.ClearObjects();
                olvHouse.SetObjects(_CurrentHouseUnit.Data);
                UpdateHouseAlliance();
            }
        }
        private void lbxHouseAllie_Enter(object sender, EventArgs e)
        {
            lbxHouseEnemy.SelectedIndices.Clear();
        }
        private void lbxHouseEnemy_Enter(object sender, EventArgs e)
        {
            lbxHouseAllie.SelectedIndices.Clear();
        }
        #endregion
        #region olv
        private void olvHouse_CellEditStarting(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            KeyValuePair<string, HouseUnit.HouseShowUnit> keyValuePair = (KeyValuePair<string, HouseUnit.HouseShowUnit>)e.RowObject;
            switch (keyValuePair.Key)
            {
                case "IQ":
                    ComboBox iqCbb = new ComboBox();
                    iqCbb.DropDownStyle = ComboBoxStyle.DropDownList;
                    iqCbb.Items.Add("0");
                    iqCbb.Items.Add("1");
                    iqCbb.Items.Add("2");
                    iqCbb.Items.Add("3");
                    iqCbb.Items.Add("4");
                    iqCbb.Items.Add("5");
                    if ((int)keyValuePair.Value.Value > 5 || (int)keyValuePair.Value.Value < 0) iqCbb.SelectedIndex = 0;
                    else iqCbb.SelectedIndex = (int)keyValuePair.Value.Value;
                    e.Control = iqCbb;
                    break;
                case "PlayerControl":
                    ComboBox boolCombobox = new ComboBox();
                    boolCombobox.DropDownStyle = ComboBoxStyle.DropDownList;
                    boolCombobox.Items.Add("False");
                    boolCombobox.Items.Add("True");
                    boolCombobox.SelectedIndex = (bool)keyValuePair.Value.Value ? 1 : 0;
                    e.Control = boolCombobox;
                    break;
                case "Country":
                    ComboBox countryCbb = new ComboBox();
                    countryCbb.DropDownStyle = ComboBoxStyle.DropDownList;
                    StaticHelper.LoadToObjectCollection(countryCbb, map.Countries);
                    CountryItem countryItem = map.Countries.GetCountry((string)keyValuePair.Value.Value);
                    countryCbb.SelectedItem = countryItem;
                    e.Control = countryCbb;
                    break;
                case "ColorName":
                    List<string> colorList = new List<string>();
                    var iniPairs= GlobalVar.GlobalRules["Colors"].ToList();
                    foreach (INIPair pair in iniPairs) colorList.Add(pair.Name);
                    ComboBox colorCbb = new ComboBox();
                    colorCbb.DropDownStyle = ComboBoxStyle.DropDownList;
                    colorCbb.Items.AddRange(colorList.ToArray());
                    if (colorList.Exists(s => s == (string)keyValuePair.Value.Value))
                        colorCbb.SelectedItem = (string)keyValuePair.Value.Value;
                    e.Control = colorCbb;
                    break;
                case "NodeCounts":
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
            e.Control.Bounds = e.CellBounds;
        }

        private void olvHouse_CellEditFinishing(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            if (!e.Cancel)
            {
                string value = e.Control.Text;
                KeyValuePair<string, HouseUnit.HouseShowUnit> keyValuePair = (KeyValuePair<string, HouseUnit.HouseShowUnit>)e.RowObject;
                HouseUnit.HouseShowUnit ret = new HouseUnit.HouseShowUnit(keyValuePair.Value.ShowName, keyValuePair.Value.Value);
                switch (keyValuePair.Key)
                {
                    case "IQ":
                        ret.Value = ((ComboBox)e.Control).SelectedIndex;
                        _CurrentHouseUnit.Data["IQ"] = ret;
                        break;
                    case "PlayerControl":
                        ret.Value = ((ComboBox)e.Control).SelectedIndex == 0 ? false : true;
                        _CurrentHouseUnit.Data["PlayerControl"] = ret;
                        break;
                    case "Country":
                        ret.Value = string.IsNullOrEmpty(e.Control.Text) ? keyValuePair.Value.Value : ret.Value = e.Control.Text;
                        _CurrentHouseUnit.Data["Country"] = ret;
                        break;
                    case "ColorName":
                        ret.Value = string.IsNullOrEmpty(e.Control.Text) ? keyValuePair.Value.Value : ret.Value = e.Control.Text;
                        _CurrentHouseUnit.Data["ColorName"] = ret;
                        break;
                    case "TechLevel":
                        try { ret.Value = int.Parse(e.Control.Text); }
                        catch { ret.Value = 10; }
                        _CurrentHouseUnit.Data["TechLevel"] = ret;
                        break;
                    case "Credits":
                        try { ret.Value = int.Parse(e.Control.Text); }
                        catch { ret.Value = 0; }
                        _CurrentHouseUnit.Data["Credits"] = ret;
                        break;
                    case "PercentBuilt":
                        try { ret.Value = double.Parse(e.Control.Text); }
                        catch { ret.Value = 100d; }
                        if ((double)ret.Value < 0) ret.Value = 100d;
                        _CurrentHouseUnit.Data["PercentBuilt"] = ret;
                        break;
                    case "Edge":
                        ret.Value = (HouseEdges)(((ComboBox)e.Control).SelectedIndex + 1);
                        _CurrentHouseUnit.Data["Edge"] = ret;
                        break;
                    default:
                        break;
                }
            }
        }

        private void olvHouse_CellEditFinished(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            olvHouse.ClearObjects();
            olvHouse.AddObjects(_CurrentHouseUnit.Data);
            _CurrentHouse.SetFromUnit(_CurrentHouseUnit);
            UpdateAt(lbxHouses, _CurrentHouse, ref updatingLbxHousesList);
        }
        #endregion
        #region btn
        private void btnGoEnemy_Click(object sender, EventArgs e)
        {
            foreach(string house in lbxHouseAllie.SelectedItems)
                _CurrentHouse.AlliesWith.Remove(house);
            UpdateHouseAlliance();
        }

        private void btnGoAllie_Click(object sender, EventArgs e)
        {
            foreach (string house in lbxHouseEnemy.SelectedItems)
                _CurrentHouse.AlliesWith.Add(house);
            UpdateHouseAlliance();
        }
        private void btnDelHouse_Click(object sender, EventArgs e)
        {
            if (_CurrentHouse == null) return;
            if (MessageBox.Show(DICT["LGCbtnHouseDelMsgboxMain"], DICT["LGCbtnHouseDelMsgboxTitle"], MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                map.Countries.Remove(map.Countries.GetCountry(_CurrentHouse.Country).Index);
                map.Houses.Remove(_CurrentHouse.Index);
                int idx = lbxHouses.SelectedIndex;
                RemoveAt(lbxHouses, idx, ref updatingLbxHousesList);
            }
                
        }

        public static HouseItem retHouse = new HouseItem();
        private void btnNewHouse_Click(object sender, EventArgs e)
        {
            dlgNewHouse dlgNew = new dlgNewHouse();
            if (dlgNew.ShowDialog() == DialogResult.OK)
                AddTo(lbxHouses, retHouse, ref updatingLbxHousesList);
        }
        #endregion
        #region txb
        private void txbHouseAllies_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _CurrentHouse.AlliesWith.Clear();
                string[] allies = txbHouseAllies.Text.Split(',');
                foreach (string house in allies)
                {
                    bool houseExist = false;
                    foreach (HouseItem item in map.Houses)
                        if (item.Name == house)
                        {
                            houseExist = true;
                            break;
                        }
                    if (houseExist) _CurrentHouse.AlliesWith.Add(house);
                }
            }
            catch
            {
                e.Cancel = true;
            }
        }

        private void txbHouseAllies_Validated(object sender, EventArgs e)
        {
            UpdateHouseAlliance();
        }
        #endregion
        #endregion
        #endregion
    }
}
