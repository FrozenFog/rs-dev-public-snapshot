using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;

namespace RelertSharp.DrawingEngine.Presenting
{
    internal class PresentMisc : PresentBase, IPresentBase
    {
        #region Ctor - PresentMisc
        public PresentMisc(MapObjectType type, I2dLocateable xy, int z) : base(xy, z)
        {
            MiscType = type;
        }
        #endregion


        #region Public Methods - PresentMisc

        public void Dispose()
        {
            RemoveProp(pSelf, pSelfShadow);
        }

        public void SetColor(Vec4 color)
        {
            if (IsTiberiumOverlay) return;
            ColorVector = color;
            SetColor(pSelf, color);
        }
        public void MultiplyColor(Vec4 color)
        {
            ColorVector *= color;
            SetColor(ColorVector);
        }
        #endregion


        #region Public Calls - PresentMisc
        public MapObjectType MiscType { get; private set; }
        public bool IsTiberiumOverlay { get; set; }
        public bool IsValid { get { return pSelf != 0; } }
        public int pWpNum { get; set; }
        #endregion
    }
}
