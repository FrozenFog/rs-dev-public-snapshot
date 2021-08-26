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
        public virtual void Select(bool forceSelect = false)
        {
            if ((forceSelect || !isSelected) && CanSelect)
            {
                SceneObject?.ApplyTempColor(Vec4.Selector);
                isSelected = true;
                status |= ObjectStatus.Selected;
                UpdateStatusColor();
            }
        }
        public virtual void CancelSelection()
        {
            if (isSelected)
            {
                SceneObject?.RemoveTempColor();
                isSelected = false;
                status &= ~ObjectStatus.Selected;
                UpdateStatusColor();
            }
        }
        public virtual void PhaseOut(bool forcePhase = false)
        {
            if (forcePhase || !isPhased)
            {
                SceneObject.PhaseOut();
                isPhased = true;
                status |= ObjectStatus.Phased;
                UpdateStatusColor();
            }
        }
        public virtual void UnPhase()
        {
            if (isPhased)
            {
                SceneObject?.UnPhase();
                isPhased = false;
                status &= ~ObjectStatus.Phased;
                UpdateStatusColor();
            }
        }
        protected virtual void UpdateStatusColor()
        {
            bool phase = IsPhased && !isSelected && !IsHidden;
            bool select = isSelected && !IsPhased && !IsHidden;
            bool hide = IsHidden;
            if (hide) SceneObject?.Hide();
            else SceneObject?.Reveal();
            if (phase) SceneObject?.PhaseOut();
            else if (!hide) SceneObject?.UnPhase();
            if (select) SceneObject?.ApplyTempColor(Vec4.Selector);
            else if (!phase && !hide) SceneObject?.RemoveTempColor();
        }
        public virtual void Hide(bool forceHide = false)
        {
            if (forceHide || !isHidden)
            {
                SceneObject?.Hide();
                isHidden = true;
                status |= ObjectStatus.Hide;
                UpdateStatusColor();
            }
        }
        public virtual void Reveal()
        {
            if (isHidden)
            {
                SceneObject?.Reveal();
                if (isSelected) Select(true);
                isHidden = false;
                status &= ~ObjectStatus.Hide;
                UpdateStatusColor();
            }
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
        }

        public virtual TSceneInterface SceneObject { get; set; }
        public virtual bool CanSelect
        {
            get
            {
                return !IsHidden && !IsPhased;
            }
        }
        public abstract int X { get; set; }
        public abstract int Y { get; set; }
        public virtual MapObjectType ObjectType { get; protected set; } 
        public virtual bool IsSelected { get { return isSelected; } }
        public virtual bool IsHidden { get { return isHidden; } }
        public virtual bool IsPhased { get { return isPhased; } }
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
