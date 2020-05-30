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
                PresentUnit unit = GetObjectByCoord<PresentUnit>(src, x => x.Selected);
                if (unit != null)
                {
                    EraseOriginalPosition(Units, unit);
                    unit.MoveTo(dest);
                    ApplyNewPosition(Units, unit);
                }
            }
            public void ShiftUnitBy(I3dLocateable delta, I2dLocateable src)
            {
                PresentUnit unit = GetObjectByCoord<PresentUnit>(src, x => x.Selected);
                if (unit != null)
                {
                    EraseOriginalPosition(Units, unit);
                    unit.ShiftBy(delta);
                    ApplyNewPosition(Units, unit);
                }
            }
            public void MoveInfantryTo(I3dLocateable dest, I3dLocateable src, int orgSubcell, int newSubcell)
            {
                PresentInfantry inf = GetObjectByCoord<PresentInfantry>(src, x => x.SubCell == orgSubcell && x.Selected);
                if (inf != null)
                {
                    EraseOriginalPosition(Infantries, inf);
                    if (orgSubcell == newSubcell) inf.MoveTo(dest);
                    else inf.SetTo(dest, newSubcell);
                    ApplyNewPosition(Infantries, inf);
                }
            }
            public void ShiftInfantryBy(I3dLocateable delta, I2dLocateable src, int orgSubcell)
            {
                PresentInfantry inf = GetObjectByCoord<PresentInfantry>(src, x => x.SubCell == orgSubcell && x.Selected);
                if (inf != null)
                {
                    EraseOriginalPosition(Infantries, inf);
                    inf.ShiftBy(delta);
                    ApplyNewPosition(Infantries, inf);
                }
            }
            public void MoveBuildingTo(I3dLocateable dest, I3dLocateable src)
            {
                PresentStructure bud = GetObjectByCoord<PresentStructure>(src, x => !x.IsBaseNode && x.Selected);
                if (bud != null)
                {
                    EraseOriginalPosition(Structures, bud);
                    bud.MoveTo(dest);
                    ApplyNewPosition(Structures, bud);
                }
            }
            public void ShiftBuildingBy(I3dLocateable delta, I2dLocateable src)
            {
                PresentStructure bud = GetObjectByCoord<PresentStructure>(src, x => !x.IsBaseNode && x.Selected);
                if (bud != null)
                {
                    EraseOriginalPosition(Structures, bud);
                    bud.ShiftBy(delta);
                    ApplyNewPosition(Structures, bud);
                }
            }
            public void MoveTerrainTo(I3dLocateable dest, I3dLocateable src)
            {
                PresentMisc terr = GetObjectByCoord<PresentMisc>(src, x => x.MiscType == MapObjectType.Terrain || x.Selected);
                if (terr != null)
                {
                    EraseOriginalPosition(Terrains, terr);
                    terr.MoveTo(dest);
                    ApplyNewPosition(Terrains, terr);
                }
            }
            public void ShiftTerrainBy(I3dLocateable delta, I2dLocateable src)
            {
                PresentMisc terr = GetObjectByCoord<PresentMisc>(src, x => x.MiscType == MapObjectType.Terrain || x.Selected);
                if (terr != null)
                {
                    EraseOriginalPosition(Terrains, terr);
                    terr.ShiftBy(delta);
                    ApplyNewPosition(Terrains, terr);
                }
            }
            public void MoveOverlayTo(I3dLocateable dest, I3dLocateable src)
            {
                PresentMisc ov = GetObjectByCoord<PresentMisc>(src, x => x.MiscType == MapObjectType.Overlay || x.Selected);
                if (ov != null)
                {
                    EraseOriginalPosition(Overlays, ov);
                    ov.MoveTo(dest);
                    ApplyNewPosition(Overlays, ov);
                }
            }
            public void ShiftOverlayBy(I3dLocateable delta, I2dLocateable src)
            {
                PresentMisc ov = GetObjectByCoord<PresentMisc>(src, x => x.MiscType == MapObjectType.Overlay || x.Selected);
                if (ov != null)
                {
                    EraseOriginalPosition(Overlays, ov);
                    ov.ShiftBy(delta);
                    ApplyNewPosition(Overlays, ov);
                }
            }
            public void MoveSmudgeTo(I3dLocateable dest, I3dLocateable src)
            {
                PresentMisc smg = GetObjectByCoord<PresentMisc>(src, x => x.MiscType == MapObjectType.Smudge || x.Selected);
                if (smg != null)
                {
                    EraseOriginalPosition(Smudges, smg);
                    smg.MoveTo(dest);
                    ApplyNewPosition(Smudges, smg);
                }
            }
            public void ShiftSmudgeTo(I3dLocateable delta, I2dLocateable src)
            {
                PresentMisc smg = GetObjectByCoord<PresentMisc>(src, x => x.MiscType == MapObjectType.Smudge || x.Selected);
                if (smg != null)
                {
                    EraseOriginalPosition(Smudges, smg);
                    smg.ShiftBy(delta);
                    ApplyNewPosition(Smudges, smg);
                }
            }
        }
    }
}
