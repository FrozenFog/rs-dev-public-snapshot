using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.MapStructure.Objects;

namespace RelertSharp.DrawingEngine.Presenting
{
    public class PresentInfantry : PresentBase, IPresentBase
    {
        #region Ctor - PresentInfantry
        public PresentInfantry(InfantryItem inf, int height) : base(inf, height)
        {
            SubCell = inf.SubCells;
        }
        #endregion


        #region Public Methods - PresentInfantry
        public void Dispose()
        {
            if (!Disposed)
            {
                RemoveProp(pSelf, pSelfShadow);
                Disposed = true;
            }
        }
        public void MoveTo(I3dLocateable cell, int subcell)
        {
            if (subcell >0 && subcell < 4 && subcell != SubCell)
            {
                Vec3 delta = GetDeltaDistant(cell, subcell, SubCell);
                ShiftBy(delta, pSelf, pSelfShadow);
                SubCell = subcell;
                CppExtern.Scene.PresentAllObject();
            }
        }
        public override void ShiftBy(I3dLocateable delta)
        {
            ShiftBy(Vec3.ToVec3Iso(delta), pSelf, pSelfShadow);
            base.ShiftBy(delta);
        }
        public void SetColor(Vec4 color)
        {
            ColorVector = color;
            if (!selected)
            {
                SetColorStrict(color);
            }
        }
        public void MultiplyColor(Vec4 color)
        {
            ColorVector *= color;
            SetColor(ColorVector);
        }
        public void MarkSelected()
        {
            SetColorStrict(Vec4.Selector);
            selected = true;
        }
        public void Unmark()
        {
            selected = false;
            SetColorStrict(ColorVector);
        }
        #endregion


        #region Private Methods - PresentInfantry
        private void SetColorStrict(Vec4 color)
        {
            SetColor(pSelf, color);
        }
        #endregion


        #region Public Calls - PresentInfantry
        public bool IsValid { get { return pSelf != 0; } }
        public int SubCell { get; set; }
        #endregion
    }
}
