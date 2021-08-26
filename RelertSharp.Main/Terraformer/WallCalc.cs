using RelertSharp.Algorithm;
using RelertSharp.Common;
using RelertSharp.MapStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Terraformer
{
    public static class WallCalc
    {
        #region Api
        /// <summary>
        /// will directly alter the frame of the input overlay
        /// will not draw or update other info
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="wallIndex"></param>
        /// <returns></returns>
        public static void FixWallAt(OverlayUnit src, byte wallIndex)
        {
            var tiles = GlobalVar.GlobalMap.TilesData;
            WallDirection result = WallDirection.Sole;
            if (tiles[src] is Tile)
            {
                MapApi.GetAdjacentTileAround(src, out Tile[] adjs, out WallDirection[] directions);
                
                for (int i = 0; i < adjs.Length; i++)
                {
                    Tile t = adjs[i];
                    WallDirection dir = directions[i];
                    if (t.GetObejct(x => x.ObjectType == MapObjectType.Overlay) is OverlayUnit u)
                    {
                        if (u.OverlayIndex == wallIndex) result |= dir.Reverse();
                    }
                }
            }
            src.OverlayFrame = (byte)result;
        }
        public static void FixWallIn(IEnumerable<OverlayUnit> src)
        {
            Dictionary<Pnt, OverlayUnit> cells = new Dictionary<Pnt, OverlayUnit>();
            foreach (var o in src) cells[new Pnt(o)] = o;

            foreach (var o in cells.Values)
            {
                WallDirection dir = WallDirection.Sole;
                if (cells.ContainsKey(new Pnt(o.X + 1, o.Y))) dir |= WallDirection.SE;
                if (cells.ContainsKey(new Pnt(o.X, o.Y + 1))) dir |= WallDirection.SW;
                if (cells.ContainsKey(new Pnt(o.X - 1, o.Y))) dir |= WallDirection.NW;
                if (cells.ContainsKey(new Pnt(o.X, o.Y - 1))) dir |= WallDirection.NE;
                o.OverlayFrame = (byte)dir;
            }
        }
        #endregion
    }
}
