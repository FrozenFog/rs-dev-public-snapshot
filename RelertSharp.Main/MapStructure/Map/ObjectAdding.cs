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
        public SmudgeItem AddSmudge(string regname, int x, int y, bool ignore)
        {
            SmudgeItem smg = new SmudgeItem(regname, x, y, ignore);
            Smudges[x, y] = smg;
            Tiles.AddObjectOnTile(smg);
            return smg;
        }
        public UnitItem AddUnit(string id, string[] args)
        {
            UnitItem unit = new UnitItem(id, args);
            Units[id] = unit;
            Tiles.AddObjectOnTile(unit);
            return unit;
        }
        public InfantryItem AddInfantry(string id, string [] args)
        {
            InfantryItem inf = new InfantryItem(id, args);
            Infantries[id] = inf;
            Tiles.AddObjectOnTile(inf);
            return inf;
        }
        public StructureItem AddStructure(string id, string[] args)
        {
            StructureItem bud = new StructureItem(id, args);
            Buildings[id] = bud;
            foreach (I2dLocateable pos in new Square2D(bud, bud.SizeX, bud.SizeY))
            {
                Tiles.AddObjectOnTile(pos, bud);
            }
            return bud;
        }
        public AircraftItem AddAircraft(string id, string[] args)
        {
            AircraftItem air = new AircraftItem(id, args);
            Aircrafts[id] = air;
            Tiles.AddObjectOnTile(air);
            return air;
        }
        public TerrainItem AddTerrain(string coord, string regname)
        {
            TerrainItem ter = new TerrainItem(coord, regname);
            Terrains[coord] = ter;
            Tiles.AddObjectOnTile(ter);
            return ter;
        }
    }
}
