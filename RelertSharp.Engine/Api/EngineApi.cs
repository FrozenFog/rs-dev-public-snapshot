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
        public static bool EngineCtor(IntPtr hwndMain)
        {
            EngineMain.EngineCtor(hwndMain);


            return true;
        }
        public static void RefreshFrame()
        {
            if (!refreshing)
            {
                refreshing = true;
                CppExtern.Scene.PresentAllObject();
                refreshing = false;
            }
        }
    }
}
