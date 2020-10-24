using RelertSharp.Common;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;
using System.Drawing;

namespace RelertSharp.MapStructure
{
    public partial class Map
    {
        private void AddObjectToTile(IMapObject obj)
        {
            if (obj.GetType() == typeof(StructureItem))
            {
                StructureItem bud = obj as StructureItem;
                foreach (I2dLocateable pos in new Foundation2D(bud))
                {
                    Tiles.AddObjectOnTile(pos, obj);
                }
            }
            else if (obj.GetType() == typeof(SmudgeItem))
            {
                SmudgeItem smg = obj as SmudgeItem;
                foreach (I2dLocateable pos in new Square2D(smg, smg.SizeX, smg.SizeY))
                {
                    Tiles.AddObjectOnTile(pos, obj);
                }
            }
            else Tiles.AddObjectOnTile(obj);
        }
        public SmudgeItem AddSmudge(string regname, int x, int y, bool ignore)
        {
            SmudgeItem smg = new SmudgeItem(regname, x, y, ignore);
            Smudges.AddObject(smg);
            AddObjectToTile(smg);
            return smg;
        }
        public UnitItem AddUnit(string id, string[] args)
        {
            UnitItem unit = new UnitItem(id, args);
            Units[id] = unit;
            AddObjectToTile(unit);
            return unit;
        }
        public InfantryItem AddInfantry(string id, string[] args)
        {
            InfantryItem inf = new InfantryItem(id, args);
            Infantries[id] = inf;
            AddObjectToTile(inf);
            return inf;
        }
        public void DumpStructure()
        {
            foreach (StructureItem bud in Buildings)
            {
                AddObjectToTile(bud);
            }
        }
        public AircraftItem AddAircraft(string id, string[] args)
        {
            AircraftItem air = new AircraftItem(id, args);
            Aircrafts[id] = air;
            AddObjectToTile(air);
            return air;
        }
        public TerrainItem AddTerrain(string coord, string regname)
        {
            TerrainItem ter = new TerrainItem(coord, regname);
            Terrains.AddObject(ter);
            AddObjectToTile(ter);
            return ter;
        }
        public CellTagItem AddCellTag(I2dLocateable pos, string tagID)
        {
            CellTagItem cell = new CellTagItem(pos, tagID);
            Celltags.AddObject(cell);
            AddObjectToTile(cell);
            return cell;
        }
    }
}
