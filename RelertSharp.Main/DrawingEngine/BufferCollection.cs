using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.DrawingEngine.Drawables;
using RelertSharp.DrawingEngine.Presenting;

namespace RelertSharp.DrawingEngine
{
    internal class BufferCollection
    {
        [Flags]
        public enum RemoveFlag
        {
            Tiles = 1,
            Structures = 1<<1,
            Units = 1<<2,
            Infantries = 1<<3,
            Overlays = 1<<4,
            Terrains = 1<<5,
            Smudges = 1<<6,
            All = Tiles|Structures|Units|Infantries|Overlays|Terrains|Smudges
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
            if ((flag & RemoveFlag.Tiles) != 0) Scenes.RemoveTiles();
            if ((flag & RemoveFlag.Overlays) != 0) Scenes.RemoveOverlays();
            if ((flag & RemoveFlag.Terrains) != 0) Scenes.RemoveTerrains();
            if ((flag & RemoveFlag.Smudges) != 0) Scenes.RemoveSmudges();
        }
        #endregion


        #region Public Calls - BufferCollection
        public CScene Scenes { get; private set; } = new CScene();
        public CBuffer Buffers { get; private set; } = new CBuffer();
        public CFile Files { get; private set; } = new CFile();
        #endregion


        public class CScene
        {
            #region Ctor - Scene
            public CScene() { }
            #endregion


            #region Private Methods - CScene
            private void DeleteFromScene(int id)
            {
                if (id != 0) CppExtern.ObjectUtils.RemoveObjectAtScene(id);
            }
            #endregion


            #region Public Methods - CScene
            public void RemoveInfantries()
            {
                foreach (PresentInfantry infantry in Infantries.Values) infantry.Dispose();
                Infantries.Clear();
            }
            public void RemoveUnits()
            {
                foreach (PresentUnit unit in Units.Values) unit.Dispose();
                Units.Clear();
            }
            public void RemoveStructures()
            {
                foreach (PresentStructure structure in Structures.Values) structure.Dispose();
                Structures.Clear();
            }
            public void RemoveTiles()
            {
                foreach (PresentTile tile in Tiles.Values) tile.Dispose();
                Tiles.Clear();
            }
            public void RemoveOverlays()
            {
                foreach (int id in Overlays.Values) DeleteFromScene(id);
                Overlays.Clear();
            }
            public void RemoveTerrains()
            {
                foreach (int id in Terrains.Values) DeleteFromScene(id);
                Terrains.Clear();
            }
            public void RemoveSmudges()
            {
                foreach (int id in Smudges.Values) DeleteFromScene(id);
                Smudges.Clear();
            }
            #endregion


            #region Public Calls - Scene
            public Dictionary<int, PresentStructure> Structures { get; private set; } = new Dictionary<int, PresentStructure>();
            public Dictionary<int, PresentUnit> Units { get; private set; } = new Dictionary<int, PresentUnit>();
            public Dictionary<int, PresentInfantry> Infantries { get; private set; } = new Dictionary<int, PresentInfantry>();
            public Dictionary<int, PresentTile> Tiles { get; private set; } = new Dictionary<int, PresentTile>();
            public Dictionary<int, int> Overlays { get; private set; } = new Dictionary<int, int>();
            public Dictionary<int, int> Terrains { get; private set; } = new Dictionary<int, int>();
            public Dictionary<int, int> Smudges { get; private set; } = new Dictionary<int, int>();
            #endregion
        }


        public class CBuffer
        {
            #region Ctor - Buffer
            public CBuffer() { }
            #endregion


            #region Public Calls - CBuffer
            public Dictionary<string, DrawableStructure> Structures { get; private set; } = new Dictionary<string, DrawableStructure>();
            public Dictionary<string, DrawableUnit> Units { get; private set; } = new Dictionary<string, DrawableUnit>();
            public Dictionary<string, DrawableInfantry> Infantries { get; private set; } = new Dictionary<string, DrawableInfantry>();
            #endregion
        }


        public class CFile
        {
            #region Ctor - File
            public CFile() { }
            #endregion


            #region Public Calls - File
            public Dictionary<string, int> Shp { get; private set; } = new Dictionary<string, int>();
            public Dictionary<string, int> Vxl { get; private set; } = new Dictionary<string, int>();
            public Dictionary<string, int> Tmp { get; private set; } = new Dictionary<string, int>();
            public Dictionary<string, int> Pal { get; private set; } = new Dictionary<string, int>();
            #endregion
        }
    }
}
