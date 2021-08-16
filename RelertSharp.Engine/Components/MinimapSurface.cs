using RelertSharp.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Engine
{
    public class MinimapSurface
    {
        private Color background;
        private Bitmap surface;
        private Rectangle rect;
        private Size mapSize, imgSize;
        private Graphics ds;
        private Dictionary<I2dLocateable, Color> points = new Dictionary<I2dLocateable, Color>();
        public MinimapSurface(Rectangle mapSize)
        {
            int width = mapSize.Width + mapSize.X;
            int height = mapSize.Height + mapSize.Y;
            this.mapSize = new Size(width, height);
            imgSize = new Size(width * 2, height * 2);
            surface = new Bitmap(width * 2, height * 2);
            rect = new Rectangle(0, 0, surface.Width, surface.Height);
            background = Color.Black;
        }


        #region Api
        public void SetBackgroundColor(Color c)
        {
            background = c;
        }
        public void RemoveObjectAt(I2dLocateable pos)
        {
            if (pos != null) points.Remove(pos);
        }
        public void ClearAllObjects()
        {
            points.Clear();
        }
        public void SetColor(I2dLocateable pos, Color c)
        {
            if (pos != null) points[pos] = c;
        }
        #endregion


        #region Private
        private void DrawAll()
        {
            surface.Dispose();
            surface = new Bitmap(mapSize.Width * 2, mapSize.Height * 2);
            foreach (var pair in points)
            {
                Utils.Misc.TileToFlatCoord(pair.Key, mapSize.Width, out int x, out int y);
                surface.SetPixel(x, y, pair.Value);
                surface.SetPixel(x + 1, y, pair.Value);
            }
        }
        #endregion


        #region Calls
        public Bitmap Image
        {
            get
            {
                Bitmap img = new Bitmap(imgSize.Width, imgSize.Height);
                ds = Graphics.FromImage(img);
                ds.Clear(background);
                DrawAll();
                ds.DrawImage(surface, 0, 0);
                ds.Dispose();
                return img;
            }
        }
        #endregion
    }
}
