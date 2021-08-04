using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Algorithm;
using RelertSharp.Common;
using RelertSharp.MapStructure;
using RelertSharp.Engine.Api;

namespace RelertSharp.Wpf.MapEngine.Helper
{
    internal static class TileSelector
    {
        private static DdaLineDrawing dda = new DdaLineDrawing();
        private static LinkedList<I2dLocateable> controlNodes = new LinkedList<I2dLocateable>();
        private static I2dLocateable prevCell;
        private static HashSet<Tile> selected = new HashSet<Tile>();
        private static bool filtSet, filtHeight;



        #region Api
        public static void AddLineControlNode(I2dLocateable cell, bool select = true)
        {
            var tiles = GlobalVar.GlobalMap.TilesData;
            controlNodes.AddLast(cell);
            if (select)
            {
                dda.SetControlNode(prevCell == null ? cell : prevCell, cell);
                EngineApi.InvokeLock();
                foreach (I2dLocateable pos in dda.GetLineCells())
                {
                    if (tiles[pos] is Tile t)
                    {
                        t.Select();
                        selected.Add(t);
                    }
                }
                EngineApi.InvokeUnlock();
            }
            prevCell = cell;
        }
        public static void BucketAt(I2dLocateable cell)
        {
            var tiles = GlobalVar.GlobalMap.TilesData;
            var dict = GlobalVar.TileDictionary;
            if (tiles[cell] is Tile center)
            {
                int set = dict.GetTileSetIndexFromTile(center);
                List<Predicate<Tile>> predicates = new List<Predicate<Tile>>()
                {
                    x => !x.IsSelected
                };
                if (filtSet) predicates.Add(x => dict.GetTileSetIndexFromTile(x) == set);
                if (filtHeight) predicates.Add(x => x.RealHeight == center.RealHeight);
                Bfs2D bfs = new Bfs2D(tiles, center, predicates);
                EngineApi.InvokeLock();
                foreach (Tile t in bfs)
                {
                    t.Select();
                    selected.Add(t);
                }
                EngineApi.InvokeUnlock();
            }
        }
        public static void BucketTilesetFilter(bool enable)
        {
            filtSet = enable;
        }
        public static void BucketHeightFilter(bool enable)
        {
            filtHeight = enable;
        }
        public static void CancelAllTileSelection()
        {
            selected.Foreach(x => x.CancelSelection());
            selected.Clear();
        }
        public static void RiseAllSelectedTile()
        {
            EngineApi.InvokeLock();
            foreach (Tile t in selected)
            {
                t.Rise();
            }
            EngineApi.InvokeUnlock();
        }
        public static void SinkAllSelectedTile()
        {
            EngineApi.InvokeLock();
            foreach (Tile t in selected)
            {
                t.Sink();
            }
            EngineApi.InvokeUnlock();
        }
        public static void AllSetHeightTo(int height)
        {
            EngineApi.InvokeLock();
            foreach (Tile t in selected)
            {
                t.SetHeightTo(height);
            }
            EngineApi.InvokeUnlock();
        }
        #endregion


        #region Calls
        public static IEnumerable<Tile> SelectedTile { get { return selected; } }
        public static bool IsTileSetFilter { get { return filtSet; } }
        public static bool IsHeightFilter { get { return filtHeight; } }
        #endregion
    }
}
