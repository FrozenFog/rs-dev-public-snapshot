using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.MapEngine.Helper
{
    public enum PanelMouseState
    {
        None = 0,
        ObjectBrush = 1,
        DEBUG = 65535
    }
    internal static class MouseState
    {
        public static PanelMouseState State { get; private set; } = PanelMouseState.None;
        public static void SetState(PanelMouseState state)
        {
            State = state;
        }
    }
}
