using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.Engine.DrawableBuffer;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;

namespace RelertSharp.Engine.MapObjects
{
    internal sealed class MapBuilding : MapObjectBase, ISceneObject
    {
        #region Ctor
        internal MapBuilding(StructureItem item, int z, bool vxlTurret, DrawableStructure src) : base(item, z)
        {
            VoxelTurret = vxlTurret;
            FoundationX = src.FoundationX;
            FoundationY = src.FoundationY;
        }
        public MapBuilding(BaseNode node, int z, bool vxlTurret) : base(node, z)
        {
            IsBaseNode = true;
            VoxelTurret = vxlTurret;
        }
        #endregion


        #region Public Methods - PresentStructure
        public override void MoveTo(I3dLocateable cell, int subcell = -1)
        {
            Vec3 delta = GetDeltaDistant(cell);
            foreach (int p in Pointers) ShiftBy(delta, p);
            base.MoveTo(cell);
        }
        public override void ShiftBy(I3dLocateable delta)
        {
            Vec3 distant = Vec3.ToVec3Iso(delta);
            foreach (int p in Pointers) ShiftBy(distant, p);
            base.ShiftBy(delta);
        }
        public void SetTransparency(bool isTransparency)
        {
            if (isTransparency) SetTransparency(Vec4.Transparency);
            else SetTransparency(Vec4.DeTransparency);
        }
        public void Dispose()
        {
            if (!Disposed)
            {
                if (VoxelTurret)
                {
                    RemoveProp(PresentFileTypeFlag.Vxl, pTurretAnim, pTurretBarl);
                    RemoveProp(PresentFileTypeFlag.Vxl, pTurretAnimShadow, pTurretBarlShadow);
                    pTurretAnim = 0;
                    pTurretBarl = 0;
                    pTurretAnimShadow = 0; pTurretBarlShadow = 0;
                }
                foreach (int p in Pointers) RemoveProp(PresentFileTypeFlag.Shp, p);
                Disposed = true;
            }
        }
        public void SetColor(Vec4 color)
        {
            if (!IsBaseNode)
            {
                ColorVector = color;
                if (!selected)
                {
                    SetColorStrict(ColorVector);
                }
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
                foreach (int p in Pointers) SetColor(Vec4.HideCompletely);
                IsHidden = true;
            }
        }
        public void Reveal()
        {
            if (IsHidden)
            {
                SetColorStrict(ColorVector);
                foreach (int p in Shadows) SetColor(Vec4.One);
                SetColor(pAlphaImg, Vec4.One);
                IsHidden = false;
            }
        }
        #endregion


        #region Private Methods - PresentStructure
        private void SetTransparency(Vec4 color)
        {
            foreach (int p in Pointers) CppExtern.ObjectUtils.SetObjectColorCoefficient(p, color);
            if (VoxelTurret)
            {
                if (color.V < 1) color.V /= 2;
                else color.V *= 2;
                CppExtern.ObjectUtils.SetObjectColorCoefficient(pTurretAnim, color);
                if (pTurretBarl != 0) CppExtern.ObjectUtils.SetObjectColorCoefficient(pTurretBarl, color);
            }
            else
            {
                CppExtern.ObjectUtils.SetObjectColorCoefficient(pTurretAnim, color);
            }
        }
        protected override void SetColorStrict(Vec4 color)
        {
            SetColor(pSelf, color);
            SetColor(pActivateAnim, color);
            SetColor(pActivateAnim2, color);
            SetColor(pActivateAnim3, color);
            SetColor(pIdleAnim, color);
            SetColor(pSuperAnim, color);
            SetColor(pTurretAnim, color);
            SetColor(pTurretBarl, color);
            SetColor(pBib, color);
            SetColor(pPlug1, color);
            SetColor(pPlug2, color);
            SetColor(pPlug3, color);
        }
        #endregion


        #region Internal
        internal void SetShadowZAdjust()
        {
            foreach (int pshadow in Shadows) if (pshadow != 0) CppExtern.ObjectUtils.SetObjectZAdjust(pshadow, Constant.DrawingEngine.ZAdjust.Shadow);
        }
        internal void SetBasenodeZ()
        {
            foreach (int p in Pointers.Except(Shadows)) if (p != 0) CppExtern.ObjectUtils.SetObjectZAdjust(p, -4);
            CppExtern.ObjectUtils.SetObjectZAdjust(pTurretAnim, -5);
        }
        #endregion


        #region Public Calls - PresentStructure
        public int[] Pointers
        {
            get
            {
                return new int[] { pSelf, pActivateAnim, pActivateAnim2, pActivateAnim3, pBib, pIdleAnim, pSuperAnim, pTurretAnim, pTurretBarl,
                pSelfShadow, pActivateAnimShadow, pActivateAnim2Shadow, pActivateAnim3Shadow, pBibShadow, pIdleAnimShadow, pSuperAnimShadow, pTurretAnimShadow, pTurretBarlShadow,
                pPlug1, pPlug1Shadow, pPlug2, pPlug2Shadow, pPlug3, pPlug3Shadow,
                pAlphaImg};
            }
        }
        public int[] Shadows
        {
            get
            {
                return new int[] {pSelfShadow, pActivateAnimShadow,pActivateAnim2Shadow,pActivateAnim3Shadow,pBibShadow,pIdleAnimShadow,pSuperAnimShadow,pTurretAnimShadow, pTurretBarlShadow,
                pPlug1Shadow,pPlug2Shadow,pPlug3Shadow };
            }
        }
        public int pPlug1 { get; set; }
        public int pPlug2 { get; set; }
        public int pPlug3 { get; set; }
        public int pPlug1Shadow { get; set; }
        public int pPlug2Shadow { get; set; }
        public int pPlug3Shadow { get; set; }
        public int pActivateAnim { get; set; }
        public int pActivateAnimShadow { get; set; }
        public int pIdleAnim { get; set; }
        public int pIdleAnimShadow { get; set; }
        public int pActivateAnim2 { get; set; }
        public int pActivateAnim2Shadow { get; set; }
        public int pActivateAnim3 { get; set; }
        public int pActivateAnim3Shadow { get; set; }
        public int pSuperAnim { get; set; }
        public int pSuperAnimShadow { get; set; }
        public int pTurretAnim { get; set; }
        public int pTurretAnimShadow { get; set; }
        public int pTurretBarl { get; set; }
        public int pTurretBarlShadow { get; set; }
        public int pBib { get; set; }
        public int pBibShadow { get; set; }
        public int pAlphaImg { get; set; }
        public bool VoxelTurret { get; set; }
        public bool IsValid { get { return !((pSelf | pActivateAnim | pActivateAnim2 | pActivateAnim3 | pBib | pTurretAnim | pIdleAnim) == 0); } }
        public bool IsBaseNode { get; set; }
        public int FoundationX { get; private set; }
        public int FoundationY { get; private set; }
        #endregion
    }
}
