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
        public LightningView()
        {
            InitializeComponent();
            DataContext = new LightningVm();
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
            IsControlEnable(ckbEnable.IsChecked.Value);
            LightningTypeChanged(null, null);
        }
    }
}
