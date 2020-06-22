using System.Linq;
using RelertSharp.MapStructure;
using RelertSharp.DrawingEngine.Presenting;
using RelertSharp.Common;
using static RelertSharp.Utils.Misc;
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
                foreach (PresentTile p in Buffer.Scenes.Tiles.Values)
                {
                    if (p.Buildable) p.SetColor(buildable);
                    else p.SetColor(unable);
                }
                foreach (IPresentBase obj in Buffer.Scenes.OneCellObjects)
                {
                    if (!Buffer.Scenes.Tiles.Keys.Contains(obj.Coord)) continue;
                    Buffer.Scenes.Tiles[obj.Coord].SetColor(unable);
                }
                foreach (PresentStructure ps in Buffer.Scenes.Structures.Values)
                {
                    foreach (I2dLocateable pos in new Square2D(ps, ps.FoundationX, ps.FoundationY))
                    {
                        int coord = pos.Coord;
                        if (!Buffer.Scenes.Tiles.Keys.Contains(coord)) continue;
                        Buffer.Scenes.Tiles[coord].SetColor(unable);
                    }
                }
            }
            else
            {
                foreach (PresentTile p in Buffer.Scenes.Tiles.Values)
                {
                    p.SetColor(Vec4.One);
                }
            }
        }
        public void IndicatePassableTile(bool enable)
        {
            Vec4 passable = new Vec4(0.6f, 0.6f, 1, 1);
            Vec4 unable = new Vec4(1, 0.6f, 0.6f, 1);
            if (enable)
            {
                foreach (PresentTile p in Buffer.Scenes.Tiles.Values)
                {
                    if (p.LandPassable) p.SetColor(passable);
                    else p.SetColor(unable);
                }
                foreach (PresentStructure ps in Buffer.Scenes.Structures.Values)
                {
                    foreach (I2dLocateable pos in new Square2D(ps, ps.FoundationX, ps.FoundationY))
                    {
                        int coord = pos.Coord;
                        if (!Buffer.Scenes.Tiles.Keys.Contains(coord)) continue;
                        Buffer.Scenes.Tiles[coord].SetColor(unable);
                    }
                }
                foreach (PresentMisc msc in Buffer.Scenes.MiscWithoutSmudges)
                {
                    int coord = msc.Coord;
                    if (Buffer.Scenes.Tiles.Keys.Contains(coord))
                    {
                        if ((msc.IsTiberiumOverlay || msc.IsRubble) && !msc.IsMoveBlockingOverlay) Buffer.Scenes.Tiles[coord].SetColor(passable);
                        else Buffer.Scenes.Tiles[coord].SetColor(unable);
                    }
                }
            }
            else
            {
                foreach (PresentTile t in Buffer.Scenes.Tiles.Values)
                {
                    t.SetColor(Vec4.One);
                }
            }
        }
        private void ApplyLampAt(I2dLocateable pos, Vec4 color, int range)
        {
            Buffer.Scenes.BeginLamp();
            Vec4 grade = (color - Vec4.One) / range;
            for (int r = 0; r <= range; r++)
            {
                Circle2D c = new Circle2D(pos, r);
                foreach (I2dLocateable target in c)
                {
                    Buffer.Scenes.ColoringTile(target, color);
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
        public void SetSceneLightning(LightningItem light)
        {
            Vec4 color = new Vec4(light.Red, light.Green, light.Blue, 1);
            Vec4 amb = Vec4.Unit3(light.Ambient);
            color *= amb;
            sceneColor = color;
            ambientLight = amb;
            lightningLevel = light.Level;
            foreach (PresentTile t in Buffer.Scenes.Tiles.Values)
            {
                Vec4 tilecolor = sceneColor;
                tilecolor += Vec4.Unit3(light.Level * t.Height);
                t.SetColor(tilecolor);
            }
            foreach (IPresentBase obj in Buffer.Scenes.MapObjects) obj.SetColor(ambientLight);
            foreach (PresentMisc misc in Buffer.Scenes.MapMiscs)
            {
                if (misc.IsWall) misc.SetColor(ambientLight);
                else if (misc.IsHiBridge)
                {
                    Vec4 hiColor = Vec4.Unit3(light.Level * (misc.Z + 4)) + sceneColor;
                    misc.SetColor(hiColor);
                }
                else misc.SetColor(sceneColor);
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
