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
    /// DlgCmdInput.xaml 的交互逻辑
    /// </summary>
    public partial class DlgCmdInput : Window
    {
        public DlgCmdInput()
        {
            InitializeComponent();
        }
        public DlgCmdInput(string label = null, string title = null)
        {
            InitializeComponent();
            if (!title.IsNullOrEmpty()) Title = title;
            if (!label.IsNullOrEmpty()) lblName.Content = label;
        }

        #region Private
        private bool IsValidCommand()
        {
            return true;
        }
        #endregion

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (ResultCommand.IsNullOrEmpty())
            {
                GuiUtil.Warning("Empty command!");
                return;
            }
            if (!IsValidCommand())
            {
                GuiUtil.Warning("Invalid command!");
                return;
            }
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }


        public string ResultCommand { get { return txbMain.Text; } }
    }
}
