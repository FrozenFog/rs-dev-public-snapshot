using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Common
{
    public class Square2D : IEnumerable<I2dLocateable>
    {
        private I2dLocateable src;
        private int x, y;
        public Square2D(I2dLocateable pos, int lenX, int lenY) { src = pos;x = lenX;y = lenY; }
        public Square2D(I2dLocateable up, I2dLocateable down)
        {
            if (up.X >= down.X && up.Y >= down.Y)
            {
                src = down;
                x = up.X - down.X + 1;
                y = up.Y - down.Y + 1;
            }
            else if (up.X <= down.X && up.Y >= down.Y)
            {
                src = new Pnt(up.X, down.Y);
                x = down.X - up.X + 1;
                y = up.Y - down.Y + 1;
            }
            else if (up.X >= down.X && up.Y <= down.Y)
            {
                src = new Pnt(down.X, up.Y);
                x = up.X - down.X + 1;
                y = down.Y - up.Y + 1;
            }
            else
            {
                src = up;
                x = down.X - up.X + 1;
                y = down.Y - up.Y + 1;
            }
        }
        public IEnumerator<I2dLocateable> GetEnumerator() { return new Square2DEnumerator(src, x, y); }
        IEnumerator IEnumerable.GetEnumerator() { return new Square2DEnumerator(src, x, y); }
    }


    internal class Square2DEnumerator : IEnumerator<I2dLocateable>
    {
        private int xMax = -1, yMax = -1;
        private int xNow, yNow;
        private I2dLocateable org;

        public Square2DEnumerator(I2dLocateable pos, int lenX, int lenY)
        {
            xMax = lenX + pos.X;
            yMax = lenY + pos.Y;
            xNow = pos.X - 1;
            yNow = pos.Y;
            org = pos;
        }

        public I2dLocateable Current { get { return new Base2D(xNow, yNow); } }

        object IEnumerator.Current { get { return Current; } }

        public void Dispose()
        {
            xMax = -1;
            yMax = -1;
        }

        public bool MoveNext()
        {
            xNow++;
            if (xNow >= xMax)
            {
                xNow = org.X;
                yNow++;
            }
            return yNow < yMax && xNow < xMax;
        }

        public void Reset()
        {
            xMax = -1;
            yMax = -1;
        }
    }
}
