using RelertSharp.Common;
using RelertSharp.MapStructure;
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
    /// DlgMonitorResult.xaml 的交互逻辑
    /// </summary>
    public partial class DlgMonitorResult : Window
    {
        public DlgMonitorResult()
        {
            InitializeComponent();
        }

        public void ReadFromMonitor(MapReadingMonitor monitor)
        {
            foreach (var log in monitor.GetLogs)
            {
                MonitorResultVm vm = new MonitorResultVm()
                {
                    Id = log.Id,
                    Message = log.Message,
                    Name = log.Name,
                    Type = log.Type,
                    Data = log.Data
                };
                lvMain.Items.Add(vm);
            }
        }



        internal class MonitorResultVm
        {
            public object Data { get; set; }
            public string Type { get; set; }
            public int X
            {
                get { if (Data is I2dLocateable pos) return pos.X; return -1; }
            }
            public int Y
            {
                get { if (Data is I2dLocateable pos) return pos.Y; return -1; }
            }
            public string Message { get; set; }
            public string Name { get; set; }
            public string Id { get; set; }
        }

        private void Accept(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
