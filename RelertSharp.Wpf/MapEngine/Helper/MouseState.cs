using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.MapEngine.Helper
{
    [Flags]
    public enum PanelMouseState
    {
        None = 0,
        ObjectBrush = 1,
        TileSingleBrush = 1 << 1,
        TileLineSelecting = 1 << 2,
        TileBucketSelecting = 1 << 3,
        TileSingleSelecting = 1 << 4,
        TileSelecting = TileLineSelecting | TileBucketSelecting | TileSingleSelecting,
        TileBucketFill = 1 << 5,
        TileBrush = TileSingleBrush | TileBucketFill,
        DEBUG = 65535
    }
    internal static class MouseState
    {
        public static PanelMouseState State { get; private set; } = PanelMouseState.None;
        public static void SetState(PanelMouseState state)
        {
            if (State != state)
            {
                if (State == PanelMouseState.TileSingleBrush) TilePaintBrush.SuspendBrush();
                else if (State == PanelMouseState.ObjectBrush) PaintBrush.SuspendBrush();
                State = state;
            }
        }
        public static bool IsState(PanelMouseState state)
        {
            return (State & state) != 0;
        }
    }
}
