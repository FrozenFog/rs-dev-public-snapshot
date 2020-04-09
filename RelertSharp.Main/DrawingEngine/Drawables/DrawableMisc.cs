using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;

namespace RelertSharp.DrawingEngine.Drawables
{
    internal class DrawableMisc : DrawableBase
    {
        #region Ctor - DrawableMisc
        public DrawableMisc(MapObjectType type, string nameid) : base(nameid)
        {
            MiscType = type;
        }
        #endregion


        #region Public Calls - DrawableMisc
        public MapObjectType MiscType { get; private set; }
        public ShpFlatType FlatType { get; set; }
        public int pPal { get; set; }
        public bool IsZeroVec { get; set; }
        public bool IsTiberiumOverlay { get; set; }
        #endregion
    }
}
