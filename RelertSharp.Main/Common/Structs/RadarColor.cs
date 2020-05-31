using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RelertSharp.Common
{
    public struct RadarColor
    {
        public RadarColor(Color l, Color r)
        {
            Left = l;
            Right = r;
        }
        public RadarColor(Color one)
        {
            Left = one;
            Right = one;
        }

        public Color Left;
        public Color Right;
    }
}
