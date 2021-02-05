using RelertSharp.Common;
using System.Drawing;

namespace RelertSharp.Engine.DrawableBuffer
{
    internal class DrawableStructure : DrawableBase, IDrawableBase
    {
        #region Ctor
        public DrawableStructure(string nameID) : base(nameID) { }
        #endregion


        #region Public Calls
        public Color MinimapColor { get; set; } = Color.FromArgb(0x00000000);
        public int Height { get; set; }
        public int FoundationX { get; set; }
        public int FoundationY { get; set; }
        public int pActivateAnim { get; set; }
        public int pShadowActivateAnim { get; set; }
        public short ActivateAnimCount { get; set; }
        public int pIdleAnim { get; set; }
        public int pShadowIdleAnim { get; set; }
        public short IdleAnimCount { get; set; }
        public int pActivateAnim2 { get; set; }
        public int pShadowActivateAnim2 { get; set; }
        public short ActivateAnim2Count { get; set; }
        public int pActivateAnim3 { get; set; }
        public int pShadowActivateAnim3 { get; set; }
        public short ActivateAnim3Count { get; set; }
        public int pSuperAnim { get; set; }
        public int pShadowSuperAnim { get; set; }
        public short SuperAnimCount { get; set; }
        public int pTurretAnim { get; set; }
        public int pShadowTurretAnim { get; set; }
        public short TurretAnimCount { get; set; }
        public int pTurretBarl { get; set; }
        public Pnt offsetTurret { get; set; }
        public int pBib { get; set; }
        public int pShadowBib { get; set; }
        public int pAlphaImg { get; set; }
        public short BibCount { get; set; }
        public bool VoxelTurret { get; set; }
        public bool FlatSelf { get { return pTurretAnim != 0; } }
        public bool IsValid { get { return pSelf != 0 || pActivateAnim != 0 || pBib != 0 || pIdleAnim != 0; } }
        public int TurretZAdjust { get; set; }
        public int ActivateZAdjust { get; set; }
        public int Activate2ZAdjust { get; set; }
        public int Activate3ZAdjust { get; set; }
        public int SuperZAdjust { get; set; }
        public int IdleZAdjust { get; set; }
        #endregion
    }
}
