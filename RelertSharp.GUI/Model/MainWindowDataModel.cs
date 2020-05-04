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

            Engine.Refresh();
        }
        #endregion


        #region Public Calls - MainWindowDataModel
        public SelectingFlag SelectingFlags { get; set; } = SelectingFlag.Units | SelectingFlag.Infantries | SelectingFlag.Buildings;
        public LightningItem LightningItem { get; set; }
        public List<InfantryItem> Infantries { get; private set; } = new List<InfantryItem>();
        public List<UnitItem> Units { get; private set; } = new List<UnitItem>();
        public List<StructureItem> Buildings { get; private set; } = new List<StructureItem>();
        #endregion
    }
}
