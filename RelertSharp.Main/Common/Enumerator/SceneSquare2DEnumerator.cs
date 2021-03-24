using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace RelertSharp.Common
{
    public class SceneSquare2D : IEnumerable<I2dLocateable>
    {
        private Pnt begin, end;
        private int width;

        public SceneSquare2D(Point LT, Point RB, Pnt cellLT, Pnt cellRB, int mapwidth)
        {
            if (LT.X > RB.X && LT.Y > RB.Y)
            {
                Utils.Misc.Swap(ref LT, ref RB);
            }
            else if (LT.X > RB.X && LT.Y < RB.Y)
            {
                int right = LT.X;
                LT.X = RB.X;
                RB.X = right;
            }
            else if (LT.X < RB.X && LT.Y > RB.Y)
            {
                int up = RB.Y;
                RB.Y = LT.Y;
                LT.Y = up;
            }
            Utils.Misc.TileToFlatCoord(cellLT, mapwidth, out int beginX, out int beginY);
            Utils.Misc.TileToFlatCoord(cellRB, mapwidth, out int endX, out int endY);
            begin = new Pnt(beginX, beginY);
            end = new Pnt(endX, endY);
            width = mapwidth;
        }
        public SceneSquare2D(I2dLocateable cellLT, I2dLocateable cellRB, int mapWidth)
        {
            Utils.Misc.TileToFlatCoord(cellLT, mapWidth, out int beginX, out int beginY);
            Utils.Misc.TileToFlatCoord(cellRB, mapWidth, out int endX, out int endY);
            begin = new Pnt(beginX, beginY);
            end = new Pnt(endX, endY);
            width = mapWidth;
        }

        public IEnumerator<I2dLocateable> GetEnumerator()
        {
            return new SceneSquare2DEnumerator(begin, end, width);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new SceneSquare2DEnumerator(begin, end, width);
        }
    }


    public class SceneSquare2DEnumerator : IEnumerator<I2dLocateable>
    {
        private Pnt begin, end;
        private Pnt current;
        private int width;
        private bool invalid = false;

        public SceneSquare2DEnumerator(Pnt LT, Pnt RB, int mapwidth)
        {
            begin = LT;
            end = RB;
            width = mapwidth;
            current = begin;
            current.X--;
            invalid = begin.X <= 0 || begin.Y <= 0 || end.X <= 0 || end.Y <= 0;
        }

        public I2dLocateable Current
        {
            get
            {
                Utils.Misc.FlatCoordToTile(current, width, out int x, out int y);
                return new Pnt(x, y);
            }
        }

        object IEnumerator.Current { get { return Current; } }

        public void Dispose()
        {
            begin = Pnt.Zero;
            end = Pnt.Zero;
            current = Pnt.Zero;
            width = -1;
        }

        public bool MoveNext()
        {
            if (invalid) return false;
            if (current.Y <= end.Y)
            {
                if (current.X < end.X)
                {
                    current.X++;
                }
                else
                {
                    current.Y++;
                    current.X = begin.X;
                }
                return true;
            }
            return false;
        }

        public void Reset()
        {
            current = begin;
        }
    }
}
