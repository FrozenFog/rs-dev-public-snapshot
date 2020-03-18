using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.SubWindows;
using RelertSharp.MapStructure;
using RelertSharp.FileSystem;
using RelertSharp.Utils;

namespace RelertSharp
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnOpenINIComp_Click(object sender, EventArgs e)
        {
            INIComparator c = new INIComparator();
            c.ShowDialog();
        }
    }
}
