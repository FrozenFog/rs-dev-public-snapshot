﻿using RelertSharp.Common;
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
        private static bool isLat, isSuspended;

        private static TileLayer Tiles { get { return GlobalVar.CurrentMapDocument.Map.TilesData; } }

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
            foreach (Tile t in body) MapApi.SetTile(t);
            foreach (Tile t in under) t.RevealAllTileImg();

            foreach (Tile t in surroundLat) MapApi.SetTile(t);
            foreach (Tile t in surroundUnder) t.RevealAllTileImg();
        }
        public static void LoadTileBrush(TileSetItemVm src)
        {
            EngineApi.InvokeLock();
            tileSetOffset.Clear();
            foreach (Tile t in body) t.Dispose();
            foreach (Tile t in under) t.RevealAllTileImg();
            foreach (Tile t in surroundLat) t.Dispose();
            foreach (Tile t in surroundUnder) t.RevealAllTileImg();
            body.Clear();
            under.Clear();
            surroundLat.Clear();
            surroundUnder.Clear();
            foreach (var subtile in src.SubTiles)
            {
                Tile t = new Tile(src.TileIndex, subtile.SubIndex, INIT_X + subtile.Dx, INIT_Y + subtile.Dy, subtile.Dz);
                EngineApi.DrawTile(t);
                tileSetOffset.Add(new Pnt3(subtile.Dx, subtile.Dy, subtile.Dz));
                body.Add(t);
            }
            isLat = src.IsLat;
            EngineApi.InvokeUnlock();
            MoveTileBrushTo(prevPos);
        }
        public static bool MoveTileBrushTo(I3dLocateable pos)
        {
            EngineApi.InvokeLock();
            foreach (Tile t in under) t.RevealAllTileImg();
            foreach (Tile t in surroundLat) t.Dispose();
            foreach (Tile t in surroundUnder) t.RevealAllTileImg();
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
                    body[i].MoveTo(tileDest);
                    body[i].RevealAllTileImg();
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
        public static void SuspendBrush()
        {
            if (!isSuspended)
            {
                foreach (Tile t in body) t.Hide();
                foreach (Tile t in surroundUnder) t.RevealAllTileImg();
                foreach (Tile t in surroundLat) t.Hide();
                foreach (Tile t in under) t.RevealAllTileImg();
                isSuspended = true;
            }
        }
        public static void ResumeBrush()
        {
            if (isSuspended)
            {
                foreach (Tile t in body) t.RevealAllTileImg();
                foreach (Tile t in surroundUnder) t.Hide();
                foreach (Tile t in surroundLat) t.RevealAllTileImg();
                foreach (Tile t in under) t.Hide();
                isSuspended = false;
            }
        }
        #endregion

        public static bool LatEnable { get; set; } = true;
    }
}
