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
        public event Dele3dLocateableHandler MousePosChanged;
        private IntPtr _handle;
        private bool drew = false;
        private bool rendering = false;
        private int prevW, prevH;
        private int nWidth { get { return this.ScaledWidth(); } }
        private int nHeight { get { return this.ScaledHeight(); } }

        private static readonly object renderLock = new object();
        private static readonly object resizeLock = new object();


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
            EngineApi.MouseMoveTileMarkRedrawRequest += EngineApi_MouseMoveTileMarkRedrawRequest;
            EngineApi.RedrawRequest += RedrawInvokeHandler;
            EngineApi.ResizeRequest += ResizeInvokeHandler;
            //CompositionTarget.Rendering += CompositionTarget_Rendering;

            _baseRefreshing = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 0, 0, 400)
            };
            _baseRefreshing.Tick += ResizeTick;
            _baseRefreshing.Start();
        }

        private void ResizeInvokeHandler(object sender, EventArgs e)
        {
            Resize();
        }

        private void RedrawInvokeHandler(object sender, EventArgs e)
        {
            RenderFrame();
        }

        private void EngineApi_MouseMoveTileMarkRedrawRequest(object sender, EventArgs e)
        {
            RenderFrame();
        }

        private void FrontBufferChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            RenderFrame();
        }

        private void ResizeTick(object sender, EventArgs e)
        {
            if (nWidth != prevW || nHeight != prevH)
            {
                Resize();
            }
        }
        private void Resize()
        {
            lock (resizeLock)
            {
                int w = (int)(nWidth * EngineApi.ScaleFactor);
                int h = (int)(nHeight * EngineApi.ScaleFactor);
                if (d3dimg.PixelWidth != w || d3dimg.PixelHeight != h)
                {
                    prevW = w; prevH = h;
                    EngineApi.SuspendRendering();
                    _handle = EngineApi.ResetHandle(w, h);
                    EngineApi.ResizeMinimapClientWindow(w, h);
                    EngineApi.ResumeRendering();
                    RenderFrame();
                }
            }
        }

        private void RenderFrame()
        {
            lock (renderLock)
            {
                if (drew && !rendering)
                {
                    //EngineApi.RefreshFrame();
                    if (d3dimg.IsFrontBufferAvailable /*&& arg.RenderingTime != lastRender*/)
                    {
                        //_handle = EngineApi.ResetHandle(nWidth, nHeight);
                        if (_handle != IntPtr.Zero)
                        {
                            rendering = true;
                            d3dimg.Lock();
                            d3dimg.SetBackBuffer(D3DResourceType.IDirect3DSurface9, _handle);
                            EngineApi.RenderFrame();
                            d3dimg.AddDirtyRect(new Int32Rect(0, 0, d3dimg.PixelWidth, d3dimg.PixelHeight));
                            d3dimg.Unlock();
                            rendering = false;
                            //lastRender = arg.RenderingTime;
                        }
                    }
                }
            }
        }

        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            if (drew)
            {
                Point p = e.GetPosition(this);
                p.X *= GuiUtil.MonitorScale * EngineApi.ScaleFactor;
                p.Y *= GuiUtil.MonitorScale * EngineApi.ScaleFactor;
                Vec3 pos = EngineApi.ClientPointToCellPos(p.GdiPoint(), out int subcell);
                if (EngineApi.MouseOnTile(pos)) MousePosChanged?.Invoke(this, pos.To3dLocateable());
            }
        }

        private void HandleMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (drew)
            {
                if (e.Delta > 0) EngineApi.ChangeScaleFactor(-0.1);
                else EngineApi.ChangeScaleFactor(0.1);
            }
        }
    }
}
