using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.Common;
using RelertSharp.MapStructure.Points;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest
    {
        public void MoveBrushObjectTo(Vec3 pos, int subcell)
        {
            if (drew)
            {
                I3dLocateable cell;
                if (pos == Vec3.Zero)
                {
                    cell = GlobalVar.Engine.GetPreviousLegalTile();
                    subcell = 1;
                }
                else cell = pos.To3dLocateable();
                pnlPick.MoveBurhObjectTo(cell, subcell);
            }
        }
        public void AddBrushObjectToMap()
        {
            if (drew)
            {
                if (pnlPick.Result.BrushObject != null)
                {
                    if (!pnlPick.Result.BrushObject.SceneObject.IsValid)
                    {
                        DialogResult result = GuiUtils.YesNoWarning("This object has no image available, use at own risk.\nAre you sure to put this on map?");
                        if (result == DialogResult.No) return;
                    }
                    if (pnlPick.CurrentType == PickPanelType.CombatObject)
                    {
                        if (pnlPick.Result.ObjectType == MapObjectType.Building)
                        {
                            if (!rbPanelBrush.IsSimulating || pnlPick.CanBuild)
                            {
                                List<object> paramNow = new List<object>() { true };
                                List<object> paramPrev = new List<object>() { false };
                                if (rbPanelBrush.AddBaseNode)
                                {
                                    IMapObject node = pnlPick.ReleaseAdditionalBaseNode(out string ownerhouse);
                                    paramNow.Add(node);
                                    paramPrev.Add(node);
                                    if (node != null) Map.AddBaseNode(node, ownerhouse);
                                }
                                if (!rbPanelBrush.IgnoreBuilding)
                                {
                                    IMapObject obj = pnlPick.ReleaseBrushObject(rbPanelBrush.IsSimulating, out bool canBuild);
                                    if (rbPanelBrush.IsSimulating && !canBuild)
                                    {
                                        obj.Dispose();
                                        return;
                                    }
                                    paramNow.Add(obj);
                                    paramPrev.Add(obj);
                                    Map.AddObjectFromBrush(obj);
                                }

                                if (paramNow.Count != 0)
                                {
                                    Current.UndoRedo.PushCommand(Model.UndoRedoCommandType.DrawObject, paramPrev, paramNow);
                                }
                            }
                        }
                        else
                        {
                            IMapObject obj = pnlPick.ReleaseBrushObject(false, out bool b);
                            Map.AddObjectFromBrush(obj);
                            Current.UndoRedo.PushCommand(Model.UndoRedoCommandType.DrawObject, new object[] { false, obj }, new object[] { true, obj });
                        }

                    }
                    else
                    {
                        IMapObject obj = pnlPick.ReleaseBrushObject(false, out bool build);
                        Map.AddObjectFromBrush(obj);
                        Current.UndoRedo.PushCommand(Model.UndoRedoCommandType.DrawObject, new object[] { false, obj }, new object[] { true, obj });
                    }
                    GlobalVar.Engine.Refresh();
                    RedrawMinimapAll();
                }
                else if (pnlPick.CurrentType == PickPanelType.Waypoints)
                {
                    pnlPick.ReleaseWaypoint(out WaypointItem wp);
                    Current.UndoRedo.PushCommand(Model.UndoRedoCommandType.DrawObject, new object[] { false, wp }, new object[] { true, wp });
                    GlobalVar.Engine.Refresh();
                }
            }
        }
    }
}
