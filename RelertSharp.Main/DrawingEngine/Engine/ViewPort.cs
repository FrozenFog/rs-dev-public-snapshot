using System;
using System.Drawing;
using System.Collections.Generic;
using RelertSharp.MapStructure;
using RelertSharp.Common;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.DrawingEngine
{
    public partial class Engine
    {
        private TileLayer _cellFindingReferance;
        private Vec3 previousTile = Vec3.Zero;

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
        public Point CellPosToClientPos(I3dLocateable pos)
        {
            Pnt result = new Pnt();
            CppExtern.Scene.ScenePositionToClientPosition(ToVec3Iso(pos), ref result);
            return result.ToPoint();
        }
        public I3dLocateable GetPreviousLegalTile()
        {
            return previousTile.To3dLocateable();
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
        public void DrawSelectingRectangle(Pnt begin, Pnt end, bool isIsometric)
        {
            Buffer.Scenes.ResetSelectingRectangle();
            Vec3 p1 = new Vec3(), p2 = new Vec3(), p3 = new Vec3(), p4 = new Vec3();
            if (isIsometric)
            {
                CppExtern.Scene.ClientPositionToScenePosition(begin, ref p1);
                CppExtern.Scene.ClientPositionToScenePosition(end, ref p3);
                p2 = new Vec3(p3.X, p1.Y, 0);
                p4 = new Vec3(p1.X, p3.Y, 0);
            }
            else
            {
                CppExtern.Scene.ClientPositionToScenePosition(begin, ref p1);
                CppExtern.Scene.ClientPositionToScenePosition(end, ref p3);
                Pnt tmp1 = new Pnt(begin.X, end.Y);
                Pnt tmp2 = new Pnt(end.X, begin.Y);
                CppExtern.Scene.ClientPositionToScenePosition(tmp1, ref p2);
                CppExtern.Scene.ClientPositionToScenePosition(tmp2, ref p4);
            }
            p1 += 20 * _NormTileVec;
            p2 += 20 * _NormTileVec;
            p3 += 20 * _NormTileVec;
            p4 += 20 * _NormTileVec;
            DrawRectangleLine(p1, p2, p3, p4, _white, Buffer.Scenes.RectangleLines);
            CppExtern.Scene.PresentAllObject();
        }
        public void ReleaseDrawingRectangle()
        {
            Buffer.Scenes.ResetSelectingRectangle();
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
            CppExtern.Scene.MoveFocusOnScreen(delta.X * 1.2f, delta.Y * 2f);
            SetMinimapClientPos();
            Refresh();
        }


        private void DrawRectangleLine(Vec3 p1, Vec3 p2, Vec3 p3, Vec3 p4, uint color, List<int> dest)
        {
            dest.Clear();
            dest.Add(CppExtern.ObjectUtils.CreateLineObjectAtScene(p1, p2, color, color));
            dest.Add(CppExtern.ObjectUtils.CreateLineObjectAtScene(p2, p3, color, color));
            dest.Add(CppExtern.ObjectUtils.CreateLineObjectAtScene(p3, p4, color, color));
            dest.Add(CppExtern.ObjectUtils.CreateLineObjectAtScene(p4, p1, color, color));
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
