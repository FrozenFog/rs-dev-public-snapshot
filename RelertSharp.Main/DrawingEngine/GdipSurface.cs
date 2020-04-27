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
        private readonly Color nullcolor = Color.FromArgb(0, 0, 0, 0);
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
            if (subindex > t.SubTiles.Count - 1) subindex = 0;
            SetMinimapColorAt(x, y, t.SubTiles[subindex].RadarColor.Left);
            SetMinimapColorAt(x + 1, y, t.SubTiles[subindex].RadarColor.Right);
        }
        public void DrawStructure(DrawableStructure d, I2dLocateable pos)
        {
            foreach (I2dLocateable p in new Square2D(pos, d.FoundationX, d.FoundationY))
            {
                To2dCoord(p, out int x, out int y);
                SetMinimapColorAt(x, y, d.MinimapColor);
                SetMinimapColorAt(x + 1, y, d.MinimapColor);
            }
        }
        public void DrawMisc(DrawableMisc d, I2dLocateable pos)
        {
            To2dCoord(pos, out int x, out int y);
            if (d.RadarColor == nullcolor) return;
            SetMinimapColorAt(x, y, d.RadarColor);
            SetMinimapColorAt(x + 1, y, d.RadarColor);
        }
        public void DrawObject(IDrawableBase d, I2dLocateable pos)
        {
            To2dCoord(pos, out int x, out int y);
            Color c = Utils.Misc.ToColor(d.RemapColor);
            if (c == nullcolor) return;
            SetMinimapColorAt(x, y, c);
            SetMinimapColorAt(x + 1, y, c);
        }
        #endregion


        #region Private Methods - GdipSurface
        private void SetMinimapColorAt(int x,int y, Color color)
        {
            if (x < 0 || y < 0 || x >= minimap.Width || y >= minimap.Height) return;
            minimap.SetPixel(x, y, color);
        }
        private void To2dCoord(I2dLocateable pos, out int x, out int y)
        {
            x = pos.X - pos.Y + mapsize.Width - 1;
            y = pos.X + pos.Y - mapsize.Width - 1;
        }
        private Bitmap ResizeTo(Bitmap src, Size destSize)
        {
            float scale = Math.Min(destSize.Width / (float)src.Width, destSize.Height / (float)src.Height);
            Bitmap dest = new Bitmap((int)(src.Width*scale), (int)(src.Height*scale));
            Graphics g = Graphics.FromImage(dest);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.DrawImage(src, new Rectangle(0, 0, destSize.Width, destSize.Height), new Rectangle(0, 0, src.Width, src.Height), GraphicsUnit.Pixel);
            g.Dispose();
            src.Save("1.png");
            return dest;
        }
        #endregion


        #region Public Calls - GdipSurface
        public Bitmap MiniMap { get { return ResizeTo(minimap, panelSize); }  }
        #endregion
    }
}
