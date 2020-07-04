using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure;

namespace RelertSharp.Common
{
    public class Bfs2D : IEnumerable
    {
        private Predicate<Tile> predicate;
        TileLayer referance;
        Tile begin;


        public Bfs2D(TileLayer src, Tile pos, Predicate<Tile> predicate)
        {
            this.predicate = predicate;
            referance = src;
            begin = pos;
        }
        public IEnumerator GetEnumerator() { return new Bfs2DEnumerator(referance, begin, predicate); }
    }


    internal class Bfs2DEnumerator : IEnumerator<Tile>
    {
        private Predicate<Tile> predicate;
        private TileLayer referance;
        private Tile begin;
        private Tile now;
        private Queue<Tile> queue = new Queue<Tile>();
        private List<Tile> marked = new List<Tile>();


        public Bfs2DEnumerator(TileLayer referance, Tile begin, Predicate<Tile> predicate)
        {
            this.referance = referance;
            this.begin = begin;
            this.predicate = predicate;
            if (predicate.Invoke(begin))
            {
                queue.Enqueue(begin);
                marked.Add(begin);
            }
        }

        public Tile Current { get { return now; } }

        object IEnumerator.Current { get { return now; } }

        public void Dispose()
        {
            now = null;
            marked.Clear();
            queue.Clear();
        }

        public bool MoveNext()
        {
            while (queue.Count != 0)
            {
                Tile t = queue.Dequeue();
                foreach (Tile neb in referance.GetNeighbor(t))
                {
                    if (!marked.Contains(neb) && predicate.Invoke(neb))
                    {
                        queue.Enqueue(neb);
                        marked.Add(neb);
                    }
                }
                now = t;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            now = begin;
            marked.Clear();
            queue.Clear();
            queue.Enqueue(begin);
        }
    }
}
