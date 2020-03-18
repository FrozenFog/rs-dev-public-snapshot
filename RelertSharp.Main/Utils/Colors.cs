using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Utils
{
    public class RGBColor
    {



        #region Ctor - RGBColor
        public RGBColor(byte red, byte green, byte blue)
        {
            R = red;
            G = green;
            B = blue;
        }
        public RGBColor(int red, int green, int blue)
        {
            R = (byte)red;
            G = (byte)green;
            B = (byte)blue;
        }
        public RGBColor(string[] rgbStringArray)
        {
            R = byte.Parse(rgbStringArray[0]);
            G = byte.Parse(rgbStringArray[1]);
            B = byte.Parse(rgbStringArray[2]);
        }
        public RGBColor(HSBColor hsb)
        {
            //TODO:HSB to RGB
        }
        #endregion


        #region Public Calls - RGBColor
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }
        #endregion
    }


    public class HSBColor
    {



        #region Ctor - HSBColor
        public HSBColor(byte h, byte s, byte b)
        {
            H = h;
            S = s;
            B = b;
        }
        public HSBColor(int h, int s, int b)
        {
            H = (byte)h;
            S = (byte)s;
            B = (byte)b;
        }
        public HSBColor(string[] hsbStringArray)
        {
            H = byte.Parse(hsbStringArray[0]);
            S = byte.Parse(hsbStringArray[1]);
            B = byte.Parse(hsbStringArray[2]);
        }
        public HSBColor(RGBColor rgb)
        {
            //TODO: RGB to HSB
        }
        #endregion


        #region Public Calls - HSBColor
        public byte H { get; set; }
        public byte S { get; set; }
        public byte B { get; set; }
        #endregion
    }
}
