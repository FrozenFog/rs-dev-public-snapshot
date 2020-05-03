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
        public void ReleaseAll()
        {
            foreach (UnitItem unit in Units)
            {
                Engine.UnSelectUnitAt(unit);
            }
            Units.Clear();

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

            Engine.Refresh();
        }
        #endregion


        #region Public Calls - MainWindowDataModel
        public SelectingFlag SelectingFlags { get; set; } = SelectingFlag.Units;
        public LightningItem LightningItem { get; set; }
        public List<InfantryItem> Infantries { get; private set; } = new List<InfantryItem>();
        public List<UnitItem> Units { get; private set; } = new List<UnitItem>();
        #endregion
    }
}
