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
        public static void MoveCameraTo(I2dLocateable mapPos, int height)
        {
            EngineMain.MoveTo(mapPos, height);
        }
        public static void MoveCameraTo(I3dLocateable mapPos)
        {
            EngineMain.MoveTo(mapPos);
        }
    }
}
