using RelertSharp.Common;
using RelertSharp.Engine.Api;
using RelertSharp.MapStructure;
using RelertSharp.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.MapEngine.Helper
{
    internal static class TilePaintBrush
    {
        private const int INIT_X = -100, INIT_Y = -100;
        private static List<Tile> body = new List<Tile>();
        private static List<Tile> under = new List<Tile>();
        private static List<Tile> surroundLat = new List<Tile>();
        private static List<Tile> surroundUnder = new List<Tile>();
        private static List<I3dLocateable> tileSetOffset = new List<I3dLocateable>();
        private static I3dLocateable prevPos;
        private static Pnt3 offset;
        private static bool isLat, isSuspended, isFlat, isClipboard;
        private static TileSetItemVm currentSet;

        private static TileLayer Tiles { get { return GlobalVar.GlobalMap.TilesData; } }

        static TilePaintBrush()
        {
            prevPos = new Pnt3(INIT_X, INIT_Y, 0);
            offset = new Pnt3();
        }


        #region Api
        #region Invoker
        public static event EventHandler SelectTileForwardRequest;
        public static void InvokeForward()
        {
            SelectTileForwardRequest?.Invoke(null, null);
        }
        public static event EventHandler SelectTileBackwardRequest;
        public static void InvokeBackward()
        {
            SelectTileBackwardRequest?.Invoke(null, null);
        }
        #endregion
        #region Move, Add, Load
        public static void AddTileToMap()
        {
            EngineApi.InvokeLock();
            List<Tile> org = new List<Tile>(under);
            org.AddRange(surroundUnder);
            List<Tile> dest = new List<Tile>(body);
            dest.AddRange(surroundLat);
            UndoRedoHub.PushCommand(dest, org);
            foreach (Tile t in body) MapApi.SetTile(t);
            foreach (Tile t in under) t.Reveal();

            foreach (Tile t in surroundLat) MapApi.SetTile(t);
            foreach (Tile t in surroundUnder) t.Reveal();
            EngineApi.InvokeUnlock();
        }
        public static void LoadTileBrush(TileSetItemVm src)
        {
            isClipboard = false;
            currentSet = src;
            EngineApi.InvokeLock();
            tileSetOffset.Clear();
            foreach (Tile t in body) t.Dispose();
            foreach (Tile t in under) t.Reveal();
            foreach (Tile t in surroundLat) t.Dispose();
            foreach (Tile t in surroundUnder) t.Reveal();
            body.Clear();
            under.Clear();
            surroundLat.Clear();
            surroundUnder.Clear();
            foreach (var subtile in src.SubTiles)
            {
                Tile t = new Tile(src.TileIndex, subtile.SubIndex, INIT_X + subtile.Dx, INIT_Y + subtile.Dy, subtile.Dz);
                EngineApi.DrawTile(t);
                t.FlatToGround(isFlat);
                tileSetOffset.Add(new Pnt3(subtile.Dx, subtile.Dy, subtile.Dz));
                body.Add(t);
            }
            isLat = src.IsLat;
            MoveTileBrushTo(prevPos);
            EngineApi.InvokeUnlock();
        }
        /// <summary>
        /// used by clipboard
        /// </summary>
        /// <param name="src"></param>
        public static void LoadTileBrush(IEnumerable<ITile> src)
        {
            isClipboard = true;
            currentSet = null;
            EngineApi.InvokeLock();
            tileSetOffset.Clear();
            foreach (Tile t in body) t.Dispose();
            foreach (Tile t in under) t.Reveal();
            foreach (Tile t in surroundLat) t.Dispose();
            foreach (Tile t in surroundUnder) t.Reveal();
            body.Clear();
            under.Clear();
            surroundLat.Clear();
            surroundUnder.Clear();
            foreach (ITile tile in src)
            {
                Tile t = new Tile(tile.TileIndex, tile.SubIndex, INIT_X + tile.X, INIT_Y + tile.Y, tile.Z);
                EngineApi.DrawTile(t);
                t.FlatToGround(isFlat);
                tileSetOffset.Add(new Pnt3(tile));
                body.Add(t);
            }
            isLat = false;
            MoveTileBrushTo(prevPos);
            EngineApi.InvokeUnlock();
        }
        public static bool MoveTileBrushTo(I3dLocateable pos)
        {
            /// pre-process height(flat ground or not)
            if (Tiles[pos] is Tile mapPosition) pos.Z = mapPosition.RealHeight;

            /// do job
            EngineApi.InvokeLock();
            foreach (Tile t in under) t.Reveal();
            foreach (Tile t in surroundLat) t.Dispose();
            foreach (Tile t in surroundUnder) t.Reveal();
            under.Clear();
            surroundLat.Clear();
            surroundUnder.Clear();
            for (int i = 0; i < body.Count; i++)
            {
                I3dLocateable tileDest = new Pnt3(pos.X + offset.X + tileSetOffset[i].X, pos.Y + offset.Y + tileSetOffset[i].Y, pos.Z + offset.Z + tileSetOffset[i].Z);
                /// if destination has no tile (out of size)
                /// hide the tile.
                /// otherwise reveal and move
                if (Tiles[tileDest] is Tile)
                {
                    body[i].MoveTo(tileDest, isFlat);
                    body[i].Reveal();
                    body[i].FlatToGround(isFlat);
                }
                else
                {
                    body[i].Hide();
                }
                if (Tiles[body[i]] is Tile tUnder)
                {
                    under.Add(tUnder);
                    tUnder.Hide();
                }

                // lat
                if (LatEnable && isLat)
                {
                    MapApi.GetAdjacentTileAround(body[i], out Tile[] adjs, out WallDirection[] dirs);
                    for (int j = 0; j < adjs.Length; j++)
                    {
                        Tile adj = adjs[j];
                        if (MapApi.NeedLat(adj))
                        {
                            adj.Hide();
                            surroundUnder.Add(adj);
                            Tile surround = Tile.CopyAsReferance(adj);
                            MapApi.SwitchCenterTileToLat(surround, body[i], dirs[j]);
                            EngineApi.DrawTile(surround);
                            surroundLat.Add(surround);
                        }
                    }
                    MapApi.SwitchCenterTileToLat(body[i]);
                }
            }
            prevPos = pos;
            EngineApi.InvokeUnlock();
            return true;
        }
        #endregion
        #region Offset
        public static void MoveOffsetXAxis(int amount)
        {
            offset.X += amount;
        }
        public static void MoveOffsetYAxis(int amount)
        {
            offset.Y += amount;
        }
        public static void MoveOffsetHeight(int amount)
        {
            if (offset.Z <= Constant.DrawingEngine.MapMaxHeight || amount < 0)
            {
                offset.Z += amount;
            }
        }
        public static void ResetOffset()
        {
            offset = new Pnt3();
        }
        #endregion
        #region Brush suspending
        public static void SuspendBrush()
        {
            if (isClipboard)
            {
                foreach (Tile t in body) t.Dispose();
                isClipboard = false;
            }
            else
            {
                if (!isSuspended)
                {
                    foreach (Tile t in body) t.Hide();
                    foreach (Tile t in surroundUnder) t.Reveal();
                    foreach (Tile t in surroundLat) t.Hide();
                    foreach (Tile t in under) t.Reveal();
                    isSuspended = true;
                }
            }
        }
        public static void ResumeBrush()
        {
            if (isSuspended)
            {
                foreach (Tile t in body) t.Reveal();
                foreach (Tile t in surroundUnder) t.Hide();
                foreach (Tile t in surroundLat) t.Reveal();
                foreach (Tile t in under) t.Hide();
                isSuspended = false;
            }
        }
        #endregion
        #region Framework & Flat
        public static void SwitchToFramework(bool enable)
        {
            foreach (Tile t in body) t.SwitchToFramework(enable);
            foreach (Tile t in surroundLat) t.SwitchToFramework(enable);
        }
        public static void SwitchToFlatGround(bool enable)
        {
            foreach (Tile t in body) t.FlatToGround(enable);
            foreach (Tile t in surroundLat) t.FlatToGround(enable);
            isFlat = enable;
        }
        #endregion
        #region Bucket
        public static void BucketTileAt(I2dLocateable center, IEnumerable<Predicate<Tile>> predicates = null)
        {
            var tiles = GlobalVar.GlobalMap.TilesData;
            if (currentSet != null && tiles[center] is Tile seed)
            {
                EngineApi.InvokeLock();
                if (predicates == null || !predicates.Any())
                {
                    predicates = new List<Predicate<Tile>>()
                    {
                        x => x.IsSelected
                    };
                }
                Bfs2D bfs = new Bfs2D(tiles, seed, predicates);
                HashSet<Tile> targets = new HashSet<Tile>();
                foreach (Tile t in bfs) targets.Add(t);
                if (targets.Count == 0) goto end;
                int xMax = targets.Max(x => x.X), yMax = targets.Max(x => x.Y);
                int xMin = targets.Min(x => x.X), yMin = targets.Min(x => x.Y);
                int dx = xMax - xMin;
                int dy = yMax - yMin;
                int xNum = RsMath.Ceil(dx / (double)currentSet.SetWidth), yNum = RsMath.Ceil(dy / (double)currentSet.SetHeight);

                // begin position: xMin, yMin
                for (int y = yMin; y <= yMax; y += currentSet.SetHeight)
                {
                    for (int x = xMin; x <= xMax; x += currentSet.SetWidth)
                    {
                        foreach (var subtile in currentSet.SubTiles)
                        {
                            int targetX = x + subtile.Dx;
                            int targetY = y + subtile.Dy;
                            if (tiles[targetX, targetY] is Tile target && target.IsSelected)
                            {
                                Tile t = new Tile(currentSet.TileIndex, subtile.SubIndex, targetX, targetY, target.RealHeight + subtile.Dz);
                                t.FlatToGround(isFlat);
                                MapApi.SetTile(t);
                            }
                        }
                    }
                }
                end:
                EngineApi.InvokeUnlock();
            }


        }
        #endregion
        #endregion

        public static bool LatEnable { get; set; } = true;
    }
}
