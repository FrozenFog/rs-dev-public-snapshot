using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace relert_sharp.Utils
{
    public class RGBColor
    {
        private byte _R, _G, _B;
        public RGBColor(byte red, byte green, byte blue)
        {
            _R = red;
            _G = green;
            _B = blue;
        }
        public RGBColor(string[] rgbStringArray)
        {
            _R = byte.Parse(rgbStringArray[0]);
            _G = byte.Parse(rgbStringArray[1]);
            _B = byte.Parse(rgbStringArray[2]);
        }
        public RGBColor(HSBColor hsb)
        {
            ////unfinished
        }
        #region Public Calls - RGBColor
        public byte R
        {
            get { return _R; }
            set { _R = value; }
        }
        public byte G
        {
            get { return _G; }
            set { _G = value; }
        }
        public byte B
        {
            get { return _B; }
            set { _B = value; }
        }
        #endregion
    }
    public class HSBColor
    {
        private byte _h, _s, _b;
        public HSBColor(byte h, byte s, byte b)
        {
            _h = h;
            _s = s;
            _b = b;
        }
        public HSBColor(string[] hsbStringArray)
        {
            _h = byte.Parse(hsbStringArray[0]);
            _s = byte.Parse(hsbStringArray[1]);
            _b = byte.Parse(hsbStringArray[2]);
        }
        public HSBColor(RGBColor rgb)
        {
            ////unfinished
        }
        #region Public Calls - HSBColor
        public byte H
        {
            get { return _h; }
            set { _h = value; }
        }
        public byte S
        {
            get { return _s; }
            set { _s = value; }
        }
        public byte B
        {
            get { return _b; }
            set { _b = value; }
        }
        #endregion
    }
}
