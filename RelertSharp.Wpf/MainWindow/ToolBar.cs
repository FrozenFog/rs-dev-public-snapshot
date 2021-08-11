using RelertSharp.Wpf.MapEngine.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RelertSharp.Wpf
{
    public partial class MainWindow : Window
    {
        private void ToolNavigateBack(object sender, RoutedEventArgs e)
        {
            NavigationHub.BackTrace();
        }

        private void ToolRiseTile(object sender, RoutedEventArgs e)
        {
            TileSelector.RiseAllSelectedTile();
        }

        private void ToolNavigateForward(object sender, RoutedEventArgs e)
        {
            NavigationHub.RedoTrace();
        }

        private void ToolSinkTile(object sender, RoutedEventArgs e)
        {
            TileSelector.SinkAllSelectedTile();
        }

        private void ToolRoughRamp(object sender, RoutedEventArgs e)
        {
            TileSelector.FixRampInSelectedTile();
        }
    }
}
