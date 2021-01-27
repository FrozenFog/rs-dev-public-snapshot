using RelertSharp.Common;
using RelertSharp.IniSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace System.Drawing
{
    public static class PointExtension
    {
        public static Point Delta(this Point prev, Point now)
        {
            return new Point(now.X - prev.X, now.Y - prev.Y);
        }
    }



    public static class RectangleExtension
    {
        /// <summary>
        /// Return x,y,w,h string for ini key-value
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string ParseString(this Rectangle src)
        {
            return string.Format("{0},{1},{2},{3}", src.X, src.Y, src.Width, src.Height);
        }
    }


    public static class ImageExtension
    {
        public static Image ResizeDraw(this Image src, Size newSize)
        {
            if (src.Width > newSize.Width || src.Height > newSize.Height) return src;
            Bitmap dest = new Bitmap(newSize.Width, newSize.Height);
            Point pos = newSize.GetLeftTopOfSize(src.Size);
            Graphics g = Graphics.FromImage(dest);
            g.DrawImage(src, pos);
            g.Dispose();
            return dest;
        }
    }



    public static class SizeExtension
    {
        public static Point GetLeftTopOfSize(this Size host, Size intake)
        {
            int x = (host.Width - intake.Width) / 2;
            int y = (host.Height - intake.Height) / 2;
            return new Point(x, y);
        }
        public static Size Union(this Size szA, Size szB)
        {
            Size result = new Size(szB.Width, szB.Height);
            if (szA.Width > szB.Width) result.Width = szA.Width;
            if (szA.Height > szB.Height) result.Height = szA.Height;
            return result;
        }
    }

    public static class ColorExtension
    {
        public static string HexCode(this Color c)
        {
            return "#" + c.ToArgb().ToString("X").Substring(2);
        }
    }
}

namespace System.Collections.Generic
{
    public static class ListExtension
    {
        public static void Reposition<T>(this List<T> dest, T item, int indexTarget, int indexNow)
        {
            dest.RemoveAt(indexNow);
            dest.Insert(indexTarget, item);
        }
        public static void SetValueAll<T>(this List<T> src, T value)
        {
            for (int i = 0; i< src.Count; i++)
            {
                src[i] = value;
            }
        }
        public static string JoinBy<T>(this IEnumerable<T> src, string joint = ",")
        {
            string result = "";
            bool empty = true;
            foreach (T item in src)
            {
                empty = false;
                result += item.ToString() + joint;
            }
            if (!empty) result = result.Substring(0, result.Length - joint.Length);
            return result;
        }
        public static IIndexableItem ValueEqual(this IEnumerable<IIndexableItem> src, string target)
        {
            if (target == Constant.ITEM_ALL) return ComboItem.AllItem;
            if (target == Constant.ITEM_NONE) return ComboItem.NoneItem;
            return src.Where(x => x.Value == target).FirstOrDefault();
        }
        public static IIndexableItem IndexEqual(this IEnumerable<IIndexableItem> src, string target)
        {
            if (target == Constant.ITEM_ALL) return ComboItem.AllItem;
            if (target == Constant.ITEM_NONE) return ComboItem.NoneItem;
            return src.Where(x => x.Id == target).FirstOrDefault();
        }
        public static IIndexableItem ValueEqual(this IEnumerable<IIndexableItem> src, int target)
        {
            return src.Where(x => x.Value == target.ToString()).FirstOrDefault();
        }
    }





    public static class HashSetExtension
    {
        public static void AddRange<T>(this HashSet<T> hashset, IEnumerable<T> src)
        {
            foreach (var obj in src)
                hashset.Add(obj);
        }
    }





    public static class DictionaryExtension
    {
        public static Dictionary<TKey, TValue> Clone<TKey, TValue>(this Dictionary<TKey, TValue> src)
        {
            return new Dictionary<TKey, TValue>(src);
        }

        public static void JoinWith<TKey, TValue>(this Dictionary<TKey, TValue> src, Dictionary<TKey, TValue> target)
        {
            foreach (TKey k in target.Keys)
            {
                src[k] = target[k];
            }
        }
    }
}





namespace System
{
    public static class SystemExtensions
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

        public static decimal AsDecimal(this float src)
        {
            return (decimal)src;
        }



        public static string Replace(this string src, int pos, char c)
        {
            return src.Remove(pos, 1).Insert(pos, c.ToString());
        }

        public static string ToLang(this string src)
        {
            return RelertSharp.Language.DICT[src];
        }
        public static bool IsNullOrEmpty(this string src)
        {
            return string.IsNullOrEmpty(src);
        }

        public static int ToInt(this bool src)
        {
            return src ? 1 : 0;
        }
        public static bool ParseBool(this string src, bool def = false)
        {
            if (bool.TryParse(src, out bool b)) return b;
            return def;
        }
        public static int ParseInt(this string src, int def = 0)
        {
            if (int.TryParse(src, out int i)) return i;
            return def;
        }
        public static bool IniParseBool(this string s, bool def = false)
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
        public static string YesNo(this bool src)
        {
            return src ? "yes" : "no";
        }
    }
}
