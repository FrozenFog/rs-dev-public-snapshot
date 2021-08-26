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
    /// DlgNampInput.xaml 的交互逻辑
    /// </summary>
    public partial class DlgNameInput : Window
    {
        public DlgNameInput()
        {
            InitializeComponent();
            Validation = IsValidName;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="label">Default label: Name</param>
        /// <param name="title">Default title: Relert Sharp</param>
        public DlgNameInput(string label = null, string title = null)
        {
            InitializeComponent();
            if (!title.IsNullOrEmpty()) Title = title;
            if (!label.IsNullOrEmpty()) lblName.Content = label;
        }


        #region Private
        private bool IsValidName(string name)
        {
            if (name.IsNullOrEmpty()) return false;
            if (name.ContainChars('=', ',', '.')) return false;
            return true;
        }
        #endregion

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (Validation == null) Validation = IsValidName;
            if (ResultName.IsNullOrEmpty())
            {
                GuiUtil.Warning("Name is empty.");
                return;
            }
            if (!Validation(ResultName))
            {
                GuiUtil.Warning(InvalidWarning);
                return;
            }
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }


        public string ResultName { get { return txbName.Text; } }
        public string InvalidWarning { get; set; } = "Invalid name, must not contain \"=\", \",\"";
        public Predicate<string> Validation { get; set; } 
    }
}
