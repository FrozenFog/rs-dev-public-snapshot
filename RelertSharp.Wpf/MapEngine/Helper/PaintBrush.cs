using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using RelertSharp.Engine.Api;
using RelertSharp.Common;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;
using RelertSharp.MapStructure;
using RelertSharp.Wpf.Common;
using RelertSharp.Wpf.MapEngine.Helper;

namespace RelertSharp.Wpf
{
    internal static class PaintBrush
    {
        #region Components
        public static ObjectBrushConfig Config { get; private set; }
        public static ObjectBrushFilter Filter { get; private set; }
        private static Map Map { get { return GlobalVar.GlobalMap; } }
        private static IMapObject currentObject;
        private static bool isSuspended;
        #endregion
        #region Api
        public static void SetConfig(IMapObjectBrushConfig config, IObjectBrushFilter filter)
        {
            Config = config as ObjectBrushConfig;
            Filter = filter as ObjectBrushFilter;
        }
        public static void DisposeBrushObject()
        {
            currentObject?.Dispose();
        }
        public static void SuspendBrush()
        {
            if (!isSuspended)
            {
                currentObject?.Dispose();
                isSuspended = true;
            }
        }
        public static void ResumeBrush()
        {
            if (isSuspended)
            {
                if (currentObject != null) EngineApi.DrawObject(currentObject);
                isSuspended = false;
            }
        }
        #region Objects
        public static void LoadBrushObject(string regname, MapObjectType type, bool dispose = true)
        {
            bool isValid = true;
            Config.RegName = regname;
            if (dispose) currentObject?.Dispose();
            switch (type)
            {
                case MapObjectType.Infantry:
                    currentObject = new InfantryItem(regname);
                    break;
                case MapObjectType.Vehicle:
                    currentObject = new UnitItem(regname);
                    break;
                case MapObjectType.Building:
                    currentObject = new StructureItem(regname);
                    break;
                case MapObjectType.Aircraft:
                    currentObject = new AircraftItem(regname);
                    break;
                case MapObjectType.Terrain:
                    currentObject = new TerrainItem(regname);
                    break;
                case MapObjectType.Smudge:
                    currentObject = new SmudgeItem(regname);
                    break;
                case MapObjectType.Overlay:
                    currentObject = new OverlayUnit(Config.OverlayIndex, Config.OverlayFrame);
                    break;
                case MapObjectType.Celltag:
                    currentObject = new CellTagItem(Config.AttatchedTag);
                    break;
                case MapObjectType.Waypoint:
                    currentObject = new WaypointItem(Config.Pos, Config.WaypointNum);
                    break;
                default:
                    isValid = false;
                    break;
            }
            if (isValid)
            {
                currentObject.ApplyConfig(Config, Filter);
                EngineApi.DrawObject(currentObject);
            }
        }
        private static int prevSubcell = -1;
        private static I3dLocateable prevPos;
        /// <summary>
        /// Return true if needs redraw
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="subCell"></param>
        /// <returns></returns>
        public static bool MoveBrushObjectTo(I3dLocateable dest, int subCell = -1)
        {
            bool result = false;
            if (currentObject != null)
            {
                if (currentObject.ObjectType == MapObjectType.Infantry)
                {
                    if (subCell != prevSubcell || dest != prevPos) result = true;
                }
                else
                {
                    if (dest != prevPos) result = true;
                }
                currentObject.MoveTo(dest, subCell);
                Config.Pos.X = dest.X;
                Config.Pos.Y = dest.Y;
                Config.Height = dest.Z;
                Config.SubCell = subCell;
                prevSubcell = subCell;
                prevPos = dest;
            }
            return result;
        }
        public static void AddBrushObjectToMap()
        {
            if (currentObject != null && LayerControl.IsTypeVisible(currentObject.ObjectType))
            {
                IMapObject drawedObject = null;
                IObjectBrushFilter dummyFilter = new ObjectBrushFilter();
                switch (currentObject.ObjectType)
                {
                    case MapObjectType.Infantry:
                        drawedObject = MapApi.AddInfantry(Config, dummyFilter);
                        break;
                    case MapObjectType.Vehicle:
                        drawedObject = MapApi.AddUnit(Config, dummyFilter);
                        break;
                    case MapObjectType.Building:
                        drawedObject = MapApi.AddBuilding(Config, dummyFilter);
                        break;
                    case MapObjectType.Aircraft:
                        drawedObject = MapApi.AddAircraft(Config, dummyFilter);
                        break;
                    case MapObjectType.Terrain:
                        drawedObject = MapApi.AddTerrain(Config, dummyFilter);
                        break;
                    case MapObjectType.Smudge:
                        drawedObject = MapApi.AddSmudge(Config, dummyFilter);
                        break;
                    case MapObjectType.Overlay:
                        drawedObject = MapApi.AddOverlay(Config, dummyFilter);
                        break;
                    case MapObjectType.Celltag:
                        drawedObject = MapApi.AddCellTag(Config, dummyFilter);
                        break;
                    case MapObjectType.Waypoint:
                        drawedObject = MapApi.AddWaypoint(Config, dummyFilter);
                        break;
                }
                if (drawedObject != null)
                {
                    EngineApi.DrawObject(drawedObject);
                    EngineApi.ApplyLightningToObject(drawedObject);
                    UndoRedoHub.PushCommand(drawedObject);
                }
            }
        }
        public static void RefreshObjectAttribute()
        {
            currentObject.ApplyConfig(Config, Filter);
            currentObject?.Dispose();
            EngineApi.DrawObject(currentObject);
        }
        #endregion

        #endregion
    }
}
