using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
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
        public bool IsHiBridge { get; set; }
        public bool IsOffsetBridge { get; set; }
        public MapObjectType MiscType { get; private set; }
        public ShpFlatType FlatType { get; set; }
        public int pPal { get; set; }
        public bool IsZeroVec { get; set; }
        public bool IsFlatOnly { get; set; }
        public bool IsRubble { get; set; }
        public bool IsTiberiumOverlay { get; set; }
        public bool IsMoveBlockingOverlay { get; set; }
        public Color RadarColor { get; set; } = Color.FromArgb(0, 0, 0, 0);//null color
        #endregion
    }
}
