using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// DlgTriggerListAddGroup.xaml 的交互逻辑
    /// </summary>
    public partial class DlgTriggerListAddGroup : Window
    {
        public DlgTriggerListAddGroup()
        {
            InitializeComponent();
        }

        private bool IsValidName()
        {
            if (ResultName.IsNullOrEmpty()) return false;
            if (ResultName.ContainChars('=', ',', '.')) return false;
            return true;
        }



        public string ResultName { get { return txbName.Text; } }
        public bool IsRoot { get { return ckbRoot.IsChecked.Value; } }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (!IsValidName())
            {
                GuiUtil.Warning("Invalid group name, must not contain \"=\", \",\", or \".\"");
                return;
            }
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
