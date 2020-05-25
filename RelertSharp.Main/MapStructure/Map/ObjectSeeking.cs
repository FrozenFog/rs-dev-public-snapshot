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
        #region Private Generic Seeking
        private T GetMapObject<T>(I2dLocateable pos) where T : ObjectItemBase, IMapObject
        {
            Tile t = Tiles[pos];
            if (t != null)
            {
                foreach (IMapObject obj in t.GetObjects())
                {
                    if (obj.GetType() == typeof(T)) return obj as T;
                }
            }
            return null;
        }
        private void UpdateObject<T>(T src) where T : ObjectItemBase, IMapObject
        {
            Tiles[src].RemoveObject(src);
            Tiles[src].AddObject(src);
        }
        private T GetMapObject<T>(I2dLocateable pos, string regname) where T: ObjectItemBase, IMapObject
        {
            Tile t = Tiles[pos];
            if (t != null)
            {
                foreach (IMapObject obj in t.GetObjects())
                {
                    if (obj.GetType() == typeof(T))
                    {
                        if (obj.RegName == regname) return obj as T;
                    }
                }
            }
            return null;
        }
        #endregion


        #region Public Methods - Seeking
        private bool buildingDumped = false;
        public void DumpStructures()
        {
            if (!buildingDumped)
            {
                foreach (StructureItem bud in Buildings)
                {
                    foreach (I2dLocateable pos in new Square2D(bud, bud.SizeX, bud.SizeY))
                    {
                        Tiles.AddObjectOnTile(pos, bud);
                    }
                }
                buildingDumped = true;
            }
        }
        public StructureItem GetBuilding(I2dLocateable pos)
        {
            if (!buildingDumped) DumpStructures();
            StructureItem refer = GetMapObject<StructureItem>(pos);
            if (refer != null) return Buildings[refer.ID];
            return null;
        }
        public void UpdateBuilding(StructureItem bud)
        {
            if (!buildingDumped) DumpStructures();
            foreach (I2dLocateable pos in new Square2D(bud, bud.SizeX, bud.SizeY))
            {
                UpdateObject(bud);
            }
            Buildings[bud.ID] = bud;
        }
        public UnitItem GetUnit(I2dLocateable pos)
        {
            UnitItem refer = GetMapObject<UnitItem>(pos);
            if (refer != null) return Units[refer.ID];
            return null;
        }
        public void UpdateUnit(UnitItem unit)
        {
            UpdateObject(unit);
            Units[unit.ID] = unit;
        }
        public AircraftItem GetAircraft(I2dLocateable pos)
        {
            AircraftItem refer = GetMapObject<AircraftItem>(pos);
            if (refer != null) return Aircrafts[refer.ID];
            return null;
        }
        public void UpdateAircraft(AircraftItem air)
        {
            UpdateObject(air);
            Aircrafts[air.ID] = air;
        }
        public InfantryItem GetInfantry(I2dLocateable pos, int subcell)
        {
            Tile t = Tiles[pos];
            if (t != null)
            {
                foreach (IMapObject obj in t.GetObjects())
                {
                    if (obj.GetType() == typeof(InfantryItem))
                    {
                        InfantryItem inf = obj as InfantryItem;
                        if (inf.SubCells == subcell) return Infantries[inf.ID];
                    }
                }
            }
            return null;
        }
        public void UpdateInfantry(InfantryItem inf)
        {
            UpdateObject(inf);
            Infantries[inf.ID] = inf;
        }
        public TerrainItem GetTerrain(I2dLocateable pos)
        {
            return Terrains[pos];
        }
        #endregion
    }
}
