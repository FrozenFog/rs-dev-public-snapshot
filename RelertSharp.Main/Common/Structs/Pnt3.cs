using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int Coord => Utils.Misc.CoordInt(X, Y);

        int I3dLocateable.Z => Z;

        int I2dLocateable.X => X;

        int I2dLocateable.Y => Y;
    }
}
