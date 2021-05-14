using RelertSharp.Wpf.Common;
using RelertSharp.Wpf.ViewModel;
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
    /// TriggerView.xaml 的交互逻辑
    /// </summary>
    public partial class TriggerView : UserControl, IObjectReciver, IRsView
    {
        public TriggerView()
        {
            InitializeComponent();
            DataContext = new TriggerVm();
        }

        public GuiViewType ViewType => GuiViewType.Trigger;

        public void ReciveObject(object sender, object recived)
        {
            if (recived != null) DataContext = new TriggerVm(recived);
            else DataContext = new TriggerVm();
        }

        private void ScrollToSelection(object sender, SelectionChangedEventArgs e)
        {
            if (lbxOwner.SelectedItem != null) lbxOwner.ScrollIntoView(lbxOwner.SelectedItem);
        }
    }
}
