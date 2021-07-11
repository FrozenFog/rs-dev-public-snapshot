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
        public static event I3dLocateableHandler MoveCameraRequested;
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
        public static void CallMoveCameraTo(I3dLocateable pos)
        {
            MoveCameraRequested?.Invoke(pos);
            CameraPosition = pos;
        }
        public static void MoveCameraTo(I2dLocateable mapPos, int height)
        {
            EngineMain.MoveTo(mapPos, height);
            CameraPosition = new Pnt3(mapPos, height);
        }
        public static void MoveCameraTo(I3dLocateable mapPos)
        {
            EngineMain.MoveTo(mapPos);
            CameraPosition = mapPos;
        }
        public static void ShiftViewBy(Point delta)
        {
            EngineMain.ViewShift(delta);
        }
        //public static void DrawSelectingRectangle(Point begin, Point end, bool isIsometric)
        //{
        //    EngineMain.DrawSelectingRectangle(Pnt.FromPoint(begin), Pnt.FromPoint(end), isIsometric);
        //}
        //public static void ReleaseSelectingRectangle()
        //{
        //    EngineMain.ReleaseDrawingRectangle();
        //}
        /// <summary>
        /// Return true if pos is changed
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="subcell"></param>
        /// <returns></returns>
        public static bool MouseOnTile(Vec3 pos, bool needIndicate = true)
        {
            if (pos != Vec3.Zero)
            {
                if (needIndicate)
                {
                    if (EngineMain.SelectTile(pos))
                    {
                        return true;
                    }
                }
                else
                {
                    EngineMain.UnmarkPreviousTile();
                    return false;
                }
            }
            return false;
        }
        public static void ChangeScaleFactor(double delta)
        {
            if (!rendering)
            {
                lock (lockRenderer)
                {
                    if (ScaleFactor >= 4 && delta > 0) return;
                    if (ScaleFactor <= 0.5 && delta < 0) return;
                    ScaleFactor += delta;
                    //ResizeRequest?.Invoke(null, null);
                    //MinimapClientResizeRequest?.Invoke(null, null);
                }
            }
        }
        public static void SetScaleFactor(double value)
        {
            if (!rendering)
            {
                lock (lockRenderer)
                {
                    if (value > 5 || value < 0.5) return;
                    ScaleFactor = value;
                    //ResizeRequest?.Invoke(null, null);
                    //MinimapClientResizeRequest?.Invoke(null, null);
                }
            }
        }
        public static void ScaleFactorInvoke()
        {
            if (!rendering && renderEnable)
            {
                ResizeRequested?.Invoke(null, null);
                MinimapClientResizeRequest?.Invoke(null, null);
            }
        }


        public static I3dLocateable CameraPosition { get; private set; } = new Pnt3();
    }
}
