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
using RelertSharp.SubWindows.LogicEditor;
using RelertSharp.SubWindows.INIEditor;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest : Form
    {
        private Map map;
        private int tilecount = 0, objectcount = 0;
        private bool initialized = false;
        private List<string> failed = new List<string>();
        private MainWindowDataModel Current = new MainWindowDataModel();

        private LogicEditor logicEditor = new LogicEditor();
        private INIEditor iNIEditor = new INIEditor();

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

        private Map Map { get { return map; } }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            EngineInitialize(panel1.Handle, pnlMiniMap);
            DrawAll();
            GlobalVar.Engine.MoveTo(map.CenterPoint);
            pnlMiniMap.BackgroundImage = GlobalVar.Engine.MiniMap;
            GlobalVar.Engine.Refresh();
            drew = true;
            button1.Enabled = false;
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
                MmbDown(e);
            }
            else if (e.Button == MouseButtons.Left)
            {
                LmbDown(e);
            }
            else if (e.Button == MouseButtons.Right)
            {
                RmbDown(e);
            }
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            onMoving = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMoving(e);
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
                LmbClick(e);
            }
        }
        private void panel1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            HandlingKey(e);
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            panel1.Focus();
        }

        private void pnlMiniMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                minimapMoving = true;
            }
        }

        private void pnlMiniMap_MouseMove(object sender, MouseEventArgs e)
        {
            MinimapMoving(e);
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

        private void button2_Click(object sender, EventArgs e)
        {
            logicEditor.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            iNIEditor.Show();
        }

        private void pnlMiniMap_MouseClick(object sender, MouseEventArgs e)
        {
            minimapMoving = true;
            MinimapMoving(e);
            minimapMoving = false;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                MmbUp(e);
            }
            else if (e.Button == MouseButtons.Left)
            {
                LmbUp(e);
            }
        }
    }
}
