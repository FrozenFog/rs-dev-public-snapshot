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
                requireFocus = true;
            }
        }
        private Vec3 prevMoveCellTileBrush = Vec3.Zero;
        private void MoveTileBrushObjectTo(Vec3 cell)
        {
            if (cell != Vec3.Zero && prevMoveCellTileBrush != cell)
            {
                prevMoveCellTileBrush = cell;
                pnlTile.Result.MoveTo(cell.To3dLocateable());
                Engine.Refresh();
            }
        }
        private void AddTileToPos(Vec3 cell)
        {
            if (cell != Vec3.Zero)
            {
                pnlTile.Result.AddTileAt(cell.To3dLocateable(), out List<object> newtiles, out List<object> orgTiles);
                Current.UndoRedo.PushCommand(Model.UndoRedoCommandType.DrawTile, orgTiles, newtiles);
                RedrawMinimapAll();
                Engine.Refresh();
            }
        }
    }
}
