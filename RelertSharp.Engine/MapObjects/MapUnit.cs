using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.Engine.DrawableBuffer;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Objects;

namespace RelertSharp.Engine.MapObjects
{
    internal sealed class MapUnit : MapObjectBase, ISceneObject
    {
        #region Ctor - MapUnit
        public MapUnit(UnitItem unit, int height, bool vxl) : base(unit, height)
        {
            IsVxl = vxl;
        }
        public MapUnit(DrawableUnit src, AircraftItem air, int height) : base(air, height)
        {
            IsVxl = src.IsVxl;
        }
        #endregion


        #region Public Methods - MapUnit
        public void Dispose()
        {
            if (!Disposed)
            {
                if (IsVxl) RemoveProp(PresentFileTypeFlag.Vxl, pSelf, pSelfShadow);
                else RemoveProp(PresentFileTypeFlag.Shp, pSelf);
                RemoveProp(PresentFileTypeFlag.Vxl, pBarrel, pBarrelShadow);
                RemoveProp(PresentFileTypeFlag.Vxl, pTurret, pTurretShadow);
                Disposed = true;
            }
        }
        public override void MoveTo(I3dLocateable cell, int subcell = -1)
        {
            Vec3 delta = GetDeltaDistant(cell);
            ShiftBy(delta, pSelf, pSelfShadow);
            ShiftBy(delta, pBarrel, pBarrelShadow);
            ShiftBy(delta, pTurret, pTurretShadow);
            base.MoveTo(cell);
        }
        public override void ShiftBy(I3dLocateable delta)
        {
            Vec3 distant = Vec3.ToVec3Iso(delta);
            ShiftBy(distant, pSelf, pSelfShadow);
            ShiftBy(distant, pBarrel, pBarrelShadow);
            ShiftBy(distant, pTurret, pTurretShadow);
            base.ShiftBy(delta);
        }
        public override void Hide()
        {
            base.Hide();
            SetShadow(Vec4.HideCompletely);
        }
        public override void Reveal()
        {
            base.Reveal();
            SetShadow(Vec4.One);
        }
        #endregion


        #region Internal
        internal void SetUnitShadowZAdjust()
        {
            float zAdjust = Constant.DrawingEngine.ZAdjust.Shadow;
            CppExtern.ObjectUtils.SetObjectZAdjust(pSelfShadow, zAdjust);
            CppExtern.ObjectUtils.SetObjectZAdjust(pTurretShadow, zAdjust);
            CppExtern.ObjectUtils.SetObjectZAdjust(pBarrelShadow, zAdjust);
            CppExtern.ObjectUtils.SetObjectZAdjust(pSelf, Constant.DrawingEngine.ZAdjust.VxlSelf);
            CppExtern.ObjectUtils.SetObjectZAdjust(pTurret, Constant.DrawingEngine.ZAdjust.VxlTurret);
            CppExtern.ObjectUtils.SetObjectZAdjust(pBarrel, Constant.DrawingEngine.ZAdjust.VxlBarrel);
        }
        #endregion


        #region Private Methods - MapUnit
        protected override void SetColorStrict(Vec4 color)
        {
            SetColor(pSelf, color);
            SetColor(pTurret, color);
            SetColor(pBarrel, color);
        }
        private void SetShadow(Vec4 color)
        {
            SetColor(pSelfShadow, color);
            SetColor(pTurretShadow, color);
            SetColor(pBarrelShadow, color);
        }
        #endregion


        #region Public Calls - MapUnit
        public int pBarrel { get; set; }
        public int pBarrelShadow { get; set; }
        public int pTurret { get; set; }
        public int pTurretShadow { get; set; }
        public bool IsValid { get { return !((pSelf | pBarrel | pTurret) == 0); } }
        public bool IsVxl { get; private set; }
        #endregion
    }
}
