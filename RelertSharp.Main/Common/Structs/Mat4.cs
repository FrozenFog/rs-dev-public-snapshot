using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Common
{
    public struct Mat4
    {
        public Mat4(float[] src)
        {
            Mat = new double[16];
            for (int i = 0; i < src.Length; i++)
            {
                Mat[i] = src[i];
            }
            if (src.Length != 16) Mat[15] = 1;
        }
        public Mat4(double[] src)
        {
            Mat = new double[16];
            for (int i = 0; i< src.Length; i++)
            {
                Mat[i] = src[i];
            }
            if (src.Length != 16) Mat[15] = 1;
        }
        public Mat4(double r1c1, double r2c2, double r3c3, double r4c4 = 1)
        {
            Mat = new double[16]
            {
                r1c1, 0, 0, 0,
                0, r2c2, 0, 0,
                0, 0, r3c3, 0,
                0, 0, 0, r4c4
            };
        }
        public void AddToColumn(int col, Vec4 target)
        {
            if (RsMath.InRange(col, 0, 3))
            {
                Mat[col] += target.X;
                Mat[col + 4] += target.Y;
                Mat[col + 8] += target.Z;
                Mat[col + 12] += target.V;
            }
        }
        public Vec4 GetLine(int line)
        {
            if (RsMath.InRange(line, 0, 3))
            {
                Vec4 r = new Vec4(
                    Mat[line * 4 + 0],
                    Mat[line * 4 + 1],
                    Mat[line * 4 + 2],
                    Mat[line * 4 + 3]);
                return r;
            }
            return Vec4.Zero;
        }
        public Vec4 GetColumn(int col)
        {
            if (RsMath.InRange(col, 0, 3))
            {
                Vec4 r = new Vec4(
                    Mat[col],
                    Mat[col + 4],
                    Mat[col + 8],
                    Mat[col + 12]);
                return r;
            }
            return Vec4.Zero;
        }
        public void SetLine(Vec4 vec, int line)
        {
            if (RsMath.InRange(line, 0, 3))
            {
                Mat[line * 4 + 0] = vec.X;
                Mat[line * 4 + 1] = vec.Y;
                Mat[line * 4 + 2] = vec.Z;
                Mat[line * 4 + 3] = vec.V;
            }
        }
        public static Mat4 Unit3(double scale)
        {
            return new Mat4()
            {
                Mat = new double[16]
                {
                    scale, 0, 0, 0,
                    0, scale, 0, 0,
                    0, 0, scale, 0,
                    0, 0, 0, 1
                }
            };
        }

        #region Fields;
        public double[] Mat;
        #endregion



        #region Operator
        public static Mat4 operator*(Mat4 src, double b)
        {
            double[] mat = new double[16];
            Array.Copy(src.Mat, mat, 16);
            for (int i = 0; i < 16; i++) mat[i] *= b;
            return new Mat4(mat);
        }
        public static Vec4 operator*(Mat4 src, Vec4 vec)
        {
            Vec4 result = new Vec4();
            result.X = (float)(src.GetLine(0) ^ vec);
            result.Y = (float)(src.GetLine(1) ^ vec);
            result.Z = (float)(src.GetLine(2) ^ vec);
            result.V = (float)(src.GetLine(3) ^ vec);
            return result;
        }
        public static Vec4 operator*(Vec4 vec, Mat4 src)
        {
            Vec4 result = new Vec4()
            {
                X = (float)(src.GetColumn(0) ^ vec),
                Y = (float)(src.GetColumn(1) ^ vec),
                Z = (float)(src.GetColumn(2) ^ vec),
                V = (float)(src.GetColumn(3) ^ vec)
            };
            return result;
        }
        public static Mat4 operator*(Mat4 a, Mat4 b)
        {
            Vec4 l1 = a.GetLine(0); Vec4 c1 = b.GetColumn(0);
            Vec4 l2 = a.GetLine(1); Vec4 c2 = b.GetColumn(1);
            Vec4 l3 = a.GetLine(2); Vec4 c3 = b.GetColumn(2);
            Vec4 l4 = a.GetLine(3); Vec4 c4 = b.GetColumn(3);
            double[] d = new double[16]
            {
                l1 ^ c1,
                l1 ^ c2,
                l1 ^ c3,
                l1 ^ c4,

                l2 ^ c1,
                l2 ^ c2,
                l2 ^ c3,
                l2 ^ c4,

                l3 ^ c1,
                l3 ^ c2,
                l3 ^ c3,
                l3 ^ c4,

                l4 ^ c1,
                l4 ^ c2,
                l4 ^ c3,
                l4 ^ c4,
            };
            return new Mat4(d);
        }
        #endregion
    }
}
