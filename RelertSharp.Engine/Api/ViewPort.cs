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
    }
}
