using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Common
{
    public struct PntPos : IPosition
    {
        public int X;
        public int Y;
        public int SubCell;


        public override bool Equals(object obj) => obj is PntPos && ((PntPos)obj) == this;
        public override int GetHashCode() => base.GetHashCode();

        public PntPos(int x, int y)
        {
            X = x; Y = y;
            SubCell = -1;
        }
        public PntPos(I2dLocateable src)
        {
            X = src.X; Y = src.Y;
            SubCell = -1;
        }
        public PntPos(I3dLocateable src)
        {
            X = src.X; Y = src.Y;
            SubCell = -1;
        }
        public PntPos(IPosition src)
        {
            X = src.X; Y = src.Y;
            SubCell = src.SubCell;
        }

        public static PntPos Zero { get { return new PntPos(0, 0); } }
        public static PntPos OneX { get { return new PntPos(1, 0); } }
        public static PntPos OneY { get { return new PntPos(0, 1); } }
        public static PntPos One { get { return new PntPos(1, 1); } }
        /// <summary>
        /// (1, -1)
        /// </summary>
        public static PntPos Diag { get { return new PntPos(1, -1); } }

        public int Coord { get { return Utils.Misc.CoordInt(X, Y); } }

        public static PntPos operator +(PntPos a, PntPos b)
        {
            return new PntPos(a.X + b.X, a.Y + b.Y);
        }
        public static PntPos operator -(PntPos a, PntPos b)
        {
            return new PntPos(a.X - b.X, a.Y - b.Y);
        }
        public static bool operator ==(PntPos a, PntPos b)
        {
            return a.X == b.X && a.Y == b.Y;
        }
        public static bool operator !=(PntPos a, PntPos b)
        {
            return a.X != b.X || a.Y != b.Y;
        }
        public static bool operator ==(PntPos a, int b)
        {
            return a.X == b && a.Y == b;
        }
        public static bool operator ==(int b, PntPos a)
        {
            return a == b;
        }
        public static bool operator !=(PntPos a, int b)
        {
            return a.X != b || a.Y != b;
        }
        public static bool operator !=(int b, PntPos a)
        {
            return a != b;
        }

        int I2dLocateable.X { get { return X; } set { X = value; } }

        int I2dLocateable.Y { get { return Y; } set { Y = value; } }

        int IPosition.SubCell { get { return SubCell; } }

        public static Pnt FromPoint(Point src)
        {
            return new Pnt() { X = src.X, Y = src.Y };
        }
        public Point ToPoint()
        {
            return new Point(X, Y);
        }
        public override string ToString()
        {
            return string.Format("{0},{1}", X, Y);
        }
    }
}
