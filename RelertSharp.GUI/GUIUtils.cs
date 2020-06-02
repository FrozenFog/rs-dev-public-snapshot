using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelertSharp.GUI
{
    internal static class GuiUtils
    {
        public static void LoadToObjectCollection(ComboBox dest, IEnumerable<object> src)
        {
            dest.Items.Clear();
            if (src == null) return;
            dest.Items.AddRange(src.ToArray());
            int wd = src.Max(x => x.ToString().Length) * 7;
            dest.DropDownWidth = wd;
        }
        public static void LoadToObjectCollection(ListBox dest, IEnumerable<object> src)
        {
            dest.Items.Clear();
            if (src == null) return;
            dest.Items.AddRange(src.ToArray());
        }
        public static void LoadToObjectCollection(ListView dest, IEnumerable<ListViewItem> src)
        {
            dest.Items.Clear();
            dest.BeginUpdate();
            if (src.Count() > 0)
            {
                dest.Items.AddRange(src.ToArray());
            }
            dest.EndUpdate();
        }
        public static void AddTo(ListBox dest, object obj, ref bool locker)
        {
            locker = true;
            dest.Items.Add(obj);
            locker = false;
            dest.SelectedItem = obj;
        }
        public static void UpdateAt(ListBox dest, object updatevalue, ref bool locker)
        {
            locker = true;
            int index = dest.SelectedIndex;
            if (index < 0)
            {
                locker = false;
                return;
            }
            dest.Items.RemoveAt(index);
            dest.Items.Insert(index, updatevalue);
            dest.SelectedIndex = index;
            locker = false;
        }
        public static void UpdateAt(ListBox dest, object updatedvalue, int index, ref bool locker)
        {
            locker = true;
            int prev = dest.SelectedIndex;
            if (index < 0)
            {
                locker = false;
                return;
            }
            dest.Items.RemoveAt(index);
            dest.Items.Insert(index, updatedvalue);
            dest.SelectedIndex = prev;
            locker = false;
        }
        public static void UpdateAt(ListView dest, ListViewItem item, ref bool locker)
        {
            locker = true;
            int index = dest.SelectedIndices[0];
            dest.Items.RemoveAt(index);
            dest.Items.Insert(index, item);
            dest.SelectedIndices.Clear();
            dest.SelectedIndices.Add(index);
            locker = false;
        }
        public static void RemoveAt(ListView dest, int index, ref bool locker)
        {
            locker = true;
            dest.SelectedIndices.Clear();
            dest.Items.RemoveAt(index);
            locker = false;
            if (index != 0) dest.SelectedIndices.Add(index - 1);
        }
        public static void RemoveAt(ListBox dest, int index, ref bool locker)
        {
            locker = true;
            dest.Items.RemoveAt(index);
            locker = false;
            if (index != 0) dest.SelectedIndex = index - 1;
            else if (dest.Items.Count > 0) dest.SelectedIndex = 0;
        }
        public static void GoEnter(KeyEventArgs e, Action a)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                a.Invoke();
            }
        }
    }
}
