﻿using RelertSharp.Common;
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
        public virtual void AddObject(T item)
        {
            if (string.IsNullOrEmpty(item.Id))
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



    public class PointItemBase : IndexableItem, I2dLocateable
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
        public PointItemBase(PointItemBase src)
        {
            X = src.X;
            Y = src.Y;
            SceneObject = src.SceneObject;
        }


        #region Public Methods - PointItemBase
        public void MoveTo(I3dLocateable pos, int subcell = -1)
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
        public virtual void Select()
        {
            if (!Selected)
            {
                Selected = true;
                SceneObject.ApplyTempColor(Vec4.Selector);
            }
        }
        public virtual void CancelSelection()
        {
            if (Selected)
            {
                Selected = false;
                SceneObject.RemoveTempColor();
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
        public virtual int GetHeight(Map source = null)
        {
            if (source != null) return source.GetHeightFromTile(this);
            else if (GlobalVar.HasMap) return GlobalVar.GlobalMap.GetHeightFromTile(this);
            return Constant.MapStructure.INVALID_HEIGHT;
        }
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
        public virtual string RegName { get { return string.Empty; } set { } }
        public virtual MapObjectType ObjectType { get; protected set; } = MapObjectType.Undefined;
        public virtual ISceneObject SceneObject { get; set; }
        #endregion
    }
}
