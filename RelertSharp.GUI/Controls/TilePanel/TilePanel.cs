using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.MapStructure;
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.GUI.GuiUtils;

namespace RelertSharp.GUI.Controls
{
    public partial class TilePanel : UserControl
    {
        private bool isFramework = false;
        private Map Map { get { return CurrentMapDocument.Map; } }
        public TilePanel()
        {
            InitializeComponent();
        }


        public void Initialize()
        {
            SetLanguage();

            InitializeAllTilePanel();
            InitializeGeneralTilePanel();
        }
        public void SetFramework(bool frameworkEnable)
        {
            isFramework = frameworkEnable;
            cbbAllTiles_SelectedIndexChanged(cbbAllTiles, new EventArgs());
        }


        private void SetLanguage()
        {
            foreach (Control c in Controls) Language.SetControlLanguage(c);
        }
    }
}
