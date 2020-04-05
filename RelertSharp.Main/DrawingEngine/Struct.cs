using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.DrawingEngine
{
    public struct Vec3
    {
        public float X;
        public float Y;
        public float Z;
        public static Vec3 Zero
        {
            get { return new Vec3() { X = 0, Y = 0, Z = 0 }; }
        }
        public static Vec3 operator+(Vec3 a, Vec3 b)
        {
            return new Vec3() { X = a.X + b.X, Y = a.Y + b.Y, Z = a.Z + b.Z };
        }
        public static Vec3 operator-(Vec3 a, Vec3 b)
        {
            return new Vec3() { X = a.X - b.X, Y = a.Y - b.Y, Z = a.Z - b.Z };
        }
        public static Vec3 operator*(Vec3 a, int b)
        {
            return new Vec3() { X = a.X * b, Y = a.Y * b, Z = a.Z * b };
        }
        public static Vec3 operator*(int a, Vec3 b)
        {
            return b * a;
        }
        public static Vec3 operator+(Vec3 a, Pnt b)
        {
            float sq3 = (float)Math.Sqrt(3);
            float sq2 = (float)Math.Sqrt(2);
            //float dz = b.Y * 2 / sq3;
            float dx = sq2 * b.Y + b.X / sq2;
            float dy = sq2 * b.Y - b.X / sq2;
            return new Vec3() { X = a.X + dx, Y = a.Y + dy, Z = a.Z /*- dz*/ };
        }
        public static Vec3 operator+(Pnt b, Vec3 a)
        {
            return a + b;
        }
    }
    public struct Pnt
    {
        public int X;
        public int Y;
    }
}
