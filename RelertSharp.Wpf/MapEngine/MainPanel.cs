using RelertSharp.Engine.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.Wpf.MapEngine
{
    public partial class MainPanel : UserControl
    {
        public event EventHandler RedrawBegin;
        public event EventHandler RedrawEnd;
        private bool initialized = false;
        public MainPanel()
        {
            InitializeComponent();
        }

        public void InitializePanel()
        {
            bool b = EngineApi.EngineCtor(Width, Height);
            initialized = true;
        }
        public async void DrawMap()
        {
            EngineApi.DrawMap(CurrentMapDocument.Map);
        }

        private void MainPanel_Resize(object sender, EventArgs e)
        {
            RedrawBegin?.Invoke(this, new EventArgs());
            EngineApi.RefreshFrame();
            RedrawEnd?.Invoke(this, new EventArgs());
        }
    }
}
