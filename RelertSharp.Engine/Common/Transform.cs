using RelertSharp.Common;
using RelertSharp.Engine.MapObjects;
using System;
using static RelertSharp.Engine.EngineMain;

namespace RelertSharp.Engine
{
    internal static class Transformation
    {
        internal static Vec3 MoveVertical(Vec3 src, int px)
        {
            src.X += px;
            src.Y += px;
            return src;
        }
        internal static Vec3 MoveHorizontal(Vec3 src, int px)
        {
            int offset = (int)(px / Math.Sqrt(3));
            src.X += offset;
            src.Y -= offset;
            return src;
        }
        internal static Vec3 ToVec3Iso(MapInfantry inf, int subcell)
        {
            float x = inf.X + 0.25f, y = inf.Y + 0.25f, z = inf.Z;
            if (subcell == 2) x -= 0.5f;
            if (subcell == 3) y -= 0.5f;
            return ToVec3Iso(x, y, z);
        }
        internal static Vec3 ToVec3Iso(I3dLocateable d3)
        {
            return ToVec3Iso(d3.X, d3.Y, d3.Z);
        }
        internal static Vec3 ToVec3Zero(I3dLocateable d3)
        {
            return ToVec3Zero(d3.X, d3.Y, d3.Z);
        }
        internal static Vec3 ToVec3Iso(I2dLocateable d2, int height = 0)
        {
            return ToVec3Iso(d2.X, d2.Y, height);
        }
        internal static Vec3 ToVec3Zero(I2dLocateable d2, int height = 0)
        {
            return ToVec3Zero(d2.X, d2.Y, height);
        }
        internal static Vec3 ToVec3Iso(float x, float y, float z)
        {
            return new Vec3() { X = x * _30SQ2, Y = y * _30SQ2, Z = z * _10SQ3 };
        }
        internal static Vec3 ToVec3Iso(Vec3 zero)
        {
            return new Vec3() { X = zero.X + _15SQ2, Y = zero.Y + _15SQ2, Z = zero.Z };
        }
        internal static Vec3 ToVec3Zero(Vec3 iso)
        {
            return new Vec3() { X = iso.X - _15SQ2, Y = iso.Y - _15SQ2, Z = iso.Z };
        }
        internal static Vec3 ToVec3Zero(float x, float y, float z)
        {
            return new Vec3() { X = x * _30SQ2 - _15SQ2, Y = y * _30SQ2 - _15SQ2, Z = z * _10SQ3 };
        }
        internal static Vec3 OffsetTo(float multiply)
        {
            return _generalOffset * multiply;
        }
    }
}
