using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using RelertSharp.DrawingEngine;
using RelertSharp.FileSystem;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Points;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Logic;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using RelertSharp.GUI.Model;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest
    {
        private bool drew = false;
        private bool isLoading = false;
        private LoadingWindow lw;
        private List<string> _failed = new List<string>();


        private bool DrawAll(BackgroundWorker worker)
        {
            Log.Write("Begin Rendering");
            isLoading = true;
            lw = new LoadingWindow();
            lw.Show();
            lw.Refresh();
            lw.Begin();
            System.Runtime.GCSettings.LatencyMode = System.Runtime.GCLatencyMode.Batch;
            DrawTiles();
            DrawOverlays();
            DrawSmudges();
            DrawTerrains();
            DrawObjects();
            DrawWaypoints();
            DrawCelltags();
            System.Runtime.GCSettings.LatencyMode = System.Runtime.GCLatencyMode.Interactive;
            lw.EndDrawing();
            lw.Close();
            worker.ReportProgress(1);
            isLoading = false;
            return true;
        }
        private bool EngineInitialize(IntPtr mainHandle, Panel minimapPanel)
        {
            Log.Write("Engine Initializing");
#if RELEASE
            try
            {
#endif
                GlobalVar.Engine = new Engine();
                Engine eg = GlobalVar.Engine;
                Log.Write("Binding Engine Handle...");
                Log.Write(string.Format("MAINHANDLE-{0}, MINIHANDLE-{1}", mainHandle.ToString(), minimapPanel.Handle.ToString()));
                initialized = GlobalVar.Engine.Initialize(mainHandle, minimapPanel.Size, Map.Info.Size, Map.TilesData);
                Log.Write("Initializing Theater");
                GlobalVar.Engine.SetTheater(GlobalVar.GlobalConfig.GetTheater(Map.Info.TheaterName));
                Log.Write("Resetting...");
                GlobalVar.Engine.SetBackgroundColor(Color.FromArgb(30, 30, 30));
                GlobalVar.Engine.ResetMinimapWindow(panel1.Size);
                pnlTile.Initialize();
                Log.Write("ENGINE INITIALIZE COMPLETE");
                return true;
#if RELEASE
        }
            catch (Exception e)
            {
                GuiUtils.Fatal(string.Format("Fatal Error!\n Debug Message:{0}\n\nTrace:\n{1}", e.Message, e.StackTrace));
                Log.Write("======CRITICAL======    Engine Initialize failed!");
                return false;
            }
#endif

        }
        private void DrawTiles()
        {
            Log.Write("Drawing Tiles");
            lw.StartDrawing(Map.TilesData.Count(), "Tiles");
            foreach (Tile t in Map.TilesData)
            {
                lw.Incre();
                if (!GlobalVar.Engine.DrawGeneralItem(t))
                {
                    _failed.Add(string.Format("Tile in {0}", t.Coord));
                }
            }
            lw.EndItems(LoadingWindow.LoadingFlag.Tiles);
        }
        private void DrawTerrains()
        {
            Log.Write("Drawing Terrain");
            lw.StartDrawing(Map.Terrains.Count(), "Terrain");
            foreach (TerrainItem terr in Map.Terrains)
            {
                lw.Incre();
                if (!GlobalVar.Engine.DrawGeneralItem(terr, Map.GetHeightFromTile(terr)))
                {
                    _failed.Add(string.Format("Terrain in {0}", terr.CoordString));
                }
            }
            lw.EndItems(LoadingWindow.LoadingFlag.Terrains);
        }
        private void DrawSmudges()
        {
            Log.Write("Drawing Smudges");
            lw.StartDrawing(Map.Smudges.Count(), "Smudges");
            foreach (SmudgeItem smg in Map.Smudges)
            {
                lw.Incre();
                if (!GlobalVar.Engine.DrawGeneralItem(smg, Map.GetHeightFromTile(smg)))
                {
                    _failed.Add(string.Format("Smudge in {0}", smg.CoordString));
                }
            }
            lw.EndItems(LoadingWindow.LoadingFlag.Smudges);
        }
        private void DrawOverlays()
        {
            Log.Write("Drawing Overlays");
            lw.StartDrawing(Map.Overlays.Count(), "Overlays");
            foreach (OverlayUnit o in Map.Overlays)
            {
                lw.Incre();
                if (!GlobalVar.Engine.DrawGeneralItem(o, Map.GetHeightFromTile(o)))
                {
                    _failed.Add(string.Format("Overlay in {0}, id {1}", o.Coord, GlobalVar.GlobalRules.GetOverlayName(o.Index)));
                }
            }
            lw.EndItems(LoadingWindow.LoadingFlag.Overlays);
        }
        private void DrawWaypoints()
        {
            Log.Write("Drawing Waypoints");
            lw.StartDrawing(Map.Waypoints.Count(), "Waypoints");
            foreach (WaypointItem w in Map.Waypoints)
            {
                lw.Incre();
                if (!GlobalVar.Engine.DrawWaypoint(w, Map.GetHeightFromTile(w)))
                {
                    _failed.Add(string.Format("Waypoint in {0}", w.CoordString));
                }
            }
            lw.EndItems(LoadingWindow.LoadingFlag.Waypoints);
        }
        private void DrawCelltags()
        {
            Log.Write("Drawing Celltags");
            lw.StartDrawing(Map.Celltags.Count(), "Celltags");
            foreach (CellTagItem c in Map.Celltags)
            {
                lw.Incre();
                if (!GlobalVar.Engine.DrawCelltag(c, Map.GetHeightFromTile(c), true))
                {
                    _failed.Add(string.Format("Cellta at {0}", c.CoordString));
                }
            }
            lw.EndItems(LoadingWindow.LoadingFlag.Celltags);
        }
        private void DrawObjects()
        {
            Log.Write("Drawing Infantries");
            lw.StartDrawing(Map.Infantries.Count(), "Infantries");
            foreach (InfantryItem inf in Map.Infantries)
            {
                lw.Incre();
                if (!GlobalVar.Engine.DrawObject(inf, Map.GetHeightFromTile(inf), Map.GetHouseColor(inf.OwnerHouse)))
                    _failed.Add(inf.RegName);
            }
            lw.EndItems(LoadingWindow.LoadingFlag.Infantries);

            Log.Write("Drawing Buildings");
            lw.StartDrawing(Map.Buildings.Count(), "Buildings");
            foreach (StructureItem structure in Map.Buildings)
            {
                lw.Incre();
                if (!GlobalVar.Engine.DrawObject(structure, Map.GetHeightFromTile(structure), Map.GetHouseColor(structure.OwnerHouse)))
                    _failed.Add(structure.RegName);
            }

            Log.Write("Drawing Units");
            lw.StartDrawing(Map.Units.Count(), "Units");
            lw.EndItems(LoadingWindow.LoadingFlag.Buildings);
            foreach (UnitItem unit in Map.Units)
            {
                lw.Incre();
                if (!GlobalVar.Engine.DrawObject(unit, Map.GetHeightFromTile(unit), Map.GetHouseColor(unit.OwnerHouse)))
                    _failed.Add(unit.RegName);
            }
            lw.EndItems(LoadingWindow.LoadingFlag.Units);

            Log.Write("Drawing Aircrafts");
            lw.StartDrawing(Map.Aircrafts.Count(), "Aircrafts");
            foreach (AircraftItem air in Map.Aircrafts)
            {
                lw.Incre();
                if (!GlobalVar.Engine.DrawObject(air, Map.GetHeightFromTile(air), Map.GetHouseColor(air.OwnerHouse)))
                    _failed.Add(air.RegName);
            }
            lw.EndItems(LoadingWindow.LoadingFlag.Aircrafts);

            Log.Write("Drawing BaseNodes");
            lw.StartDrawing(Map.Houses.Count(), "BaseNodes");
            foreach (HouseItem house in Map.Houses)
            {
                lw.Incre();
                foreach (BaseNode node in house.BaseNodes)
                {
                    if (!GlobalVar.Engine.DrawObject(node, Map.GetHeightFromTile(node), Map.GetHouseColor(house.Name)))
                        _failed.Add("Node " + node.RegName);
                }
            }
            foreach (LightSource light in Map.LightSources)
            {
                if (!GlobalVar.Engine.DrawLightSource(light, Map.GetHeightFromTile(light)))
                    _failed.Add("Light source at " + light.CoordString);
            }
            lw.EndItems(LoadingWindow.LoadingFlag.BaseNodes);
        }
    }
}
