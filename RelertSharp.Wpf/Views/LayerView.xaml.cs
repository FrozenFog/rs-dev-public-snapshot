using AvalonDock.Layout;
using RelertSharp.Common;
using RelertSharp.Engine.Api;
using RelertSharp.Wpf.Common;
using RelertSharp.Wpf.MapEngine.Helper;
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
    /// LayerView.xaml 的交互逻辑
    /// </summary>
    public partial class LayerView : UserControl, IRsView
    {
        public LayerView()
        {
            InitializeComponent();
        }

        public GuiViewType ViewType => GuiViewType.LayerControl;

        public LayoutAnchorable ParentAncorable { get; set; }
        public LayoutDocument ParentDocument { get; set; }

        private void SelectableChanged(object sender, RoutedEventArgs e)
        {
            EngineApi.InvokeLock();
            CheckBox ckb = sender as CheckBox;
            string tag = ckb.Tag.ToString();
            Enum.TryParse<MapObjectType>(tag, out MapObjectType type);
            if (ckb.IsChecked.Value) Selector.AddSelectionFlag(type);
            else Selector.RemoveSelectionFlag(type);
            EngineApi.InvokeUnlock();
        }

        private void VisibleChanged(object sender, RoutedEventArgs e)
        {
            EngineApi.InvokeLock();
            CheckBox ckb = sender as CheckBox;
            bool isVisible = ckb.IsChecked.Value;
            string tag = ckb.Tag.ToString();
            Enum.TryParse<MapObjectType>(tag, out MapObjectType type);
            LayerControl.SetVisibility(type, isVisible);
            if (!isVisible)
            {
                foreach (var child in stkSelectable.Children)
                {
                    if (child is CheckBox selectableCkb && selectableCkb.Tag.ToString() == tag)
                    {
                        selectableCkb.IsChecked = false;
                        SelectableChanged(selectableCkb, null);
                        selectableCkb.IsEnabled = false;
                        break;
                    }
                }
            }
            else
            {
                foreach (var child in stkSelectable.Children)
                {
                    if (child is CheckBox selectableCkb && selectableCkb.Tag.ToString() == tag)
                    {
                        selectableCkb.IsEnabled = true;
                        break;
                    }
                }
            }
            EngineApi.InvokeRedraw();
            EngineApi.InvokeUnlock();
        }
    }
}
