using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RelertSharp.Utils
{
    public static class HSBColor
    {
        public static Color FromHSB(int _h, int _s, int _b, bool base256 = true)
        {
            if (base256)
            {
                _h = (int)(_h / 255F * 360);
                _s = (int)(_s / 255F * 100);
                _b = (int)(_b / 255F * 100);
            }
            if (_h == 360) _h = 0;
            float r, g, b;
            if (_s == 0) r = g = b = _b;
            else
            {
                float sectorPos = _h / 60f;
                int num = (int)Math.Floor(sectorPos);
                float delta = sectorPos - num;
                float p = _b * (100 - _s) / 100;
                float q = _b * (100 - (_s * delta)) / 100;
                float t = _b * (100 - (_s * (1 - delta))) / 100;
                switch (num)
                {
                    case 0:
                        r = _b;g = t;b = p;
                        break;
                    case 1:
                        r = q;g = _b;b = p;
                        break;
                    case 2:
                        r = p;g = _b;b = t;
                        break;
                    case 3:
                        r = p;g = q;b = _b;
                        break;
                    case 4:
                        r = t;g = p;b = _b;
                        break;
                    case 5:
                        r = _b;g = p;b = q;
                        break;
                    default:
                        r = 0;g = 0;b = 0;
                        break;
                }
            }
            return Color.FromArgb((int)(r / 100 * 255), (int)(g / 100 * 255), (int)(b / 100 * 255));
        }
        public static Color FromHSB(string[] stringlist, bool base256 = true)
        {
            return FromHSB(int.Parse(stringlist[0]), int.Parse(stringlist[1]), int.Parse(stringlist[2]), base256);
        }
        public static Color FromHSB(Color hsbcolor)
        {
            return FromHSB(hsbcolor.R, hsbcolor.G, hsbcolor.B);
        }
    }
}
