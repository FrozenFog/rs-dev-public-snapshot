using RelertSharp.MapStructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RelertSharp.Common
{
    public class Bfs2D : IEnumerable
    {
        private IEnumerable<Predicate<Tile>> predicates;
        TileLayer referance;
        Tile begin;


        public Bfs2D(TileLayer src, Tile pos, IEnumerable<Predicate<Tile>> predicates)
        {
            this.predicates = predicates;
            referance = src;
            begin = pos;
        }
        public IEnumerator GetEnumerator() { return new Bfs2DEnumerator(referance, begin, predicates); }
    }


    internal class Bfs2DEnumerator : IEnumerator<Tile>
    {
        private IEnumerable<Predicate<Tile>> predicates;
        private TileLayer referance;
        private Tile begin;
        private Tile now;
        private Queue<Tile> queue = new Queue<Tile>();
        private HashSet<Tile> marked = new HashSet<Tile>();


        public Bfs2DEnumerator(TileLayer referance, Tile begin, IEnumerable<Predicate<Tile>> predicates)
        {
            this.referance = referance;
            this.begin = begin;
            this.predicates = predicates;
            if (IsTrue(begin))
            {
                queue.Enqueue(begin);
                marked.Add(begin);
            }
        }
        private bool IsTrue(Tile tile)
        {
            bool result = predicates.Count() != 0;
            foreach (Predicate<Tile> predicate in predicates) result = result && predicate.Invoke(tile);
            return result;
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
                    if (!marked.Contains(neb) && IsTrue(neb))
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
