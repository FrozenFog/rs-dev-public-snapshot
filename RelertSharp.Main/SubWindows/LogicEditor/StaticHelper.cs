using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using relert_sharp.IniSystem;
using relert_sharp.Common;

namespace relert_sharp.SubWindows.LogicEditor
{
    internal static class StaticHelper
    {
        public static void LoadToObjectCollection(ComboBox dest, IEnumerable<object> src)
        {
            dest.Items.Clear();
            dest.Items.AddRange(src.ToArray());
        }
        public static void LoadToObjectCollection(ListBox dest, IEnumerable<object> src)
        {
            dest.Items.Clear();
            dest.Items.AddRange(src.ToArray());
        }
        public static void SelectCombo(ComboBox dest, string param)
        {
            foreach (TechnoPair p in dest.Items)
            {
                if (p.Index == param) dest.SelectedItem = p;
            }
        }
    }
}
