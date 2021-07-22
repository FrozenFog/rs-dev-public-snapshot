using AvalonDock.Layout;
using RelertSharp.Common;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Wpf.Common;
using RelertSharp.Wpf.ViewModel;
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

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// GlobalSearchView.xaml 的交互逻辑
    /// </summary>
    public partial class GlobalSearchView : UserControl, IRsView
    {
        public GuiViewType ViewType => GuiViewType.GlobalSearch;

        public LayoutAnchorable ParentAncorable { get; set; }
        public LayoutDocument ParentDocument { get; set; }
        private SearchConditionVm Root = new SearchConditionVm(true);
        private SearchConditionVm SelectedCondition { get { return trvConditions.SelectedItem as SearchConditionVm; } }
        private List<object> SearchResult = new List<object>();
        public GlobalSearchView()
        {
            InitializeComponent();
            dragCond = new DragDropHelper<SearchConditionVm.SearchConditionModel, SearchConditionVm>(trvConditions);
            trvConditions.Items.Add(Root);
        }


        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var map = GlobalVar.CurrentMapDocument.Map;
            List<object> results = new List<object>();
            results = results
                .Concat(SearchIn(map.Buildings))
                .Concat(SearchIn(map.Infantries))
                .Concat(SearchIn(map.Units))
                .Concat(SearchIn(map.Aircrafts))
                .Concat(SearchIn(map.Celltags))
                .Concat(SearchIn(map.Terrains))
                .Concat(SearchIn(map.Smudges))
                .Concat(SearchIn(map.Triggers))
                .Concat(SearchIn(map.Teams))
                .Concat(SearchIn(map.Taskforces))
                .Concat(SearchIn(map.Scripts))
                .Concat(SearchIn(map.Tags))
                .Concat(SearchIn(map.Waypoints)).ToList();
            SearchResult = results;
        }
        private void btnSearchIn_Click(object sender, RoutedEventArgs e)
        {
            List<object> results = SearchIn(SearchResult);
            SearchResult = results;
        }


        #region Search
        private List<object> SearchIn(IEnumerable<object> src)
        {
            List<object> results = new List<object>();
            var map = GlobalVar.CurrentMapDocument.Map;
            foreach (object obj in src)
            {
                if (obj is TriggerItem trg)
                {
                    bool eventValid = false;
                    foreach (var evnt in trg.Events)
                    {
                        eventValid = eventValid || Root.IsValidObject(evnt);
                    }
                    bool actionValid = false;
                    foreach (var act in trg.Actions)
                    {
                        actionValid = actionValid || Root.IsValidObject(act);
                    }
                    if (eventValid || actionValid) results.Add(trg);
                }
                else if (obj is TeamScriptGroup script)
                {
                    bool scriptValid = false;
                    foreach (var item in script.Data)
                    {
                        scriptValid = scriptValid || Root.IsValidObject(item);
                    }
                    if (scriptValid) results.Add(script);
                }
                else
                {
                    if (Root.IsValidObject(obj)) results.Add(obj);
                }
            }
            return results;
        }
        #endregion
        #region Menu
        #region Conditions
        private void PreviewRightDown(object sender, MouseButtonEventArgs e)
        {
            if (SelectedCondition != null) SelectedCondition.IsSelected = false;
            SearchConditionVm item = trvConditions.GetItemAtMouse<SearchConditionVm, TextBlock>(e);
            if (item != null) item.IsSelected = true;
            PreviewCondMenu();
        }
        private void PreviewCondMenu()
        {
            if (SelectedCondition != null)
            {
                bool canDel = !SelectedCondition.IsRoot;
                menuDel.IsEnabled = canDel;
            }
            else
            {
                menuDel.IsEnabled = false;
            }
        }

        private void Menu_AddCond(object sender, RoutedEventArgs e)
        {
            var vm = new SearchConditionVm();
            if (SelectedCondition == null || SelectedCondition.IsRoot)
            {
                Root.AddItem(vm);
            }
            else
            {
                if (SelectedCondition.IsTree) SelectedCondition.AddItem(vm);
                else SelectedCondition.Ancestor.AddItem(vm);
            }
        }

        private void Menu_AddGroup(object sender, RoutedEventArgs e)
        {
            var vm = new SearchConditionVm(true);
            if (SelectedCondition == null || SelectedCondition.IsRoot) Root.AddItem(vm);
            else
            {
                if (SelectedCondition.IsTree) SelectedCondition.AddItem(vm);
                else SelectedCondition.Ancestor.AddItem(vm);
            }
        }

        private void Menu_RemoveCond(object sender, RoutedEventArgs e)
        {
            if (!SelectedCondition.IsRoot)
            {
                SelectedCondition.RemoveFromAncestor();
            }
        }
        #endregion

        #endregion

        #region DragDrop
        private DragDropHelper<SearchConditionVm.SearchConditionModel, SearchConditionVm> dragCond;
        private void CondDragMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                dragCond.BeginDrag(e.GetPosition(trvConditions));
                dragCond.SetReferanceVm(trvConditions.GetItemAtMouse<SearchConditionVm, StackPanel>(e));
                dragCond.SetDragItem(dragCond.ReferanceVm.Data);
                e.Handled = true;
            }
        }

        private void CondDragMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                dragCond.EndDrag();
                if (dragCond.DragItem != null) dragCond.ReferanceVm.IsSelected = true;
            }
        }

        private void CondDragDrop(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
            e.Handled = true;
            var vm = dragCond.ReferanceVm;
            SearchConditionVm target = e.GetContext<SearchConditionVm, TextBlock>();
            if (vm != null)
            {
                if (target == null || target.Equals(Root))
                {
                    vm.RemoveFromAncestor();
                    Root.AddItem(vm);
                }
                else if (target.IsDescendantOf(vm)) return;
                else if (target.IsTree)
                {
                    vm.RemoveFromAncestor();
                    target.AddItem(vm);
                }
                else if (!target.IsTree)
                {
                    vm.RemoveFromAncestor();
                    target.Ancestor.AddItem(vm);
                }
            }
        }

        private void CondDragMove(object sender, MouseEventArgs e)
        {
            if (dragCond.IsDraging && e.LeftButton== MouseButtonState.Pressed)
            {
                Point current = e.GetPosition(trvConditions);
                dragCond.MouseMoveDrag(current);
            }
        }

        private void CondDragLeave(object sender, MouseEventArgs e)
        {
            dragCond.EndDrag();
        }
        private void CondDragOver(object sender, DragEventArgs e)
        {
            Point current = e.GetPosition(trvConditions);
            dragCond.MouseMoveDrag(current);
        }
        #endregion
    }
}
