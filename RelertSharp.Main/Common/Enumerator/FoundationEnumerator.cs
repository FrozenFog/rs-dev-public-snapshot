using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure.Objects;

namespace RelertSharp.Common
{
    public class Foundation2D : IEnumerable<I2dLocateable>
    {
        private I2dLocateable src;
        private List<bool> shape;
        private int x, y;
        public Foundation2D(StructureItem src)
        {
            this.src = src;
            x = src.SizeX;
            y = src.SizeY;
            shape = src.BuildingShape;
        }

        public IEnumerator<I2dLocateable> GetEnumerator() { return new FoundationEnumerator(shape, src, x, y); }

        IEnumerator IEnumerable.GetEnumerator() { return new FoundationEnumerator(shape, src, x, y); }
    }


    internal class FoundationEnumerator : IEnumerator<I2dLocateable>
    {
        private int xMax = -1, yMax = -1;
        private int xNow, yNow;
        private int pos = -1;
        private I2dLocateable begin;
        private List<bool> shape;


        public FoundationEnumerator(List<bool> shape, I2dLocateable begin, int x, int y)
        {
            xMax = x + begin.X;
            yMax = y + begin.Y;
            xNow = begin.X - 1;
            yNow = begin.Y;
            this.begin = begin;
            this.shape = shape;
        }

        public I2dLocateable Current { get { return new Pnt(xNow, yNow); } }

        object IEnumerator.Current { get { return Current; } }

        public void Dispose()
        {
            xMax = -1;
            yMax = -1;
        }

        public bool MoveNext()
        {
            pos++;
            if (xNow >= xMax)
            {
                xNow = begin.X;
                yNow++;
            }
            return yNow < yMax && xNow < xMax && shape[pos];
        }

        public void Reset()
        {
            xNow = begin.X - 1;
            yNow = begin.Y;
        }
    }
}
