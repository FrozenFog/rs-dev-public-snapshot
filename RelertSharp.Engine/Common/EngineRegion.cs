using System;
using RelertSharp.Engine.Api;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Engine
{
    public class EngineRegion : IDisposable
    {
        private bool NeedRedraw;
        public EngineRegion(bool redraw = false)
        {
            EngineApi.InvokeLock();
            NeedRedraw = redraw;
        }


        public void Dispose()
        {
            if (NeedRedraw) EngineApi.InvokeRedraw();
            EngineApi.InvokeUnlock();
        }
    }
}
