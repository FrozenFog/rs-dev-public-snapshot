﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.MapStructure.Objects;

namespace RelertSharp.DrawingEngine.Presenting
{
    public class PresentUnit : PresentBase, IPresentBase
    {
        #region Ctor - PresentUnit
        public PresentUnit(UnitItem unit, int height, bool vxl) : base(unit, height)
        {
            IsVxl = vxl;
        }
        public PresentUnit(AircraftItem air, int height) : base(air, height) { }
        #endregion


        #region Public Methods - PresentUnit
        public void Dispose()
        {
            RemoveProp(pBarrel);
            RemoveProp(pSelf);
            RemoveProp(pTurret);
        }
        public override void MoveTo(I3dLocateable cell)
        {
            Vec3 delta = GetDeltaDistant(cell);
            ShiftBy(delta, pSelf, pSelfShadow);
            ShiftBy(delta, pBarrel);
            ShiftBy(delta, pTurret);
            base.MoveTo(cell);
        }
        public override void ShiftBy(I3dLocateable delta)
        {
            Vec3 distant = Vec3.ToVec3Iso(delta);
            ShiftBy(distant, pSelf, pSelfShadow);
            ShiftBy(distant, pBarrel);
            ShiftBy(distant, pTurret);
            base.ShiftBy(delta);
        }
        public void SetColor(Vec4 color)
        {
            ColorVector = color;
            if (!selected)
            {
                SetColorStrict(ColorVector);
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


        #region Private Methods - PresentUnit
        private void SetColorStrict(Vec4 color)
        {
            SetColor(pSelf, color);
            SetColor(pTurret, color);
            SetColor(pBarrel, color);
        }
        #endregion


        #region Public Calls - PresentUnit
        public int pBarrel { get; set; }
        public int pTurret { get; set; }
        public bool IsValid { get { return !((pSelf | pBarrel | pTurret) == 0); } }
        public bool IsVxl { get; private set; }
        #endregion
    }
}
