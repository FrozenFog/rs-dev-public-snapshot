﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using RelertSharp.MapStructure;
using RelertSharp.Common;

namespace RelertSharp.Engine.Api
{
    public static partial class EngineApi
    {
        public static event EventHandler RedrawRequest;
        public static event EventHandler ResizeRequest;
        public static event EventHandler MouseMoveTileMarkRedrawRequest;
        private static readonly object lockRenderer = new object();

        private static bool initialized = false;
        private static bool renderEnable = true;
        private static bool rendering = false;

        private static Map Map { get { return GlobalVar.CurrentMapDocument.Map; } }


        public static bool EngineCtor(int width, int height)
        {
            EngineMain.EngineCtor(width, height);
            initialized = true;
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
        public static void InvokeRedraw()
        {
            RedrawRequest?.Invoke(null, null);
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
    }
}