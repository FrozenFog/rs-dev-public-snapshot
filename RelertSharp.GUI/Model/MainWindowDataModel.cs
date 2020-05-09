﻿using System;
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
    public class MainWindowDataModel
    {
        private Map map { get { return CurrentMapDocument.Map; } }
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
            IsometricRectangle = 2
        }


        #region Ctor - MainWindowDataModel
        public MainWindowDataModel()
        {

        }
        #endregion


        #region Public Methods - MainWindowDataModel
        public void SelectUnitAt(I2dLocateable pos)
        {
            UnitItem unit = map.Units.FindByCoord(pos);
            if (unit != null && !Units.Contains(unit))
            {
                Units.Add(unit);
                Engine.SelectUnitAt(pos);
            }
        }
        public void SelectInfantryAt(I2dLocateable pos, int subcell)
        {
            InfantryItem inf = map.Infantries.FindByCoord(pos, subcell);
            if (inf != null && !Infantries.Contains(inf))
            {
                Infantries.Add(inf);
                Engine.SelectInfantryAt(pos, subcell);
            }
        }
        public void SelectBuildingAt(I2dLocateable pos)
        {
            StructureItem building = map.Buildings.FindByCoord(pos);
            if (building != null && !Buildings.Contains(building))
            {
                Buildings.Add(building);
                Engine.SelectBuildingAt(building);
            }
        }
        public void SelectTerrainAt(I2dLocateable pos)
        {
            TerrainItem terr = map.Terrains.FindByCoord(pos);
            if (terr != null && !Terrains.Contains(terr))
            {
                Terrains.Add(terr);
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
            }
            Units.Clear();
            foreach (InfantryItem inf in Infantries)
            {
                Engine.UnSelectInfantryAt(inf, inf.SubCells);
            }
            Infantries.Clear();
            foreach (StructureItem building in Buildings)
            {
                Engine.UnSelectBuindingAt(building);
            }
            Buildings.Clear();
            foreach (TerrainItem terr in Terrains)
            {
                Engine.UnSelectTerrainAt(terr);
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
                map.Units.RemoveByCoord(unit);
            }
            Units.Clear();
            foreach (InfantryItem inf in Infantries)
            {
                Engine.RemoveInfantryAt(inf, inf.SubCells);
                map.Infantries.RemoveByCoord(inf, inf.SubCells);
            }
            Infantries.Clear();
            foreach (StructureItem st in Buildings)
            {
                Engine.RemoveBuildingAt(st);
                map.Buildings.RemoveByCoord(st);
            }
            Buildings.Clear();
            foreach (TerrainItem terr in Terrains)
            {
                Engine.RemoveTerrainAt(terr);
                map.Terrains.RemoveByCoord(terr);
            }
            Terrains.Clear();
            foreach (OverlayUnit ov in Overlays)
            {
                Engine.RemoveOverlayAt(ov);
                map.Overlays.RemoveByCoord(ov);
            }
            Overlays.Clear();

            Engine.Refresh();
        }
        #endregion


        #region Public Calls - MainWindowDataModel
        public SelectingFlag SelectingFlags { get; set; } = SelectingFlag.Units | SelectingFlag.Infantries | SelectingFlag.Buildings | SelectingFlag.Terrains | SelectingFlag.Overlays;
        public SelectingBoxMode SelectingBoxFlag { get; set; } = SelectingBoxMode.ClientRectangle;
        public LightningItem LightningItem { get; set; }
        public List<InfantryItem> Infantries { get; private set; } = new List<InfantryItem>();
        public List<UnitItem> Units { get; private set; } = new List<UnitItem>();
        public List<StructureItem> Buildings { get; private set; } = new List<StructureItem>();
        public List<TerrainItem> Terrains { get; private set; } = new List<TerrainItem>();
        public List<OverlayUnit> Overlays { get; private set; } = new List<OverlayUnit>();
        #endregion
    }
}
