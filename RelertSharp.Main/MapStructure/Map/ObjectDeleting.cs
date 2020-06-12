using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;
using RelertSharp.Common;

namespace RelertSharp.MapStructure
{
    public partial class Map
    {
        public void RemoveInfantry(InfantryItem inf)
        {
            inf.Dispose();
            Infantries.RemoveByID(inf.ID);
            Tiles[inf].RemoveObject(inf);
        }
        public void RemoveUnit(UnitItem unit)
        {
            unit.Dispose();
            Units.RemoveByID(unit.ID);
            Tiles[unit].RemoveObject(unit);
        }
        public void RemoveBuilding(StructureItem bud)
        {
            bud.Dispose();
            Buildings.RemoveByID(bud.ID);
            foreach (I2dLocateable pos in new Square2D(bud, bud.SizeX, bud.SizeY))
            {
                Tiles[pos].RemoveObject(bud);
            }
        }
        public void RemoveAircraft(AircraftItem air)
        {
            air.Dispose();
            Aircrafts.RemoveByID(air.ID);
            Tiles[air].RemoveObject(air);
        }
        public void RemoveTerrains(TerrainItem ter)
        {
            ter.Dispose();
            Terrains.RemoveByCoord(ter);
            Tiles[ter].RemoveObject(ter);
        }
        public void RemoveOverlay(OverlayUnit o)
        {
            o.Dispose();
            Overlays.RemoveByCoord(o);
            Tiles[o].RemoveObject(o);
        }
    }
}
