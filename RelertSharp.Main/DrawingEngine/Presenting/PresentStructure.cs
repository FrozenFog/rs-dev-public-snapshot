using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure.Objects;

namespace RelertSharp.DrawingEngine.Presenting
{
    internal class PresentStructure : PresentBase, IPresentBase
    {
        #region Ctor
        public PresentStructure(StructureItem item, int z) : base(item, z) { }
        #endregion


        #region Public Calls - PresentStructure
        public int pActivateAnim { get; set; }
        public int pIdleAnim { get; set; }
        public int pActivateAnim2 { get; set; }
        public int pActivateAnim3 { get; set; }
        public int pTurretAnim { get; set; }
        public int pBib { get; set; }
        public bool VoxelTurret { get; set; }
        public bool IsValid { get { return !((pSelf | pActivateAnim | pActivateAnim2 | pActivateAnim3 | pBib | pTurretAnim | pIdleAnim) == 0); } }
        #endregion
    }
}
