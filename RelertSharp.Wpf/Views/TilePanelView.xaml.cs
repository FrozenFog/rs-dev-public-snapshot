﻿using AvalonDock.Layout;
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

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// TileOverlayView.xaml 的交互逻辑
    /// </summary>
    public partial class TilePanelView : UserControl, IFinalizeableView
    {
        private bool _isLoaded = false;
        public TilePanelView()
        {
            InitializeComponent();
            GlobalVar.MapDocumentLoaded += MapReloadedHandler;
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
                GlobalVar.GlobalConfig.UserConfig.FavouriteTileSet.Items.Clear();
                foreach (TileSetTreeVm vm in trvMain.Items)
                {
                    if (vm.IsCustomRoot)
                    {
                        foreach (TileSetTreeVm tree in vm.Items)
                        {
                            var favItem = packAsFavItem(tree);
                            GlobalVar.GlobalConfig.UserConfig.FavouriteTileSet.Items.Add(favItem);
                        }
                        break;
                    }
                }
            }
        }


        #region Public

        #endregion


        #region Handler
        #region Load & selection
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
                foreach (var favTree in GlobalVar.GlobalConfig.UserConfig.FavouriteTileSet.Items)
                {
                    var vm = extractAsVm(favTree);
                    root.AddItem(vm);
                }
                trvMain.Items.Add(root);
            }
            loadGeneral();
            loadCustom();
            loadFav();
            _isLoaded = true;
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
                MouseState.SetState(PanelMouseState.TileBrush);
                if (lvMain.SelectedItem is TileSetItemVm vm)
                {
                    TilePaintBrush.ResumeBrush();
                    TilePaintBrush.LoadTileBrush(vm);
                    Engine.Api.EngineApi.InvokeRedraw();
                }
            }
        }
        #endregion


        #region Menu

        #endregion
        #endregion
    }
}
