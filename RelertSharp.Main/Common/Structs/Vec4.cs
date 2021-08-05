using System;

namespace RelertSharp.Common
{
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
        public Vec4(double r, double g, double b, double a)
        {
            X = (float)r; Y = (float)g;Z = (float)b;V = (float)a;
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
        public static Vec4 BaseNodeSelector
        {
            get { return new Vec4(0.5f, 0.5f, 1, 0.6f); }
        }
        public static Vec4 TileIndicator
        {
            get { return new Vec4(1, 0, 0, 1); }
        }
        public static Vec4 TileExIndi
        {
            get { return new Vec4(0, 1, 0, 1); }
        }
        public static Vec4 BaseNodeColor
        {
            get { return new Vec4(0.5f, 1, 0.5f, 0.6f); }
        }
        public static Vec4 HideCompletely
        {
            get { return new Vec4(1, 1, 1, 0); }
        }
        public static Vec4 Hide50
        {
            get { return new Vec4(1f, 1f, 1f, 0.5f); }
        }
        public static Vec4 BuildableTile
        {
            get { return new Vec4(0, 1, 0, 1); }
        }
        public static Vec4 UnBuildableTile
        {
            get { return new Vec4(1, 0, 0, 1); }
        }


        #region operator
        public static double operator^(Vec4 a, Vec4 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.V * b.V;
        }
        public static Vec4 operator +(Vec4 a, Vec4 b)
        {
            return new Vec4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.V + b.V);
        }
        public static Vec4 operator -(Vec4 a, Vec4 b)
        {
            return new Vec4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.V - b.V);
        }
        public static Vec4 operator *(Vec4 a, Vec4 b)
        {
            return new Vec4(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.V * b.V);
        }
        public static Vec4 operator *(Vec4 a, float b)
        {
            return new Vec4(a.X * b, a.Y * b, a.Z * b, a.V * b);
        }
        public static Vec4 operator *(float a, Vec4 b)
        {
            return b * a;
        }
        public static Vec4 operator /(Vec4 a, float b)
        {
            return new Vec4(a.X / b, a.Y / b, a.Z / b, a.V / b);
        }
        public static Vec4 operator /(Vec4 a, Vec4 b)
        {
            return new Vec4(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.V / b.V);
        }
        public static bool operator ==(Vec4 a, Vec4 b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.V == b.V;
        }
        public static bool operator !=(Vec4 a, Vec4 b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z || a.V != b.V;
        }
        public static Vec4 operator !(Vec4 src)
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
}
