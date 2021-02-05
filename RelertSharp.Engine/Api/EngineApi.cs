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
        public static bool BindMainHwnd(IntPtr handle)
        {
            if (!CppExtern.Scene.SetUpScene(handle)) return false;
            if (!CppExtern.Scene.ResetSceneView()) return false;
            return true;
        }
        public static bool InitializeMiniMap(Size sz)
        {

        }
    }
}
