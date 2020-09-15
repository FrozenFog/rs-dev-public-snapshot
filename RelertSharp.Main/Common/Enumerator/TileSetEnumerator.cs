using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RelertSharp.Common
{
    public class TileSet2D : IEnumerable<I2dLocateable>
    {
        private I2dLocateable begin;
        private IEnumerable<I2dLocateable> offsets;
        public TileSet2D(I2dLocateable pos, IEnumerable<I2dLocateable> offsets)
        {
            begin = pos;
            this.offsets = offsets;
        }

        public IEnumerator<I2dLocateable> GetEnumerator()
        {
            return new TileSetEnumerator(begin, offsets);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new TileSetEnumerator(begin, offsets);
        }
    }
    internal class TileSetEnumerator : IEnumerator<I2dLocateable>
    {
        private int xNow, yNow;
        private I2dLocateable original;
        private int idx;
        private List<I2dLocateable> offsets;


        public TileSetEnumerator(I2dLocateable pos, IEnumerable<I2dLocateable> offsets)
        {
            xNow = pos.X;
            yNow = pos.Y;
            original = pos;
            idx = 0;
            this.offsets = offsets.ToList();
        }

        public I2dLocateable Current { get { return new Pnt(xNow, yNow); } }

        object IEnumerator.Current { get { return Current; } }

        public void Dispose()
        {
            xNow = -1;
            yNow = -1;
        }

        public bool MoveNext()
        {
            if (idx == offsets.Count) return false;
            xNow = original.X + offsets[idx].X;
            yNow = original.Y + offsets[idx++].Y;
            return true;
        }

        public void Reset()
        {
            xNow = -1;
            yNow = -1;
        }
    }
}
