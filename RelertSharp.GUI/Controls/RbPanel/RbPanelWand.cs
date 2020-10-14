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
    public partial class RbPanelWand : RbPanelBase
    {
        public RbPanelWand()
        {
            InitializeComponent();
        }
        public void Initialize()
        {
            SetLanguage();
        }


        public bool SameHeight { get { return ckbSameZ.Checked; } }
        public bool SameSet { get { return ckbSameSet.Checked; } }
        public bool SameIndex { get { return ckbSameIndex.Checked; } }
    }
}
