using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest
    {
        private void LogicJumpToWaypoint(object logic, I2dLocateable cell)
        {
            if (drew && cell != null)
            {
                int height = Map.GetHeightFromTile(cell);
                GlobalVar.Engine.MoveTo(cell, height);
                pnlMiniMap.BackgroundImage = GlobalVar.Engine.MiniMap;
                GlobalVar.Engine.Refresh();
            }
        }
    }
}
