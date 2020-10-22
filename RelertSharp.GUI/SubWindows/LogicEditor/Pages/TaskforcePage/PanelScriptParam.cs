using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;
using RelertSharp.IniSystem;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    internal partial class PanelScriptParam : UserControl
    {
        internal event ScriptHandler ScriptUpdating;
        internal event SoundPlayingHandler NeedPlayingSound;
        internal event I2dLocateableHandler JumpToWaypoint;
        internal event ScriptHandler NewScriptAdded;
        internal event EventHandler ScriptDeleted;


        internal TeamScriptItem CurrentScript { get; set; }
        private TriggerDescription CurrentDesc { get { return cbbScriptType.SelectedItem as TriggerDescription; } }
        private Map Map { get { return GlobalVar.CurrentMapDocument.Map; } }
        private TeamScriptGroup ParentGroup { get; set; }


        private int currentIndex = -1;


        public PanelScriptParam()
        {
            InitializeComponent();
        }

        public void Initialize(IEnumerable<TriggerDescription> scripts)
        {
            isControlRefreshing = true;
            SetLanguage();
            cbbScriptType.LoadAs(scripts);
            cbbScriptType.SelectedIndex = 0;
            isControlRefreshing = false;
        }
        public void Reload(TeamScriptItem item, TeamScriptGroup parent, int selectedIndex)
        {
            CurrentScript = item;
            ParentGroup = parent;
            currentIndex = selectedIndex;
            RefreshControl();
        }
        #region OnEvent
        protected virtual void OnScriptUpdated()
        {
            ScriptUpdating?.Invoke(this, CurrentScript);
        }
        protected virtual void OnSoundPlaying()
        {
            if (cbbParam.SelectedItem is TechnoPair p)
            {
                NeedPlayingSound?.Invoke(this, CurrentDesc.Parameters[0], p);
            }
        }
        protected virtual void OnJumpToWaypoint(I2dLocateable cell)
        {
            JumpToWaypoint?.Invoke(this, cell);
        }
        protected virtual void OnScriptAdded()
        {
            NewScriptAdded?.Invoke(this, CurrentScript);
        }
        protected virtual void OnScriptDeleted()
        {
            ScriptDeleted?.Invoke(this, new EventArgs());
        }
        #endregion


        private void SetLanguage()
        {
            foreach (Control c in Controls) c.SetLanguage();
        }
        private bool isControlRefreshing = false;
        private void RefreshControl()
        {
            isControlRefreshing = true;
            if (CurrentScript == null)
            {
                lklParamName.Visible = false;
                cbbParam.Visible = false;
                txbParam.Visible = false;
                lblNa.Visible = true;
                lblParamName.Text = Language.DICT["LGClblScriptCurPara"];
                lblParamName.Visible = true;
                currentIndex = -1;
                foreach (Control c in Controls) c.ClearContent();
            }
            else
            {
                mtxbScriptID.Text = CurrentScript.ScriptActionIndex.ToString();
                cbbScriptType.SelectedIndex = CurrentScript.ScriptActionIndex;
                rtxbScriptDesc.Text = CurrentDesc.Description;
                rtxbScriptDesc.Refresh();
                LoadScriptParam();
            }
            isControlRefreshing = false;
        }
        private void LoadScriptParam()
        {
            TriggerDescription desc = CurrentDesc;
            lblNa.Visible = false;
            cbbParam.Visible = false;
            txbParam.Visible = false;
            if (CurrentDesc.Parameters.Count == 0)
            {
                lblNa.Visible = true;
                lklParamName.Visible = false;
                lblParamName.Visible = true;
                lblParamName.Text = Language.DICT["LGClblScriptCurPara"];
            }
            else
            {
                switch (desc.Parameters[0].Type)
                {
                    case TriggerParam.ParamType.Waypoint:
                    case TriggerParam.ParamType.PlainString:
                    case TriggerParam.ParamType.CellPos:
                        txbParam.Visible = true;
                        txbParam.Text = CurrentScript.ActionValue;
                        break;
                    case TriggerParam.ParamType.SelectableString:
                        cbbParam.Visible = true;
                        IEnumerable<TechnoPair> data = Map.GetComboCollections(CurrentDesc.Parameters[0]);
                        cbbParam.LoadAs(data);
                        cbbParam.Text = CurrentScript.ActionValue;
                        break;
                }
                if (desc.Parameters[0].Traceable)
                {
                    lklParamName.Visible = true;
                    lblParamName.Visible = false;
                    lklParamName.Text = Language.DICT[desc.Parameters[0].Name];
                }
                else
                {
                    lklParamName.Visible = false;
                    lblParamName.Visible = true;
                    lblParamName.Text = Language.DICT[desc.Parameters[0].Name];
                }
            }
        }
        private bool indexUpdating = false;
        private void cbbScriptType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!isControlRefreshing && CurrentScript != null)
            {
                indexUpdating = true;
                mtxbScriptID.Text = cbbScriptType.SelectedIndex.ToString();
                CurrentScript.ScriptActionIndex = cbbScriptType.SelectedIndex;
                CurrentScript.ActionValue = CurrentDesc.InitParams[0];
                rtxbScriptDesc.Text = CurrentDesc.Description;
                rtxbScriptDesc.Refresh();
                isControlRefreshing = true;
                LoadScriptParam();
                isControlRefreshing = false;
                indexUpdating = false;
                OnScriptUpdated();
            }
        }

        private void mtxbScriptID_Validated(object sender, EventArgs e)
        {
            if (!isControlRefreshing && !indexUpdating && CurrentScript != null)
            {
                try
                {
                    int i = int.Parse(mtxbScriptID.Text);
                    if (i < cbbScriptType.Items.Count) cbbScriptType.SelectedIndex = i;
                }
                catch
                {
                    mtxbScriptID.Text = "0";
                    cbbScriptType.SelectedIndex = 0;
                }
            }
        }
        private void ParamChanged(object sender, EventArgs e)
        {
            if (!isControlRefreshing && CurrentScript != null)
            {
                if (sender.GetType() == typeof(TextBox))
                {
                    TextBox b = sender as TextBox;
                    CurrentScript.ActionValue = b.Text;
                }
                else
                {
                    ComboBox cbb = sender as ComboBox;
                    if (cbb.SelectedItem is TechnoPair p)
                    {
                        CurrentScript.ActionValue = p.Index;
                    }
                    else
                    {
                        CurrentScript.ActionValue = cbb.Text;
                    }
                }
                OnScriptUpdated();
            }
        }

        private void lklParamName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (CurrentScript != null)
            {
                switch (CurrentDesc.Parameters[0].ComboType)
                {
                    case TriggerParam.ComboContent.EvaNames0:
                    case TriggerParam.ComboContent.SoundNames0:
                    case TriggerParam.ComboContent.ThemeNames0:
                        OnSoundPlaying();
                        break;
                }
                if (CurrentDesc.Parameters[0].Type == TriggerParam.ParamType.Waypoint)
                {
                    I2dLocateable cell = Map.Waypoints.FindByID(txbParam.Text);
                    OnJumpToWaypoint(cell);
                }
                else if (CurrentDesc.Parameters[0].Type == TriggerParam.ParamType.CellPos)
                {
                    try
                    {
                        int coord = int.Parse(txbParam.Text);
                        Pnt cell = new Pnt(Utils.Misc.Coord128X(coord), Utils.Misc.Coord128Y(coord));
                        OnJumpToWaypoint(cell);
                    }
                    catch { }
                }
            }
        }

        private void btnNewScript_Click(object sender, EventArgs e)
        {
            if (ParentGroup != null)
            {
                CurrentScript = ParentGroup.NewScript(currentIndex);
                OnScriptAdded();
            }
        }

        private void btnDelScript_Click(object sender, EventArgs e)
        {
            if (ParentGroup != null && ParentGroup.Data.Count > 0)
            {
                CurrentScript = null;
                ParentGroup.RemoveScript(currentIndex);
                OnScriptDeleted();
            }
        }

        private void btnCopyScript_Click(object sender, EventArgs e)
        {
            if (ParentGroup != null && CurrentScript != null)
            {
                TeamScriptItem newitem = ParentGroup.NewScript(currentIndex, CurrentScript);
                OnScriptAdded();
            }
        }
    }
}
