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
        protected bool selected = false;
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
        public virtual void ApplyTempColor(Vec4 color)
        {
            SetColorStrict(color);
        }

        public virtual void RemoveTempColor()
        {
            SetColorStrict(ColorVector);
        }
        #endregion


        #region Protected - MapObjectBase
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
        public bool IsHidden { get; protected set; }
        public int ID { get; set; }
        public Vec4 ColorVector { get; set; } = Vec4.One;
        public int Coord { get { return Utils.Misc.CoordInt(X, Y); } }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int pSelf { get; set; }
        public int pSelfShadow { get; set; }
        public bool Selected { get { return selected; } set { selected = value; } }
        public bool Disposed { get; set; }
        public RadarColor RadarColor { get; set; }
        #endregion
    }
}
