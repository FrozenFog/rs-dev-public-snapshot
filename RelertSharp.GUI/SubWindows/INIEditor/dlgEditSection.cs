using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RelertSharp.Language;

namespace RelertSharp.SubWindows.INIEditor
{
    public partial class dlgEditSection : Form
    {
        public dlgEditSection(string origin)
        {
            InitializeComponent();
            InitializeLanguage();
            txbSectionName.Text = origin;
        }

        private void InitializeLanguage()
        {
            foreach (Control c in Controls) c.SetLanguage();
            Text = Text.ToLang();
        }

        private void Confirm()
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Decline()
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e) => Confirm();

        private void btnCancel_Click(object sender, EventArgs e) => Decline();

        private void txbSectionName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Confirm();
            else if (e.KeyCode == Keys.Escape) Decline();
        }
    }
}
