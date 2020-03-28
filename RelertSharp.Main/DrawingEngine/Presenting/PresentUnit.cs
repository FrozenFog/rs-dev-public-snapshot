using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure.Objects;

namespace RelertSharp.DrawingEngine.Presenting
{
    public class PresentUnit : PresentBase, IPresentBase
    {
        public PresentUnit(UnitItem unit, int height) : base(unit, height) { }


        #region Public Calls - PresentUnit
        public int pBarrel { get; set; }
        public int pTurret { get; set; }
        public bool IsValid { get { return !((pSelf | pBarrel | pTurret) == 0); } }
        #endregion
    }
}
