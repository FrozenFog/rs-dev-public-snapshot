using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Common
{
    public class Disk2D : IEnumerable
    {
        private I2dLocateable pos;
        private int radius;
        public Disk2D(I2dLocateable src, int radius)
        {
            this.radius = radius;
            pos = src;
        }
        public IEnumerator GetEnumerator() { return new Disk2DEnumerator(pos, radius); }
    }

    internal class Disk2DEnumerator : IEnumerator<I2dLocateable>
    {
        private const int MAX_DEG = 360 / 8;
        private double deltaDeg;
        private I2dLocateable prev;
        private Pnt data;
        private int radius;
        private double thetaDeg;

        private int ox, oy;
        private int halfQuad = 0;
        private int dx, dy;

        private bool isScanLine;
        private int yMin;


        public Disk2DEnumerator(I2dLocateable src, int r)
        {
            radius = r;
            ox = src.X;
            oy = src.Y;
            data = new Pnt(src);
            data.X += r;
            prev = new Pnt(-1, -1);
            thetaDeg = 0;
            CalcDelta(r);
        }
        private const double a = 44.95389;
        private const double b = -0.99734;
        private void CalcDelta(int radius)
        {
            /// formula : deg = a * radius ^ b
            /// where a = 44.95389, std-err 0.00974
            ///       b = -0.99734, std-err 1.9899e-4
            /// reduced chi-sqr = 1.13662e-4
            /// adj. r-sqr = 0.99996

            deltaDeg = a * Math.Pow(radius, b);
        }
        private double ToRad(double deg)
        {
            return Math.PI * (deg / 180f);
        }
        private void ProceedQuad()
        {
            switch (halfQuad)
            {
                case 0:
                    dx = (int)Math.Round((float)radius * Math.Cos(ToRad(thetaDeg)));
                    dy = (int)Math.Round((float)radius * Math.Sin(ToRad(thetaDeg)));
                    data.X = ox + dx;
                    data.Y = oy + dy;
                    yMin = oy - dy;
                    break;
                case 1:
                    data.X = ox + dy;
                    data.Y = oy + dx;
                    yMin = oy - dx;
                    break;
                case 2:
                    data.X = ox - dy;
                    data.Y = oy + dx;
                    yMin = oy - dx;
                    break;
                case 3:
                    data.X = ox - dx;
                    data.Y = oy + dy;
                    yMin = oy - dy;
                    break;
                case 4:
                    halfQuad = -1;
                    thetaDeg += deltaDeg;
                    break;
            }
            halfQuad++;
        }
        private void Refresh()
        {
            if (isScanLine)
            {
                data.Y--;
                isScanLine = data.Y > yMin;
            }
            else
            {
                ProceedQuad();
                isScanLine = true;
            }
        }

        public I2dLocateable Current { get { return data; } }

        object IEnumerator.Current { get { return data; } }

        public void Dispose()
        {
            thetaDeg = MAX_DEG;
            radius = -1;
        }

        public bool MoveNext()
        {
            while (thetaDeg < MAX_DEG + deltaDeg)
            {
                Refresh();
                if (data.X != prev.X || data.Y != prev.Y) break;
            }
            prev = data;
            return thetaDeg < MAX_DEG + deltaDeg;
        }

        public void Reset()
        {
            thetaDeg = 0;
        }
    }
}
