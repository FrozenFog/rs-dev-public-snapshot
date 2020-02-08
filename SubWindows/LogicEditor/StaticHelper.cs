using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace relert_sharp.SubWindows.LogicEditor
{
    internal static class StaticHelper
    {
        public static void LoadToObjectCollection(ref ComboBox dest, IEnumerable<object> src)
        {
            dest.Items.Clear();
            foreach (object item in src)
            {
                dest.Items.Add(item);
            }
        }
        public static void LoadToObjectCollection(ref ListBox dest, IEnumerable<object> src)
        {
            dest.Items.Clear();
            foreach (object item in src)
            {
                dest.Items.Add(item);
            }
        }
    }
}
