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

namespace RelertSharp.Wpf
{
    internal static class PaintBrush
    {
        #region Components
        public static ObjectBrushConfig Config { get; private set; }
        private static Map Map { get { return GlobalVar.CurrentMapDocument.Map; } }
        private static IMapObject currentObject;
        #endregion
        #region Api
        public static void SetConfig(IMapObjectBrushConfig config)
        {
            Config = config as ObjectBrushConfig;
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
                currentObject.ApplyConfig(Config);
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
            if (currentObject != null)
            {
                IMapObject drawedObject = null;
                switch (currentObject.ObjectType)
                {
                    case MapObjectType.Infantry:
                        drawedObject = MapApi.AddInfantry(Config);
                        break;
                    case MapObjectType.Unit:
                        drawedObject = MapApi.AddUnit(Config);
                        break;
                    case MapObjectType.Building:
                        drawedObject = MapApi.AddBuilding(Config);
                        break;
                    case MapObjectType.Aircraft:
                        drawedObject = MapApi.AddAircraft(Config);
                        break;
                    case MapObjectType.Terrain:
                        drawedObject = MapApi.AddTerrain(Config);
                        break;
                    case MapObjectType.Smudge:
                        drawedObject = MapApi.AddSmudge(Config);
                        break;
                    case MapObjectType.Overlay:
                        drawedObject = MapApi.AddOverlay(Config);
                        break;
                    case MapObjectType.Celltag:
                        drawedObject = MapApi.AddCellTag(Config);
                        break;
                    case MapObjectType.Waypoint:
                        drawedObject = MapApi.AddWaypoint(Config);
                        break;
                }
                if (drawedObject != null)
                {
                    EngineApi.DrawObject(drawedObject);
                }
            }
        }
        public static void RefreshObjectAttribute()
        {
            currentObject.ApplyConfig(Config);
            currentObject?.Dispose();
            EngineApi.DrawObject(currentObject);
        }
        #endregion
        #region Tiles

        #endregion

        #endregion
    }


    internal class ObjectBrushConfig : IMapObjectBrushConfig
    {
        #region Interface
        public string OwnerHouse { get; set; }

        public string RegName { get; set; }

        public I2dLocateable Pos { get; set; } = new Pnt();

        public int Height { get; set; }

        public int SubCell { get; set; } = -1;

        public int FacingRotation {
            get; set;
        }

        public string AttatchedTag {
            get; set;
        }

        public string MissionStatus {
            get; set;
        }

        public string Group {
            get; set;
        }

        public int HealthPoint {
            get; set;
        }

        public int VeterancyPercentage {
            get; set;
        }

        public bool AboveGround {
            get; set;
        }

        public bool AutoRecruitYes {
            get; set;
        }

        public bool AutoRecruitNo {
            get; set;
        }

        public string WaypointNum {
            get; set;
        }

        public byte OverlayIndex {
            get; set;
        }

        public byte OverlayFrame {
            get; set;
        }
        #endregion

    }
}
