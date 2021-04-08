using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Algorithm
{
    public static class FisherYatesShuffle
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> src, Func<T, T, bool> swapCondition = null, int seed = 0)
        {
            Random rnd;
            if (seed == 0) rnd = new Random();
            else rnd = new Random(seed);
            List<T> r = new List<T>(src);
            for (int i = r.Count - 1; i >= 1; i--)
            {
                int maxTry = 100;
                if (swapCondition != null)
                {
                    while (maxTry-- > 0)
                    {
                        int j = rnd.Next(0, i + 1);
                        T tmp = r[i];
                        if (swapCondition(tmp, r[j]))
                        {
                            r[i] = r[j];
                            r[j] = tmp;
                            break;
                        }
                    }
                }
                else
                {
                    int j = rnd.Next(0, i + 1);
                    T tmp = r[i];
                    r[i] = r[j];
                    r[j] = tmp;
                }
            }
            return r;
        }
    }
}
