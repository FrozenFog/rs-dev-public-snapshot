using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.Common;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;

namespace RelertSharp.GUI.Controls
{
    public partial class PickPanel
    {
        private bool canBuild = false;
        private I3dLocateable previousCell;
        private int previousSubcell = -1;

        public void MoveBurhObjectTo(I3dLocateable cell, int subcell = 1)
        {
            if (brush.BrushObject != null && brush.BrushObject.SceneObject != null)
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
                        previousCell = cell;
                        GlobalVar.Engine.Refresh();
                    }
                }
                if (brush.BrushObject.GetType() == typeof(StructureItem))
                {
                    canBuild = GlobalVar.Engine.MarkBuildingShape(brush.BrushObject as StructureItem);
                }
            }
            else previousCell = cell;
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
        public IMapObject ReleaseAdditionalBaseNode(out string ownerHouse)
        {
            if (brush.BrushObject.GetType() == typeof(StructureItem))
            {
                BaseNode n = new BaseNode(brush.BrushObject as StructureItem);
                ownerHouse = (brush.BrushObject as StructureItem).OwnerHouse;
                GlobalVar.Engine.DrawBrushObject(n, brush.BrushObject.SceneObject.Z, ownerHouse);
                return n;
            }
            ownerHouse = "";
            return null;
        }
        public void ReleaseWaypoint(out WaypointItem wp)
        {
            wp = null;
            try
            {
                if (Map.Waypoints.FindByPos(previousCell) == null)
                {
                    if (IsWpDesignatedNum)
                    {
                        InputWindow input = new InputWindow();
                        input.SetDialog("Waypoint", "Input waypoint ID, must not exceed 701:");
                        if (input.ShowDialog() == DialogResult.OK)
                        {
                            if (int.TryParse(input.InputResult, out int num) && Map.Waypoints.IsValidNum(num))
                            {
                                wp = new WaypointItem(previousCell, num);
                            }
                            else
                            {
                                GuiUtils.Warning("Invalid Waypoint!\nMax waypoint num is 701!");
                                return;
                            }
                        }
                    }
                    else if (IsWpFirstNum)
                    {
                        wp = new WaypointItem(previousCell, Map.Waypoints.NewID());
                    }
                    if (wp != null)
                    {
                        Map.Waypoints.AddObject(wp);
                        GlobalVar.Engine.DrawWaypoint(wp, Map.GetHeightFromTile(wp));
                        lbxWaypoint.Items.Add(wp);
                    }
                }
            }
            catch (RSException.InvalidWaypointException e)
            {
                GuiUtils.Warning("Invalid Waypoint!\nMax waypoint num is 701!");
            }
        }


        public bool CanBuild
        {
            get
            {
                return canBuild && !brush.IsInvalidItem;
            }
        }
    }
}
