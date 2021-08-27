using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;

namespace RelertSharp.Engine.MapObjects
{
    internal abstract class MapObjectBase : I3dLocateable
    {
        private bool lightLocked, hasTempColor;
        protected Vec4 tempColor = Vec4.One, colorVector = Vec4.One;
        protected enum PresentFileTypeFlag
        {
            Shp,
            Tmp,
            Vxl,
            CommonLine,
            CommonPicture
        }


        #region Ctor - MapObjectBase
        public MapObjectBase() { }
        public MapObjectBase(I2dLocateable obj, int height)
        {
            X = obj.X;
            Y = obj.Y;
            Z = height;
        }
        public MapObjectBase(I3dLocateable obj)
        {
            X = obj.X;
            Y = obj.Y;
            Z = obj.Z;
        }
        #endregion


        #region Public
        public virtual void Hide()
        {
            SetColorStrict(Vec4.HideCompletely);
            IsHidden = true;
        }
        public virtual void Reveal()
        {
            IsHidden = false;
            RefreshColor();
        }
        public virtual void ApplyTempColor(Vec4 color)
        {
            tempColor = color;
            hasTempColor = true;
            if (!IsHidden) RefreshColor();
        }
        public virtual void RemoveTempColor()
        {
            hasTempColor = false;
            tempColor = colorVector;
            if (!IsHidden) RefreshColor();
        }
        public virtual void LockLight()
        {
            lightLocked = true;
        }
        public virtual void UnlockLight()
        {
            lightLocked = false;
            RefreshColor();
        }
        public void MultiplyColor(Vec4 color)
        {
            colorVector *= color;
            SetColor(ColorVector);
        }
        public void DivColor(Vec4 color)
        {
            colorVector /= color;
            SetColor(ColorVector);
        }
        public void AddColor(Vec4 color)
        {
            colorVector += color;
            SetColor(ColorVector);
        }
        public virtual void SetColor(Vec4 color)
        {
            colorVector = color;
            if (!IsColorLocked)
            {
                RefreshColor();
            }
        }
        #endregion


        #region Protected - MapObjectBase
        /// <summary>
        /// SetColorStrict : ColorVector
        /// </summary>
        protected virtual void RefreshColor()
        {
            SetColorStrict(ColorVector);
        }
        protected abstract void SetColorStrict(Vec4 color);
        protected virtual void RemoveProp(PresentFileTypeFlag flag, int pself, int pextra = 0)
        {
            switch (flag)
            {
                case PresentFileTypeFlag.Shp:
                    if (pself != 0) CppExtern.ObjectUtils.RemoveShpFromScene(pself);
                    if (pextra != 0) CppExtern.ObjectUtils.RemoveShpFromScene(pextra);
                    break;
                case PresentFileTypeFlag.Vxl:
                    if (pself != 0) CppExtern.ObjectUtils.RemoveVxlFromScene(pself);
                    if (pextra != 0) CppExtern.ObjectUtils.RemoveVxlFromScene(pextra);
                    break;
                case PresentFileTypeFlag.Tmp:
                    if (pself != 0) CppExtern.ObjectUtils.RemoveTmpFromScene(pself);
                    if (pextra != 0) CppExtern.ObjectUtils.RemoveTmpFromScene(pextra);
                    break;
                case PresentFileTypeFlag.CommonLine:
                    if (pself != 0) CppExtern.ObjectUtils.RemoveCommonFromScene(pself);
                    if (pextra != 0) CppExtern.ObjectUtils.RemoveCommonFromScene(pextra);
                    break;
                case PresentFileTypeFlag.CommonPicture:
                    if (pself != 0) CppExtern.ObjectUtils.RemoveCommonTextureFromScene(pself);
                    if (pextra != 0) CppExtern.ObjectUtils.RemoveCommonTextureFromScene(pextra);
                    break;
            }
        }
        protected virtual void SetColor(int pointer, Vec4 color)
        {
            if (pointer != 0) CppExtern.ObjectUtils.SetObjectColorCoefficient(pointer, color);
        }
        protected virtual void ShiftBy(Vec3 displacement, int ptr, int pshadow = 0)
        {
            if (ptr != 0) CppExtern.ObjectUtils.MoveObject(ptr, displacement);
            if (pshadow != 0) CppExtern.ObjectUtils.MoveObject(pshadow, displacement);
        }
        protected virtual void SetLocation(Vec3 pos, int ptr, int pshadow = 0)
        {
            if (ptr != 0) CppExtern.ObjectUtils.SetObjectLocation(ptr, pos);
            if (pshadow != 0) CppExtern.ObjectUtils.SetObjectLocation(pshadow, pos);
        }
        public virtual void MoveTo(I3dLocateable pos, int subcell = -1)
        {
            X = pos.X;
            Y = pos.Y;
            Z = pos.Z;
        }
        public virtual void ShiftBy(I3dLocateable delta)
        {
            X += delta.X;
            Y += delta.Y;
            Z += delta.Z;
        }
        public virtual bool CoordEquals(I3dLocateable pos)
        {
            return X == pos.X && Y == pos.Y && Z == pos.Z;
        }
        protected Vec3 GetDeltaDistant(I3dLocateable newpos)
        {
            Vec3 delta = Vec3.ToVec3Iso(Vec3.FromXYZ(newpos) - Vec3.FromXYZ(this));
            X = newpos.X;
            Y = newpos.Y;
            Z = newpos.Z;
            return delta;
        }
        protected Vec3 GetDeltaDistant(I3dLocateable pos, int subcell, int orgSubcell)
        {
            Vec3 delta = Vec3.ToVec3Iso(pos, subcell) - Vec3.ToVec3Iso(this, orgSubcell);
            X = pos.X;
            Y = pos.Y;
            Z = pos.Z;
            return delta;
        }
        #endregion


        #region Public Calls - MapObjectBase
        public virtual bool IsHidden { get; protected set; }
        public bool IsColorLocked { get { return lightLocked || IsHidden; } }
        public int ID { get; set; }
        public Vec4 ActualColor { get { return colorVector; } }
        public Vec4 ColorVector
        {
            get
            {
                if (hasTempColor) return tempColor;
                else return colorVector;
            }
            set
            {
                if (hasTempColor) tempColor = value;
                else colorVector = value;
            }
        }
        public int Coord { get { return Utils.Misc.CoordInt(X, Y); } }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int pSelf { get; set; }
        public int pSelfShadow { get; set; }
        public bool Disposed { get; set; }
        public RadarColor RadarColor { get; set; }
        #endregion
    }
}
