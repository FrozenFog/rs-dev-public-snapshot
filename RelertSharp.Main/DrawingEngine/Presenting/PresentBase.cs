using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;
using RelertSharp.MapStructure;
using RelertSharp.Common;

namespace RelertSharp.DrawingEngine.Presenting
{
    public class PresentBase : I3dLocateable
    {
        protected bool selected = false;


        #region Ctor - PresentBase
        public PresentBase() { }
        public PresentBase(I2dLocateable obj, int height)
        {
            X = obj.X;
            Y = obj.Y;
            Z = height;
        }
        public PresentBase(I3dLocateable obj)
        {
            X = obj.X;
            Y = obj.Y;
            Z = obj.Z;
        }
        #endregion


        #region Protected - PresentBase
        protected virtual void RemoveProp(int pointer, int pshadow = 0)
        {
            if (pointer != 0) CppExtern.ObjectUtils.RemoveObjectFromScene(pointer);
            if (pshadow != 0) CppExtern.ObjectUtils.RemoveObjectFromScene(pshadow);
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
        public virtual void MoveTo(I3dLocateable pos)
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
        protected Vec3 GetDeltaDistant(I3dLocateable newpos)
        {
            Vec3 delta = Vec3.ToVec3Iso(Vec3.FromXYZ(newpos) - Vec3.FromXYZ(this));
            X = newpos.X;
            Y = newpos.Y;
            Z = newpos.Z;
            return delta;
        }
        #endregion


        #region Public Calls - PresentBase
        public int ID { get; set; }
        public Vec4 ColorVector { get; set; } = Vec4.One;
        public int Coord { get { return Utils.Misc.CoordInt(X, Y); } }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int pSelf { get; set; }
        public int pSelfShadow { get; set; }
        public bool Selected { get { return selected; } set { selected = value; } }
        public RadarColor RadarColor { get; set; }
        #endregion
    }


    public interface IPresentBase : I3dLocateable
    {
        int ID { get; set; }
        int pSelf { get; set; }
        int pSelfShadow { get; set; }
        bool IsValid { get; }
        bool Selected { get; set; }
        RadarColor RadarColor { get; set; }
        void Dispose();
        void SetColor(Vec4 color);
        void MultiplyColor(Vec4 color);
        void MarkSelected();
        void Unmark();
        void MoveTo(I3dLocateable pos);
        void ShiftBy(I3dLocateable delta);
    }
}
