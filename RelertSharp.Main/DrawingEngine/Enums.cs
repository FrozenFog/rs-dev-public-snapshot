using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.DrawingEngine
{
    internal enum DrawableType { Shp, Tmp, Vxl }
    internal enum ShpFlatType
    {
        Vertical = 0,
        FlatGround = 1,
        Box1 = 2
    }
    internal enum ShaderType
    {
        Normal = 0,
        Shadow = 1,
        Alpha = 2
    }
}
