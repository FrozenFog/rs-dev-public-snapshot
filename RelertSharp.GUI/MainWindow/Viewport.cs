using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.DrawingEngine;
using RelertSharp.FileSystem;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Points;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Logic;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using RelertSharp.GUI.Model;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest
    {
        private Point selectorBoxLT = Point.Empty;
        private bool isSelecting = false;


        private void SceneSelectionBoxSet(MouseEventArgs e)
        {
            if (drew)
            {
                selectorBoxLT = e.Location;
                isSelecting = true;
            }
        }
        private void DrawSelectingBoxOnScene(MouseEventArgs now)
        {
            if (isSelecting)
            {
                GlobalVar.Engine.DrawSelectingRectangle(Pnt.FromPoint(selectorBoxLT), Pnt.FromPoint(now.Location), Current.SelectingBoxFlag == MainWindowDataModel.SelectingBoxMode.IsometricRectangle);
            }
        }
        private void SelectSceneItemsInsideBox(MouseEventArgs releasePoint)
        {
            if (drew && isSelecting)
            {
                GlobalVar.Engine.ReleaseDrawingRectangle();
                var mode = Current.SelectingFlags;
                IEnumerable<I2dLocateable> iter;
                if (Current.SelectingBoxFlag == MainWindowDataModel.SelectingBoxMode.ClientRectangle) iter = new SceneSquare2D(selectorBoxLT, releasePoint.Location, map.Info.Size.Width);
                else
                {
                    Vec3 up = GlobalVar.Engine.ClientPointToCellPos(selectorBoxLT);
                    Vec3 down = GlobalVar.Engine.ClientPointToCellPos(releasePoint.Location);
                    iter = new Square2D(up.To2dLocateable(), down.To2dLocateable());
                }
                foreach (I2dLocateable pos in iter)
                {
                    SelectAt(pos, mode, releasePoint, false);
                }
                GlobalVar.Engine.Refresh();
                isSelecting = false;
            }
        }
        private void PreciseSelecting(MouseEventArgs e)
        {
            if (drew)
            {
                var flag = Current.SelectingFlags;
                if (flag == MainWindowDataModel.SelectingFlag.None) return;
                I2dLocateable pos = GlobalVar.Engine.ClientPointToCellPos(e.Location).To2dLocateable();
                SelectAt(pos, flag, e, true);
                GlobalVar.Engine.Refresh();
            }
        }
        private void SelectAt(I2dLocateable pos, MainWindowDataModel.SelectingFlag flag, MouseEventArgs e, bool isPrecise)
        {
            if ((flag | MainWindowDataModel.SelectingFlag.Units) != 0)
            {
                Current.SelectUnitAt(pos);
            }
            if ((flag | MainWindowDataModel.SelectingFlag.Infantries) != 0)
            {
                if (isPrecise)
                {
                    I2dLocateable infpos = GlobalVar.Engine.ClientPointToCellPos(e.Location, out int subcell).To2dLocateable();
                    Current.SelectInfantryAt(infpos, subcell);
                }
                else
                {
                    Current.SelectInfantryAt(pos, 1);
                    Current.SelectInfantryAt(pos, 2);
                    Current.SelectInfantryAt(pos, 3);
                }
            }
            if ((flag | MainWindowDataModel.SelectingFlag.Buildings) != 0)
            {
                Current.SelectBuildingAt(pos);
            }
            if ((flag | MainWindowDataModel.SelectingFlag.Terrains) != 0)
            {
                Current.SelectTerrainAt(pos);
            }
            if ((flag | MainWindowDataModel.SelectingFlag.Overlays) != 0)
            {
                Current.SelectOverlayAt(pos);
            }
        }
    }
}
