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
            SubCell = inf.SubCell;
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
        public override void MoveTo(I3dLocateable cell, int subcell)
        {
            if (subcell >= 2 && subcell <= 4 && !(subcell == SubCell && CoordEquals(cell)))
            {
                Vec3 delta = GetDeltaDistant(cell, subcell, SubCell);
                ShiftBy(delta, pSelf, pSelfShadow);
                SubCell = subcell;
                base.MoveTo(cell);
            }
        }
        public override void ShiftBy(I3dLocateable delta)
        {
            ShiftBy(Vec3.ToVec3Iso(delta), pSelf, pSelfShadow);
            base.ShiftBy(delta);
        }
        public override void Hide()
        {
            base.Hide();
            SetColor(pSelfShadow, Vec4.HideCompletely);
        }
        public override void Reveal()
        {
            base.Reveal();
            SetColor(pSelfShadow, Vec4.One);
        }
        #endregion


        #region Private Methods - MapInfantry
        protected override void SetColorStrict(Vec4 color)
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
