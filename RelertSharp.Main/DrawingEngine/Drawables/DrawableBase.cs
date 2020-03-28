using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.DrawingEngine.Drawables
{
    public class DrawableBase
    {
        #region Ctor - DrawableBase
        public DrawableBase() { }
        public DrawableBase(string nameid)
        {
            NameID = nameid;
        }
        #endregion


        #region Public Calls - DrawableBase
        public string NameID { get; set; }
        public int pSelf { get; set; }
        public int pPalCustom { get; set; }
        #endregion
    }


    public interface IDrawableBase
    {
        string NameID { get; set; }
        int pSelf { get; set; }
        int pPalCustom { get; set; }
    }
}
