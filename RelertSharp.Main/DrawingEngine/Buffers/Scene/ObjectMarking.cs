using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.DrawingEngine.Presenting;
using RelertSharp.Common;

namespace RelertSharp.DrawingEngine
{
    internal partial class BufferCollection
    {
        public partial class CScene
        {
            private void Mark<T>(int coord) where T : PresentBase, IPresentBase
            {
                if (Tiles.Keys.Contains(coord))
                {
                    T obj = Tiles[coord].GetFirstTileObject<T>();
                    if (obj != null) obj.MarkSelected();
                }
            }
            private void Mark<T>(int coord, Predicate<T> predicate) where T : PresentBase, IPresentBase
            {
                if (Tiles.Keys.Contains(coord))
                {
                    T obj = Tiles[coord].GetFirstTileObject<T>(predicate);
                    if (obj != null) obj.MarkSelected();
                }
            }
            private void UnMark<T>(int coord, Predicate<T> predicate) where T : PresentBase, IPresentBase
            {
                if (Tiles.Keys.Contains(coord))
                {
                    T obj = Tiles[coord].GetFirstTileObject<T>(predicate);
                    if (obj != null) obj.Unmark();
                }
            }


            public void MarkUnit(int coord)
            {
                Mark<PresentUnit>(coord);
            }
            public void UnMarkUnit(int coord)
            {
                UnMark<PresentUnit>(coord, x => x.Selected);
            }
            public void MarkInfantry(int coord, int subcell)
            {
                Mark<PresentInfantry>(coord, x => x.SubCell == subcell);
            }
            public void UnMarkInfantry(int coord, int subcell)
            {
                UnMark<PresentInfantry>(coord, x => (x.SubCell == subcell) && x.Selected);
            }
            public void MarkBuilding(int coord, bool isNode = false)
            {
                Mark<PresentStructure>(coord, x => x.IsBaseNode == isNode);
            }
            public void UnMarkBuilding(int coord, bool isNode = false)
            {
                UnMark<PresentStructure>(coord, x => (x.IsBaseNode == isNode) && x.Selected);
            }
            public void MarkTerrain(int coord)
            {
                Mark<PresentMisc>(coord, x => x.MiscType == MapObjectType.Terrain);
            }
            public void UnMarkTerrain(int coord)
            {
                UnMark<PresentMisc>(coord, x => (x.MiscType == MapObjectType.Terrain) && x.Selected);
            }
            public void MarkOverlay(int coord)
            {
                Mark<PresentMisc>(coord, x => x.MiscType == MapObjectType.Overlay);
            }
            public void UnMarkOverlay(int coord)
            {
                UnMark<PresentMisc>(coord, x => (x.MiscType == MapObjectType.Overlay) && x.Selected);
            }
            public void MarkSmudge(int coord)
            {
                Mark<PresentMisc>(coord, x => x.MiscType == MapObjectType.Smudge);
            }
            public void UnMarkSmudge(int coord)
            {
                UnMark<PresentMisc>(coord, x => (x.MiscType == MapObjectType.Smudge) && x.Selected);
            }
        }
    }
}
