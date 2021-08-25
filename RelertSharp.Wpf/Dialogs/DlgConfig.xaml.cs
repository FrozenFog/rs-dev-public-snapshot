using RelertSharp.Common;
using RelertSharp.Common.Config;
using RelertSharp.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace RelertSharp.Wpf.Dialogs
{
    /// <summary>
    /// DlgConfig.xaml 的交互逻辑
    /// </summary>
    public partial class DlgConfig : Window
    {
        private readonly ConfigDialogVm vm = new ConfigDialogVm();
        public DlgConfig()
        {
            InitializeComponent();
            DataContext = null;
            DataContext = vm;
        }

        private void OpenPath(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                AddExtension = true,
                Filter = "Relert sharp xml config file (*.xml)|*.xml"
            };
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string name = dlg.FileName;
                try
                {
                    GlobalVar.GlobalConfig = new RsConfig(name);
                }
                catch (Exception ex)
                {
                    GuiUtil.Fatal("Invalid config file.");
                    GlobalVar.GlobalConfig = null;
                    txbPath.Text = string.Empty;
                }
                txbPath.Text = dlg.FileName;
                txbInfo.Text = GlobalVar.GlobalConfig.Info;
                vm.Update();
            }
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void AcceptClick(object sender, RoutedEventArgs e)
        {
            if (GlobalVar.GlobalConfig == null)
            {
                GuiUtil.Fatal("Invalid config file.");
            }
            else if (!ValidGamePath(GlobalVar.GlobalConfig.GamePath))
            {
                GuiUtil.Fatal("Cannot find valid game executeable file.\n(gamemd.exe or game.exe)");
            }
            else
            {
                DialogResult = true;
                Close();
            }
        }

        private void GamePath(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = dlg.SelectedPath;
                if (ValidGamePath(path))
                {
                    vm.GamePath = path;
                }
                else
                {
                    GuiUtil.Fatal("Cannot find valid game executeable file.\n(gamemd.exe or game.exe)");
                    vm.GamePath = string.Empty;
                }
            }
        }


        private bool ValidGamePath(string path)
        {
            if (path.IsNullOrEmpty()) return false;
            return File.Exists(System.IO.Path.Combine(path, "gamemd.exe")) ||
                    File.Exists(System.IO.Path.Combine(path, "game.exe"));
        }
    }
}
