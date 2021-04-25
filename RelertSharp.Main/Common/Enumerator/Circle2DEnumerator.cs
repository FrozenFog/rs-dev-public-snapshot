using System;
using System.Collections;
using System.Collections.Generic;

namespace RelertSharp.Common
{
    public class Circle2D : IEnumerable
    {
        private I2dLocateable pos;
        private int range;
        public Circle2D(I2dLocateable src, int r) { pos = src; range = r; }
        public IEnumerator GetEnumerator() { return new Circle2DEnumerator(pos, range); }
    }

    internal class Circle2DEnumerator : IEnumerator<I2dLocateable>
    {
        private const double MAX_RAD = Math.PI / 4;
        private double deltaRad;
        private I2dLocateable last;
        private Pnt data;
        private int range;
        private double thetaRad;
        private int ox, oy;
        private int halfQuad = 0;
        private int dx, dy;


        public Circle2DEnumerator(I2dLocateable src, int r)
        {
            range = r;
            ox = src.X;
            oy = src.Y;
            data = new Pnt(src);
            data.X += r;
            last = new Pnt(-1, -1);
            thetaRad = 0;
            CalcDelta(r);
        }

        public I2dLocateable Current { get { return data; } }

        private void Refresh()
        {
            switch (halfQuad)
            {
                case 0:
                    dx = (int)Math.Round((float)range * Math.Cos(thetaRad));
                    dy = (int)Math.Round((float)range * Math.Sin(thetaRad));
                    data.X = ox + dx;
                    data.Y = oy + dy;
                    break;
                case 1:
                    data.X = ox + dy;
                    data.Y = oy + dx;
                    break;
                case 2:
                    data.X = ox - dy;
                    data.Y = oy + dx;
                    break;
                case 3:
                    data.X = ox - dx;
                    data.Y = oy + dy;
                    break;
                case 4:
                    data.X = ox - dx;
                    data.Y = oy - dy;
                    break;
                case 5:
                    data.X = ox - dy;
                    data.Y = oy - dx;
                    break;
                case 6:
                    data.X = ox + dy;
                    data.Y = oy - dx;
                    break;
                case 7:
                    data.X = ox + dx;
                    data.Y = oy - dy;
                    break;
                case 8:
                    halfQuad = -1;
                    thetaRad += deltaRad;
                    break;
            }
            halfQuad++;
        }
        private const double a = 44.95389 * Math.PI / 180f;
        private const double b = -0.99734;
        private void CalcDelta(int radius)
        {
            /// formula : deg = a * radius ^ b
            /// where a = 44.95389, std-err 0.00974
            ///       b = -0.99734, std-err 1.9899e-4
            /// reduced chi-sqr = 1.13662e-4
            /// adj. r-sqr = 0.99996

            deltaRad = a * Math.Pow(radius, b);
            //deltaDeg = 360d / 7000;
        }

        object IEnumerator.Current { get { return Current; } }

        public void Dispose()
        {
            thetaRad = MAX_RAD;
            range = -1;
        }

        public bool MoveNext()
        {
            while (thetaRad < MAX_RAD + deltaRad)
            {
                Refresh();
                if (data.X != last.X || data.Y != last.Y) break;
            }
            last = data;
            return thetaRad < MAX_RAD + deltaRad;
        }

        public void Reset()
        {
            thetaRad = 0;
        }
    }
}
