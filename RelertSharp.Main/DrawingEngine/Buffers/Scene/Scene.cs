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
    internal partial class BufferCollection
    {
        public class CScene
        {
            #region Ctor - Scene
            public CScene() { }
            #endregion


            #region Private Methods - CScene

            #endregion


            #region Public Methods - CScene


            #region Remove & add Object
            #region Unit
            public void RemoveUnitAt(int coord)
            {
                PresentUnit u = RemoveAtBy(Units, coord);
                if (Tiles.Keys.Contains(coord)) Tiles[coord].TileObjects.Remove(u);
            }
            public void AddUnitAt(int coord, PresentUnit unit)
            {
                Units[coord] = unit;
                if (Tiles.Keys.Contains(coord)) Tiles[coord].TileObjects.Add(unit);
            }
            public void MoveUnitTo(I3dLocateable dest, I3dLocateable src)
            {
                int org = src.Coord;
                PresentUnit unit = Units[org];
                Units.Remove(org);
                Tiles[org].TileObjects.Remove(unit);
                unit.MoveTo(dest);
                Tiles[unit.Coord].TileObjects.Add(unit);
                Units[unit.Coord] = unit;
            }
            #endregion
            #region Infantry
            public void RemoveInfantryAt(int coord, int subcell)
            {
                PresentInfantry inf = RemoveAtBy(Infantries, (coord << 2) + subcell);
                if (Tiles.Keys.Contains(coord)) Tiles[coord].TileObjects.Remove(inf);
            }
            public void AddInfantryAt(int coord, PresentInfantry inf, int subcell)
            {
                Infantries[(coord << 2) + subcell] = inf;
                if (Tiles.Keys.Contains(coord)) Tiles[coord].TileObjects.Add(inf);
            }
            public void MoveInfantryTo(I3dLocateable dest, I3dLocateable src, int orgSubcell, int newSubcell)
            {
                int org = (src.Coord << 2) + orgSubcell;
                PresentInfantry inf = Infantries[org];
                Infantries.Remove(org);
                Tiles[src.Coord].TileObjects.Remove(inf);
                if (orgSubcell == newSubcell) inf.MoveTo(dest);
                else inf.SetTo(dest, newSubcell);
                Tiles[inf.Coord].TileObjects.Add(inf);
                Infantries[(inf.Coord << 2) + newSubcell] = inf;
            }
            #endregion
            #region Building
            public void RemoveBuildingAt(int coord)
            {
                PresentStructure bud = RemoveAtBy(Structures, coord);
                foreach(I2dLocateable pos in new Square2D(bud, bud.FoundationX, bud.FoundationY))
                {
                    int subcord = pos.Coord;
                    if (Tiles.Keys.Contains(subcord)) Tiles[subcord].TileObjects.Remove(bud);
                }
            }
            public void AddBuildingAt(int coord, PresentStructure bud, bool isNode)
            {
                if (isNode)
                {
                    Structures[(coord << 1) + 1] = bud;
                }
                else
                {
                    Structures[coord] = bud;
                    foreach(I2dLocateable pos in new Square2D(bud, bud.FoundationX, bud.FoundationY))
                    {
                        int subcord = pos.Coord;
                        if (Tiles.Keys.Contains(subcord)) Tiles[subcord].TileObjects.Add(bud);
                    }
                }
            }
            public void MoveBuildingTo(I3dLocateable dest, I3dLocateable src)
            {
                int org = src.Coord << 1;
                int dst = dest.Coord << 1;
                PresentStructure bud = Structures[org];
                Structures.Remove(org);
                foreach(I2dLocateable pos in new Square2D(bud, bud.FoundationX, bud.FoundationY))
                {
                    int subcoord = pos.Coord;
                    if (Tiles.Keys.Contains(subcoord)) Tiles[subcoord].TileObjects.Remove(bud);
                }
                bud.MoveTo(dest);
                Structures[dst] = bud;
                foreach(I2dLocateable pos in new Square2D(bud, bud.FoundationX, bud.FoundationY))
                {
                    int subcoord = pos.Coord;
                    if (Tiles.Keys.Contains(subcoord)) Tiles[subcoord].TileObjects.Add(bud);
                }
            }
            #endregion
            #region Overlay
            public void RemoveOverlayAt(int coord)
            {
                PresentMisc ov = RemoveAtBy(Overlays, coord);
                if (Tiles.Keys.Contains(coord)) Tiles[coord].TileObjects.Remove(ov);
            }
            public void AddOverlayAt(int coord, PresentMisc ov)
            {
                Overlays[coord] = ov;
                if (Tiles.Keys.Contains(coord)) Tiles[coord].TileObjects.Add(ov);
            }
            public void MoveOverlayTo(I3dLocateable dest, I3dLocateable src)
            {
                int org = src.Coord;
                int dst = dest.Coord;
                if (!Overlays.Keys.Contains(dst))
                {
                    PresentMisc ov = Overlays[org];
                    Overlays.Remove(org);
                    Tiles[org].TileObjects.Remove(ov);
                    ov.MoveTo(dest);
                    Overlays[dst] = ov;
                    Tiles[dst].TileObjects.Add(ov);
                }
            }
            #endregion
            #region Terrain
            public void RemoveTerrainAt(int coord)
            {
                PresentMisc ter = RemoveAtBy(Terrains, coord);
                if (Tiles.Keys.Contains(coord)) Tiles[coord].TileObjects.Remove(ter);
            }
            public void AddTerrainAt(int coord, PresentMisc terr)
            {
                Terrains[coord] = terr;
                if (Tiles.Keys.Contains(coord)) Tiles[coord].TileObjects.Add(terr);
            }
            public void MoveTerrainTo(I3dLocateable dest, I3dLocateable src)
            {
                int org = src.Coord;
                int dst = dest.Coord;
                PresentMisc terr = Terrains[org];
                Terrains.Remove(org);
                Tiles[org].TileObjects.Remove(terr);
                terr.MoveTo(dest);
                Terrains[dst] = terr;
                Tiles[dst].TileObjects.Add(terr);
            }
            #endregion
            #endregion


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


            #region Mark
            public void MarkUnit(int coord)
            {
                MarkBy(Units, coord);
            }
            public void UnMarkUnit(int coord)
            {
                UnMarkBy(Units, coord);
            }
            public void MarkInfantry(int coord, int subcell)
            {
                MarkBy(Infantries, (coord << 2) + subcell);
            }
            public void UnMarkInfantry(int coord, int subcell)
            {
                UnMarkBy(Infantries, (coord << 2) + subcell);
            }
            public bool HasInfantryAt(int coord)
            {
                return Infantries.Keys.Contains((coord << 2) + 1) ||
                    Infantries.Keys.Contains((coord << 2) + 2) ||
                    Infantries.Keys.Contains((coord << 2) + 3);
            }
            public void MarkBuilding(int coord)
            {
                MarkBy(Structures, coord);
            }
            public void UnMarkBuilding(int coord)
            {
                UnMarkBy(Structures, coord);
            }
            public void MarkTerrain(int coord)
            {
                MarkBy(Terrains, coord);
            }
            public void UnMarkTerrain(int coord)
            {
                UnMarkBy(Terrains, coord);
            }
            public void MarkOverlay(int coord)
            {
                MarkBy(Overlays, coord);
            }
            public void UnMarkOverlay(int coord)
            {
                UnMarkBy(Overlays, coord);
            }
            #endregion


            #region ViewPort
            public void ResetSelectingRectangle()
            {
                foreach (int line in RectangleLines)
                {
                    if (line != 0) CppExtern.ObjectUtils.RemoveObjectFromScene(line);
                }
                RectangleLines.Clear();
            }
            #endregion
            #endregion


            #region Private Methods - Scene
            private void MarkBy<T>(Dictionary<int, T> src, int coord) where T : IPresentBase
            {
                if (src.Keys.Contains(coord))
                {
                    src[coord].MarkSelected();
                }
            }
            private void UnMarkBy<T>(Dictionary<int, T> src, int coord) where T : IPresentBase
            {
                if (src.Keys.Contains(coord))
                {
                    src[coord].Unmark();
                }
            }
            private T RemoveAtBy<T>(Dictionary<int, T> src, int coord) where T : PresentBase, IPresentBase
            {
                if (src.Keys.Contains(coord))
                {
                    T result = src[coord];
                    src[coord].Dispose();
                    src.Remove(coord);
                    return result;
                }
                return null;
            }
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
            public Dictionary<int, DrawableTile> DrawableTiles { get; private set; } = new Dictionary<int, DrawableTile>();
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
            public List<int> RectangleLines { get; private set; } = new List<int>();
            #endregion
        }
    }

}
