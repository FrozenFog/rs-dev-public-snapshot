using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RelertSharp.Algorithm
{
    // Simplex noise referance, contains original perlin noise
    // http://staffwww.itn.liu.se/~stegu/simplexnoise/simplexnoise.pdf
    public static class PerlinNoiseGenerator
    {
        private static readonly byte[] p = new byte[256] {
            151, 160, 137, 91, 90, 15, 
            131, 13, 201, 95, 96, 53, 194, 233, 7, 225, 140, 36, 103, 30, 69, 142, 8, 99, 37, 240, 21, 10, 23, 
            190, 6, 148, 247, 120, 234, 75, 0, 26, 197, 62, 94, 252, 219, 203, 117, 35, 11, 32, 57, 177, 33, 
            88, 237, 149, 56, 87, 174, 20, 125, 136, 171, 168, 68, 175, 74, 165, 71, 134, 139, 48, 27, 166, 
            77, 146, 158, 231, 83, 111, 229, 122, 60, 211, 133, 230, 220, 105, 92, 41, 55, 46, 245, 40, 244, 
            102, 143, 54, 65, 25, 63, 161, 1, 216, 80, 73, 209, 76, 132, 187, 208, 89, 18, 169, 200, 196, 
            135, 130, 116, 188, 159, 86, 164, 100, 109, 198, 173, 186, 3, 64, 52, 217, 226, 250, 124, 123, 
            5, 202, 38, 147, 118, 126, 255, 82, 85, 212, 207, 206, 59, 227, 47, 16, 58, 17, 182, 189, 28, 42, 
            223, 183, 170, 213, 119, 248, 152, 2, 44, 154, 163, 70, 221, 153, 101, 155, 167, 43, 172, 9, 
            129, 22, 39, 253, 19, 98, 108, 110, 79, 113, 224, 232, 178, 185, 112, 104, 218, 246, 97, 228, 
            251, 34, 242, 193, 238, 210, 144, 12, 191, 179, 162, 241, 81, 51, 145, 235, 249, 14, 239, 107, 
            49, 192, 214, 31, 181, 199, 106, 157, 184, 84, 204, 176, 115, 121, 50, 45, 127, 4, 150, 254, 
            138, 236, 205, 93, 222, 114, 67, 29, 24, 72, 243, 141, 128, 195, 78, 66, 215, 61, 156, 180
        };
        private static byte[] permChart;
        private static readonly Vector[] grad =
        {
            new Vector(1, 2),
            new Vector(2, 1),
            new Vector(-1, 2),
            new Vector(-2, 1),
            new Vector(-1, -2),
            new Vector(-2, -1),
            new Vector(1, -2),
            new Vector(2, -1),
        };
        private static double DefaultCurve(float t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);
        }

        /// <summary>
        /// return 2d perlin noise graph widh designated size
        /// </summary>
        /// <param name="width">desighated width(x scale)</param>
        /// <param name="height">desighated height(y scale)</param>
        /// <param name="scaleFactor">frequency, to be percise</param>
        /// <param name="amplitude"></param>
        /// <param name="seed">leave it empty will use time-based random generator</param>
        /// <returns></returns>
        public static double[] Generate2DNoise(PerlinNoiseGeneratorConfig config)
        {
            Random r;
            if (config.Seed == 0)
            {
                Random sr = new Random();
                r = new Random(sr.Next());
            }
            else r = new Random(config.Seed);
            if (config.CurveFunction == null) CurveFunc = DefaultCurve;
            else CurveFunc = config.CurveFunction;
            int w = config.Width;
            int h = config.Height;

            double[] result = new double[w * h];
            float scale = config.Scale;
            int iteration = config.Iteration;

            while (iteration-- > 0)
            {
                int smooth = config.SmoothIteration;
                int randomOffset = r.Next();
                InitPerm(randomOffset);
                double[] arr = new double[w * h];
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        double value = CalcValue(x, y, scale);
                        arr[x + y * w] = value;
                    }
                }
                if (iteration % 4 == 0) arr = arr.Mirror2dArray(w, h);
                else if (iteration % 4 == 1) arr = arr.Mirror2dArray(w, h, false);
                else if (iteration % 4 == 2)
                {
                    arr = arr.Mirror2dArray(w, h);
                    arr = arr.Mirror2dArray(w, h, false);
                }
                float deltaScale = (float)(r.NextDouble() + 0.5);
                scale *= deltaScale;
                while (smooth-- > 0) Smooth(ref arr, w, h);
                AddArr(ref result, arr);
            }
            if (config.Normalize) RsMath.NormalizeArray(ref result);
            return result;
        }


        private static Func<float, double> CurveFunc;
        private static double CalcValue(int ix, int iy, float scale)
        {
            float x = ix / scale;
            float y = iy / scale;

            int X = RsMath.Floor(x);
            int Y = RsMath.Floor(y);

            float u = x - X;
            float v = y - Y;

            X = X & 255;
            Y = Y & 255;

            int iGrad00 = permChart[X + permChart[Y]] % 8;
            int iGrad01 = permChart[X + permChart[Y + 1]] % 8;
            int iGrad10 = permChart[X + 1 + permChart[Y]] % 8;
            int iGrad11 = permChart[X + 1 + permChart[Y + 1]] % 8;
            Vector g00 = grad[iGrad00];
            Vector g01 = grad[iGrad01];
            Vector g10 = grad[iGrad10];
            Vector g11 = grad[iGrad11];

            double n00 = RsMath.ScalarProduct(g00, new Vector(u, v));
            double n01 = RsMath.ScalarProduct(g01, new Vector(u, u - 1));
            double n10 = RsMath.ScalarProduct(g10, new Vector(u - 1, v));
            double n11 = RsMath.ScalarProduct(g11, new Vector(u - 1, v - 1));

            double n_x0 = n10 * (1 - CurveFunc(u)) + n00 * CurveFunc(u);
            double n_x1 = n01 * (1 - CurveFunc(u)) + n11 * CurveFunc(u);
            double n_xy = n_x0 * (1 - CurveFunc(v)) + n_x1 * CurveFunc(v);

            return n_xy;
        }

        private static void InitPerm(int seed)
        {
            permChart = new byte[256 * 2];
            for (int i = 0; i< permChart.Length; i++)
            {
                permChart[i] = p[i % 255];
            }
            permChart.Shuffle(null, seed);
        }

        private static void AddArr(ref double[] dest, double[] srcArr, double scale = 1)
        {
            for (int i = 0; i< dest.Length; i++)
            {
                dest[i] += srcArr[i] + scale;
            }
        }
        private static void Smooth(ref double[] arr, int width, int height)
        {
            double[] org = new double[width * height];
            Array.Copy(arr, org, arr.Length);
            double getAt(int x, int y)
            {
                if (x < 0 || y < 0 || x >= width || y >= height) return 0;
                return org[x + y * width];
            }
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double sum =
                        getAt(x - 1, y - 1) + getAt(x, y - 1) + getAt(x + 1, y - 1) +
                        getAt(x - 1, y) + getAt(x, y) + getAt(x + 1, y) +
                        getAt(x - 1, y + 1) + getAt(x, y + 1) + getAt(x + 1, y + 1);
                    arr[x + y * width] = sum / 9d;
                }
            }
        }
    }

    public class PerlinNoiseGeneratorConfig
    {
        public PerlinNoiseGeneratorConfig()
        {

        }

        public int Width { get; set; }
        public int Height { get; set; }
        public float Scale { get; set; } = 5f;
        public bool Amplify { get; set; }
        public int Seed { get; set; }
        public int Iteration { get; set; } = 4;
        public int SmoothIteration { get; set; } = 1;
        public bool Normalize { get; set; } = true;
        public Func<float, double> CurveFunction { get; set; }
    }
}
