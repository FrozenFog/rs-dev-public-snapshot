using RelertSharp.Common;
using RelertSharp.DrawingEngine.Presenting;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Points;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.DrawingEngine
{
    public partial class Engine
    {
        private Vec4 ambientLight = Vec4.One;
        private Vec4 sceneColor = Vec4.One;
        private float lightningLevel = 0.0f;


        public void IndicateBuildableTile(bool enable)
        {
            Vec4 buildable = new Vec4(0.6f, 1, 0.6f, 1);
            Vec4 unable = new Vec4(1, 0.6f, 0.6f, 1);
            if (enable)
            {
                foreach (Tile t in Map.TilesData)
                {
                    if (t.Buildable) t.SceneObject.SetColor(buildable);
                    else t.SceneObject.SetColor(unable);
                }
            }
            else
            {
                foreach (Tile t in Map.TilesData)
                {
                    t.SceneObject.SetColor(Vec4.One);
                }
            }
        }
        public void IndicatePassableTile(bool enable)
        {
            Vec4 passable = new Vec4(0.6f, 0.6f, 1, 1);
            Vec4 unable = new Vec4(1, 0.6f, 0.6f, 1);
            if (enable)
            {
                foreach (Tile t in Map.TilesData)
                {
                    if (t.Passable) t.SceneObject.SetColor(passable);
                    else t.SceneObject.SetColor(unable);
                }
            }
            else
            {
                foreach (Tile t in Map.TilesData)
                {
                    t.SceneObject.SetColor(Vec4.One);
                }
            }
        }
        private void ApplyLampAt(I2dLocateable pos, Vec4 color, int range)
        {
            foreach (Tile t in Map.TilesData) t.SceneObject.Lamped = false;
            Vec4 grade = (color - Vec4.One) / range;
            for (int r = 0; r <= range; r++)
            {
                Circle2D c = new Circle2D(pos, r);
                foreach (I2dLocateable target in c)
                {
                    if (Map.TilesData[target] is Tile t && !t.SceneObject.Lamped)
                    {
                        t.SceneObject.MultiplyColor(color);
                        t.SceneObject.Lamped = true;
                    }
                }
                color -= grade;
            }
        }
        public void SetObjectLampLightning(IMapObject item, bool enableLightning)
        {
            Vec4 lampcolor = GlobalRules.GetBuildingLampData(item.RegName, out float intensity, out int visibility);
            if (!enableLightning) lampcolor = Vec4.One;
            if (enableLightning && intensity == 0f || lampcolor == Vec4.One) return;
            int range = visibility >> 8;
            ApplyLampAt(item, lampcolor * Vec4.Unit3(1f + intensity * 1.5f), range + 1);
        }
        public void SetObjectLampLightning(LightSource src, bool enableLightning)
        {
            Vec4 color = src.ToVec4();
            if (enableLightning && src.IsEnable && src.Intensity != 0f && color != Vec4.One)
            {
                ApplyLampAt(src, color * Vec4.Unit3(1f + src.Intensity * 1.5f), src.Range);
            }
        }
        public void SetSceneLightning(LightningItem light)
        {
            Vec4 color = new Vec4(light.Red, light.Green, light.Blue, 1);
            Vec4 amb = Vec4.Unit3(light.Ambient);
            color *= amb;
            sceneColor = color;
            ambientLight = amb;
            lightningLevel = light.Level;
            foreach (Tile t in Map.TilesData)
            {
                Vec4 tilecolor = sceneColor;
                tilecolor += Vec4.Unit3(light.Level * t.Height);
                t.SceneObject.SetColor(tilecolor);
                foreach (IMapObject obj in t.GetObjects())
                {
                    if (obj is OverlayUnit o)
                    {
                        if (o.SceneObject.IsWall) o.SceneObject.SetColor(ambientLight);
                        else if (o.SceneObject.IsHiBridge)
                        {
                            Vec4 hicolor = Vec4.Unit3(light.Level * (o.SceneObject.Z + 4)) + sceneColor;
                            o.SceneObject.SetColor(hicolor);
                        }
                        else o.SceneObject.SetColor(sceneColor);
                    }
                    else obj.SceneObject.SetColor(ambientLight);
                }
            }
        }
        public void SetObjectLightningStandalone(IPresentBase src)
        {
            if (src.GetType() == typeof(PresentMisc))
            {
                PresentMisc msc = src as PresentMisc;
                if (msc.IsWall) msc.SetColor(ambientLight);
                else if (msc.IsHiBridge)
                {
                    Vec4 hicolor = Vec4.Unit3(lightningLevel * (msc.Z + 4)) + sceneColor;
                    msc.SetColor(hicolor);
                }
                else msc.SetColor(sceneColor);
            }
            else
            {
                src.SetColor(ambientLight);
            }
        }
    }
}
