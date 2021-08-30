using RelertSharp.Common;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Points;
using RelertSharp.Wpf.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RelertSharp.Wpf.MapEngine.Helper
{
    internal static class ExtentedFunc
    {
        private static DragDropHelper<WaypointItem, object> wpPick;


        #region Api
        #region WpDrag
        public static void BindControl(FrameworkElement src)
        {
            wpPick = new DragDropHelper<WaypointItem, object>(src);
        }
        public static void BeginWaypointDrag(Point mouseDown, I2dLocateable cell)
        {
            if (GlobalVar.GlobalMap.TilesData[cell] is Tile t && t.GetObejct(x => x.ObjectType == MapObjectType.Waypoint) is WaypointItem item)
            {
                wpPick.BeginDrag(mouseDown);
                wpPick.SetDragItem(item);
                IsDraggingWaypoint = true;
            }
        }
        public static void WpDragMove(Point current)
        {
            wpPick.MouseMoveDrag(current);
        }
        public static void EndWaypointDrag()
        {
            wpPick.EndDrag();
            IsDraggingWaypoint = false;
        }
        #endregion
        #endregion



        #region Calls
        public static bool IsDraggingWaypoint { get; private set; }
        #endregion
    }
}
