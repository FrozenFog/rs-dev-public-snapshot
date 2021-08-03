using AvalonDock.Layout;
using RelertSharp.Common;
using RelertSharp.Wpf;
using RelertSharp.Wpf.Common;
using RelertSharp.Wpf.MapEngine.Helper;
using RelertSharp.Wpf.ViewModel;
using RelertSharp.Common.Config.Model;
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
using RelertSharp.Wpf.Dialogs;

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// TileOverlayView.xaml 的交互逻辑
    /// </summary>
    public partial class TilePanelView : UserControl, IFinalizeableView
    {
        private bool _isLoaded = false;
        private TileSetTreeVm vmFav;
        private List<FavouriteItemTree> favTilesets;
        public TilePanelView()
        {
            InitializeComponent();
            GlobalVar.MapLoadCompleteAsync += MapLoadComplete;
            TilePaintBrush.SelectTileForwardRequest += ForwardSelection;
            TilePaintBrush.SelectTileBackwardRequest += BackwardSelection;
        }

        private void BackwardSelection(object sender, EventArgs e)
        {
            if (lvMain.Items.Count > 0)
            {
                if (lvMain.SelectedIndex == 0)
                {
                    lvMain.SelectedIndex = lvMain.Items.Count - 1;
                }
                else lvMain.SelectedIndex--;
            }
        }

        private void ForwardSelection(object sender, EventArgs e)
        {
            if (lvMain.Items.Count > 0)
            {
                if (lvMain.SelectedIndex == lvMain.Items.Count - 1)
                {
                    lvMain.SelectedIndex = 0;
                }
                else lvMain.SelectedIndex++;
            }
        }

        public GuiViewType ViewType => GuiViewType.TilePanel;

        public LayoutAnchorable ParentAncorable { get; set; }
        public LayoutDocument ParentDocument { get; set; }

        public void DoFinalization()
        {
            if (_isLoaded)
            {
                FavouriteItemTree packAsFavItem(TileSetTreeVm tree)
                {
                    var dest = new FavouriteItemTree()
                    {
                        Title = tree.Title,
                    };
                    if (!tree.IsTree)
                    {
                        dest.Value = tree.SetIndex.ToString();
                        dest.Type = MapObjectType.Tile;
                    }
                    foreach (TileSetTreeVm vm in tree.Items)
                    {
                        var item = packAsFavItem(vm);
                        dest.Items.Add(item);
                    }
                    return dest;
                }
                favTilesets.Clear();
                foreach (TileSetTreeVm vm in trvMain.Items)
                {
                    if (vm.IsCustomRoot)
                    {
                        foreach (TileSetTreeVm tree in vm.Items)
                        {
                            var favItem = packAsFavItem(tree);
                            favTilesets.Add(favItem);
                        }
                        break;
                    }
                }
            }
        }


        #region Public
        public void SetFramework(bool isEnable)
        {
            foreach (TileSetTreeVm vm in trvMain.Items)
            {
                vm.All(x =>
                {
                    if (x is TileSetTreeVm y) y.SetFramework(isEnable);
                });
            }
        }
        #endregion


        #region Handler
        #region Load & selection
        private async void MapLoadComplete()
        {
            void add_to(MapStructure.TileSet set, TileSetTreeVm dest)
            {
                if (set.AllowPlace && !set.IsFramework)
                {
                    TileSetTreeVm src = new TileSetTreeVm(set);
                    dest.AddItem(src);
                }
            }
            TileSetTreeVm loadGeneral()
            {
                TileSetTreeVm general = new TileSetTreeVm("General Tile Sets");
                foreach (var p in GlobalVar.TileDictionary.GeneralTilesets)
                {
                    var tileset = GlobalVar.TileDictionary.GetTileSetFromIndex(p.Value);
                    add_to(tileset, general);
                }
                return general;
            }
            List<TileSetTreeVm> loadCustom()
            {
                List<TileSetTreeVm> result = new List<TileSetTreeVm>();
                foreach (var classInfo in GlobalVar.GlobalConfig.ModConfig.TileSetInfo.Classes)
                {
                    TileSetTreeVm root = new TileSetTreeVm(classInfo.Title);
                    Regex re = new Regex(classInfo.RegexFormat);
                    foreach (var tileset in GlobalVar.TileDictionary.TileSets)
                    {
                        if (re.Match(tileset.SetName).Success) add_to(tileset, root);
                    }
                    result.Add(root);
                }
                TileSetTreeVm all = new TileSetTreeVm("All tile sets");
                foreach (var tileset in GlobalVar.TileDictionary.TileSets)
                {
                    add_to(tileset, all);
                }
                result.Add(all);
                return result;
            }
            TileSetTreeVm loadFav()
            {
                TileSetTreeVm extractAsVm(FavouriteItemTree src)
                {
                    TileSetTreeVm vm;
                    if (src.Type == MapObjectType.Tile)
                    {
                        int.TryParse(src.Value, out int idxSet);
                        MapStructure.TileSet set = GlobalVar.TileDictionary.GetTileSetFromIndex(idxSet);
                        vm = new TileSetTreeVm(set);
                    }
                    else vm = new TileSetTreeVm(src.Title);
                    foreach (var fav in src.Items)
                    {
                        TileSetTreeVm subVm = extractAsVm(fav);
                        vm.AddItem(subVm);
                    }
                    return vm;
                }
                TileSetTreeVm root = new TileSetTreeVm("Favourite")
                {
                    IsCustomRoot = true
                };
                foreach (var favTree in favTilesets)
                {
                    var vm = extractAsVm(favTree);
                    root.AddItem(vm);
                }
                vmFav = root;
                return root;
            }
            favTilesets = GlobalVar.GlobalConfig.UserConfig.GetFavTilesetsByTheater(GlobalVar.GlobalMap.Info.TheaterName);
            List<TileSetTreeVm> vms = new List<TileSetTreeVm>();
            await Task.Run(() =>
            {
                vms.Add(loadGeneral());
                vms.AddRange(loadCustom());
                vms.Add(loadFav());
            });
            Dispatcher.Invoke(() =>
            {
                trvMain.Items.Clear();
                trvMain.Items.AddRange(vms);
                _isLoaded = true;
            });
        }

        private void SelectedSetChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (trvMain.SelectedItem is TileSetTreeVm vm)
            {
                lvMain.ItemsSource = vm.SubTileCollection;
                if (lvMain.Items.Count > 0)
                {
                    lvMain.SelectedIndex = 0;
                }
            }
        }

        private bool _isTileSelectingLocked = false;
        private void SelectTileChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isTileSelectingLocked)
            {
                if (!MouseState.IsState(PanelMouseState.TileBrush))
                {
                    MouseState.SetState(PanelMouseState.TileSingleBrush);
                }
                if (lvMain.SelectedItem is TileSetItemVm vm)
                {
                    TilePaintBrush.ResumeBrush();
                    TilePaintBrush.LoadTileBrush(vm);
                    Engine.Api.EngineApi.InvokeRedraw();
                }
            }
        }
        #endregion


        #region Mouse
        private bool isAddInvoked = false;
        private void Menu_AddToDesignated(object sender, RoutedEventArgs e)
        {
            if (isAddInvoked && sender is MenuItem src && src.DataContext is TileSetTreeVm vm && trvMain.SelectedItem is TileSetTreeVm target)
            {
                vm.AddItem(new TileSetTreeVm(target));
                isAddInvoked = false;
            }
        }
        private void PreviewRight(object sender, MouseButtonEventArgs e)
        {
            bool canDel = false, canFav = false, canAdd = false;
            menuAddTo.Items.Clear();
            if (trvMain.GetItemAtMouse<TileSetTreeVm, TextBlock>(e) is TileSetTreeVm vm)
            {
                MenuItem castAsMenu(TileSetTreeVm src)
                {
                    MenuItem item = new MenuItem();
                    item.Header = src.Title;
                    item.DataContext = src;
                    if (src.IsTree)
                    {
                        item.Click += Menu_AddToDesignated;
                    }
                    foreach (TileSetTreeVm sub in src.Items)
                    {
                        if (sub.IsTree)
                        {
                            item.Items.Add(castAsMenu(sub));
                        }
                    }
                    return item;
                }
                vm.IsSelected = true;
                canDel = vm.IsCustom && !vm.IsCustomRoot;
                canFav = !canDel && !vm.IsTree;
                canAdd = vm.IsCustom;
                menuAddTo.Items.Add(castAsMenu(vmFav));
                isAddInvoked = true;
            }
            menuDelFav.IsEnabled = canDel;
            menuAddTo.IsEnabled = canFav;
            menuAddGroup.IsEnabled = canAdd;
        }
        #endregion


        #region Menu
        private void Menu_AddFavGroup(object sender, RoutedEventArgs e)
        {
            DlgNameInput dlg = new DlgNameInput();
            if (dlg.ShowDialog().Value && trvMain.SelectedItem is TileSetTreeVm dest)
            {
                string name = dlg.ResultName;
                TileSetTreeVm vm = new TileSetTreeVm(name);
                if (dest.IsCustomRoot) vmFav.AddItem(vm);
                else if (dest.IsCustom)
                {
                    if (dest.IsTree) dest.AddItem(vm);
                    else dest.Ancestor.AddItem(vm);
                }
            }
        }

        private void Menu_DelFav(object sender, RoutedEventArgs e)
        {
            if (GuiUtil.YesNoWarning("All item inside will be removed.\nAre you sure?"))
            {
                if (trvMain.SelectedItem is TileSetTreeVm vm)
                {
                    vm.RemoveFromAncestor();
                }
            }
        }
        #endregion

        #endregion
    }
}
