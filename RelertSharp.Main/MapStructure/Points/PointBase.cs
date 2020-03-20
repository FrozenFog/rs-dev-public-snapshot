using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure.Points
{
    public class PointCollectionBase<T> : IEnumerable<T>
    {
        private Dictionary<string, T> data = new Dictionary<string, T>();
        public PointCollectionBase() { }


        #region Public Calls - PointCollectionBase
        public T this[int x, int y]
        {
            get
            {
                string coord = CoordString(x, y);
                if (data.Keys.Contains(coord)) return data[coord];
                return default(T);
            }
            set
            {
                data[CoordString(x, y)] = value;
            }
        }
        public T this[string coord]
        {
            get
            {
                if (data.Keys.Contains(coord)) return data[coord];
                return default(T);
            }
            set
            {
                data[coord] = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.Values.GetEnumerator();
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
            CoordInt = int.Parse(_coord);
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
        public int CoordInt
        {
            get { return Utils.Misc.CoordInt(CoordX, CoordY); }
            set
            {
                CoordX = CoordIntX(value);
                CoordY = CoordIntY(value);
            }
        }
        #endregion;
    }
}
