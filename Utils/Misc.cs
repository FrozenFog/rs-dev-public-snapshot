using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using relert_sharp.FileSystem;

namespace relert_sharp.Utils
{
    public class Misc
    {
        public static void Init_Language()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            LangFile f = null;
            switch (Constant.CurrentLanguage)
            {
                case Constant.Language.EnglishUS:
                    f = new LangFile("en-us.lang", false);
                    break;
                case Constant.Language.Chinese:
                    f = new LangFile("chs.lang", false);
                    break;
            }
            foreach (INIEntity ent in f.IniData)
            {
                foreach (INIPair p in ent.DataList)
                {
                    dict[p.Name] = p.Value;
                }
            }
            Constant.Trans = dict;
        }
        public static string[] Split(string s, char c, int t = 1)
        {
            string[] tmp = s.Split(new char[1] { c }, t + 1);
            return tmp;
        }
        /// <summary>
        /// Get Ini key type
        /// </summary>
        /// <param name="keyname"></param>
        /// <returns></returns>
        public static Constant.INIKeyType GetKeyType(string keyname)
        {
            if (Constant.Interpreter.SightLike.Contains(keyname))
            {
                return Constant.INIKeyType.SightLike;
            }
            else if (Constant.Interpreter.ActiveBoolLike.Contains(keyname))
            {
                return Constant.INIKeyType.ActiveLike;
            }
            else if (Constant.Interpreter.PassiveBoolLike.Contains(keyname))
            {
                return Constant.INIKeyType.PassiveLike;
            }
            else if (Constant.Interpreter.AcquireBoolLike.Contains(keyname))
            {
                return Constant.INIKeyType.AcquireLike;
            }
            else if (Constant.Interpreter.MultiplierLike.Contains(keyname))
            {
                return Constant.INIKeyType.MultiplierLike;
            }
            else if (Constant.Interpreter.NameLike.Contains(keyname))
            {
                return Constant.INIKeyType.NameLike;
            }
            else if (Constant.Interpreter.NameListLike.Contains(keyname))
            {
                return Constant.INIKeyType.NameListLike;
            }
            else if (Constant.Interpreter.NumListLike.Contains(keyname))
            {
                return Constant.INIKeyType.NumListLike;
            }
            else if (keyname.Contains("Versus.") && !keyname.Contains("Retaliate") && !keyname.Contains("PassiveAcquire"))
            {
                return Constant.INIKeyType.VersusLike;
            }
            else if (keyname == "Verses")
            {
                return Constant.INIKeyType.VersesListLike;
            }
            else if (keyname == "Armor")
            {
                return Constant.INIKeyType.Armor;
            }
            else if (keyname == "")
            {
                return Constant.INIKeyType.Null;
            }
            else
            {
                return Constant.INIKeyType.DefaultString;
            }
        }
        public static dynamic GetNonNull(object obj1, object obj2)
        {
            if (obj1.GetType() == typeof(Constant.INIKeyType))
            {
                if ((Constant.INIKeyType)obj1 == Constant.INIKeyType.Null) return obj2;
                return obj1;
            }
            if (obj1 == null || obj1.ToString() == "")
            {
                if (obj2 == null || obj2.ToString() == "")
                {
                    return null;
                }
                return obj2;
            }
                
            return obj1;
        }
        public static List<string> Trim(string[] obj)
        {
            for (int i = 0; i < obj.Count(); i++)
            {
                obj[i] = obj[i].Trim();
            }
            return obj.ToList();
        }
        public static string Join(List<string> sl, string joint)
        {
            if (sl.Count == 0) return "";
            int i = 0;
            string result = sl[i];
            i++;
            for (; i < sl.Count(); i++)
            {
                result += joint + sl[i];
            }
            return result;
        }
    }
}
