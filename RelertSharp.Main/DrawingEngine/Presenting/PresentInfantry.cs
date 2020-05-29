﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.MapStructure.Objects;

namespace RelertSharp.DrawingEngine.Presenting
{
    internal class PresentInfantry : PresentBase, IPresentBase
    {
        #region Ctor - PresentInfantry
        public PresentInfantry(InfantryItem inf, int height) : base(inf, height) { }
        #endregion


        #region Public Methods - PresentInfantry
        public void Dispose()
        {
            RemoveProp(pSelf, pSelfShadow);
        }
        public void SetTo(I3dLocateable cell, int subcell)
        {
            if (subcell != -1)
            {
                Vec3 pos = Vec3.ToVec3Iso(cell, subcell);
                SetLocation(pos, pSelf, pSelfShadow);
                X = cell.X;
                Y = cell.Y;
                Z = cell.Z;
            }
        }
        public void MoveTo(I3dLocateable cell)
        {
            Vec3 delta = GetDeltaDistant(cell);
            ShiftBy(delta, pSelf, pSelfShadow);
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
        #endregion
    }
}
