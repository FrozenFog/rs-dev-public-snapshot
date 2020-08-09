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


        public void RemoveItemFromSelectList<T>(T item) where T : IMapObject
        {
            Type t = item.GetType();
            if (t == typeof(AircraftItem)) Aircrafts.Remove(item as AircraftItem);
            else if (t == typeof(StructureItem)) Buildings.Remove(item as StructureItem);
            else if (t == typeof(UnitItem)) Units.Remove(item as UnitItem);
            else if (t == typeof(InfantryItem)) Infantries.Remove(item as InfantryItem);
        }
        public void SelectAircraftAt(I2dLocateable pos)
        {
            AircraftItem air = map.GetAircraft(pos);
            if (air != null && !Aircrafts.Contains(air))
            {
                Aircrafts.Add(air);
                air.Select();
            }
        }
        public void SelectUnitAt(I2dLocateable pos)
        {
            UnitItem unit = map.GetUnit(pos);
            if (unit != null && !Units.Contains(unit))
            {
                Units.Add(unit);
                unit.Select();
            }
        }
        public void SelectInfantryAt(I2dLocateable pos, int subcell)
        {
            InfantryItem inf = map.GetInfantry(pos, subcell);
            if (inf != null && !Infantries.Contains(inf))
            {
                Infantries.Add(inf);
                inf.Select();
            }
        }
        public void SelectBuildingAt(I2dLocateable pos)
        {
            StructureItem building = map.GetBuilding(pos);
            if (building != null && !Buildings.Contains(building))
            {
                Buildings.Add(building);
                building.Select();
            }
        }
        public void SelectTerrainAt(I2dLocateable pos)
        {
            TerrainItem terr = map.Terrains.GetItemByPos(pos);
            if (terr != null && !Terrains.Contains(terr))
            {
                Terrains.Add(terr);
                terr.Select();
            }
        }
        public void SelectOverlayAt(I2dLocateable pos)
        {
            OverlayUnit ov = map.Overlays[pos.X, pos.Y];
            if (ov != null && !Overlays.Contains(ov))
            {
                Overlays.Add(ov);
                ov.Select();
            }
        }
        public void SelectWaypointAt(I2dLocateable pos)
        {
            WaypointItem wp = map.Waypoints.FindByPos(pos);
            if (wp != null && !Waypoints.Contains(wp))
            {
                Waypoints.Add(wp);
                wp.Select();
            }
        }
        public void ReleaseAll()
        {
            foreach (UnitItem unit in Units) unit.UnSelect();
            Units.Clear();
            foreach (InfantryItem inf in Infantries) inf.UnSelect();
            Infantries.Clear();
            foreach (StructureItem building in Buildings) building.UnSelect();
            Buildings.Clear();
            foreach (TerrainItem terr in Terrains) terr.UnSelect();
            Terrains.Clear();
            foreach (OverlayUnit ov in Overlays) ov.UnSelect();
            Overlays.Clear();
            foreach (CellTagItem cell in Celltags) cell.UnSelect();
            Celltags.Clear();
            foreach (WaypointItem wp in Waypoints) wp.UnSelect();
            Waypoints.Clear();


            Engine.Refresh();
        }

        public void RemoveAll()
        {
            foreach (UnitItem unit in Units)
            {
                map.RemoveUnit(unit);
            }
            Units.Clear();
            foreach (InfantryItem inf in Infantries)
            {
                map.RemoveInfantry(inf);
            }
            Infantries.Clear();
            foreach (StructureItem st in Buildings)
            {
                map.RemoveBuilding(st);
            }
            Buildings.Clear();
            foreach (TerrainItem terr in Terrains)
            {
                map.RemoveTerrains(terr);
            }
            Terrains.Clear();
            foreach (OverlayUnit ov in Overlays)
            {
                map.RemoveOverlay(ov);
            }
            Overlays.Clear();
            foreach (WaypointItem wp in Waypoints)
            {
                map.RemoveWaypoint(wp);
            }
            Waypoints.Clear();

            Engine.RemoveDisposedObjects();
            Engine.Refresh();
            Engine.RedrawMinimapAll();
        }


        #region Public Calls - MainWindowDataModel
        public SelectingFlag SelectingFlags { get; set; } = SelectingFlag.Units | SelectingFlag.Infantries | SelectingFlag.Buildings | SelectingFlag.Terrains | SelectingFlag.Overlays | SelectingFlag.Waypoints;
        public SelectingBoxMode SelectingBoxFlag { get; set; } = SelectingBoxMode.ClientRectangle;
        public List<AircraftItem> Aircrafts { get; private set; } = new List<AircraftItem>();
        public List<InfantryItem> Infantries { get; private set; } = new List<InfantryItem>();
        public List<UnitItem> Units { get; private set; } = new List<UnitItem>();
        public List<StructureItem> Buildings { get; private set; } = new List<StructureItem>();
        public List<TerrainItem> Terrains { get; private set; } = new List<TerrainItem>();
        public List<OverlayUnit> Overlays { get; private set; } = new List<OverlayUnit>();
        public List<CellTagItem> Celltags { get; private set; } = new List<CellTagItem>();
        public List<WaypointItem> Waypoints { get; private set; } = new List<WaypointItem>();
        public IEnumerable<ICombatObject> CombatObjects { get { return Infantries.Concat<ICombatObject>(Units).Concat(Buildings); } }
        /// <summary>
        /// Aircraft, Infantries, Units, Buildings, Terrains
        /// </summary>
        public IEnumerable<IMapObject> SelectedMapObjects { get { return Aircrafts.Concat<IMapObject>(Infantries).Concat(Units).Concat(Buildings).Concat(Terrains).Concat(Waypoints); } }
        #endregion
    }
}
