using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using relert_sharp.IniSystem;
using relert_sharp.MapStructure.Logic;
using relert_sharp.Common;

namespace relert_sharp.SubWindows.LogicEditor
{
    internal static class StaticHelper
    {
        public static void LoadToObjectCollection<T>(ComboBox dest, IList<T> src)
        {
            int max = src.Max(x => x.ToString().Length) * 7;
            dest.DataSource = src;
            dest.DropDownWidth = max;
        }
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
                dest.Text = "0";
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
                dest.Text = "0";
            }
        }
    }
}
