using RelertSharp.Common;
using RelertSharp.Common.Config.Model;
using RelertSharp.Engine;
using RelertSharp.Engine.Api;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure;
using RelertSharp.Wpf.Common;
using RelertSharp.Wpf.Dialogs;
using RelertSharp.Wpf.MapEngine.Helper;
using RelertSharp.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
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
    /// MapObjectBrushView.xaml 的交互逻辑
    /// </summary>
    public partial class MapObjectBrushView : UserControl, IRsView, IFinalizeableView
    {
        private Rules Rules { get { return GlobalVar.GlobalRules; } }

        public GuiViewType ViewType => GuiViewType.ObjctPanel;
        public AvalonDock.Layout.LayoutAnchorable ParentAncorable { get; set; }
        public AvalonDock.Layout.LayoutDocument ParentDocument { get; set; }
        private bool isObjLoaded = false;

        private ObjectBrushConfig cfg;
        private ObjectBrushFilter filter;
        private ObjectAttributeApplierVm context;
        private ObjectPickVm favRoot;
        public MapObjectBrushView()
        {
            InitializeComponent();
            GlobalVar.MapDocumentLoaded += MapReloadedHandler;
        }

        private void MapReloadedHandler()
        {
            ReloadAllObjects();
        }


        #region Public
        public void ReloadAllObjects()
        {
            trvMain.Items.Clear();
            var cfg = GlobalVar.GlobalConfig.ModConfig.ObjectInfo;
            ObjectPickVm load_custom(IEnumerable<CustomItemInfo> classes, IEnumerable<INIPair> src, MapObjectType type)
            {
                ObjectPickVm root = new ObjectPickVm("Custom");
                foreach (var info in classes)
                {
                    ObjectPickVm vm = new ObjectPickVm(info.Title);
                    Regex re = new Regex(info.RegexFormat);
                    foreach (INIPair p in src)
                    {
                        string name = Rules[p.Value].GetString(Constant.KEY_NAME);
                        if (!name.IsNullOrEmpty() && re.IsMatch(name))
                        {
                            add_to_node(vm, p.Value, type);
                        }
                    }
                    root.AddItem(vm);
                }
                return root;
            }
            void add_to_node(ObjectPickVm dest, string regname, MapObjectType type = MapObjectType.Undefined)
            {
                dest.AddItem(new ObjectPickVm(regname, Rules.FormatTreeNodeName(regname), type));
            }
            List<ObjectPickVm> init_side()
            {
                var sideInfo = GlobalVar.GlobalConfig.ModGeneral.SideInfo;
                int num = sideInfo.Count;
                List<ObjectPickVm> result = new List<ObjectPickVm>();
                for (int i = 0; i< num; i++)
                {
                    result.Add(new ObjectPickVm(sideInfo[i].Name.ToLang()));
                }
                result.Add(new ObjectPickVm("Others"));
                return result;
            }
            void building()
            {
                ObjectPickVm root = new ObjectPickVm("Buildings");
                root.SetIcon(FindResource("HeadBud"));
                List<ObjectPickVm> sides = init_side();
                ObjectPickVm tech = new ObjectPickVm("Tech Buildings");
                foreach (INIPair p in Rules[Constant.RulesHead.HEAD_BUILDING])
                {
                    if (Rules.IsTechBuilding(p.Value))
                    {
                        add_to_node(tech, p.Value, MapObjectType.Building);
                    }
                    else
                    {
                        int side = Rules.GuessSide(p.Value, CombatObjectType.Building, true);
                        if (side >= 0) add_to_node(sides[side], p.Value, MapObjectType.Building);
                        else add_to_node(sides.Last(), p.Value, MapObjectType.Building);
                    }
                }
                sides.Insert(sides.Count - 1, tech);
                sides.ForEach(x => root.AddItem(x));
                root.AddItem(load_custom(cfg.Buildings, Rules[Constant.RulesHead.HEAD_BUILDING], MapObjectType.Building));
                trvMain.Items.Add(root);
            }
            void generic(string title, string rulesHead, object icon, CombatObjectType combatType, MapObjectType objType, IEnumerable<CustomItemInfo> classes)
            {
                ObjectPickVm root = new ObjectPickVm(title);
                root.SetIcon(icon);
                List<ObjectPickVm> sides = init_side();
                foreach (INIPair p in Rules[rulesHead])
                {
                    int side = Rules.GuessSide(p.Value, combatType);
                    if (side >= 0) add_to_node(sides[side], p.Value, objType);
                    else add_to_node(sides.Last(), p.Value, objType);
                }
                sides.ForEach(x => root.AddItem(x));
                root.AddItem(load_custom(classes, Rules[rulesHead], objType));
                trvMain.Items.Add(root);
            }
            void favourites()
            {
                favRoot = new ObjectPickVm("Stars")
                {
                    IsFavourite = true,
                    IsFavRoot = true
                };
                favRoot.SetIcon(FindResource("HeadFav"));
                void readInto(FavouriteItemTree src, ObjectPickVm parent)
                {
                    if (src.IsNull) return;
                    ObjectPickVm dest = new ObjectPickVm(src.Value, src.Title, src.Type);
                    if (src.Items != null) foreach (var sub in src.Items) readInto(sub, dest);
                    parent.AddItem(dest);
                }
                foreach (var fav in GlobalVar.GlobalConfig.UserConfig.FavouriteObjects.Items)
                {
                    readInto(fav, favRoot);
                }
                trvMain.Items.Add(favRoot);
            }
            void unit()
            {
                ObjectPickVm uRoot = new ObjectPickVm("Units");
                uRoot.SetIcon(FindResource("HeadUnit"));
                ObjectPickVm nRoot = new ObjectPickVm("Navals");
                nRoot.SetIcon(FindResource("HeadNaval"));
                List<ObjectPickVm> uSides = init_side();
                List<ObjectPickVm> nSides = init_side();
                foreach (INIPair p in Rules[Constant.RulesHead.HEAD_VEHICLE])
                {
                    if (Rules[p.Value]["MovementZone"] == "Water")
                    {
                        int side = Rules.GuessSide(p.Value, CombatObjectType.Naval);
                        if (side >= 0) add_to_node(nSides[side], p.Value, MapObjectType.Vehicle);
                        else add_to_node(nSides.Last(), p.Value, MapObjectType.Vehicle);
                    }
                    else
                    {
                        int side = Rules.GuessSide(p.Value, CombatObjectType.Vehicle);
                        if (side >= 0) add_to_node(uSides[side], p.Value, MapObjectType.Vehicle);
                        else add_to_node(uSides.Last(), p.Value, MapObjectType.Vehicle);
                    }
                }
                uSides.ForEach(x => uRoot.AddItem(x));
                nSides.ForEach(x => nRoot.AddItem(x));
                uRoot.AddItem(load_custom(cfg.Units, Rules[Constant.RulesHead.HEAD_VEHICLE], MapObjectType.Vehicle));
                trvMain.Items.Add(uRoot);
                trvMain.Items.Add(nRoot);
            }
            void terrain()
            {
                ObjectPickVm root = new ObjectPickVm("Terrains");
                root.SetIcon(FindResource("HeadTerrain"));
                ObjectPickVm tib = new ObjectPickVm("Resource Fountain");
                ObjectPickVm all = new ObjectPickVm("All Terrain");
                foreach (INIPair p in Rules[Constant.RulesHead.HEAD_TERRAIN])
                {
                    if (Rules[p.Value].ParseBool("SpawnsTiberium")) add_to_node(tib, p.Value, MapObjectType.Terrain);
                    add_to_node(all, p.Value, MapObjectType.Terrain);
                }
                ObjectPickVm custom = new ObjectPickVm("Custom");
                root.AddItem(tib);
                root.AddItem(load_custom(cfg.Terrains, Rules[Constant.RulesHead.HEAD_TERRAIN], MapObjectType.Terrain));
                root.AddItem(all);
                trvMain.Items.Add(root);
            }
            void overlay()
            {
                ObjectPickVm root = new ObjectPickVm("Overlays");
                root.SetIcon(FindResource("HeadOverlay"));
                ObjectPickVm rock = new ObjectPickVm("Rocks");
                ObjectPickVm resource = new ObjectPickVm("Resources");
                ObjectPickVm others = new ObjectPickVm("Others");
                ObjectPickVm walls = new ObjectPickVm("Walls");
                ObjectPickVm bridges = new ObjectPickVm("Bridges");
                ObjectPickVm rail = new ObjectPickVm("Railway");
                int i = 0;
                foreach (INIPair p in Rules[Constant.RulesHead.HEAD_OVERLAY])
                {
                    INIEntity ent = Rules[p.Value];
                    ObjectPickVm vm = new ObjectPickVm(ent.GetString(Constant.KEY_NAME, p.Value), p.Value, (byte)i);
                    if (ent.ParseBool("IsARock")) rock.AddItem(vm);
                    else if (ent.ParseBool("Wall")) walls.AddItem(vm);
                    else if (ent.ParseBool("Tiberium")) resource.AddItem(vm);
                    else if (ent.GetString("Land") == "Railroad") rail.AddItem(vm);
                    else if (ent.ParseBool("Overrides") || ent.GetString("Land") == "Road") bridges.AddItem(vm);
                    else others.AddItem(vm);
                    i++;
                }
                root.AddItems(walls, rock, bridges, resource, rail, others);
                trvMain.Items.Add(root);
            }
            void celltagWp()
            {
                ObjectPickVm vm = new ObjectPickVm(string.Empty, "Celltag", MapObjectType.Celltag);
                vm.SetIcon(FindResource("HeadCelltag"));
                ObjectPickVm waypoint = new ObjectPickVm("Waypoint");
                ObjectPickVm assign = new ObjectPickVm(string.Empty, "Assign number", MapObjectType.Waypoint)
                {
                    AssignWaypoint = true
                };
                waypoint.AddItem(new ObjectPickVm(string.Empty, "First available", MapObjectType.Waypoint));
                waypoint.AddItem(assign);
                waypoint.SetIcon(FindResource("HeadWaypoint"));
                trvMain.Items.Add(vm);
                trvMain.Items.Add(waypoint);
            }
            void smudge()
            {
                ObjectPickVm root = new ObjectPickVm("Smudges");
                root.SetIcon(FindResource("HeadSmudge"));
                ObjectPickVm oneCell = new ObjectPickVm("One Cell");
                ObjectPickVm multiCell = new ObjectPickVm("Multi Cell");
                foreach (INIPair p in Rules[Constant.RulesHead.HEAD_SMUDGE])
                {
                    INIEntity ent = Rules[p.Value];
                    if (ent.IsEmpty) continue;
                    int width = ent.ParseInt("Width", 1);
                    int height = ent.ParseInt("Height", 1);
                    ObjectPickVm item = new ObjectPickVm(p.Value, p.Value, MapObjectType.Smudge);
                    if (width * height == 1) oneCell.AddItem(item);
                    else multiCell.AddItem(item);
                }
                root.AddItems(oneCell, multiCell);
                trvMain.Items.Add(root);
            }
            building();
            generic("Infantries", Constant.RulesHead.HEAD_INFANTRY, FindResource("HeadInf"), CombatObjectType.Infantry, MapObjectType.Infantry, cfg.Infantries);
            unit();
            generic("Aircrafts", Constant.RulesHead.HEAD_AIRCRAFT, FindResource("HeadAir"), CombatObjectType.Aircraft, MapObjectType.Aircraft, cfg.Aircrafts);
            terrain();
            overlay();
            smudge();
            celltagWp();
            favourites();
            ReloadAttributeCombo();
            isObjLoaded = true;
        }
        internal void BindBrushConfig(ObjectBrushConfig config, ObjectBrushFilter filter)
        {
            cfg = config; this.filter = filter;
            context = new ObjectAttributeApplierVm(config, filter);
            context.AttributeRefreshRequest += HandleAttrubuteRefresh;
            context.ObjectRefreshRequest += ObjectRefreshHandler;
            DataContext = context;
        }
        public void DoFinalization()
        {
            if (isObjLoaded)
            {
                FavouriteItemTree packAsFavItem(ObjectPickVm vm)
                {
                    var dest = new FavouriteItemTree()
                    {
                        Title = vm.Title,
                        Value = vm.RegName,
                        Type = vm.Type
                    };
                    foreach (ObjectPickVm sub in vm.Items)
                    {
                        var item = packAsFavItem(sub);
                        dest.Items.Add(item);
                    }
                    return dest;
                }
                GlobalVar.GlobalConfig.UserConfig.FavouriteObjects.Items.Clear();
                foreach (ObjectPickVm vm in favRoot.Items)
                {
                    GlobalVar.GlobalConfig.UserConfig.FavouriteObjects.Items.Add(packAsFavItem(vm));
                }
            }
        }
        #endregion

        #region Handler
        private void OverlayFrameSelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvOverlay.SelectedItem is ObjectPickVm selected)
            {
                PaintBrush.SetOverlayInfo(selected.OverlayIndex, selected.OverlayData);
                PaintBrush.ResumeBrush();
                PaintBrush.LoadBrushObject(selected.RegName, MapObjectType.Overlay);
                MouseState.SetState(PanelMouseState.ObjectBrush);
            }
        }
        private void BrushItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (trvMain.SelectedItem is ObjectPickVm selected)
            {
                IEnumerable<IIndexableItem> upgradeType = GlobalVar.GlobalRules.GetBuildingUpgradeList(selected.RegName);
                cbbUpg1.ItemsSource = upgradeType;
                cbbUpg2.ItemsSource = upgradeType;
                cbbUpg3.ItemsSource = upgradeType;
                context.SetObjectType(selected.Type);
                if (selected.Type == MapObjectType.Overlay)
                {
                    context.SetOverlayFrames(selected.OverlayIndex, selected.RegName, out bool isValidOverlay, out byte firstValidFrame);
                    PaintBrush.SetOverlayInfo(selected.OverlayIndex, firstValidFrame);
                    if (!isValidOverlay)
                    {
                        PaintBrush.InvalidateBrushObject();
                        return;
                    }
                }
                if (selected.Type == MapObjectType.Waypoint)
                {
                    int wp = GlobalVar.GlobalMap.Waypoints.NewID();
                    PaintBrush.SetWaypointIndex(wp.ToString());
                    PaintBrush.AssignWaypointId = selected.AssignWaypoint;
                }
                if (selected.Type != MapObjectType.Undefined)
                {
                    PaintBrush.ResumeBrush();
                    PaintBrush.LoadBrushObject(selected.RegName, selected.Type);
                    MouseState.SetState(PanelMouseState.ObjectBrush);
                }
            }
        }
        private void HandleAttrubuteRefresh(object sender, EventArgs e)
        {
            ObjectPickVm selected = trvMain.SelectedItem as ObjectPickVm;
            IEnumerable<IIndexableItem> upgradeType = GlobalVar.GlobalRules.GetBuildingUpgradeList(selected.RegName);
            cbbUpg1.ItemsSource = upgradeType;
            cbbUpg2.ItemsSource = upgradeType;
            cbbUpg3.ItemsSource = upgradeType;
            context.SetObjectType(selected.Type);
        }
        private void ObjectRefreshHandler(object sender, EventArgs e)
        {
            ObjectPickVm selected = trvMain.SelectedItem as ObjectPickVm;
            if (selected.Type != MapObjectType.Undefined)
            {
                PaintBrush.ResumeBrush();
                PaintBrush.LoadBrushObject(selected.RegName, selected.Type);
            }
        }
        private void FacingWheel(object sender, MouseWheelEventArgs e)
        {
            bool inc = e.Delta > 0;
            int delta = 32 * (inc ? 1 : -1);
            int result = context.Facing + delta;
            if (result <= 0) result += 256;
            if (result >= 256) result -= 256;
            context.Facing = result;
        }
        #endregion



        #region Private
        private void ReloadAttributeCombo()
        {
            cbbOwner.ItemsSource = GlobalCollectionVm.Houses;
            cbbStatus.ItemsSource = GlobalVar.GlobalConfig.ModConfig.GetCombo(Constant.Config.DefaultComboType.TYPE_MAP_OBJSTATE).CastToCombo();
            cbbSpotlight.ItemsSource = GlobalVar.GlobalConfig.ModConfig.GetCombo(Constant.Config.DefaultComboType.TYPE_MAP_SPOTLIGHT).CastToCombo();
        }
        #endregion

        #region Menu
        private void Menu_AddObjToRandomize(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement elem && elem.DataContext is ObjectPickVm vm)
            {
                if (vm.Type == MapObjectType.Overlay) RandomizeBrush.AddRandomObject(vm.RegName, vm.OverlayIndex, vm.OverlayData);
                else RandomizeBrush.AddRandomObject(vm.RegName, vm.Type);
            }
        }
        private ObjectPickVm waitToAdd;
        private void Menu_PrevClickFav(object sender, RoutedEventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            menu.Items.Clear();
            MenuItem castAsMenu(ObjectPickVm src)
            {
                MenuItem item = new MenuItem()
                {
                    Header = src.Title,
                    DataContext = src
                };
                if (src.IsTree)
                {
                    item.Click += MenuAddToDesignated;
                }
                foreach (ObjectPickVm sub in src.Items)
                {
                    if (sub.IsTree) item.Items.Add(castAsMenu(sub));
                }
                return item;
            }
            waitToAdd = menu.DataContext as ObjectPickVm;
            foreach (ObjectPickVm fav in favRoot.Items)
            {
                menu.Items.Add(castAsMenu(fav));
            }
        }

        private void MenuAddToDesignated(object sender, RoutedEventArgs e)
        {
            if (waitToAdd != null)
            {
                using (var _ = new EngineRegion())
                {
                    var dest = (sender as MenuItem).DataContext as ObjectPickVm;
                    dest.AddItem(waitToAdd);
                }
            }
        }

        private void Menu_AddFavGroup(object sender, RoutedEventArgs e)
        {
            DlgNameInput dlg = new DlgNameInput("Group name");
            var dest = (sender as MenuItem).DataContext as ObjectPickVm;
            if (dlg.ShowDialog().Value)
            {
                string name = dlg.ResultName;
                ObjectPickVm vm = new ObjectPickVm(name)
                {
                    IsFavourite = true
                };
                if (dest.IsRoot) dest.AddItem(vm);
                else dest.Ancestor.AddItem(vm);
            }
        }

        private void Menu_RemoveFav(object sender, RoutedEventArgs e)
        {
            ObjectPickVm vm = (sender as MenuItem).DataContext as ObjectPickVm;
            vm.RemoveFromAncestor();
        }
        #endregion
    }
}
