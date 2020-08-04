using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelertSharp.SubWindows.INIEditor
{
    public partial class INIEditor : Form
    {
        public INIEditor()
        {
            InitializeComponent();
        }

        private void INIEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }


    }
}
