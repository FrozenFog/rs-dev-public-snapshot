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
        TileSingleSelecting = 1 << 2,
        TileLineSelecting = 1 << 3,
        TileBucketSelecting = 1 << 4,
        TileBoxSelecting = 1 << 5,
        TileSelecting = TileLineSelecting | TileBucketSelecting | TileBoxSelecting | TileSingleSelecting,
        TileBucketFlood = 1 << 6,
        ObjectPasteBrush = 1 << 7,
        TileFlatting = 1 << 8,
        TileSingleRising = 1 << 9,
        TileSingleSinking = 1 << 10,
        InteliRampBrush = 1 << 11,
        TileBrush = TileSingleBrush | TileBucketFlood | InteliRampBrush,
        DEBUG = 65535
    }
    internal static class MouseState
    {
        public static event Action MouseStateChanged;
        public static PanelMouseState State { get; private set; } = PanelMouseState.None;
        public static PanelMouseState PrevState { get; private set; } = PanelMouseState.None;
        public static void SetState(PanelMouseState state)
        {
            if (State != state)
            {
                State = state;
                MouseStateChanged?.Invoke();
                PrevState = state;
            }
        }
        public static bool IsState(PanelMouseState state)
        {
            return (State & state) != 0;
        }
        public static bool PrevIsState(PanelMouseState state)
        {
            return (PrevState & state) != 0;
        }
    }
}
