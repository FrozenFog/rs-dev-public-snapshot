using RelertSharp.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Algorithm
{
    public static class MapPosition
    {
        /// <summary>
        /// Use to draw designated pixel from Map coord to Minimap.
        /// Isometric tile collapsed to 2d x-y surface as x-axis stretched 2x
        /// </summary>
        /// <param name="pos">Map coord</param>
        /// <param name="mapWidth">Map width, hight is irrelevant</param>
        /// <param name="x">out x pos in minimap pixel(left)</param>
        /// <param name="y">out y pos in minimap pixel</param>
        public static void ToMinimapPxPos(I2dLocateable pos, int mapWidth, out int x, out int y)
        {
            x = pos.X - pos.Y + mapWidth - 1;
            y = pos.X + pos.Y - mapWidth - 1;
        }
        /// <summary>
        /// Used only in Map local size(player visible size) tile position calc
        /// </summary>
        /// <param name="mapWidth">Map width, height is irrelevant</param>
        /// <param name="size">Map local size(visible size)</param>
        /// <param name="LT">Out tile Left-Top position</param>
        /// <param name="RB">Out tile Right-Bottom position</param>
        public static void VisiblePointToTile(int mapWidth, Rectangle size, out I2dLocateable LT, out I2dLocateable RB)
        {
            int xLt = size.X;
            int yLt = size.Y;
            int xRb = size.X + size.Width - 1;
            int yRb = size.Y + size.Height + 2;
            LT = new Pnt(xLt + yLt - 1, mapWidth + yLt - xLt - 2);
            RB = new Pnt(xRb + yRb - 1, mapWidth + yRb - xRb - 2);
        }
        /// <summary>
        /// Used only in Map local size(player visible size) tile position calc
        /// </summary>
        /// <param name="mapWidth">Map width, height is irrelevant</param>
        /// <param name="size">Map local size(visible size)</param>
        /// <param name="LT">Out tile Left-Top position</param>
        /// <param name="w">Real rectangle Width, used by enumerator</param>
        /// <param name="h">Real rectangle Height, used by enumerator</param>
        public static void VisiblePointToTile(int mapWidth, Rectangle size, out I2dLocateable LT, out int w, out int h)
        {
            int xLt = size.X;
            int yLt = size.Y;
            int xRb = size.X + size.Width - 1;
            int yRb = size.Y + size.Height + 2;
            LT = new Pnt(xLt + yLt - 1, mapWidth + yLt - xLt - 2);
            w = size.Width;
            h = size.Height + 3;
        }
    }
}
