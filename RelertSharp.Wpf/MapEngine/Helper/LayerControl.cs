using RelertSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.MapEngine.Helper
{
    internal static class LayerControl
    {
        private static MapStructure.Map Map { get { return GlobalVar.GlobalMap; } }
        private static MapObjectType visibleType = MapObjectType.AllSelectableObjects;
        #region Api
        public static void SetVisibility(MapObjectType type, bool isEnable)
        {
            IEnumerable<IMapObject> src = new List<IMapObject>();
            bool isType(MapObjectType target)
            {
                return (type & target) != 0;
            }
            if (isType(MapObjectType.Aircraft)) src = src.Concat(Map.Aircrafts);
            if (isType(MapObjectType.Infantry)) src = src.Concat(Map.Infantries);
            if (isType(MapObjectType.Building)) src = src.Concat(Map.Buildings);
            if (isType(MapObjectType.Vehicle)) src = src.Concat(Map.Units);
            if (isType(MapObjectType.Celltag)) src = src.Concat(Map.Celltags);
            if (isType(MapObjectType.Waypoint)) src = src.Concat(Map.Waypoints);
            if (isType(MapObjectType.Terrain)) src = src.Concat(Map.Terrains);
            if (isType(MapObjectType.Smudge)) src = src.Concat(Map.Smudges);
            if (isType(MapObjectType.Overlay)) src = src.Concat(Map.Overlays);
            if (isType(MapObjectType.BaseNode)) src = src.Concat(Map.AllBaseNodes);

            if (isEnable)
            {
                src.Foreach(x => x.Reveal());
                visibleType |= type;
            }
            else
            {
                src.Foreach(x => x.Hide());
                Selector.UnselectObject(src);
                visibleType &= ~type;
            }
        }
        public static bool IsTypeVisible(MapObjectType type)
        {
            return (type & visibleType) != 0;
        }
        public static void AddVisibility(MapObjectType type)
        {
            SetVisibility(type, true);
        }
        public static void RemoveVisibility(MapObjectType type)
        {
            SetVisibility(type, false);
        }
        #endregion
    }
}
