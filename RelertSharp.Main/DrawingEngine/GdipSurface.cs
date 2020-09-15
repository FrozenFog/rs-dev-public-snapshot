using RelertSharp.Common;
using RelertSharp.DrawingEngine.Drawables;
using RelertSharp.DrawingEngine.Presenting;
using System;
using System.Drawing;
using static RelertSharp.Utils.Misc;

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
        private Size scaledMinimapSize = Size.Empty;
        private Pnt scaledMinimapPos = Pnt.Zero;
        private float minimapScale;


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
                GlobalVar.Log.Write("Minimap init failed!");
                return false;
            }
        }
        public void ResetSize(Size src)
        {
            panelSize = src;
        }
        public void DrawTile(DrawableTile t, I2dLocateable pos, int subindex)
        {
            TileToFlatCoord(pos, mapsize.Width, out int x, out int y);
            if (subindex > t.SubTiles.Count - 1) subindex = 0;
            SetMinimapColorAt(x, y, t.SubTiles[subindex].RadarColor.Left);
            SetMinimapColorAt(x + 1, y, t.SubTiles[subindex].RadarColor.Right);
        }
        public void DrawTile(PresentTile t)
        {
            TileToFlatCoord(t, mapsize.Width, out int x, out int y);
            SetMinimapColorAt(x, y, t.RadarColor.Left);
            SetMinimapColorAt(x + 1, y, t.RadarColor.Right);
        }
        public void DrawStructure(DrawableStructure d, I2dLocateable pos, bool isBaseNode)
        {
            if (isBaseNode) return;
            foreach (I2dLocateable p in new Square2D(pos, d.FoundationX, d.FoundationY))
            {
                TileToFlatCoord(p, mapsize.Width, out int x, out int y);
                SetMinimapColorAt(x, y, d.MinimapColor);
                SetMinimapColorAt(x + 1, y, d.MinimapColor);
            }
        }
        public void DrawMisc(DrawableMisc d, I2dLocateable pos)
        {
            TileToFlatCoord(pos, mapsize.Width, out int x, out int y);
            if (d.RadarColor == nullcolor) return;
            SetMinimapColorAt(x, y, d.RadarColor);
            SetMinimapColorAt(x + 1, y, d.RadarColor);
        }
        public void DrawObject(IDrawableBase d, I2dLocateable pos, out Color c)
        {
            TileToFlatCoord(pos, mapsize.Width, out int x, out int y);
            c = ToColor(d.RemapColor);
            SetMinimapColorAt(x, y, c);
            SetMinimapColorAt(x + 1, y, c);
        }
        public void DrawColorable(I2dLocateable pos, IMapObject src)
        {
            TileToFlatCoord(pos, mapsize.Width, out int x, out int y);
            IPresentBase p = src.SceneObject;
            SetMinimapColorAt(x, y, p.RadarColor.Left);
            SetMinimapColorAt(x + 1, y, p.RadarColor.Right);
        }
        public void SetClientWindowSize(Rectangle client)
        {
            clientSize = client;
        }
        public Pnt GetPointFromMinimapSeeking(Pnt panelClicked)
        {
            Pnt minimapPos = panelClicked - scaledMinimapPos;
            int x = (int)(minimapPos.X / minimapScale);
            int y = (int)(minimapPos.Y / minimapScale);
            return ToTilePos(x, y);
        }
        #endregion


        #region Private Methods - GdipSurface
        private void GetScaledMinimapPos()
        {
            scaledMinimapPos.X = (panelSize.Width - scaledMinimapSize.Width) / 2;
            scaledMinimapPos.Y = (panelSize.Height - scaledMinimapSize.Height) / 2;
        }
        private void SetMinimapColorAt(int x, int y, Color color)
        {
            if (x < 0 || y < 0 || x >= minimap.Width || y >= minimap.Height) return;
            if (color == nullcolor) return;
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
        private Pnt ToTilePos(int x, int y)
        {
            return new Pnt((x + y + 2) / 2, mapsize.Width + (y - x) / 2);
        }
        private Bitmap ResizeTo(Bitmap src, Size destSize)
        {
            float scaleW = destSize.Width / (float)src.Width;
            float scaleH = destSize.Height / (float)src.Height;
            minimapScale = Math.Min(scaleW, scaleH);
            scaledMinimapSize = new Size((int)(src.Width * minimapScale), (int)(src.Height * minimapScale));
            GetScaledMinimapPos();
            Bitmap dest = new Bitmap(scaledMinimapSize.Width, scaledMinimapSize.Height);
            Graphics g = Graphics.FromImage(dest);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(src, new Rectangle(Point.Empty, dest.Size), new Rectangle(Point.Empty, src.Size), GraphicsUnit.Pixel);
            Rectangle indicator = GetClientWindowRectangle(ClientPos, dest.Size);
            g.DrawRectangle(borderPen, indicator);
            g.Dispose();
            return dest;
        }
        private Rectangle GetClientWindowRectangle(Point currentPos, Size destImgSize)
        {
            float scaleX = clientSize.Width / (float)sceneSize.Width;
            float scaleY = clientSize.Height / (float)sceneSize.Height;
            TileToFlatCoord(Pnt.FromPoint(currentPos), mapsize.Width, out int x, out int y);
            int rx = (int)(x * minimapScale);
            int ry = (int)(y * minimapScale);
            return new Rectangle(rx, ry, (int)(destImgSize.Width * scaleX), (int)(destImgSize.Height * scaleY));
        }
        #endregion


        #region Public Calls - GdipSurface
        public Bitmap MiniMap { get { return ResizeTo(minimap, panelSize); } }
        public Point ClientPos { get; set; }
        #endregion
    }
}
