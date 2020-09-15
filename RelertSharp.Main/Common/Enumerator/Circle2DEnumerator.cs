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
        private I2dLocateable last;
        private Pnt data;
        private int range;
        private int thetadeg;
        private int ox, oy;


        public Circle2DEnumerator(I2dLocateable src, int r)
        {
            range = r;
            ox = src.X;
            oy = src.Y;
            data = new Pnt(src);
            data.X += r;
            last = new Pnt(-1, -1);
            thetadeg = 0;
        }

        public I2dLocateable Current { get { return data; } }

        private void Refresh()
        {
            int dx = (int)Math.Round((float)range * Math.Cos(ToRad(thetadeg)));
            int dy = (int)Math.Round((float)range * Math.Sin(ToRad(thetadeg)));
            data.X = dx + ox;
            data.Y = dy + oy;
        }
        private double ToRad(int deg)
        {
            return Math.PI * (deg / 180f);
        }

        object IEnumerator.Current { get { return Current; } }

        public void Dispose()
        {
            thetadeg = 360;
            range = -1;
        }

        public bool MoveNext()
        {
            while (thetadeg < 360)
            {
                thetadeg++;
                Refresh();
                if (data.X != last.X || data.Y != last.Y) break;
            }
            last = data;
            return thetadeg < 360;
        }

        public void Reset()
        {
            thetadeg = 0;
        }
    }
}
