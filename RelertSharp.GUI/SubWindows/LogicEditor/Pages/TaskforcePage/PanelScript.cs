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
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    internal partial class PanelScript : UserControl
    {
        internal TeamScriptGroup CurrentCollection { get; set; }
        internal TeamScriptItem CurrentItem { get; set; }


        public PanelScript()
        {
            InitializeComponent();
        }

        public void Initialize(IEnumerable<TriggerDescription> scripts)
        {
            pnlParam.Initialize(scripts);
            SetLanguage();
        }
        public void Reload(TeamScriptGroup item)
        {
            CurrentCollection = item;
            RefreshControl();
        }


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
        }

        private bool updatingLbxScriptList = false;
        private void lbxScriptList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!isControlRefreshing && !updatingLbxScriptList)
            {
                CurrentItem = lbxScriptList.SelectedItem as TeamScriptItem;
                pnlParam.Reload(CurrentItem);
            }
        }
    }
}
