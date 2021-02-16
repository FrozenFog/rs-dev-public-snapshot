using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RelertSharp.Engine.Api
{
    public static partial class EngineApi
    {
        private static bool initialized = false;
        private static bool refreshing = false;
        private static bool renderEnable = true;
        public static bool EngineCtor(int width, int height)
        {
            EngineMain.EngineCtor(width, height);


            return true;
        }
        public static void SuspendRendering()
        {
            renderEnable = false;
        }
        public static void ResumeRendering()
        {
            renderEnable = true;
        }
        public static void RefreshFrame()
        {
            if (renderEnable && !refreshing)
            {
                refreshing = true;
                CppExtern.Scene.PresentAllObject();
                refreshing = false;
            }
        }
        public static IntPtr ResetHandle(Size sz)
        {
            EngineMain.Handle = CppExtern.Scene.SetSceneSize(sz.Width, sz.Height);
            return EngineMain.Handle;
        }
        public static IntPtr ResetHandle(int width, int height)
        {
            EngineMain.Handle = CppExtern.Scene.SetSceneSize(width, height);
            return EngineMain.Handle;
        }
        public static IntPtr EngineHandle { get { return EngineMain.Handle; } }
    }
}
