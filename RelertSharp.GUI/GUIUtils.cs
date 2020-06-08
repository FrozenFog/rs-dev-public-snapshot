using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.Common;

namespace RelertSharp.GUI
{
    internal static class GuiUtils
    {
        public static bool SafeRun(Action a, string errorMsg)
        {
            try
            {
                a.Invoke();
                return true;
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(RSException.MixEntityNotFoundException))
                {
                    RSException.MixEntityNotFoundException mx = e as RSException.MixEntityNotFoundException;
                    Fatal(string.Format("{0}\nError message: {1}\nFile name: {3}\n\nTrace:\n{2}", errorMsg, mx.RSMessage, e.StackTrace, mx.FileName));
                }
                else
                {
                    Fatal(string.Format("{0}\nError message: {1}\n\nTrace:\n{2}", errorMsg, e.Message, e.StackTrace));
                }
                return false;
            }
        }
        public static void Fatal(string content)
        {
            MessageBox.Show(content, "Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void InsertAt(ListBox dest, object item, ref bool locker)
        {
            locker = true;
            int index = dest.SelectedIndex;
            if (index >= 0)
            {
                dest.Items.Insert(index + 1, item);
            }
            else
            {
                dest.Items.Add(item);
            }
            locker = false;
            dest.SelectedIndex = index + 1;
        }
        public static void ClearControlContent(Control c)
        {
            Type t = c.GetType();
            if (t == typeof(ListView)) (c as ListView).Items.Clear();
            else if (t == typeof(MaskedTextBox) || t == typeof(TextBox)) c.Text = "";
            else if (t == typeof(ComboBox))
            {
                ComboBox cbb = c as ComboBox;
                if (cbb.Items.Count > 0) cbb.SelectedIndex = 0;
                else cbb.Text = "";
            }
            else if (t == typeof(GroupBox))
            {
                foreach (Control child in (c as GroupBox).Controls) ClearControlContent(child);
            }
        }
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
            else if (dest.Items.Count > 0) dest.SelectedIndices.Add(0);
        }
        public static void RemoveAt(ListBox dest, int index, ref bool locker)
        {
            locker = true;
            dest.Items.RemoveAt(index);
            locker = false;
            if (index != 0) dest.SelectedIndex = index - 1;
            else if (dest.Items.Count > 0) dest.SelectedIndex = 0;
            else dest.SelectedItem = null;
        }
        public static void RemoveAt(ListBox dest, ref bool locker)
        {
            int index = dest.SelectedIndex;
            locker = true;
            dest.Items.RemoveAt(index);
            locker = false;
            if (index != 0) dest.SelectedIndex = index - 1;
            else if (dest.Items.Count > 0) dest.SelectedIndex = 0;
            else dest.SelectedItem = null;
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
