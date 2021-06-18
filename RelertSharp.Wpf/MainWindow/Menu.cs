using RelertSharp.Wpf.Common;
using RelertSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace RelertSharp.Wpf
{
    public partial class MainWindow : Window
    {
        #region File
        private void MenuOpenMap(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string name = dlg.FileName;
                Engine.Api.EngineApi.DisposeMap();
                GlobalVar.LoadMapDocument(name);
            }
        }
        #endregion
        #region Debug
        private const string mapname = "areddawn.map";
        private void DebugClick()
        {
            GlobalVar.LoadMapDocument(mapname);
        }

        private void DebugClick(object sender, RoutedEventArgs e)
        {
            DebugClick();
        }

        private void DebugClick2(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
