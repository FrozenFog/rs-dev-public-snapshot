using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.MapStructure.Objects;

namespace RelertSharp.GUI.Controls
{
    public partial class PickPanel
    {
        private bool canBuild = false;
        private I3dLocateable previousCell;
        private int previousSubcell = -1;

        public void MoveBurhObjectTo(I3dLocateable cell, int subcell = 1)
        {
            if (brush.BrushObject != null && brush.BrushObject.SceneObject !=null)
            {
                if (brush.BrushObject.GetType() == typeof(InfantryItem))
                {
                    if (previousCell == null || previousSubcell == -1 ||
                        previousCell.Coord != cell.Coord || previousSubcell != subcell)
                    {
                        (brush.BrushObject as InfantryItem).MoveTo(cell, subcell);
                        previousCell = cell;
                        previousSubcell = subcell;
                        GlobalVar.Engine.Refresh();
                    }
                }
                else
                {
                    if (previousCell == null || previousCell.Coord != cell.Coord)
                    {
                        brush.MoveBrushObjectTo(cell);
                        brush.BrushObject.MoveTo(cell);
                        previousCell = cell;
                        GlobalVar.Engine.Refresh();
                    }
                }
                if (brush.BrushObject.GetType() == typeof(StructureItem))
                {
                    canBuild = GlobalVar.Engine.MarkBuildingShape(brush.BrushObject as StructureItem);
                }
            }
        }
        public IMapObject ReleaseBrushObject(bool simulating, out bool canBuild)
        {
            if (simulating && !this.canBuild)
            {
                canBuild = false;
                return null;
            }
            canBuild = this.canBuild;
            if (shiftHidden) brush.RedrawBrushObject();
            return brush.ReleaseObject();
        }
    }
}
