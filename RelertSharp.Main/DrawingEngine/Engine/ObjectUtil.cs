using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using RelertSharp.FileSystem;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Points;
using RelertSharp.MapStructure.Objects;
using RelertSharp.DrawingEngine.Drawables;
using RelertSharp.DrawingEngine.Presenting;
using RelertSharp.Common;
using static RelertSharp.Utils.Misc;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.DrawingEngine
{
    public partial class Engine
    {
        #region Unit
        public void SelectUnitAt(I2dLocateable pos)
        {
            Buffer.Scenes.MarkUnit(pos.Coord);
        }
        public void UnSelectUnitAt(I2dLocateable pos)
        {
            Buffer.Scenes.UnMarkUnit(pos.Coord);
        }
        public void RemoveUnitAt(I2dLocateable pos)
        {
            Buffer.Scenes.RemoveUnitAt(pos.Coord);
        }
        public void UpdateUnitAttribute(UnitItem unit, int height, uint color, bool isSelected)
        {
            RemoveUnitAt(unit);
            DrawObject(unit, height, color);
            if (isSelected) SelectUnitAt(unit);
        }
        public void UpdateAircraftAttribute(AircraftItem air, int height, uint color, bool isSelected)
        {
            RemoveUnitAt(air);
            DrawObject(air, height, color);
            if (isSelected) SelectUnitAt(air);
        }
        #endregion


        #region Infantry
        public void SelectInfantryAt(I2dLocateable pos, int subcell)
        {
            Buffer.Scenes.MarkInfantry(pos.Coord, subcell);
        }
        public void UnSelectInfantryAt(I2dLocateable pos, int subcell)
        {
            Buffer.Scenes.UnMarkInfantry(pos.Coord, subcell);
        }
        public void RemoveInfantryAt(I2dLocateable pos, int subcell)
        {
            Buffer.Scenes.RemoveInfantryAt(pos.Coord, subcell);
        }
        public void UpdateInfantryAttribute(InfantryItem inf, int height, uint color, int subcell, bool isSelected)
        {
            RemoveInfantryAt(inf, subcell);
            DrawObject(inf, height, color);
            if (isSelected) SelectInfantryAt(inf, subcell);
        }
        #endregion


        #region Building
        public void SelectBuildingAt(I2dLocateable pos)
        {
            Buffer.Scenes.MarkBuilding(pos.Coord);
        }
        public void UnSelectBuindingAt(I2dLocateable pos)
        {
            Buffer.Scenes.UnMarkBuilding(pos.Coord);
        }
        public void RemoveBuildingAt(I2dLocateable pos)
        {
            Buffer.Scenes.RemoveBuildingAt(pos.Coord);
        }
        public void UpdateBuildingAttribute(StructureItem bud, int height, uint color, bool isSelected)
        {
            RemoveBuildingAt(bud);
            DrawObject(bud, height, color);
            if (isSelected) SelectBuildingAt(bud);
        }
        #endregion


        #region Misc
        public void SelectTerrainAt(I2dLocateable pos)
        {
            Buffer.Scenes.MarkTerrain(pos.Coord);
        }
        public void UnSelectTerrainAt(I2dLocateable pos)
        {
            Buffer.Scenes.UnMarkTerrain(pos.Coord);
        }
        public void RemoveTerrainAt(I2dLocateable pos)
        {
            Buffer.Scenes.RemoveTerrainAt(pos.Coord);
        }

        public void SelectOverlayAt(I2dLocateable pos)
        {
            Buffer.Scenes.MarkOverlay(pos.Coord);
        }
        public void UnSelectOverlayAt(I2dLocateable pos)
        {
            Buffer.Scenes.UnMarkOverlay(pos.Coord);
        }
        public void RemoveOverlayAt(I2dLocateable pos)
        {
            Buffer.Scenes.RemoveOverlayAt(pos.Coord);
        }
        #endregion
    }
}
