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
using RelertSharp.MapStructure.Logic;
using RelertSharp.Wpf.ViewModel;
using RelertSharp.Common;

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// TeamListView.xaml 的交互逻辑
    /// </summary>
    public partial class TeamListView : UserControl
    {
        public TeamListView()
        {
            InitializeComponent();
            DataContext = GlobalCollectionVm.Teams;
        }
        private void RefreshContext()
        {
            DataContext = null;
            DataContext = GlobalCollectionVm.Teams;
        }

        private void TeamIdUnchecked(object sender, RoutedEventArgs e)
        {
            GlobalVar.CurrentMapDocument?.Map.Teams.ChangeDisplay(IndexableDisplayType.NameOnly);
            GlobalCollectionVm.UpdateTeamsAll();
        }

        private void TeamIdChecked(object sender, RoutedEventArgs e)
        {
            GlobalVar.CurrentMapDocument?.Map.Teams.ChangeDisplay(IndexableDisplayType.IdAndName);
            GlobalCollectionVm.UpdateTeamsAll();
        }

        private void AscendingSort(object sender, RoutedEventArgs e)
        {
            GlobalVar.CurrentMapDocument?.Map.Teams.AscendingSort();
            GlobalCollectionVm.UpdateTeamsAll();
        }

        private void DescendingSort(object sender, RoutedEventArgs e)
        {
            GlobalVar.CurrentMapDocument?.Map.Teams.DescendingSort();
            GlobalCollectionVm.UpdateTeamsAll();
        }
    }
}
