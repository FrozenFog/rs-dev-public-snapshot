using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.DrawingEngine.Presenting;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure.Points
{
    public class PointCollectionBase<T> : IEnumerable<T> where T : PointItemBase
    {
        private Dictionary<string, T> data = new Dictionary<string, T>();


        public PointCollectionBase() { }


        #region Public Methods - ObjectBase
        public void AddObject(T item)
        {
            if (string.IsNullOrEmpty(item.ID))
            {
                for (int i = 0; i < 10000; i++)
                {
                    if (!data.Keys.Contains(i.ToString()))
                    {
                        item.ID = i.ToString();
                        break;
                    }
                }
            }
            data[item.ID] = item;
        }
        public void RemoveObjectByID(T item)
        {
            if (data.Keys.Contains(item.ID)) data.Remove(item.ID);
        }
        #endregion


        #region Public Calls - PointCollectionBase
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
        public PointItemBase(I2dLocateable pos)
        {
            X = pos.X;
            Y = pos.Y;
        }
        public PointItemBase(PointItemBase src)
        {
            X = src.X;
            Y = src.Y;
            SceneObject = src.SceneObject;
        }


        #region Public Methods - PointItemBase
        public void MoveTo(I3dLocateable pos)
        {
            X = pos.X;
            Y = pos.Y;
            SceneObject.MoveTo(pos);
        }
        public void ShiftBy(I3dLocateable delta)
        {
            X += delta.X;
            Y += delta.Y;
            SceneObject.ShiftBy(delta);
        }
        public void Select()
        {
            if (!Selected)
            {
                Selected = true;
                SceneObject.MarkSelected();
            }
        }
        public void UnSelect()
        {
            if (Selected)
            {
                Selected = false;
                SceneObject.Unmark();
            }
        }
        public void Dispose()
        {
            Selected = false;
            SceneObject.Dispose();
        }
        public void Hide()
        {
            if (!IsHidden)
            {
                SceneObject.Hide();
                IsHidden = true;
            }
        }
        public void Reveal()
        {
            if (IsHidden)
            {
                SceneObject.Reveal();
                IsHidden = false;
            }
        }
        #endregion


        #region Public Calls - PointItemBase
        public bool IsHidden { get; protected set; }
        public string CoordString
        {
            get
            {
                return Utils.Misc.CoordString(Y, X);
            }
        }
        public int X { get; set; }
        public int Y { get; set; }
        public string ID { get; set; }
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
        public string RegName { get { return string.Empty; } }
        #endregion


        #region Protected
        protected virtual IPresentBase SceneObject { get; set; }
        #endregion
    }
}
