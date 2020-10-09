using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.FileSystem;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Points;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Logic;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using RelertSharp.GUI.Model;
using static RelertSharp.Common.GlobalVar;

using System.Threading;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest
    {
        private bool isObjectMoving = false, isPreciseMovingMode = false;
        private Vec3 previousObjectLocation;
        private int previousSubcell = -1;


        private void ObjectMovingBegin(Vec3 pos, int subcell)
        {
            previousSubcell = subcell;
            if (pos == Vec3.Zero) previousObjectLocation = Vec3.FromXYZ(Engine.GetPreviousLegalTile());
            else previousObjectLocation = pos;
            if (Current.SelectedMapObjects.Count() == 0)
            {
                PreciseSelecting(pos, subcell);
                isPreciseMovingMode = true;
            }
            isObjectMoving = true;
            if (Current.SelectedMapObjects.Count() == 0)
            {
                isObjectMoving = false;
                isPreciseMovingMode = false;
            }
        }
        private bool movingInProgress = false;
        private void OnObjectMoving(Vec3 posNow, int subcell)
        {
            if (isObjectMoving && !movingInProgress)
            {
                movingInProgress = true;
                if (posNow == Vec3.Zero) posNow = Vec3.FromXYZ(Engine.GetPreviousLegalTile());

                if (posNow != previousObjectLocation)
                {
                    if (isPreciseMovingMode && subcell != -1)
                    {
                        IMapObject obj = Current.SelectedMapObjects.First();
                        Map.MoveObjectTo(obj, posNow.To3dLocateable(), subcell);
                    }
                    else
                    {
                        Vec3 delta = posNow - previousObjectLocation;
                        foreach (IMapObject obj in Current.SelectedMapObjects)
                        {
                            I3dLocateable deltaCell = Map.ReferanceDeltaCell(obj, delta.To2dLocateable());
                            if (!Map.IsOutOfSize(obj, deltaCell))
                            {
                                Map.ShiftObjectBy(obj, deltaCell);
                            }
                        }
                    }
                    previousObjectLocation = posNow;
                    RedrawMinimapAll();
                    Engine.Refresh();
                }
                else if (subcell != -1 && isPreciseMovingMode)
                {
                    IMapObject obj = Current.SelectedMapObjects.First();
                    Map.MoveObjectTo(obj, posNow.To3dLocateable(), subcell);
                }
                movingInProgress = false;
            }
        }

        private void ObjectMovingEnd(MouseEventArgs e)
        {
            isObjectMoving = false;
            isPreciseMovingMode = false;
        }
    }
}
