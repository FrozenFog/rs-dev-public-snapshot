using RelertSharp.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Engine.Api
{
    public static partial class EngineApi
    {
        public static Vec3 ClientPointToCellPos(Point src)
        {
            return EngineMain.ClientPointToCellPos(src);
        }
        public static Vec3 ClientPointToCellPos(Point src, out int subcell)
        {
            Vec3 pos = EngineMain.ClientPointToCellPos(src, out int sub);
            subcell = sub;
            return pos;
        }
        public static void MoveCameraTo(I2dLocateable mapPos, int height)
        {
            EngineMain.MoveTo(mapPos, height);
        }
        public static void MoveCameraTo(I3dLocateable mapPos)
        {
            EngineMain.MoveTo(mapPos);
        }
        /// <summary>
        /// Return true if pos is changed
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="subcell"></param>
        /// <returns></returns>
        public static bool MouseOnTile(Vec3 pos)
        {
            if (pos != Vec3.Zero)
            {
                if (EngineMain.SelectTile(pos))
                {
                    MouseMoveTileMarkRedrawRequest?.Invoke(null, null);
                    return true;
                }
            }
            return false;
        }
        private static readonly object scaleFactorLock = new object();
        public static void ChangeScaleFactor(double delta)
        {
            lock (scaleFactorLock)
            {
                if (ScaleFactor >= 4 && delta > 0) return;
                if (ScaleFactor <= 0.5 && delta < 0) return;
                ScaleFactor += delta;
                ResizeRequest?.Invoke(null, null);
                MinimapClientResizeRequest?.Invoke(null, null);
            }
        }
    }
}
