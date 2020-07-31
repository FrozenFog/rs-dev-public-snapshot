using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Utils
{
    public static class HashSetExtension
    {
        public static void AddRange(this HashSet<string> hashset,IEnumerable<string> src)
        {
            foreach (var obj in src)
                hashset.Add(obj);
        }
    }
}
