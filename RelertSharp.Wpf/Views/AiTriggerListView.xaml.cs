﻿using System;
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
using RelertSharp.Wpf.ViewModel;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Wpf.Common;
using AvalonDock.Layout;

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// AiTriggerListView.xaml 的交互逻辑
    /// </summary>
    public partial class AiTriggerListView : UserControl, IListContainer, IRsView
    {
        private GlobalAiTriggerVm context { get { return DataContext as GlobalAiTriggerVm; } }

        public GuiViewType ViewType { get { return GuiViewType.AiTriggerList; } }

        public LayoutAnchorable ParentAncorable { get; set; }
        public LayoutDocument ParentDocument { get; set; }

        public AiTriggerListView()
        {
            InitializeComponent();
            DataContext = GlobalCollectionVm.AiTriggers;
            GlobalVar.MapDocumentLoaded += MapReloadedHandler;
        }

        private void MapReloadedHandler()
        {
            lbxMain.ItemsSource = null;
            lbxMain.ItemsSource = GlobalCollectionVm.AiTriggers;
        }

        public event ContentCarrierHandler ItemSelected;

        private void IdUnchecked(object sender, RoutedEventArgs e)
        {
            GlobalVar.GlobalMap?.AiTriggers.ChangeDisplay(IndexableDisplayType.NameOnly);
            GlobalCollectionVm.AiTriggers.UpdateAll();
        }

        private void IdChecked(object sender, RoutedEventArgs e)
        {
            GlobalVar.GlobalMap?.AiTriggers.ChangeDisplay(IndexableDisplayType.IdAndName);
            GlobalCollectionVm.AiTriggers.UpdateAll();
        }

        private void AscendingSort(object sender, RoutedEventArgs e)
        {
            GlobalVar.GlobalMap?.AiTriggers.AscendingSort();
            GlobalCollectionVm.AiTriggers.UpdateAll();
        }

        private void DescendingSort(object sender, RoutedEventArgs e)
        {
            GlobalVar.GlobalMap?.AiTriggers.DescendingSort();
            GlobalCollectionVm.AiTriggers.UpdateAll();
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
            ItemSelected?.Invoke(this, lbxMain.SelectedItem);
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
            return lbxMain.SelectedItem as IIndexableItem;
        }
    }
}
