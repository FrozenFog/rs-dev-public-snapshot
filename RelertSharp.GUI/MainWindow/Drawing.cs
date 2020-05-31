﻿using System;
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
            isLoading = true;
            lw = new LoadingWindow();
            lw.Show();
            lw.Refresh();
            lw.Begin();
            GlobalVar.GlobalDir.BeginPreload();
            DrawTiles();
            DrawOverlays();
            DrawSmudges();
            DrawTerrains();
            DrawObjects();
            DrawWaypoints();
            DrawCelltags();
            GlobalVar.GlobalDir.DisposePreloaded();
            lw.EndDrawing();
            lw.Close();
            worker.ReportProgress(1);
            isLoading = false;
            return true;
        }
        private bool EngineInitialize(IntPtr mainHandle, Panel minimapPanel)
        {
            try
            {
                GlobalVar.Engine = new Engine();
                Engine eg = GlobalVar.Engine;
                initialized = GlobalVar.Engine.Initialize(mainHandle, minimapPanel.Size, map.Info.Size, map.TilesData);
                GlobalVar.Engine.SetTheater(GlobalVar.GlobalConfig.GetTheater(map.Info.TheaterName));
                GlobalVar.Engine.SetBackgroundColor(Color.FromArgb(30, 30, 30));
                GlobalVar.Engine.ResetMinimapWindow(panel1.Size);
                return true;
            }
            catch
            {
                return false;
            }

        }
        private void DrawTiles()
        {
            lw.StartDrawing(map.TilesData.Count(), "Tiles");
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
            lw.StartDrawing(map.Terrains.Count(), "Terrain");
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
            lw.StartDrawing(map.Smudges.Count(), "Smudges");
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
            lw.StartDrawing(map.Overlays.Count(), "Overlays");
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
            lw.StartDrawing(map.Waypoints.Count(), "Waypoints");
            foreach (WaypointItem w in map.Waypoints)
            {
                lw.Incre();
                if (!GlobalVar.Engine.DrawWaypoint(w, map.GetHeightFromTile(w)))
                {
                    _failed.Add(string.Format("Waypoint in {0}", w.CoordString));
                }
            }
            lw.EndItems(LoadingWindow.LoadingFlag.Waypoints);
        }
        private void DrawCelltags()
        {
            lw.StartDrawing(map.Celltags.Count(), "Celltags");
            foreach (CellTagItem c in map.Celltags)
            {
                lw.Incre();
                if (!GlobalVar.Engine.DrawCelltag(c, map.GetHeightFromTile(c), true))
                {
                    _failed.Add(string.Format("Cellta at {0}", c.CoordString));
                }
            }
            lw.EndItems(LoadingWindow.LoadingFlag.Celltags);
        }
        private void DrawObjects()
        {
            lw.StartDrawing(map.Infantries.Count(), "Infantries");
            foreach (InfantryItem inf in Map.Infantries)
            {
                lw.Incre();
                if (!GlobalVar.Engine.DrawObject(inf, Map.GetHeightFromTile(inf), Map.GetHouseColor(inf.OwnerHouse)))
                    _failed.Add(inf.RegName);
            }
            lw.EndItems(LoadingWindow.LoadingFlag.Infantries);
            lw.StartDrawing(map.Buildings.Count(), "Buildings");
            foreach (StructureItem structure in Map.Buildings)
            {
                lw.Incre();
                if (!GlobalVar.Engine.DrawObject(structure, Map.GetHeightFromTile(structure), Map.GetHouseColor(structure.OwnerHouse)))
                    _failed.Add(structure.RegName);
            }
            lw.StartDrawing(map.Units.Count(), "Units");
            lw.EndItems(LoadingWindow.LoadingFlag.Buildings);
            foreach (UnitItem unit in Map.Units)
            {
                lw.Incre();
                if (!GlobalVar.Engine.DrawObject(unit, Map.GetHeightFromTile(unit), Map.GetHouseColor(unit.OwnerHouse)))
                    _failed.Add(unit.RegName);
            }
            lw.EndItems(LoadingWindow.LoadingFlag.Units);
            lw.StartDrawing(map.Aircrafts.Count(), "Aircrafts");
            foreach (AircraftItem air in Map.Aircrafts)
            {
                lw.Incre();
                if (!GlobalVar.Engine.DrawObject(air, Map.GetHeightFromTile(air), Map.GetHouseColor(air.OwnerHouse)))
                    _failed.Add(air.RegName);
            }
            lw.EndItems(LoadingWindow.LoadingFlag.Aircrafts);
            lw.StartDrawing(map.Houses.Count(), "BaseNodes");
            foreach (HouseItem house in Map.Houses)
            {
                lw.Incre();
                foreach (BaseNode node in house.BaseNodes)
                {
                    if (!GlobalVar.Engine.DrawObject(node, Map.GetHeightFromTile(node), Map.GetHouseColor(house.Name)))
                        _failed.Add("Node " + node.RegName);
                }
            }
            lw.EndItems(LoadingWindow.LoadingFlag.BaseNodes);
        }
    }
}
