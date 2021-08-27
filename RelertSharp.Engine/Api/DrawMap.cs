using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;
using RelertSharp.IniSystem;
using static RelertSharp.Common.GlobalVar;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;

namespace RelertSharp.Engine.Api
{
    public static partial class EngineApi
    {
        public static event MapDrawingProgressEventHandler DrawingProgressTick;
        public static event MapDrawingRegistEventHandler ProgressRegisted;
        public static event MapDrawingProgressEventHandler DrawingProgressCompleted;
        public static event EventHandler MapDrawingBegin;
        public static event Action MapDrawingComplete;
        public static void SetTheater(TheaterType type)
        {
            EngineMain.SetTheater(type);
            TheaterReloaded?.Invoke(null, null);
        }
        public static void DrawObject(IMapObject obj)
        {
            int height = obj.GetHeight();
            switch (obj.ObjectType)
            {
                case MapObjectType.Infantry:
                    InfantryItem inf = obj as InfantryItem;
                    EngineMain.DrawInfantry(inf, height, Map.GetHouseColor(inf));
                    break;
                case MapObjectType.Aircraft:
                    AircraftItem air = obj as AircraftItem;
                    EngineMain.DrawAircraft(air, height, Map.GetHouseColor(air));
                    break;
                case MapObjectType.Vehicle:
                    UnitItem unit = obj as UnitItem;
                    EngineMain.DrawUnit(unit, height, Map.GetHouseColor(unit));
                    break;
                case MapObjectType.Building:
                    StructureItem bud = obj as StructureItem;
                    EngineMain.DrawBuilding(bud, height, Map.GetHouseColor(bud));
                    break;
                case MapObjectType.Overlay:
                    OverlayUnit o = obj as OverlayUnit;
                    EngineMain.DrawOverlay(o, height);
                    break;
                case MapObjectType.Waypoint:
                    WaypointItem wp = obj as WaypointItem;
                    EngineMain.DrawWaypoint(wp, height);
                    break;
                case MapObjectType.BaseNode:
                    BaseNode node = obj as BaseNode;
                    EngineMain.DrawBaseNode(node, height, Map.GetHouseColor(node.Parent.Name));
                    break;
                case MapObjectType.Terrain:
                    TerrainItem ter = obj as TerrainItem;
                    EngineMain.DrawTerrain(ter, height);
                    break;
                case MapObjectType.Smudge:
                    SmudgeItem smg = obj as SmudgeItem;
                    EngineMain.DrawSmudge(smg, height);
                    break;
                case MapObjectType.Celltag:
                    CellTagItem cell = obj as CellTagItem;
                    EngineMain.DrawCelltag(cell, height, true);
                    break;
            }
        }
        public static void RedrawObject(IMapObject src)
        {
            Vec4 color = Vec4.Zero;
            if (src.SceneObject != null) color = src.SceneObject.ActualColor;
            src.SceneObject?.Dispose();
            DrawObject(src);
            if (color != Vec4.Zero) src.SceneObject.SetColor(color);
            if (src.IsSelected)
            {
                src.CancelSelection();
                src.Select();
            }
        }
        public static void DrawTile(Tile t)
        {
            EngineMain.DrawTile(t);
        }
        public static void DisposeMap()
        {
            CppExtern.Scene.ClearSceneObjects();
            MapDrawed = false;
        }
        private static void RegistDrawingProgress()
        {
            ProgressRegisted?.Invoke("Tiles", Map.TilesData.Count(), MapObjectType.Tile);
            ProgressRegisted?.Invoke("Infantries", Map.Infantries.Count(), MapObjectType.Infantry);
            ProgressRegisted?.Invoke("Units", Map.Units.Count(), MapObjectType.Vehicle);
            ProgressRegisted?.Invoke("Aircrafts", Map.Aircrafts.Count(), MapObjectType.Aircraft);
            ProgressRegisted?.Invoke("Buildings", Map.Buildings.Count(), MapObjectType.Building);
            ProgressRegisted?.Invoke("Terrains", Map.Terrains.Count(), MapObjectType.Terrain);
            ProgressRegisted?.Invoke("Smudges", Map.Smudges.Count(), MapObjectType.Smudge);
            ProgressRegisted?.Invoke("Overlays", Map.Overlays.Count(), MapObjectType.Overlay);
            ProgressRegisted?.Invoke("Waypoints", Map.Waypoints.Count(), MapObjectType.Waypoint);
            ProgressRegisted?.Invoke("Celltags", Map.Celltags.Count(), MapObjectType.Celltag);
            ProgressRegisted?.Invoke("Basenodes", Map.Houses.Sum(x => x.BaseNodes.Count), MapObjectType.BaseNode);
        }
        public static async Task DrawMap(Map map)
        {
            GlobalDir.SuspendLog();
            MapDrawingBegin?.Invoke(null, null);
            RegistDrawingProgress();
            EngineApi.InvokeLock();
            SetTheater(map.Info.TheaterType);
            await Task.Run(() =>
            {
                foreach (Tile t in map.TilesData)
                {
                    EngineMain.DrawTile(t);
                    DrawingProgressTick?.Invoke(MapObjectType.Tile);
                }
                DrawingProgressCompleted?.Invoke(MapObjectType.Tile);
                foreach (InfantryItem inf in map.Infantries)
                {
                    EngineMain.DrawInfantry(inf, inf.GetHeight(), map.GetHouseColor(inf));
                    DrawingProgressTick?.Invoke(MapObjectType.Infantry);
                }
                DrawingProgressCompleted?.Invoke(MapObjectType.Infantry);
                foreach (UnitItem u in map.Units)
                {
                    EngineMain.DrawUnit(u, u.GetHeight(), map.GetHouseColor(u));
                    DrawingProgressTick?.Invoke(MapObjectType.Vehicle);
                }
                DrawingProgressCompleted?.Invoke(MapObjectType.Vehicle);
                foreach (AircraftItem air in map.Aircrafts)
                {
                    EngineMain.DrawAircraft(air, air.GetHeight(), map.GetHouseColor(air));
                    DrawingProgressTick?.Invoke(MapObjectType.Aircraft);
                }
                DrawingProgressCompleted?.Invoke(MapObjectType.Aircraft);
                foreach (StructureItem b in map.Buildings)
                {
                    EngineMain.DrawBuilding(b, b.GetHeight(), map.GetHouseColor(b));
                    DrawingProgressTick?.Invoke(MapObjectType.Building);
                }
                DrawingProgressCompleted?.Invoke(MapObjectType.Building);
                foreach (TerrainItem terr in map.Terrains)
                {
                    EngineMain.DrawTerrain(terr, terr.GetHeight());
                    DrawingProgressTick?.Invoke(MapObjectType.Terrain);
                }
                DrawingProgressCompleted?.Invoke(MapObjectType.Terrain);
                foreach (SmudgeItem smg in map.Smudges)
                {
                    EngineMain.DrawSmudge(smg, smg.GetHeight());
                    DrawingProgressTick?.Invoke(MapObjectType.Smudge);
                }
                DrawingProgressCompleted?.Invoke(MapObjectType.Smudge);
                foreach (OverlayUnit o in map.Overlays)
                {
                    EngineMain.DrawOverlay(o, o.GetHeight());
                    DrawingProgressTick?.Invoke(MapObjectType.Overlay);
                }
                DrawingProgressCompleted?.Invoke(MapObjectType.Overlay);
                foreach (WaypointItem wp in map.Waypoints)
                {
                    EngineMain.DrawWaypoint(wp, wp.GetHeight());
                    DrawingProgressTick?.Invoke(MapObjectType.Waypoint);
                }
                DrawingProgressCompleted?.Invoke(MapObjectType.Waypoint);
                foreach (CellTagItem ct in map.Celltags)
                {
                    EngineMain.DrawCelltag(ct, ct.GetHeight(), true);
                    DrawingProgressTick?.Invoke(MapObjectType.Celltag);
                }
                DrawingProgressCompleted?.Invoke(MapObjectType.Celltag);
                foreach (HouseItem house in map.Houses)
                {
                    foreach (BaseNode node in house.BaseNodes)
                    {
                        EngineMain.DrawBaseNode(node, node.GetHeight(), map.GetHouseColor(house.Name));
                        DrawingProgressTick?.Invoke(MapObjectType.BaseNode);
                    }
                }
                DrawingProgressCompleted?.Invoke(MapObjectType.BaseNode);
            });
            MoveCameraTo(map.CenterPoint, map.GetHeightFromTile(map.CenterPoint));
            RedrawMinimapAll();
            EngineApi.InvokeUnlock();
            MapDrawed = true;
            MapDrawingComplete?.Invoke();
            GlobalDir.ResumeLog();
        }
        public static bool MapDrawed { get; private set; } = false;
    }
}
