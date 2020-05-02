using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Common
{
    public struct Base2D : I2dLocateable
    {
        public Base2D(I2dLocateable src)
        {
            X = src.X; Y = src.Y;
        }
        public Base2D(int x, int y)
        {
            X = x;Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int Coord { get { return Utils.Misc.CoordInt(this); } }
    }

    public interface I2dLocateable
    {
        int X { get; }
        int Y { get; }
        int Coord { get; }
    }


    public interface I3dLocateable : I2dLocateable
    {
        int Z { get; }
    }


    public interface IMapObject : I2dLocateable
    {
        string RegName { get; }
    }
}
