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
            get { return !rmbMoving && !Selector.IsSelecting; }
        }
        private Point downPos, downPosOrg;
        private MouseButton downBtn;
        private Stopwatch watchClick = new Stopwatch();
        #endregion


        #region Handler Firstpass

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
                    case PanelMouseState.TileBrush:
                        if (wheelUp) TilePaintBrush.InvokeBackward();
                        else TilePaintBrush.InvokeForward();
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
                case PanelMouseState.TileBrush:
                    TilePaintBrush.SuspendBrush();
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
                        Selector.EndSelecting(GuiUtil.IsKeyDown(Key.LeftAlt, Key.RightAlt));
                    }
                    else if (Selector.IsMoving) Selector.EndSelectedObjectsMoving();
                    EngineApi.InvokeRedraw();
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
                        Selector.BeginSelecting(unscaled, graphicTop, cell);
                    }
                    break;
            }
        }

        private void LmbClick(Point point, Point unscaled)
        {
            Vec3 pos = EngineApi.ClientPointToCellPos(point.GdiPoint(), out int subcell);
            I3dLocateable cell = pos.To3dLocateable();

            switch (MouseState.State)
            {
                case PanelMouseState.None:
                    bool addSelect = GuiUtil.IsKeyDown(Key.LeftShift, Key.RightShift);
                    bool reverseSelect = GuiUtil.IsKeyDown(Key.LeftAlt, Key.RightAlt);
                    if (!addSelect && !reverseSelect) Selector.UnselectAll();
                    Selector.SelectAt(cell, subcell, reverseSelect);
                    EngineApi.InvokeRedraw();
                    break;
                case PanelMouseState.ObjectBrush:
                    PaintBrush.AddBrushObjectToMap();
                    EngineApi.InvokeRedraw();
                    break;
                case PanelMouseState.TileBrush:
                    TilePaintBrush.AddTileToMap();
                    EngineApi.InvokeRedraw();
                    break;
            }
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
        private bool MouseMoved(Point point, Point unscaled)
        {
            bool redraw = false, noIndicate = false;
            Vec3 pos = EngineApi.ClientPointToCellPos(point.GdiPoint(), out int subcell);
            I3dLocateable cell = pos.To3dLocateable();
            bool notFound = cell.X == 0 && cell.Y == 0 && cell.Z == 0;
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
            switch (MouseState.State)
            {
                case PanelMouseState.ObjectBrush:
                    redraw = PaintBrush.MoveBrushObjectTo(cell, subcell);
                    break;
                case PanelMouseState.TileBrush:
                    if (!notFound)
                    {
                        redraw = TilePaintBrush.MoveTileBrushTo(cell);
                    }
                    break;
            }
            if (!noIndicate)
            {
                if (EngineApi.MouseOnTile(pos, NeedIndicating))
                {
                    MousePosChanged?.Invoke(pos.To3dLocateable());
                    redraw = true;
                }
            }
            nop:
            return redraw;
        }
        #endregion
    }
}
