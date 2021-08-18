﻿using RelertSharp.Algorithm;
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
        private static LinkedList<I2dLocateable> wallNodes = new LinkedList<I2dLocateable>();

       
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
            if (MouseState.PrevState == PanelMouseState.InteliWallBrush) EndWallDrawing();
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
        public static void BeginWallDrawing()
        {
            if (!IsDrawingWall)
            {
                wallNodes.Clear();
                IsDrawingWall = true;
            }
        }
        public static void AddNodeAt(I2dLocateable cell)
        {
            if (RsMath.I2dValid(cell) && IsDrawingWall)
            {
                wallNodes.AddLast(cell);
            }
        }
        public static void LayWall(int damage)
        {
            if (wallNodes.Count > 0)
            {
                HashSet<I2dLocateable> path = new HashSet<I2dLocateable>();
                I2dLocateable prev = wallNodes.First.Value;
                var node = wallNodes.First;
                while (node.Next != null)
                {
                    dda.SetControlNode(prev, node.Next.Value);
                    node = node.Next;
                    prev = node.Value;
                    foreach (I2dLocateable cell in dda.GetLineCells()) path.Add(cell);
                }
                using (var _ = new EngineRegion())
                {
                    // lay wall
                    foreach (I2dLocateable cell in path)
                    {
                        OverlayUnit o = new OverlayUnit(CurrentOverlayIndex, 0)
                        {
                            X = cell.X,
                            Y = cell.Y
                        };
                        MapApi.AddObject(o);
                    }
                    // fix wall
                    foreach (I2dLocateable cell in path)
                    {
                        OverlayUnit o = WallCalc.FixWallAt(cell, CurrentOverlayIndex);
                        MapApi.AddObject(o);
                        EngineApi.DrawObject(o);
                    }
                }
            }
        }
        public static void EndWallDrawing()
        {
            IsDrawingWall = false;
        }
        #endregion
        #region Resource

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
        public static bool IsDrawingWall { get; private set; }
        public static byte CurrentOverlayIndex { get; set; }
        public static bool CurrentOverlayIsWall
        {
            get
            {
                return GlobalVar.GlobalRules.IsWallOverlay(CurrentOverlayIndex);
            }
        }
        #endregion
    }
}
