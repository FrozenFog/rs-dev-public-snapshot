using RelertSharp.Wpf.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RelertSharp.Wpf.ViewModel;
using RelertSharp.Common;
using RelertSharp.MapStructure.Logic;
using RelertSharp.MapStructure;

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// TriggerListView.xaml 的交互逻辑
    /// </summary>
    public partial class TriggerListView : UserControl, IListContainer, IRsView
    {
        private Map map { get { return GlobalVar.CurrentMapDocument.Map; } }
        public GuiViewType ViewType { get { return GuiViewType.TriggerList; } }

        public TriggerListView()
        {
            InitializeComponent();
            DataContext = GlobalCollectionVm.Triggers;
        }

        public void ReloadMapTrigger()
        {
            trvMain.Items.Clear();
            Regex group = new Regex("\\[.*\\]");
            Dictionary<string, TriggerTreeItemVm> groups = new Dictionary<string, TriggerTreeItemVm>();
            foreach (TriggerItem trg in map.Triggers)
            {
                Match m = group.Match(trg.Name);
                if (m.Success)
                {
                    string groupName = m.Value.Peel();
                    string trgName = trg.Name.Substring(m.Index + m.Length).Trim();
                    TriggerTreeItemVm item = new TriggerTreeItemVm();
                    item.Title = groupName;
                    if (groups.ContainsKey(groupName))
                    {
                        groups[groupName].AddItem(trg, trgName);
                    }
                    else
                    {
                        item.AddItem(trg, trgName);
                        groups[groupName] = item;
                    }
                }
                else
                {
                    TriggerTreeItemVm item = new TriggerTreeItemVm(trg);
                    item.Title = trg.ToString();
                    groups[trg.Name] = item;
                }
            }
            groups.Values.Foreach(x =>
            {
                if (x.IsTree) trvMain.Items.Add(x);
            });
            groups.Values.Foreach(x =>
            {
                if (!x.IsTree) trvMain.Items.Add(x);
            });
        }

        public event ContentCarrierHandler ItemSelected;

        private void SelectedItemChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemSelected?.Invoke(this, trvMain.SelectedItem);
        }

        public void SortBy(bool ascending)
        {
            /// not available
        }

        public void ShowingId(bool enable)
        {
            /// not available
        }
    }
}
