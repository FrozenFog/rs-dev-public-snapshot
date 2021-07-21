using AvalonDock.Layout;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Wpf.Common;
using RelertSharp.Wpf.ViewModel;
using RelertSharp.Common;
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
using RelertSharp.Wpf.Dialogs;
using RelertSharp.MapStructure;

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// LocalVarView.xaml 的交互逻辑
    /// </summary>
    public partial class LocalVarView : UserControl, IRsView
    {
        public GuiViewType ViewType => GuiViewType.LocalVarPanel;

        public LayoutAnchorable ParentAncorable { get; set; }
        public LayoutDocument ParentDocument { get; set; }
        private LocalVarVm SelectedItem { get { return lbxMain.SelectedItem as LocalVarVm; } }
        private Map Map { get { return GlobalVar.CurrentMapDocument.Map; } }
        public LocalVarView()
        {
            InitializeComponent();
            GlobalVar.MapDocumentLoaded += MapReloadedHandler;
            DataContext = new LocalVarVm();
            dragDrop = new DragDropHelper<LocalVarItem, LocalVarVm>(lbxMain);
        }

        private void MapReloadedHandler(object sender, EventArgs e)
        {
            foreach (LocalVarItem local in GlobalVar.CurrentMapDocument.Map.LocalVariables)
            {
                lbxMain.Items.Add(new LocalVarVm(local));
            }
        }

        private void SelectedItemChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedItem != null) DataContext = SelectedItem;
            else DataContext = new LocalVarVm();
        }


        #region Menu
        private void PreviewRightDown(object sender, MouseButtonEventArgs e)
        {
            bool hasItem = SelectedItem != null;
            menuRemove.IsEnabled = hasItem;
        }
        private void Menu_Add(object sender, RoutedEventArgs e)
        {
            DlgNameInput dlg = new DlgNameInput("Variable Name");
            if (dlg.ShowDialog().Value)
            {
                var local = Map.AddLocalVar(dlg.ResultName);
                if (local != null)
                {
                    LocalVarVm vm = new LocalVarVm(local);
                    lbxMain.Items.Add(vm);
                    lbxMain.SelectedItem = vm;
                }
            }
        }

        private void Menu_Remove(object sender, RoutedEventArgs e)
        {
            var local = SelectedItem.Data;
            if (Map.RemoveVariable(local))
            {
                lbxMain.Items.Remove(SelectedItem);
            }
        }
        #endregion


        #region Dragdrop
        private DragDropHelper<LocalVarItem, LocalVarVm> dragDrop;

        private void DragMouseMove(object sender, MouseEventArgs e)
        {
            if (dragDrop.IsDraging && e.LeftButton == MouseButtonState.Pressed)
            {
                Point current = e.GetPosition(lbxMain);
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
                dragDrop.BeginDrag(e.GetPosition(lbxMain));
                dragDrop.SetReferanceVm(lbxMain.GetItemAtMouse<LocalVarVm, TextBlock>(e));
                dragDrop.SetDragItem(dragDrop.ReferanceVm.Data);
                e.Handled = true;
            }
        }

        private void DragMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                dragDrop.EndDrag();
                if (dragDrop.DragItem != null) dragDrop.ReferanceVm.IsSelected = true;
            }
        }
        #endregion
    }
}
