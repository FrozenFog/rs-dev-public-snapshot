using RelertSharp.Wpf.Common;
using RelertSharp.Common;
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
        #region Debug
        private void DebugClick()
        {
            tmrInit.Stop();
            triggerList.ReloadMapTrigger();
            ObjectBrushConfig cfg = new ObjectBrushConfig();
            ObjectBrushFilter filter = new ObjectBrushFilter();
            PaintBrush.SetConfig(cfg, filter);
            objectPanel.ReloadAllObjects();
            objectPanel.BindBrushConfig(cfg, filter);
            Terraformer.HillGenerator.RunTest();
        }

        private void DebugClick(object sender, RoutedEventArgs e)
        {
            DebugClick();
        }

        private void DebugClick2(object sender, RoutedEventArgs e)
        {
            //NavigationHub.BackTrace();
            Algorithm.PerlinNoiseGeneratorConfig cfg = new Algorithm.PerlinNoiseGeneratorConfig()
            {
                Amplify = false,
                Width = 50,
                Height = 50,
                Iteration = 10,
                Scale = 5f,
                SmoothIteration = 15
            };
            double[] r = RelertSharp.Algorithm.PerlinNoiseGenerator.Generate2DNoise(cfg);

            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < 50; y++)
            {
                for (int x = 0; x < 50; x++)
                {
                    double v = r[x + y * 50];
                    sb.Append(string.Format("{0}\t{1}\t{2}\n", x, y, v));
                }
            }
            Utils.Misc.DebugSave(sb.ToString(), "123.txt");
        }
        #endregion
    }
}
