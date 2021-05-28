using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using RelertSharp.Common;
using RelertSharp.Engine.Api;
using RelertSharp.Wpf.MapEngine.Helper;

namespace RelertSharp.Wpf.MapEngine
{
    public partial class MainPanel
    {
        #region Head
        private enum MouseAction
        {
            None = 0,
            DEBUG = 65535
        }
        private MouseAction mouseState = MouseAction.DEBUG;
        private bool rmbMoving = false;
        private bool selecting = false;
        private bool NeedIndicating
        {
            get { return !rmbMoving && !selecting; }
        }
        #endregion


        #region Rmb
        private void RmbDown(Point point, Point unscaled)
        {
            switch (mouseState)
            {
                case MouseAction.DEBUG:
                    rmbMoving = true;
                    Navigating.BeginRightClickMove(point);
                    break;
            }
        }
        private void RmbUp(Point point, Point unscaled)
        {
            switch (mouseState)
            {
                case MouseAction.DEBUG:
                    rmbMoving = false;
                    Navigating.EndRightClickMove();
                    break;
            }
        }
        #endregion

        #region Lmb
        private void LmbUp(Point point, Point unscaled)
        {
            switch (mouseState)
            {
                case MouseAction.DEBUG:
                    selecting = false;
                    Selector.EndSelecting();
                    //SuspendMouseHandlerFor(susMsSelect);
                    break;
            }
        }

        private void LmbDown(Point point, Point unscaled)
        {
            switch (mouseState)
            {
                case MouseAction.DEBUG:
                    selecting = true;
                    Selector.BeginSelecting(unscaled, false, graphicTop);
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
