using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Common
{
    public struct Vec3
    {
        public float X;
        public float Y;
        public float Z;
        private const float _15SQ2 = 21.213203f;
        private const float _30SQ2 = 42.426406f;
        private const float _10SQ3 = 17.320508f;


        public Vec3(int x, int y, int z)
        {
            X = x; Y = y; Z = z;
        }
        public Vec3(double x, double y, double z)
        {
            X = (float)x; Y = (float)y; Z = (float)z;
        }
        public Vec3(I3dLocateable src)
        {
            X = src.X;Y = src.Y;Z = src.Z;
        }
        public int ToCoord()
        {
            return Utils.Misc.CoordInt(X, Y);
        }
        public Vec3 Rise()
        {
            return new Vec3(X, Y, Z + 0.05f);
        }
        public Vec3 MoveX(float delta)
        {
            return new Vec3(X + delta, Y, Z);
        }
        public static Vec3 FromXYZ(I3dLocateable src)
        {
            return new Vec3(src.X, src.Y, src.Z);
        }
        public static Vec3 FromXY(I2dLocateable src)
        {
            return new Vec3(src.X, src.Y, 0);
        }
        public static Vec3 One
        {
            get { return new Vec3(1, 1, 1); }
        }
        public static Vec3 Zero
        {
            get { return new Vec3(0, 0, 0); }
        }
        public static Vec3 DefaultBox
        {
            get { return new Vec3(1, 1, 5); }
        }
        public static Vec3 ToVec3Iso(I3dLocateable src)
        {
            return ToVec3Iso(src.X, src.Y, src.Z);
        }
        public static Vec3 ToVec3Iso(Vec3 src)
        {
            return ToVec3Iso(src.X, src.Y, src.Z);
        }
        public static Vec3 ToVec3Iso(I3dLocateable src, int subcell)
        {
            float x = src.X + 0.25f, y = src.Y + 0.25f, z = src.Z;
            if (subcell == 2) x -= 0.5f;
            else if (subcell == 3) y -= 0.5f;
            return ToVec3Iso(x, y, z);
        }
        public static Vec3 ToVec3Iso(float x, float y, float z)
        {
            return new Vec3(x * _30SQ2, y * _30SQ2, z * _10SQ3);
        }
        public static Vec3 ToVec3Zero(I3dLocateable src)
        {
            return ToVec3Zero(src.X, src.Y, src.Z);
        }
        public static Vec3 ToVec3Zero(float x, float y, float z)
        {
            return new Vec3(x * _30SQ2 - _15SQ2, y * _30SQ2 - _15SQ2, z * _10SQ3);
        }
        #region operator
        public static Vec3 operator-(Vec3 a)
        {
            return Zero - a;
        }
        public static Vec3 operator +(Vec3 a, int b)
        {
            return new Vec3() { X = a.X + b, Y = a.Y + b, Z = a.Z + b };
        }
        public static Vec3 operator +(int b, Vec3 a)
        {
            return a + b;
        }
        public static Vec3 operator +(Vec3 a, Vec3 b)
        {
            return new Vec3() { X = a.X + b.X, Y = a.Y + b.Y, Z = a.Z + b.Z };
        }
        public static Vec3 operator -(Vec3 a, Vec3 b)
        {
            return new Vec3() { X = a.X - b.X, Y = a.Y - b.Y, Z = a.Z - b.Z };
        }
        public static Vec3 operator *(Vec3 a, int b)
        {
            return new Vec3() { X = a.X * b, Y = a.Y * b, Z = a.Z * b };
        }
        public static Vec3 operator *(int a, Vec3 b)
        {
            return b * a;
        }
        public static Vec3 operator /(Vec3 a, float b)
        {
            return new Vec3(a.X / b, a.Y / b, a.Z / b);
        }
        public static Vec3 operator +(Vec3 a, Pnt b)
        {
            float sq3 = (float)Math.Sqrt(3);
            float sq2 = (float)Math.Sqrt(2);
            //float dz = b.Y * 2 / sq3;
            float dx = sq2 * b.Y + b.X / sq2;
            float dy = sq2 * b.Y - b.X / sq2;
            return new Vec3() { X = a.X + dx, Y = a.Y + dy, Z = a.Z /*- dz*/ };
        }
        public static Vec3 operator +(Pnt b, Vec3 a)
        {
            return a + b;
        }
        public static bool operator ==(Vec3 a, Vec3 b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }
        public static bool operator ==(Vec3 a, I3dLocateable b)
        {
            return (int)a.X == b.X && (int)a.Y == b.Y && (int)a.Z == b.Z;
        }
        public static bool operator !=(Vec3 a, Vec3 b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z;
        }
        public static bool operator !=(Vec3 a, I3dLocateable b)
        {
            return (int)a.X != b.X || (int)a.Y != b.Y || (int)a.Z != b.Z;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public Pnt To2dLocateable()
        {
            return new Pnt((int)X, (int)Y);
        }
        public I3dLocateable To3dLocateable()
        {
            return new Pnt3((int)X, (int)Y, (int)Z);
        }
        #endregion
    }
}
