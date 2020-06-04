﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using RelertSharp.MapStructure.Logic;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Points;
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    internal partial class ParameterPanel : UserControl
    {
        internal event LogicItemUpdateHandler ItemUpdated;
        internal event SoundPlayingHandler NeedPlayingShound;
        internal event TriggerUpdateHandler TriggerTracing;
        internal event I2dLocateableHandler JumpToWaypoint;
        private ComboBox cbbCsf, cbbPrev;


        internal LogicItem CurrentItem { get; set; }
        internal List<TriggerParam> CurrentParams { get { return (cbbEventAbst.SelectedItem as TriggerDescription).Parameters; } }
        private Map Map { get { return CurrentMapDocument.Map; } }


        private bool initialized = false;
        private ListBox lbxReferance;
        private LinkLabel[] lkls;
        private TextBox[] txbs;
        private CheckBox[] ckbs;
        private ComboBox[] cbbs;


        public ParameterPanel()
        {
            InitializeComponent();
        }


        #region Public Methods
        public void Initialize(IEnumerable<TriggerDescription> descriptions, ListBox refer, bool isEvent)
        {
            SetLanguage();
            LoadToObjectCollection(cbbEventAbst, descriptions);
            lkls = new LinkLabel[4] { lklEP1, lklEP2, lklEP3, lklEP4 };
            txbs = new TextBox[4] { txbEP1, txbEP2, txbEP3, txbEP4 };
            ckbs = new CheckBox[4] { ckbEP1, ckbEP2, ckbEP3, ckbEP4 };
            cbbs = new ComboBox[4] { cbbEP1, cbbEP2, cbbEP3, cbbEP4 };
            lblNoParamE.Location = new Point(gpbEventParam.Size.Width / 2 - lblNoParamE.Size.Width / 2, gpbEventParam.Size.Height / 2 - lblNoParamE.Size.Height);
            lbxReferance = refer;
            cbbCsf = new ComboBox()
            {
                Location = cbbEP1.Location,
                Size = cbbEP1.Size,
                Visible = false,
                Name = "cbbCsf"
            };
            cbbCsf.Items.AddRange(GlobalCsf.TechnoPairs.ToArray());
            cbbCsf.SelectedIndexChanged += new EventHandler(ParamChanged);
            gpbEventParam.Controls.Add(cbbCsf);
            if (!isEvent)
            {
                mtxbEventID.Mask = "000";
            }
        }
        public void Reload(LogicItem item)
        {
            CurrentItem = item;
            if (item == null) ClearContent();
            else RefreshControl();
            mtxbEventID.Focus();
            mtxbEventID.SelectAll();
        }
        #endregion


        #region OnEvent
        protected virtual void OnLogicItemChanged()
        {
            ItemUpdated?.Invoke(this, CurrentItem);
        }
        protected virtual void OnPlayingSound(TriggerParam param, TechnoPair p)
        {
            NeedPlayingShound?.Invoke(this, param, p);
        }
        protected virtual void OnTracingTrigger(TriggerItem trigger)
        {
            TriggerTracing?.Invoke(this, trigger);
        }
        protected virtual void OnWaypointJump(I2dLocateable cell)
        {
            JumpToWaypoint?.Invoke(this, cell);
        }
        #endregion


        private bool isControlRefreshing = false;
        private void RefreshControl()
        {
            isControlRefreshing = true;
            mtxbEventID.Text = CurrentItem.ID.ToString();
            cbbEventAbst.SelectedIndex = CurrentItem.ID;
            txbEventAnno.Text = CurrentItem.Comment;
            isControlRefreshing = false;
            AbstractChanged();
        }
        private void AbstractChanged()
        {
            TriggerDescription desc = cbbEventAbst.SelectedItem as TriggerDescription;
            rtxbEventDetail.Text = desc.Description;
            rtxbEventDetail.Refresh();
            RefreshParam();
        }


        #region ParamRefreshing
        private bool isParamRefreshing = false;
        private void RefreshParam()
        {
            isParamRefreshing = true;
            TriggerDescription desc = cbbEventAbst.SelectedItem as TriggerDescription;
            string[] parameters = CurrentItem.Parameters;
            foreach(Control c in gpbEventParam.Controls)
            {
                c.Visible = false;
                c.Enabled = true;
            }
            if (desc.Parameters.Count == 0) lblNoParamE.Visible = true;
            else
            {
                int i = 0;
                foreach (TriggerParam param in desc.Parameters)
                {
                    switch (param.Type)
                    {
                        case TriggerParam.ParamType.PlainString:
                        case TriggerParam.ParamType.Waypoint:
                            SetParamControls(txbs, param, parameters, i);
                            break;
                        case TriggerParam.ParamType.SelectableString:
                            SetParamControls(cbbs, param, parameters, i);
                            break;
                        case TriggerParam.ParamType.Bool:
                            SetParamControls(ckbs, param, parameters, i);
                            break;
                    }
                    SetParamControls(lkls, param, parameters, i);
                    i++;
                }
            }
            isParamRefreshing = false;
        }
        private int indexPrev = -1;
        private bool csfEnabled = false;
        private void SwapCombobox(bool isCsfNow, int indexNow)
        {
            if (isCsfNow && !csfEnabled)
            {
                csfEnabled = true;
                cbbPrev = cbbs[indexNow];
                cbbs[indexNow] = cbbCsf;
                indexPrev = indexNow;
                cbbCsf.Tag = cbbPrev.Tag;
            }
            else if (!isCsfNow && csfEnabled)
            {
                if (indexPrev != -1)
                {
                    csfEnabled = false;
                    cbbs[indexPrev] = cbbPrev;
                }
            }
        }
        private void SetParamControls(Control[] controls, TriggerParam param, string[] paramData, int controlIndex)
        {
            if (controls.GetType() == typeof(LinkLabel[]))
            {
                ((LinkLabel)controls[controlIndex]).Text = param.Name;
                if (!param.Traceable) ((LinkLabel)controls[controlIndex]).Enabled = false;
            }
            else if (controls.GetType() == typeof(ComboBox[]))
            {
                if (param.ComboType == TriggerParam.ComboContent.CsfLabel)
                {
                    SwapCombobox(true, controlIndex);
                }
                else
                {
                    SwapCombobox(false, controlIndex);
                    Cursor = Cursors.WaitCursor;
                    IList<object> data = GetComboCollections(param).ToList();
                    LoadToObjectCollection((ComboBox)controls[controlIndex], data);
                }
                SelectCombo((ComboBox)controls[controlIndex], param.GetParameter(paramData), param);
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
        private void SetLanguage()
        {
            foreach (Control c in Controls) Language.SetControlLanguage(c);
        }
        public void ClearContent()
        {
            foreach(Control c in Controls)
            {
                ClearContent(c);
            }
        }
        private void ClearContent(Control c)
        {
            Type t = c.GetType();
            if (t == typeof(TextBox) ||
                t == typeof(MaskedTextBox) ||
                t == typeof(RichTextBox)) c.Text = "";
            else if (t == typeof(GroupBox))
            {
                foreach (Control child in (c as GroupBox).Controls) ClearContent(child);
            }
            else if (t == typeof(ComboBox))
            {
                ComboBox cbb = c as ComboBox;
                if (cbb.Items.Count > 0) cbb.SelectedIndex = 0;
                else cbb.Text = "";
            }
        }
        private IEnumerable<object> GetComboCollections(TriggerParam param)
        {
            switch (param.ComboType)
            {
                case TriggerParam.ComboContent.Aircrafts:
                    return GlobalRules.AircraftList;
                case TriggerParam.ComboContent.Buildings:
                    return GlobalRules.BuildingList;
                case TriggerParam.ComboContent.Infantries:
                    return GlobalRules.InfantryList;
                case TriggerParam.ComboContent.Units:
                    return GlobalRules.VehicleList;
                case TriggerParam.ComboContent.SoundNames:
                    return GlobalSound.SoundList;
                case TriggerParam.ComboContent.EvaNames:
                    return GlobalSound.EvaList;
                case TriggerParam.ComboContent.ThemeNames:
                    return GlobalSound.ThemeList;
                case TriggerParam.ComboContent.LocalVar:
                    return Map.LocalVariables.ToTechno();
                case TriggerParam.ComboContent.SuperWeapons:
                    return GlobalRules.SuperWeaponList;
                case TriggerParam.ComboContent.CsfLabel:
                    return GlobalCsf.TechnoPairs;
                case TriggerParam.ComboContent.Triggers:
                    return Map.Triggers.ToTechno();
                case TriggerParam.ComboContent.Tags:
                    return Map.Tags.ToTechno();
                case TriggerParam.ComboContent.TechnoType:
                    return GlobalRules.TechnoList;
                case TriggerParam.ComboContent.GlobalVar:
                    return GlobalRules.GlobalVar;
                case TriggerParam.ComboContent.Teams:
                    return Map.Teams.ToTechno();
                case TriggerParam.ComboContent.Houses:
                    return Map.Countries.ToTechno();
                case TriggerParam.ComboContent.Animations:
                    return GlobalRules.AnimationList;
                case TriggerParam.ComboContent.ParticalAnim:
                    return GlobalRules.ParticalList;
                case TriggerParam.ComboContent.VoxelAnim:
                    return GlobalRules.VoxAnimList;
                case TriggerParam.ComboContent.BuildingID:
                    return GlobalRules.BuildingIDList;
                case TriggerParam.ComboContent.Movies:
                    return GlobalRules.MovieList;
                case TriggerParam.ComboContent.Warhead:
                    return GlobalRules.WarheadList;
                default:
                    return null;
            }
        }
        private void SelectCombo(ComboBox dest, string param, TriggerParam lookup)
        {
            switch (lookup.ComboType)
            {
                case TriggerParam.ComboContent.CsfLabel:
                    param = param.ToLower();
                    Select(dest, param);
                    break;
                case TriggerParam.ComboContent.SoundNames:
                case TriggerParam.ComboContent.EvaNames:
                case TriggerParam.ComboContent.ThemeNames:
                case TriggerParam.ComboContent.TechnoType:
                case TriggerParam.ComboContent.BuildingID:
                    Select(dest, param, false);
                    break;
                default:
                    Select(dest, param);
                    break;
            }
        }
        private void Select(ComboBox dest, string param, bool isIndex = true)
        {
            if (isIndex)
            {
                foreach (TechnoPair p in dest.Items)
                {
                    if (p.Index == param)
                    {
                        dest.SelectedItem = p;
                        return;
                    }
                }
            }
            else
            {
                foreach (TechnoPair p in dest.Items)
                {
                    if (p.RegName == param)
                    {
                        dest.SelectedItem = p;
                        return;
                    }
                }
            }
            dest.Text = param;
        }
        #endregion
        #region ParamWriting
        private void ParamChanged(object sender, EventArgs e)
        {
            if (!isParamRefreshing)
            {
                Type t = sender.GetType();
                int paramsindex = int.Parse(((Control)sender).Tag.ToString());
                TriggerParam param = (cbbEventAbst.SelectedItem as TriggerDescription).Parameters[paramsindex];
                int i = param.ParamPos;
                bool isBase26 = param.Type == TriggerParam.ParamType.Waypoint;
                if (t == typeof(TextBox))
                {
                    string text = ((TextBox)sender).Text;
                    WriteParam(text, i, isBase26);
                }
                else if (t == typeof(ComboBox))
                {
                    TechnoPair p = ((ComboBox)sender).SelectedItem as TechnoPair;
                    if (p == null) p = new TechnoPair(((ComboBox)sender).Text, "");
                    WriteParam(p, i);
                }
                else if (t == typeof(CheckBox))
                {
                    WriteParam(((CheckBox)sender).Checked, i);
                }
            }
        }
        private void WriteParam(string value, int pos, bool base26 = false)
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
            CurrentItem.Parameters[pos] = value;
        }
        private void WriteParam(TechnoPair value, int pos)
        {
            WriteParam(value.Index, pos);
        }
        private void WriteParam(bool value, int pos)
        {
            CurrentItem.Parameters[pos] = value ? "1" : "0";
        }
        #endregion

        private bool isUpdatingIndex = false;
        private void cbbEventAbst_SelectedIndexChanged(object sender, EventArgs e)
        {
            isUpdatingIndex = true;
            if (lbxReferance.Items.Count == 0) return;
            TriggerDescription desc = cbbEventAbst.SelectedItem as TriggerDescription;
            int evid = desc.ID;
            mtxbEventID.Text = evid.ToString();
            if (CurrentItem.ID != evid)
            {
                CurrentItem.ID = evid;
                CurrentItem.Parameters = desc.InitParams;
            }
            
            OnLogicItemChanged();
            AbstractChanged();
            isUpdatingIndex = false;
        }

        private void mtxbEventID_Validated(object sender, EventArgs e)
        {
            if (!isUpdatingIndex)
            {
                if (string.IsNullOrEmpty(mtxbEventID.Text)) return;
                int i = int.Parse(mtxbEventID.Text);
                if (cbbEventAbst.Items.Count > i) cbbEventAbst.SelectedIndex = i;
            }
        }

        private void lkl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int tagid = int.Parse(((LinkLabel)sender).Tag.ToString());
            TechnoPair p;
            TriggerDescription desc;
            p = cbbs[tagid].SelectedItem as TechnoPair;
            desc = cbbEventAbst.SelectedItem as TriggerDescription;
            TriggerParam param = desc.Parameters[tagid];
            switch (param.ComboType)
            {
                case TriggerParam.ComboContent.SoundNames:
                case TriggerParam.ComboContent.ThemeNames:
                case TriggerParam.ComboContent.EvaNames:
                    OnPlayingSound(param, p);
                    break;
                case TriggerParam.ComboContent.Triggers:
                    string triggerid = p.Index;
                    TriggerItem trigger = Map.Triggers[triggerid];
                    OnTracingTrigger(trigger);
                    break;
            }
            if (param.Type == TriggerParam.ParamType.Waypoint)
            {
                string wpid = txbs[tagid].Text;
                WaypointItem wp = Map.Waypoints.FindByID(wpid);
                OnWaypointJump(wp);
            }
        }
    }
}