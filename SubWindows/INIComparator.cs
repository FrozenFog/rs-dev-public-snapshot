using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using relert_sharp.Utils;
using relert_sharp.FileSystem;

namespace relert_sharp.SubWindows
{
    public partial class INIComparator : Form
    {
        public INIComparator()
        {
            InitializeComponent();
        }

        private void btnSelectPath1_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Title = "Select INI File";
            o.Filter = "INI File|*.ini|Map File|*.map,*.yrm,*.mpr";
            o.InitialDirectory = Application.StartupPath;
            if (DialogResult.OK == o.ShowDialog())
            {
                txbINIAPath.Text = o.FileName;
            }
        }

        private void btnSelectPath2_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Title = "Select INI File";
            o.Filter = "INI File|*.ini|Map File|*.map,*.yrm,*.mpr";
            o.InitialDirectory = Application.StartupPath;
            if (DialogResult.OK == o.ShowDialog())
            {
                txbINIBPath.Text = o.FileName;
            }
        }

        private void btnRunCompare_Click(object sender, EventArgs e)
        {
            INIFile f1 = new INIFile(txbINIAPath.Text);
            INIFile f2 = new INIFile(txbINIBPath.Text);
        }
    }
}
