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
using RelertSharp.Engine;

namespace RelertSharp.Wpf.MapEngine
{
    /// <summary>
    /// MainPanel.xaml 的交互逻辑
    /// </summary>
    public partial class MainPanel : UserControl, IRsView
    {
        public event I3dLocateableHandler MousePosChanged;
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

        //private RenderTargetBitmap img;




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
#if DEBUG
            EngineApi.DrawMap(GlobalVar.CurrentMapDocument.Map);
#else
            await EngineApi.DrawMap(GlobalVar.CurrentMapDocument.Map);
#endif
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
            EngineApi.RedrawRequested += RedrawInvokeHandler;
            EngineApi.ResizeRequested += ResizeInvokeHandler;
            EngineApi.LockRequested += LockInvokeHandler;
            EngineApi.UnlockRequested += UnlockInvokeHandler;
            NavigationHub.GoToPositionRequest += MoveCameraInvokeHandler;
        }

        #region PERMANENTLY SOLVED ENGINE DEAD
        private bool locked = false;
        private void UnlockInvokeHandler(object sender, EventArgs e)
        {
            if (locked)
            {
                d3dimg.Unlock();
                locked = false;
            }
        }

        private void LockInvokeHandler(object sender, EventArgs e)
        {
            if (!locked)
            {
                d3dimg.Lock();
                locked = true;
            }
        }
        #endregion

        private void ResizeInvokeHandler(object sender, EventArgs e)
        {
            Resize();
            RenderFrame();
        }

        private void RedrawInvokeHandler(object sender, EventArgs e)
        {
            RenderFrame();
        }

        private void MoveCameraInvokeHandler(I3dLocateable pos)
        {
            if (drew)
            {
                EngineApi.MoveCameraTo(pos, pos.Z);
                RenderFrame();
            }
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
                }
            }
        }

        private void RenderFrame(bool manualLock = false)
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
                                if (!manualLock) EngineApi.InvokeLock();
                                d3dimg.SetBackBuffer(D3DResourceType.IDirect3DSurface9, _handle);
                                EngineApi.RenderFrame();
                                d3dimg.AddDirtyRect(new Int32Rect(0, 0, d3dimg.PixelWidth, d3dimg.PixelHeight));
                                if (!manualLock) EngineApi.InvokeUnlock();
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

                    EngineApi.ScaleFactorInvoke();
                    //_wheelResizeDelay.Stop();
                    //_wheelResizeDelay.Start();
                    //SuspendMouseHandler();
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
            if (drew)
            {
                EngineApi.InvokeLock();
                Point p = e.GetPosition(this);
                Point pOrg = e.GetPosition(this);
                GuiUtil.ScaleWpfMousePoint(ref p);
                if (e.ChangedButton == MouseButton.Left) this.LmbDown(p, pOrg);
                else if (e.ChangedButton == MouseButton.Right) this.RmbDown(p, pOrg);
                else if (e.ChangedButton == MouseButton.Middle) this.MmbDown(p, pOrg);
                EngineApi.InvokeUnlock();
            }
        }

        private void HandleMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (drew)
            {
                EngineApi.InvokeLock();
                if (e.Delta > 0) EngineApi.ChangeScaleFactor(-0.1);
                else EngineApi.ChangeScaleFactor(0.1);
                ScaleFactorChanged?.Invoke(null, null);
                EngineApi.ScaleFactorInvoke();
                EngineApi.InvokeUnlock();
            }
        }
    }
}
