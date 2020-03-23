using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.DrawingEngine
{
    internal class DrawableStructure
    {
        #region Ctor
        public DrawableStructure(string nameID)
        {
            NameID = nameID;
        }
        #endregion


        #region Public Calls
        public string NameID { get; set; }
        public int pSelf { get; set; }
        public int pActivateAnim { get; set; }
        public int pIdleAnim { get; set; }
        public int pActivateAnim2 { get; set; }
        public int pActivateAnim3 { get; set; }
        public int pTurretAnim { get; set; }
        public Pnt offsetTurret { get; set; }
        public int pBib { get; set; }
        public bool VoxelTurret { get; set; }
        #endregion
    }
}
