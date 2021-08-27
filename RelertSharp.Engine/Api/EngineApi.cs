using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using RelertSharp.MapStructure;
using RelertSharp.Common;
using System.Threading;

namespace RelertSharp.Engine.Api
{
    public static partial class EngineApi
    {
        public static event EventHandler RedrawRequested;
        public static event EventHandler ResizeRequested;
        public static event EventHandler TheaterReloaded;
        public static event EventHandler LockRequested;
        public static event EventHandler UnlockRequested;
        private static readonly object lockRenderer = new object();

        private static bool initialized = false;
        private static bool renderEnable = true;
        private static bool rendering = false;

        private static Map Map { get { return GlobalVar.GlobalMap; } }


        public static bool EngineCtor(int width, int height)
        {
            EngineMain.EngineCtor(width, height);
            initialized = true;
            return true;
        }
        public static void BindTopWpfCanvas(System.Windows.Controls.Canvas canvas)
        {
            EngineMain.BindCanvas(canvas);
        }
        public static void SuspendRendering()
        {
            renderEnable = false;
        }
        public static void ResumeRendering()
        {
            renderEnable = true;
        }
        public static void RenderFrame()
        {
            if (renderEnable)
            {
                lock (lockRenderer)
                {
                    rendering = true;
                    CppExtern.Scene.PresentAllObjectLock();
                    rendering = false;
                }
            }
        }
        private static bool handlingRedrawRequest = false;
        private static MiniTimer timer = new MiniTimer();
        public static void InvokeRedraw()
        {
            if (!handlingRedrawRequest)
            {
                timer.Start();
                handlingRedrawRequest = true;
                RedrawRequested?.Invoke(null, null);
                handlingRedrawRequest = false;
                timer.Stop();
                long avg = timer.Average;
            }
        }
        public static void InvokeLock()
        {
            LockRequested?.Invoke(null, null);
        }
        public static void InvokeUnlock()
        {
            UnlockRequested?.Invoke(null, null);
        }
        public static IntPtr ResetHandle(Size sz)
        {
            return ResetHandle(sz.Width, sz.Height);
        }
        public static IntPtr ResetHandle(int width, int height)
        {
            if (initialized)
            {
                renderEnable = false;
                EngineMain.Handle = CppExtern.Scene.SetSceneSizeLock(width, height);
                renderEnable = true;
                return EngineMain.Handle;
            }
            return IntPtr.Zero;
        }
        public static void SetBackgroundColor(Color color)
        {
            CppExtern.Scene.SetBackgroundColor(color.R, color.G, color.B);
        }
        public static IntPtr EngineHandle { get { return EngineMain.Handle; } }
        public static double ScaleFactor { get; internal set; } = 1;
        public const int MaxRate = 60;
    }
}
