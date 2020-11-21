using System;
using System.Drawing;

namespace RelertSharp.Utils
{
    public static class HSBColor
    {
        public static void ToHsl(decimal r, decimal g, decimal b, out decimal h, out decimal s, out decimal l)
        {
            decimal max = Misc.MaxItem(r, g, b);
            decimal min = Misc.MinItem(r, g, b);
            decimal delta = max - min;

            h = 0;
            if (max == r) h = (g - b) / delta % 6 * 60;
            else if (max == g) h = ((b - r) / delta + 2) * 60;
            else if (max == b) h = ((r - g) / delta + 4) * 60;

            l = (max + min) / 2;

            s = delta == 0 ? 0 : delta / (1 - Math.Abs(2 * l - 1));
        }
        public static void FromHsl(decimal h, decimal s, decimal l, out decimal r, out decimal g, out decimal b)
        {
            decimal c = (1 - Math.Abs(2 * l - 1)) * s;
            decimal x = c * (1 - Math.Abs((h / 60) % 2 - 1));
            decimal m = l - c / 2;
            if (0 <= h && h < 60)
            {
                r = c; g = x; b = 0;
            }
            else if (60 <= h && h < 120)
            {
                r = x; g = c; b = 0;
            }
            else if (120 <= h && h < 180)
            {
                r = 0; g = c; b = x;
            }
            else if (180 <= h && h < 240)
            {
                r = 0; g = x; b = c;
            }
            else if (240 <= h && h < 300)
            {
                r = x; g = 0; b = c;
            }
            else
            {
                r = c; g = 0; b = x;
            }

            r += m;
            g += m;
            b += m;
        }
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
                        r = _b; g = t; b = p;
                        break;
                    case 1:
                        r = q; g = _b; b = p;
                        break;
                    case 2:
                        r = p; g = _b; b = t;
                        break;
                    case 3:
                        r = p; g = q; b = _b;
                        break;
                    case 4:
                        r = t; g = p; b = _b;
                        break;
                    case 5:
                        r = _b; g = p; b = q;
                        break;
                    default:
                        r = 0; g = 0; b = 0;
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
