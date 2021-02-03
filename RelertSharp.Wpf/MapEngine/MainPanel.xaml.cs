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
using System.Windows.Interop;

namespace RelertSharp.Wpf.MapEngine
{
    /// <summary>
    /// MainPanel.xaml 的交互逻辑
    /// </summary>
    public partial class MainPanel : UserControl
    {
        private IntPtr Handle;
        public MainPanel()
        {
            InitializeComponent();
        }
        public void Initialize()
        {
            HwndSource a = ((HwndSource)PresentationSource.FromVisual(this.cnvMain));
            Handle = a.Handle;
        }
    }
}
