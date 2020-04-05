using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RelertSharp.DrawingEngine
{
    internal class GdipSurface
    {
        private Bitmap bmp;
        private Graphics ds;


        #region Ctor - GdipSurface
        public GdipSurface(int w, int h)
        {
            InitSurface(w, h);
        }
        #endregion


        #region Public Methods - GdipSurface
        public void InitSurface(int w, int h)
        {
            bmp = new Bitmap(w, h);
            ds = Graphics.FromImage(bmp);
        }
        #endregion
    }
}
