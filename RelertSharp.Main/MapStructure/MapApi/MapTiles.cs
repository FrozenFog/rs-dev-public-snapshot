﻿using System;
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
        public static void SetTile(IMapTileBrushConfig config)
        {
            if (Map.TilesData[config.Pos] is Tile org)
            {
                org.ReplaceWithNewTileConfig(config);
                org.Redraw();
            }
        }
        /// <summary>
        /// Will use map data for adjacent 4 referance tile
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
                t.HideTileImg();
                t.HideExtraImg();
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
                Map.TilesData[pos].RevealAllTileImg();
                hiddenTiles.Remove(pos);
            }
        }
        public static void RevealAllHiddenTile()
        {
            foreach (I2dLocateable pos in hiddenTiles)
            {
                if (hiddenTiles.Contains(pos))
                {
                    Map.TilesData[pos].RevealAllTileImg();
                }
            }
            hiddenTiles.Clear();
        }
        #endregion
    }
}