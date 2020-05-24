using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelertSharp.GUI.RbPanel
{
    public partial class RbPanelBase : UserControl
    {
        public RbPanelBase()
        {
            InitializeComponent();
            Visible = false;
        }

        protected virtual void SetLanguage()
        {
            foreach(Control c in Controls)
            {
                Language.SetControlLanguage(c);
            }
        }
    }
}
