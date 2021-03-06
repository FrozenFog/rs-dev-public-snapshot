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
        TilePhasing = 1 << 12,
        InteliWallBrush = 1 << 13,
        InteliCliffBrush = 1 << 14,
        ObjectRandomBrush = 1 << 15,
        ObjectArrayBrush = 1 << 16,
        WallBreakdownBrush = 1 << 17,
        TiberiumBrush = 1 << 18,
        WaypointPicker = 1 << 19,
        TileBrush = TileSingleBrush | TileBucketFlood | InteliRampBrush | InteliCliffBrush,
        PaintBrush = InteliWallBrush | ObjectBrush | ObjectRandomBrush | ObjectArrayBrush | TiberiumBrush,
        DEBUG = 1 << 31
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
