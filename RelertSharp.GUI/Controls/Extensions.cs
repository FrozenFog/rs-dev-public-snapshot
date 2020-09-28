using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace System.Windows.Forms
{
    /// <summary>
    /// This class aims to extend some useful functions which some controls don't owns.
    /// Like DataGridView, which does't have BeginUpdate/EndUpdate
    /// </summary>
    public static class ControlExtension
    {
        [DllImport("user32.dll", EntryPoint = "SendMessageA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SendMessage(IntPtr hwnd, uint uMsg, int wParam, int lParam);
        private const int WM_SETREDRAW = 0xB;



        #region Generic
        public static void BeginUpdate(this Control control)
        {
            SendMessage(control.Handle, WM_SETREDRAW, 0, 0);
        }

        /// <summary>
        /// callRedraw decides if the control will redraw now
        /// </summary>
        /// <param name="callRedraw"></param>
        public static void EndUpdate(this Control control, bool callRedraw = true)
        {
            SendMessage(control.Handle, WM_SETREDRAW, 1, 0);
            if (callRedraw) control.Refresh();
        }
        public static void ClearContent(this Control c)
        {
            Type t = c.GetType();
            if (c is ListView lv) lv.Items.Clear();
            else if (t == typeof(TextBox) || t == typeof(MaskedTextBox)) c.Text = "";
            else if (c is ComboBox cbb)
            {
                if (cbb.Items.Count > 0) cbb.SelectedIndex = 0;
                else cbb.Text = "";
            }
            else if (c is GroupBox gpb)
            {
                foreach (Control child in gpb.Controls) child.ClearContent();
            }
        }
        public static int ParseInt(this Control src, int def = 0)
        {
            if (int.TryParse(src.Text, out int value)) return value;
            return def;
        }
        #endregion



        #region ListBox
        public static void InsertAt(this ListBox dest, object item, ref bool locker)
        {
            locker = true;
            int index = dest.SelectedIndex;
            if (index >= 0) dest.Items.Insert(index + 1, item);
            else dest.Items.Add(item);
            locker = false;
            dest.SelectedIndex = index + 1;
        }
        public static void Add(this ListBox dest, object obj, ref bool locker)
        {
            locker = true;
            dest.Items.Add(obj);
            locker = false;
            dest.SelectedItem = obj;
        }
        public static void UpdateAt(this ListBox dest, object value, ref bool locker)
        {
            locker = true;
            int index = dest.SelectedIndex;
            if (index < 0)
            {
                locker = false;
                return;
            }
            dest.Items.RemoveAt(index);
            dest.Items.Insert(index, value);
            dest.SelectedIndex = index;
            locker = false;
        }
        public static void UpdateAt(this ListBox dest, object value, int index, ref bool locker)
        {
            locker = true;
            int prev = dest.SelectedIndex;
            if (index < 0)
            {
                locker = false;
                return;
            }
            dest.Items.RemoveAt(index);
            dest.Items.Insert(index, value);
            dest.SelectedIndex = prev;
            locker = false;
        }
        public static void RepositionCurrentItemTo(this ListBox dest, int targetDest)
        {
            if (dest.SelectedItem is object obj)
            {
                int index = dest.SelectedIndex;
                dest.Items.RemoveAt(index);
                dest.Items.Insert(targetDest, obj);
                dest.SelectedIndex = targetDest;
            }
        }
        public static void LoadAs(this ListBox dest, params object[] src)
        {
            dest.Items.Clear();
            dest.BeginUpdate();
            if (src != null && src.Count() > 0) dest.Items.AddRange(src.ToArray());
            dest.EndUpdate();
        }
        public static void RemoveAt(this ListBox dest, ref bool locker)
        {
            int index = dest.SelectedIndex;
            locker = true;
            dest.Items.RemoveAt(index);
            locker = false;
            if (index != 0) dest.SelectedIndex = index - 1;
            else if (dest.Items.Count > 0) dest.SelectedIndex = 0;
            else dest.SelectedItem = null;
        }
        public static void RemoveAt(this ListBox dest, int index, ref bool locker)
        {
            locker = true;
            dest.Items.RemoveAt(index);
            locker = false;
            if (index != 0) dest.SelectedIndex = index - 1;
            else if (dest.Items.Count > 0) dest.SelectedIndex = 0;
            else dest.SelectedItem = null;
        }
        #endregion



        #region ComboBox
        public static void AutoDropdownWidth(this ComboBox cbb)
        {
            if (cbb != null && cbb.Items.Count != 0)
            {
                int max = cbb.Width;
                foreach (var i in cbb.Items)
                    max = i.ToString().Length * 7 > max ? i.ToString().Length * 7 : max;
                cbb.DropDownWidth = max;
            }
        }
        public static void SelectFirst<T>(this ComboBox dest, Func<T, bool> predicate)
        {
            IEnumerable<T> target = dest.Items.Cast<T>().Where(predicate);
            if (target.Count() != 0) dest.SelectedItem = target.First();
        }
        public static void UpdateTextAs(this ComboBox dest, string text)
        {
            dest.SelectedItem = null;
            dest.Text = text;
        }
        public static void LoadAs(this ComboBox dest, object first, IEnumerable<object> src)
        {
            dest.Items.Clear();
            dest.BeginUpdate();
            if (first != null) dest.Items.Add(first);
            if (src != null)
            {
                dest.Items.AddRange(src.ToArray());
            }
            dest.AutoDropdownWidth();
            dest.EndUpdate();
            dest.SelectedText = string.Empty;
        }
        public static void LoadAs(this ComboBox dest, IEnumerable<object> src)
        {
            dest.Items.Clear();
            dest.BeginUpdate();
            if (src != null)
            {
                dest.Items.AddRange(src.ToArray());
            }
            dest.AutoDropdownWidth();
            dest.EndUpdate();
            dest.SelectedText = string.Empty;
        }
        #endregion




        #region TextBoxType
        public static int TrimValue(this MaskedTextBox src, int floor, int ceil)
        {
            if (int.TryParse(src.Text, out int value))
            {
                if (value < floor) return floor;
                if (value > ceil) return ceil;
                return value;
            }
            return floor;
        }
        #endregion


        #region TreeNode
        public static TreeNode GetRoot(this TreeNode node)
        {
            TreeNode result;
            result = node;
            while (result.Parent != null)
            {
                result = result.Parent;
            }
            return result;
        }
        public static void LoadAs(this TreeNode dest, IEnumerable<TreeNode> src)
        {
            dest.Nodes.AddRange(src.ToArray());
        }
        public static void LoadAs(this TreeNode dest, params TreeNode[] src)
        {
            foreach (TreeNode n in src) dest.Nodes.Add(n);
        }
        public static void LoadAs(this TreeView dest, params TreeNode[] nodes)
        {
            dest.Nodes.AddRange(nodes);
        }
        #endregion



        #region ListView
        public static void LoadAs(this ListView dest, IEnumerable<ListViewItem> src)
        {
            dest.Items.Clear();
            dest.BeginUpdate();
            if (src != null && src.Count() > 0) dest.Items.AddRange(src.ToArray());
            dest.EndUpdate();
        }
        public static void UpdateAt(this ListView dest, ListViewItem item, ref bool locker)
        {
            locker = true;
            int index = dest.SelectedIndices[0];
            dest.Items.RemoveAt(index);
            dest.Items.Insert(index, item);
            dest.SelectedIndices.Clear();
            dest.SelectedIndices.Add(index);
            locker = false;
        }
        public static void RemoveAt(this ListView dest, int index, ref bool locker)
        {
            locker = true;
            dest.SelectedIndices.Clear();
            dest.Items.RemoveAt(index);
            locker = false;
            if (index != 0) dest.SelectedIndices.Add(index - 1);
            else if (dest.Items.Count > 0) dest.SelectedIndices.Add(0);
        }
        #endregion
    }
}
