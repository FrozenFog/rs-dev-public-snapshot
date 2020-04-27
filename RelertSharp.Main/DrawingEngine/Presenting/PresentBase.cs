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
        #endregion


        #region Public Calls - PresentBase
        public Vec4 ColorVector { get; set; } = Vec4.One;
        public int Coord { get { return Utils.Misc.CoordInt(X, Y); } }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int pSelf { get; set; }
        public int pSelfShadow { get; set; }
        #endregion
    }


    public interface IPresentBase
    {
        int X { get; set; }
        int Y { get; set; }
        int Z { get; set; }
        int pSelf { get; set; }
        int pSelfShadow { get; set; }
        int Coord { get; }
        bool IsValid { get; }
        void Dispose();
        void SetColor(Vec4 color);
        void MultiplyColor(Vec4 color);
    }
}
