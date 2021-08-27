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
using RelertSharp.Algorithm;
using RelertSharp.Wpf.Dialogs;

namespace RelertSharp.Wpf.MapEngine
{
    /// <summary>
    /// MainPanel.xaml 的交互逻辑
    /// </summary>
    public partial class MainPanel : UserControl, IRsView
    {
        public event ScenePositionHandler MousePosChanged;
        public event EventHandler ScaleFactorChanged;
        private IntPtr _handle;
        private bool drew = false;
        private bool rendering = false;
        private int prevW, prevH;
        private int nWidth { get { return this.ScaledWidth(); } }
        private int nHeight { get { return this.ScaledHeight(); } }
        private double dpi;
        private D3dImg d3dimg = new D3dImg();

        public GuiViewType ViewType { get { return GuiViewType.MainPanel; } }
        public AvalonDock.Layout.LayoutAnchorable ParentAncorable { get; set; }
        public AvalonDock.Layout.LayoutDocument ParentDocument { get; set; }

        private static readonly object lockRender = new object();
        private static readonly object lockMouse = new object();

        //private RenderTargetBitmap img;




        public MainPanel()
        {
            InitializeComponent();
            imgelt.Source = d3dimg;
            keyClickAction = new DelayedAction(null, KeyHoldToClick, CLICK_INTERVAL);
            GlobalVar.MapDocumentRedrawRequested += MapRedrawHandler;
            GlobalVar.MapDocumentLoaded += MapReloadedHandler;
            EngineApi.RedrawRequested += RedrawInvokeHandler;
            EngineApi.ResizeRequested += ResizeInvokeHandler;
            EngineApi.LockRequested += LockInvokeHandler;
            EngineApi.UnlockRequested += UnlockInvokeHandler;
            EngineApi.MapDrawingBegin += BeginDrawingHandler;
            NavigationHub.GoToPositionRequest += MoveCameraInvokeHandler;
        }

        private void BeginDrawingHandler(object sender, EventArgs e)
        {
            DlgLoading dlg = new DlgLoading();
            dlg.Show();
        }

        private void MapReloadedHandler()
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
        public bool SaveMapScreenshotAs(string path)
        {
            bool result = false;
            if (drew)
            {
                lock (lockRender)
                {
                    const double wWindow = 1600;
                    const double hWindow = 900;
                    // suspend responding
                    SuspendEvent();
                    EngineApi.SuspendRendering();
                    var map = GlobalVar.GlobalMap;

                    // save previous status
                    double prevScale = EngineApi.ScaleFactor;
                    I3dLocateable prevPos = new Pnt3(EngineApi.CameraPosition);

                    // total size
                    var destMapSize = map.Info.Size;
                    int totalPicWidth = (int)((destMapSize.Width + destMapSize.X) * 60);
                    int totalPicHeight = (int)((destMapSize.Height + destMapSize.Y) * 30);

                    // split into several section
                    List<I3dLocateable> cameraPositions = new List<I3dLocateable>();
                    List<BitmapSource> sections = new List<BitmapSource>();
                    int countWidth = (int)Math.Ceiling(totalPicWidth / wWindow);
                    int countHeight = (int)Math.Ceiling(totalPicHeight / hWindow);
                    int wCell = destMapSize.Width / countWidth;
                    int hCell = destMapSize.Height / countHeight;
                    int pxOffsetY = 0;
                    for (int y = 0; y < countHeight + 1; y++)
                    {
                        int pxOffsetX = 0;
                        for (int x = 0; x < countWidth + 1; x++)
                        {
                            MapPosition.MinimapPxPosToCell(pxOffsetX * 2, pxOffsetY * 2, destMapSize.Width, out I2dLocateable cell);
                            cameraPositions.Add(new Pnt3(cell, 0));
                            pxOffsetX += wCell;
                        }
                        pxOffsetY += hCell;
                    }


                    // begin
                    // set size and scale
                    EngineApi.InvokeLock();
                    EngineApi.ChangeScaleFactor(1);
                    IntPtr drawHandle = EngineApi.ResetHandle((int)wWindow, (int)hWindow);

                    // draw sections
                    foreach (I3dLocateable cell in cameraPositions)
                    {
                        EngineApi.MoveCameraTo(cell);
                        d3dimg.SetBackBuffer(D3DResourceType.IDirect3DSurface9, drawHandle);
                        EngineApi.RenderFrame();
                        var buffer = d3dimg.DrawBackBuffer(); int redrawCount = 0;
                        while (buffer == null && redrawCount++ < 10)
                        {
                            drawHandle = EngineApi.ResetHandle((int)wWindow, (int)hWindow);
                            d3dimg.SetBackBuffer(D3DResourceType.IDirect3DSurface9, drawHandle);
                            EngineApi.RenderFrame();
                            buffer = d3dimg.DrawBackBuffer();
                        }
                        if (buffer == null)
                        {
                            result = false;
                            goto end;
                        }
                        BitmapSource src = buffer.Clone();
                        CroppedBitmap crp = new CroppedBitmap(src, new Int32Rect(1, 1, src.PixelWidth - 2, src.PixelHeight - 2));
                        sections.Add(crp);
                    }

                    // combine sections
                    DrawingGroup scene = new DrawingGroup();
                    double yOffset = 0;
                    for (int y = 0, i = 0; y < countHeight + 1; y++)
                    {
                        double xOffset = 0;
                        for (int x = 0; x < countWidth + 1; x++)
                        {
                            ImageDrawing section = new ImageDrawing();
                            section.ImageSource = sections[i++];
                            section.Rect = new Rect(xOffset - 1, yOffset - 1, wWindow, hWindow);
                            scene.Children.Add(section);
                            xOffset += wCell * 60;
                        }
                        yOffset += hCell * 30;
                    }
                    var drawingImage = new Image { Source = new DrawingImage(scene) };
                    drawingImage.Arrange(new Rect(0, 0, totalPicWidth, totalPicHeight));
                    RenderTargetBitmap bmp = new RenderTargetBitmap(totalPicWidth, totalPicHeight, 96, 96, PixelFormats.Pbgra32);
                    bmp.Render(drawingImage);
                    using (var fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                    {
                        JpegBitmapEncoder encoder = new JpegBitmapEncoder() { QualityLevel = 90 };
                        encoder.Frames.Add(BitmapFrame.Create(bmp));
                        encoder.Save(fs);
                    }

                    result = true;

                end:
                    // revert
                    _handle = EngineApi.ResetHandle(nWidth, nHeight);
                    EngineApi.SetScaleFactor(prevScale);
                    EngineApi.MoveCameraTo(prevPos);
                    // resume
                    EngineApi.ResumeRendering();
                    d3dimg.SetBackBuffer(D3DResourceType.IDirect3DSurface9, _handle);
                    EngineApi.RenderFrame();
                    d3dimg.AddDirtyRect(new Int32Rect(0, 0, d3dimg.PixelWidth, d3dimg.PixelHeight));
                    EngineApi.InvokeUnlock();
                    ResumeEvent();

                    //EngineApi.InvokeLock();
                    //EngineApi.ChangeScaleFactor(1);
                    //EngineApi.MoveCameraTo(destCenter, 0);
                    //IntPtr drawHandle = EngineApi.ResetHandle(width, height);
                    //d3dimg.SetBackBuffer(D3DResourceType.IDirect3DSurface9, drawHandle);
                    //EngineApi.RenderFrame();
                    //BitmapSource bmp = d3dimg.DrawBackBuffer();
                    //using (var fs = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                    //{
                    //    JpegBitmapEncoder encoder = new JpegBitmapEncoder() { QualityLevel = 90 };
                    //    encoder.Frames.Add(BitmapFrame.Create(bmp));
                    //    encoder.Save(fs);
                    //}

                    //// revert original status
                    //_handle = EngineApi.ResetHandle(nWidth, nHeight);
                    //EngineApi.SetScaleFactor(prevScale);
                    //EngineApi.MoveCameraTo(prevPos);
                    //EngineApi.ResumeRendering();
                    //d3dimg.SetBackBuffer(D3DResourceType.IDirect3DSurface9, _handle);
                    //EngineApi.RenderFrame();
                    //d3dimg.AddDirtyRect(new Int32Rect(0, 0, d3dimg.PixelWidth, d3dimg.PixelHeight));
                    //EngineApi.InvokeUnlock();
                    //ResumeEvent();
                }
            }
            return result;
        }
        public async void DrawMap()
        {
            EngineApi.SuspendRendering();
            await EngineApi.DrawMap(GlobalVar.GlobalMap);
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
                EngineApi.BindTopWpfCanvas(cnvMain);
                _handle = EngineApi.ResetHandle(nWidth, nHeight);
                d3dimg.IsFrontBufferAvailableChanged += FrontBufferChanged;
                initialized = true;
            }
        }

        #region PERMANENTLY SOLVED ENGINE DEAD
        private bool manualLockOverride = false;
        private int lockLevel = 0;
        private void UnlockInvokeHandler(object sender, EventArgs e)
        {
            lockLevel--;
            if (lockLevel == 0)
            {
                manualLockOverride = false;
                d3dimg.Unlock();
            }
        }

        private void LockInvokeHandler(object sender, EventArgs e)
        {
            if (lockLevel == 0)
            {
                manualLockOverride = true;
                d3dimg.Lock();
            }
            lockLevel++;
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
                                }
                                d3dimg.SetBackBuffer(D3DResourceType.IDirect3DSurface9, _handle);
                                EngineApi.RenderFrame();
                                d3dimg.AddDirtyRect(new Int32Rect(0, 0, d3dimg.PixelWidth, d3dimg.PixelHeight));
                                if (!manualLockOverride)
                                {
                                    d3dimg.Unlock();
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

namespace System.Windows.Interop
{
    public class D3dImg : D3DImage
    {
        public D3dImg() : base() { }

        public BitmapSource DrawBackBuffer()
        {
            return base.CopyBackBuffer();
        }
    }
}
