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
using RelertSharp.Engine.Api;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.Wpf.MapEngine
{
    /// <summary>
    /// MainPanel.xaml 的交互逻辑
    /// </summary>
    public partial class MainPanel : UserControl
    {
        public MainPanel()
        {
            InitializeComponent();
        }
        public void Initialize()
        {
            HwndSource hwnd = ((HwndSource)PresentationSource.FromVisual(this.cnvMain));
            EngineApi.EngineCtor(hwnd.Handle);
        }
        public async void DrawMap()
        {
            await EngineApi.DrawMap(CurrentMapDocument.Map);
        }
    }
}
