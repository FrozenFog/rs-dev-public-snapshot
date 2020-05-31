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
            Infantries.RemoveByID(inf.ID);
            Tiles[inf].RemoveObject(inf);
        }
        public void RemoveUnit(UnitItem unit)
        {
            Units.RemoveByID(unit.ID);
            Tiles[unit].RemoveObject(unit);
        }
        public void RemoveBuilding(StructureItem bud)
        {
            Buildings.RemoveByID(bud.ID);
            foreach (I2dLocateable pos in new Square2D(bud, bud.SizeX, bud.SizeY))
            {
                Tiles[pos].RemoveObject(bud);
            }
        }
        public void RemoveAircraft(AircraftItem air)
        {
            Aircrafts.RemoveByID(air.ID);
            Tiles[air].RemoveObject(air);
        }
        public void RemoveTerrains(TerrainItem ter)
        {
            Terrains.RemoveByCoord(ter);
        }
    }
}
