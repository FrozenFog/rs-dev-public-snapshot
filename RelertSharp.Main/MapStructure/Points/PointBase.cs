using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure.Points
{
    public class PointCollectionBase<T> : IEnumerable<T> where T : PointItemBase
    {
        private Dictionary<string, T> data = new Dictionary<string, T>();
        public PointCollectionBase() { }


        #region Public Methods - ObjectBase
        public virtual T FindByCoord(I2dLocateable src)
        {
            foreach (T item in data.Values)
            {
                if (item.X == src.X && item.Y == src.Y) return item;
            }
            return null;
        }
        public virtual void RemoveByCoord(I2dLocateable src)
        {
            Dictionary<string, T> tmp = new Dictionary<string, T>(data);
            foreach (T item in tmp.Values)
            {
                if (item.X == src.X && item.Y == src.Y)
                {
                    data.Remove(item.CoordString);
                    return;
                }
            }
        }
        #endregion


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



    public class PointItemBase : I2dLocateable
    {
        public PointItemBase() { }
        public PointItemBase(string _coord)
        {
            CoordString = _coord;
            Coord = int.Parse(_coord);
        }
        public PointItemBase(int _x, int _y)
        {
            X = _x;
            Y = _y;
            CoordString = CoordString(X, Y);
        }


        #region Public Calls - PointItemBase
        public string CoordString { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Coord
        {
            get { return CoordInt(X, Y); }
            set
            {
                X = CoordIntX(value);
                Y = CoordIntY(value);
            }
        }
        #endregion;
    }
}
