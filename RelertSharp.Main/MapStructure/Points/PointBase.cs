using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure.Points
{
    public class PointCollectionBase
    {
        private Dictionary<string, PointItemBase> data = new Dictionary<string, PointItemBase>();
        public PointCollectionBase() { }


        #region Public Calls - PointCollectionBase
        public PointItemBase this[int x, int y]
        {
            get
            {
                string coord = CoordString(x, y);
                if (data.Keys.Contains(coord)) return data[coord];
                return new PointItemBase();
            }
            set
            {
                data[CoordString(x, y)] = value;
            }
        }
        public PointItemBase this[string coord]
        {
            get
            {
                if (data.Keys.Contains(coord)) return data[coord];
                return new PointItemBase();
            }
            set
            {
                data[coord] = value;
            }
        }
        #endregion
    }



    public class PointItemBase
    {
        private int x, y;
        public PointItemBase() { }
        public PointItemBase(string _coord)
        {
            Coord = _coord;
            x = CoordByteX(int.Parse(Coord));
            y = CoordByteY(int.Parse(Coord));
        }
        public PointItemBase(int _x, int _y)
        {
            Coord = CoordString(x, y);
            x = _x;
            y = _y;
        }


        #region Public Calls - PointItemBase
        public string Coord { get; set; }
        public int CoordX { get { return x; } set { x = value; } }
        public int CoordY { get { return y; } set { y = value; } }
        #endregion;
    }
}
