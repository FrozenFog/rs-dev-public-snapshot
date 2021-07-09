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
        public AvalonDock.Layout.LayoutAnchorable ParentAncorable { get; set; }
        public AvalonDock.Layout.LayoutDocument ParentDocument { get; set; }

        private static readonly object lockRender = new object();
        private static readonly object lockMouse = new object();

        //private RenderTargetBitmap img;




        public MainPanel()
        {
            InitializeComponent();
            keyClickAction = new DelayedAction(null, KeyHoldToClick, CLICK_INTERVAL);
            GlobalVar.MapDocumentRedrawRequested += MapRedrawHandler;
            GlobalVar.MapDocumentLoaded += MapReloadedHandler;
            EngineApi.RedrawRequested += RedrawInvokeHandler;
            EngineApi.ResizeRequested += ResizeInvokeHandler;
            EngineApi.LockRequested += LockInvokeHandler;
            EngineApi.UnlockRequested += UnlockInvokeHandler;
            NavigationHub.GoToPositionRequest += MoveCameraInvokeHandler;
        }

        private void MapReloadedHandler(object sender, EventArgs e)
        {
            InitializePanel();
        }

        private void MapRedrawHandler(object sender, EventArgs e)
        {
            drew = false;
            SuspendEvent();
            DrawMap();
            ResumeEvent();
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
        private bool initialized = false;
        public void InitializePanel()
        {
            if (!initialized)
            {
                dpi = this.GetScale();
                EngineApi.EngineCtor(nWidth, nHeight);
                _handle = EngineApi.ResetHandle(nWidth, nHeight);
                d3dimg.IsFrontBufferAvailableChanged += FrontBufferChanged;
                initialized = true;
            }
        }

        #region PERMANENTLY SOLVED ENGINE DEAD
        private bool locked = false;
        private bool manualLockOverride = false;
        private int lockLevel = 0;
        private void UnlockInvokeHandler(object sender, EventArgs e)
        {
            if (locked)
            {
                manualLockOverride = false;
                lockLevel--;
                if (lockLevel == 0)
                {
                    d3dimg.Unlock();
                    locked = false;
                }
            }
        }

        private void LockInvokeHandler(object sender, EventArgs e)
        {
            if (!locked)
            {
                manualLockOverride = true;
                if (lockLevel == 0)
                {
                    d3dimg.Lock();
                    locked = true;
                }
                lockLevel++;
            }
        }
        #endregion

        private bool mouseSuspended = false;
        private void SuspendEvent()
        {
            if (!mouseSuspended)
            {
                gridMain.MouseMove -= HandleMouseMove;
                gridMain.MouseWheel -= HandleMouseWheel;
                gridMain.MouseDown -= HandleMouseDown;
                gridMain.MouseUp -= HandleMouseUp;
                gridMain.SizeChanged -= HandleSizeChanged;
                mouseSuspended = true;
            }
        }
        private void ResumeEvent()
        {
            if (mouseSuspended)
            {
                gridMain.MouseMove += HandleMouseMove;
                gridMain.MouseWheel += HandleMouseWheel;
                gridMain.MouseDown += HandleMouseDown;
                gridMain.MouseUp += HandleMouseUp;
                gridMain.SizeChanged += HandleSizeChanged;
                mouseSuspended = false;
            }
        }
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
                                if (!manualLockOverride)
                                {
                                    d3dimg.Lock();
                                    locked = true;
                                }
                                d3dimg.SetBackBuffer(D3DResourceType.IDirect3DSurface9, _handle);
                                EngineApi.RenderFrame();
                                d3dimg.AddDirtyRect(new Int32Rect(0, 0, d3dimg.PixelWidth, d3dimg.PixelHeight));
                                if (!manualLockOverride)
                                {
                                    d3dimg.Unlock();
                                    locked = false;
                                }
                                rendering = false;
                                //lastRender = arg.RenderingTime;
                            }
                        }
                    }
                }
            });
        }

        private void HandleSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (drew)
            {
                if (nWidth != prevW || nHeight != prevH)
                {
                    EngineApi.InvokeLock();
                    EngineApi.ScaleFactorInvoke();
                    EngineApi.InvokeUnlock();
                    //_wheelResizeDelay.Stop();
                    //_wheelResizeDelay.Start();
                    //SuspendMouseHandler();
                }
            }
        }
    }
}
