using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;

namespace RelertSharp.MapStructure
{
    public static partial class MapApi
    {
        private static MapTheaterTileSet TileDictionary { get { return GlobalVar.TileDictionary; } }
        #region Set Tile & LAT
        /// <summary>
        /// The direction returned is from result to referance center tile
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="adjacent"></param>
        /// <param name="directions"></param>
        public static void GetAdjacentTileAround(I2dLocateable pos, out Tile[] adjacent, out WallDirection[] directions)
        {
            adjacent = Map.TilesData.GetNeighbor(pos, out List<WallDirection> dir).ToArray();
            directions = dir.ToArray();
        }
        public static void GetDiagonalTileAround(I2dLocateable pos, out Tile[] diagonal, out WallDirection[] directions)
        {
            diagonal = Map.TilesData.GetDiagonalTile(pos, out List<WallDirection> dir).ToArray();
            directions = dir.ToArray();
        }
        public static List<Tile> GetBorderTileAround(IEnumerable<Tile> src)
        {
            HashSet<Pnt> center = src.Select(x => new Pnt(x.X, x.Y)).ToHashSet();
            Dictionary<Pnt, Tile> result = new Dictionary<Pnt, Tile>();
            foreach (Tile body in src)
            {
                GetAdjacentTileAround(body, out Tile[] adjs, out WallDirection[] _);
                foreach (Tile adj in adjs)
                {
                    if (!center.Contains(new Pnt(adj))) result[new Pnt(adj)] = adj;
                }
            }
            return result.Values.ToList();
        }
        public static void SetTile(IMapTileBrushConfig config)
        {
            if (Map.TilesData[config.Pos] is Tile org && !org.IsFrozen)
            {
                org.ReplaceWithNewTileConfig(config);
                org.Redraw();
            }
        }
        public static bool SetTile(int tileIndex, byte subindex, I2dLocateable pos, int height = -1)
        {
            if (Map.TilesData[pos] is Tile org && !org.IsFrozen)
            {
                org.SetTileTo(tileIndex, subindex);
                if (height != -1) org.SetHeightTo(height, false, true);
                org.Redraw();
                return true;
            }
            return false;
        }
        public static List<Tile> SelectTile(IEnumerable<I2dLocateable> posTile)
        {
            List<Tile> result = new List<Tile>();
            foreach (I2dLocateable pos in posTile)
            {
                if (Map.TilesData[pos] is Tile t) result.Add(t);
            }
            return result;
        }
        public static void SetTile(Tile src)
        {
            if (Map.TilesData[src] is Tile org && !org.IsFrozen)
            {
                org.SetTileTo(src.TileIndex, src.SubIndex);
                org.SetHeightTo(src.RealHeight, false, true);
                org.Redraw();
            }
        }
        /// <summary>
        /// Will use map data for adjacent 4 referance tile
        /// Center tile will be redrawn
        /// </summary>
        /// <param name="center"></param>
        public static void SwitchCenterTileToLat(Tile center)
        {
            //Tile center = config.ComposeTile();
            if (TileDictionary.IsLat(center))
            {
                TileSet setCenter = TileDictionary.GetTileSetFromTile(center);
                int result = 0;
                Pnt pos = new Pnt(center);
                if (Map.TilesData[pos + Pnt.OneX] is Tile dr && TileDictionary.IsClearLat(dr, center)) result += (int)WallDirection.SE;
                if (Map.TilesData[pos - Pnt.OneX] is Tile ul && TileDictionary.IsClearLat(ul, center)) result += (int)WallDirection.NW;
                if (Map.TilesData[pos + Pnt.OneY] is Tile dl && TileDictionary.IsClearLat(dl, center)) result += (int)WallDirection.SW;
                if (Map.TilesData[pos - Pnt.OneY] is Tile ur && TileDictionary.IsClearLat(ur, center)) result += (int)WallDirection.NE;

                SetCenterTileToLat(center, setCenter, result);
            }
        }
        /// <summary>
        /// Use certain direction tile as Lat referance,
        /// used by adjacent tile Lat calculation
        /// </summary>
        /// <param name="center">Center tile(adjacent) that will affect by Lat, WILL change tile property</param>
        /// <param name="referanceTile">The REAL center tile, will not be affected</param>
        /// <param name="referanceDirection">Direction from <paramref name="center"/> to <paramref name="referanceTile"/></param>
        public static void SwitchCenterTileToLat(Tile center, Tile referanceTile, WallDirection referanceDirection)
        {
            if (center != null && TileDictionary.IsLat(center))
            {
                TileSet setCenter = TileDictionary.GetTileSetFromTile(center);
                TileSet referance = TileDictionary.GetTileSetFromTile(referanceTile);
                int result = 0;
                if (!TileDictionary.LatEqual(setCenter.SetIndex, referance.SetIndex))
                {
                    result = (int)referanceDirection;
                    //referanceDirection = WallDirection.All;
                }
                Pnt pos = new Pnt(center);
                if (referanceDirection != WallDirection.SE && Map.TilesData[pos + Pnt.OneX] is Tile dr && TileDictionary.IsClearLat(dr, center)) result += (int)WallDirection.SE;
                if (referanceDirection != WallDirection.NW && Map.TilesData[pos - Pnt.OneX] is Tile ul && TileDictionary.IsClearLat(ul, center)) result += (int)WallDirection.NW;
                if (referanceDirection != WallDirection.SW && Map.TilesData[pos + Pnt.OneY] is Tile dl && TileDictionary.IsClearLat(dl, center)) result += (int)WallDirection.SW;
                if (referanceDirection != WallDirection.NE && Map.TilesData[pos - Pnt.OneY] is Tile ur && TileDictionary.IsClearLat(ur, center)) result += (int)WallDirection.NE;

                SetCenterTileToLat(center, setCenter, result < 0 ? 0 : result);
                //if (!TileDictionary.IsConnLat(setCenter.SetIndex)) setCenter = TileDictionary.GetTileSetFromIndex(TileDictionary.SwitchLatIndex(setCenter.SetIndex));
                //if (Constant.LATSystem.idxClear == setCenter.SetIndex) result = 0;
                //center.TileIndex = setCenter.Offset + result;
                //center.Redraw();
            }
        }
        public static bool NeedLat(Tile obj)
        {
            if (obj == null) return false;
            return Constant.LATSystem.LatSet.Contains(TileDictionary.GetTileSetIndexFromTile(obj));
        }

        private static void SetCenterTileToLat(Tile center, TileSet setCenter, int direction)
        {
            if (direction == 0 && TileDictionary.IsConnLat(setCenter.SetIndex))
            {
                setCenter = TileDictionary.SwapSet(setCenter);
            }
            else if (direction != 0 && !TileDictionary.IsConnLat(setCenter.SetIndex) && setCenter.SetIndex != Constant.LATSystem.idxClear)
            {
                setCenter = TileDictionary.SwapSet(setCenter);
            }
            else if (direction != 0 && setCenter.SetIndex == Constant.LATSystem.idxClear)
            {
                direction = 0;
            }
            center.TileIndex = setCenter.Offset + direction;
            center.Redraw();
        }
        #endregion


        #region Hide & Reveal
        private static HashSet<I2dLocateable> hiddenTiles = new HashSet<I2dLocateable>();
        public static void HideTileAt(IEnumerable<I2dLocateable> poses)
        {
            foreach (I2dLocateable pos in poses) HideTileAt(pos);
        }
        public static void HideTileAt(I2dLocateable pos)
        {
            if (Map.TilesData[pos] is Tile t && !hiddenTiles.Contains(t))
            {
                hiddenTiles.Add(t);
                t.Hide();
            }
        }
        public static void RevealTileAt(IEnumerable<I2dLocateable> poses)
        {
            foreach (I2dLocateable pos in poses) RevealTileAt(pos);
        }
        public static void RevealTileAt(I2dLocateable pos)
        {
            if (hiddenTiles.Contains(pos))
            {
                Map.TilesData[pos].Reveal();
                hiddenTiles.Remove(pos);
            }
        }
        public static void RevealAllHiddenTile()
        {
            foreach (I2dLocateable pos in hiddenTiles)
            {
                if (hiddenTiles.Contains(pos))
                {
                    Map.TilesData[pos].Reveal();
                }
            }
            hiddenTiles.Clear();
        }
        #endregion


        #region Framework & Flat Ground
        public static void SetFramework(bool enable)
        {
            if (GlobalVar.HasMap) Map.TilesData.SwitchFramework(enable);
        }
        public static void SetFlatGround(bool enable)
        {
            if (GlobalVar.HasMap) Map.TilesData.SwitchFlatGround(enable);
        }
        #endregion
    }
}
