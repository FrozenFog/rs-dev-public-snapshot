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

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// LightningView.xaml 的交互逻辑
    /// </summary>
    public partial class LightningView : UserControl
    {
        internal event EventHandler LightningChangedRequest;
        public int ContentWidth { get { return (int)grdColContent.ActualWidth; } }
        private Lightning MapLightning { get { return GlobalVar.CurrentMapDocument.Map.LightningCollection; } }
        private LightningItem light;
        private DispatcherTimer refreshTimer;
        public LightningView()
        {
            InitializeComponent();
            DataContext = new LightningVm();
            refreshTimer = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 0, 0, 200)
            };
            refreshTimer.Tick += RefreshTimerTicked;
        }

        private void RefreshTimerTicked(object sender, EventArgs e)
        {
            refreshTimer.Stop();
            SetLight(light);
        }

        private void LoadLight(LightningItem item)
        {
            DataContext = new LightningVm(item);
            light = item;
            SetLight(item);
        }
        private void SetLight(LightningItem item)
        {
            if (item != null)
            {
                EngineApi.ApplyLightning(item, ckbEnable.IsChecked.Value);
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


        private void LightningTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboItem item = cbbType.SelectedItem as ComboItem;
            if (item != null)
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
            SetLight(light);
        }

        private void EnableChecked(object sender, RoutedEventArgs e)
        {
            if (cbbType.SelectedIndex < 0) cbbType.SelectedIndex = 0;
            IsControlEnable(ckbEnable.IsChecked.Value);
            LightningTypeChanged(null, null);
        }

        private bool increWheelAdjust = false;
        private void WheelValueHandler(object sender, MouseWheelEventArgs e)
        {
            TextBox txb = sender as TextBox;
            double delta = 0;
            if (e.Delta > 0) delta = 0.01;
            else delta = -0.01;
            if (increWheelAdjust) delta *= 10;
            double value = txb.Text.ParseDouble();
            value += delta;
            txb.Text = value.ForcePositive().ToString(6);
            QueueRefresh();
        }

        private void ShiftKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift) increWheelAdjust = true;
        }

        private void ShiftKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift) increWheelAdjust = false;
        }

        private void QueueRefresh()
        {
            refreshTimer.Stop();
            refreshTimer.Start();
        }

        private void EditFocusLost(object sender, RoutedEventArgs e)
        {
            QueueRefresh();
        }
    }
}
