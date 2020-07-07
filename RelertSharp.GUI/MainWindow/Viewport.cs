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
        private I3dLocateable prevSelectingLT;
        private I3dLocateable prevSelectingRB;
        private bool isSelecting = false;


        private void SceneSelectionBoxSet(MouseEventArgs e)
        {
            if (drew)
            {
                selectorBoxLT = e.Location;
                prevSelectingLT = GlobalVar.Engine.GetPreviousLegalTile();
                prevSelectingRB = prevSelectingLT;
                isSelecting = true;
            }
        }
        private void DrawSelectingBoxOnScene(MouseEventArgs now)
        {
            if (isSelecting)
            {
                GlobalVar.Engine.DrawSelectingRectangle(Pnt.FromPoint(selectorBoxLT), Pnt.FromPoint(now.Location), Current.SelectingBoxFlag == MainWindowDataModel.SelectingBoxMode.IsometricRectangle);
                prevSelectingRB = GlobalVar.Engine.GetPreviousLegalTile();
            }
        }
        private void SelectSceneItemsInsideBox(Vec3 releaseCell)
        {
            if (drew && isSelecting)
            {
                GlobalVar.Engine.ReleaseDrawingRectangle();
                var mode = Current.SelectingFlags;
                IEnumerable<I2dLocateable> iter;
                if (Current.SelectingBoxFlag == MainWindowDataModel.SelectingBoxMode.ClientRectangle)
                {
                    Point LT = GlobalVar.Engine.CellPosToClientPos(prevSelectingLT);
                    Point RB = GlobalVar.Engine.CellPosToClientPos(prevSelectingRB);
                    iter = new SceneSquare2D(LT, RB, map.Info.Size.Width);
                }
                else
                {
                    iter = new Square2D(prevSelectingLT, prevSelectingRB);
                }
                foreach (I2dLocateable pos in iter)
                {
                    SelectAt(pos, mode, releaseCell, false, 0);
                }
                GlobalVar.Engine.Refresh();
                isSelecting = false;
            }
        }
        private void PreciseSelecting(Vec3 cell, int subcell)
        {
            if (drew)
            {
                var flag = Current.SelectingFlags;
                if (flag == MainWindowDataModel.SelectingFlag.None) return;
                I2dLocateable pos = cell.To2dLocateable();
                SelectAt(pos, flag, cell, true, subcell);
                GlobalVar.Engine.Refresh();
            }
        }
        private void SelectAt(I2dLocateable pos, MainWindowDataModel.SelectingFlag flag, Vec3 cell, bool isPrecise, int subcell)
        {
            if ((flag | MainWindowDataModel.SelectingFlag.Units) != 0)
            {
                Current.SelectUnitAt(pos);
            }
            if ((flag | MainWindowDataModel.SelectingFlag.Infantries) != 0)
            {
                if (isPrecise)
                {
                    I2dLocateable infpos = cell.To2dLocateable();
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
        private bool RbPanelVisible()
        {
            bool b = rbPanelBrush.Visible || rbPanelAttribute.Visible || rbPanelWand.Visible || rbPanelBucket.Visible;
            return b;
        }
        private void HideRbPanel()
        {
            rbPanelBrush.Visible = false;
            rbPanelAttribute.Visible = false;
            rbPanelWand.Visible = false;
            rbPanelBucket.Visible = false;
        }
        private void RedrawMinimapAll()
        {
            GlobalVar.Engine.RedrawMinimapAll();
            pnlMiniMap.BackgroundImage = GlobalVar.Engine.MiniMap;
        }
    }
}
