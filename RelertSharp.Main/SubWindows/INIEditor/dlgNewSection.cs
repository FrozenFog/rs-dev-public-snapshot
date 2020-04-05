using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.FileSystem;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;
using RelertSharp.IniSystem;
using static RelertSharp.Language;

namespace RelertSharp.SubWindows.INIEditor
{
    public partial class dlgNewSection : Form
    {
        public dlgNewSection()
        {
            InitializeComponent();
            foreach (Control c in Controls) SetControlLanguage(c);
            Text = DICT[Text];
            
        }

        private void btnNewSectionC_Click(object sender, EventArgs e)
        {
            retSectionName = txbNewSection.Text.Trim();
            DialogResult = DialogResult.OK;
            this.Close();
        }

        public string retSectionName { get; protected set; }
    }
}
