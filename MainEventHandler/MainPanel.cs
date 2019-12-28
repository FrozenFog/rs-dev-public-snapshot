using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace relert_sharp
{
    public partial class MainWindow
    {
        #region EventHandler - pnlInMain
        private void pnlInMain_Paint(object sender, PaintEventArgs e)
        {
            pnlInMain.MakeCurrent();

            GL.Clear(ClearBufferMask.ColorBufferBit);
            pnlInMain.SwapBuffers();
        }
        private void pnlInMain_Resize(object sender, EventArgs e)
        {
            if (pnlInMain.ClientSize.Height == 0)
                pnlInMain.ClientSize = new Size(pnlInMain.ClientSize.Width, 1);

            GL.Viewport(0, 0, pnlInMain.ClientSize.Width, pnlInMain.ClientSize.Height);
        }
        #endregion
    }
}
