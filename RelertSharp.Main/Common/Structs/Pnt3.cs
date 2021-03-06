namespace RelertSharp.Common
{
    public struct Pnt3 : I3dLocateable
    {
        public int X;
        public int Y;
        public int Z;

        public Pnt3(int x, int y, int z)
        {
            X = x; Y = y; Z = z;
        }
        public Pnt3(I2dLocateable d2, int height)
        {
            X = d2.X; Y = d2.Y; Z = height;
        }
        public Pnt3(I3dLocateable src)
        {
            X = src.X; Y = src.Y; Z = src.Z;
        }
        public static Pnt3 operator +(Pnt3 a, Pnt b)
        {
            return new Pnt3(a.X + b.X, a.Y + b.Y, a.Z);
        }
        public static Pnt3 operator -(Pnt3 a, Pnt b)
        {
            return new Pnt3(a.X - b.X, a.Y - b.Y, a.Z);
        }
        public int Coord => Utils.Misc.CoordInt(X, Y);

        int I3dLocateable.Z { get { return Z; } set { Z = value; } }

        int I2dLocateable.X { get { return X; } set { X = value; } }

        int I2dLocateable.Y { get { return Y; } set { Y = value; } }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}", X, Y, Z);
        }
    }
}
