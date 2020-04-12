using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RelertSharp.Common
{
    public struct VFileInfo
    {
        public IntPtr ptr;
        public uint size;
    }

    public struct Vec4
    {
        public float X;
        public float Y;
        public float Z;
        public float V;


        public Vec4(float r, float g, float b, float a)
        {
            X = r; Y = g; Z = b; V = a;
        }
        public static Vec4 Unit3(float num)
        {
            return new Vec4(num, num, num, 1);
        }
        public static Vec4 Unit4(float num)
        {
            return new Vec4(num, num, num, num);
        }
        public static Vec4 Zero
        {
            get { return new Vec4(0, 0, 0, 0); }
        }
        public static Vec4 TileIndicator
        {
            get { return new Vec4(1, 0, 0, 1); }
        }
        public static Vec4 TileExIndi
        {
            get { return new Vec4(0, 1, 0, 1); }
        }
        public static Vec4 Transparency
        {
            get { return new Vec4(0.5f, 1, 0.5f, 0.6f); }
        }
        public static Vec4 DeTransparency
        {
            get { return new Vec4(1, 1, 1, 1); }
        }
        public static Vec4 operator +(Vec4 a, Vec4 b)
        {
            return new Vec4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.V + b.V);
        }
        public static Vec4 operator -(Vec4 a, Vec4 b)
        {
            return new Vec4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.V - b.V);
        }
        public static Vec4 operator*(Vec4 a, Vec4 b)
        {
            return new Vec4(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.V * b.V);
        }
    }
    public struct Vec3
    {
        public float X;
        public float Y;
        public float Z;


        public Vec3(int x, int y, int z)
        {
            X = x; Y = y; Z = z;
        }
        public Vec3(double x, double y, double z)
        {
            X = (float)x; Y = (float)y; Z = (float)z;
        }
        public int ToCoord()
        {
            return Utils.Misc.CoordInt(X, Y);
        }
        public Vec3 Rise()
        {
            return new Vec3(X, Y, Z + 0.1f);
        }
        public static Vec3 FromXYZ(I3dLocateable src)
        {
            return new Vec3() { X = src.X, Y = src.Y, Z = src.Z };
        }
        public static Vec3 Zero
        {
            get { return new Vec3() { X = 0, Y = 0, Z = 0 }; }
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
        public static bool operator !=(Vec3 a, Vec3 b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z;
        }
    }
    public struct Pnt
    {
        public int X;
        public int Y;

        public static Pnt FromPoint(Point src)
        {
            return new Pnt() { X = src.X, Y = src.Y };
        }
    }
}
