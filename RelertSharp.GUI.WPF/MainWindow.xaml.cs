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
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Logic;
using RelertSharp.FileSystem;

namespace RelertSharp.GUI.WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Map map;
        private TriggerItem[] triggers;
        private int i = 0;


        public MainWindow()
        {
            InitializeComponent();
            MapFile mf = new MapFile("ESURV.map");
            map = mf.Map;
            triggers = map.Triggers.ToArray();
            txbName.DataContext = triggers[i];
        }

        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            txbName.DataContext = triggers[i++];
        }
    }
}
