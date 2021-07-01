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
using System.Collections.ObjectModel;
using RelertSharp.Wpf.Dialogs;
using RelertSharp.MapStructure.Logic;

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// ScriptListView.xaml 的交互逻辑
    /// </summary>
    public partial class ScriptListView : UserControl, IListContainer, IRsView
    {
        public GuiViewType ViewType { get { return GuiViewType.ScriptList; } }
        public AvalonDock.Layout.LayoutAnchorable ParentAncorable { get; set; }
        public AvalonDock.Layout.LayoutDocument ParentDocument { get; set; }
        private MapStructure.Map Map { get { return GlobalVar.CurrentMapDocument.Map; } }
        private ScriptListVm SelectedItem { get { return lbxMain.SelectedItem as ScriptListVm; } }
        private IndexableDisplayType displayType = IndexableDisplayType.IdAndName;

        public ScriptListView()
        {
            InitializeComponent();
            DataContext = GlobalCollectionVm.Scripts;
            GlobalVar.MapDocumentLoaded += MapReloadedHandler;
            NavigationHub.GoToScriptRequest += SelectItem;
            NavigationHub.BindScriptList(this);
            dragScript = new DragDropHelper<TeamScriptGroup, ScriptListVm>(lbxMain);
        }

        private void MapReloadedHandler(object sender, EventArgs e)
        {
            lbxMain.Items.Clear();
            foreach (var script in GlobalVar.CurrentMapDocument.Map.Scripts)
            {
                lbxMain.Items.Add(new ScriptListVm(script));
            }
        }


        #region Private
        #endregion

        public event ContentCarrierHandler ItemSelected;

        private void IdUnchecked(object sender, RoutedEventArgs e)
        {
            displayType = IndexableDisplayType.NameOnly;
            foreach (ScriptListVm script in lbxMain.Items)
            {
                script.ChangeDisplay(displayType);
            }
            GlobalVar.CurrentMapDocument?.Map.Scripts.ChangeDisplay(displayType);
            lbxMain.Items.Refresh();
        }

        private void IdChecked(object sender, RoutedEventArgs e)
        {
            displayType = IndexableDisplayType.IdAndName;
            foreach (ScriptListVm script in lbxMain.Items)
            {
                script.ChangeDisplay(displayType);
            }
            GlobalVar.CurrentMapDocument?.Map.Scripts.ChangeDisplay(displayType);
            lbxMain.Items.Refresh();
        }

        private void AscendingSort(object sender, RoutedEventArgs e)
        {
            List<ScriptListVm> src = lbxMain.Items.Cast<ScriptListVm>().ToList();
            src = src.OrderBy(x => x.Title).ToList();
            lbxMain.Items.Clear();
            src.ForEach(x => lbxMain.Items.Add(x));
        }

        private void DescendingSort(object sender, RoutedEventArgs e)
        {
            List<ScriptListVm> src = lbxMain.Items.Cast<ScriptListVm>().ToList();
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
            if (lbxMain.SelectedItem is ScriptListVm script)
            {
                ItemSelected?.Invoke(this, script.Data);
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
            if (lbxMain.SelectedItem is ScriptListVm vm) return vm.Data;
            return null;
        }


        #region Mouse
        private void PreviewRightDown(object sender, MouseButtonEventArgs e)
        {
            bool hasItem = SelectedItem != null;
            menuCopy.IsEnabled = hasItem;
            menuDelete.IsEnabled = hasItem;
        }
        #endregion


        #region Menu
        private void Menu_Add(object sender, RoutedEventArgs e)
        {
            DlgNameInput dlg = new DlgNameInput("Script name");
            if (dlg.ShowDialog().Value)
            {
                var sc = Map.AddScript(dlg.ResultName);
                sc.ChangeDisplay(displayType);
                ScriptListVm vm = new ScriptListVm(sc);
                if (lbxMain.SelectedIndex == -1) lbxMain.Items.Add(vm);
                else lbxMain.Items.Insert(lbxMain.SelectedIndex + 1, vm);
                lbxMain.SelectedItem = vm;
            }
        }

        private void Menu_Copy(object sender, RoutedEventArgs e)
        {
            if (SelectedItem != null)
            {
                var copy = Map.AddScript(SelectedItem.Data);
                copy.ChangeDisplay(displayType);
                ScriptListVm vm = new ScriptListVm(copy);
                lbxMain.Items.Insert(lbxMain.SelectedIndex, vm);
                lbxMain.SelectedItem = vm;
            }
        }

        private void Menu_Delete(object sender, RoutedEventArgs e)
        {
            var sc = SelectedItem.Data;
            if (Map.RemoveScript(sc))
            {
                lbxMain.Items.Remove(SelectedItem);
            }
        }
        #endregion

        #region DragDrop
        private DragDropHelper<TeamScriptGroup, ScriptListVm> dragScript;
        private void DragMouseMove(object sender, MouseEventArgs e)
        {
            if (dragScript.IsDraging && e.LeftButton == MouseButtonState.Pressed)
            {
                Point current = e.GetPosition(lbxMain);
                dragScript.MouseMoveDrag(current);
            }
        }

        private void DragMouseLeave(object sender, MouseEventArgs e)
        {
            dragScript.EndDrag();
        }
        private void DragMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                dragScript.BeginDrag(e.GetPosition(lbxMain));
                dragScript.SetReferanceVm(lbxMain.GetItemAtMouse<ScriptListVm, TextBlock>(e));
                dragScript.SetDragItem(dragScript.ReferanceVm.Data);
                e.Handled = true;
            }
        }

        private void DragMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                dragScript.EndDrag();
                if (dragScript.DragItem != null) dragScript.ReferanceVm.IsSelected = true;
            }
        }
        #endregion
    }
}
