using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RelertSharp.Engine.Api;
using RelertSharp.Common;
using System.Windows.Threading;

namespace RelertSharp.Wpf.MapEngine
{
    /// <summary>
    /// MainPanel.xaml 的交互逻辑
    /// </summary>
    public partial class MainPanel : UserControl
    {
        private IntPtr _handle;
        private TimeSpan lastRender = new TimeSpan();
        private const string D3DSOURCE = "SceneResource";
        private readonly object _updating = new object();
        private bool initialized = false;
        private bool drew = false;
        private readonly object _resizing = new object();
        private int nWidth { get { return this.ScaledWidth(); } }
        private int nHeight { get { return this.ScaledHeight(); } }
        private DispatcherTimer _baseRefreshing;




        public MainPanel()
        {
            InitializeComponent();
        }
        public async void DrawMap()
        {
            EngineApi.SuspendRendering();
            EngineApi.DrawMap(GlobalVar.CurrentMapDocument.Map);
            drew = true;
            EngineApi.ResumeRendering();
            RenderFrame();
        }
        public void InitializePanel()
        {
            EngineApi.EngineCtor(nWidth, nHeight);
            _handle = EngineApi.ResetHandle(nWidth, nHeight);
            d3dimg.IsFrontBufferAvailableChanged += FrontBufferChanged;
            //CompositionTarget.Rendering += CompositionTarget_Rendering;

            _baseRefreshing = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 0, 0, 400)
            };
            _baseRefreshing.Tick += ResizeTick;
            _baseRefreshing.Start();
        }

        private void FrontBufferChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (drew)
            {
                RenderFrame();
            }
        }

        private void ResizeTick(object sender, EventArgs e)
        {
            if ((int)imgelt.ActualWidth != d3dimg.Width || (int)imgelt.ActualHeight != d3dimg.Height)
            {
                EngineApi.SuspendRendering();
                _handle = EngineApi.ResetHandle(nWidth, nHeight);
                EngineApi.ResumeRendering();
                RenderFrame();
            }
        }

        private void RenderFrame()
        {
            if (drew)
            {
                //EngineApi.RefreshFrame();
                if (d3dimg.IsFrontBufferAvailable /*&& arg.RenderingTime != lastRender*/)
                {
                    //_handle = EngineApi.ResetHandle(nWidth, nHeight);
                    if (_handle != IntPtr.Zero)
                    {
                        d3dimg.Lock();
                        d3dimg.SetBackBuffer(D3DResourceType.IDirect3DSurface9, _handle);
                        EngineApi.RefreshFrame();
                        d3dimg.AddDirtyRect(new Int32Rect(0, 0, d3dimg.PixelWidth, d3dimg.PixelHeight));
                        d3dimg.Unlock();
                        //lastRender = arg.RenderingTime;
                    }
                }
            }
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            RenderingEventArgs arg = (RenderingEventArgs)e;
            if (drew)
            {
                if (d3dimg.IsFrontBufferAvailable && arg.RenderingTime != lastRender)
                {
                    //_handle = EngineApi.ResetHandle(nWidth, nHeight);
                    if (_handle != IntPtr.Zero)
                    {
                        d3dimg.Lock();
                        d3dimg.SetBackBuffer(D3DResourceType.IDirect3DSurface9, _handle);
                        EngineApi.RefreshFrame();
                        d3dimg.AddDirtyRect(new Int32Rect(0, 0, d3dimg.PixelWidth, d3dimg.PixelHeight));
                        d3dimg.Unlock();
                        lastRender = arg.RenderingTime;
                    }
                }
            }
        }
    }
}
