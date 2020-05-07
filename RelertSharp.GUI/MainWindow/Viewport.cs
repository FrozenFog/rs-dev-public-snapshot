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
        private void SelectSceneItemsInsideBox(MouseEventArgs releasePoint)
        {
            if (drew && isSelecting)
            {
                var mode = Current.SelectingFlags;
                foreach (I2dLocateable pos in new SceneSquare2D(selectorBoxLT, releasePoint.Location, map.Info.Size.Width))
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
