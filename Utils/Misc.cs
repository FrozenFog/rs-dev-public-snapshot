using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using relert_sharp.FileSystem;
using relert_sharp.Common;

namespace relert_sharp.Utils
{
    public class Misc
    {
        public static void Init_Language()
        {
            GlobalVar.CurrentLanguage = ELanguage.Chinese;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            LangFile f = null;
            switch (GlobalVar.CurrentLanguage)
            {
                case ELanguage.EnglishUS:
                    f = new LangFile("en-us.lang");
                    break;
                case ELanguage.Chinese:
                    f = new LangFile("chs.lang");
                    break;
            }
            foreach (INIEntity ent in f.IniData)
            {
                foreach (INIPair p in ent.DataList)
                {
                    dict[p.Name] = p.Value;
                }
            }
            Language.DICT = new Lang(dict);
        }
        public static bool ParseBool(string s, bool def = false)
        {
            if (s != "")
            {
                if (Constant.BoolTrue.Contains(s)) return true;
                else if (Constant.BoolFalse.Contains(s)) return false;
                else if (int.Parse(s) == 1) return true;
                else if (int.Parse(s) == 0) return false;
            }
            return def;
        }
        public static dynamic GetNonNull(object obj1, object obj2)
        {
            if (obj1.GetType() == typeof(INIKeyType))
            {
                if ((INIKeyType)obj1 == INIKeyType.Null) return obj2;
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
        public static string CoordString(int x, int y)
        {
            return (x * 1000 + y).ToString();
        }
        public static int CoordInt(int x, int y)
        {
            return 1000 * x + y;
        }
        public static int CoordByteX(int crd)
        {
            return crd % 512;
        }
        public static int CoordByteY(int crd)
        {
            return crd / 512;
        }
        public static uint[] ToUintArray(byte[] data)
        {
            uint[] result = new uint[data.Length / 4];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = BitConverter.ToUInt32(data, 4 * i);
            }
            return result;
        }
        public static byte[] ToByteArray(uint[] data)
        {
            byte[] result = new byte[data.Length * 4];
            for (int i = 0; i < data.Length; i++)
            {
                WriteToArray(result, BitConverter.GetBytes(data[i]), 4 * i);
            }
            return result;
        }
        /// <summary>
        /// Write src into dest from [offset]
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="src"></param>
        /// <param name="offset"></param>
        public static void WriteToArray(byte[] dest, byte[] src, int offset)
        {
            for (int i = 0; i < src.Length && i + offset < dest.Length; i++)
            {
                dest[i + offset] = src[i];
            }
        }
        /// <summary>
        /// Return x,y,w,h string for ini key-value
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static string FromRectangle(Rectangle rect)
        {
            return rect.X.ToString() + "," + rect.Y.ToString() + "," + rect.Width.ToString() + "," + rect.Height.ToString();
        }
        /// <summary>
        /// Return waypoint in int, must between A - ZZ
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int WaypointInt(string s)
        {
            s = s.ToUpper();
            if (s == "") return 0;
            char c1 = (char)64, c2 = (char)64;
            if (s.Length == 1)
            {
                c2 = s[0];
            }
            else
            {
                c1 = s[0];
                c2 = s[1];
            }
            int result = (c1 - 64) * 26 + (c2 - 64);
            if (result < 1 || result > 702) throw new RSException.InvalidWaypointException(result);
            return result - 1;
        }
        /// <summary>
        /// Return waypoint in alphabet format, [0, 701]
        /// </summary>
        /// <param name="waypoint"></param>
        /// <returns></returns>
        public static string WaypointString(int waypoint)
        {
            if (waypoint < 0 || waypoint > 701) throw new RSException.InvalidWaypointException(waypoint);
            int c1, c2;
            if (waypoint < 26)
            {
                c1 = waypoint % 26 + 65;
                return ((char)c1).ToString();
            }
            else
            {
                c1 = waypoint / 26 + 64;
                c2 = waypoint % 26 + 65;
                string result = ((char)c1).ToString() + ((char)c2).ToString();
                return result;
            }
        }
    }

}
