using RelertSharp.Common;
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
        public static Vec4 SceneColor { get; private set; } = Vec4.One;
        public static Vec4 ObjectColor { get; private set; } = Vec4.One;
        public static void ApplyLightning(LightningItem color, bool enable)
        {
            if (!enable) color = LightningItem.None;
            EngineMain.SetSceneLightning(color, out var scene, out var obj);
            SceneColor = scene;
            ObjectColor = obj;
            foreach (StructureItem s in Map.Buildings) EngineMain.SetObjectLampLightning(s, enable);
            foreach (LightSource src in Map.LightSources) EngineMain.SetObjectLampLightning(src, enable);
        }
        public static void ApplyLightningToObject(IMapObject obj)
        {
            if (obj == null || obj.SceneObject == null) return;
            EngineMain.SetObjectLightning(obj);
        }
    }
}
