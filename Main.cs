using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using relert_sharp.SubWindows;
using relert_sharp.Utils;

namespace relert_sharp
{
    public partial class Main : Form
    {
        public Cons.Language lang = Cons.Language.Chinese;
        public Main()
        {
            InitializeComponent();
        }

        private void btnOpenINIComp_Click(object sender, EventArgs e)
        {
            INIComparator c = new INIComparator(lang);
            c.ShowDialog();
        }
    }
}
