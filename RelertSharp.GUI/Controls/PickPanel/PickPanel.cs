using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.Common;
using RelertSharp.IniSystem;
using RelertSharp.GUI.Model.BrushModel;
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI.Controls
{
    public partial class PickPanel : UserControl
    {
        private Dictionary<string, string> regname_pcx = new Dictionary<string, string>();
        private BrushModel brush = new BrushModel();
        private bool drew = false;


        public PickPanel()
        {
            InitializeComponent();
        }


        public void Initialize()
        {
            SetLanguage();
            InitializeGeneralPanel();
        }
        public void DrawComplete()
        {
            drew = true;
        }
        private void SetLanguage()
        {
            foreach (Control c in Controls) Language.SetControlLanguage(c);
        }


        public BrushModel Result { get { return brush; } }
    }
}
