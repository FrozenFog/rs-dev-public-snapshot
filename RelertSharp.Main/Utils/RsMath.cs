using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp
{
    public static class RsMath
    {
        public static int TrimTo(this int src, int floor, int ceil)
        {
            if (floor > ceil) throw new ArgumentException("Floor is grater than Ceil");
            if (src >= ceil) return ceil;
            if (src <= floor) return floor;
            return src;
        }

        public static short TrimTo(this short src, short floor, short ceil)
        {
            if (floor > ceil) throw new ArgumentException("Floor is grater than Ceil");
            if (src >= ceil) return ceil;
            if (src <= floor) return floor;
            return src;
        }

        public static decimal TrimTo(this decimal src, decimal floor, decimal ceil)
        {
            if (floor > ceil) throw new ArgumentException("Floor is grater than Ceil");
            if (src >= ceil) return ceil;
            if (src <= floor) return floor;
            return src;
        }

        public static bool InRange(this double src, double floor, double ceil)
        {
            return src >= floor && src <= ceil;
        }

        public static double ForcePositive(this double src)
        {
            if (src <= 0) return 0;
            return src;
        }
        public static int ForcePositive(this int src)
        {
            if (src <= 0) return 0;
            return src;
        }
    }
}
