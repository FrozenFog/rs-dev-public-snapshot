using RelertSharp.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.MapStructure.Points
{
    public class PointCollectionBase<T> : IEnumerable<T> where T : PointItemBase
    {
        protected Dictionary<string, T> data = new Dictionary<string, T>();


        public PointCollectionBase() { }


        #region Public Methods - ObjectBase
        public virtual void AddObject(T item, bool forceRenewId = false)
        {
            if (string.IsNullOrEmpty(item.Id) || forceRenewId)
            {
                for (int i = 0; i < 10000; i++)
                {
                    if (!data.Keys.Contains(i.ToString()))
                    {
                        item.Id = i.ToString();
                        break;
                    }
                }
            }
            data[item.Id] = item;
        }
        public virtual T GetItemByPos(I2dLocateable pos)
        {
            foreach (T item in this)
            {
                if (item.Coord == pos.Coord) return item;
            }
            return null;
        }
        public void RemoveObjectByID(T item)
        {
            if (data.Keys.Contains(item.Id)) data.Remove(item.Id);
        }
        public bool HasId(string id)
        {
            return data.Keys.Contains(id);
        }
        public bool HasId(T item)
        {
            return data.Keys.Contains(item.Id);
        }
        internal void Clear()
        {
            data.Clear();
        }
        #endregion


        #region Public Calls - PointCollectionBase
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



    public class PointItemBase : BaseVisibleObject<ISceneObject>, I2dLocateable
    {
        public PointItemBase()
        {

        }
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


        #region Public Methods - PointItemBase
        public virtual void ApplyConfig(IMapObjectBrushConfig config, IObjectBrushFilter filter, bool applyPosAndName = false)
        {
            if (applyPosAndName)
            {
                X = config.Pos.X;
                Y = config.Pos.Y;
            }
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
        public override int X { get; set; }
        public override int Y { get; set; }
        public override int Coord
        {
            get { return CoordInt(X, Y); }
            set
            {
                X = CoordIntX(value);
                Y = CoordIntY(value);
            }
        }
        public virtual string RegName { get { return string.Empty; } set { } }
        #endregion
    }
}
