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

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// TriggerListView.xaml 的交互逻辑
    /// </summary>
    public partial class TriggerListView : UserControl, IListContainer, IRsView
    {
        private Map map { get { return GlobalVar.CurrentMapDocument.Map; } }
        private TriggerTreeItemVm SelectedItem { get { return trvMain.SelectedItem as TriggerTreeItemVm; } }
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
                    trg.Name = trgName;
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
            List<TriggerTreeItemVm> tree = trvMain.Items.Cast<TriggerTreeItemVm>().ToList();
            trvMain.Items.Clear();
            tree.Foreach(x => x.Sort(ascending));
            if (ascending) tree = tree.OrderBy(x => x.Title).ToList();
            else tree = tree.OrderByDescending(x => x.Title).ToList();
            tree.Foreach(x => { if (x.IsTree) trvMain.Items.Add(x); });
            tree.Foreach(x => { if (!x.IsTree) trvMain.Items.Add(x); });
        }

        public void ShowingId(bool enable)
        {
            /// not available
        }

        #region Misc
        private void PreviewRightDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock b = e.OriginalSource as TextBlock;
            if (b != null)
            {
                bool a = b.Focus();
            }
        }
        #endregion


        #region DragDrop
        private Point prevMouseDown;
        private TriggerTreeItemVm dragItem;
        private void TrgDragOver(object sender, DragEventArgs e)
        {
            Point current = e.GetPosition(trvMain);
            if (RsMath.ChebyshevDistance(current, prevMouseDown) > 10)
            {
                TriggerTreeItemVm src = GetItemOnDrag(e);
                if (src == null) e.Effects = DragDropEffects.Scroll;
                else if (src.Title != dragItem.Title) e.Effects = DragDropEffects.Move;
                else e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private void TrgDragDrop(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
            e.Handled = true;
            TriggerTreeItemVm target = GetItemOnDrag(e);
            if (dragItem != null)
            {
                if (target != null && target.Title != dragItem.Title)
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
                    if (addToTarget) target.AddItem(dragItem);
                    if (addToAncestor) target.Ancestor.AddItem(dragItem);
                    ////e.Effects = DragDropEffects.Move;
                    //
                    //if (dragItem.IsRoot)
                    //{
                    //    // if target item is root too, do nothing
                    //    if (!target.IsRoot) trvMain.Items.Remove(dragItem);
                    //}
                    //// drag a non-root item, remove from ancestor
                    //else dragItem.RemoveFromAncestor();

                    //// target is tree, add to target child
                    //if (target.IsTree) target.AddItem(dragItem);
                    //// target is not tree nor root, add to target's ancestor
                    //else target.Ancestor.AddItem(dragItem);
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
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point current = e.GetPosition(trvMain);
                if (RsMath.ChebyshevDistance(current, prevMouseDown) > 10)
                {
                    dragItem = trvMain.SelectedItem as TriggerTreeItemVm;
                    if (dragItem != null)
                    {
                        DragDropEffects effect = DragDrop.DoDragDrop(trvMain, trvMain.SelectedItem, DragDropEffects.Move);
                    }
                }
            }
        }

        private void DragMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) prevMouseDown = e.GetPosition(trvMain);
        }

        private TriggerTreeItemVm GetItemOnDrag(DragEventArgs e)
        {
            TextBlock b = e.OriginalSource as TextBlock;
            if (b == null) return null;
            return b.DataContext as TriggerTreeItemVm;
        }
        #endregion

        #region Menu
        private void Menu_AddGroup(object sender, RoutedEventArgs e)
        {
            DlgTriggerListAddGroup addGroup = new DlgTriggerListAddGroup();
            if (addGroup.ShowDialog().Value == true)
            {
                TriggerTreeItemVm group = new TriggerTreeItemVm()
                {
                    Title = addGroup.ResultName
                };
                if (trvMain.SelectedItem is TriggerTreeItemVm ancestor)
                {
                    if (addGroup.IsRoot) trvMain.Items.Add(group);
                    else
                    {
                        if (ancestor.IsTree) ancestor.AddItem(group);
                        else if (!ancestor.IsRoot) ancestor.Ancestor.AddItem(group);
                        else trvMain.Items.Add(group);
                    }
                }
                else trvMain.Items.Add(group);
            }
        }

        private void Menu_DeleteTrigger(object sender, RoutedEventArgs e)
        {

        }

        private void Menu_DeleteGroup(object sender, RoutedEventArgs e)
        {

        }

        private void Menu_AddTrigger(object sender, RoutedEventArgs e)
        {

        }

        private void Menu_CopyTrigger(object sender, RoutedEventArgs e)
        {

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




        #region Binder
        public bool IsTrigger
        {
            get
            {
                if (SelectedItem != null) return !SelectedItem.IsTree;
                return false;
            }
        }
        public bool IsTree
        {
            get { return !IsTrigger; }
        }
        #endregion
    }
}
