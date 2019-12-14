using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.FileSystem;

namespace relert_sharp.Utils
{
    public class Lang
    {
        private Dictionary<string, string> dict = new Dictionary<string, string>();
        public Lang(Dictionary<string, string> src)
        {
            dict = src;
        }
        public string this[string key]
        {
            get
            {
                if (dict.Keys.Contains(key)) return dict[key];
                return key;
            }
        }
    }
}
namespace relert_sharp
{
    public static class Language
    {
        private static Utils.Lang translate;
        public static Utils.Lang DICT
        {
            get { return translate; }
            set { translate = value; }
        }
    }
}
