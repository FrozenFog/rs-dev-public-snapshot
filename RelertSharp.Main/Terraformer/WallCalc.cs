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
        /// return wall index of that cell
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="wallIndex"></param>
        /// <returns></returns>
        public static OverlayUnit FixWallAt(I2dLocateable cell, byte wallIndex)
        {
            var tiles = GlobalVar.GlobalMap.TilesData;
            WallDirection result = WallDirection.Sole;
            if (tiles[cell] is Tile)
            {
                MapApi.GetAdjacentTileAround(cell, out Tile[] adjs, out WallDirection[] directions);
                
                for (int i = 0; i < adjs.Length; i++)
                {
                    Tile t = adjs[i];
                    WallDirection dir = directions[i];
                    if (t.GetObejct(x => x.ObjectType == MapObjectType.Overlay) is OverlayUnit u)
                    {
                        if (u.Index == wallIndex) result |= dir.Reverse();
                    }
                }
            }
            OverlayUnit o = new OverlayUnit()
            {
                X = cell.X,
                Y = cell.Y,
                Index = wallIndex,
                Frame = (byte)result
            };
            return o;
        }
        #endregion
    }
}
