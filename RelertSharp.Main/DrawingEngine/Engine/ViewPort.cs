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
        /// Return Zero and set Infantry SubCell for -1 if not found
        /// </summary>
        /// <param name="src"></param>
        /// <param name="infsubcell"></param>
        /// <returns></returns>
        public Vec3 ClientPointToCellPos(Point src, out int infsubcell)
        {
            Pnt p = Pnt.FromPoint(src);
            Vec3 scenepos = new Vec3();
            CppExtern.Scene.ClientPositionToScenePosition(p, ref scenepos);
            scenepos += _NormTileVec * 12;
            int height = 0;
            Vec3 tileCoord = new Vec3();
            for (; height < 24; height++)
            {
                tileCoord = ScenePosToCoord(scenepos);
                if (_cellFindingReferance.HasTileOn(tileCoord))
                {
                    infsubcell = GetSubCellFromCoord(scenepos, tileCoord.To2dLocateable());
                    return tileCoord;
                }
                scenepos -= _NormTileVec / 2f;
            }
            infsubcell = -1;
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
        public void MoveTo(I3dLocateable pos)
        {
            CppExtern.Scene.SetFocusOnScene(ToVec3Iso(pos));
        }
        public void MoveTo(I2dLocateable pos)
        {
            CppExtern.Scene.SetFocusOnScene(ToVec3Iso(pos));
            SetMinimapClientPos();
        }
        public void ResetView()
        {
            CppExtern.Scene.ResetSceneView();
            Refresh();
        }
        public void ViewShift(Point previous, Point now)
        {
            Point delta = DeltaPoint(now, previous);
            CppExtern.Scene.MoveFocusOnScreen(delta.X * 1.2f, delta.Y * 1.2f);
            SetMinimapClientPos();
            Refresh();
        }
        private Vec3 ScenePosToCoord(Vec3 px)
        {
            return new Vec3
                ((int)Math.Floor(px.X / _width),
                (int)Math.Floor(px.Y / _width),
                (int)Math.Floor(px.Z / _height));
        }
        private int GetSubCellFromCoord(Vec3 scenePos, I2dLocateable tilepos)
        {
            float x = scenePos.X / _width;
            float y = scenePos.Y / _width;
            float dx = x - tilepos.X;
            float dy = y - tilepos.Y;
            int subcell = 1;
            if (dy - dx > 0.25f && dx < 0.5f) subcell = 2;
            else if (dy - dx < -0.25f && dy < 0.5f) subcell = 3;
            return subcell;
        }
    }
}
