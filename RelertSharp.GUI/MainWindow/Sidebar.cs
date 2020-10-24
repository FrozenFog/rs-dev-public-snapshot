using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest
    {
        private const string ScrollPanel = "scroll";



        private bool isSidebarResizing = false;
        private int resizeY = 0;
        private void PanelResizer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isSidebarResizing = true;
                resizeY = e.Y;
            }
        }
        private void PanelResizer_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSidebarResizing && (sender as Control).Parent is Panel container)
            {
                Panel up = container.FirstChild<Panel>(x => x.Tag as string == ScrollPanel);
                int dY = e.Y - resizeY;
                bool b = container.Size.Height > 45 ? true : dY > 0;
                if (b && up != null)
                {
                    Size sz = container.Size;
                    Size uSz = up.Size;
                    sz.Height += dY;
                    uSz.Height += dY;
                    container.Size = sz;
                    up.Size = uSz;
                    container.Refresh();
                }
            }
        }
        private void PanelResizer_MouseUp(object sender, MouseEventArgs e)
        {
            isSidebarResizing = false;
        }

        private void Panelchecker_CheckedChanged0(object sender, EventArgs e)
        {
            Panel parent = (Panel)((CheckBox)sender).Parent;
            foreach (Control c in parent.Controls)
            {
                if (!c.Equals(sender)) c.Visible = (sender as CheckBox).Checked;
            }
        }
    }
}
