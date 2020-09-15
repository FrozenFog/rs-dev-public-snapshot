using RelertSharp.Common;
using System.Drawing;

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
        public bool IsWall { get; set; }
        public int SmudgeWidth { get; set; }
        public int SmudgeHeight { get; set; }
        public Color RadarColor { get; set; } = Color.FromArgb(0, 0, 0, 0);//null color
        #endregion
    }
}
