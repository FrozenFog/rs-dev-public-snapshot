using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure.Objects;

namespace RelertSharp.DrawingEngine.Presenting
{
    internal class PresentInfantry : PresentBase, IPresentBase
    {
        #region Ctor - PresentInfantry
        public PresentInfantry(InfantryItem inf, int height) : base(inf, height) { }
        #endregion


        #region Public Methods - PresentInfantry
        public void Dispose()
        {
            RemoveProp(pSelf);
        }
        #endregion


        #region Public Calls - PresentInfantry
        public bool IsValid { get { return pSelf != 0; } }
        #endregion
    }
}
