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
        public void SetColor(Vec4 color)
        {
            ColorVector = color;
            SetColor(pSelf, color);
            SetColor(pTurret, color);
            SetColor(pBarrel, color);
        }
        public void MultiplyColor(Vec4 color)
        {
            ColorVector *= color;
            SetColor(ColorVector);
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
