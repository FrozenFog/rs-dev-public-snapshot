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
    public partial class MainWindowTest : Form
    {
        private Map map;
        private int tilecount = 0, objectcount = 0;
        private bool initialized = false;
        private bool onMoving = false;
        private bool drew = false;
        private bool lightningset = false;
        private Point previousLocation;
        private List<string> failed = new List<string>();
        private MainWindowDataModel Current = new MainWindowDataModel();


        public MainWindowTest()
        {
            InitializeComponent();
            map = GlobalVar.CurrentMapDocument.Map;
            GlobalVar.GlobalRules.MapIniData = map.IniResidue;
            panel1.BackColor = Color.FromArgb(30, 30, 30);
            cbbLightningType.SelectedIndex = 0;
        }

        private bool updatingLightningData = false;
        private void UpdateLightningSide(LightningItem src, bool enable)
        {
            updatingLightningData = true;
            nmbxLightningAmbient.Enabled = enable;
            nmbxLightningBlue.Enabled = enable;
            nmbxLightningGreen.Enabled = enable;
            nmbxLightningGround.Enabled = enable;
            nmbxLightningLevel.Enabled = enable;
            nmbxLightningRed.Enabled = enable;
            btnLightningRefresh.Enabled = enable;
            nmbxLightningAmbient.Value = (decimal)src.Ambient;
            nmbxLightningBlue.Value = (decimal)src.Blue;
            nmbxLightningGreen.Value = (decimal)src.Green;
            nmbxLightningGround.Value = (decimal)src.Ground;
            nmbxLightningLevel.Value = (decimal)src.Level;
            nmbxLightningRed.Value = (decimal)src.Red;
            updatingLightningData = false;
        }
        private void WriteLightningSide(LightningItem dest)
        {
            if (!updatingLightningData)
            {
                dest.Red = (float)nmbxLightningRed.Value;
                dest.Green = (float)nmbxLightningGreen.Value;
                dest.Blue = (float)nmbxLightningBlue.Value;
                dest.Ambient = (float)nmbxLightningAmbient.Value;
                dest.Ground = (float)nmbxLightningGround.Value;
                dest.Level = (float)nmbxLightningLevel.Value;
            }
        }
        private void ApplyLightning(LightningItem color)
        {
            if (drew)
            {
                GlobalVar.Engine.SetSceneLightning(color);
                foreach (StructureItem s in map.Buildings)
                {
                    GlobalVar.Engine.SetObjectLampLightning(s, ckbLightningEnable.Checked);
                }
                GlobalVar.Engine.Refresh();
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

        private Map Map { get { return map; } }

        private void button1_Click(object sender, EventArgs e)
        {
            GlobalVar.Engine = new Engine();
            Engine eg = GlobalVar.Engine;
            initialized = GlobalVar.Engine.Initialize(panel1.Handle, pnlMiniMap.Size, map.Info.Size, map.TilesData);
            GlobalVar.Engine.SetTheater(GlobalVar.GlobalConfig.GetTheater(map.Info.TheaterName));
            GlobalVar.Engine.SetBackgroundColor(Color.FromArgb(30, 30, 30));
            GlobalVar.GlobalDir.BeginPreload();
            DrawTiles();
            DrawOverlays();
            DrawSmudges();
            DrawTerrains();
            DrawObjects();
            DrawWaypoints();
            DrawCelltags();
            GlobalVar.GlobalDir.DisposePreloaded();
            GlobalVar.Engine.ResetMinimapWindow(panel1.Size);
            pnlMiniMap.BackgroundImage = GlobalVar.Engine.MiniMap;
            GlobalVar.Engine.Refresh();
            initialized = true;
            drew = true;
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            if (initialized)
            {
                GlobalVar.Engine.ResetView();
                GlobalVar.Engine.ResetMinimapWindow(panel1.Size);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                onMoving = true;
                previousLocation = e.Location;
            }
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            onMoving = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (initialized)
            {
                Vec3 pos = GlobalVar.Engine.ClientPointToCellPos(e.Location);
                lblMouseX.Text = string.Format("MouseX : {0}", e.Location.X);
                lblMouseY.Text = string.Format("MouseY : {0}", e.Location.Y);
                if (pos != Vec3.Zero)
                {
                    lblx.Text = string.Format("X : {0}", pos.X);
                    lbly.Text = string.Format("Y : {0}", pos.Y);
                    lblz.Text = string.Format("Z : {0}", pos.Z);
                    if (GlobalVar.Engine.SelectTile(pos)) GlobalVar.Engine.Refresh();
                }
                if (onMoving)
                {
                    GlobalVar.Engine.ViewShift(previousLocation, e.Location);
                    pnlMiniMap.BackgroundImage = GlobalVar.Engine.MiniMap;
                    previousLocation = e.Location;
                }
            }
        }

        private void ckbLight_CheckedChanged(object sender, EventArgs e)
        {
            bool enable = ckbLightningEnable.Checked;
            ckbBuildableTiles.Enabled = !enable;
            ckbGroundPassableTiles.Enabled = !enable;
            LightningItem color;
            if (!enable) color = new LightningItem();
            else color = Current.LightningItem;
            ApplyLightning(color);
            UpdateLightningSide(color, enable);
        }
        private void Panelchecker_CheckedChanged0(object sender, EventArgs e)
        {
            Panel parent = (Panel)((CheckBox)sender).Parent;
            parent.Controls[0].Visible = ((CheckBox)sender).Checked;
        }

        private void ckbLightningType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = cbbLightningType.Text;
            switch (s)
            {
                case "LightningStorm":
                    Current.LightningItem = map.LightningCollection.Ion;
                    break;
                case "Normal":
                    Current.LightningItem = map.LightningCollection.Normal;
                    break;
                case "Dominator":
                    Current.LightningItem = map.LightningCollection.Dominator;
                    break;
                default:
                    return;
            }
            if (!ckbLightningEnable.Checked) return;
            ApplyLightning(Current.LightningItem);
            UpdateLightningSide(Current.LightningItem, ckbLightningEnable.Checked);
        }

        private void btnLightningRefresh_Click(object sender, EventArgs e)
        {
            ApplyLightning(Current.LightningItem);
        }

        private void nmbxLightningRed_ValueChanged(object sender, EventArgs e)
        {
            switch (cbbLightningType.Text)
            {
                case "LightningStorm":
                    WriteLightningSide(map.LightningCollection.Ion);
                    break;
                case "Normal":
                    WriteLightningSide(map.LightningCollection.Normal);
                    break;
                case "Dominator":
                    WriteLightningSide(map.LightningCollection.Dominator);
                    break;
            }
            WriteLightningSide(Current.LightningItem);
        }

        private void ckbBuildableTiles_CheckedChanged(object sender, EventArgs e)
        {
            if (initialized && drew)
            {
                ckbLightningEnable.Enabled = !ckbBuildableTiles.Checked;
                ckbGroundPassableTiles.Enabled = !ckbBuildableTiles.Checked;
                GlobalVar.Engine.IndicateBuildableTile(ckbBuildableTiles.Checked);
                GlobalVar.Engine.Refresh();
            }
        }

        private void ckbGroundPassableTiles_CheckedChanged(object sender, EventArgs e)
        {
            if (initialized && drew)
            {
                ckbLightningEnable.Enabled = !ckbGroundPassableTiles.Checked;
                ckbBuildableTiles.Enabled = !ckbGroundPassableTiles.Checked;
                GlobalVar.Engine.IndicatePassableTile(ckbGroundPassableTiles.Checked);
                GlobalVar.Engine.Refresh();
            }
        }

        private void pnlMiniMap_SizeChanged(object sender, EventArgs e)
        {
            if (initialized && drew)
            {
                GlobalVar.Engine.ResizeMinimap(pnlMiniMap.Size);
                pnlMiniMap.BackgroundImage = GlobalVar.Engine.MiniMap;
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var flag = Current.SelectingFlags;
                if (flag == MainWindowDataModel.SelectingFlag.None) return;
                I2dLocateable pos = GlobalVar.Engine.ClientPointToCellPos(e.Location).To2dLocateable();
                if ((flag | MainWindowDataModel.SelectingFlag.Units) != 0)
                {
                    Current.SelectUnitAt(pos);
                }
                if ((flag | MainWindowDataModel.SelectingFlag.Infantries) != 0)
                {
                    I2dLocateable infpos = GlobalVar.Engine.ClientPointToCellPos(e.Location, out int subcell).To2dLocateable();
                    Current.SelectInfantryAt(infpos, subcell);
                }
                if ((flag | MainWindowDataModel.SelectingFlag.Buildings) != 0)
                {
                    Current.SelectBuildingAt(pos);
                }
                if ((flag | MainWindowDataModel.SelectingFlag.Terrains) != 0)
                {
                    Current.SelectTerrainAt(pos);
                }
                if ((flag|MainWindowDataModel.SelectingFlag.Overlays) != 0)
                {
                    Current.SelectOverlayAt(pos);
                }
            }
        }
        private void panel1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Current.ReleaseAll();
            }
            if (e.KeyCode == Keys.Delete)
            {
                Current.RemoveAll();
            }
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            panel1.Focus();
        }

        private bool minimapMoving = false;
        private void pnlMiniMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                minimapMoving = true;
            }
        }

        private void pnlMiniMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (initialized && drew && minimapMoving)
            {
                GlobalVar.Engine.MinimapMoving(e.Location);
                pnlMiniMap.BackgroundImage = GlobalVar.Engine.MiniMap;
            }
        }

        private void pnlMiniMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                minimapMoving = false;
            }
        }

        private void pnlMiniMap_MouseLeave(object sender, EventArgs e)
        {
            minimapMoving = false;
        }

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            pnlMiniMap_SizeChanged(sender, e);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                panel1_MouseLeave(null, null);
            }
        }
    }
}
