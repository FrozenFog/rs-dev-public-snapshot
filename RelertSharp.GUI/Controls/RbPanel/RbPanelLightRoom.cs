using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using RelertSharp.MapStructure.Points;
using RelertSharp.MapStructure;
using RelertSharp.Common;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.GUI.Controls
{
    public partial class RbPanelLightRoom : RbPanelBase
    {
        private LightSource current = null;
        private Map Map { get { return Common.GlobalVar.CurrentMapDocument.Map; } }
        internal event I2dLocateableHandler TraceLight;
        internal event EventHandler LightItemChanged;
        private LightSource Relocator { get; set; }
        internal bool Relocating = false;


        public RbPanelLightRoom()
        {
            InitializeComponent();
        }
        public void Initialize()
        {
            SetLanguage();
            updatingGui = true;
            lbxAllLight.LoadAs(Map.LightSources);
            updatingGui = false;
        }
        public void RelocateTo(I3dLocateable pos)
        {
            Relocator.MoveTo(pos);
            Relocating = false;
            lbxAllLight.UpdateAt(Relocator, ref updatingGui);
            lbxAllLight.Enabled = true;
        }


        private void LoadEntity(LightSource src)
        {
            SaveEntity();
            current = src;
            UpdateGui();
        }
        private void UpdateGui()
        {
            updatingGui = true;
            txbName.Text = current.Name;
            nmbxRed.Value = current.Red.AsDecimal();
            nmbxGreen.Value = current.Green.AsDecimal();
            nmbxBlue.Value = current.Blue.AsDecimal();
            nmbxIntensity.Value = current.Intensity.AsDecimal();
            nmbxVisibility.Value = current.Visibility;
            ckbEnabled.Checked = current.IsEnable;
            UpdateColorPreview();
            UpdateSatTracker();
            updatingGui = false;
        }
        private void UpdateColorPreview()
        {
            Color c = current.ToColor();
            Image img = new Bitmap(btnColor.Size.Width, btnColor.Size.Height);
            using (Graphics g = Graphics.FromImage(img))
            {
                g.Clear(c);
            }
            btnColor.Image = img;
            txbHex.Text = c.HexCode();
        }
        private void SaveEntity()
        {
            if (current != null)
            {
                current.Name = txbName.Text;
                current.Red = nmbxRed.Float();
                current.Green = nmbxGreen.Float();
                current.Blue = nmbxBlue.Float();
                current.Intensity = nmbxIntensity.Float();
                current.Visibility = nmbxVisibility.Int();
                current.IsEnable = ckbEnabled.Checked;
                LightItemChanged.Invoke(this, new EventArgs());
            }
        }

        private void UpdateSatTracker()
        {
            orgColor = new Vec3(nmbxRed.Value, nmbxGreen.Value, nmbxBlue.Value);
            tkbSaturation.Value = 50;
        }



        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog()
            {
                AllowFullOpen = true,
                AnyColor = true
            };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                current.FromColor(dlg.Color);
                UpdateGui();
                LightItemChanged.Invoke(this, new EventArgs());
            }
        }

        private void lbxAllLight_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lbxAllLight.SelectedItem is LightSource src && src.IsLocationSetted)
            {
                TraceLight?.Invoke(this, src);
            }
        }

        private bool updatingGui = false;
        private void lbxAllLight_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lbxAllLight.SelectedItem is LightSource src && !updatingGui)
            {
                LoadEntity(src);
                cmsLightItem.Enabled = true;
            }
        }

        private void RbPanelLightRoom_Leave(object sender, EventArgs e)
        {
            SaveEntity();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            LightSource src = new LightSource(Color.White, "DefaultLight");
            Map.LightSources.AddObject(src);
            if (src.SceneObject == null) GlobalVar.Engine.DrawBrushObject(src);
            lbxAllLight.Items.Add(src);
            lbxAllLight.SelectedItem = src;
        }

        private void tsmiSetSourcePos_Click(object sender, EventArgs e)
        {
            if (lbxAllLight.SelectedItem is LightSource src)
            {
                Relocating = true;
                Relocator = src;
                lbxAllLight.Enabled = false;
            }
        }

        private void EndEdit(object sender ,EventArgs e)
        {
            if (!updatingGui)
            {
                updatingGui = true;
                tmrUpdater.Enabled = true;
                tmrUpdater.Start();
            }
        }

        private Vec3 orgColor;
        private bool scrollingTracker = false;
        private void tkbSaturation_Scroll(object sender, EventArgs e)
        {
            if (!scrollingTracker)
            {
                scrollingTracker = true;
                tmrUpdater.Enabled = true;
                tmrUpdater.Start();
                Vec3 newColor = orgColor * (tkbSaturation.Value / 50f);
                nmbxRed.Value = (decimal)newColor.X;
                nmbxGreen.Value = (decimal)newColor.Y;
                nmbxBlue.Value = (decimal)newColor.Z;
                scrollingTracker = false;
            }
        }

        private void tmrUpdater_Tick(object sender, EventArgs e)
        {
            updatingGui = true;
            SaveEntity();
            lbxAllLight.UpdateAt(current, ref updatingGui);
            UpdateColorPreview();
            updatingGui = false;
            tmrUpdater.Enabled = false;
            tmrUpdater.Stop();
        }

        private void txbHex_Validated(object sender, EventArgs e)
        {
            if (!updatingGui)
            {
                string text = txbHex.Text;
                Color c = Color.White;
                if (text.StartsWith("#"))
                {
                    try
                    {
                        int value = Convert.ToInt32(text.Substring(1), 16);
                        c = Color.FromArgb(value);
                    }
                    catch
                    {
                        c = Color.White;
                    }
                    current.FromColor(c);
                    UpdateGui();
                    LightItemChanged.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
