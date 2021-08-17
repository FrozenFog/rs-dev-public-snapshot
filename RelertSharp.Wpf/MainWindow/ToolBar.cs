using RelertSharp.Engine.Api;
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
            EngineApi.InvokeRedraw();
        }

        private void ToolNavigateForward(object sender, RoutedEventArgs e)
        {
            NavigationHub.RedoTrace();
        }

        private void ToolSinkTile(object sender, RoutedEventArgs e)
        {
            TileSelector.SinkAllSelectedTile();
            EngineApi.InvokeRedraw();
        }

        private void ToolSmoothRamp(object sender, RoutedEventArgs e)
        {
            TileSelector.FixRampInSelectedTile();
            EngineApi.InvokeRedraw();
        }

        private void ToolRoughRamp(object sender, RoutedEventArgs e)
        {
            TileSelector.RoughRampInSelectedTile();
            EngineApi.InvokeRedraw();
        }

        private void ToolClearTile(object sender, RoutedEventArgs e)
        {
            TileSelector.ClearAllTileAsZero(false);
            EngineApi.InvokeRedraw();
        }

        private void ToolZeroTile(object sender, RoutedEventArgs e)
        {
            TileSelector.ClearAllTileAsZero(true);
            EngineApi.InvokeRedraw();
        }

        private void ToolUnPhaseAllTile(object sender, RoutedEventArgs e)
        {
            TileSelector.UnPhaseAllTile();
            EngineApi.InvokeRedraw();
        }
    }
}
