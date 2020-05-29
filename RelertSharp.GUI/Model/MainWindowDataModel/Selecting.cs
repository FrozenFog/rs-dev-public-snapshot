using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.FileSystem;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;
using RelertSharp.Common;
using RelertSharp.DrawingEngine.Presenting;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.GUI.Model
{
    public partial class MainWindowDataModel
    {
        [Flags]
        public enum SelectingFlag
        {
            None = 0,
            Infantries = 1,
            Buildings = 1 << 2,
            Units = 1 << 3,
            Aircrafts = 1 << 4,
            Terrains = 1 << 5,
            Overlays = 1 << 6,
            Smudges = 1 << 7,
            BaseNodes = 1 << 8,
            Waypoints = 1 << 9,
            CellTags = 1 << 10,
        }
        public enum SelectingBoxMode
        {
            ClientRectangle = 1,
            IsometricRectangle = 2,
            Precise = 3
        }


        public void SelectUnitAt(I2dLocateable pos)
        {
            UnitItem unit = map.GetUnit(pos);
            if (unit != null && !Units.Contains(unit))
            {
                Units.Add(unit);
                unit.Selected = true;
                Engine.SelectUnitAt(pos);
            }
        }
        public void SelectInfantryAt(I2dLocateable pos, int subcell)
        {
            InfantryItem inf = map.GetInfantry(pos, subcell);
            if (inf != null && !Infantries.Contains(inf))
            {
                Infantries.Add(inf);
                inf.Selected = true;
                Engine.SelectInfantryAt(pos, subcell);
            }
        }
        public void SelectBuildingAt(I2dLocateable pos)
        {
            StructureItem building = map.GetBuilding(pos);
            if (building != null && !Buildings.Contains(building))
            {
                Buildings.Add(building);
                building.Selected = true;
                Engine.SelectBuildingAt(building);
            }
        }
        public void SelectTerrainAt(I2dLocateable pos)
        {
            TerrainItem terr = map.Terrains[pos];
            if (terr != null && !Terrains.Contains(terr))
            {
                Terrains.Add(terr);
                terr.Selected = true;
                Engine.SelectTerrainAt(terr);
            }
        }
        public void SelectOverlayAt(I2dLocateable pos)
        {
            OverlayUnit ov = map.Overlays[pos.X, pos.Y];
            if (ov != null && !Overlays.Contains(ov))
            {
                Overlays.Add(ov);
                Engine.SelectOverlayAt(ov);
            }
        }
        public void ReleaseAll()
        {
            foreach (UnitItem unit in Units)
            {
                Engine.UnSelectUnitAt(unit);
                unit.Selected = false;
            }
            Units.Clear();
            foreach (InfantryItem inf in Infantries)
            {
                Engine.UnSelectInfantryAt(inf, inf.SubCells);
                inf.Selected = false;
            }
            Infantries.Clear();
            foreach (StructureItem building in Buildings)
            {
                Engine.UnSelectBuindingAt(building);
                building.Selected = false;
            }
            Buildings.Clear();
            foreach (TerrainItem terr in Terrains)
            {
                Engine.UnSelectTerrainAt(terr);
                terr.Selected = false;
            }
            Terrains.Clear();
            foreach (OverlayUnit ov in Overlays)
            {
                Engine.UnSelectOverlayAt(ov);
            }
            Overlays.Clear();


            Engine.Refresh();
        }
        public void RemoveAll()
        {
            foreach (UnitItem unit in Units)
            {
                Engine.RemoveUnitAt(unit);
                map.RemoveUnit(unit);
            }
            Units.Clear();
            foreach (InfantryItem inf in Infantries)
            {
                Engine.RemoveInfantryAt(inf, inf.SubCells);
                map.RemoveInfantry(inf);
            }
            Infantries.Clear();
            foreach (StructureItem st in Buildings)
            {
                Engine.RemoveBuildingAt(st);
                map.RemoveBuilding(st);
            }
            Buildings.Clear();
            foreach (TerrainItem terr in Terrains)
            {
                Engine.RemoveTerrainAt(terr);
                map.RemoveTerrains(terr);
            }
            Terrains.Clear();
            foreach (OverlayUnit ov in Overlays)
            {
                Engine.RemoveOverlayAt(ov);
                map.Overlays.RemoveByCoord(ov);
            }
            Overlays.Clear();

            Engine.Refresh();
            Engine.RedrawMinimapAll();
        }


        #region Public Calls - MainWindowDataModel
        public SelectingFlag SelectingFlags { get; set; } = SelectingFlag.Units | SelectingFlag.Infantries | SelectingFlag.Buildings | SelectingFlag.Terrains | SelectingFlag.Overlays;
        public SelectingBoxMode SelectingBoxFlag { get; set; } = SelectingBoxMode.ClientRectangle;
        public List<AircraftItem> Aircrafts { get; private set; } = new List<AircraftItem>();
        public List<InfantryItem> Infantries { get; private set; } = new List<InfantryItem>();
        public List<UnitItem> Units { get; private set; } = new List<UnitItem>();
        public List<StructureItem> Buildings { get; private set; } = new List<StructureItem>();
        public List<TerrainItem> Terrains { get; private set; } = new List<TerrainItem>();
        public List<OverlayUnit> Overlays { get; private set; } = new List<OverlayUnit>();
        public IEnumerable<ICombatObject> CombatObjects { get { return Infantries.Concat<ICombatObject>(Units).Concat(Buildings); } }
        public IEnumerable<IMapObject> SelectedMapObjects { get { return Aircrafts.Concat<IMapObject>(Infantries).Concat(Units).Concat(Buildings).Concat(Terrains); } }
        #endregion
    }
}
