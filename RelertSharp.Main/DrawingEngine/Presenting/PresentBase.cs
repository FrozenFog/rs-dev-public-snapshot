using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure.Objects;

namespace RelertSharp.DrawingEngine.Presenting
{
    public class PresentBase
    {
        #region Ctor - PresentBase
        public PresentBase() { }
        public PresentBase(ObjectItemBase obj, int height)
        {
            X = obj.X;
            Y = obj.Y;
            Z = height;
        }
        #endregion


        #region Protected - PresentBase
        protected virtual void RemoveProp(int pointer)
        {
            if (pointer != 0) CppExtern.ObjectUtils.RemoveObjectAtScene(pointer);
        }
        #endregion


        #region Public Calls - PresentBase
        public int Coord { get { return Utils.Misc.CoordInt(X, Y); } }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int pSelf { get; set; }
        #endregion
    }


    public interface IPresentBase
    {
        int X { get; set; }
        int Y { get; set; }
        int Z { get; set; }
        int pSelf { get; set; }
        bool IsValid { get; }
        void Dispose();
    }
}
