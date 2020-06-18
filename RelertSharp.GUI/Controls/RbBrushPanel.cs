using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelertSharp.GUI.Controls
{
    internal partial class RbBrushPanel : RbPanelBase
    {
        public RbBrushPanel()
        {
            InitializeComponent();
        }
        public void Initialize()
        {
            SetLanguage();
        }


        public bool IsSimulating { get { return ckbSimBud.Checked; } }
        public bool AddBaseNode { get { return ckbNode.Checked; } }
        public bool IgnoreBuilding { get { return ckbIgnoreBuilding.Checked; } }
    }
}
