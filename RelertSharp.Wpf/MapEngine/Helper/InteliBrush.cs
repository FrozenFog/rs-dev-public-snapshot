using RelertSharp.Algorithm;
using RelertSharp.Common;
using RelertSharp.Engine;
using RelertSharp.Engine.Api;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure;
using RelertSharp.Terraformer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.MapEngine.Helper
{
    internal static class InteliBrush
    {
        private static int rampSelector = 0;
        private static List<Tile> inteliRampResult = new List<Tile>();
        private static I2dLocateable prevPos = new Pnt();
        private static DdaLineDrawing dda;

       
        static InteliBrush()
        {
            dda = new DdaLineDrawing()
            {
                FixCorner = true
            };
            MouseState.MouseStateChanged += HandleStateChanged;
        }

        private static void HandleStateChanged()
        {
            if (MouseState.PrevState == PanelMouseState.InteliWallBrush) EndWallAligning();
            if (MouseState.PrevState == PanelMouseState.InteliCliffBrush) EndCliffAlign();
        }


        #region Api
        #region Ramp
        /// <summary>
        /// Tile is not drawn
        /// return true if pos is changed
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static bool InteliRampAt(I2dLocateable cell)
        {
            bool changed = !RsMath.I2dEqual(cell, prevPos);
            if (changed)
            {
                inteliRampResult = RampCalc.LookupRampAt(cell);
                rampSelector = RsMath.TrimTo(rampSelector, 0, inteliRampResult.Count - 1);
                if (inteliRampResult.Count == 0) CurrentInteliRamp = Tile.EmptyTile;
                else CurrentInteliRamp = inteliRampResult[rampSelector];
            }
            prevPos = cell;
            return changed;
        }
        public static void IncreInteliRamp()
        {
            rampSelector++; rampSelector %= inteliRampResult.Count;
            CurrentInteliRamp = inteliRampResult[rampSelector];
        }
        public static void DecreInteliRamp()
        {
            rampSelector--;
            if (rampSelector < 0) rampSelector = inteliRampResult.Count - 1;
            CurrentInteliRamp = inteliRampResult[rampSelector];
        }
        #endregion
        #region Wall
        private static I2dLocateable wallBegin, wallEnd;
        private static List<OverlayUnit> wallLine = new List<OverlayUnit>();
        public static void BeginWallAlignAt(I2dLocateable cell)
        {
            wallBegin = new Pnt(cell);
            wallEnd = wallBegin;
        }
        public static void ApplyWallAlign()
        {
            using (var _ = new EngineRegion())
            {
                List<OverlayUnit> targets = new List<OverlayUnit>();
                foreach (OverlayUnit o in wallLine)
                {
                    OverlayUnit dest = new OverlayUnit(o);
                    MapApi.AddObject(dest);
                    targets.Add(dest);
                }
                foreach (OverlayUnit o in targets)
                {
                    WallCalc.FixWallAt(o, o.OverlayIndex);
                    EngineApi.DrawObject(o);
                }
                UndoRedoHub.PushCommand(targets);
            }
            BeginWallAlignAt(wallEnd);
        }
        public static bool AlignWallBetween(I2dLocateable endCell, byte wallIndex)
        {
            if (RsMath.I2dEmpty(endCell)) return false;
            bool moved = !RsMath.I2dEqual(wallEnd, endCell);
            if (moved)
            {
                using (var _ = new EngineRegion())
                {
                    wallEnd = new Pnt(endCell);
                    dda.SetControlNode(wallBegin, wallEnd);
                    PaintBrush.SuspendArrayBrush();
                    wallLine.Clear();
                    foreach (I2dLocateable cell in dda.GetLineCells())
                    {
                        wallLine.Add(new OverlayUnit(wallIndex, 0)
                        {
                            X = cell.X,
                            Y = cell.Y
                        });
                    }
                    WallCalc.FixWallIn(wallLine);
                    PaintBrush.LoadObjectToArrayBrush(wallLine);
                }
                return true;
            }
            return false;
        }
        public static void EndWallAligning()
        {
            using (var _ = new EngineRegion())
            {
                wallBegin = null;
                wallEnd = null;
                PaintBrush.SuspendArrayBrush();
            }
        }
        #endregion
        #region Resource

        #endregion
        #region Cliff
        private static I3dLocateable cliffBegin, cliffCurrent, alignEnd;
        private static int cliffHeightLock;
        public static void BeginAlignCliffAt(I3dLocateable cell)
        {
            cliffBegin = new Pnt3(cell);
            alignEnd = cliffBegin;
            EngineApi.SetHoverNavigation(true, cell.Z);
            cliffHeightLock = cell.Z;
        }
        public static bool AlignCliffBetween(I3dLocateable dest)
        {
            if (RsMath.I3dEmpty(dest)) return false;
            bool moved = !RsMath.I2dEqual(cliffCurrent, dest);
            if (moved)
            {
                cliffCurrent = new Pnt3(dest, cliffHeightLock);
                using (var _ = new EngineRegion())
                {
                    var list = CliffManager.AlignCliffBetween(cliffBegin, cliffCurrent, CliffAlignType, out I2dLocateable actualEndCell);
                    alignEnd = new Pnt3(actualEndCell, cliffHeightLock);
                    TilePaintBrush.SuspendBrush();
                    TilePaintBrush.LoadTileBrush(cliffBegin, list);
                    TilePaintBrush.MoveTileBrushTo(cliffBegin, cliffHeightLock);
                }
            }
            return moved;
        }
        public static void ApplyCliffAlign()
        {
            using (var _ = new EngineRegion())
            {
                TilePaintBrush.AddTileToMap();
                TilePaintBrush.SuspendBrush();
                CliffManager.IncreCliffAlignLine(alignEnd);
            }
            cliffBegin = alignEnd;
            cliffCurrent = null;
        }
        public static void EndCliffAlign()
        {
            using (var _ = new EngineRegion())
            {
                cliffBegin = cliffCurrent = null;
                EngineApi.SetHoverNavigation(false);
                TilePaintBrush.SuspendBrush();
                CliffManager.EndCliffAlign();
            }
        }
        #endregion
        #endregion


        #region Private
        #endregion


        #region Calls
        private static Tile _currentRamp = Tile.EmptyTile;
        public static Tile CurrentInteliRamp
        {
            get { return _currentRamp; }
            set
            {
                _currentRamp?.Dispose();
                _currentRamp = value;
            }
        }
        public static byte CurrentOverlayIndex { get; set; }
        public static bool CurrentOverlayIsWall
        {
            get
            {
                return GlobalVar.GlobalRules.IsWallOverlay(CurrentOverlayIndex);
            }
        }
        public static bool IsAligningCliff { get { return cliffBegin != null; } }
        public static bool IsAligningWall { get { return wallBegin != null; } }
        public static string CliffAlignType { get; set; } = "pCliff";
        #endregion
    }
}
