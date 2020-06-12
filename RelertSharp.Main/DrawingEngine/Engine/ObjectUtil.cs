﻿using System;
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
        #region Update Attribute
        //public void SelectUnitAt(I2dLocateable pos)
        //{
        //    Buffer.Scenes.MarkUnit(pos.Coord);
        //}
        //public void RemoveUnitAt(I2dLocateable pos)
        //{
        //    Buffer.Scenes.RemoveUnitAt(pos.Coord);

        //}
        public void UpdateUnitAttribute(UnitItem unit, int height, uint color)
        {
            bool selected = unit.Selected;
            Vec4 c = unit.SceneObject.ColorVector;
            unit.Dispose();
            DrawObject(unit, height, color);
            if (c != Vec4.Zero) unit.SceneObject.SetColor(c);
            if (selected) unit.Select();
        }
        public void UpdateAircraftAttribute(AircraftItem air, int height, uint color)
        {
            bool selected = air.Selected;
            Vec4 c = air.SceneObject.ColorVector;
            air.Dispose();
            DrawObject(air, height, color);
            if (c != Vec4.Zero) air.SceneObject.SetColor(c);
            if (selected) air.Select();
        }
        public void UpdateInfantryAttribute(InfantryItem inf, int height, uint color, int originalSubcell)
        {
            bool selected = inf.Selected;
            Vec4 c = inf.SceneObject.ColorVector;
            inf.Dispose();
            DrawObject(inf, height, color);
            if (c != Vec4.Zero) inf.SceneObject.SetColor(c);
            if (selected) inf.Select();
        }
        public void UpdateBuildingAttribute(StructureItem bud, int height, uint color)
        {
            bool selected = bud.Selected;
            Vec4 c = bud.SceneObject.ColorVector;
            bud.Dispose();
            DrawObject(bud, height, color);
            if (c != Vec4.Zero) bud.SceneObject.SetColor(c);
            if (selected) bud.Select();
        }
        public void RemoveDisposedObjects()
        {
            Buffer.Scenes.RemoveDisposedObject();
        }
        #endregion


        #region Infantry
        //public void SelectInfantryAt(I2dLocateable pos, int subcell)
        //{
        //    Buffer.Scenes.MarkInfantry(pos.Coord, subcell);
        //}
        //public void RemoveInfantryAt(I2dLocateable pos, int subcell)
        //{
        //    Buffer.Scenes.RemoveInfantryAt(pos.Coord, subcell);
        //}
        #endregion


        #region Building
        //public void SelectBuildingAt(I2dLocateable pos)
        //{
        //    Buffer.Scenes.MarkBuilding(pos.Coord);
        //}
        //public void RemoveBuildingAt(StructureItem bud)
        //{
        //    Buffer.Scenes.RemoveBuildingAt(bud.Coord);
        //}
        #endregion


        #region Misc
        //public void RemoveTerrainAt(I2dLocateable pos)
        //{
        //    Buffer.Scenes.RemoveTerrainAt(pos.Coord);
        //}
        //public void RemoveOverlayAt(I2dLocateable pos)
        //{
        //    Buffer.Scenes.RemoveOverlayAt(pos.Coord);
        //}
        #endregion
    }
}
