using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.GUI.WPF
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            App app = new App();
            app.InitializeComponent();
            MainWindow mw = new MainWindow();
            app.MainWindow = mw;
            app.Run();
        }
    }
}
