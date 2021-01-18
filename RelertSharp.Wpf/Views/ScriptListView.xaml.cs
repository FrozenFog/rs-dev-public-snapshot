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
    /// ScriptListView.xaml 的交互逻辑
    /// </summary>
    public partial class ScriptListView : UserControl, IListToolPageContent
    {
        public ScriptListView()
        {
            InitializeComponent();
            DataContext = GlobalCollectionVm.Scripts;
        }

        public event ContentCarrierHandler ItemSelected;

        private void IdUnchecked(object sender, RoutedEventArgs e)
        {
            GlobalVar.CurrentMapDocument?.Map.Scripts.ChangeDisplay(IndexableDisplayType.NameOnly);
            GlobalCollectionVm.Scripts.UpdateAll();
        }

        private void IdChecked(object sender, RoutedEventArgs e)
        {
            GlobalVar.CurrentMapDocument?.Map.Scripts.ChangeDisplay(IndexableDisplayType.IdAndName);
            GlobalCollectionVm.Scripts.UpdateAll();
        }

        private void AscendingSort(object sender, RoutedEventArgs e)
        {
            GlobalVar.CurrentMapDocument?.Map.Scripts.AscendingSort();
            GlobalCollectionVm.Scripts.UpdateAll();
        }

        private void DescendingSort(object sender, RoutedEventArgs e)
        {
            GlobalVar.CurrentMapDocument?.Map.Scripts.DescendingSort();
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
