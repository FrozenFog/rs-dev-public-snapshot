using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using RelertSharp.DrawingEngine.Drawables;
using RelertSharp.Common;

namespace RelertSharp.DrawingEngine
{
    internal class GdipSurface
    {
        private Graphics ds;
        private Rectangle mapsize;
        private Bitmap minimap;
        private Size panelSize;


        #region Ctor - GdipSurface
        public GdipSurface()
        {

        }
        #endregion


        #region Public Methods - GdipSurface
        public bool Initialize(Size panelsize, Rectangle mapsize)
        {
            try
            {
                this.mapsize = mapsize;
                panelSize = panelsize;
                minimap = new Bitmap(this.mapsize.Width * 2, this.mapsize.Height * 2);
                ds = Graphics.FromImage(minimap);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void DrawTile(DrawableTile t, I2dLocateable pos, int subindex)
        {
            To2dCoord(pos, out int x, out int y);
            if (subindex > t.Colors.Count - 1) subindex = 0;
            SetMinimapColorAt(x, y, t.Colors[subindex].Left);
            SetMinimapColorAt(x + 1, y, t.Colors[subindex].Right);
        }
        #endregion


        #region Private Methods - GdipSurface
        private void SetMinimapColorAt(int x,int y, Color color)
        {
            minimap.SetPixel(x, y, color);
        }
        private void To2dCoord(I2dLocateable pos, out int x, out int y)
        {
            x = pos.X - pos.Y + mapsize.Width - 1;
            y = pos.X + pos.Y - mapsize.Width - 1;
        }
        private Bitmap ResizeTo(Bitmap src, Size destSize)
        {
            Bitmap dest = new Bitmap(destSize.Width, destSize.Height);
            Graphics g = Graphics.FromImage(dest);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.DrawImage(src, new Rectangle(0, 0, destSize.Width, destSize.Height), new Rectangle(0, 0, src.Width, src.Height), GraphicsUnit.Pixel);
            g.Dispose();
            return dest;
        }
        #endregion


        #region Public Calls - GdipSurface
        public Bitmap MiniMap { get { return ResizeTo(minimap, panelSize); }  }
        #endregion
    }
}
