using RelertSharp.MapStructure;
using RelertSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common.Config.Model;

namespace RelertSharp.Terraformer
{
    public static class TiberiumCalc
    {



        #region Api
        public static List<OverlayUnit> AutoFixTiberiumAt(I2dLocateable targetCell, byte overlayIndex)
        {
            List<OverlayUnit> results = new List<OverlayUnit>();
            var tiberiums = GlobalVar.GlobalConfig.ModGeneral.TiberiumInfo;
            var thisType = tiberiums.Find(x => x.BelongsToThis(overlayIndex));
            if (thisType == null) return results;
            OverlayUnit selectTiberium(Tile x) => x.GetObejct(obj => obj is OverlayUnit o && tiberiums.Any(info => info.BelongsToThis(o.OverlayIndex))) as OverlayUnit;
            void fixAround(IEnumerable<OverlayUnit> adjAndDiag, I2dLocateable cell, int divBy, TiberiumInfo info, byte fixIndex = 255)
            {
                int sum = adjAndDiag.Sum(x => x == null ? 0 : x.OverlayFrame);
                int idx = sum / divBy + 2;
                if (idx > 0)
                {
                    OverlayUnit o = new OverlayUnit(fixIndex == 255 ? info.Rand() : fixIndex, (byte)idx.TrimTo(1, 11))
                    {
                        X = cell.X,
                        Y = cell.Y
                    };
                    results.Add(o);
                }
            }
            void fixAt(I2dLocateable centerCell, int div, bool force = false)
            {
                MapApi.GetAdjacentTileAround(centerCell, out Tile[] adjs, out WallDirection[] _);
                MapApi.GetDiagonalTileAround(centerCell, out Tile[] diags, out WallDirection[] _);
                IEnumerable<Tile> tiles = adjs.Concat(diags);
                IEnumerable<OverlayUnit> validTiberiums = tiles.Select(selectTiberium);
                Tile center = GlobalVar.GlobalMap.TilesData[centerCell];
                if (center != null)
                {
                    OverlayUnit o = selectTiberium(center);
                    TiberiumInfo info = null;
                    byte fixOrg = (byte)(o != null ? o.OverlayIndex : 255);
                    if (o != null) info = tiberiums.Find(x => x.BelongsToThis(o.OverlayIndex));
                    else if (force) info = thisType;
                    if (info != null)
                    {
                        fixAround(validTiberiums, center, div, info, fixOrg);
                    }
                }
            }
            // center
            fixAt(targetCell, 8, true);
            // diag and adj
            MapApi.GetAdjacentTileAround(targetCell, out Tile[] centerAdj, out WallDirection[] _);
            MapApi.GetDiagonalTileAround(targetCell, out Tile[] centerDiag, out WallDirection[] _);
            foreach (I2dLocateable cell in centerAdj) fixAt(cell, 7);
            foreach (I2dLocateable cell in centerDiag) fixAt(cell, 7);
            return results;
        }
        #endregion
    }
}
