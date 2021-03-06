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
    /// DlgDangerousCommit.xaml 的交互逻辑
    /// </summary>
    public partial class DlgDangerousCommit : Window
    {
        private List<string> required = new List<string>();
        private int i = 0;
        public DlgDangerousCommit()
        {
            InitializeComponent();
        }
        public DlgDangerousCommit(string contentString)
        {
            InitializeComponent();
            content.Text = contentString;
        }
        public DlgDangerousCommit(params string[] contents)
        {
            InitializeComponent();
            required = contents.ToList();
            content.Text = required.First();
        }

        public void SetContents(List<string> src)
        {
            required = src;
            i = 0;
            if (required.Count > 0) content.Text = required[i];
        }

        private void Commit(object sender, RoutedEventArgs e)
        {
            if (txbContent.Text == content.Text)
            {
                if (i + 1 >= required.Count) DialogResult = true;
                else
                {
                    content.Text = required[++i];
                    txbContent.Text = string.Empty;
                }
            }
        }

        private void Abort(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void InputChanged(object sender, TextChangedEventArgs e)
        {
            btnCommit.IsEnabled = content.Text == txbContent.Text;
        }
    }
}
