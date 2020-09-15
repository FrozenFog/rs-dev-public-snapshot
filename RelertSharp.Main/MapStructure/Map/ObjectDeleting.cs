using RelertSharp.Common;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;

namespace RelertSharp.MapStructure
{
    public partial class Map
    {
        public void RemoveObject(IMapObject obj)
        {
            if (obj is InfantryItem inf) RemoveInfantry(inf);
            else if (obj is UnitItem unit) RemoveUnit(unit);
            else if (obj is StructureItem bud) RemoveBuilding(bud);
            else if (obj is AircraftItem air) RemoveAircraft(air);
            else if (obj is TerrainItem terr) RemoveTerrains(terr);
            else if (obj is OverlayUnit o) RemoveOverlay(o);
            else if (obj is BaseNode node) RemoveBasenode(node);
            else if (obj is WaypointItem wp) RemoveWaypoint(wp);
        }
        public void RemoveInfantry(InfantryItem inf)
        {
            inf.Dispose();
            Infantries.RemoveByID(inf.ID);
            Tiles[inf]?.RemoveObject(inf);
        }
        public void RemoveUnit(UnitItem unit)
        {
            unit.Dispose();
            Units.RemoveByID(unit.ID);
            Tiles[unit]?.RemoveObject(unit);
        }
        public void RemoveBuilding(StructureItem bud)
        {
            bud.Dispose();
            Buildings.RemoveByID(bud.ID);
            foreach (I2dLocateable pos in new Foundation2D(bud))
            {
                Tiles[pos]?.RemoveObject(bud);
            }
        }
        public void RemoveAircraft(AircraftItem air)
        {
            air.Dispose();
            Aircrafts.RemoveByID(air.ID);
            Tiles[air]?.RemoveObject(air);
        }
        public void RemoveTerrains(TerrainItem ter)
        {
            ter.Dispose();
            Terrains.RemoveObjectByID(ter);
            Tiles[ter]?.RemoveObject(ter);
        }
        public void RemoveOverlay(OverlayUnit o)
        {
            o.Dispose();
            Overlays.RemoveByCoord(o);
            Tiles[o]?.RemoveObject(o);
        }
        public void RemoveBasenode(BaseNode node)
        {
            node.Dispose();
            Tiles[node]?.RemoveObject(node);
        }
        public void RemoveWaypoint(WaypointItem wp)
        {
            wp.Dispose();
            Waypoints.RemoveWaypoint(wp);
        }
    }
}
