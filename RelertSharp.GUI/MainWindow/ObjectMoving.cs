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

namespace RelertSharp.GUI
{
    public partial class MainWindowTest
    {
        private bool isObjectMoving = false, isPreciseMovingMode = false;
        private Vec3 previousObjectLocation;
        private int previousSubcell = -1;
        private void ObjectMovingBegin(MouseEventArgs e)
        {
            Vec3 pos = Engine.ClientPointToCellPos(e.Location, out int subcell);
            previousSubcell = subcell;
            if (pos == Vec3.Zero) previousObjectLocation = Vec3.FromXYZ(Engine.GetPreviousLegalTile());
            else previousObjectLocation = pos;
            if (Current.SelectedMapObjects.Count() == 0) isPreciseMovingMode = true;
            PreciseSelecting(e);
            isObjectMoving = true;
            if (Current.SelectedMapObjects.Count() == 0)
            {
                isObjectMoving = false;
                isPreciseMovingMode = false;
            }
        }
        private void OnObjectMoving(MouseEventArgs e)
        {
            if (isObjectMoving)
            {
                if (isPreciseMovingMode)
                {
                    Vec3 pos = Engine.ClientPointToCellPos(e.Location, out int subcell);
                    if ((pos != previousObjectLocation || subcell != previousSubcell) && previousSubcell != -1)
                    {
                        IMapObject obj = Current.SelectedMapObjects.First();
                    }
                }
            }
        }
    }
}
