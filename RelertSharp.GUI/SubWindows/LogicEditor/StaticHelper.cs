using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;
using System.Security.Cryptography;
using System.ComponentModel;

namespace RelertSharp.SubWindows.LogicEditor
{
    internal static class StaticHelper
    {
        public static void LoadToObjectCollection(ComboBox dest,Type type)
        {
            dest.Items.Clear();
            dest.BeginUpdate();
            string[] names = Enum.GetNames(type);
            int[] values = (int[])Enum.GetValues(type);
            int count = names.Length;
            for(int i = 0; i < count; i++)
            {
                dest.Items.Add(new EnumDisplayClass(values[i], Language.DICT[type.Name + "." + names[i]]));
            }
            Utils.Misc.AdjustComboBoxDropDownWidth(ref dest);
            dest.EndUpdate();
        }
        public static void LoadToObjectCollection<T>(ComboBox dest, IList<T> src)
        {
            if (src != null && src.Count != 0)
            {
                int max = src.Max(x => x.ToString().Length) * 7;
                dest.DropDownWidth = max;
            }
            dest.DataSource = src;
        }
    }
}
