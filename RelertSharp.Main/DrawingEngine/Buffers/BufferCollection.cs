using System;
using System.Collections.Generic;

namespace RelertSharp.DrawingEngine
{
    internal partial class BufferCollection
    {
        [Flags]
        public enum RemoveFlag
        {
            Tiles = 1,
            Structures = 1 << 1,
            Units = 1 << 2,
            Infantries = 1 << 3,
            Overlays = 1 << 4,
            Terrains = 1 << 5,
            Smudges = 1 << 6,
            Waypoints = 1 << 7,
            Celltags = 1 << 8,
            All = Tiles | Structures | Units | Infantries | Overlays | Terrains | Smudges | Waypoints | Celltags
        }
        #region Ctor - BufferCollection
        public BufferCollection()
        {

        }
        #endregion


        #region Public Methods - BufferCollection
        public void RemoveSceneItem(RemoveFlag flag)
        {
            if ((flag & RemoveFlag.Infantries) != 0) Scenes.RemoveInfantries();
            if ((flag & RemoveFlag.Units) != 0) Scenes.RemoveUnits();
            if ((flag & RemoveFlag.Structures) != 0) Scenes.RemoveStructures();
            if ((flag & RemoveFlag.Overlays) != 0) Scenes.RemoveOverlays();
            if ((flag & RemoveFlag.Terrains) != 0) Scenes.RemoveTerrains();
            if ((flag & RemoveFlag.Smudges) != 0) Scenes.RemoveSmudges();
            if ((flag & RemoveFlag.Waypoints) != 0) Scenes.RemoveWaypoints();
            if ((flag & RemoveFlag.Celltags) != 0) Scenes.RemoveCelltags();
            GC.Collect();
        }
        public void ReleaseFiles()
        {
            Files.Dispose();
        }
        #endregion


        #region Public Calls - BufferCollection
        public CScene Scenes { get; private set; } = new CScene();
        public CBuffer Buffers { get; private set; } = new CBuffer();
        public CFile Files { get; private set; } = new CFile();
        public List<int> WaypointNum { get; private set; } = new List<int>();
        #endregion

    }
}
