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
    internal partial class PanelScript : UserControl
    {
        internal event SoundPlayingHandler NeedPlayingSound;
        internal event I2dLocateableHandler JumpToWaypoint;
        internal event ScriptGroupHandler ScriptNameUpdated;
        internal event ScriptGroupHandler NewScriptAdded;
        internal event EventHandler ScriptRemoved;


        internal TeamScriptGroup CurrentCollection { get; set; }
        internal TeamScriptItem CurrentItem { get; set; }
        private Map Map { get { return GlobalVar.CurrentMapDocument.Map; } }


        public PanelScript()
        {
            InitializeComponent();
        }

        public void Initialize(IEnumerable<TriggerDescription> scripts)
        {
            pnlParam.Initialize(scripts);
            pnlParam.ScriptUpdating += PnlParam_ScriptUpdating;
            pnlParam.NeedPlayingSound += PnlParam_NeedPlayingSound;
            pnlParam.JumpToWaypoint += PnlParam_JumpToWaypoint;
            pnlParam.NewScriptAdded += PnlParam_NewScriptAdded;
            pnlParam.ScriptDeleted += PnlParam_ScriptDeleted;
            SetLanguage();
        }

        private void PnlParam_ScriptDeleted(object sender, EventArgs e)
        {
            RemoveAt(lbxScriptList, ref updatingLbxScriptList);
            if (lbxScriptList.Items.Count == 0) pnlParam.Reload(null, CurrentCollection, -1);
        }

        private void PnlParam_NewScriptAdded(object sender, TeamScriptItem script)
        {
            InsertAt(lbxScriptList, script, ref updatingLbxScriptList);
        }

        private void PnlParam_JumpToWaypoint(object sender, I2dLocateable pos)
        {
            JumpToWaypoint?.Invoke(sender, pos);
        }

        private void PnlParam_NeedPlayingSound(object sender, TriggerParam param, TechnoPair p)
        {
            NeedPlayingSound?.Invoke(sender, param, p);
        }

        private void PnlParam_ScriptUpdating(object sender, TeamScriptItem script)
        {
            UpdateAt(lbxScriptList, script, ref updatingLbxScriptList);
        }

        public void Reload(TeamScriptGroup item)
        {
            CurrentCollection = item;
            RefreshControl();
        }


        #region OnEvent
        protected virtual void OnScriptNameUpdated()
        {
            ScriptNameUpdated?.Invoke(this, CurrentCollection);
        }
        protected virtual void OnNewScriptAdded()
        {
            NewScriptAdded?.Invoke(this, CurrentCollection);
        }
        protected virtual void OnScriptRemoved()
        {
            ScriptRemoved?.Invoke(this, new EventArgs());
        }
        #endregion


        private void SetLanguage()
        {
            foreach (Control c in Controls) Language.SetControlLanguage(c);
        }
        private bool isControlRefreshing = false;
        private void RefreshControl()
        {
            isControlRefreshing = true;
            if (CurrentCollection == null)
            {
                foreach (Control c in Controls) ClearControlContent(c);
            }
            else
            {
                txbScriptID.Text = CurrentCollection.ID;
                txbScriptName.Text = CurrentCollection.Name;
                LoadToObjectCollection(lbxScriptList, CurrentCollection.Data);
            }
            isControlRefreshing = false;
            if (CurrentCollection.Data.Count > 0) lbxScriptList.SelectedIndex = 0;
            else pnlParam.Reload(null, CurrentCollection, -1);
        }

        private bool updatingLbxScriptList = false;
        private void lbxScriptList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!isControlRefreshing && !updatingLbxScriptList)
            {
                CurrentItem = lbxScriptList.SelectedItem as TeamScriptItem;
                pnlParam.Reload(CurrentItem, CurrentCollection, lbxScriptList.SelectedIndex);
            }
        }

        private void txbScriptName_Validated(object sender, EventArgs e)
        {
            if (CurrentCollection != null && !isControlRefreshing)
            {
                CurrentCollection.Name = txbScriptName.Text;
                OnScriptNameUpdated();
            }
        }

        private void btnAddScriptMem_Click(object sender, EventArgs e)
        {
            TeamScriptGroup group = Map.NewScript();
            CurrentCollection = group;
            OnNewScriptAdded();
        }

        private void btnDelScriptMem_Click(object sender, EventArgs e)
        {
            if (CurrentCollection != null)
            {
                Map.RemoveScript(CurrentCollection);
                CurrentCollection = null;
                OnScriptRemoved();
            }
        }

        private void btnCopyScriptMem_Click(object sender, EventArgs e)
        {
            if (CurrentCollection != null)
            {
                TeamScriptGroup copied = Map.NewScript(CurrentCollection);
                CurrentCollection = copied;
                OnNewScriptAdded();
            }
        }
    }
}
