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
using RelertSharp.Wpf.Dialogs;
using RelertSharp.MapStructure.Logic;

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
        private MapStructure.Map Map { get { return GlobalVar.GlobalMap; } }
        private TaskforceListVm SelectedItem { get { return lbxMain.SelectedItem as TaskforceListVm; } }
        private IndexableDisplayType displayTypeNow = IndexableDisplayType.IdAndName;

        public TaskforceListView()
        {
            InitializeComponent();
            DataContext = GlobalCollectionVm.Taskforces;
            GlobalVar.MapDocumentLoaded += MapReloadedHandler;
            NavigationHub.GoToTaskforceRequest += SelectItem;
            NavigationHub.BindTaskforceList(this);
            dragTaskforce = new DragDropHelper<TaskforceItem, TaskforceListVm>(lbxMain);
        }

        private void MapReloadedHandler(object sender, EventArgs e)
        {
            lbxMain.Items.Clear();
            foreach (var tf in GlobalVar.GlobalMap.Taskforces)
            {
                lbxMain.Items.Add(new TaskforceListVm(tf));
            }
        }

        public event ContentCarrierHandler ItemSelected;

        private void IdUnchecked(object sender, RoutedEventArgs e)
        {
            displayTypeNow = IndexableDisplayType.NameOnly;
            foreach (TaskforceListVm vm in lbxMain.Items)
            {
                vm.ChangeDisplay(displayTypeNow);
            }
            GlobalVar.GlobalMap?.Taskforces.ChangeDisplay(displayTypeNow);
            lbxMain.Items.Refresh();
        }

        private void IdChecked(object sender, RoutedEventArgs e)
        {
            displayTypeNow = IndexableDisplayType.IdAndName;
            foreach (TaskforceListVm vm in lbxMain.Items)
            {
                vm.ChangeDisplay(displayTypeNow);
            }
            GlobalVar.GlobalMap?.Taskforces.ChangeDisplay(displayTypeNow);
            lbxMain.Items.Refresh();
        }

        private void AscendingSort(object sender, RoutedEventArgs e)
        {
            List<TaskforceListVm> src = lbxMain.Items.Cast<TaskforceListVm>().ToList();
            src = src.OrderBy(x => x.Title).ToList();
            lbxMain.Items.Clear();
            src.ForEach(x => lbxMain.Items.Add(x));
        }

        private void DescendingSort(object sender, RoutedEventArgs e)
        {
            List<TaskforceListVm> src = lbxMain.Items.Cast<TaskforceListVm>().ToList();
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

        public void SelectItem(IIndexableItem item)
        {
            lbxMain.SelectItem(item, (a, b) =>
            {
                return a.Id == b.Id;
            });
        }

        public IIndexableItem GetSelectedItem()
        {
            if (lbxMain.SelectedItem is TaskforceListVm vm)
            {
                return vm.Data;
            }
            return null;
        }

        private void SelectedItemChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbxMain.SelectedItem is TaskforceListVm vm)
            {
                ItemSelected?.Invoke(this, vm.Data);
            }
        }

        #region Mouse
        private void PreviewRightDown(object sender, MouseButtonEventArgs e)
        {
            bool hasItem = lbxMain.SelectedItem != null;
            menuCopy.IsEnabled = hasItem;
            menuDelete.IsEnabled = hasItem;
        }
        #endregion


        #region Menu
        private void Menu_Add(object sender, RoutedEventArgs e)
        {
            DlgNameInput dlg = new DlgNameInput("Taskforce name");
            if (dlg.ShowDialog().Value)
            {
                var tf = Map.AddTaskforce(dlg.ResultName);
                tf.ChangeDisplay(displayTypeNow);
                TaskforceListVm vm = new TaskforceListVm(tf);
                if (lbxMain.SelectedIndex == -1) lbxMain.Items.Add(vm);
                else lbxMain.Items.Insert(lbxMain.SelectedIndex + 1, vm);
                lbxMain.SelectedItem = vm;
            }
        }

        private void Menu_Copy(object sender, RoutedEventArgs e)
        {
            if (SelectedItem != null)
            {
                var copy = Map.AddTaskforce(SelectedItem.Data);
                copy.ChangeDisplay(displayTypeNow);
                TaskforceListVm vm = new TaskforceListVm(copy);
                lbxMain.Items.Insert(lbxMain.SelectedIndex, vm);
                lbxMain.SelectedItem = vm;
            }
        }

        private void Menu_Delete(object sender, RoutedEventArgs e)
        {
            TaskforceListVm vm = SelectedItem;
            var tf = vm.Data;
            if (Map.RemoveTaskforce(tf))
            {
                lbxMain.Items.Remove(vm);
            }
        }
        #endregion


        #region Drag Drop
        private DragDropHelper<TaskforceItem, TaskforceListVm> dragTaskforce;

        private void DragMouseMove(object sender, MouseEventArgs e)
        {
            if (dragTaskforce.IsDraging && e.LeftButton == MouseButtonState.Pressed)
            {
                Point current = e.GetPosition(lbxMain);
                dragTaskforce.MouseMoveDrag(current);
            }
        }

        private void DragMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                dragTaskforce.BeginDrag(e.GetPosition(lbxMain));
                dragTaskforce.SetReferanceVm(lbxMain.GetItemAtMouse<TaskforceListVm, TextBlock>(e));
                dragTaskforce.SetDragItem(dragTaskforce.ReferanceVm?.Data);
                e.Handled = true;
            }
        }
        private void DragMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                dragTaskforce.EndDrag();
                if (dragTaskforce.DragItem != null) dragTaskforce.ReferanceVm.IsSelected = true;
            }
        }
        private void DragMouseLeave(object sender, MouseEventArgs e)
        {
            dragTaskforce.EndDrag();
        }
        #endregion
    }
}
