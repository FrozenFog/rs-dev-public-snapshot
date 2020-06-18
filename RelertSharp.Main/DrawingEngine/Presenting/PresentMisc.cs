﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;

namespace RelertSharp.DrawingEngine.Presenting
{
    public class PresentMisc : PresentBase, IPresentBase
    {
        #region Ctor - PresentMisc
        public PresentMisc(MapObjectType type, I2dLocateable xy, int z) : base(xy, z)
        {
            MiscType = type;
        }
        #endregion


        #region Public Methods - PresentMisc

        public void Dispose()
        {
            if (!Disposed)
            {
                RemoveProp(pSelf, pSelfShadow);
                foreach (int id in WaypointNums)
                {
                    RemoveProp(id);
                }
                Disposed = true;
            }
        }

        public override void MoveTo(I3dLocateable cell)
        {
            Vec3 delta = GetDeltaDistant(cell);
            ShiftBy(delta, pSelf, pSelfShadow);
            base.MoveTo(cell);
        }
        public override void ShiftBy(I3dLocateable delta)
        {
            ShiftBy(Vec3.ToVec3Iso(delta), pSelf, pSelfShadow);
            base.ShiftBy(delta);
        }
        public void SetColor(Vec4 color)
        {
            if (IsTiberiumOverlay) return;
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


        #region Private Methods - PresentMisc
        private void SetColorStrict(Vec4 color)
        {
            SetColor(pSelf, color);
        }
        #endregion


        #region Public Calls - PresentMisc
        public MapObjectType MiscType { get; private set; }
        public bool IsTiberiumOverlay { get; set; }
        public bool IsWall { get; set; }
        public bool IsMoveBlockingOverlay { get; set; }
        public bool IsRubble { get; set; }
        public bool IsValid { get { return pSelf != 0; } }
        public bool IsHiBridge { get; set; }
        public bool IsZeroVec { get; set; }
        public int SmgWidth { get; set; }
        public int SmgHeight { get; set; }
        public List<int> WaypointNums { get; private set; } = new List<int>();
        #endregion
    }
}
