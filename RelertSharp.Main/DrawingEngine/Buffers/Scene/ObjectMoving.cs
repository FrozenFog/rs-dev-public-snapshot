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
            private void EraseOriginalPosition<T>(Dictionary<int, T> refer, T obj) where T : PresentBase, IPresentBase
            {
                refer.Remove(obj.ID);
                if (obj.GetType() == typeof(PresentStructure))
                {
                    PresentStructure bud = obj as PresentStructure;
                    foreach(I2dLocateable pos in new Square2D(bud, bud.FoundationX, bud.FoundationY))
                    {
                        int sub = pos.Coord;
                        if (Tiles.Keys.Contains(sub)) Tiles[sub].TileObjects.Remove(bud);
                    }
                }
                else if (obj.GetType() == typeof(PresentMisc) && ((obj as PresentMisc).MiscType == MapObjectType.Smudge))
                {
                    PresentMisc smg = obj as PresentMisc;
                    foreach(I2dLocateable pos in new Square2D(smg, smg.SmgWidth, smg.SmgHeight))
                    {
                        int sub = pos.Coord;
                        if (Tiles.Keys.Contains(sub)) Tiles[sub].TileObjects.Remove(smg);
                    }
                }
                else
                {
                    if (Tiles.Keys.Contains(obj.Coord)) Tiles[obj.Coord].TileObjects.Remove(obj);
                }
            }
            private void ApplyNewPosition<T>(Dictionary<int, T> refer, T obj) where T : PresentBase, IPresentBase
            {
                refer[obj.ID] = obj;
                if (obj.GetType() == typeof(PresentStructure))
                {
                    PresentStructure bud = obj as PresentStructure;
                    foreach(I2dLocateable pos in new Square2D(bud, bud.FoundationX, bud.FoundationY))
                    {
                        int sub = pos.Coord;
                        if (Tiles.Keys.Contains(sub)) Tiles[sub].TileObjects.Add(bud);
                    }
                }
                else if (obj.GetType() == typeof(PresentMisc) && ((obj as PresentMisc).MiscType == MapObjectType.Smudge))
                {
                    PresentMisc smg = obj as PresentMisc;
                    foreach (I2dLocateable pos in new Square2D(smg, smg.SmgWidth, smg.SmgHeight))
                    {
                        int sub = pos.Coord;
                        if (Tiles.Keys.Contains(sub)) Tiles[sub].TileObjects.Add(smg);
                    }
                }
                else
                {
                    if (Tiles.Keys.Contains(obj.Coord)) Tiles[obj.Coord].TileObjects.Add(obj);
                }
            }


            public void MoveUnitTo(I3dLocateable dest, I3dLocateable src)
            {
                PresentUnit unit = GetObjectByCoord<PresentUnit>(src);
                if (unit != null)
                {
                    EraseOriginalPosition(Units, unit);
                    unit.MoveTo(dest);
                    ApplyNewPosition(Units, unit);
                }
            }
            public void MoveInfantryTo(I3dLocateable dest, I3dLocateable src, int orgSubcell, int newSubcell)
            {
                PresentInfantry inf = GetObjectByCoord<PresentInfantry>(src, x => x.SubCell == orgSubcell);
                if (inf != null)
                {
                    EraseOriginalPosition(Infantries, inf);
                    if (orgSubcell == newSubcell) inf.MoveTo(dest);
                    else inf.SetTo(dest, newSubcell);
                    ApplyNewPosition(Infantries, inf);
                }
            }
            public void MoveBuildingTo(I3dLocateable dest, I3dLocateable src)
            {
                PresentStructure bud = GetObjectByCoord<PresentStructure>(src, x => !x.IsBaseNode);
                if (bud != null)
                {
                    EraseOriginalPosition(Structures, bud);
                    bud.MoveTo(dest);
                    ApplyNewPosition(Structures, bud);
                }
            }
            public void MoveTerrainTo(I3dLocateable dest, I3dLocateable src)
            {
                PresentMisc terr = GetObjectByCoord<PresentMisc>(src, x => x.MiscType == MapObjectType.Terrain);
                if (terr != null)
                {
                    EraseOriginalPosition(Terrains, terr);
                    terr.MoveTo(dest);
                    ApplyNewPosition(Terrains, terr);
                }
            }
            public void MoveOverlayTo(I3dLocateable dest, I3dLocateable src)
            {
                PresentMisc ov = GetObjectByCoord<PresentMisc>(src, x => x.MiscType == MapObjectType.Overlay);
                if (ov != null)
                {
                    EraseOriginalPosition(Overlays, ov);
                    ov.MoveTo(dest);
                    ApplyNewPosition(Overlays, ov);
                }
            }
        }
    }
}
