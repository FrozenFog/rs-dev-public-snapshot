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
        private Pen borderPen;
        private Rectangle clientSize;
        private Size sceneSize;
        private Graphics ds;
        private Rectangle mapsize;
        private Bitmap minimap;
        private Size panelSize;


        #region Ctor - GdipSurface
        public GdipSurface()
        {
            borderPen = new Pen(Color.White);
        }
        #endregion


        #region Public Methods - GdipSurface
        public bool Initialize(Size panelsize, Rectangle mapsize)
        {
            try
            {
                this.mapsize = mapsize;
                sceneSize = new Size(mapsize.Width * 60, mapsize.Height * 30 + 15);
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
        public void ResetSize(Size src)
        {
            panelSize = src;
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
        public void SetClientWindowSize(Rectangle client)
        {
            clientSize = client;
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
        private void To2dCoord(Point pos, out int x, out int y)
        {
            x = pos.X - pos.Y + mapsize.Width - 1;
            y = pos.X + pos.Y - mapsize.Width - 1;
        }
        private Bitmap ResizeTo(Bitmap src, Size destSize)
        {
            float scaleW = destSize.Width / (float)src.Width;
            float scaleH = destSize.Height / (float)src.Height;
            float scale = Math.Min(scaleW, scaleH);
            Bitmap dest = new Bitmap((int)(src.Width*scale), (int)(src.Height*scale));
            Graphics g = Graphics.FromImage(dest);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(src, new Rectangle(Point.Empty, dest.Size), new Rectangle(Point.Empty, src.Size), GraphicsUnit.Pixel);
            Rectangle indicator = GetClientWindowRectangle(clientSize, ClientPos, dest.Size, scale);
            g.DrawRectangle(borderPen, indicator);
            g.Dispose();
            return dest;
        }
        private Rectangle GetClientWindowRectangle(Rectangle clientsize, Point currentPos, Size destImgSize, float posScale)
        {
            float scaleX = clientsize.Width / (float)sceneSize.Width;
            float scaleY = clientsize.Height / (float)sceneSize.Height;
            To2dCoord(currentPos, out int x, out int y);
            int rx = (int)(x * posScale);
            int ry = (int)(y * posScale);
            return new Rectangle(rx, ry, (int)(destImgSize.Width * scaleX), (int)(destImgSize.Height * scaleY));
        }
        #endregion


        #region Public Calls - GdipSurface
        public Bitmap MiniMap { get { return ResizeTo(minimap, panelSize); }  }
        public Point ClientPos { get; set; }
        #endregion
    }
}
