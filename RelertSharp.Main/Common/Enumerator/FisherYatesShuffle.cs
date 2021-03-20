using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Common
{
    public static class FisherYatesShuffle
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> src, int seed = 0)
        {
            Random rnd;
            if (seed == 0) rnd = new Random();
            else rnd = new Random(seed);
            List<T> r = new List<T>(src);
            for (int i = r.Count - 1; i >= 1; i--)
            {
                int j = rnd.Next(0, i + 1);
                T tmp = r[i];
                r[i] = r[j];
                r[j] = tmp;
            }
            return r;
        }
    }
}
