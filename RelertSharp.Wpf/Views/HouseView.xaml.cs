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
using RelertSharp.Common;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Wpf.Common;
using RelertSharp.Wpf.Dialogs;
using RelertSharp.Wpf.ViewModel;

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// CountryHouseView.xaml 的交互逻辑
    /// </summary>
    public partial class CountryHouseView : UserControl, IRsView
    {
        public GuiViewType ViewType => GuiViewType.HousePanel;
        public AvalonDock.Layout.LayoutAnchorable ParentAncorable { get; set; }
        public AvalonDock.Layout.LayoutDocument ParentDocument { get; set; }
        private HouseListVm SelectedHouse { get { return lbxHouse.SelectedItem as HouseListVm; } }
        private Map Map { get { return GlobalVar.GlobalMap; } }

        public CountryHouseView()
        {
            InitializeComponent();
            GlobalVar.MapDocumentLoaded += MapReloadedHandler;
            dragAllies = new DragDropHelper<HouseItem, HouseListVm>(lbxHouse);
        }

        private void MapReloadedHandler()
        {
            //lbxHouse.ItemsSource = null;
            //lbxHouse.ItemsSource = GlobalCollectionVm.Houses;
            GlobalVar.Log.Info("Loading House");
            lbxHouse.Items.Clear();
            foreach (HouseItem house in GlobalVar.GlobalMap.Houses)
            {
                lbxHouse.Items.Add(new HouseListVm(house));
            }

            cbbInherit.Items.Clear();
            INIEntity rulesCountries = GlobalVar.GlobalRules[Constant.RulesHead.HEAD_COUNTRY];
            foreach (INIPair p in rulesCountries)
            {
                cbbInherit.Items.Add(new ComboItem(p.Value.ToString()));
            }

            cbbColor.Items.Clear();
            INIEntity rulesColors = GlobalVar.GlobalRules["Colors"];
            foreach (INIPair p in rulesColors)
            {
                cbbColor.Items.Add(new ComboItem(p.Name));
            }
            GlobalVar.Log.Info("House Loaded");
        }



        #region Handler
        private void HouseSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbxHouse.SelectedItem is HouseListVm lsHouse)
            {
                if (GlobalVar.GlobalMap?.Countries.GetCountry(lsHouse.Data.Country) is CountryItem country)
                {
                    DataContext = new HouseVm(lsHouse.Data, country);
                }
            }
        }
        #endregion


        #region Dragdrop
        private DragDropHelper<HouseItem, HouseListVm> dragAllies;
        #region Begin Drag from lbxHouse
        private void PreviewLeftDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                dragAllies.BeginDrag(e.GetPosition(lbxAllies));
                dragAllies.SetReferanceVm(GetItemAtMouse(lbxHouse, e));
                dragAllies.SetDragItem(dragAllies.ReferanceVm?.Data);
                e.Handled = true;
            }
        }
        private void DragMouseMove(object sender, MouseEventArgs e)
        {
            if (dragAllies.IsDraging && e.LeftButton == MouseButtonState.Pressed)
            {
                Point current = e.GetPosition(lbxAllies);
                dragAllies.MouseMoveDrag(current);
            }
        }
        private void DragMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                dragAllies.EndDrag();
                if (dragAllies.DragItem != null) dragAllies.ReferanceVm.IsSelected = true;
            }
        }
        private void DragMouseLeave(object sender, MouseEventArgs e)
        {
            dragAllies.EndDrag();
        }

        private HouseListVm GetItemAtMouse(ItemsControl src, MouseButtonEventArgs e)
        {
            TextBlock item = src.GetItemControlAtMouse<TextBlock>(e);
            if (item != null) return item.DataContext as HouseListVm;
            return null;
        }
        #endregion

        #region Drop on allies
        private void AcceptDroppedAllies(object sender, DragEventArgs e)
        {
            if (dragAllies.GetDragObject(e, out HouseItem house) && DataContext is HouseVm target)
            {
                target.AddAlly(house.Name);
                lbxAllies.Items.Refresh();
            }
        }
        #endregion

        #endregion

        private void PrevRightDown(object sender, MouseButtonEventArgs e)
        {
            bool hasItem = SelectedHouse != null;
            menuRemove.IsEnabled = hasItem;
        }

        private void Menu_AddHouse(object sender, RoutedEventArgs e)
        {
            DlgNameInput dlg = new DlgNameInput("House name");
            if (dlg.ShowDialog().Value)
            {
                Map.AddHouse(dlg.ResultName, out HouseItem h, out CountryItem c);
                HouseListVm listvm = new HouseListVm(h);
                lbxHouse.Items.Add(listvm);
            }
        }

        private void Menu_RemoveHouse(object sender, RoutedEventArgs e)
        {
            if (GuiUtil.YesNoWarning("Remove a house may cause unexpected error.\nMake sure you know how many items referanced this house.\nAre you sure to remove?"))
            {
                DlgDangerousCommit commit = new DlgDangerousCommit(SelectedHouse.Data.Name);
                if (commit.ShowDialog().Value)
                {
                    Map.RemoveHouse(SelectedHouse.Data);
                    lbxHouse.Items.Remove(SelectedHouse);
                }
            }
        }

        private void AlliesDelDown(object sender, KeyEventArgs e)
        {

        }
    }
}
