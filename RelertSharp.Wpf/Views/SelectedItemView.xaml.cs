using AvalonDock.Layout;
using RelertSharp.Wpf.Common;
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
using RelertSharp.Wpf.ViewModel;
using RelertSharp.Common;
using RelertSharp.Wpf.MapEngine.Helper;
using RelertSharp.Engine.Api;
using RelertSharp.Wpf.Dialogs;

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// SelectedItemView.xaml 的交互逻辑
    /// </summary>
    public partial class SelectedItemView : UserControl, IRsView
    {
        public GuiViewType ViewType => GuiViewType.InspectorPanel;
        public LayoutAnchorable ParentAncorable { get; set; }
        public LayoutDocument ParentDocument { get; set; }
        private SelectedInspectorVm vmInspector = new SelectedInspectorVm();
        public SelectedItemView()
        {
            InitializeComponent();
            lvObjects.ItemsSource = vmInspector.SelectedObjects;
            lvTiles.ItemsSource = vmInspector.SelectedTiles;
            GlobalVar.MapDocumentLoaded += MapLoadedHandler;
            scrvInspect.DataContext = vmInspector;
            vmInspector.RedrawRequested += RedrawHandler;
            vmInspector.LoadedWithBuilding += LoadExtensionHandler;
        }

        private void LoadExtensionHandler(object sender, EventArgs e)
        {
            if (sender is string regname)
            {
                IEnumerable<IIndexableItem> upgradeType = GlobalVar.GlobalRules.GetBuildingUpgradeList(regname);
                cbbUpg1.ItemsSource = upgradeType;
                cbbUpg2.ItemsSource = upgradeType;
                cbbUpg3.ItemsSource = upgradeType;
            }
            else
            {
                cbbUpg1.ItemsSource = null;
                cbbUpg2.ItemsSource = null;
                cbbUpg3.ItemsSource = null;
            }
        }

        private void RedrawHandler()
        {
            EngineApi.InvokeLock();
            foreach (IMapObject obj in Selector.SelectedObjects)
            {
                EngineApi.RedrawObject(obj);
            }
            EngineApi.InvokeUnlock();
            EngineApi.InvokeRedraw();
        }

        private void MapLoadedHandler(object sender, EventArgs e)
        {
            ReloadCombo();
        }


        #region Inspector
        private void FacingWheel(object sender, MouseWheelEventArgs e)
        {
            if (vmInspector.CanChangeFacing())
            {
                bool inc = e.Delta > 0;
                int delta = 32 * (inc ? 1 : -1);
                int result = vmInspector.GetFacing() + delta;
                if (result <= 0) result += 256;
                if (result >= 256) result -= 256;
                vmInspector.SetFacing(result);
            }
        }
        private void ObjectItemDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is SearchResultVm vm)
            {
                NavigationHub.AutoTrace(vm.Data);
            }
        }
        #endregion


        private void ReloadCombo()
        {
            cbbOwner.ItemsSource = GlobalCollectionVm.Houses;
            cbbStatus.ItemsSource = GlobalVar.GlobalConfig.ModConfig.GetCombo(Constant.Config.DefaultComboType.TYPE_MAP_OBJSTATE).CastToCombo();
            cbbSpotlight.ItemsSource = GlobalVar.GlobalConfig.ModConfig.GetCombo(Constant.Config.DefaultComboType.TYPE_MAP_SPOTLIGHT).CastToCombo();
        }


        #region Menu
        private void MenuRightDown(object sender, MouseButtonEventArgs e)
        {
            bool hasSelected = lvObjects.SelectedItems.Count > 0;
            menuShowIni.IsEnabled = hasSelected;
            menuAddSearch.IsEnabled = hasSelected;
            menuReport.IsEnabled = hasSelected;
        }
        private void Menu_ShowIni(object sender, RoutedEventArgs e)
        {
            DlgIniInspector ini = new DlgIniInspector();
            List<string> selectedReg = new List<string>();
            foreach (SearchResultVm vm in lvObjects.SelectedItems)
            {
                if (vm.Data is IRegistable reg)
                {
                    selectedReg.Add(reg.RegName);
                }
            }
            ini.SetContents(selectedReg);
            ini.ShowDialog();
        }

        private void Menu_Report(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (SearchResultVm vm in lvObjects.SelectedItems)
            {
                sb.AppendLine(vm.GenerateReport());
            }
            Clipboard.SetText(sb.ToString());
        }

        private void Menu_AddGlobal(object sender, RoutedEventArgs e)
        {
            List<object> push = new List<object>();
            foreach (SearchResultVm vm in lvObjects.SelectedItems)
            {
                push.Add(vm.Data);
            }
            SearchHub.PushSearchResult(push, false);
        }
        #endregion
    }
}
