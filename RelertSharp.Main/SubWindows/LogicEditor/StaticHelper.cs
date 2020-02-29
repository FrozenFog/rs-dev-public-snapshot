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
            foreach (object item in src)
            {
                dest.Items.Add(item);
            }
        }
        public static void LoadToObjectCollection(ListBox dest, IEnumerable<object> src)
        {
            dest.Items.Clear();
            foreach (object item in src)
            {
                dest.Items.Add(item);
            }
        }
    }
}
