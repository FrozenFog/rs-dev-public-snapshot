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

namespace RelertSharp.GUI.SubWindows.LogicEditor
{
    public partial class dlgCopy : Form
    {
        public dlgCopy()
        {
            InitializeComponent();
            foreach (Control c in Controls) SetControlLanguage(c);
            Text = DICT[Text];
        }
        public string Result { get { return rtxbData.Text; } }
    }
}
