﻿using System;
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
        TileBucketFlood = 1 << 5,
        ObjectPasteBrush = 1 << 6,
        TileFlatting = 1 << 7,
        TileSingleRising = 1 << 8,
        TileSingleSinking = 1 << 9,
        TileBrush = TileSingleBrush | TileBucketFlood,
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
    }
}
