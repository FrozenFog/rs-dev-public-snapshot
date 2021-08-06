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
        TileBoxSelecting = 1 << 4,
        TileSelecting = TileLineSelecting | TileBucketSelecting | TileBoxSelecting,
        TileBucketFill = 1 << 5,
        ObjectPasteBrush = 1 << 6,
        TileBrush = TileSingleBrush | TileBucketFill,
        DEBUG = 65535
    }
    internal static class MouseState
    {
        public static event Action MouseStateChanged;
        public static PanelMouseState State { get; private set; } = PanelMouseState.None;
        public static void SetState(PanelMouseState state)
        {
            if (State != state)
            {
                if (State == PanelMouseState.TileSingleBrush) TilePaintBrush.SuspendBrush();
                else if (State == PanelMouseState.ObjectBrush) PaintBrush.SuspendBrush();
                State = state;
                MouseStateChanged?.Invoke();
            }
        }
        public static bool IsState(PanelMouseState state)
        {
            return (State & state) != 0;
        }
    }
}
