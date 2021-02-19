using RelertSharp.Common;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RelertSharp.Engine.Transformation;

namespace RelertSharp.Engine
{
    internal static partial class EngineMain
    {
        private static TileLayer _cellFindingReferance { get { return GlobalVar.CurrentMapDocument.Map.TilesData; } }
        private static Tile previousTile;
        private static List<I2dLocateable> buildingShape = new List<I2dLocateable>();
        private static bool markingBuildingShape = false;



        public static bool MarkBuildingShape(StructureItem bud)
        {
            if (_cellFindingReferance[bud] == null) return false;
            bool result = true;
            if (buildingShape.Count > 0)
            {
                foreach (I2dLocateable pos in buildingShape)
                {
                    if (_cellFindingReferance.HasTileOn(pos)) _cellFindingReferance[pos].UnMarkForSimulating();
                }
                buildingShape.Clear();
            }
            bool waterbound = bud.IsNaval;
            foreach (I2dLocateable pos in new Foundation2D(bud))
            {
                if (!markingBuildingShape)
                {
                    markingBuildingShape = true;
                    UnmarkAllTile();
                }
                buildingShape.Add(pos);
                bool b;
                if (!_cellFindingReferance.HasTileOn(pos)) b = false;
                else b = _cellFindingReferance[pos].MarkForSimulating(waterbound);
                result = result && b;
            }
            return result;
        }
        public static void UnmarkBuildingShape()
        {
            foreach (I2dLocateable pos in buildingShape)
            {
                if (_cellFindingReferance.HasTileOn(pos)) _cellFindingReferance[pos].UnMarkForSimulating();
            }
            buildingShape.Clear();
            markingBuildingShape = false;
        }
        /// <summary>
        /// return Vec3.Zero if it is not a valid coord
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static Vec3 ClientPointToCellPos(Point src)
        {
            Pnt p = Pnt.FromPoint(src);
            Vec3 pos = new Vec3();
            CppExtern.Scene.ClientPositionToScenePosition(p, ref pos);
            pos.Y += 8;
            pos += _NormTileVec * Constant.DrawingEngine.MapMaxHeightDrawing;
            for (int height = 0; height < Constant.DrawingEngine.MapMaxHeightDrawing * 2; height++)
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
        public static Vec3 ClientPointToCellPos(Point src, out int infsubcell)
        {
            Pnt p = Pnt.FromPoint(src);
            Vec3 scenepos = new Vec3();
            CppExtern.Scene.ClientPositionToScenePosition(p, ref scenepos);
            scenepos.Y += 8;
            scenepos += _NormTileVec * Constant.DrawingEngine.MapMaxHeightDrawing;
            int height = 0;
            Vec3 tileCoord = new Vec3();
            for (; height < Constant.DrawingEngine.MapMaxHeightDrawing * 2; height++)
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
        public static Point CellPosToClientPos(I3dLocateable pos)
        {
            Pnt result = new Pnt();
            CppExtern.Scene.ScenePositionToClientPosition(ToVec3Iso(pos), ref result);
            return result.ToPoint();
        }
        public static I3dLocateable GetPreviousLegalTile()
        {
            return previousTile;
        }
        /// <summary>
        /// Return true if pos changed
        /// </summary>
        /// <param name="newpos"></param>
        /// <returns></returns>
        public static bool SelectTile(Vec3 newpos)
        {
            if (previousTile == null || newpos != previousTile)
            {
                if (!markingBuildingShape)
                {
                    if (_cellFindingReferance[newpos.To2dLocateable()] is Tile t)
                    {
                        t.Mark(true);
                        previousTile?.Mark(false);
                    }
                }
                previousTile = _cellFindingReferance[newpos.To2dLocateable()];
                return true;
            }
            return false;
        }
        public static void UnmarkAllTile()
        {
            previousTile?.Mark(false);
        }
        public static void DrawSelectingRectangle(Pnt begin, Pnt end, bool isIsometric)
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
            p1 += Constant.DrawingEngine.SelectRectangleMultiplier * _NormTileVec;
            p2 += Constant.DrawingEngine.SelectRectangleMultiplier * _NormTileVec;
            p3 += Constant.DrawingEngine.SelectRectangleMultiplier * _NormTileVec;
            p4 += Constant.DrawingEngine.SelectRectangleMultiplier * _NormTileVec;
            DrawRectangleLine(p1, p2, p3, p4, _white, Buffer.Scenes.RectangleLines);
            CppExtern.Scene.PresentAllObject();
        }
        public static void ReleaseDrawingRectangle()
        {
            Buffer.Scenes.ResetSelectingRectangle();
        }
        private static void MoveTo(int x, int y, int z)
        {
            Pnt3 pos = new Pnt3(x, y, z);
            CppExtern.Scene.SetFocusOnScene(ToVec3Iso(pos));
            SetMinimapClientPos();
        }
        public static void MoveTo(I3dLocateable pos)
        {
            MoveTo(pos.X, pos.Y, pos.Z);
        }
        public static void MoveTo(I2dLocateable pos)
        {
            MoveTo(pos.X, pos.Y, 0);
        }
        public static void MoveTo(I2dLocateable pos, int height)
        {
            MoveTo(pos.X, pos.Y, height);
        }
        public static void ResetView()
        {
            CppExtern.Scene.ResetSceneView();
        }
        private static void ShiftBy(float dx, float dy)
        {
            CppExtern.Scene.MoveFocusOnScreen(dx, dy);
            SetMinimapClientPos();
        }
        public static void ViewShift(Point previous, Point now)
        {
            Point delta = now.Delta(previous);
            ShiftBy(delta.X * 1.2f, delta.Y * 2f);
        }
        public static void ViewShift(Point delta)
        {
            ShiftBy(delta.X, delta.Y);
        }


        private static void DrawRectangleLine(Vec3 p1, Vec3 p2, Vec3 p3, Vec3 p4, uint color, List<int> dest)
        {
            dest.Clear();
            dest.Add(CppExtern.ObjectUtils.CreateLineObjectAtScene(p1, p2, color, color));
            dest.Add(CppExtern.ObjectUtils.CreateLineObjectAtScene(p2, p3, color, color));
            dest.Add(CppExtern.ObjectUtils.CreateLineObjectAtScene(p3, p4, color, color));
            dest.Add(CppExtern.ObjectUtils.CreateLineObjectAtScene(p4, p1, color, color));
            dest.ForEach(x => CppExtern.ObjectUtils.SetObjectZAdjust(x, Constant.DrawingEngine.ZAdjust.SelectingRectangle));
        }
        private static Vec3 ScenePosToCoord(Vec3 px)
        {
            return new Vec3
                ((int)Math.Floor(px.X / _width),
                (int)Math.Floor(px.Y / _width),
                (int)Math.Floor(px.Z / _height));
        }
        private static int GetSubCellFromCoord(Vec3 scenePos, I2dLocateable tilepos)
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
