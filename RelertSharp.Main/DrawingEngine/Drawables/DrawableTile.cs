using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RelertSharp.DrawingEngine.Drawables
{
    internal class DrawableTile : DrawableBase, IDrawableBase
    {
        #region Ctor - DrawableTile
        public DrawableTile() { }
        #endregion


        #region Public Calls - DrawableTile
        public List<ColorPair> Colors { get; private set; } = new List<ColorPair>();
        #endregion


        public class ColorPair
        {
            public ColorPair(Color left, Color right)
            {
                Left = left;
                Right = right;
            }
            public Color Left { get; private set; }
            public Color Right { get; private set; }
        }
    }
}
