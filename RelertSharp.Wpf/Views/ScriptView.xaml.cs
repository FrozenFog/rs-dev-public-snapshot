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
using RelertSharp.Wpf.ViewModel;

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// ScriptView.xaml 的交互逻辑
    /// </summary>
    public partial class ScriptView : UserControl, IObjectReciver
    {
        private ScriptVm context { get { return DataContext as ScriptVm; } }
        public ScriptView()
        {
            InitializeComponent();
            DataContext = new ScriptVm();
        }

        public void ReciveObject(object sender, object recived)
        {
            DataContext = new ScriptVm(recived);
        }
    }
}
