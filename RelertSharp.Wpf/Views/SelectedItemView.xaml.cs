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
        public SelectedItemView()
        {
            InitializeComponent();
        }
    }
}
