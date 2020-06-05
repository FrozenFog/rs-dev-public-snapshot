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
        internal TeamScriptItem CurrentScript { get; set; }
        private TriggerDescription CurrentDesc { get { return cbbScriptType.SelectedItem as TriggerDescription; } }
        private Map Map { get { return GlobalVar.CurrentMapDocument.Map; } }


        public PanelScriptParam()
        {
            InitializeComponent();
        }

        public void Initialize(IEnumerable<TriggerDescription> scripts)
        {
            isControlRefreshing = true;
            SetLanguage();
            LoadToObjectCollection(cbbScriptType, scripts);
            cbbScriptType.SelectedIndex = 0;
            isControlRefreshing = false;
        }
        public void Reload(TeamScriptItem item)
        {
            CurrentScript = item;
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
            if (CurrentScript == null)
            {
                lklParamName.Visible = false;
                cbbParam.Visible = false;
                txbParam.Visible = false;
                lblNa.Visible = true;
                lblParamName.Text = Language.DICT["LGClblScriptCurPara"];
                foreach (Control c in Controls) ClearControlContent(c);
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
                    case TriggerParam.ParamType.PlainString:
                        txbParam.Visible = true;
                        txbParam.Text = CurrentScript.ActionValue;
                        break;
                    case TriggerParam.ParamType.SelectableString:
                        cbbParam.Visible = true;
                        IEnumerable<object> data = Map.GetComboCollections(CurrentDesc.Parameters[0]);
                        LoadToObjectCollection(cbbParam, data);
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
            if (!isControlRefreshing)
            {
                indexUpdating = true;
                mtxbScriptID.Text = cbbScriptType.SelectedIndex.ToString();
                RefreshControl();
                indexUpdating = false;
            }
        }

        private void mtxbScriptID_Validated(object sender, EventArgs e)
        {
            if (!isControlRefreshing && !indexUpdating)
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
    }
}
