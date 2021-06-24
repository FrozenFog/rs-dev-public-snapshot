using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using RelertSharp.Wpf.Common;

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// TaskforceListView.xaml 的交互逻辑
    /// </summary>
    public partial class TaskforceListView : UserControl, IListContainer, IRsView
    {
        public GuiViewType ViewType { get { return GuiViewType.TaskforceList; } }
        public AvalonDock.Layout.LayoutAnchorable ParentAncorable { get; set; }
        public AvalonDock.Layout.LayoutDocument ParentDocument { get; set; }

        public TaskforceListView()
        {
            InitializeComponent();
            DataContext = GlobalCollectionVm.Taskforces;
            NavigationHub.GoToTaskforceRequest += SelectItem;
            NavigationHub.BindTaskforceList(this);
        }

        public event ContentCarrierHandler ItemSelected;

        private void IdUnchecked(object sender, RoutedEventArgs e)
        {
            GlobalVar.CurrentMapDocument?.Map.Taskforces.ChangeDisplay(IndexableDisplayType.NameOnly);
            GlobalCollectionVm.Taskforces.UpdateAll();
        }

        private void IdChecked(object sender, RoutedEventArgs e)
        {
            GlobalVar.CurrentMapDocument?.Map.Taskforces.ChangeDisplay(IndexableDisplayType.IdAndName);
            GlobalCollectionVm.Taskforces.UpdateAll();
        }

        private void AscendingSort(object sender, RoutedEventArgs e)
        {
            GlobalVar.CurrentMapDocument?.Map.Taskforces.AscendingSort();
            GlobalCollectionVm.Taskforces.UpdateAll();
        }

        private void DescendingSort(object sender, RoutedEventArgs e)
        {
            GlobalVar.CurrentMapDocument?.Map.Taskforces.DescendingSort();
            GlobalCollectionVm.Scripts.UpdateAll();
        }


        public void SortBy(bool ascending)
        {
            if (ascending)
            {
                AscendingSort(null, null);
            }
            else
            {
                DescendingSort(null, null);
            }
        }

        public void ShowingId(bool enable)
        {
            if (enable)
            {
                IdChecked(null, null);
            }
            else
            {
                IdUnchecked(null, null);
            }
        }

        public void SelectItem(IIndexableItem item)
        {
            lbxMain.SelectItem(item, (a, b) =>
            {
                return a.Id == b.Id;
            });
        }

        public IIndexableItem GetSelectedItem()
        {
            return lbxMain.SelectedItem as IIndexableItem;
        }
    }
}
