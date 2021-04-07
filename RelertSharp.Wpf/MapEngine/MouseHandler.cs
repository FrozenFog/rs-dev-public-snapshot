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
            None = 0
        }
        private MouseAction mouseState = MouseAction.None;
        private bool rmbMoving = false;
        private bool NeedIndicating
        {
            get { return !rmbMoving; }
        }
        #endregion


        #region Rmb
        private void RmbDown(Point point)
        {
            switch (mouseState)
            {
                case MouseAction.None:
                    rmbMoving = true;
                    Navigating.BeginRightClickMove(point);
                    break;
            }
        }
        private void RmbUp(Point point)
        {
            switch (mouseState)
            {
                case MouseAction.None:
                    rmbMoving = false;
                    Navigating.EndRightClickMove();
                    break;
            }
        }
        #endregion

        #region Lmb
        private void LmbUp(Point point)
        {

        }

        private void LmbDown(Point point)
        {

        }
        #endregion

        #region Mmb
        private void MmbUp(Point point)
        {

        }

        private void MmbDown(Point point)
        {

        }
        #endregion

        #region Move
        /// <summary>
        /// Handle all mouse move event in main panel
        /// </summary>
        /// <param name="point"></param>
        private void MouseMoved(Point point)
        {
            bool redraw = false;
            Vec3 pos = EngineApi.ClientPointToCellPos(point.GdiPoint(), out int subcell);
            if (rmbMoving)
            {
                Navigating.UpdateDelta(point);
                redraw = false;
            }
            if (EngineApi.MouseOnTile(pos, NeedIndicating))
            {
                MousePosChanged?.Invoke(this, pos.To3dLocateable());
                redraw = true;
            }

            if (redraw)
            {
                EngineApi.InvokeRedraw();
            }
        }
        #endregion
    }
}
