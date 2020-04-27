using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Common
{
    public class Square2D : IEnumerable
    {
        private I2dLocateable src;
        private int x, y;
        public Square2D(I2dLocateable pos, int lenX, int lenY) { src = pos;x = lenX;y = lenY; }
        public IEnumerator GetEnumerator() { return new Square2DEnumerator(src, x, y); }
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
