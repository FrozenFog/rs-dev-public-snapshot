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
            Waypoints = 1<<7,
            Celltags = 1<<8,
            All = Tiles|Structures|Units|Infantries|Overlays|Terrains|Smudges|Waypoints|Celltags
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
        #endregion


        public class CScene
        {
            #region Ctor - Scene
            public CScene() { }
            #endregion


            #region Private Methods - CScene

            #endregion


            #region Public Methods - CScene


            #region Lightnings
            public void BeginLamp()
            {
                foreach (PresentTile t in Tiles.Values) t.Lamped = false;
            }
            public void ColoringTile(I2dLocateable pos, Vec4 color)
            {
                int coord = Utils.Misc.CoordInt(pos);
                if (Tiles.Keys.Contains(coord))
                {
                    if (!Tiles[coord].Lamped)
                    {
                        Tiles[coord].MultiplyColor(color);
                        Tiles[coord].Lamped = true;
                    }
                }
            }
            #endregion


            public void MarkTile(int coord, Vec4 color, Vec4 exColor, bool deselect)
            {
                if (Tiles.Keys.Contains(coord))
                {
                    Tiles[coord].Mark(color, exColor, deselect);
                }
            }


            #region Remove all
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
            public void RemoveCelltags()
            {
                foreach (PresentMisc ctg in Celltags.Values) ctg.Dispose();
                Celltags.Clear();
            }
            public void RemoveWaypoints()
            {
                foreach (PresentMisc wp in Waypoints.Values) wp.Dispose();
                Waypoints.Clear();
            }
            #endregion


            #region Mark And Single-remove
            public void MarkUnit(int coord)
            {
                Units[coord].MarkSelected();
            }
            public void UnMarkUnit(int coord)
            {
                Units[coord].Unmark();
            }
            public void RemoveUnitAt(int coord)
            {
                Units[coord].Dispose();
                Units.Remove(coord);
            }
            public void MarkInfantry(int coord, int subcell)
            {
                Infantries[coord << 2 + subcell].MarkSelected();
            }
            public void UnMarkInfantry(int coord, int subcell)
            {
                Infantries[coord << 2 + subcell].Unmark();
            }
            public void RemoveInfantryAt(int coord, int subcell)
            {
                int c = coord << 2 + subcell;
                Infantries[c].Dispose();
                Infantries.Remove(c);
            }
            public void MarkBuilding(int coord)
            {
                Structures[coord].MarkSelected();
            }
            public void UnMarkBuilding(int coord)
            {
                Structures[coord].Unmark();
            }
            public void RemoveBuildingAt(int coord)
            {
                Structures[coord].Dispose();
                Structures.Remove(coord);
            }
            #endregion


            #endregion


            #region Public Calls - Scene
            /// <summary>
            /// index : st.Coord
            /// st.Coord lsh 1 + 1 if it is BaseNode
            /// </summary>
            public Dictionary<int, PresentStructure> Structures { get; private set; } = new Dictionary<int, PresentStructure>();
            public Dictionary<int, PresentUnit> Units { get; private set; } = new Dictionary<int, PresentUnit>();
            /// <summary>
            /// index : inf.Coord lsh 2 + subcell
            /// </summary>
            public Dictionary<int, PresentInfantry> Infantries { get; private set; } = new Dictionary<int, PresentInfantry>();
            public Dictionary<int, PresentTile> Tiles { get; private set; } = new Dictionary<int, PresentTile>();
            public Dictionary<int, PresentMisc> Overlays { get; private set; } = new Dictionary<int, PresentMisc>();
            public Dictionary<int, PresentMisc> Terrains { get; private set; } = new Dictionary<int, PresentMisc>();
            public Dictionary<int, PresentMisc> Smudges { get; private set; } = new Dictionary<int, PresentMisc>();
            public Dictionary<int, PresentMisc> Celltags { get; private set; } = new Dictionary<int, PresentMisc>();
            public Dictionary<int, PresentMisc> Waypoints { get; private set; } = new Dictionary<int, PresentMisc>();
            public IEnumerable<IPresentBase> MapObjects { get { return Structures.Values.Concat<IPresentBase>(Units.Values).Concat(Infantries.Values); } }
            public IEnumerable<PresentMisc> MapMiscs { get { return Overlays.Values.Concat(Terrains.Values).Concat(Smudges.Values); } }
            public IEnumerable<PresentMisc> MiscWithoutSmudges { get { return Overlays.Values.Concat(Terrains.Values); } }
            public IEnumerable<IPresentBase> OneCellObjects { get { return Units.Values.Concat<IPresentBase>(Infantries.Values).Concat(Overlays.Values).Concat(Terrains.Values); } }
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
            public Dictionary<string, DrawableTile> Tiles { get; private set; } = new Dictionary<string, DrawableTile>();
            #endregion
        }


        public class CFile
        {
            #region Ctor - File
            public CFile() { }
            #endregion


            #region Public Methods - File
            public void Dispose()
            {
                foreach (int p in Shp.Values) CppExtern.Files.RemoveShpFile(p);
                foreach (int p in Vxl.Values) CppExtern.Files.RemoveVxlFile(p);
                foreach (int p in Tmp.Values) CppExtern.Files.RemoveTmpFile(p);
                foreach (int p in Pal.Values) CppExtern.Files.RemovePalette(p);
                Shp.Clear();
                Vxl.Clear();
                Tmp.Clear();
                Pal.Clear();
                GC.Collect();
            }
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
