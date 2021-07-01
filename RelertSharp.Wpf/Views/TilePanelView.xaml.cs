using AvalonDock.Layout;
using RelertSharp.Common;
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
    /// TileOverlayView.xaml 的交互逻辑
    /// </summary>
    public partial class TilePanelView : UserControl, IRsView
    {
        public TilePanelView()
        {
            InitializeComponent();
            GlobalVar.MapDocumentLoaded += MapReloadedHandler;
        }

        public GuiViewType ViewType => GuiViewType.TilePanel;

        public LayoutAnchorable ParentAncorable { get; set; }
        public LayoutDocument ParentDocument { get; set; }

        private void MapReloadedHandler(object sender, EventArgs e)
        {
            trvMain.Items.Clear();
            foreach (var tileset in GlobalVar.TileDictionary.TileSets)
            {
                TileSetTreeVm vm = new TileSetTreeVm(tileset);
                trvMain.Items.Add(vm);
            }
        }

        private void SelectedSetChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (trvMain.SelectedItem is TileSetTreeVm vm)
            {
                lvMain.ItemsSource = vm.SubTileCollection;
            }
        }
    }
}
