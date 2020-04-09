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
        public PresentStructure(StructureItem item, int z, bool vxlTurret) : base(item, z)
        {
            VoxelTurret = vxlTurret;
        }
        public PresentStructure(BaseNode node, int z, bool vxlTurret) : base(node, z)
        {
            IsBaseNode = true;
            VoxelTurret = vxlTurret;
        }
        #endregion


        #region Public Methods - PresentStructure
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
                SetColor(pSelf, color);
                SetColor(pActivateAnim, color);
                SetColor(pActivateAnim2, color);
                SetColor(pActivateAnim3, color);
                SetColor(pIdleAnim, color);
                SetColor(pSuperAnim, color);
                SetColor(pTurretAnim, color);
                SetColor(pTurretBarl, color);
            }
        }
        #endregion


        #region Private Methods - PresentStructure
        public void SetTransparency(Vec4 color)
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
        #endregion


        #region Public Calls - PresentStructure
        public int[] Pointers
        {
            get
            {
                return new int[] { pSelf, pActivateAnim, pActivateAnim2, pActivateAnim3, pBib, pIdleAnim,
                pSelfShadow, pActivateAnimShadow, pActivateAnim2Shadow, pActivateAnim3Shadow, pBibShadow, pIdleAnimShadow};
            }
        }
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
        #endregion
    }
}
