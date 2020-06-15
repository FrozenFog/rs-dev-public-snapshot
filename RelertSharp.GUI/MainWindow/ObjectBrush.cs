using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.Common;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest
    {
        public void MoveBrushObjectTo(MouseEventArgs e)
        {
            if (drew)
            {
                I3dLocateable cell;
                Vec3 pos = GlobalVar.Engine.ClientPointToCellPos(e.Location, out int subcell);
                if (pos == Vec3.Zero)
                {
                    cell = GlobalVar.Engine.GetPreviousLegalTile();
                    subcell = 1;
                }
                else cell = pos.To3dLocateable();
                pnlPick.MoveBurhObjectTo(cell, subcell);
            }
        }
    }
}
