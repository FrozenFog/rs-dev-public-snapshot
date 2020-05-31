using System.Drawing;

namespace RelertSharp.Common
{
    public struct Pnt : I2dLocateable
    {
        public int X;
        public int Y;


        public Pnt(int x, int y)
        {
            X = x; Y = y;
        }
        public Pnt(I2dLocateable src)
        {
            X = src.X; Y = src.Y;
        }
        public Pnt(I3dLocateable src)
        {
            X = src.X; Y = src.Y;
        }

        public static Pnt Zero { get { return new Pnt(0, 0); } }

        public int Coord { get { return Utils.Misc.CoordInt(X, Y); } }

        public static Pnt operator +(Pnt a, Pnt b)
        {
            return new Pnt(a.X + b.X, a.Y + b.Y);
        }
        public static Pnt operator -(Pnt a, Pnt b)
        {
            return new Pnt(a.X - b.X, a.Y - b.Y);
        }
        public static bool operator ==(Pnt a, Pnt b)
        {
            return a.X == b.X && a.Y == b.Y;
        }
        public static bool operator !=(Pnt a, Pnt b)
        {
            return a.X != b.X || a.Y != b.Y;
        }
        public static bool operator ==(Pnt a, int b)
        {
            return a.X == b && a.Y == b;
        }
        public static bool operator ==(int b, Pnt a)
        {
            return a == b;
        }
        public static bool operator !=(Pnt a, int b)
        {
            return a.X != b || a.Y != b;
        }
        public static bool operator !=(int b, Pnt a)
        {
            return a != b;
        }

        int I2dLocateable.X { get { return X; } }

        int I2dLocateable.Y { get { return Y; } }

        public static Pnt FromPoint(Point src)
        {
            return new Pnt() { X = src.X, Y = src.Y };
        }
        public Point ToPoint()
        {
            return new Point(X, Y);
        }
    }
}
