using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace relert_sharp.Utils
{
    public class Misc
    {
        private static readonly string[] booltrue = { "yes", "True", "true"};
        private static readonly string[] boolfalse = { "no", "False", "false" };
        private static readonly string[] nullstring = { "<none>", "None", "none" };
        public static bool IsList(string s)
        {
            return s.Contains(",");
        }
        public static bool IsPercentFloat(string s)
        {
            return s.Contains("%");
        }
        public static string[] Split(string s, char c, int t = 1)
        {
            string[] tmp = s.Split(new char[1] { c }, t+1);
            return tmp;
        }
    }
}
