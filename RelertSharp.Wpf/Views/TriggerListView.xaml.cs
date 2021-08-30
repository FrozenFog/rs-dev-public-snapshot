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
using RelertSharp.Wpf.Dialogs;
using RelertSharp.Common;
using RelertSharp.MapStructure.Logic;
using RelertSharp.MapStructure;
using System.Windows.Threading;

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// TriggerListView.xaml 的交互逻辑
    /// </summary>
    public partial class TriggerListView : UserControl, IListContainer, IRsView
    {
        private Map map { get { return GlobalVar.GlobalMap; } }
        private TriggerTreeItemVm SelectedItem { get { return trvMain.SelectedItem as TriggerTreeItemVm; } }
        public GuiViewType ViewType { get { return GuiViewType.TriggerList; } }
        public AvalonDock.Layout.LayoutAnchorable ParentAncorable { get; set; }
        public AvalonDock.Layout.LayoutDocument ParentDocument { get; set; }


        #region Ctor
        public TriggerListView()
        {
            InitializeComponent();
            DataContext = GlobalCollectionVm.Triggers;
            dragDrop = new DragDropHelper<TriggerTreeItemVm, TriggerTreeItemVm>(trvMain);
            NavigationHub.GoToTriggerRequest += SelectItem;
            NavigationHub.BindTriggerList(this);
            GlobalVar.MapDocumentLoaded += MapReloadedHandler;
            GlobalVar.MapSaveBegin += CompileTriggerName;
        }

        private void CompileTriggerName()
        {
            foreach (object o in trvMain.Items)
            {
                TriggerTreeItemVm vm = o as TriggerTreeItemVm;
                vm.Compile();
            }
        }

        private void MapReloadedHandler()
        {
            ReloadMapTrigger();
        }

        private TriggerTreeItemVm GetVm(object sender)
        {
            if (sender is FrameworkElement elem && elem.DataContext is TriggerTreeItemVm vm) return vm;
            return null;
        }
        #endregion


        #region Public
        public void SelectItem(IIndexableItem item)
        {
            foreach (object o in trvMain.Items)
            {
                TriggerTreeItemVm vm = (o as TriggerTreeItemVm).Find(item);
                if (vm != null)
                {
                    vm.IsSelected = true;
                    vm.ExpandAllAncestor();
                    return;
                }
            }
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
                    trg.Name = trgName;
                    TriggerTreeItemVm item = new TriggerTreeItemVm();
                    item.SetTitle(groupName);
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
                    item.SetTitle(trg.ToString());
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
        #endregion


        #region IContainer
        public void SortBy(bool ascending)
        {
            List<TriggerTreeItemVm> tree = trvMain.Items.Cast<TriggerTreeItemVm>().ToList();
            trvMain.Items.Clear();
            tree.Foreach(x => x.Sort(ascending));
            if (ascending) tree = tree.OrderBy(x => x.Title).ToList();
            else tree = tree.OrderByDescending(x => x.Title).ToList();
            tree.Foreach(x => { if (x.IsTree) trvMain.Items.Add(x); });
            tree.Foreach(x => { if (!x.IsTree) trvMain.Items.Add(x); });
        }
        /// <summary>
        /// not available
        /// </summary>
        /// <param name="enable"></param>
        public void ShowingId(bool enable)
        {
            throw new NotSupportedException();
        }

        public IIndexableItem GetSelectedItem()
        {
            if (trvMain.SelectedItem is TriggerTreeItemVm vm)
            {
                return vm.Data;
            }
            return null;
        }
        #endregion


        #region Selecting & Right click
        private void SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (SelectedItem != null)
            {
                ItemSelected?.Invoke(this, SelectedItem.Data);
            }
            else ItemSelected?.Invoke(this, null);
        }

        private TriggerTreeItemVm GetItemAtMouse(ItemsControl src, MouseButtonEventArgs e)
        {
            TextBlock treeitem = src.GetItemControlAtMouse<TextBlock>(e);
            if (treeitem != null) return treeitem.DataContext as TriggerTreeItemVm;
            return null;
        }
        private void PreviewRightDown(object sender, MouseButtonEventArgs e)
        {
            if (SelectedItem != null) SelectedItem.IsSelected = false;
            TriggerTreeItemVm item = GetItemAtMouse(trvMain, e);
            if (item != null) item.IsSelected = true;
            //DeselectAll(trvMain);
            //TreeViewItem item = GetSelectedItem(trvMain);
            PreviewMenuShowing();
        }
        private void PreviewMenuShowing()
        {

        }
        #endregion


        #region DragDrop
        private DragDropHelper<TriggerTreeItemVm, TriggerTreeItemVm> dragDrop;

        private void TrgDragDrop(object sender, DragEventArgs e)
        {
            IDataObject data = new DataObject();
            data = e.Data;
            if (data.GetData(typeof(TriggerTreeItemVm)) is TriggerTreeItemVm dragItem && 
                (sender as FrameworkElement).DataContext is TriggerTreeItemVm target)
            {
                if (target != null && !target.IsEqualWith(dragItem) && !target.IsDescendantOf(dragItem))
                {
                    bool sameAncestor = !target.IsTree && (dragItem.Ancestor == target.Ancestor);
                    // remove from ancestor: anything other than a root, and they share same ancestor
                    bool removeFromAncestor = !dragItem.IsRoot && !sameAncestor;
                    // add to target: if target has offspring
                    bool addToTarget = target.IsTree;
                    // add to target's ancestor: target is not tree, not root, not in same ancestor
                    bool addToAncestor = !target.IsTree && !target.IsRoot && !sameAncestor;
                    // remove from TreeView: drag a root item and there's moving action
                    bool removeFromRoot = dragItem.IsRoot && (addToTarget || addToAncestor);

                    if (removeFromAncestor) dragItem.RemoveFromAncestor();
                    if (removeFromRoot) trvMain.Items.Remove(dragItem);
                    if (addToTarget) target.InsertItem(dragItem);
                    if (addToAncestor) target.Ancestor.InsertItem(dragItem);
                }
                else if (target == null && !dragItem.IsRoot)
                {
                    dragItem.RemoveFromAncestor();
                    trvMain.Items.Add(dragItem);
                }
            }
        }

        private void DragMouseMove(object sender, MouseEventArgs e)
        {
            if (dragDrop.IsDraging && e.LeftButton == MouseButtonState.Pressed)
            {
                Point current = e.GetPosition(trvMain);
                dragDrop.MouseMoveDrag(current);
            }
        }

        private void DragMouseLeave(object sender, MouseEventArgs e)
        {
            dragDrop.EndDrag();
        }

        private void DragMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                dragDrop.BeginDrag(e.GetPosition(trvMain));
                dragDrop.SetReferanceVm(GetItemAtMouse(trvMain, e));
                dragDrop.SetDragItem(dragDrop.ReferanceVm);
                e.Handled = true;
            }
        }
        private void DragMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                dragDrop.EndDrag();
                if (dragDrop.ReferanceVm != null) dragDrop.ReferanceVm.IsSelected = true;
            }
        }
        #endregion

        #region Menu
        private void Menu_AddGroup(object sender, RoutedEventArgs e)
        {
            DlgTriggerListAddGroup addGroup = new DlgTriggerListAddGroup();
            if (addGroup.ShowDialog().Value)
            {
                TriggerTreeItemVm group = new TriggerTreeItemVm();
                group.SetTitle(addGroup.ResultName);
                if (GetVm(sender) is TriggerTreeItemVm target)
                {
                    if (target.IsTree) target.AddItem(group);
                    else if (!target.IsRoot) target.Ancestor.AddItem(group);
                    else trvMain.Items.Add(group);
                }
                else trvMain.Items.Add(group);
                group.IsSelected = true;
                group.ExpandAllAncestor();
            }
        }

        private void Menu_DeleteTrigger(object sender, RoutedEventArgs e)
        {
            if (GetVm(sender) is TriggerTreeItemVm item && item.Data is TriggerItem trigger)
            {
                if (map.RemoveTrigger(trigger))
                {
                    if (item.IsRoot) trvMain.Items.Remove(item);
                    else item.RemoveFromAncestor();
                }
            }
        }

        private void Menu_DeleteGroup(object sender, RoutedEventArgs e)
        {
            void remove(TriggerTreeItemVm tree)
            {
                if (tree.IsTree)
                {
                    foreach (TriggerTreeItemVm sub in tree.Items) remove(sub);
                }
                else
                {
                    map.RemoveTrigger(tree.Data);
                }
            }
            if (GetVm(sender) is TriggerTreeItemVm item)
            {
                if (GuiUtil.YesNoWarning("All child trigger and associated tag will be deleted.\nAre you sure?"))
                {
                    remove(item);
                    if (item.IsRoot) trvMain.Items.Remove(item);
                    else item.RemoveFromAncestor();
                }
            }
        }

        private void Menu_RenameGroup(object sender, RoutedEventArgs e)
        {
            if (GetVm(sender) is TriggerTreeItemVm vm)
            {
                DlgNameInput dlg = new DlgNameInput("Group name")
                {
                    InitialName = vm.Title
                };
                if (dlg.ShowDialog().Value)
                {
                    vm.SetTitle(dlg.ResultName);
                }
            }
        }

        private void Menu_MoveToRoot(object sender, RoutedEventArgs e)
        {
            if (GetVm(sender) is TriggerTreeItemVm vm && !vm.IsRoot)
            {
                vm.RemoveFromAncestor();
                trvMain.Items.Add(vm);
            }
        }

        private void Menu_AddTrigger(object sender, RoutedEventArgs e)
        {
            DlgNameInput dlg = new DlgNameInput("Trigger name");
            if (dlg.ShowDialog().Value)
            {
                TriggerItem trigger = map.AddTrigger(dlg.ResultName);
                trigger.Owner = map.Countries.First().Name;
                TriggerTreeItemVm vm = new TriggerTreeItemVm(trigger);

                if (GetVm(sender) is TriggerTreeItemVm target)
                {
                    if (target.IsTree) target.AddItem(vm);
                    else if (!target.IsRoot) target.Ancestor.AddItem(vm);
                    else trvMain.Items.Add(vm);
                }
                else trvMain.Items.Add(vm);
                vm.IsSelected = true;
                vm.ExpandAllAncestor();
            }
        }

        private void Menu_CopyTrigger(object sender, RoutedEventArgs e)
        {
            if (GetVm(sender) is TriggerTreeItemVm item)
            {
                TriggerItem copy = map.AddTrigger(item.Data);
                TriggerTreeItemVm vm = new TriggerTreeItemVm(copy);

                if (item.IsRoot) trvMain.Items.Add(vm);
                else item.Ancestor.AddItem(vm);

                vm.IsSelected = true;
                vm.ExpandAllAncestor();
            }
        }

        private void Menu_Ascending(object sender, RoutedEventArgs e)
        {
            SortBy(true);
        }

        private void Menu_Descending(object sender, RoutedEventArgs e)
        {
            SortBy(false);
        }
        #endregion
    }
}
