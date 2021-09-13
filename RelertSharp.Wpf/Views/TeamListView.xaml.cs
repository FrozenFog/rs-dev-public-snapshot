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
using RelertSharp.Wpf.Common;
using System.Windows.Threading;
using RelertSharp.Wpf.Dialogs;

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// TeamListView.xaml 的交互逻辑
    /// </summary>
    public partial class TeamListView : UserControl, IListContainer, IRsView
    {
        public GuiViewType ViewType { get { return GuiViewType.TeamList; } }
        public AvalonDock.Layout.LayoutAnchorable ParentAncorable { get; set; }
        public AvalonDock.Layout.LayoutDocument ParentDocument { get; set; }
        private MapStructure.Map Map { get { return GlobalVar.GlobalMap; } }
        private IndexableDisplayType displayType = IndexableDisplayType.IdAndName;
        private TeamListVm SelectedItem { get { return lbxMain.SelectedItem as TeamListVm; } }

        public TeamListView()
        {
            InitializeComponent();
            DataContext = GlobalCollectionVm.Teams;
            dragTeam = new DragDropHelper<TeamItem, TeamListVm>(lbxMain);
            GlobalVar.MapDocumentLoaded += MapReloadedHandler;
            NavigationHub.GoToTeamRequest += SelectItem;
            NavigationHub.BindTeamList(this);
        }

        private void MapReloadedHandler()
        {
            GlobalVar.Log.Info("Reading Teams");
            //lbxMain.ItemsSource = null;
            //lbxMain.ItemsSource = GlobalCollectionVm.Teams;
            lbxMain.Items.Clear();
            foreach (TeamItem t in Map.Teams)
            {
                lbxMain.Items.Add(new TeamListVm(t));
            }
            GlobalVar.Log.Info("Teams Loaded");
        }

        public event ContentCarrierHandler ItemSelected;

        private void IdUnchecked(object sender, RoutedEventArgs e)
        {
            displayType = IndexableDisplayType.NameOnly;
            foreach (TeamListVm vm in lbxMain.Items) vm.ChangeDisplay(displayType);
            GlobalVar.GlobalMap?.Teams.ChangeDisplay(displayType);
            lbxMain.Items.Refresh();
        }

        private void IdChecked(object sender, RoutedEventArgs e)
        {
            displayType = IndexableDisplayType.IdAndName;
            foreach (TeamListVm vm in lbxMain.Items) vm.ChangeDisplay(displayType);
            GlobalVar.GlobalMap?.Teams.ChangeDisplay(displayType);
            lbxMain.Items.Refresh();
        }

        private void AscendingSort(object sender, RoutedEventArgs e)
        {
            List<TeamListVm> src = lbxMain.Items.Cast<TeamListVm>().ToList();
            src = src.OrderBy(x => x.Title).ToList();
            lbxMain.Items.Clear();
            src.ForEach(x => lbxMain.Items.Add(x));
        }

        private void DescendingSort(object sender, RoutedEventArgs e)
        {
            List<TeamListVm> src = lbxMain.Items.Cast<TeamListVm>().ToList();
            src = src.OrderByDescending(x => x.Title).ToList();
            lbxMain.Items.Clear();
            src.ForEach(x => lbxMain.Items.Add(x));
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

        private void SelectedItemChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbxMain.SelectedItem is TeamListVm vm)
            {
                ItemSelected?.Invoke(this, vm.Data);
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
            if (lbxMain.SelectedItem is TeamListVm vm)
            {
                return vm.Data;
            }
            return null;
        }


        #region Drag Drop
        private DragDropHelper<TeamItem, TeamListVm> dragTeam;

        private void DragMouseMove(object sender, MouseEventArgs e)
        {
            if (dragTeam.IsDraging && e.LeftButton == MouseButtonState.Pressed)
            {
                Point current = e.GetPosition(lbxMain);
                dragTeam.MouseMoveDrag(current);
            }
        }

        private void DragMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                dragTeam.BeginDrag(e.GetPosition(lbxMain));
                dragTeam.SetReferanceVm(lbxMain.GetItemAtMouse<TeamListVm, TextBlock>(e));
                dragTeam.SetDragItem(dragTeam.ReferanceVm?.Data);
                e.Handled = true;
            }
        }
        private void DragMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                dragTeam.EndDrag();
                if (dragTeam.DragItem != null) dragTeam.ReferanceVm.IsSelected = true;
            }
        }
        private void DragMouseLeave(object sender, MouseEventArgs e)
        {
            dragTeam.EndDrag();
        }
        #endregion


        #region Menu
        private void PreviewRightDown(object sender, MouseButtonEventArgs e)
        {
            bool hasItem = SelectedItem != null;
            menuCopy.IsEnabled = hasItem;
            menuDel.IsEnabled = hasItem;
        }
        private void Menu_Add(object sender, RoutedEventArgs e)
        {
            DlgNameInput dlg = new DlgNameInput("Team name");
            if (dlg.ShowDialog().Value)
            {
                var team = Map.AddTeam(dlg.ResultName);
                team.ChangeDisplay(displayType);
                TeamListVm vm = new TeamListVm(team);
                if (lbxMain.SelectedIndex == -1) lbxMain.Items.Add(vm);
                else lbxMain.Items.Insert(lbxMain.SelectedIndex + 1, vm);
                lbxMain.SelectedItem = vm;
            }
        }

        private void Menu_Remove(object sender, RoutedEventArgs e)
        {
            var team = SelectedItem.Data;
            if (Map.RemoveTeam(team))
            {
                lbxMain.Items.Remove(SelectedItem);
            }
        }

        private void Menu_Copy(object sender, RoutedEventArgs e)
        {
            if (SelectedItem != null)
            {
                var copy = Map.AddTeam(SelectedItem.Data);
                copy.ChangeDisplay(displayType);
                TeamListVm vm = new TeamListVm(copy);
                lbxMain.Items.Insert(lbxMain.SelectedIndex, vm);
                lbxMain.SelectedItem = vm;
            }
        }
        #endregion
    }
}
