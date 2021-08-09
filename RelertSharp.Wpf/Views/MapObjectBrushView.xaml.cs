using RelertSharp.Common;
using RelertSharp.IniSystem;
using RelertSharp.Wpf.Common;
using RelertSharp.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
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
    /// MapObjectBrushView.xaml 的交互逻辑
    /// </summary>
    public partial class MapObjectBrushView : UserControl, IRsView
    {
        private Rules Rules { get { return GlobalVar.GlobalRules; } }

        public GuiViewType ViewType => GuiViewType.ObjctPanel;
        public AvalonDock.Layout.LayoutAnchorable ParentAncorable { get; set; }
        public AvalonDock.Layout.LayoutDocument ParentDocument { get; set; }

        private ObjectBrushConfig cfg;
        private ObjectBrushFilter filter;
        private ObjectAttributeApplierVm context;
        public MapObjectBrushView()
        {
            InitializeComponent();
            GlobalVar.MapDocumentLoaded += MapReloadedHandler;
        }

        private void MapReloadedHandler(object sender, EventArgs e)
        {
            ReloadAllObjects();
        }


        #region Public
        public void ReloadAllObjects()
        {
            trvMain.Items.Clear();
            void add_to_node(ObjectPickVm dest, string regname, MapObjectType type = MapObjectType.Undefined)
            {
                dest.AddItem(new ObjectPickVm(regname, Rules.FormatTreeNodeName(regname), type));
            }
            List<ObjectPickVm> init_side()
            {
                int num = Rules.GetSideCount();
                List<ObjectPickVm> result = new List<ObjectPickVm>();
                for (int i = 0; i< num; i++)
                {
                    result.Add(new ObjectPickVm(Rules.GetSideName(i).ToLang()));
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
                trvMain.Items.Add(root);
            }
            void generic(string title, string rulesHead, object icon, CombatObjectType combatType, MapObjectType objType)
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
                trvMain.Items.Add(root);
            }
            void favourites()
            {
                ObjectPickVm root = new ObjectPickVm("Favourites");
                root.SetIcon(FindResource("HeadFav"));
                void readInto(RelertSharp.Common.Config.Model.FavouriteItemTree src, ObjectPickVm parent)
                {
                    ObjectPickVm dest = new ObjectPickVm(src.Title, src.Value, src.Type);
                    if (src.Items != null) foreach (var sub in src.Items) readInto(sub, dest);
                    parent.AddItem(dest);
                }
                readInto(GlobalVar.GlobalConfig.UserConfig.FavouriteObjects, root);
                trvMain.Items.Add(root);
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
                trvMain.Items.Add(uRoot);
                trvMain.Items.Add(nRoot);
            }
            building();
            generic("Infantries", Constant.RulesHead.HEAD_INFANTRY, FindResource("HeadInf"), CombatObjectType.Infantry, MapObjectType.Infantry);
            unit();
            generic("Aircrafts", Constant.RulesHead.HEAD_AIRCRAFT, FindResource("HeadAir"), CombatObjectType.Aircraft, MapObjectType.Aircraft);
            favourites();
            ReloadAttributeCombo();
        }
        internal void BindBrushConfig(ObjectBrushConfig config, ObjectBrushFilter filter)
        {
            cfg = config; this.filter = filter;
            context = new ObjectAttributeApplierVm(config, filter);
            context.AttributeRefreshRequest += HandleAttrubuteRefresh;
            context.ObjectRefreshRequest += ObjectRefreshHandler;
            DataContext = context;
        }
        #endregion

        #region Handler
        private void BrushItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ObjectPickVm selected = trvMain.SelectedItem as ObjectPickVm;
            IEnumerable<IIndexableItem> upgradeType = GlobalVar.GlobalRules.GetBuildingUpgradeList(selected.RegName);
            cbbUpg1.ItemsSource = upgradeType;
            cbbUpg2.ItemsSource = upgradeType;
            cbbUpg3.ItemsSource = upgradeType;
            context.SetObjectType(selected.Type);
            RefreshContext();
            if (selected.Type != MapObjectType.Undefined)
            {
                PaintBrush.ResumeBrush();
                PaintBrush.LoadBrushObject(selected.RegName, selected.Type);
                MapEngine.Helper.MouseState.SetState(MapEngine.Helper.PanelMouseState.ObjectBrush);
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
            RefreshContext();
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
        private void RefreshContext()
        {
            DataContext = null;
            DataContext = context;
        }
        #endregion
    }
}
