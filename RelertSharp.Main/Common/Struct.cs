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

        /// <summary>
        /// RGBA if using as color vector
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        public Vec4(float r, float g, float b, float a)
        {
            X = r; Y = g; Z = b; V = a;
        }
        public Vec4 ToNormalize3(out float scale)
        {
            float max = Math.Max(Math.Max(X, Y), Z);
            float min = Math.Min(Math.Min(X, Y), Z);
            float delta = max - min;
            scale = 1 / delta;
            return new Vec4(X * scale, Y * scale, Z * scale, V);
        }
        public static Vec4 Unit3(float num)
        {
            return new Vec4(num, num, num, 1);
        }
        public static Vec4 Unit4(float num)
        {
            return new Vec4(num, num, num, num);
        }
        public static Vec4 One
        {
            get { return new Vec4(1, 1, 1, 1); }
        }
        public static Vec4 Zero
        {
            get { return new Vec4(0, 0, 0, 0); }
        }
        public static Vec4 Selector
        {
            get { return new Vec4(0.5f, 0.5f, 1, 1); }
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


        #region operator
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
        public static Vec4 operator*(Vec4 a, float b)
        {
            return new Vec4(a.X * b, a.Y * b, a.Z * b, a.V * b);
        }
        public static Vec4 operator*(float a ,Vec4 b)
        {
            return b * a;
        }
        public static Vec4 operator/(Vec4 a, float b)
        {
            return new Vec4(a.X / b, a.Y / b, a.Z / b, a.V / b);
        }
        public static Vec4 operator/(Vec4 a, Vec4 b)
        {
            return new Vec4(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.V / b.V);
        }
        public static bool operator==(Vec4 a, Vec4 b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.V == b.V;
        }
        public static bool operator!=(Vec4 a, Vec4 b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z || a.V != b.V;
        }
        public static Vec4 operator!(Vec4 src)
        {
            if (src == One) return src;
            Vec4 result = new Vec4(1 - src.X, 1 - src.Y, 1 - src.Z, 1);
            return result;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        #endregion
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

        #region operator
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
        public static Vec3 operator/(Vec3 a, float b)
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
        public static bool operator !=(Vec3 a, Vec3 b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z;
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


    public struct Pnt3: I3dLocateable
    {
        public int X;
        public int Y;
        public int Z;

        public Pnt3(int x,int y,int z)
        {
            X = x;Y = y;Z = z;
        }

        public int Coord => Utils.Misc.CoordInt(X, Y);

        int I3dLocateable.Z => Z;

        int I2dLocateable.X => X;

        int I2dLocateable.Y => Y;
    }


    public struct Pnt : I2dLocateable
    {
        public int X;
        public int Y;


        public Pnt(int x, int y)
        {
            X = x;Y = y;
        }
        public Pnt(I2dLocateable src)
        {
            X = src.X; Y = src.Y;
        }
        public Pnt(I3dLocateable src)
        {
            X = src.X; Y = src.Y;
        }

        public static Pnt Zero { get { return new Pnt(0, 0); } }

        public int Coord { get { return Utils.Misc.CoordInt(X, Y); } }

        public static Pnt operator+(Pnt a, Pnt b)
        {
            return new Pnt(a.X + b.X, a.Y + b.Y);
        }
        public static Pnt operator-(Pnt a, Pnt b)
        {
            return new Pnt(a.X - b.X, a.Y - b.Y);
        }
        public static bool operator==(Pnt a, Pnt b)
        {
            return a.X == b.X && a.Y == b.Y;
        }
        public static bool operator!=(Pnt a, Pnt b)
        {
            return a.X != b.X || a.Y != b.Y;
        }
        public static bool operator==(Pnt a, int b)
        {
            return a.X == b && a.Y == b;
        }
        public static bool operator==(int b, Pnt a)
        {
            return a == b;
        }
        public static bool operator!=(Pnt a, int b)
        {
            return a.X != b || a.Y != b;
        }
        public static bool operator!=(int b, Pnt a)
        {
            return a != b;
        }

        int I2dLocateable.X { get { return X; } }

        int I2dLocateable.Y { get { return Y; } }

        public static Pnt FromPoint(Point src)
        {
            return new Pnt() { X = src.X, Y = src.Y };
        }
        public Point ToPoint()
        {
            return new Point(X, Y);
        }
    }
}
