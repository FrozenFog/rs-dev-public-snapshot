using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;
using RelertSharp.IniSystem;
using static RelertSharp.Language;

namespace RelertSharp.SubWindows.LogicEditor
{
    public partial class LogicEditor : Form
    {
        private Map map;
        private DescriptCollection descriptCollection = new DescriptCollection();
        private LinkLabel[] lklEP, lklAP;
        private TextBox[] txbEP, txbAP;
        private CheckBox[] ckbEP, ckbAP;
        private ComboBox[] cbbEP, cbbAP;
        private SoundManager soundPlayer = new SoundManager();

        private List<LocalVarItem> localVarList = new List<LocalVarItem>();
        private BindingSource localVarSource = new BindingSource();


        #region Ctor - LogicEditor
        public LogicEditor()
        {
            InitializeComponent();
            SetLanguage();
            InitControls();
            map = GlobalVar.CurrentMapDocument.Map;
            SetGlobal();
            
            StaticHelper.LoadToObjectCollection(cbbEventAbst, descriptCollection.Events);
            StaticHelper.LoadToObjectCollection(cbbActionAbst, descriptCollection.Actions);
            UpdateTrgList(TriggerItem.DisplayingType.IDandName);
            LoadTaskforceList();
            LoadScriptList();
            LoadTeamList();
            LoadHouseList();
            LoadLocalVariables();
            
            lbxTriggerList.SelectedIndex = 0;
        }
        #endregion


        #region Private Methods - LogicEditor
        #region Initialization Utils
        private void SetLanguage()
        {
            foreach (Control c in Controls) SetControlLanguage(c);
            Text = DICT[Text];
        }
        private void SetGlobal()
        {
            foreach (HouseItem house in map.Houses)
            {
                if (house.PlayerControl)
                {
                    CountryItem country = map.Countries.GetCountry(house.Country);
                    GlobalVar.PlayerSide = country.Side;
                    break;
                }
            }
            GlobalVar.GlobalRules.Override(map.IniResidue.Values);
        }

        private void InitControls()
        {
            lklEP = new LinkLabel[4] { lklEP1, lklEP2, lklEP3, lklEP4 };
            lklAP = new LinkLabel[4] { lklAP1, lklAP2, lklAP3, lklAP4 };
            txbEP = new TextBox[4] { txbEP1, txbEP2, txbEP3, txbEP4 };
            txbAP = new TextBox[4] { txbAP1, txbAP2, txbAP3, txbAP4 };
            ckbEP = new CheckBox[4] { ckbEP1, ckbEP2, ckbEP3, ckbEP4 };
            ckbAP = new CheckBox[4] { ckbAP1, ckbAP2, ckbAP3, ckbAP4 };
            cbbEP = new ComboBox[4] { cbbEP1, cbbEP2, cbbEP3, cbbEP4 };
            cbbAP = new ComboBox[4] { cbbAP1, cbbAP2, cbbAP3, cbbAP4 };
            lblNoParamE.Location = new Point(gpbEventParam.Size.Width / 2 - lblNoParamE.Size.Width / 2, gpbEventParam.Size.Height / 2 - lblNoParamE.Size.Height);
            lblNoParamA.Location = new Point(gpbActionParam.Size.Width / 2 - lblNoParamA.Size.Width / 2, gpbActionParam.Size.Height / 2 - lblNoParamA.Size.Height);
        }
        private void LoadHouseList()
        {
            map.Countries.AscendingSort();
            StaticHelper.LoadToObjectCollection(lbxTriggerHouses, map.Countries);
        }
        private void LoadTaskforceList()
        {
            StaticHelper.LoadToObjectCollection(lbxTaskList, map.TaskForces);
            List<TechnoPair> technoPairs = new List<TechnoPair>();
            technoPairs.Add(new TechnoPair(string.Empty, "(Nothing)"));
            technoPairs.AddRange(GlobalVar.GlobalRules.InfantryList);
            technoPairs.AddRange(GlobalVar.GlobalRules.AircraftList);
            technoPairs.AddRange(GlobalVar.GlobalRules.VehicleList);
            foreach (TechnoPair techno in technoPairs)
                //if (!string.IsNullOrEmpty(techno.UIName))
                techno.ResetAbst(TechnoPair.AbstractType.CsfName, TechnoPair.IndexType.RegName);
            //technoPairs.Sort((x, y) => x.RegName.CompareTo(y.RegName));
            StaticHelper.LoadToObjectCollection(cbbTaskType, technoPairs);
        }
        private void LoadScriptList()
        {
            StaticHelper.LoadToObjectCollection(lbxScriptList, map.Scripts);
        }
        private void LoadTeamList()
        {
            StaticHelper.LoadToObjectCollection(lbxTeamList, map.Teams);
        }
        private void LoadLocalVariables()
        {
            chklbxLocalVar.Items.Clear();
            chklbxLocalVar.DataSource = localVarSource;
            localVarList = map.LocalVariables.ToList();
            if (localVarList == null) return;
            localVarSource.DataSource = localVarList;
            for (int idx = 0, count = localVarList.Count; idx < count; ++idx)
                chklbxLocalVar.SetItemChecked(idx, localVarList[idx].InitState);
        }
        #endregion

        #region General GUI Update Utils
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
        private void UpdateContent(TriggerItem trg)
        {
            UpdateTags(trg.ID);
            StaticHelper.LoadToObjectCollection(lbxEventList, trg.Events);
            if (lbxEventList.Items.Count > 0) lbxEventList.SelectedIndex = 0;
            else
            {
                ClearContent(gpbEvents);
                mtxbEventID.Text = "00";
            }
            StaticHelper.LoadToObjectCollection(lbxActionList, trg.Actions);
            if (lbxActionList.Items.Count > 0) lbxActionList.SelectedIndex = 0;
            else
            {
                ClearContent(gpbActions);
                mtxbActionID.Text = "00";
            }
        }
        private void UpdateActionContent(LogicItem item)
        {
            mtxbActionID.Text = item.ID.ToString();
            txbActionAnno.Text = item.Comment;
            cbbActionAbst.SelectedIndex = item.ID;
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
                        case TriggerParam.ParamType.Waypoint:
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
        private void UpdateTrgList(TriggerItem.DisplayingType type = TriggerItem.DisplayingType.Remain)
        {
            lbxTriggerList.Tag = (int)type;
            map.Triggers.SetToString(type);
            StaticHelper.LoadToObjectCollection(lbxTriggerList, map.Triggers);
            cbbAttatchedTrg.Items.Clear();
            cbbAttatchedTrg.Items.Add(TriggerItem.NullTrigger);
            foreach (TriggerItem trigger in map.Triggers)
            {
                cbbAttatchedTrg.Items.Add(trigger);
            }
        }
        private void UpdateTags(string triggerID)
        {
            TriggerItem trg = map.Triggers[triggerID];
            var tags = map.Tags.GetTagFromTrigger(triggerID, trg);
            txbTrgID.Text = triggerID;
            txbTrgName.Text = trg.Name;
            StaticHelper.LoadToObjectCollection(cbbTagID, tags);
            cbbTagID.SelectedIndex = tags.Count >= 0 ? 0 : -1;
            cbbTagID_SelectedIndexChanged(cbbTagID, null);
            rdbRepeat0.Checked = false;
            rdbRepeat1.Checked = false;
            rdbRepeat2.Checked = false;
            ckbDisabled.Checked = trg.Disabled;
            lbxTriggerHouses.SelectedItem = map.Countries.GetCountry(trg.House);
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
                        case TriggerParam.ParamType.Waypoint:
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
        private void UpdateTaskforceContent(int lvSelectedindex)
        {
            TaskforceItem taskforce = lbxTaskList.SelectedItem as TaskforceItem;
            txbTaskName.Text = taskforce.Name;
            mtxbTaskGroup.Text = taskforce.Group.ToString();
            txbTaskID.Text = taskforce.ID;

            imglstPcx.Images.Clear();
            lvTaskforceUnits.SelectedIndices.Clear();
            lvTaskforceUnits.Items.Clear();
            if (taskforce.Members.Count > 0)
            {
                Dictionary<string, Image> dict = GlobalVar.GlobalDir.GetPcxImages(taskforce.MemberPcxNames);
                foreach (string key in dict.Keys)
                {
                    imglstPcx.Images.Add(key, dict[key]);
                }
                IEnumerable<TaskforceUnit> units = taskforce.Members;
                IEnumerable<ListViewItem> items = TaskforceItem.ToListViewItems(units);
                StaticHelper.LoadToObjectCollection(lvTaskforceUnits, items);
                lvTaskforceUnits.SelectedIndices.Add(lvSelectedindex);
            }
        }
        #endregion

        #region Trace Utils
        private async void ManageSound(TriggerParam param, TechnoPair p)
        {
            if (soundPlayer.IsPlaying)
            {
                soundPlayer.Stop();
                UseWaitCursor = false;
            }
            else
            {
                string name = soundPlayer.GetSoundName(p, (SoundType)param.ComboType);
                await Task.Run(() =>
                {
                    soundPlayer.LoadWav(GlobalVar.GlobalSoundBank.GetSound(name));
                });
                soundPlayer.Play();
                UseWaitCursor = false;
            }
        }
        #endregion

        #region Misc Utils
        private void AddTo(ListBox dest, object obj, ref bool locker)
        {
            locker = true;
            dest.Items.Add(obj);
            locker = false;
            dest.SelectedItem = obj;
        }
        private void UpdateAt(ListBox dest, object updatevalue, ref bool locker)
        {
            locker = true;
            int index = dest.SelectedIndex;
            if (index < 0)
            {
                locker = false;
                return;
            }
            dest.Items.RemoveAt(index);
            dest.Items.Insert(index, updatevalue);
            dest.SelectedIndex = index;
            locker = false;
        }
        private void UpdateAt(ListView dest, ListViewItem item, ref bool locker)
        {
            locker = true;
            int index = dest.SelectedIndices[0];
            dest.Items.RemoveAt(index);
            dest.Items.Insert(index, item);
            dest.SelectedIndices.Clear();
            dest.SelectedIndices.Add(index);
            locker = false;
        }
        private void RemoveAt(ListView dest, int index, ref bool locker)
        {
            locker = true;
            dest.SelectedIndices.Clear();
            dest.Items.RemoveAt(index);
            locker = false;
            if (index != 0) dest.SelectedIndices.Add(index - 1);
        }
        private void RemoveAt(ListBox dest, int index, ref bool locker)
        {
            locker = true;
            dest.Items.RemoveAt(index);
            locker = false;
            if (index != 0) dest.SelectedIndex = index - 1;
        }
        private IEnumerable<object> GetComboCollections(TriggerParam param)
        {
            switch (param.ComboType)
            {
                case TriggerParam.ComboContent.Aircrafts:
                    return GlobalVar.GlobalRules.AircraftList;
                case TriggerParam.ComboContent.Buildings:
                    return GlobalVar.GlobalRules.BuildingList;
                case TriggerParam.ComboContent.Infantries:
                    return GlobalVar.GlobalRules.InfantryList;
                case TriggerParam.ComboContent.Units:
                    return GlobalVar.GlobalRules.VehicleList;
                case TriggerParam.ComboContent.SoundNames:
                    return GlobalVar.GlobalSound.SoundList;
                case TriggerParam.ComboContent.EvaNames:
                    return GlobalVar.GlobalSound.EvaList;
                case TriggerParam.ComboContent.ThemeNames:
                    return GlobalVar.GlobalSound.ThemeList;
                case TriggerParam.ComboContent.LocalVar:
                    return map.LocalVariables.ToTechno();
                case TriggerParam.ComboContent.SuperWeapons:
                    return GlobalVar.GlobalRules.SuperWeaponList;
                case TriggerParam.ComboContent.CsfLabel:
                    return GlobalVar.GlobalCsf.TechnoPairs;
                case TriggerParam.ComboContent.Triggers:
                    return map.Triggers.ToTechno();
                case TriggerParam.ComboContent.Tags:
                    return map.Tags.ToTechno();
                case TriggerParam.ComboContent.TechnoType:
                    return GlobalVar.GlobalRules.TechnoList;
                case TriggerParam.ComboContent.GlobalVar:
                    return GlobalVar.GlobalRules.GlobalVar;
                case TriggerParam.ComboContent.Teams:
                    return map.Teams.ToTechno();
                case TriggerParam.ComboContent.Houses:
                    return map.Countries.ToTechno();
                case TriggerParam.ComboContent.Animations:
                    return GlobalVar.GlobalRules.AnimationList;
                case TriggerParam.ComboContent.ParticalAnim:
                    return GlobalVar.GlobalRules.ParticalList;
                case TriggerParam.ComboContent.VoxelAnim:
                    return GlobalVar.GlobalRules.VoxAnimList;
                case TriggerParam.ComboContent.BuildingID:
                    return GlobalVar.GlobalRules.BuildingIDList;
                case TriggerParam.ComboContent.Movies:
                    return GlobalVar.GlobalRules.MovieList;
                case TriggerParam.ComboContent.Warhead:
                    return GlobalVar.GlobalRules.WarheadList;
                default:
                    return null;
            }
        }
        private void GoEnter(KeyEventArgs e, Action a)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                a.Invoke();
            }
        }
        #endregion

        #region Parameter Utils
        private void SetParamControls(Control[] controls, TriggerParam param, string[] paramData, int controlIndex)
        {
            if (controls.GetType() == typeof(LinkLabel[]))
            {
                ((LinkLabel)controls[controlIndex]).Text = param.Name;
                if (!param.Traceable) ((LinkLabel)controls[controlIndex]).Enabled = false;
            }
            else if (controls.GetType() == typeof(ComboBox[]))
            {
                Cursor = Cursors.WaitCursor;
                IList<object> data = GetComboCollections(param).ToList();
                StaticHelper.LoadToObjectCollection((ComboBox)controls[controlIndex], data);
                StaticHelper.SelectCombo((ComboBox)controls[controlIndex], param.GetParameter(paramData), param);
                Cursor = Cursors.Arrow;
            }
            else
            {
                if (param.Type == TriggerParam.ParamType.Bool)
                {
                    ((CheckBox)controls[controlIndex]).Checked = param.GetParameter(paramData, true);
                }
                else
                {
                    controls[controlIndex].Text = param.GetParameter(paramData);
                }
            }
            controls[controlIndex].Visible = true;
        }
        private void WriteParam(string value, int pos, LogicType type, ref bool locker, bool base26 = false)
        {
            if (base26)
            {
                try
                {
                    int v = int.Parse(value);
                    value = Utils.Misc.WaypointString(v);
                }
                catch
                {
                    value = "A";
                }
            }
            if (type == LogicType.EventLogic)
            {
                _CurrentEvent.Parameters[pos] = value;
                UpdateAt(lbxEventList, _CurrentEvent, ref locker);
            }
            else
            {
                _CurrentAction.Parameters[pos] = value;
                UpdateAt(lbxActionList, _CurrentAction, ref locker);
            }
        }
        private void WriteParam(TechnoPair value, int pos, LogicType type, ref bool locker)
        {
            WriteParam(value.Index, pos, type, ref locker);
        }
        private void WriteParam(bool value, int pos, LogicType type, ref bool locker)
        {
            if (type == LogicType.EventLogic)
            {
                _CurrentEvent.Parameters[pos] = value ? "1" : "0";
                UpdateAt(lbxEventList, _CurrentEvent, ref locker);
            }
            else
            {
                _CurrentAction.Parameters[pos] = value ? "1" : "0";
                UpdateAt(lbxActionList, _CurrentAction, ref locker);
            }
        }
        private void ParamChanged(object sender, LogicType type, ref bool locker)
        {
            Type t = sender.GetType();
            int paramsindex = int.Parse(((Control)sender).Tag.ToString());
            TriggerParam param;
            if (type == LogicType.EventLogic) param = _CurrentEventParameters[paramsindex];
            else param = _CurrentActionParameters[paramsindex];
            int i = param.ParamPos;
            bool isBase26 = param.Type == TriggerParam.ParamType.Waypoint;
            if (t == typeof(TextBox))
            {
                string text = ((TextBox)sender).Text;
                WriteParam(text, i, type, ref locker, isBase26);
            }
            else if (t == typeof(ComboBox))
            {
                TechnoPair p = ((ComboBox)sender).SelectedItem as TechnoPair;
                if (p == null) p = new TechnoPair(((ComboBox)sender).Text, "");
                WriteParam(p, i, type, ref locker);
            }
            else if (t == typeof(CheckBox))
            {
                WriteParam(((CheckBox)sender).Checked, i, type, ref locker);
            }
        }
        #endregion

        #endregion


        #region Private Calls - LogicEditor
        private TriggerDescription _CurrentEventDesc { get { return cbbEventAbst.SelectedItem as TriggerDescription; } }
        private TriggerDescription _CurrentActionDesc { get { return cbbActionAbst.SelectedItem as TriggerDescription; } }
        private List<TriggerParam> _CurrentEventParameters { get { return _CurrentEventDesc.Parameters; } }
        private List<TriggerParam> _CurrentActionParameters { get { return _CurrentActionDesc.Parameters; } }
        private TriggerItem _CurrentBoxTrigger { get { return lbxTriggerList.SelectedItem as TriggerItem; } }
        private TriggerItem _CurrentTrigger { get { return map.Triggers[txbTrgID.Text]; } }
        private TaskforceItem _CurrentTaskforce { get { return map.TaskForces[txbTaskID.Text]; } }
        private TaskforceUnit _CurrentBoxTaskforceUnit { get { return TaskforceUnit.FromListviewItem(lvTaskforceUnits.SelectedItems[0]); } }
        private TaskforceUnit _CurrentTaskforceUnit { get { return _CurrentTaskforce.Members[lvTaskforceUnits.SelectedIndices[0]]; } }
        private LogicItem _CurrentEvent { get { return _CurrentTrigger.Events[_CurrentBoxEvent.idx]; } }
        private LogicItem _CurrentBoxEvent { get { return lbxEventList.SelectedItem as LogicItem; } }
        private LogicItem _CurrentAction { get { return _CurrentTrigger.Actions[_CurrentBoxAction.idx]; } }
        private LogicItem _CurrentBoxAction { get { return lbxActionList.SelectedItem as LogicItem; } }
        private TagItem _CurrentTag { get { return map.Tags[cbbTagID.Text]; } }
        private SearchCollection _SearchResult { get; set; } = new SearchCollection();
        #endregion
    }
}
