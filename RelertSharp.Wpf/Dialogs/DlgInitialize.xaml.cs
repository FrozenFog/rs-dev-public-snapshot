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
    /// DlgInitialize.xaml 的交互逻辑
    /// </summary>
    public partial class DlgInitialize : Window
    {
        private bool disposed = false;
        public DlgInitialize()
        {
            InitializeComponent();
        }


        public void SetStatus(string content)
        {
            Dispatcher.Invoke(() => txblStatus.Text = content);
        }
        public void CallShowDlg()
        {
            if (!disposed)
            {
                ShowDialog();
            }
        }
        public void CallClose()
        {
            Dispatcher.Invoke(() =>
            {
                disposed = true;
                DialogResult = true;
                Close();
            });
        }
    }
}
