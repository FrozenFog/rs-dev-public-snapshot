using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.DrawingEngine.Drawables
{
    internal class DrawableStructure : DrawableBase, IDrawableBase
    {
        #region Ctor
        public DrawableStructure(string nameID) : base(nameID) { }
        #endregion


        #region Public Calls
        public int pActivateAnim { get; set; }
        public int pIdleAnim { get; set; }
        public int pActivateAnim2 { get; set; }
        public int pActivateAnim3 { get; set; }
        public int pTurretAnim { get; set; }
        public int pTurretBarl { get; set; }
        public Pnt offsetTurret { get; set; }
        public int pBib { get; set; }
        public bool VoxelTurret { get; set; }
        public bool FlatSelf { get { return pTurretAnim != 0; } }
        #endregion
    }
}
