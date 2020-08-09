﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.DrawingEngine.Drawables
{
    internal class DrawableInfantry : DrawableBase, IDrawableBase
    {
        #region Ctor - DrawableInfantry
        public DrawableInfantry() { }
        #endregion


        #region Public Calls - DrawableInfantry
        public bool IsEmpty { get { return pSelf == 0 && pShadow == 0; } }
        #endregion
    }
}
