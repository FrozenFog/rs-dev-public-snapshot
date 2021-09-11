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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static RelertSharp.Wpf.Common.GuiConst.Strings;

namespace RelertSharp.Wpf.Dialogs
{
    /// <summary>
    /// DlgSetting.xaml 的交互逻辑
    /// </summary>
    public partial class DlgSetting : Window
    {
        private SettingVm vm = new SettingVm();
        public DlgSetting()
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void CancelChange(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void AcceptChange(object sender, RoutedEventArgs e)
        {
            vm.ApplyChanges();
            GuiUtil.Asterisk("Settings applied!\nSome changes need to restart the program to take effect.");
            DialogResult = true;
        }

        private void SelectGamePath(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = dlg.SelectedPath;
                if (path.IsNullOrEmpty()) GuiUtil.Warning("Invalid game path!");
                else vm.GamePath = path;
            }
        }

        private void SelectConfigPath(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                AddExtension = true,
                Filter = FILTER_CONFIG
            };
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = dlg.FileName;
                if (path.IsNullOrEmpty()) GuiUtil.Warning("Invalid config path!");
                else vm.ConfigPath = path;
            }
        }
    }
}
