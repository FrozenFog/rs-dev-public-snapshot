using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using RelertSharp.FileSystem;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Points;
using RelertSharp.MapStructure.Objects;
using RelertSharp.DrawingEngine.Drawables;
using RelertSharp.DrawingEngine.Presenting;
using RelertSharp.Common;
using static RelertSharp.Utils.Misc;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.DrawingEngine
{
    public partial class Engine
    {
        private Vec3 ToVec3Iso(PresentInfantry inf, int subcell)
        {
            float x = inf.X + 0.25f, y = inf.Y + 0.25f, z = inf.Z;
            if (subcell == 2) x -= 0.5f;
            if (subcell == 3) y -= 0.5f;
            return ToVec3Iso(x, y, z);
        }
        private Vec3 ToVec3Iso(I3dLocateable d3)
        {
            return ToVec3Iso(d3.X, d3.Y, d3.Z);
        }
        private Vec3 ToVec3Zero(I3dLocateable d3)
        {
            return ToVec3Zero(d3.X, d3.Y, d3.Z);
        }
        private Vec3 ToVec3Iso(I2dLocateable d2, int height = 0)
        {
            return ToVec3Iso(d2.X, d2.Y, height);
        }
        private Vec3 ToVec3Zero(I2dLocateable d2, int height = 0)
        {
            return ToVec3Zero(d2.X, d2.Y, height);
        }
        private Vec3 ToVec3Iso(float x, float y, float z)
        {
            return new Vec3() { X = x * _30SQ2, Y = y * _30SQ2, Z = z * _10SQ3 };
        }
        private Vec3 ToVec3Iso(Vec3 zero)
        {
            return new Vec3() { X = zero.X + _15SQ2, Y = zero.Y + _15SQ2, Z = zero.Z };
        }
        private Vec3 ToVec3Zero(Vec3 iso)
        {
            return new Vec3() { X = iso.X - _15SQ2, Y = iso.Y - _15SQ2, Z = iso.Z };
        }
        private Vec3 ToVec3Zero(int x, int y, int z)
        {
            return new Vec3() { X = x * _30SQ2 - _15SQ2, Y = y * _30SQ2 - _15SQ2, Z = z * _10SQ3 };
        }
    }
}
