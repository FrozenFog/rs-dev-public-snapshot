using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RelertSharp.Utils
{
    public class Lang
    {
        private Dictionary<string, string> dict = new Dictionary<string, string>();
        public Lang(Dictionary<string, string> src)
        {
            dict = src;
        }
        public string this[string key]
        {
            get
            {
                if (dict.Keys.Contains(key))
                {
                    string result = dict[key];
                    if (result.Contains("\\"))
                    {
                        result = result.Replace("\\n", "\n");
                        result = result.Replace("\\r", "\r");
                    }
                    return result;
                }
                return key;
            }
        }
    }
}
namespace RelertSharp
{
    public static class Language
    {
        private static Utils.Lang translate;
        public static Utils.Lang DICT
        {
            get { return translate; }
            set { translate = value; }
        }
        //public static void SetControlLanguage(Control p)
        //{
        //    var t = p.GetType();
        //    if (t == typeof(GroupBox))
        //    {
        //        foreach (Control c in ((GroupBox)p).Controls)
        //        {
        //            SetControlLanguage(c);
        //        }
        //    }
        //    else if (t == typeof(Panel))
        //    {
        //        foreach (Control c in ((Panel)p).Controls)
        //        {
        //            SetControlLanguage(c);
        //        }
        //    }
        //    else if (t == typeof(SplitContainer))
        //    {
        //        foreach (Control c in ((SplitContainer)p).Panel1.Controls) SetControlLanguage(c);
        //        foreach (Control c in ((SplitContainer)p).Panel2.Controls) SetControlLanguage(c);
        //    }
        //    else if (t == typeof(TabPage))
        //    {
        //        foreach (Control c in ((TabPage)p).Controls)
        //        {
        //            SetControlLanguage(c);
        //        }
        //    }
        //    else if (t == typeof(TabControl))
        //    {
        //        foreach (TabPage pg in ((TabControl)p).TabPages)
        //        {
        //            SetControlLanguage(pg);
        //        }
        //    }
        //    else if (t == typeof(ListView))
        //    {
        //        foreach (ColumnHeader col in ((ListView)p).Columns)
        //        {
        //            col.Text = DICT[col.Text];
        //        }
        //    }
        //    else if (t == typeof(DataGridView))
        //    {
        //        foreach (DataGridViewTextBoxColumn col in ((DataGridView)p).Columns)
        //        {
        //            col.HeaderText = DICT[col.HeaderText];
        //        }
        //    }
        //    else if (t == typeof(BrightIdeasSoftware.ObjectListView))
        //    {
        //        foreach (BrightIdeasSoftware.OLVColumn col in ((BrightIdeasSoftware.ObjectListView)p).Columns)
        //        {
        //            col.Text = DICT[col.Text];
        //        }
        //    }
        //    else if (t == typeof(TableLayoutPanel))
        //    {
        //        foreach (Control c in (p as TableLayoutPanel).Controls)
        //        {
        //            SetControlLanguage(c);
        //        }
        //    }
        //    else if (t == typeof(ToolStrip))
        //    {
        //        foreach (ToolStripItem c in (p as ToolStrip).Items)
        //        {
        //            c.Text = DICT[c.Text];
        //        }
        //    }
        //    else if (t == typeof(ContextMenuStrip))
        //    {
        //        foreach (ToolStripMenuItem tsmi in (p as ContextMenuStrip).Items)
        //        {
        //            tsmi.Text = DICT[tsmi.Text];
        //        }
        //    }
        //    if (p.ContextMenuStrip != null)
        //    {
        //        foreach (ToolStripItem item in p.ContextMenuStrip.Items)
        //        {
        //            item.Text = DICT[item.Text];
        //        }
        //    }
        //    p.Text = DICT[p.Text];
        //}
    }
}
