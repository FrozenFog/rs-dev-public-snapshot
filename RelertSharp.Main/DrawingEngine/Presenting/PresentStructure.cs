using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;
using RelertSharp.Common;

namespace RelertSharp.DrawingEngine.Presenting
{
    internal class PresentStructure : PresentBase, IPresentBase
    {
        #region Ctor
        public PresentStructure(StructureItem item, int z, bool vxlTurret, Drawables.DrawableStructure src) : base(item, z)
        {
            VoxelTurret = vxlTurret;
            FoundationX = src.FoundationX;
            FoundationY = src.FoundationY;
        }
        public PresentStructure(BaseNode node, int z, bool vxlTurret) : base(node, z)
        {
            IsBaseNode = true;
            VoxelTurret = vxlTurret;
        }
        #endregion


        #region Public Methods - PresentStructure
        public override void MoveTo(I3dLocateable cell)
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
            foreach (int p in Pointers) RemoveProp(p);
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
        private void SetColorStrict(Vec4 color)
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
        }
        #endregion


        #region Public Calls - PresentStructure
        public int[] Pointers
        {
            get
            {
                return new int[] { pSelf, pActivateAnim, pActivateAnim2, pActivateAnim3, pBib, pIdleAnim, pSuperAnim, pTurretAnim, pTurretBarl,
                pSelfShadow, pActivateAnimShadow, pActivateAnim2Shadow, pActivateAnim3Shadow, pBibShadow, pIdleAnimShadow, pSuperAnimShadow, pTurretAnimShadow,
                pPlug1, pPlug1Shadow, pPlug2, pPlug2Shadow, pPlug3, pPlug3Shadow};
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
        public int pBib { get; set; }
        public int pBibShadow { get; set; }
        public bool VoxelTurret { get; set; }
        public bool IsValid { get { return !((pSelf | pActivateAnim | pActivateAnim2 | pActivateAnim3 | pBib | pTurretAnim | pIdleAnim) == 0); } }
        public bool IsBaseNode { get; set; }
        public int FoundationX { get; private set; }
        public int FoundationY { get; private set; }
        #endregion
    }
}
