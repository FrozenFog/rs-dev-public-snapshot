using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using RelertSharp.Common;
using RelertSharp.Engine.Api;
using RelertSharp.Wpf.Common;
using RelertSharp.Wpf.MapEngine.Helper;

namespace RelertSharp.Wpf.MapEngine
{
    public partial class MainPanel
    {
        #region Head
        private const int CLICK_INTERVAL = 300;
        private bool rmbMoving = false;
        private bool NeedIndicating
        {
            get { return !rmbMoving && !Selector.IsSelecting && MouseState.State != PanelMouseState.TileSingleBrush; }
        }
        private Point downPos, downPosOrg;
        private MouseButton downBtn;
        private Stopwatch watchClick = new Stopwatch();
        #endregion


        #region Handler Firstpass
        private void HandleMouseLeave(object sender, MouseEventArgs e)
        {
            if (drew)
            {
                bool redraw = MouseLeaved();
                isActivate = false;
                if (redraw) EngineApi.InvokeRedraw();
            }
        }
        private void HandleMouseEnter(object sender, MouseEventArgs e)
        {
            if (drew)
            {
                isActivate = true;
            }
        }

        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            lock (lockMouse)
            {
                if (drew)
                {
                    Point p = e.GetPosition(this);
                    Point pOrg = e.GetPosition(this);
                    GuiUtil.ScaleWpfMousePoint(ref p);
                    bool redraw = MouseMoved(p, pOrg);

                    if (redraw) EngineApi.InvokeRedraw();
                }
            }
        }

        private void HandleMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (drew)
            {
                watchClick.Stop();
                EngineApi.InvokeLock();
                Point p = e.GetPosition(this);
                Point pOrg = e.GetPosition(this);
                GuiUtil.ScaleWpfMousePoint(ref p);
                if (e.ChangedButton == MouseButton.Left) this.LmbUp(p, pOrg);
                else if (e.ChangedButton == MouseButton.Right) this.RmbUp(p, pOrg);
                else if (e.ChangedButton == MouseButton.Middle) this.MmbUp(p, pOrg);
                if (watchClick.ElapsedMilliseconds < CLICK_INTERVAL && RsMath.ChebyshevDistance(downPos, p) < 10)
                {
                    if (downBtn == MouseButton.Left) this.LmbClick(downPos, downPosOrg);
                    else if (downBtn == MouseButton.Right) this.RmbClick(downPos, downPosOrg);
                    else if (downBtn == MouseButton.Middle) this.MmbClick(downPos, downPosOrg);
                }
                EngineApi.InvokeUnlock();
            }
        }

        private void HandleMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (drew)
            {
                watchClick.Restart();
                Point p = e.GetPosition(this);
                Point pOrg = e.GetPosition(this);
                GuiUtil.ScaleWpfMousePoint(ref p);
                downPos = p;
                downPosOrg = pOrg;
                downBtn = e.ChangedButton;
                EngineApi.InvokeLock();
                if (downBtn == MouseButton.Left) this.LmbDown(downPos, downPosOrg);
                else if (downBtn == MouseButton.Right) this.RmbDown(downPos, downPosOrg);
                else if (downBtn == MouseButton.Middle) this.MmbDown(downPos, downPosOrg);
                EngineApi.InvokeUnlock();
            }
        }

        private void HandleMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (drew)
            {
                bool wheelUp = e.Delta > 0;
                EngineApi.InvokeLock();
                switch (MouseState.State)
                {
                    case PanelMouseState.None:
                        if (wheelUp) EngineApi.ChangeScaleFactor(-0.1);
                        else EngineApi.ChangeScaleFactor(0.1);
                        ScaleFactorChanged?.Invoke(null, null);
                        EngineApi.ScaleFactorInvoke();
                        break;
                    case PanelMouseState.TileSingleBrush:
                        if (wheelUp) TilePaintBrush.InvokeBackward();
                        else TilePaintBrush.InvokeForward();
                        break;
                    case PanelMouseState.InteliRampBrush:
                        if (wheelUp) InteliBrush.IncreInteliRamp();
                        else InteliBrush.DecreInteliRamp();
                        TilePaintBrush.LoadTileBrush(InteliBrush.CurrentInteliRamp);
                        EngineApi.InvokeRedraw();
                        break;
                    case PanelMouseState.ObjectRandomBrush:
                        RandomizeBrush.NextRandomObject();
                        EngineApi.InvokeRedraw();
                        break;
                }
                EngineApi.InvokeUnlock();
            }
        }
        #endregion


        #region Rmb
        private void RmbDown(Point point, Point unscaled)
        {
            rmbMoving = true;
            Navigating.BeginRightClickMove(point);
            //switch (mouseState)
            //{
            //    case MouseAction.DEBUG:
            //        rmbMoving = true;
            //        Navigating.BeginRightClickMove(point);
            //        break;
            //}
        }
        private void RmbUp(Point point, Point unscaled)
        {
            if (rmbMoving)
            {
                rmbMoving = false;
                Navigating.EndRightClickMove();
            }
            //switch (mouseState)
            //{
            //    case MouseAction.DEBUG:

            //        break;
            //}
        }
        private void RmbClick(Point point, Point unscaled)
        {
            switch (MouseState.State)
            {
                case PanelMouseState.ObjectBrush:
                    PaintBrush.SuspendBrush();
                    break;
                case PanelMouseState.TileSingleBrush:
                    TilePaintBrush.SuspendBrush();
                    break;
                case PanelMouseState.ObjectPasteBrush:
                    MapClipboard.SuspendClipObjects();
                    break;
                case PanelMouseState.InteliWallBrush:
                    if (InteliBrush.IsAligningWall)
                    {
                        InteliBrush.EndWallAligning();
                        EngineApi.InvokeRedraw();
                        return;
                    }
                    break;
            }
            MouseState.SetState(PanelMouseState.None);
            EngineApi.InvokeRedraw();
        }
        #endregion

        #region Lmb
        private void LmbUp(Point point, Point unscaled)
        {
            Vec3 pos = EngineApi.ClientPointToCellPos(point.GdiPoint(), out int subcell);
            I3dLocateable cell = pos.To3dLocateable();

            switch (MouseState.State)
            {
                case PanelMouseState.None:
                    if (Selector.IsSelecting)
                    {
                        Selector.EndSelecting(GuiUtil.IsAltDown());
                    }
                    else if (Selector.IsMoving) Selector.EndSelectedObjectsMoving();
                    EngineApi.InvokeRedraw();
                    break;
                case PanelMouseState.TileBoxSelecting:
                    if (TileSelector.IsSelecting)
                    {
                        TileSelector.EndSelecting(GuiUtil.IsAltDown());
                    }
                    EngineApi.InvokeRedraw();
                    break;
                case PanelMouseState.TileSingleSelecting:
                    if (TileSelector.IsSingleSelecting)
                    {
                        TileSelector.EndSingleSelect();
                        EngineApi.InvokeRedraw();
                    }
                    break;
                case PanelMouseState.TileFlatting:
                    if (TilePaintBrush.IsRampFlatOn)
                    {
                        TilePaintBrush.EndRampFlat();
                        EngineApi.InvokeRedraw();
                    }
                    break;
                case PanelMouseState.WallBreakdownBrush:
                    InteliBrush.EndBreakdownWall();
                    break;
                case PanelMouseState.WaypointPicker:
                    if (ExtentedFunc.IsDraggingWaypoint) ExtentedFunc.EndWaypointDrag();
                    break;
            }
        }

        private void LmbDown(Point point, Point unscaled)
        {
            Vec3 pos = EngineApi.ClientPointToCellPos(point.GdiPoint(), out int subcell);
            I3dLocateable cell = pos.To3dLocateable();

            switch (MouseState.State)
            {
                case PanelMouseState.None:
                    if (Selector.IsPositionHasSelectedItem(cell, subcell))
                    {
                        Selector.BeginSelectedObjectsMoving(cell);
                    }
                    else
                    {
                        if (GuiUtil.IsKeyUp(Key.LeftShift, Key.RightShift) && GuiUtil.IsKeyUp(Key.LeftAlt, Key.RightAlt))
                        {
                            Selector.UnselectAll();
                            EngineApi.InvokeRedraw();
                        }
                        Selector.BeginSelecting(unscaled, selectorBoxCanvas, cell);
                    }
                    break;
                case PanelMouseState.TileBoxSelecting:
                    if (!GuiUtil.IsShiftDown() && !GuiUtil.IsAltDown())
                    {
                        TileSelector.UnselectAll();
                        EngineApi.InvokeRedraw();
                    }
                    TileSelector.BeginSelecting(unscaled, selectorBoxCanvas, cell);
                    break;
                case PanelMouseState.TileSingleSelecting:
                    if (!GuiUtil.IsShiftDown() && !GuiUtil.IsAltDown())
                    {
                        TileSelector.UnselectAll();
                        EngineApi.InvokeRedraw();
                    }
                    TileSelector.BeginSingleSelect(GuiUtil.IsAltDown());
                    break;
                case PanelMouseState.TileFlatting:
                    TilePaintBrush.BeginRampFlat(cell.Z);
                    break;
                case PanelMouseState.WallBreakdownBrush:
                    InteliBrush.BeginBreakDownWall();
                    break;
                case PanelMouseState.WaypointPicker:
                    ExtentedFunc.BeginWaypointDrag(unscaled, cell);
                    break;
            }
        }

        private void LmbClick(Point point, Point unscaled)
        {
            Vec3 pos = EngineApi.ClientPointToCellPos(point.GdiPoint(), out int subcell);
            I3dLocateable cell = pos.To3dLocateable();
            EngineApi.InvokeLock();
            switch (MouseState.State)
            {
                case PanelMouseState.None:
                    bool addSelect = GuiUtil.IsShiftDown();
                    bool reverseSelect = GuiUtil.IsAltDown();
                    if (!addSelect && !reverseSelect) Selector.UnselectAll();
                    Selector.SelectAt(cell, subcell, reverseSelect);
                    break;
                case PanelMouseState.ObjectBrush:
                    PaintBrush.AddBrushObjectToMap();
                    break;
                case PanelMouseState.ObjectRandomBrush:
                    if (RandomizeBrush.HasItem) PaintBrush.AddBrushObjectToMap();
                    break;
                case PanelMouseState.TileSingleBrush:
                    TilePaintBrush.AddTileToMap();
                    break;
                case PanelMouseState.TileSingleSelecting:
                    TileSelector.SingleSelectAt(cell);
                    break;
                case PanelMouseState.TileLineSelecting:
                    TileSelector.AddLineControlNode(cell);
                    break;
                case PanelMouseState.TileBucketSelecting:
                    TileSelector.BucketAt(cell);
                    break;
                case PanelMouseState.TileBucketFlood:
                    TilePaintBrush.BucketTileAt(cell);
                    break;
                case PanelMouseState.ObjectPasteBrush:
                    MapClipboard.AddClipObjectToMap();
                    break;
                case PanelMouseState.TileSingleRising:
                    TileSelector.RiseTile(cell);
                    break;
                case PanelMouseState.TileSingleSinking:
                    TileSelector.SinkTile(cell);
                    break;
                case PanelMouseState.InteliRampBrush:
                    if (!InteliBrush.CurrentInteliRamp.IsEmptyTile) TilePaintBrush.AddTileToMap();
                    break;
                case PanelMouseState.TilePhasing:
                    TileSelector.PhaseTileAt(cell);
                    break;
                case PanelMouseState.InteliWallBrush:
                    if (InteliBrush.IsAligningWall) InteliBrush.ApplyWallAlign();
                    else InteliBrush.BeginWallAlignAt(cell);
                    break;
                case PanelMouseState.InteliCliffBrush:
                    if (InteliBrush.IsAligningCliff) InteliBrush.ApplyCliffAlign();
                    else InteliBrush.BeginAlignCliffAt(cell);
                    break;
                case PanelMouseState.TiberiumBrush:
                    InteliBrush.ApplyTiberiumFix();
                    break;
            }
            EngineApi.InvokeUnlock();
            EngineApi.InvokeRedraw();
        }
        #endregion

        #region Mmb
        private void MmbUp(Point point, Point unscaled)
        {

        }

        private void MmbDown(Point point, Point unscaled)
        {

        }
        private void MmbClick(Point point, Point unscaled)
        {

        }
        #endregion

        #region Move
        /// <summary>
        /// Handle all mouse move event in main panel
        /// return true if needs redraw, prevent lag from redrawing
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private I3dLocateable prevCell, cellNow;
        private int prevSubcell;
        private bool MouseMoved(Point point, Point unscaled)
        {
            bool redraw = false, noIndicate = false;
            Vec3 pos = EngineApi.ClientPointToCellPos(point.GdiPoint(), out int subcell);
            I3dLocateable cell = pos.To3dLocateable();
            cellNow = cell;
            bool notFound = cell.X == 0 && cell.Y == 0 && cell.Z == 0;
            bool isCellChanged = !RsMath.I3dEqual(cell, prevCell);
            bool isSubcellChanged = subcell != prevSubcell;
            if (rmbMoving)
            {
                Navigating.UpdateDelta(point);
                redraw = false;
                goto nop;
            }
            if (Selector.IsSelecting)
            {
                Selector.UpdateSelectingRectangle(unscaled, cell);
                redraw = false;
                goto nop;
            }
            if (Selector.IsMoving)
            {
                Selector.MoveSelectedObjectsTo(cell, subcell);
                redraw = true;
            }
            if (TileSelector.IsSingleSelecting)
            {
                TileSelector.SingleSelectAt(cell);
                redraw = true;
                goto nop;
            }
            if (TileSelector.IsSelecting)
            {
                TileSelector.UpdateSelectingRectangle(unscaled, cell);
                redraw = false;
                goto nop;
            }
            if (TileSelector.IsPhasing)
            {
                redraw = TileSelector.PhaseTileAt(cell); 
                goto nop;
            }
            if (TilePaintBrush.IsRampFlatOn)
            {
                redraw = TilePaintBrush.RampFlatAt(cell);
                goto nop;
            }
            if (ExtentedFunc.IsDraggingWaypoint)
            {
                ExtentedFunc.WpDragMove(unscaled);
                goto nop;
            }
            switch (MouseState.State)
            {
                case PanelMouseState.ObjectRandomBrush:
                    if (isCellChanged)
                    {
                        RandomizeBrush.NextRandomObject();
                        redraw = true;
                    }
                    PaintBrush.MoveBrushObjectTo(cell, subcell);
                    break;
                case PanelMouseState.ObjectBrush:
                    redraw = PaintBrush.MoveBrushObjectTo(cell, subcell);
                    break;
                case PanelMouseState.ObjectPasteBrush:
                    redraw = MapClipboard.MoveClipObjectTo(cell);
                    break;
                case PanelMouseState.TileSingleBrush:
                    if (!notFound)
                    {
                        redraw = TilePaintBrush.MoveTileBrushTo(cell);
                    }
                    break;
                case PanelMouseState.InteliRampBrush:
                    if (!notFound)
                    {
                        bool posChanged = InteliBrush.InteliRampAt(cell);
                        if (posChanged)
                        {
                            TilePaintBrush.LoadTileBrush(InteliBrush.CurrentInteliRamp);
                        }
                        redraw = TilePaintBrush.MoveTileBrushTo(cell);
                    }
                    break;
                case PanelMouseState.InteliCliffBrush:
                    if (InteliBrush.IsAligningCliff)
                    {
                        redraw = InteliBrush.AlignCliffBetween(cell);
                    }
                    break;
                case PanelMouseState.InteliWallBrush:
                    if (InteliBrush.IsAligningWall && InteliBrush.CurrentOverlayIsWall)
                    {
                        redraw = InteliBrush.AlignWallBetween(cell, InteliBrush.CurrentOverlayIndex);
                    }
                    break;
                case PanelMouseState.WallBreakdownBrush:
                    if (InteliBrush.IsBreakdownWall)
                    {
                        InteliBrush.BreakdownwallAt(cell);
                        redraw = isCellChanged;
                    }
                    break;
                case PanelMouseState.TiberiumBrush:
                    InteliBrush.FixTiberiumAt(cell);
                    redraw = isCellChanged;
                    break;
            }
            if (!noIndicate)
            {
                if (isCellChanged || isSubcellChanged)
                {
                    EngineApi.MouseOnTile(pos, NeedIndicating);
                    MousePosChanged?.Invoke(pos.To3dLocateable(), subcell);
                    redraw = true;
                }
            }
            nop:
            prevSubcell = subcell;
            prevCell = cell;
            return redraw;
        }
        private bool MouseLeaved()
        {
            switch (MouseState.State)
            {
                case PanelMouseState.WaypointPicker:
                    if (ExtentedFunc.IsDraggingWaypoint) ExtentedFunc.EndWaypointDrag();
                    break;
            }

            return false;
        }
        #endregion
    }
}
