using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static relert_sharp.Language;

namespace relert_sharp.SubWindows
{
    public partial class LogicEditor : Form
    {
        public LogicEditor()
        {
            InitializeComponent();
            Set_Language();
        }


        #region Private Methods - LogicEditor
        private void Set_Language()
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
    }
}
