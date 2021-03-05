using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Engine.Api
{
    public static partial class EngineApi
    {
        public static void ApplyLightning(LightningItem color, bool enable)
        {
            if (!enable) color = LightningItem.None;
            EngineMain.SetSceneLightning(color);
            foreach (StructureItem s in Map.Buildings) EngineMain.SetObjectLampLightning(s, enable);
            foreach (LightSource src in Map.LightSources) EngineMain.SetObjectLampLightning(src, enable);
        }
    }
}
