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

namespace RelertSharp.GUI.Controls
{
    public partial class RbPanelLightRoom : RbPanelBase
    {
        private LightSource current = null;
        private Map Map { get { return Common.GlobalVar.CurrentMapDocument.Map; } }
        internal event I2dLocateableHandler TraceLight;


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
            UpdateColorPreview();
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
                current.Visibility = nmbxIntensity.Int();
                current.IsEnable = ckbEnabled.Checked;
            }
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
            lbxAllLight.Items.Add(src);
            lbxAllLight.SelectedItem = src;
        }
    }
}
