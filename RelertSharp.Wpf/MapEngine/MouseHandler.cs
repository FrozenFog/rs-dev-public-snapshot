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
        private const int CLICK_INTERVAL = 50;
        private bool rmbMoving = false;
        private bool selecting = false;
        private bool NeedIndicating
        {
            get { return !rmbMoving && !selecting; }
        }
        private bool isHold = false;
        private DelayedAction holdClickAction;
        private Point downPos, downPosOrg;
        private MouseButton downBtn;
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
                holdClickAction.Stop();
                EngineApi.InvokeLock();
                if (isHold)
                {
                    Point p = e.GetPosition(this);
                    Point pOrg = e.GetPosition(this);
                    GuiUtil.ScaleWpfMousePoint(ref p);
                    if (e.ChangedButton == MouseButton.Left) this.LmbUp(p, pOrg);
                    else if (e.ChangedButton == MouseButton.Right) this.RmbUp(p, pOrg);
                    else if (e.ChangedButton == MouseButton.Middle) this.MmbUp(p, pOrg);
                    isHold = false;
                }
                else
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
                Point p = e.GetPosition(this);
                Point pOrg = e.GetPosition(this);
                GuiUtil.ScaleWpfMousePoint(ref p);
                downPos = p;
                downPosOrg = pOrg;
                downBtn = e.ChangedButton;
                holdClickAction.Restart();
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

        private void MouseHoldToClick()
        {
            isHold = true;
            EngineApi.InvokeLock();
            if (downBtn == MouseButton.Left) this.LmbDown(downPos, downPosOrg);
            else if (downBtn == MouseButton.Right) this.RmbDown(downPos, downPosOrg);
            else if (downBtn == MouseButton.Middle) this.MmbDown(downPos, downPosOrg);
            EngineApi.InvokeUnlock();
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
            switch (MouseState.State)
            {
                case PanelMouseState.None:
                    selecting = false;
                    Selector.EndSelecting();
                    //SuspendMouseHandlerFor(susMsSelect);
                    break;
            }
        }

        private void LmbDown(Point point, Point unscaled)
        {
            switch (MouseState.State)
            {
                case PanelMouseState.None:
                    selecting = true;
                    Selector.BeginSelecting(unscaled, false, graphicTop);
                    break;
            }
        }

        private void LmbClick(Point point, Point unscaled)
        {
            switch (MouseState.State)
            {
                case PanelMouseState.ObjectBrush:
                    PaintBrush.AddBrushObjectToMap();
                    break;
                case PanelMouseState.TileBrush:
                    TilePaintBrush.AddTileToMap();
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
            bool redraw = false;
            Vec3 pos = EngineApi.ClientPointToCellPos(point.GdiPoint(), out int subcell);
            I3dLocateable i3dpos = pos.To3dLocateable();
            bool notFound = i3dpos.X == 0 && i3dpos.Y == 0 && i3dpos.Z == 0;
            if (rmbMoving)
            {
                Navigating.UpdateDelta(point);
                redraw = false;
            }
            if (selecting)
            {
                Selector.UpdateSelectingRectangle(unscaled);
                redraw = false;
            }
            if (!rmbMoving && !selecting)
            {
                switch (MouseState.State)
                {
                    case PanelMouseState.ObjectBrush:
                        redraw = PaintBrush.MoveBrushObjectTo(i3dpos, subcell);
                        break;
                    case PanelMouseState.TileBrush:
                        if (!notFound)
                        {
                            redraw = TilePaintBrush.MoveTileBrushTo(i3dpos);
                        }
                        break;
                }
            }
            if (EngineApi.MouseOnTile(pos, NeedIndicating))
            {
                MousePosChanged?.Invoke(pos.To3dLocateable());
                redraw = true;
            }
            return redraw;
        }
        #endregion
    }
}
