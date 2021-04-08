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
using System.Threading;
using RelertSharp.Wpf.Views;
using RelertSharp.Wpf.Common;

namespace RelertSharp.Wpf.MapEngine
{
    /// <summary>
    /// MainPanel.xaml 的交互逻辑
    /// </summary>
    public partial class MainPanel : UserControl, IRsView
    {
        public event Dele3dLocateableHandler MousePosChanged;
        public event EventHandler ScaleFactorChanged;
        private IntPtr _handle;
        private bool drew = false;
        private bool rendering = false;
        private int prevW, prevH;
        private int nWidth { get { return this.ScaledWidth(); } }
        private int nHeight { get { return this.ScaledHeight(); } }
        private double dpi;

        public GuiViewType ViewType { get { return GuiViewType.MainPanel; } }

        private static readonly object lockRender = new object();
        private static readonly object lockMouse = new object();

        private const int susMsResize = 600;


        private DispatcherTimer _wheelResizeDelay;
        private DispatcherTimer _suspendMouseDelay;

        private RenderTargetBitmap img;




        public MainPanel()
        {
            InitializeComponent();
        }
        internal void HandleRedrawRequest()
        {
            if (drew)
            {
                RenderFrame();
            }
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
            dpi = this.GetScale();
            EngineApi.EngineCtor(nWidth, nHeight);
            _handle = EngineApi.ResetHandle(nWidth, nHeight);
            d3dimg.IsFrontBufferAvailableChanged += FrontBufferChanged;
            EngineApi.RedrawRequest += RedrawInvokeHandler;
            EngineApi.ResizeRequest += ResizeInvokeHandler;
            _wheelResizeDelay = new DispatcherTimer();
            _wheelResizeDelay.Tick += WheelResizeInvoker;
            _wheelResizeDelay.Interval = new TimeSpan(0, 0, 0, 0, susMsResize);
            _suspendMouseDelay = new DispatcherTimer();
            _suspendMouseDelay.Tick += SuspendMouseTick;

            //CompositionTarget.Rendering += CompositionTarget_Rendering;

            //_baseRefreshing = new DispatcherTimer()
            //{
            //    Interval = new TimeSpan(0, 0, 0, 0, 400)
            //};
            //_baseRefreshing.Tick += ResizeTick;
            //_baseRefreshing.Start();
        }

        private void SuspendMouseTick(object sender, EventArgs e)
        {
            if (drew && mouseSuspendedMs)
            {
                _suspendMouseDelay.Stop();
                Thread.Sleep(100);
                imgelt.MouseMove += HandleMouseMove;
                imgelt.MouseDown += HandleMouseDown;
                imgelt.MouseUp += HandleMouseUp;
            }
        }

        private void WheelResizeInvoker(object sender, EventArgs e)
        {
            if (drew)
            {
                EngineApi.ScaleFactorInvoke();
                _wheelResizeDelay.Stop();
                Thread.Sleep(300);
                ResumeMouseHandler();
            }
        }
        private bool mouseSuspended = false;
        private void SuspendMouseHandler()
        {
            if (!mouseSuspended)
            {
                mouseSuspended = true;
                imgelt.MouseMove -= HandleMouseMove;
                //imgelt.MouseWheel -= HandleMouseWheel;
            }
        }
        private bool mouseSuspendedMs = false;
        private void SuspendMouseHandlerFor(int milSec)
        {
            if (!mouseSuspendedMs)
            {
                mouseSuspended = true;
                imgelt.MouseMove -= HandleMouseMove;
                imgelt.MouseDown -= HandleMouseDown;
                imgelt.MouseUp -= HandleMouseUp;
                _suspendMouseDelay.Stop();
                _suspendMouseDelay.Interval = new TimeSpan(0, 0, 0, 0, milSec);
                _suspendMouseDelay.Start();
            }
        }
        private void ResumeMouseHandler()
        {
            if (mouseSuspended)
            {
                mouseSuspended = false;
                imgelt.MouseMove += HandleMouseMove;
                //imgelt.MouseWheel += HandleMouseWheel;
            }
        }

        private void ResizeInvokeHandler(object sender, EventArgs e)
        {
            Resize();
        }

        private void RedrawInvokeHandler(object sender, EventArgs e)
        {
            RenderFrame();
        }

        private void FrontBufferChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            RenderFrame();
        }

        private void Resize()
        {
            if (!rendering)
            {
                int w = (int)(nWidth * EngineApi.ScaleFactor);
                int h = (int)(nHeight * EngineApi.ScaleFactor);
                if (d3dimg.PixelWidth != w || d3dimg.PixelHeight != h)
                {
                    rendering = true;
                    prevW = w; prevH = h;
                    EngineApi.SuspendRendering();
                    _handle = EngineApi.ResetHandle(w, h);
                    EngineApi.ResizeMinimapClientWindow(w, h);
                    EngineApi.ResumeRendering();
                    rendering = false;
                    RenderFrame();
                }
            }
        }

        private void RenderFrame()
        {
            this.Dispatcher.Invoke(() =>
            {
                lock (lockRender)
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
            });
        }

        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            lock (lockMouse)
            {
                if (drew)
                {
                    Point p = e.GetPosition(this);
                    Point pOrg = e.GetPosition(this);
                    GuiUtil.ScaleWpfMousePoint(ref p);
                    bool redraw = MouseMoved(p, pOrg);

                    if (redraw) EngineApi.InvokeRedraw();
                }
            }
        }

        private void HandleSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (drew)
            {
                if (nWidth != prevW || nHeight != prevH)
                {
                    SuspendMouseHandler();
                    Resize();
                    ResumeMouseHandler();
                }
            }
        }

        private void HandleMouseUp(object sender, MouseButtonEventArgs e)
        {
            lock (lockMouse)
            {
                if (drew)
                {
                    Point p = e.GetPosition(this);
                    Point pOrg = e.GetPosition(this);
                    GuiUtil.ScaleWpfMousePoint(ref p);
                    if (e.ChangedButton == MouseButton.Left) this.LmbUp(p, pOrg);
                    else if (e.ChangedButton == MouseButton.Right) this.RmbUp(p, pOrg);
                    else if (e.ChangedButton == MouseButton.Middle) this.MmbUp(p, pOrg);
                }
            }
        }

        private void HandleMouseDown(object sender, MouseButtonEventArgs e)
        {
            lock (lockMouse)
            {
                if (drew)
                {
                    Point p = e.GetPosition(this);
                    Point pOrg = e.GetPosition(this);
                    GuiUtil.ScaleWpfMousePoint(ref p);
                    if (e.ChangedButton == MouseButton.Left) this.LmbDown(p, pOrg);
                    else if (e.ChangedButton == MouseButton.Right) this.RmbDown(p, pOrg);
                    else if (e.ChangedButton == MouseButton.Middle) this.MmbDown(p, pOrg);
                }
            }
        }

        private void HandleMouseWheel(object sender, MouseWheelEventArgs e)
        {
            lock (lockMouse)
            {
                if (drew)
                {
                    _wheelResizeDelay.Stop();
                    _wheelResizeDelay.Start();
                    SuspendMouseHandler();
                    if (e.Delta > 0) EngineApi.ChangeScaleFactor(-0.1);
                    else EngineApi.ChangeScaleFactor(0.1);
                    ScaleFactorChanged?.Invoke(null, null);
                }
            }
        }
    }
}
