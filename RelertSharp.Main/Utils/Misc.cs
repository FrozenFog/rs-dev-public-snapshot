using RelertSharp.Common;
using RelertSharp.FileSystem;
using RelertSharp.IniSystem;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RelertSharp.Utils
{
    public class Misc
    {
        private static RsLog Log { get { return GlobalVar.Log; } }


        public static void Init_Language()
        {
            GlobalVar.CurrentLanguage = ELanguage.EnglishUS;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            LangFile f = null;
            switch (GlobalVar.CurrentLanguage)
            {
                case ELanguage.EnglishUS:
                    f = new LangFile("en-us.lang");
                    Log.Write("Language set as English-US");
                    break;
                case ELanguage.Chinese:
                    f = new LangFile("chs.lang");
                    Log.Write("Language set as Chinese");
                    break;
            }
            if (System.IO.File.Exists("external.lang"))
            {
                INIFile ext = new INIFile("external.lang");
                f.Merge(ext);
            }
            foreach (INIEntity ent in f.IniData)
            {
                foreach (INIPair p in ent.DataList)
                {
                    if (!dict.Keys.Contains(p.Name)) dict[p.Name] = p.Value;
                }
            }
            Language.DICT = new Lang(dict);
            Log.Write("Language Init complete");
        }
        public static void RepositionItemInList<T>(List<T> dest, T item, int indexTarget, int indexNow)
        {
            dest.RemoveAt(indexNow);
            dest.Insert(indexTarget, item);
        }
        public static List<T> InitializeListWithCap<T>(int size)
        {
            List<T> result = new List<T>();
            for (int i = 0; i < size; i++)
            {
                result.Add(default(T));
            }
            return result;
        }
        public static void SetListValue<T>(ref List<T> src, T value)
        {
            for (int i = 0; i < src.Count; i++)
            {
                src[i] = value;
            }
        }
        public static Image ResizeImage(Image src, Size newSize)
        {
            if (src.Width > newSize.Width || src.Height > newSize.Height) return src;
            Bitmap dest = new Bitmap(newSize.Width, newSize.Height);
            Point pos = GetResizePoint(newSize, src.Size);
            Graphics g = Graphics.FromImage(dest);
            g.DrawImage(src, pos);
            g.Dispose();
            return dest;
        }
        public static Size GetMaxSize(Size szA, Size szB)
        {
            Size result = new Size(szB.Width, szB.Height);
            if (szA.Width > szB.Width) result.Width = szA.Width;
            if (szA.Height > szB.Height) result.Height = szA.Height;
            return result;
        }
        public static Point GetResizePoint(Size dest, Size src)
        {
            int x = (dest.Width - src.Width) / 2;
            int y = (dest.Height - src.Height) / 2;
            return new Point(x, y);
        }
        public static T MemCpy<T>(T src)
        {
            T dest = Activator.CreateInstance<T>();
            Type tIn = src.GetType();
            foreach (var itemDest in dest.GetType().GetProperties())
            {
                var itemSrc = tIn.GetProperty(itemDest.Name);
                if (itemSrc != null && itemDest.CanWrite)
                {
                    itemDest.SetValue(dest, itemSrc.GetValue(src));
                }
            }
            return dest;
        }
        public static int Region(int _floor, int _ceil, int _src)
        {
            if (_src >= _ceil) return _ceil;
            if (_src <= _floor) return _floor;
            return _src;
        }
        public static string Replace(string src, int pos, char c)
        {
            return src.Remove(pos, 1).Insert(pos, c.ToString());
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
        public static string YesNo(bool src)
        {
            return src ? "yes" : "no";
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
        public static string Join(char joint, IEnumerable<object> objects)
        {
            string result = "";
            if (objects.Count() != 0)
            {
                foreach (object obj in objects)
                {
                    if (obj.GetType() == typeof(bool)) result += ((bool)obj ? "1" : "0") + joint;
                    else result += obj.ToString() + joint;
                }
                result = result.Substring(0, result.Length - 1);
            }
            return result;
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
        public static float ParseFloat(string src, float def = 0)
        {
            if (string.IsNullOrEmpty(src)) return def;
            return float.Parse(src);
        }
        public static double ParseDouble(string src, double def = 0)
        {
            if (string.IsNullOrEmpty(src)) return def;
            return double.Parse(src);
        }
        public static string CoordString(int x, int y)
        {
            return (x * 1000 + y).ToString();
        }
        public static int CoordInt(int x, int y)
        {
            return 1000 * x + y;
        }
        public static int CoordInt(I2dLocateable pos)
        {
            return 1000 * pos.X + pos.Y;
        }
        public static int CoordInt(float x, float y)
        {
            return (int)(1000 * x + y);
        }
        public static int CoordByteX(int crd)
        {
            return crd % 512;
        }
        public static int CoordByteY(int crd)
        {
            return crd >> 9;
        }
        public static int CoordIntX(int crd)
        {
            return crd % 1000;
        }
        public static int CoordIntY(int crd)
        {
            return crd / 1000;
        }
        public static int Coord128X(int coord)
        {
            return coord % 128;
        }
        public static int Coord128Y(int coord)
        {
            return coord >> 7;
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
            if (result < 1 || result > 702)
            {
                try
                {
                    return int.Parse(s);
                }
                catch
                {
                    throw new RSException.InvalidWaypointException(result);
                }
            }
            return result - 1;
        }
        /// <summary>
        /// Return waypoint in alphabet format, [0, 701]
        /// </summary>
        /// <param name="waypoint"></param>
        /// <returns></returns>
        public static string WaypointString(int waypoint)
        {
            if (waypoint < 0 || waypoint > 701)
            {
                return waypoint.ToString();
                //throw new RSException.InvalidWaypointException(waypoint);
            }
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
        /// <summary>
        /// Return int of Little-Endian byte string, using for ai-trigger condition
        /// </summary>
        /// <param name="byteString"></param>
        /// <returns></returns>
        public static int FromLEByteString(string byteString)
        {
            int result = 0;
            for (int i = 0; i < 4; i++)
            {
                result += Convert.ToInt32("0x" + byteString.Substring(i * 2, 2), 16) << (i * 8);
            }
            return result;
        }
        /// <summary>
        /// Return string From int, using for saving ai-trigger condition
        /// </summary>
        /// <param name="byteInt"></param>
        /// <returns></returns>
        public static string ToLEByteString(int byteInt)
        {
            string ret = string.Empty;
            string src = string.Format("{0:X8}", byteInt);
            src = src.ToUpper();
            for (int i = 3; i >= 0; i--)
            {
                ret += src.Substring(i * 2, 2);
            }

            return ret;
        }
        /// <summary>
        /// Return black(0x00000000) if something happened
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static Color ToColor(string[] src)
        {
            int r = 0xFF, g = 0xFF, b = 0xFF;
            if (src.Length != 3) return Color.FromArgb(0x00000000);
            try
            {
                r = int.Parse(src[0]);
                g = int.Parse(src[1]);
                b = int.Parse(src[2]);
            }
            catch
            {
                return Color.FromArgb(0x00000000);
            }
            return Color.FromArgb(r, g, b);
        }
        public static Color ToColor(uint remapcolor)
        {
            int r = (int)(remapcolor & 0xFF);
            int g = (int)(remapcolor & 0xFF00) >> 8;
            int b = (int)(remapcolor & 0xFF0000) >> 16;
            return Color.FromArgb(r, g, b);
        }
        public static Rectangle UnionRectangle(ref Rectangle rectA, ref Rectangle rectB)
        {
            Rectangle result = Rectangle.Union(rectA, rectB);
            int aX = rectA.Location.X - result.Location.X;
            int aY = rectA.Location.Y - result.Location.Y;
            int bX = rectB.Location.X - result.Location.X;
            int bY = rectB.Location.Y - result.Location.Y;
            rectA.Location = new Point(aX, aY);
            rectB.Location = new Point(bX, bY);
            return result;
        }
        public static Rectangle RectFromIntList(int[] src)
        {
            if (src.Length == 2) return new Rectangle(0, 0, src[0], src[1]);
            else if (src.Length == 4) return new Rectangle(src[0], src[1], src[2], src[3]);
            else return new Rectangle();
        }
        public static int TimeInt(string s)
        {
            string[] tmp = s.Split(new char[] { ':' });
            try
            {
                int result = int.Parse(tmp[2]);
                return result + int.Parse(tmp[1]) * 60 + int.Parse(tmp[0]) * 3600;
            }
            catch
            {
                return 0;
            }
        }
        public static string TimeString(int seconds)
        {
            int h = seconds / 3600;
            int m = seconds % 3600 / 60;
            int s = seconds % 60;
            return string.Format("{0:D2}:{0:D2}:{0:D2}", h, m, s);
        }
        public static byte[] GetBytes(short[] data)
        {
            byte[] result = new byte[sizeof(short) * data.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i++] = (byte)data[i / sizeof(short)];
                result[i] = (byte)(data[i / sizeof(short)] >> 4);
            }
            return result;
        }
        public static Point DeltaPoint(Point pre, Point now)
        {
            return new Point(now.X - pre.X, now.Y - pre.Y);
        }
        public static void DebugSave(byte[] data, string filename)
        {
            System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Dispose();
        }
        public static void TileToFlatCoord(I2dLocateable pos, int mapWidth, out int x, out int y)
        {
            x = pos.X - pos.Y + mapWidth - 1;
            y = pos.X + pos.Y - mapWidth - 1;
        }
        public static void FlatCoordToTile(I2dLocateable pos, int mapWidth, out int tileX, out int tileY)
        {
            tileX = (pos.X + pos.Y + 2) / 2;
            tileY = (2 * mapWidth + pos.Y - pos.X) / 2;
        }
        public static void Swap<T>(ref T src1, ref T src2) where T : struct
        {
            T tmp = src1;
            src1 = src2;
            src2 = tmp;
        }
        public static void AdjustComboBoxDropDownWidth(ref System.Windows.Forms.ComboBox cbb)
        {
            if (cbb != null && cbb.Items.Count != 0)
            {
                int max = cbb.Width;
                foreach (var i in cbb.Items)
                    max = i.ToString().Length * 7 > max ? i.ToString().Length * 7 : max;
                cbb.DropDownWidth = max;
            }
        }

        public static string ParseIEnumerableToString(IEnumerable<object> pContainer)
        {
            string ret = "";
            foreach (object obj in pContainer)
                ret += obj.ToString();
            return ret;
        }

        public static string ParseObjectsToString(params object[] pObjects)
        {
            string ret = "";
            foreach (object obj in pObjects)
                ret += obj.ToString();
            return ret;
        }

    }

}
