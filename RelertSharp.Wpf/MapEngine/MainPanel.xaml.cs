﻿using System;
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

namespace RelertSharp.Wpf.MapEngine
{
    /// <summary>
    /// MainPanel.xaml 的交互逻辑
    /// </summary>
    public partial class MainPanel : UserControl
    {
        public event Dele3dLocateableHandler MousePosChanged;
        public event EventHandler ScaleFactorChanged;
        private IntPtr _handle;
        private bool drew = false;
        private bool rendering = false;
        private int prevW, prevH;
        private int nWidth { get { return this.ScaledWidth(); } }
        private int nHeight { get { return this.ScaledHeight(); } }

        private static readonly object renderLock = new object();
        private static readonly object mouseHandleLock = new object();
        private bool handlingMouse = false;


        private DispatcherTimer _baseRefreshing;
        private DispatcherTimer _wheelResize;




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
            EngineApi.EngineCtor(nWidth, nHeight);
            _handle = EngineApi.ResetHandle(nWidth, nHeight);
            d3dimg.IsFrontBufferAvailableChanged += FrontBufferChanged;
            EngineApi.MouseMoveTileMarkRedrawRequest += EngineApi_MouseMoveTileMarkRedrawRequest;
            EngineApi.RedrawRequest += RedrawInvokeHandler;
            EngineApi.ResizeRequest += ResizeInvokeHandler;
            _wheelResize = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 0, 0, 700)
            };
            _wheelResize.Tick += WheelResizeInvoker;
            //CompositionTarget.Rendering += CompositionTarget_Rendering;

            //_baseRefreshing = new DispatcherTimer()
            //{
            //    Interval = new TimeSpan(0, 0, 0, 0, 400)
            //};
            //_baseRefreshing.Tick += ResizeTick;
            //_baseRefreshing.Start();
        }

        private void WheelResizeInvoker(object sender, EventArgs e)
        {
            if (drew)
            {
                EngineApi.ScaleFactorInvoke();
                _wheelResize.Stop();
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
            if (!handlingMouse)
            {
                if (drew)
                {
                    Point p = e.GetPosition(this);
                    p.X *= GuiUtil.MonitorScale * EngineApi.ScaleFactor;
                    p.Y *= GuiUtil.MonitorScale * EngineApi.ScaleFactor;
                    Vec3 pos = EngineApi.ClientPointToCellPos(p.GdiPoint(), out int subcell);
                    if (EngineApi.MouseOnTile(pos))
                    {
                        handlingMouse = true;
                        MousePosChanged?.Invoke(this, pos.To3dLocateable());
                        handlingMouse = false;
                    }
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

        private void HandleMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (drew)
            {
                _wheelResize.Stop();
                _wheelResize.Start();
                SuspendMouseHandler();
                if (e.Delta > 0) EngineApi.ChangeScaleFactor(-0.1);
                else EngineApi.ChangeScaleFactor(0.1);
                ScaleFactorChanged?.Invoke(null, null);
            }
        }
    }
}
