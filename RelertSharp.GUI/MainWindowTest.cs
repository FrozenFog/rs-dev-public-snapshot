using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using RelertSharp.DrawingEngine;
using RelertSharp.FileSystem;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Points;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Logic;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using RelertSharp.GUI.Model;
using RelertSharp.GUI.SubWindows.LogicEditor;
using RelertSharp.SubWindows.INIEditor;
using RelertSharp.GUI.Controls;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest : Form
    {
        private static RsLog Log { get { return GlobalVar.Log; } }
        private bool initialized = false;
        private bool saved = true;
        private bool requireFocus = false;
        private bool isBusy = false;
        private MainWindowDataModel Current = new MainWindowDataModel();

        private readonly LogicEditor logicEditor = new LogicEditor();
        private readonly INIEditor iNIEditor = new INIEditor();
        private readonly MapVerifyForm verifyForm = new MapVerifyForm();

        public MainWindowTest()
        {
            Log.Write("Mainwindow Initializing");
            InitializeComponent();
            InitializeControl();
            GlobalVar.GlobalRules.MapIniData = Map.IniResidue;
            panel1.BackColor = Color.FromArgb(30, 30, 30);
            cbbLightningType.SelectedIndex = 0;
            Log.Write("Mainwindow Initialized");
        }

        private void InitializeControl()
        {
            (panel1 as Control).KeyDown += new KeyEventHandler(panel1_KeyDown);
            (panel1 as Control).KeyUp += new KeyEventHandler(panel1_KeyUp);


            foreach (Control c in Controls) c.SetLanguage();
            cmsToolSelect.SetLanguage();
            Text = Language.DICT[Text] + Constant.ReleaseDate;

            InitializeHandler();

            pnlPick.Initialize();
            tmrAutosave.Interval = GlobalVar.GlobalConfig.Local.AutoSaveTimeMilSec;
            tmrAutosave.Enabled = true;
            tsmiMainDevMode.Checked = GlobalVar.GlobalConfig.Local.DevMode;

            initialized = true;
        }


        protected override void WndProc(ref Message m)
        {
            if (!isLoading)
            {
                base.WndProc(ref m);
            }
        }

        private Map Map { get { return GlobalVar.CurrentMapDocument.Map; } }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            if(EngineInitialize(panel1.Handle, pnlMiniMap))
            {
                bgwDraw.RunWorkerAsync();
            }
            else
            {
                GuiUtils.Fatal("Engine Initialize Failed!!");
            }
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            if (initialized && drew)
            {
                GlobalVar.Engine.ResetView();
                GlobalVar.Engine.ResetMinimapWindow(panel1.Size);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (drew)
            {
                saved = false;
                panel1.Focus();
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

        private void ckbLightningType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = cbbLightningType.Text;
            switch (s)
            {
                case "LightningStorm":
                    Current.LightningItem = Map.LightningCollection.Ion;
                    break;
                case "Normal":
                    Current.LightningItem = Map.LightningCollection.Normal;
                    break;
                case "Dominator":
                    Current.LightningItem = Map.LightningCollection.Dominator;
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
                    WriteLightningSide(Map.LightningCollection.Ion);
                    break;
                case "Normal":
                    WriteLightningSide(Map.LightningCollection.Normal);
                    break;
                case "Dominator":
                    WriteLightningSide(Map.LightningCollection.Dominator);
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
                RedrawMinimapAll();
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
            e.IsInputKey = true;
            HandlingKey(e);
        }
        private void panel1_KeyDown(object sender, KeyEventArgs e)
        {
            HandlingKeyDown(e);
        }
        private void panel1_KeyUp(object sender, KeyEventArgs e)
        {
            HandlingKeyUp(e);
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
            logicEditor.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            iNIEditor.Show();
            iNIEditor.BringToFront();
        }

        private void pnlMiniMap_MouseClick(object sender, MouseEventArgs e)
        {
            minimapMoving = true;
            MinimapMoving(e);
            minimapMoving = false;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (drew)
            {
                if (e.Button == MouseButtons.Middle)
                {
                    MmbUp(e);
                }
                else if (e.Button == MouseButtons.Left)
                {
                    LmbUp(e);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    RmbUp(e);
                }
            }
        }

        private void ToolBoxClickHandler(object sender, EventArgs e)
        {
            ToolBoxClick(sender as ToolStripButton);
        }

        private void txbCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.OemQuestion && e.Control)
            {
                txbCommand.Visible = false;
                panel1.Focus();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ExecuteCommand(txbCommand.Text);
                txbCommand.Visible = false;
                panel1.Focus();
            }
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LmbDoubleClicked(e);
            }
        }
        private void bgwDraw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if (DrawAll(worker)) pnlPick.DrawComplete();
            GlobalVar.Engine.MoveTo(Map.CenterPoint);
            GlobalVar.Engine.Refresh();
            RedrawMinimapAll();
            rbPanelAttribute.Initialize(Map.Houses, Map.Tags);
            rbPanelBrush.Initialize();
            rbPanelWand.Initialize();
            rbPanelBucket.Initialize();
            rbPanelLightRoom.Initialize();
            drew = true;
        }

        private void bgwDraw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 1) // basic drawing done
            {
                button1.Enabled = false;
                listBox1.Items.Clear();
                listBox1.Items.AddRange(_failed.ToArray());
                ToolBoxClick(toolBtnArrow);
                prevCur = panel1.Cursor;
            }
        }

        private void ToolBoxRightClickHandler(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point pos = (sender as ToolStripButton).GetCurrentParent().PointToScreen(e.Location);
                pos.Y += 24;
                ToolBoxRightClick(sender as ToolStripButton, pos);
            }
        }

        private void panel1_MouseEnter_1(object sender, EventArgs e)
        {
            PanelMouseEnter();
        }

        private void MainWindowTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!saved || !logicEditor.ChangeSaved)
            {
                DialogResult d = MessageBox.Show("Save changes to map file?", "Relert Sharp", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (d == DialogResult.Cancel) e.Cancel = true;
                else if (d == DialogResult.Yes)
                {
                    tsmiMainSaveMapAs_Click(null, null);
                }
            }
        }

        private void tmrAutosave_Tick(object sender, EventArgs e)
        {
            if (!isBusy)
            {
                DateTime now = DateTime.Now;
                string path = string.Format("{0}\\AutoSave\\", Application.StartupPath);
                string name = string.Format("{0}-{1}-{2} {3}-{4}-{5}.map", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                GlobalVar.CurrentMapDocument.SaveMapAs(path, name);
            }
        }
    }
}
