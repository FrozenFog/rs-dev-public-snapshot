using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.DrawingEngine.Drawables;
using RelertSharp.DrawingEngine.Presenting;
using RelertSharp.Common;

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
            public void ColoringTile(int coord, Vec4 color, Vec4 exColor)
            {
                if (Tiles.Keys.Contains(coord))
                {
                    PresentTile t = Tiles[coord];
                    CppExtern.ObjectUtils.SetObjectColorCoefficient(t.pSelf, color);
                    if (t.pExtra != 0) CppExtern.ObjectUtils.SetObjectColorCoefficient(t.pExtra, exColor);
                }
            }
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
                foreach (PresentMisc o in Overlays.Values) o.Dispose();
                Overlays.Clear();
            }
            public void RemoveTerrains()
            {
                foreach (PresentMisc terr in Terrains.Values) terr.Dispose();
                Terrains.Clear();
            }
            public void RemoveSmudges()
            {
                foreach (PresentMisc smg in Smudges.Values) smg.Dispose();
                Smudges.Clear();
            }
            #endregion


            #region Public Calls - Scene
            public Dictionary<int, PresentStructure> Structures { get; private set; } = new Dictionary<int, PresentStructure>();
            public Dictionary<int, PresentUnit> Units { get; private set; } = new Dictionary<int, PresentUnit>();
            public Dictionary<int, PresentInfantry> Infantries { get; private set; } = new Dictionary<int, PresentInfantry>();
            public Dictionary<int, PresentTile> Tiles { get; private set; } = new Dictionary<int, PresentTile>();
            public Dictionary<int, PresentMisc> Overlays { get; private set; } = new Dictionary<int, PresentMisc>();
            public Dictionary<int, PresentMisc> Terrains { get; private set; } = new Dictionary<int, PresentMisc>();
            public Dictionary<int, PresentMisc> Smudges { get; private set; } = new Dictionary<int, PresentMisc>();
            public Dictionary<int, PresentMisc> Celltags { get; private set; } = new Dictionary<int, PresentMisc>();
            public Dictionary<int, PresentMisc> Waypoints { get; private set; } = new Dictionary<int, PresentMisc>();
            public IEnumerable<IPresentBase> MapObjects { get { return Structures.Values.Concat<IPresentBase>(Units.Values).Concat(Infantries.Values); } }
            public IEnumerable<PresentMisc> MapMiscs { get { return Overlays.Values.Concat(Terrains.Values).Concat(Smudges.Values); } }
            public IEnumerable<PresentMisc> LogicObjects { get { return Celltags.Values.Concat(Waypoints.Values); } }
            #endregion
        }


        public class CBuffer
        {
            #region Ctor - Buffer
            public CBuffer() { }
            #endregion


            #region Public Calls - CBuffer
            public int Celltag { get; set; }
            public int WaypointBase { get; set; }
            public Dictionary<string, DrawableStructure> Structures { get; private set; } = new Dictionary<string, DrawableStructure>();
            public Dictionary<string, DrawableUnit> Units { get; private set; } = new Dictionary<string, DrawableUnit>();
            public Dictionary<string, DrawableInfantry> Infantries { get; private set; } = new Dictionary<string, DrawableInfantry>();
            public Dictionary<string, DrawableMisc> Miscs { get; private set; } = new Dictionary<string, DrawableMisc>();
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
            public int WaypointBase { get; set; }
            public int CelltagBase { get; set; }
            #endregion
        }
    }
}
