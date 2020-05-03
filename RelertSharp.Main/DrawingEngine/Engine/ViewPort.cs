using System;
using System.Drawing;
using RelertSharp.MapStructure;
using RelertSharp.Common;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.DrawingEngine
{
    public partial class Engine
    {
        private TileLayer _cellFindingReferance;
        public Vec3 ClientPointToCellPos(Point src)
        {
            Pnt p = Pnt.FromPoint(src);
            Vec3 pos = new Vec3();
            CppExtern.Scene.ClientPositionToScenePosition(p, ref pos);
            pos += _NormTileVec * 12;
            for (int height = 0; height < 24; height++)
            {
                Vec3 tilepos = ScenePosToCoord(pos);
                if (_cellFindingReferance.HasTileOn(tilepos)) return tilepos;
                pos -= _NormTileVec / 2f;
            }
            return Vec3.Zero;
        }
        /// <summary>
        /// Return true if pos changed
        /// </summary>
        /// <param name="newpos"></param>
        /// <returns></returns>
        public bool SelectTile(Vec3 newpos)
        {
            if (newpos != previousTile)
            {
                Buffer.Scenes.MarkTile(newpos.ToCoord(), Vec4.TileIndicator, Vec4.TileExIndi, false);
                Buffer.Scenes.MarkTile(previousTile.ToCoord(), Vec4.Zero, Vec4.Zero, true);
                previousTile = newpos;
                return true;
            }
            return false;
        }
        public void MoveTo(int x, int y, int z = 0)
        {
            CppExtern.Scene.MoveFocusOnScene(ToVec3Iso(x, y, z));
        }
        public void ResetView()
        {
            CppExtern.Scene.ResetSceneView();
            Refresh();
        }
        public void ViewShift(Point previous, Point now)
        {
            Vec3 LT = new Vec3();
            CppExtern.Scene.ClientPositionToScenePosition(Pnt.Zero, ref LT);
            Vec3 coord = ScenePosToCoord(LT);
            minimap.ClientPos = new Point((int)coord.X, (int)coord.Y);
            Point delta = DeltaPoint(now, previous);
            CppExtern.Scene.MoveFocusOnScreen(delta.X * 1.2f, delta.Y * 1.2f);
            Refresh();
        }
        private Vec3 ScenePosToCoord(Vec3 px)
        {
            return new Vec3
                ((int)Math.Floor(px.X / _width),
                (int)Math.Floor(px.Y / _width),
                (int)Math.Floor(px.Z / _height));
        }
    }
}
