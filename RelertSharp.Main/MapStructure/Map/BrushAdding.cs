using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;
using RelertSharp.Common;
using RelertSharp.DrawingEngine.Presenting;

namespace RelertSharp.MapStructure
{
    public partial class Map
    {
        public void AddObjectFromBrush(IMapObject obj)
        {
            Type t = obj.GetType();
            if (t == typeof(UnitItem)) Units.AddFromBrush(obj as UnitItem);
            else if (t == typeof(InfantryItem)) Infantries.AddFromBrush(obj as InfantryItem);
            else if (t == typeof(AircraftItem)) Aircrafts.AddFromBrush(obj as AircraftItem);
            else if (t == typeof(StructureItem)) Buildings.AddFromBrush(obj as StructureItem);
            else if (t == typeof(TerrainItem)) Terrains.AddObject(obj as TerrainItem);
            AddObjectToTile(obj);
        }
        public void AddBaseNode(IMapObject obj, string ownerHouse)
        {
            BaseNode node = obj as BaseNode;
            if (node != null) Houses.GetHouse(ownerHouse)?.BaseNodes.Add(node);
        }
    }
}
