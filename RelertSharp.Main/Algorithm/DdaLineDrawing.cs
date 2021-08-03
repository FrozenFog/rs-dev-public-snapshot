using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;

namespace RelertSharp.Algorithm
{
    public class DdaLineDrawing
    {
        private I2dLocateable begin, end;
        public DdaLineDrawing() { }


        #region Public
        public void SetControlNode(I2dLocateable begin, I2dLocateable end)
        {
            this.begin = begin;
            this.end = end;
        }
        public List<I2dLocateable> GetLineCells()
        {
            List<I2dLocateable> results = new List<I2dLocateable>()
            {
                begin
            };
            int dx = end.X - begin.X, dy = end.Y - begin.Y, ceil;
            double x = begin.X, y = begin.Y;
            double k, incX, incY;
            if (Math.Abs(dx) >= Math.Abs(dy))
            {
                k = dy / (double)dx;
                ceil = Math.Abs(dx);
                incX = dx > 0 ? 1 : -1;
                incY = dy / (double)ceil;
            }
            else
            {
                k = (double)dx / dy;
                ceil = Math.Abs(dy);
                incY = dy > 0 ? 1 : -1;
                incX = dx / (double)ceil;
            }

            for (int i = 0; i < ceil; i++)
            {
                double xNewD = x + incX;
                double yNewD = y + incY;
                int xNew = RsMath.Round(xNewD), yNew = RsMath.Round(yNewD);
                int xOrg = RsMath.Round(x), yOrg = RsMath.Round(y);
                if (FixCorner)
                {
                    if (xNew != xOrg && yNew != yOrg)
                    {
                        results.Add(new Pnt(xOrg, yNew));
                    }
                }
                results.Add(new Pnt(xNew, yNew));
                x += incX;
                y += incY;
            }
            return results;
        }
        #endregion


        #region Call
        public bool IsReverse { get; set; }
        public bool FixCorner { get; set; }
        #endregion
    }
}
