using RelertSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RelertSharp
{
    public static class RsMath
    {
        /// <summary>
        /// include both
        /// </summary>
        /// <param name="src"></param>
        /// <param name="floor"></param>
        /// <param name="ceil"></param>
        /// <returns></returns>
        public static int TrimTo(this int src, int floor, int ceil)
        {
            if (floor > ceil) throw new ArgumentException("Floor is grater than Ceil");
            if (src >= ceil) return ceil;
            if (src <= floor) return floor;
            return src;
        }

        /// <summary>
        /// include both
        /// </summary>
        /// <param name="src"></param>
        /// <param name="floor"></param>
        /// <param name="ceil"></param>
        /// <returns></returns>
        public static long TrimTo(this long src, long floor, long ceil)
        {
            if (floor > ceil) throw new ArgumentException("Floor is grater than Ceil");
            if (src >= ceil) return ceil;
            if (src <= floor) return floor;
            return src;
        }

        public static short TrimTo(this short src, short floor, short ceil)
        {
            if (floor > ceil) throw new ArgumentException("Floor is grater than Ceil");
            if (src >= ceil) return ceil;
            if (src <= floor) return floor;
            return src;
        }

        public static decimal TrimTo(this decimal src, decimal floor, decimal ceil)
        {
            if (floor > ceil) throw new ArgumentException("Floor is grater than Ceil");
            if (src >= ceil) return ceil;
            if (src <= floor) return floor;
            return src;
        }

        public static float TrimTo(this float src, float floor, float ceil)
        {
            if (floor > ceil) throw new ArgumentException("Floor is grater than Ceil");
            if (src >= ceil) return ceil;
            if (src <= floor) return floor;
            return src;
        }

        /// <summary>
        /// include both
        /// </summary>
        /// <param name="src"></param>
        /// <param name="floor"></param>
        /// <param name="ceil"></param>
        /// <returns></returns>
        public static bool InRange(this double src, double floor, double ceil)
        {
            return src >= floor && src <= ceil;
        }
        public static bool IsInt(this double src)
        {
            return (int)src == src;
        }
        public static double ForcePositive(this double src)
        {
            if (src <= 0) return 0;
            return src;
        }
        public static int ForcePositive(this int src)
        {
            if (src <= 0) return 0;
            return src;
        }
        public static int Floor(double src)
        {
            return src >= 0 ? (int)src : (int)src - 1;
        }
        public static int Ceil(double src)
        {
            return src >= 0 ? (int)src + 1 : (int)src;
        }
        public static int Round(double src)
        {
            return (int)Math.Round(src);
        }
        public static int ChebyshevDistance(System.Windows.Point p1, System.Windows.Point p2)
        {
            return (int)Math.Max(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
        }
        public static float ScalarProduct(I2dLocateable a, I2dLocateable b)
        {
            return a.X * b.X + a.Y * b.Y;
        }
        public static float ScalarProduct(I3dLocateable a, I3dLocateable b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }
        public static float ScalarProduct(Vec3 a, Vec3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }
        public static double ScalarProduct(Vector a, Vector b)
        {
            return a.X * b.X + a.Y * b.Y;
        }
        public static I2dLocateable I2dSubi(I2dLocateable value, I2dLocateable minusBy)
        {
            return new Pnt(value.X - minusBy.X, value.Y - minusBy.Y);
        }
        public static I2dLocateable I2dAddi(I2dLocateable a, I2dLocateable b)
        {
            return new Pnt(a.X + b.X, a.Y + b.Y);
        }
        public static bool I2dEqual(I2dLocateable a, I2dLocateable b)
        {
            if (a == null || b == null) return false;
            return a.X == b.X && a.Y == b.Y;
        }
        public static bool I2dEmpty(I2dLocateable src)
        {
            return src.X == 0 && src.Y == 0;
        }
        public static bool I3dEqual(I3dLocateable a, I3dLocateable b)
        {
            if (a == null || b == null) return false;
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }
        public static bool I2dValid(I2dLocateable pos)
        {
            return pos.X > 0 && pos.Y > 0;
        }
        public static I3dLocateable I3dSubi(I3dLocateable value, I3dLocateable minusBy)
        {
            return new Pnt3(value.X - minusBy.X, value.Y - minusBy.Y, value.Z - minusBy.Z);
        }
        public static I3dLocateable I3dAddi(I3dLocateable a, I3dLocateable b)
        {
            return new Pnt3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static bool I3dEmpty(I3dLocateable src)
        {
            return src.X == 0 && src.Y == 0 && src.Z == 0;
        }
        public static double I2dMagnitude(I2dLocateable vec)
        {
            return Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y);
        }
        public static double I2dAngle(I2dLocateable a, I2dLocateable b)
        {
            double prod = ScalarProduct(a, b);
            double k = I2dMagnitude(a) * I2dMagnitude(b);
            if (k == 0) return 360;
            return Math.Acos(prod / k) / Math.PI * 180;
        }


        public static T[] Mirror2dArray<T>(this T[] arr, int width, int height, bool isXArray = true)
        {
            T[] result = new T[arr.Length];
            if (isXArray)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        result[width - x - 1 + y * width] = arr[x + y * width];
                    }
                }
            }
            else
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        result[x + (height - y - 1) * width] = arr[x + y * width];
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// scale between -1.0 and 1.0
        /// </summary>
        /// <param name="arr"></param>
        public static void NormalizeArray(ref double[] arr)
        {
            double max = arr.Max();
            double min = arr.Min();
            double mid = (max + min) / 2;
            double scaled = (max - min) / 2;
            for (int i = 0; i < arr.Length; i++)
            {
                double normalize = (arr[i] - mid) / scaled;
                arr[i] = normalize;
            }
        }
    }
}
