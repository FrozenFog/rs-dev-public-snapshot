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
        protected bool isSelected = false;
        protected bool isHidden = false;

        public virtual int GetHeight(Map source = null)
        {
            if (source != null) return source.GetHeightFromTile(this);
            else if (GlobalVar.HasMap) return GlobalVar.GlobalMap.GetHeightFromTile(this);
            else return Constant.MapStructure.INVALID_HEIGHT;
        }
        public virtual void Select(bool forceSelect = false)
        {
            if (forceSelect || !isSelected)
            {
                SceneObject?.ApplyTempColor(Vec4.Selector);
                isSelected = true;
            }
        }
        public virtual void CancelSelection()
        {
            if (isSelected)
            {
                SceneObject?.RemoveTempColor();
                isSelected = false;
            }
        }
        public virtual void Hide(bool forceHide = false)
        {
            if (forceHide || !isHidden)
            {
                SceneObject?.Hide();
                isHidden = true;
            }
        }
        public virtual void Reveal()
        {
            if (isHidden)
            {
                SceneObject?.Reveal();
                if (isSelected) Select(true);
                isHidden = false;
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
        public abstract int X { get; set; }
        public abstract int Y { get; set; }
        public virtual MapObjectType ObjectType { get; protected set; } 
        public virtual bool IsSelected { get { return isSelected; } }
        public virtual bool IsHidden { get { return isHidden; } }
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
