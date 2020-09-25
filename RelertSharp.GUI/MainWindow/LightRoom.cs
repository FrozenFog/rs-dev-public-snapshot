using RelertSharp.Common;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest
    {
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
                saved = false;
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
        private void RefreshLightningSilent()
        {
            if (drew && ckbLightningEnable.Checked)
            {
                GlobalVar.Engine.SetSceneLightning(Current.LightningItem);
                foreach (StructureItem s in map.Buildings) GlobalVar.Engine.SetObjectLampLightning(s, ckbLightningEnable.Checked);
                foreach (LightSource src in Map.LightSources) GlobalVar.Engine.SetObjectLampLightning(src);
            }
        }
    }
}
