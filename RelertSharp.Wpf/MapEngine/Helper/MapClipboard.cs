using RelertSharp.Common;
using RelertSharp.Engine.Api;
using RelertSharp.MapStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.MapEngine.Helper
{
    internal static class MapClipboard
    {
        private static List<ClipboardTile> clipTiles = new List<ClipboardTile>();
        private static List<ClipboardObject> clipObjects = new List<ClipboardObject>();
        private static readonly I3dLocateable INIT_POS = new Pnt3(-100, -100, 0);
        private static I2dLocateable prevPos;


        #region Api
        public static void AddTileToClipboard()
        {
            clipTiles.Clear();
            if (!TileSelector.SelectedTile.Any()) return;
            I3dLocateable align = GetAlignment(TileSelector.SelectedTile);
            TileSelector.SelectedTile.Foreach(x =>
            {
                var clip = ClipboardTile.FromTile(x);
                clip.Offset = RsMath.I3dSubi(x, align);
                clipTiles.Add(clip);
            });
        }
        public static void AddObjectToClipboard()
        {
            clipObjects.Clear();
            if (!Selector.SelectedObjects.Any()) return;
            I2dLocateable align = GetAlignment(Selector.SelectedObjects);
            Selector.SelectedObjects.Foreach(x =>
            {
                var clip = ClipboardObject.FromObject(x);
                clip.Offset = RsMath.I2dSubi(x, align);
                clipObjects.Add(clip);
            });
        }
        public static void LoadToTileBrush()
        {
            TilePaintBrush.LoadTileBrush(clipTiles);
        }
        public static void LoadToObjectBrush()
        {
            foreach (var clip in clipObjects)
            {
                clip.Object = clip.ReferanceSource.ConstructFromParameter(clip.Param);
                I2dLocateable pos = RsMath.I2dAddi(INIT_POS, clip.Offset);
                clip.ApplyPosition(pos);
                EngineApi.DrawObject(clip.Object);
            }
        }
        public static bool MoveClipObjectTo(I2dLocateable alignDest)
        {
            var tiles = GlobalVar.GlobalMap.TilesData;
            foreach (var clip in clipObjects)
            {
                I2dLocateable pos = RsMath.I2dAddi(alignDest, clip.Offset);
                if (tiles[pos] is Tile t)
                {
                    clip.Object.MoveTo(t);
                }
            }
            if (prevPos == null)
            {
                prevPos = alignDest;
                return true;
            }
            else
            {
                bool b = prevPos.Coord == alignDest.Coord;
                prevPos = alignDest;
                return b;
            }
        }
        public static void AddClipObjectToMap()
        {
            foreach (var clip in clipObjects)
            {
                IMapObject clone = clip.Object.ConstructFromParameter(clip.Object.ExtractParameter());
                MapApi.AddObject(clone, true);
                EngineApi.DrawObject(clone);
            }
        }
        public static void SuspendClipObjects()
        {
            foreach (var clip in clipObjects)
            {
                clip.Object?.Dispose();
            }
            clipObjects.Clear();
        }
        #endregion


        #region Private
        private static I3dLocateable GetAlignment(IEnumerable<I3dLocateable> src)
        {
            int x = src.Min(d => d.X);
            int y = src.Min(d => d.Y);
            int z = src.Min(d => d.Z);
            return new Pnt3(x, y, z);
        }
        private static I2dLocateable GetAlignment(IEnumerable<I2dLocateable> src)
        {
            int x = src.Min(d => d.X);
            int y = src.Min(d => d.Y);
            return new Pnt(x, y);
        }
        #endregion



        private class ClipboardTile : ITile
        {
            public int TileIndex { get; set; }
            public byte SubIndex { get; set; }
            public I3dLocateable Offset { get; set; }
            public Tile Tile { get; set; }
            public int Z { get => Offset.Z; set => Offset.Z = value; }
            public int X { get => Offset.X; set => Offset.X = value; }
            public int Y { get => Offset.Y; set => Offset.Y = value; }

            public int Coord => Offset.Coord;

            public static ClipboardTile FromTile(Tile src)
            {
                return new ClipboardTile()
                {
                    TileIndex = src.TileIndex,
                    SubIndex = src.SubIndex
                };
            }
        }
        private class ClipboardObject
        {
            public string[] Param { get; set; }
            public MapObjectType Type { get; set; }
            public I2dLocateable Offset { get; set; }
            public IMapObject ReferanceSource { get; private set; }
            public IMapObject Object { get; set; }


            /// <summary>
            /// apply position to Object
            /// </summary>
            /// <param name="pos"></param>
            public void ApplyPosition(I2dLocateable pos)
            {
                Object.X = pos.X;
                Object.Y = pos.Y;
            }
            public static ClipboardObject FromObject(IMapObject src)
            {
                return new ClipboardObject()
                {
                    Param = src.ExtractParameter(),
                    Type = src.ObjectType,
                    ReferanceSource = src
                };
            }
        }
    }
}
