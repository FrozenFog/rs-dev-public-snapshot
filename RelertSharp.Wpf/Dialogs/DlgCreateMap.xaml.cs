using RelertSharp.Common;
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
using System.Windows.Shapes;

namespace RelertSharp.Wpf.Dialogs
{
    /// <summary>
    /// DlgCreateMap.xaml 的交互逻辑
    /// </summary>
    public partial class DlgCreateMap : Window
    {
        private MapCreatingVm vm = new MapCreatingVm();
        public DlgCreateMap()
        {
            InitializeComponent();
            LoadComboBox();
            DataContext = null;
            DataContext = vm;
        }

        private void LoadComboBox()
        {
            foreach (var p in GlobalVar.GlobalRules[Constant.RulesHead.HEAD_COUNTRY])
            {
                cbbHouse.Items.Add(p.Value);
            }
            foreach (var item in GlobalVar.GlobalConfig.ModGeneral.Theater)
            {
                if (!item.IsVirtual) cbbTheater.Items.Add(item.Name);
            }
        }



        public IMapCreationConfig Config { get { return vm.Config; } }

        private void Accept(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
