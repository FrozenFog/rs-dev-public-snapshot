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
                case MapObjectType.Unit:
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
#if DEBUG
        public static void DrawMap(Map map)
        {
            SetTheater(map.Info.TheaterType);
            foreach (Tile t in map.TilesData) EngineMain.DrawTile(t);
            foreach (InfantryItem inf in map.Infantries) EngineMain.DrawInfantry(inf, inf.GetHeight(), map.GetHouseColor(inf));
            foreach (UnitItem u in map.Units) EngineMain.DrawUnit(u, u.GetHeight(), map.GetHouseColor(u));
            foreach (AircraftItem air in map.Aircrafts) EngineMain.DrawAircraft(air, air.GetHeight(), map.GetHouseColor(air));
            foreach (StructureItem b in map.Buildings) EngineMain.DrawBuilding(b, b.GetHeight(), map.GetHouseColor(b));
            foreach (TerrainItem terr in map.Terrains) EngineMain.DrawTerrain(terr, terr.GetHeight());
            foreach (SmudgeItem smg in map.Smudges) EngineMain.DrawSmudge(smg, smg.GetHeight());
            foreach (OverlayUnit o in map.Overlays) EngineMain.DrawOverlay(o, o.GetHeight());
            foreach (WaypointItem wp in map.Waypoints) EngineMain.DrawWaypoint(wp, wp.GetHeight());
            foreach (CellTagItem ct in map.Celltags) EngineMain.DrawCelltag(ct, ct.GetHeight(), true);
            foreach (HouseItem house in map.Houses)
            {
                foreach (BaseNode node in house.BaseNodes) EngineMain.DrawBaseNode(node, node.GetHeight(), map.GetHouseColor(house.Name));
            }
            MoveCameraTo(map.CenterPoint, map.GetHeightFromTile(map.CenterPoint));
            RedrawMinimapAll();
        }
#else
        public static async Task DrawMap(Map map)
        {
            await Task.Run(() =>
            {
                SetTheater(map.Info.TheaterType);
                foreach (Tile t in map.TilesData) EngineMain.DrawTile(t);
                foreach (InfantryItem inf in map.Infantries) EngineMain.DrawInfantry(inf, inf.GetHeight(), map.GetHouseColor(inf));
                foreach (UnitItem u in map.Units) EngineMain.DrawUnit(u, u.GetHeight(), map.GetHouseColor(u));
                foreach (StructureItem b in map.Buildings) EngineMain.DrawBuilding(b, b.GetHeight(), map.GetHouseColor(b));
                foreach (TerrainItem terr in map.Terrains) EngineMain.DrawTerrain(terr, terr.GetHeight());
                foreach (SmudgeItem smg in map.Smudges) EngineMain.DrawSmudge(smg, smg.GetHeight());
                foreach (OverlayUnit o in map.Overlays) EngineMain.DrawOverlay(o, o.GetHeight());
                foreach (WaypointItem wp in map.Waypoints) EngineMain.DrawWaypoint(wp, wp.GetHeight());
                foreach (CellTagItem ct in map.Celltags) EngineMain.DrawCelltag(ct, ct.GetHeight(), true);
                MoveCameraTo(map.CenterPoint, map.GetHeightFromTile(map.CenterPoint));
                //RefreshFrame();
            });
        }
#endif
    }
}
