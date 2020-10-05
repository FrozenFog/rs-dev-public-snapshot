using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.MapStructure;
using RelertSharp.Common;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.GUI
{
    public partial class MapVerifyForm : Form
    {
        private Map Map { get { return CurrentMapDocument.Map; } }
        private ListViewComparer lvComparer;
        internal event LogicTraceHandler TraceLogic;
        internal event I2dLocateableHandler TraceMapPosition;


        public MapVerifyForm()
        {
            InitializeComponent();
            InitializeControl();
        }

        public void CheckMap()
        {
            Wait = true;
            lvVerifyResult.BeginUpdate();
            lvVerifyResult.Items.Clear();
            List<VerifyResultItem> items = Map.Verify();
            foreach (VerifyResultItem v in items)
            {
                VerifyViewItem lvi = new VerifyViewItem
                {
                    ImageKey = v.Level.ToString(),
                    Text = v.VerifyType.ToString().ToLang()
                };
                lvi.SubItems.Add(v.Message);
                lvi.SubItems.Add(v.Level.ToString());
                lvi.SubItems.Add(v.LogicType == null ? v.Pos.FormatXY() : string.Format("{0}:{1}",v.LogicType, v.IdNavigator));

                lvi.Instance = v;

                lvVerifyResult.Items.Add(lvi);
            }
            lvVerifyResult.AutoColumnWidth();
            lvVerifyResult.EndUpdate();
        }



        private void InitializeControl()
        {
            imgErrors.Images.Add("Success", Properties.Resources.verifyInform);
            imgErrors.Images.Add("Suggest", Properties.Resources.verifyInform);
            imgErrors.Images.Add("Warning", Properties.Resources.verifyWarning);
            imgErrors.Images.Add("Critical", Properties.Resources.verifyError);
            imgErrors.Images.Add("CannotBeSaved", Properties.Resources.verifyFatal);

            lvComparer = new ListViewComparer();
            lvVerifyResult.ListViewItemSorter = lvComparer;
            foreach (Control c in this.Controls) c.SetLanguage();
            this.SetLanguage();
        }

        private void lvVerifyResult_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvComparer.TargetCol)
            {
                if (lvComparer.Order == SortOrder.Ascending) 
                    lvComparer.Order = SortOrder.Descending;
                else 
                    lvComparer.Order = SortOrder.Ascending;
            }
            else
            {
                lvComparer.TargetCol = e.Column;
                lvComparer.Order = SortOrder.Ascending;
            }
            lvVerifyResult.Sort();
        }

        private void lvVerifyResult_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvVerifyResult.SelectedItems.Count > 0)
            {
                VerifyViewItem item = lvVerifyResult.SelectedItems[0] as VerifyViewItem;
                VerifyResultItem instance = item.Instance;
                if (instance.LogicType == null)
                {
                    TraceMapPosition.Invoke(this, instance.Pos);
                }
                else
                {
                    TraceLogic.Invoke(this, instance.LogicType.Value, instance.IdNavigator);
                }
            }
        }



        private class VerifyViewItem : ListViewItem
        {
            public VerifyResultItem Instance { get; set; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Hide();
            Wait = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Hide();
            Wait = false;
        }
        private void MapVerifyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }




        public bool Wait { get; private set; } = true;

    }
}
