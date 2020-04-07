using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;

namespace RelertSharp.DrawingEngine.Presenting
{
    internal class PresentTile : PresentBase, IPresentBase
    {
        #region Ctor - PresentTile
        public PresentTile(int pself, int pextra)
        {
            pSelf = pself;
            pExtra = pextra;
        }
        #endregion


        #region Public Methods - PresentTile
        public void Dispose()
        {
            RemoveProp(pSelf);
            RemoveProp(pExtra);
        }
        public void SetColor(Vec4 color)
        {
            SetColor(pSelf, color);
            SetColor(pExtra, color);
        }
        #endregion


        #region Public Calls - PresentTile
        public int pExtra { get; set; }
        public bool IsValid { get { return pSelf != 0 || pExtra != 0; } }
        #endregion
    }
}
