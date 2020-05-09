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
                    if ((mode | MainWindowDataModel.SelectingFlag.Infantries) != 0)
                    {
                        Current.SelectInfantryAt(pos, 1);
                        Current.SelectInfantryAt(pos, 2);
                        Current.SelectInfantryAt(pos, 3);
                    }
                    if ((mode | MainWindowDataModel.SelectingFlag.Units) != 0)
                    {
                        Current.SelectUnitAt(pos);
                    }
                    if ((mode | MainWindowDataModel.SelectingFlag.Buildings) != 0)
                    {
                        Current.SelectBuildingAt(pos);
                    }
                    if ((mode | MainWindowDataModel.SelectingFlag.Overlays) != 0)
                    {
                        Current.SelectOverlayAt(pos);
                    }
                    if ((mode | MainWindowDataModel.SelectingFlag.Terrains) != 0)
                    {
                        Current.SelectTerrainAt(pos);
                    }
                }
                GlobalVar.Engine.Refresh();
                isSelecting = false;
            }
        }
    }
}
