using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure.Objects;

namespace RelertSharp.DrawingEngine
{
    internal class PresentStructure
    {
        #region Ctor
        public PresentStructure(StructureItem item, int z)
        {
            X = item.X;
            Y = item.Y;
            Z = z;
        }
        #endregion


        #region Public Calls - PresentStructure
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int pSelf { get; set; }
        public int pActivateAnim { get; set; }
        public int pIdleAnim { get; set; }
        public int pActivateAnim2 { get; set; }
        public int pActivateAnim3 { get; set; }
        public int pTurretAnim { get; set; }
        public int pBib { get; set; }
        public bool VoxelTurret { get; set; }
        #endregion
    }
}
