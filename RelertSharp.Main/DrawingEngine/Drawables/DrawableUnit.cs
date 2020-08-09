using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;

namespace RelertSharp.DrawingEngine.Drawables
{
    public class DrawableUnit : DrawableBase, IDrawableBase
    {
        public DrawableUnit(string nameid) : base(nameid) { }


        #region Public Calls - DrawableUnit
        public int pTurret { get; set; }
        public int pBarrel { get; set; }
        public Vec3 TurretOffset { get; set; }
        public bool IsVxl { get; set; } = true;
        public bool IsEmpty { get { return pSelf == 0 && pTurret == 0; } }
        #endregion
    }
}
