using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.DrawingEngine.Presenting;

namespace RelertSharp.DrawingEngine
{
    internal partial class BufferCollection
    {
        public partial class CScene
        {
            public void RemoveUnitAt(int coord)
            {
                if (Tiles.Keys.Contains(coord))
                {
                    PresentUnit u = Tiles[coord].GetFirstTileObject<PresentUnit>();
                    if (u != null)
                    {
                        Tiles[coord].TileObjects.Remove(u);
                        Units.Remove(u.ID);
                        u.Dispose();
                    }
                }
            }
            public void RemoveInfantryAt(int coord, int subcell)
            {
                if (Tiles.Keys.Contains(coord))
                {
                    PresentInfantry inf = Tiles[coord].GetFirstTileObject<PresentInfantry>(x=>x.SubCell == subcell);
                    if (inf != null)
                    {
                        Tiles[coord].TileObjects.Remove(inf);
                        Infantries.Remove(inf.ID);
                        inf.Dispose();
                    }
                }
            }
            public void RemoveBuildingAt(int coord, bool isBaseNode = false)
            {
                if (Tiles.Keys.Contains(coord))
                {
                    PresentStructure bud = Tiles[coord].GetFirstTileObject<PresentStructure>(x=>x.IsBaseNode == isBaseNode);
                    if (bud != null)
                    {
                        foreach (I2dLocateable pos in new Square2D(bud, bud.FoundationX, bud.FoundationY))
                        {
                            int sub = pos.Coord;
                            if (Tiles.Keys.Contains(sub)) Tiles[pos.Coord].TileObjects.Remove(bud);
                        }
                        Structures.Remove(bud.ID);
                        bud.Dispose();
                    }
                }
            }
            public void RemoveOverlayAt(int coord)
            {
                if (Tiles.Keys.Contains(coord))
                {
                    PresentMisc ov = Tiles[coord].GetFirstTileObject<PresentMisc>(x=>x.MiscType == MapObjectType.Overlay);
                    if (ov != null)
                    {
                        Tiles[coord].TileObjects.Remove(ov);
                        Overlays.Remove(ov.ID);
                        ov.Dispose();
                    }
                }
            }
            public void RemoveTerrainAt(int coord)
            {
                if (Tiles.Keys.Contains(coord))
                {
                    PresentMisc terr = Tiles[coord].GetFirstTileObject<PresentMisc>(x => x.MiscType == MapObjectType.Terrain);
                    if (terr != null)
                    {
                        Tiles[coord].TileObjects.Remove(terr);
                        Terrains.Remove(terr.ID);
                        terr.Dispose();
                    }
                }
            }
            public void RemoveSmudges(int coord)
            {
                if (Tiles.Keys.Contains(coord))
                {
                    PresentMisc smg = Tiles[coord].GetFirstTileObject<PresentMisc>(x => x.MiscType == MapObjectType.Smudge);
                    if (smg != null)
                    {
                        foreach (I2dLocateable pos in new Square2D(smg, smg.SmgWidth, smg.SmgHeight))
                        {
                            int sub = pos.Coord;
                            if (Tiles.Keys.Contains(coord)) Tiles[coord].TileObjects.Remove(smg);
                        }
                        Smudges.Remove(smg.ID);
                        smg.Dispose();
                    }
                }
            }
        }
    }
}
