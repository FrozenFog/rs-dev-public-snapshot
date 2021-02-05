using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Objects;

namespace RelertSharp.Engine.MapObjects
{
    internal sealed class MapInfantry : MapObjectBase, ISceneObject
    {
        #region Ctor - MapInfantry
        public MapInfantry(InfantryItem inf, int height) : base(inf, height)
        {
            SubCell = inf.SubCells;
        }
        #endregion


        #region Public Methods - MapInfantry
        public void Dispose()
        {
            if (!Disposed)
            {
                RemoveProp(PresentFileTypeFlag.Shp, pSelf, pSelfShadow);
                Disposed = true;
            }
        }
        public void MoveTo(I3dLocateable cell, int subcell)
        {
            if (subcell > 0 && subcell < 4 && !(subcell == SubCell && CoordEquals(cell)))
            {
                Vec3 delta = GetDeltaDistant(cell, subcell, SubCell);
                ShiftBy(delta, pSelf, pSelfShadow);
                SubCell = subcell;
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
        public void Hide()
        {
            if (!IsHidden)
            {
                SetColorStrict(Vec4.HideCompletely);
                SetColor(pSelfShadow, Vec4.HideCompletely);
                IsHidden = true;
            }
        }
        public void Reveal()
        {
            if (IsHidden)
            {
                SetColorStrict(ColorVector);
                SetColor(pSelfShadow, Vec4.One);
                IsHidden = false;
            }
        }
        #endregion


        #region Private Methods - MapInfantry
        private void SetColorStrict(Vec4 color)
        {
            SetColor(pSelf, color);
        }
        #endregion


        #region Public Calls - MapInfantry
        public bool IsValid { get { return pSelf != 0; } }
        public int SubCell { get; set; }
        #endregion
    }
}
