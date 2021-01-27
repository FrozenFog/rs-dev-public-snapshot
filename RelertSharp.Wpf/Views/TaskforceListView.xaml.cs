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

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// TaskforceListView.xaml 的交互逻辑
    /// </summary>
    public partial class TaskforceListView : UserControl, IListContainer
    {
        public TaskforceListView()
        {
            InitializeComponent();
            DataContext = GlobalCollectionVm.Taskforces;
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
    }
}
