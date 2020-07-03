using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.Common;
using RelertSharp.MapStructure;
using RelertSharp.DrawingEngine.Presenting;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest
    {
        private void PnlTile_NewTileSelected(object sender, TileSet set, int index)
        {
            if (drew)
            {
                pnlTile.Result.Reload(set, index);
            }
        }
        private void MoveTileBrushObjectTo(Vec3 cell)
        {
            if (cell != Vec3.Zero)
            {
                pnlTile.Result.MoveTo(cell.To3dLocateable());
                Engine.Refresh();
            }
        }
        private void AddTileToPos(Vec3 cell)
        {
            if (cell != Vec3.Zero)
            {
                pnlTile.Result.AddTileAt(cell.To3dLocateable());
                Engine.RedrawMinimapAll();
                pnlMiniMap.BackgroundImage = Engine.MiniMap;
                Engine.Refresh();
            }
        }
    }
}
