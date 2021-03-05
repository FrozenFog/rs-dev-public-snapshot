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

namespace RelertSharp.Engine.Api
{
    public static partial class EngineApi
    {
        public static event MapDrawingProgressEventHandler DrawingProgressTick;
#if RELEASE
        public static async Task DrawMap(Map map)
        {
            await Task.Run(() =>
            {
                EngineMain.SetTheater(map.Info.TheaterType);
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
#if DEBUG
        public static void DrawMap(Map map)
        {
            EngineMain.SetTheater(map.Info.TheaterType);
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
#endif
    }
}
