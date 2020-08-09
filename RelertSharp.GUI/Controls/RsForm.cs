using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.Utils;

namespace RelertSharp.GUI.Controls
{
    /// <summary>
    /// *obsolete*
    /// Aimed at optimizing rendering effecient
    /// </summary>
    class RsForm : Form
    {
        #region Delegates
        public delegate void FormStateChangeBeginHandler(object sender, EventArgs e);
        public delegate void FormStateChangeEndHandler(object sender, EventArgs e);
        #endregion

        #region Events
        public event FormStateChangeBeginHandler FormStateChangeBegin;
        public event FormStateChangeEndHandler FormStateChangeEnd;
        #endregion

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
        }
    }
}
