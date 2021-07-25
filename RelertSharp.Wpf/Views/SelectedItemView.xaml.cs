﻿using AvalonDock.Layout;
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
        #endregion


        private void ReloadCombo()
        {
            cbbOwner.ItemsSource = GlobalCollectionVm.Houses;
            cbbStatus.ItemsSource = GlobalVar.GlobalConfig.ModConfig.GetCombo(Constant.Config.DefaultComboType.TYPE_MAP_OBJSTATE).CastToCombo();
            cbbSpotlight.ItemsSource = GlobalVar.GlobalConfig.ModConfig.GetCombo(Constant.Config.DefaultComboType.TYPE_MAP_SPOTLIGHT).CastToCombo();
        }
    }
}
