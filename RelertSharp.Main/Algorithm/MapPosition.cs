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
        /// Use to regular square 2d enumerator's (isometric) Up & Down
        /// field selecting, as click position may not always up & down
        /// </summary>
        /// <param name="p1">first point, order is irrelevant</param>
        /// <param name="p2">second point, order is irrelevant</param>
        /// <param name="up">out Top cell position</param>
        /// <param name="down">out Down cell position</param>
        public static void RegularCellToIsometricSquare(I2dLocateable p1, I2dLocateable p2, out I2dLocateable up, out I2dLocateable down)
        {
            up = new Pnt(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y));
            down = new Pnt(Math.Max(p1.X, p2.X), Math.Max(p1.Y, p2.Y));
        }
        /// <summary>
        /// Use to regular scene square 2d enumerator's Left-Top & Right-Bottom
        /// field selecting, as click position may not always LT & RB
        /// </summary>
        /// <param name="p1">first point, order is irrelevant</param>
        /// <param name="p2">second point, order is irrelevant</param>
        /// <param name="mapWidth">Map width, height is irrelevant</param>
        /// <param name="LT">out Left-Top cell position</param>
        /// <param name="RB">out Right-Bottom cell position</param>
        public static void RegularCellToSceneSquare(I2dLocateable p1, I2dLocateable p2, int mapWidth, out I2dLocateable LT, out I2dLocateable RB)
        {
            CellToMinimapPxPos(p1, mapWidth, out int x1, out int y1);
            CellToMinimapPxPos(p2, mapWidth, out int x2, out int y2);
            Pnt lt = new Pnt(Math.Min(x1, x2), Math.Min(y1, y2));
            Pnt rb = new Pnt(Math.Max(x1, x2), Math.Max(y1, y2));
            MinimapPxPosToCell(lt.X, lt.Y, mapWidth, out I2dLocateable i2dlt);
            MinimapPxPosToCell(rb.X, rb.Y, mapWidth, out I2dLocateable i2drb);
            LT = i2dlt;
            RB = i2drb;
        }
        /// <summary>
        /// Use to draw designated pixel from Map coord to Minimap.
        /// Isometric tile collapsed to 2d x-y surface as x-axis stretched 2x
        /// </summary>
        /// <param name="pos">Map coord</param>
        /// <param name="mapWidth">Map width, hight is irrelevant</param>
        /// <param name="x">out x pos in minimap pixel(left)</param>
        /// <param name="y">out y pos in minimap pixel</param>
        public static void CellToMinimapPxPos(I2dLocateable pos, int mapWidth, out int x, out int y)
        {
            x = pos.X - pos.Y + mapWidth - 1;
            y = pos.X + pos.Y - mapWidth - 1;
        }
        /// <summary>
        /// Convert 2d x-y surface coord to map cell coord
        /// </summary>
        /// <param name="x">2d pixel X pos</param>
        /// <param name="y">2d pixel Y pos</param>
        /// <param name="mapWidth">Map width, height is irrelevant</param>
        /// <param name="cell">out cell coord</param>
        public static void MinimapPxPosToCell(int x, int y, int mapWidth, out I2dLocateable cell)
        {
            cell = new Pnt((x + y + 2) / 2, mapWidth + (y - x) / 2);
        }
        /// <summary>
        /// Used only in Map local size(player visible size) tile position calc.
        /// This area is visible to player, include border
        /// </summary>
        /// <param name="mapWidth">Map width, height is irrelevant</param>
        /// <param name="size">Map local size(visible size)</param>
        /// <param name="LT">Out tile Left-Top position</param>
        /// <param name="RB">Out tile Right-Bottom position</param>
        public static void GetVisibleArea(int mapWidth, Rectangle size, out I2dLocateable LT, out I2dLocateable RB)
        {
            int xLt = size.X;
            int yLt = size.Y;
            int xRb = size.X + size.Width - 1;
            int yRb = size.Y + size.Height + 2;
            LT = new Pnt(xLt + yLt - 3, mapWidth + yLt - xLt - 3);
            RB = new Pnt(xRb + yRb - 3, mapWidth + yRb - xRb - 3);
        }
        /// <summary>
        /// Used only in Map local size(player visible size) tile position calc.
        /// This area is movable for player, include border
        /// </summary>
        /// <param name="mapWidth">Map width, height is irrelevant</param>
        /// <param name="size">Map local size(visible size)</param>
        /// <param name="LT">Out tile Left-Top position</param>
        /// <param name="RB">Out tile Right-Bottom position</param>
        public static void GetMovableArea(int mapWidth, Rectangle size, out I2dLocateable LT, out I2dLocateable RB)
        {
            int xLt = size.X;
            int yLt = size.Y;
            int xRb = size.X + size.Width - 1;
            int yRb = size.Y + size.Height + 2;
            LT = new Pnt(xLt + yLt + 1, mapWidth + yLt - xLt);
            RB = new Pnt(xRb + yRb - 4, mapWidth + yRb - xRb - 3);
        }
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
