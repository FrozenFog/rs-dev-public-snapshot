using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;

namespace RelertSharp.SubWindows.LogicEditor
{
    internal static class StaticHelper
    {
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
        public static void LoadToObjectCollection<T>(ComboBox dest, IList<T> src)
        {
            if (src != null && src.Count != 0)
            {
                int max = src.Max(x => x.ToString().Length) * 7;
                dest.DropDownWidth = max;
            }
            dest.DataSource = src;
        }
        public static void LoadToObjectCollection(ComboBox dest, IEnumerable<object> src)
        {
            dest.Items.Clear();
            if (src == null) return;
            dest.Items.AddRange(src.ToArray());
        }
        public static void LoadToObjectCollection(ListBox dest, IEnumerable<object> src)
        {
            dest.Items.Clear();
            if (src == null) return;
            dest.Items.AddRange(src.ToArray());
        }
        public static void SelectCombo(ComboBox dest, string param, TriggerParam lookup)
        {
            switch (lookup.ComboType)
            {
                case TriggerParam.ComboContent.CsfLabel:
                    param = param.ToLower();
                    Select(dest, param);
                    break;
                case TriggerParam.ComboContent.SoundNames:
                case TriggerParam.ComboContent.EvaNames:
                case TriggerParam.ComboContent.ThemeNames:
                case TriggerParam.ComboContent.TechnoType:
                case TriggerParam.ComboContent.BuildingID:
                    Select(dest, param, false);
                    break;
                default:
                    Select(dest, param);
                    break;
            }
        }
        private static void Select(ComboBox dest, string param, bool isIndex = true)
        {
            if (isIndex)
            {
                foreach (TechnoPair p in dest.Items)
                {
                    if (p.Index == param)
                    {
                        dest.SelectedItem = p;
                        return;
                    }
                }
            }
            else
            {
                foreach (TechnoPair p in dest.Items)
                {
                    if (p.RegName == param)
                    {
                        dest.SelectedItem = p;
                        return;
                    }
                }
            }
            dest.Text = param;
        }
    }
}
