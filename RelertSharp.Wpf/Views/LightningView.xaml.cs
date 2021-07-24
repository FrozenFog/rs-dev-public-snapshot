using RelertSharp.Common;
using RelertSharp.MapStructure;
using RelertSharp.Wpf.ViewModel;
using RelertSharp.Engine.Api;
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
using System.Windows.Threading;
using RelertSharp.Wpf.Common;

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// LightningView.xaml 的交互逻辑
    /// </summary>
    public partial class LightningView : UserControl, IRsView
    {
        internal event EventHandler LightningChangedRequest;
        private readonly DelayedAction delayedRefresh;
        public int ContentWidth { get { return (int)grdColContent.ActualWidth; } }
        private Lightning MapLightning { get { return GlobalVar.GlobalMap.LightningCollection; } }

        public GuiViewType ViewType { get { return GuiViewType.LightningPanel; } }
        public AvalonDock.Layout.LayoutAnchorable ParentAncorable { get; set; }
        public AvalonDock.Layout.LayoutDocument ParentDocument { get; set; }

        private LightningItem light;
        public LightningView()
        {
            InitializeComponent();
            DataContext = new LightningVm();
            delayedRefresh = new DelayedAction(null, SetLight, 200);
        }

        private void LoadLight(LightningItem item)
        {
            DataContext = new LightningVm(item);
            light = item;
            SetLight();
        }
        private void SetLight()
        {
            if (light != null)
            {
                EngineApi.ApplyLightning(light, ckbEnable.IsChecked.Value);
                LightningChangedRequest?.Invoke(null, null);
            }
        }
        private void IsControlEnable(bool enabled)
        {
            btnRefresh.IsEnabled = enabled;
            cbbType.IsEnabled = enabled;
            txbRed.IsEnabled = enabled;
            txbGreen.IsEnabled = enabled;
            txbBlue.IsEnabled = enabled;
            txbAmbient.IsEnabled = enabled;
            txbGround.IsEnabled = enabled;
            txbLevel.IsEnabled = enabled;
        }

        #region Handler
        private void LightningTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbType.SelectedItem is ComboItem item)
            {
                MapLightningType lightningType = (MapLightningType)item.Value.ParseInt();
                switch (lightningType)
                {
                    case MapLightningType.Normal:
                        LoadLight(MapLightning.Normal);
                        break;
                    case MapLightningType.Storm:
                        LoadLight(MapLightning.Ion);
                        break;
                    case MapLightningType.Dominator:
                        LoadLight(MapLightning.Dominator);
                        break;
                }
            }
        }

        private void RefreshClicked(object sender, RoutedEventArgs e)
        {
            SetLight();
        }

        private void EnableChecked(object sender, RoutedEventArgs e)
        {
            if (GlobalVar.CurrentMapDocument == null) return;
            if (cbbType.SelectedIndex < 0) cbbType.SelectedIndex = 0;
            IsControlEnable(ckbEnable.IsChecked.Value);
            LightningTypeChanged(null, null);
        }

        private void WheelValueHandler(object sender, MouseWheelEventArgs e)
        {
            TextBox txb = sender as TextBox;
            double delta = 0;
            if (e.Delta > 0) delta = 0.01;
            else if (e.Delta < 0) delta = -0.01;
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)) delta *= 10;
            double value = txb.Text.ParseDouble();
            value += delta;
            txb.Text = value.ForcePositive().ToString(6);
            delayedRefresh.Restart();
        }

        private void EditFocusLost(object sender, RoutedEventArgs e)
        {
            delayedRefresh.Restart();
        }
        #endregion

    }
}
