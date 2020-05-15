using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        private bool DrawAll()
        {
            GlobalVar.GlobalDir.BeginPreload();
            DrawTiles();
            DrawOverlays();
            DrawSmudges();
            DrawTerrains();
            DrawObjects();
            //DrawWaypoints();
            DrawCelltags();
            GlobalVar.GlobalDir.DisposePreloaded();
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
            foreach (Tile t in Map.TilesData)
            {
                tilecount++;
                if (!GlobalVar.Engine.DrawGeneralItem(t))
                {
                    failed.Add(string.Format("Tile in {0}", t.Coord));
                }
            }
        }
        private void DrawTerrains()
        {
            foreach (TerrainItem terr in Map.Terrains)
            {
                objectcount++;
                if (!GlobalVar.Engine.DrawGeneralItem(terr, Map.GetHeightFromTile(terr)))
                {
                    failed.Add(string.Format("Terrain in {0}", terr.CoordString));
                }
            }
        }
        private void DrawSmudges()
        {
            foreach (SmudgeItem smg in Map.Smudges)
            {
                objectcount++;
                if (!GlobalVar.Engine.DrawGeneralItem(smg, Map.GetHeightFromTile(smg)))
                {
                    failed.Add(string.Format("Smudge in {0}", smg.CoordString));
                }
            }
        }
        private void DrawOverlays()
        {
            foreach (OverlayUnit o in Map.Overlays)
            {
                objectcount++;
                if (!GlobalVar.Engine.DrawGeneralItem(o, Map.GetHeightFromTile(o)))
                {
                    failed.Add(string.Format("Overlay in {0}, id {1}", o.Coord, GlobalVar.GlobalRules.GetOverlayName(o.Index)));
                }
            }
        }
        private void DrawWaypoints()
        {
            foreach (WaypointItem w in map.Waypoints)
            {
                if (!GlobalVar.Engine.DrawWaypoint(w, map.GetHeightFromTile(w)))
                {
                    failed.Add(string.Format("Waypoint in {0}", w.CoordString));
                }
            }
        }
        private void DrawCelltags()
        {
            foreach (CellTagItem c in map.Celltags)
            {
                if (!GlobalVar.Engine.DrawCelltag(c, map.GetHeightFromTile(c), true))
                {
                    failed.Add(string.Format("Cellta at {0}", c.CoordString));
                }
            }
        }
        private void DrawObjects()
        {
            foreach (InfantryItem inf in Map.Infantries)
            {
                objectcount++;
                if (!GlobalVar.Engine.DrawObject(inf, Map.GetHeightFromTile(inf), Map.GetHouseColor(inf.OwnerHouse)))
                    failed.Add(inf.RegName);
            }
            foreach (StructureItem structure in Map.Buildings)
            {
                objectcount++;
                if (!GlobalVar.Engine.DrawObject(structure, Map.GetHeightFromTile(structure), Map.GetHouseColor(structure.OwnerHouse)))
                    failed.Add(structure.RegName);
            }
            foreach (UnitItem unit in Map.Units)
            {
                objectcount++;
                if (!GlobalVar.Engine.DrawObject(unit, Map.GetHeightFromTile(unit), Map.GetHouseColor(unit.OwnerHouse)))
                    failed.Add(unit.RegName);
            }
            foreach (AircraftItem air in Map.Aircrafts)
            {
                objectcount++;
                if (!GlobalVar.Engine.DrawObject(air, Map.GetHeightFromTile(air), Map.GetHouseColor(air.OwnerHouse)))
                    failed.Add(air.RegName);
            }
            foreach (HouseItem house in Map.Houses)
            {
                foreach (BaseNode node in house.BaseNodes)
                {
                    objectcount++;
                    if (!GlobalVar.Engine.DrawObject(node, Map.GetHeightFromTile(node), Map.GetHouseColor(house.Name)))
                        failed.Add("Node " + node.RegName);
                }
            }
            listBox1.Items.AddRange(failed.ToArray());
        }
    }
}
