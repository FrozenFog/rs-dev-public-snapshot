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
        public virtual void RemoveByCoord(I2dLocateable src)
        {
            string coord = CoordString(src.Y, src.X);
            if (data.Keys.Contains(coord)) data.Remove(coord);
        }
        #endregion


        #region Public Calls - PointCollectionBase
        public T this[int x, int y]
        {
            get
            {
                string coord = CoordString(y, x);
                if (data.Keys.Contains(coord)) return data[coord];
                return null;
            }
            set
            {
                data[CoordString(y, x)] = value;
            }
        }
        public T this[string coord]
        {
            get
            {
                if (data.Keys.Contains(coord)) return data[coord];
                return null;
            }
            set
            {
                data[coord] = value;
            }
        }
        public T this[I2dLocateable pos]
        {
            get
            {
                string coord = CoordString(pos.Y, pos.X);
                if (data.Keys.Contains(coord)) return data[coord];
                return null;
            }
            set
            {
                string coord = CoordString(pos.Y, pos.X);
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
            Coord = int.Parse(_coord);
        }
        public PointItemBase(int _x, int _y)
        {
            X = _x;
            Y = _y;
        }


        #region Public Methods - PointItemBase
        public void MoveTo(I2dLocateable pos)
        {
            X = pos.X;
            Y = pos.Y;
        }
        public void ShiftBy(I2dLocateable delta)
        {
            X += delta.X;
            Y += delta.Y;
        }
        #endregion


        #region Public Calls - PointItemBase
        public string CoordString
        {
            get
            {
                return Utils.Misc.CoordString(Y, X);
            }
        }
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
        public bool Selected { get; set; }
        #endregion;
    }
}
