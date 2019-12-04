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
        private Cons.Language language;
        public Lang(Cons.Language l)
        {
            language = l;
            LangFile f = null;
            if (l == Cons.Language.EnglishUS) f = new LangFile("en-us.lang", false);
            else if (l == Cons.Language.Chinese) f = new LangFile("chs.lang", false);
            foreach (INIEntity ent in f.IniData)
            {
                foreach (INIPair p in ent.DataList)
                {
                    dict[p.Name] = p.Value;
                }
            }
            
        }
        public Cons.Language Lg
        {
            get { return language; }
        }
        public string Ds(string key)
        {
            if (dict.Keys.Contains(key)) return dict[key];
            else return key;
        }
    }
}
