using AvalonDock.Layout;
using RelertSharp.Common;
using RelertSharp.Wpf.Common;
using RelertSharp.Wpf.ViewModel;
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
            void add_to(MapStructure.TileSet set, TileSetTreeVm dest)
            {
                if (set.AllowPlace && !set.IsFramework)
                {
                    TileSetTreeVm src = new TileSetTreeVm(set);
                    dest.AddItem(src);
                }
            }
            void loadGeneral()
            {
                TileSetTreeVm general = new TileSetTreeVm("General Tile Sets");
                foreach (var p in GlobalVar.TileDictionary.GeneralTilesets)
                {
                    var tileset = GlobalVar.TileDictionary.GetTileSetFromIndex(p.Value);
                    add_to(tileset, general);
                }
                trvMain.Items.Add(general);
            }
            void loadCustom()
            {
                foreach (var classInfo in GlobalVar.GlobalConfig.ModConfig.TileSetInfo.Classes)
                {
                    TileSetTreeVm root = new TileSetTreeVm(classInfo.Title);
                    Regex re = new Regex(classInfo.RegexFormat);
                    foreach (var tileset in GlobalVar.TileDictionary.TileSets)
                    {
                        if (re.Match(tileset.SetName).Success) add_to(tileset, root);
                    }
                    trvMain.Items.Add(root);
                }
                TileSetTreeVm all = new TileSetTreeVm("All tile sets");
                foreach (var tileset in GlobalVar.TileDictionary.TileSets)
                {
                    add_to(tileset, all);
                }
                trvMain.Items.Add(all);
            }
            void loadFav()
            {

            }
            loadGeneral();
            loadCustom();
            loadFav();
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
