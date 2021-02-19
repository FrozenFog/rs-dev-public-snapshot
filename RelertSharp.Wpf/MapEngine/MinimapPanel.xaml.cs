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
using RelertSharp.Common;
using RelertSharp.Engine.Api;

namespace RelertSharp.Wpf.MapEngine
{
    /// <summary>
    /// MinimapPanel.xaml 的交互逻辑
    /// </summary>
    public partial class MinimapPanel : UserControl
    {
        private bool drew = false;
        private bool moving = false;
        public MinimapPanel()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            EngineApi.MinimapRedrawed += MinimapRedrawed;
            EngineApi.ResetMinimap(GlobalVar.CurrentMapDocument.Map.Info.Size, (int)ActualWidth, (int)ActualHeight, GuiUtil.MonitorScale);
        }

        public void ResumeDrawing()
        {
            drew = true;
        }
        public void SuspendDrawing()
        {
            drew = false;
        }

        private void MinimapRedrawed(object sender, EventArgs e)
        {
            if (drew)
            {
                ImageSource src = EngineApi.MinimapImgSource;
                this.img.Source = src;
            }
        }

        private void PanelSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (drew)
            {
                EngineApi.ResizeMinimap(this.ScaledWidth(), this.ScaledHeight());
            }
        }

        private void HandleMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                moving = true;
                HandleMouseMove(sender, e);
            }
        }

        private void HandleMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                moving = false;
            }
        }

        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            if (drew && moving)
            {
                double scale = this.GetScale();
                Point p = e.GetPosition(this);
                p.X *= scale;
                p.Y *= scale;
                EngineApi.MinimapMoveTo(p.GdiPoint());
                EngineApi.InvokeRedraw();
            }
        }

        private void HandleMouseLeave(object sender, MouseEventArgs e)
        {
            moving = false;
        }
    }
}
