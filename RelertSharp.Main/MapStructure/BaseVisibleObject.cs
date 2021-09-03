using RelertSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.MapStructure
{
    public abstract class BaseVisibleObject<TSceneInterface> : IndexableItem, I2dLocateable where TSceneInterface : ISceneObject
    {
        [Flags]
        protected enum ObjectStatus
        {
            Normal = 0,
            Selected = 1,
            Phased = 1 << 1,
            Hide = 1 << 2,
            TileExtraHide = 1 << 3,
            Lighted = 1 << 4
        }
        protected ObjectStatus status = ObjectStatus.Normal;
        protected bool isSelected = false;
        protected bool isHidden = false;
        protected bool isPhased = false;

        public virtual int GetHeight(Map source = null)
        {
            if (source != null) return source.GetHeightFromTile(this);
            else if (GlobalVar.HasMap) return GlobalVar.GlobalMap.GetHeightFromTile(this);
            else return Constant.MapStructure.INVALID_HEIGHT;
        }
        public virtual void MoveTo(I3dLocateable pos, int subcell = -1)
        {
            X = pos.X;
            Y = pos.Y;
            SceneObject?.MoveTo(pos, subcell);
        }
        public virtual void ShiftBy(I3dLocateable delta)
        {
            X += delta.X;
            Y += delta.Y;
            SceneObject?.ShiftBy(delta);
        }
        public virtual void Dispose()
        {
            isSelected = false;
            isHidden = false;
            SceneObject?.Dispose();
            Disposed = true;
        }
        #region Colors
        public virtual void Select()
        {
            if (!isSelected && CanSelect)
            {
                SceneObject?.ApplyTempColor(Vec4.Selector);
                isSelected = true;
                status |= ObjectStatus.Selected;
            }
        }
        public virtual void CancelSelection()
        {
            if (isSelected)
            {
                SceneObject?.RemoveTempColor();
                isSelected = false;
                status &= ~ObjectStatus.Selected;
            }
        }
        public virtual void PhaseOut()
        {
            if (!isPhased && !isHidden)
            {
                SceneObject.ApplyTempColor(Vec4.Hide75);
                isPhased = true;
                status |= ObjectStatus.Phased;
            }
        }
        public virtual void UnPhase()
        {
            if (isPhased)
            {
                SceneObject?.RemoveTempColor();
                isPhased = false;
                status &= ~ObjectStatus.Phased;
            }
        }
        public virtual void Hide()
        {
            if (!isHidden)
            {
                SceneObject?.Hide();
                isHidden = true;
                status |= ObjectStatus.Hide;
            }
        }
        public virtual void Reveal()
        {
            if (isHidden && !Disposed)
            {
                SceneObject?.Reveal();
                isHidden = false;
                status &= ~ObjectStatus.Hide;
            }
        }
        #endregion
        private TSceneInterface _sceneobject;

        public virtual TSceneInterface SceneObject
        {
            get { return _sceneobject; }
            set
            {
                if (value != null)
                {
                    _sceneobject = value;
                    Disposed = false;
                }
                else Disposed = true;
            }
        }
        public virtual bool CanSelect
        {
            get
            {
                return !IsHidden && !IsPhased && !Disposed;
            }
        }
        public abstract int X { get; set; }
        public abstract int Y { get; set; }
        public virtual MapObjectType ObjectType { get; protected set; } 
        public virtual bool IsSelected { get { return isSelected; } }
        public virtual bool IsHidden { get { return isHidden; } }
        public virtual bool IsPhased { get { return isPhased; } }
        public bool Disposed { get; protected set; }
        public virtual int Coord
        {
            get { return Utils.Misc.CoordInt(X, Y); }
            set
            {
                X = Utils.Misc.CoordIntX(value);
                Y = Utils.Misc.CoordIntY(value);
            }
        }
    }
}
