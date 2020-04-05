using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        #endregion


        #region Public Calls - PresentTile
        public int pExtra { get; set; }
        public bool IsValid { get { return pSelf != 0 || pExtra != 0; } }
        #endregion
    }
}
