using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RelertSharp.Encoding;
using System.Collections;
using RelertSharp.FileSystem;
using RelertSharp.Utils;
using RelertSharp.Common;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;
using RelertSharp.DrawingEngine.Presenting;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.MapStructure
{
    public class TileLayer : IEnumerable<Tile>
    {
        private Dictionary<int, Tile> data = new Dictionary<int, Tile>();
        private List<int> indexs = new List<int>();
        private byte bottomLevel = 255;


        #region Ctor - TileLayer
        public TileLayer(string stringPack, Rectangle Size)
        {
            byte[] fromBase64 = Convert.FromBase64String(stringPack);
            int tileNum = (Size.Width * 2 - 1) * Size.Height;
            byte[] tileData = PackEncoding.DecodePack(fromBase64, tileNum, PackType.IsoMapPack);
            BinaryReader br = new BinaryReader(new MemoryStream(tileData));
            for (; tileNum > 0; tileNum--)
            {
                short x = br.ReadInt16();
                short y = br.ReadInt16();
                int tileIndex = br.ReadInt32();
                byte tileSubIndex = br.ReadByte();
                byte level = br.ReadByte();
                byte iceGrowth = br.ReadByte();
                bottomLevel = Math.Min(level, bottomLevel);
                int coord = Misc.CoordInt(x, y);
                if ((x | y | tileIndex | tileSubIndex | level | iceGrowth) == 0) continue;
                data[coord] = new Tile(x, y, tileIndex, tileSubIndex, level, iceGrowth);
                indexs.Add(coord);
            }
        }
        #endregion


        #region Private Methods - TileLayer
        private void LayTileWeb(int xmin, int ymax, int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                int x = xmin;
                int y = ymax;
                for (int j = 0; j < width; j++)
                {
                    if (this[x, y] == null) this[x, y] = Tile.EmptyTileAt(x, y);
                    x++;
                    y--;
                }
                xmin++;
                ymax++;
            }
        }
        private void SetLatBase(Tile center, TileSet setCenter, int direction)
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
        //private void SetTilePixelInPreview(Bitmap dest, Tile t, int bmpx, int bmpy)
        //{
        //    TileAbstract abs = GlobalVar.TileDictionary.GetTileAbstract(t.TileIndex);
        //    var sub = abs[t.SubIndex];
        //    dest.SetPixel(bmpx, bmpy, sub.ColorLeft);
        //    dest.SetPixel(bmpx + 1, bmpy, sub.ColorRight);
        //}
        #endregion


        #region Public Methods - TileLayer
        //public Bitmap GenerateShot(Rectangle rect)
        //{
        //    int width = rect.Width;
        //    int height = rect.Height;
        //    Bitmap bmp = new Bitmap(width * 2, height * 2);
        //    int xmin = 1;
        //    int ymax = width;
        //    for (int j = 0; j < height; j++)
        //    {
        //        int x = xmin;
        //        int y = ymax;
        //        for (int i = 0; i<width; i++)
        //        {
        //            SetTilePixelInPreview(bmp, this[x, y], 2 * i, 2 * j);
        //            x++; y--;
        //        }
        //        xmin++; ymax++;
        //    }
        //    xmin = 2;
        //    ymax = width;
        //    for (int j = 0; j < height; j++)
        //    {
        //        int x = xmin;
        //        int y = ymax;
        //        for (int i = 0; i < width - 1; i++)
        //        {
        //            SetTilePixelInPreview(bmp, this[x, y], 2 * i + 1, 2 * j + 1);
        //            x++;y--;
        //        }
        //        xmin++; ymax++;
        //    }
        //    return bmp;
        //}
        public IEnumerable<Tile> GetNeighbor(Tile src)
        {
            List<Tile> result = new List<Tile>();
            Pnt pos = new Pnt(src);
            if (this[pos + Pnt.OneX] is Tile dr) result.Add(dr);
            if (this[pos - Pnt.OneX] is Tile ul) result.Add(ul);
            if (this[pos + Pnt.OneY] is Tile dl) result.Add(dl);
            if (this[pos - Pnt.OneY] is Tile ur) result.Add(ur);
            return result;
        }
        public List<Tile> GetNeighbor(Tile src, out List<WallDirection> directions)
        {
            List<Tile> result = new List<Tile>();
            directions = new List<WallDirection>();
            Pnt pos = new Pnt(src);
            if (this[pos + Pnt.OneX] is Tile dr)
            {
                result.Add(dr);
                directions.Add(WallDirection.NW);
            }
            if (this[pos - Pnt.OneX] is Tile ul)
            {
                result.Add(ul);
                directions.Add(WallDirection.SE);
            }
            if (this[pos + Pnt.OneY] is Tile dl)
            {
                result.Add(dl);
                directions.Add(WallDirection.NE);
            }
            if (this[pos - Pnt.OneY] is Tile ur)
            {
                result.Add(ur);
                directions.Add(WallDirection.SW);
            }
            return result;
        }
        public bool NeedLat(Tile obj)
        {
            if (obj == null) return false;
            return Constant.LATSystem.LatSet.Contains(TileDictionary.GetTileSetIndexFromTile(obj));
        }
        public void SwitchLat(Tile center)
        {
            if (center != null && TileDictionary.IsLat(center))
            {
                TileSet setCenter = TileDictionary.GetTileSetFromTile(center);
                int result = 0;
                Pnt pos = new Pnt(center);
                if (this[pos + Pnt.OneX] is Tile dr && TileDictionary.IsClearLat(dr, center)) result += (int)WallDirection.SE;
                if (this[pos - Pnt.OneX] is Tile ul && TileDictionary.IsClearLat(ul, center)) result += (int)WallDirection.NW;
                if (this[pos + Pnt.OneY] is Tile dl && TileDictionary.IsClearLat(dl, center)) result += (int)WallDirection.SW;
                if (this[pos - Pnt.OneY] is Tile ur && TileDictionary.IsClearLat(ur, center)) result += (int)WallDirection.NE;

                SetLatBase(center, setCenter, result);
                //else
                //{
                //    if (!TileDictionary.IsConnLat(setCenter.SetIndex)) setCenter = TileDictionary.SwapSet(setCenter);
                //    if (Constant.LATSystem.idxClear == setCenter.SetIndex) result = 0;
                //}
            }
        }
        public void SwitchLat(Tile center, Tile referanceTile, WallDirection referanceDirection)
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
                if (referanceDirection != WallDirection.SE && this[pos + Pnt.OneX] is Tile dr && TileDictionary.IsClearLat(dr, center)) result += (int)WallDirection.SE;
                if (referanceDirection != WallDirection.NW && this[pos - Pnt.OneX] is Tile ul && TileDictionary.IsClearLat(ul, center)) result += (int)WallDirection.NW;
                if (referanceDirection != WallDirection.SW && this[pos + Pnt.OneY] is Tile dl && TileDictionary.IsClearLat(dl, center)) result += (int)WallDirection.SW;
                if (referanceDirection != WallDirection.NE && this[pos - Pnt.OneY] is Tile ur && TileDictionary.IsClearLat(ur, center)) result += (int)WallDirection.NE;

                SetLatBase(center, setCenter, result < 0 ? 0 : result);
                //if (!TileDictionary.IsConnLat(setCenter.SetIndex)) setCenter = TileDictionary.GetTileSetFromIndex(TileDictionary.SwitchLatIndex(setCenter.SetIndex));
                //if (Constant.LATSystem.idxClear == setCenter.SetIndex) result = 0;
                //center.TileIndex = setCenter.Offset + result;
                //center.Redraw();
            }
        }
        public void AddObjectOnTile(IMapObject src)
        {
            this[src]?.AddObject(src);
        }
        public void AddObjectOnTile(I2dLocateable pos, IMapObject src)
        {
            this[pos]?.AddObject(src);
        }
        public void FixEmptyTiles(int width, int height)
        {
            LayTileWeb(1, width, width, height);
            LayTileWeb(2, width, width - 1, height);
        }
        public bool HasTileOn(I3dLocateable pos)
        {
            Tile t = this[pos.X, pos.Y];
            if (t != null) return t.Z == pos.Z;
            return false;
        }
        public bool HasTileOn(I2dLocateable tile)
        {
            return this[tile] != null;
        }
        public bool HasTileOn(Vec3 pos)
        {
            Tile t = this[pos.ToCoord()];
            if (t != null) return t.Z == pos.Z;
            return false;
        }
        public void Sort()
        {
            int[] result = new int[indexs.Count];
            Dictionary<int, List<int>> byTileIndex = new Dictionary<int, List<int>>();
            foreach (int coord in indexs)
            {
                int tileindex = data[coord].TileIndex;
                if (!byTileIndex.Keys.Contains(tileindex))
                {
                    byTileIndex[tileindex] = new List<int>();
                }
                byTileIndex[tileindex].Add(coord);
            }
            byTileIndex = byTileIndex.OrderBy(p => p.Key).ToDictionary(p => p.Key, o => o.Value);
            int i = 0;
            foreach (List<int> sameTileIndex in byTileIndex.Values)
            {
                Dictionary<int, List<int>> bySubIndex = new Dictionary<int, List<int>>();
                foreach (int coord in sameTileIndex)
                {
                    int subindex = data[coord].SubIndex;
                    if (!bySubIndex.Keys.Contains(subindex))
                    {
                        bySubIndex[subindex] = new List<int>();
                    }
                    bySubIndex[subindex].Add(coord);
                }
                bySubIndex = bySubIndex.OrderBy(p => p.Key).ToDictionary(p => p.Key, o => o.Value);
                foreach (List<int> sameSubIndex in bySubIndex.Values)
                {
                    Dictionary<int, List<int>> byHeight = new Dictionary<int, List<int>>();
                    foreach (int coord in sameSubIndex)
                    {
                        int height = (int)data[coord].Height;
                        if (!byHeight.Keys.Contains(height))
                        {
                            byHeight[height] = new List<int>();
                        }
                        byHeight[height].Add(coord);
                    }
                    byHeight = byHeight.OrderBy(p => p.Key).ToDictionary(p => p.Key, o => o.Value);
                    foreach (List<int> li in byHeight.Values)
                    {
                        foreach (int coord in li)
                        {
                            result[i] = coord;
                            i++;
                        }
                    }
                }
            }
            indexs = result.ToList();
        }
        public IEnumerable<Tile> RemoveEmptyTiles()
        {
            List<Tile> result = new List<Tile>();
            foreach (Tile t in data.Values)
            {
                if (!t.IsRemoveable) result.Add(t);
            }
            return result;
        }
        public string CompressToString()
        {
            IEnumerable<Tile> result = RemoveEmptyTiles();
            //Sort();
            byte[] preCompress = new byte[result.Count() * 11];
            for (int i = 0; i < result.Count(); i++)
            {
                byte[] tileData = result.ElementAt(i).GetBytes();
                Misc.WriteToArray(preCompress, tileData, i * 11);
            }
            byte[] lzoPack = PackEncoding.EncodeToPack(preCompress, PackType.IsoMapPack);
            return Convert.ToBase64String(lzoPack);
        }
        #region Enumerator
        public IEnumerator<Tile> GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }
        #endregion
        #endregion


        #region Public Calls - TileLayer
        public Tile this[I2dLocateable src]
        {
            get
            {
                int coord = src.Coord;
                if (data.Keys.Contains(coord)) return data[coord];
                return null;
            }
            set
            {
                data[src.Coord] = value;
            }
        }
        public Tile this[int x, int y]
        {
            get
            {
                int coord = Misc.CoordInt(x, y);
                if (data.Keys.Contains(coord)) return data[coord];
                return null;
            }
            set
            {
                data[Misc.CoordInt(x, y)] = value;
            }
        }
        public Tile this[int coord]
        {
            get
            {
                if (data.Keys.Contains(coord)) return data[coord];
                return null;
            }
            set
            {
                data[coord] = value;
            }
        }
        public Dictionary<int, Tile> Data
        {
            get { return data; }
        }
        public byte BottomLevel
        {
            get { return bottomLevel; }
        }
        #endregion
    }


    public class Tile : I3dLocateable
    {
        private int tileIndex;
        private List<IMapObject> objectsOnTile = new List<IMapObject>();
        private bool isSelfHidden = false, isExtraHidden = false;
        private bool isFramework, isFlat;
        private bool marked = false;
        private int originalHeight;


        #region Ctor - Tile
        public Tile(short _x, short _y, int _TileIndex, byte _TileSubIndex,  byte _Level, byte _IceGrowth)
        {
            X16 = _x;
            Y16 = _y;
            tileIndex = _TileIndex;
            SubIndex = _TileSubIndex;
            Height = _Level;
            IceGrowth = _IceGrowth;
        }
        public Tile(int tileindex, byte subindex, int x, int y, int z)
        {
            TileIndex = tileindex;
            SubIndex = subindex;
            X = x;
            Y = y;
            Height = (byte)z;
        }
        public Tile(Tile src)
        {
            TileIndex = src.TileIndex;
            SubIndex = src.SubIndex;
            X = src.X;
            Y = src.Y;
            Height = src.Height;
            originalHeight = src.originalHeight;
            isFlat = src.isFlat;
            isFramework = src.isFramework;
            Selected = src.Selected;
            marked = src.marked;
            isSelfHidden = src.isSelfHidden;
            isExtraHidden = src.isExtraHidden;
        }
        #endregion


        #region Private Methods - Tile
        private void MoveTileObjectsTo(I2dLocateable pos, int height)
        {
            foreach (IMapObject obj in objectsOnTile)
            {
                if (obj is StructureItem bud && bud.Coord != Coord) continue;
                if (obj is SmudgeItem smg && smg.Coord != Coord) continue;
                obj.MoveTo(new Pnt3(pos, height));
            }
        }
        private void MoveTileTo(I2dLocateable cell, int height, bool isFlatMode = false)
        {
            SceneObject.MoveTo(new Pnt3(cell, height));
            X = cell.X;
            Y = cell.Y;
            if (height != Height && !isFlatMode)
            {
                Height = (byte)height;
                if (IsHyte)
                {
                    SceneObject.Dispose();
                    Engine.DrawGeneralItem(this);
                }
                if (isFramework) SwitchToFramework(true);
            }
            if (Selected)
            {
                if (!isSelfHidden) SceneObject.MarkSelf(Vec4.Selector);
                if (!isExtraHidden) SceneObject.MarkExtra(Vec4.Selector);
            }
        }
        #endregion


        #region Public Methods - Tile
        public void Redraw()
        {
            Vec4 color = Vec4.Zero;
            if (SceneObject != null) color = SceneObject.ColorVector;
            SceneObject?.Dispose();
            Engine.DrawGeneralItem(this);
            if (color != Vec4.Zero) SceneObject.SetColor(color);
            if (isFlat)
            {
                isFlat = false;
                FlatToGround(true);
            }
            if (isFramework)
            {
                isFramework = false;
                SwitchToFramework(true);
            }
            if (Selected)
            {
                if (!isSelfHidden) SceneObject.MarkSelf(Vec4.Selector);
                if (!isExtraHidden) SceneObject.MarkExtra(Vec4.Selector);
            }
        }
        public void Rise()
        {
            if (Height < Constant.DrawingEngine.MapMaxHeight)
            {
                MoveTileTo(this, Height + 1);
                MoveTileObjectsTo(this, Height);
            }
        }
        public void Sink()
        {
            if (Height > 0)
            {
                MoveTileTo(this, Height - 1);
                MoveTileObjectsTo(this, Height);
            }
        }
        public void Mark(bool marked)
        {
            this.marked = marked;
            if (!Selected)
            {
                if (marked)
                {
                    if (!isSelfHidden) SceneObject.MarkSelf(Vec4.TileIndicator);
                    if (!isExtraHidden) SceneObject.MarkExtra(Vec4.TileExIndi);
                }
                else
                {
                    if (!isSelfHidden) SceneObject.MarkSelf(Vec4.Zero, true);
                    if (!isExtraHidden) SceneObject.MarkExtra(Vec4.Zero, true);
                }
            }
        }
        public void SwitchToFramework(bool enable)
        {
            isFramework = enable;
            SceneObject.SwitchToFramework(enable);
            if (isSelfHidden) SceneObject.HideSelf();
            if (isExtraHidden) SceneObject.HideExtra();
            if (Selected)
            {
                if (!isSelfHidden) SceneObject.MarkSelf(Vec4.Selector);
                if (!isExtraHidden) SceneObject.MarkExtra(Vec4.Selector);
            }
        }
        public void FlatToGround(bool enable)
        {
            if (enable && !isFlat)
            {
                originalHeight = Height;
                I3dLocateable pos = new Pnt3(this, 0);
                MoveTileObjectsTo(this, 0);
                MoveTileTo(this, 0, true);
                Height = 0;
                HideExtraImg();
                isFlat = true;
            }
            else if (!enable && isFlat)
            {
                I3dLocateable pos = new Pnt3(this, originalHeight);
                MoveTileObjectsTo(this, originalHeight);
                MoveTileTo(this, originalHeight, true);
                Height = (byte)originalHeight;
                RevealExtraImg();
                isFlat = false;
            }
        }
        public void ReplaceWith(Tile newtile)
        {
            foreach (IMapObject obj in objectsOnTile)
            {
                if (obj is StructureItem bud)
                {
                    if (obj.Coord == newtile.Coord) obj.MoveTo(newtile); 
                }
                else if (obj is SmudgeItem smg)
                {
                    if (obj.Coord == newtile.Coord) obj.MoveTo(newtile);
                }
                else obj.MoveTo(newtile);
                newtile.AddObject(obj);
            }
        }
        public void Dispose()
        {
            SceneObject.Dispose();
        }
        public byte[] GetBytes()
        {
            byte[] result = new byte[11];
            Misc.WriteToArray(result, BitConverter.GetBytes(X), 0);
            Misc.WriteToArray(result, BitConverter.GetBytes(Y), 2);
            Misc.WriteToArray(result, BitConverter.GetBytes(tileIndex), 4);
            result[8] = SubIndex;
            result[9] = Height;
            result[10] = IceGrowth;
            List<char> cs = new List<char>();
            return result;
        }
        public List<IMapObject> GetObjects()
        {
            return objectsOnTile;
        }
        public void AddObject(IMapObject src)
        {
            objectsOnTile.Add(src);
        }
        /// <summary>
        /// By type and RegName
        /// </summary>
        /// <param name="src"></param>
        public void RemoveObject(IMapObject src)
        {
            int i = 0;
            for (; i< objectsOnTile.Count; i++)
            {
                if (objectsOnTile[i].GetType() == src.GetType() && objectsOnTile[i].RegName == src.RegName)
                {
                    objectsOnTile.RemoveAt(i);
                    break;
                }
            }
        }
        public bool MarkForSimulating(bool waterBound = false)
        {
            bool b = waterBound ? WaterBuildable : Buildable;
            if (b) SceneObject.MarkForBuildable(Vec4.BuildableTile);
            else SceneObject.MarkForBuildable(Vec4.UnBuildableTile);
            return b;
        }
        public void UnMarkForSimulating()
        {
            SceneObject.UnMarkForBuildable();
        }
        public void HideTileImg()
        {
            if (!isSelfHidden)
            {
                SceneObject.HideSelf();
                isSelfHidden = true;
            }
        }
        public void HideExtraImg()
        {
            if (!isExtraHidden)
            {
                SceneObject.HideExtra();
                isExtraHidden = true;
            }
        }
        public void RevealAllTileImg()
        {
            if (isSelfHidden)
            {
                SceneObject.RevealSelf();
                isSelfHidden = false;
            }
            RevealExtraImg();
        }
        public void RevealExtraImg()
        {
            if (isExtraHidden)
            {
                SceneObject.RevealExtra();
                isExtraHidden = false;
            }
        }
        public void MoveTo(Tile dest, int height, int alig)
        {
            if (SceneObject != null)
            {
                if (isFlat)
                {
                    originalHeight = alig + height;
                    height = 0;
                }
                MoveTileTo(dest, height);
            }
        }
        public void Select()
        {
            if (!Selected && SceneObject != null)
            {
                if (!isSelfHidden) SceneObject.MarkSelf(Vec4.Selector);
                if (!isExtraHidden) SceneObject.MarkExtra(Vec4.Selector);
                Selected = true;
            }
        }
        public void DeSelect()
        {
            if (Selected && SceneObject != null)
            {
                if (!isSelfHidden) SceneObject.MarkSelf(Vec4.Zero, true);
                if (!isExtraHidden) SceneObject.MarkExtra(Vec4.Zero, true);
                Selected = false;
            }
        }
        #endregion


        #region Public Calls - Tile
        public dynamic[] Attributes
        {
            get { return new dynamic[] { X16, Y16, tileIndex, SubIndex, Height, IceGrowth }; }
        }
        public bool IsDefault
        {
            get { return (tileIndex == 65535 || tileIndex == 0) && Height == 0 && SubIndex == 0; }
        }
        public bool IsRemoveable
        {
            get { return IsDefault && IceGrowth == 0; }
        }
        public static Tile EmptyTile
        {
            get { return new Tile(0, 0, 65535, 0, 0, 0); }
        }
        public static Tile EmptyTileAt(int x, int y)
        {
            return new Tile((short)x, (short)y, 0, 0, 0, 0);
        }
        public short X16 { get; set; }
        public short Y16 { get; set; }
        public int X { get { return X16; } set { X16 = (short)value; } }
        public int Y { get { return Y16; } set { Y16 = (short)value; } }
        public int Z { get { return Height; } set { Height = (byte)value; } }
        public bool Buildable
        {
            get
            {
                if (!SceneObject.Buildable) return false;
                foreach (IMapObject obj in objectsOnTile)
                {
                    Type t = obj.GetType();
                    if (t == typeof(UnitItem) ||
                        t == typeof(StructureItem) ||
                        t == typeof(InfantryItem) ||
                        t == typeof(AircraftItem) ||
                        t == typeof(OverlayUnit) ||
                        t == typeof(TerrainItem)) return false;
                }
                return true;
            }
        }
        public bool WaterBuildable
        {
            get
            {
                if (!SceneObject.WaterPassable) return false;
                foreach (IMapObject obj in objectsOnTile)
                {
                    Type t = obj.GetType();
                    if (t == typeof(UnitItem) ||
                        t == typeof(StructureItem) ||
                        t == typeof(InfantryItem) ||
                        t == typeof(AircraftItem) ||
                        t == typeof(OverlayUnit) ||
                        t == typeof(TerrainItem)) return false;
                }
                return true;
            }
        }
        public bool Passable
        {
            get
            {
                if (!(SceneObject.LandPassable || SceneObject.WaterPassable)) return false;
                bool result = true;
                foreach (IMapObject obj in objectsOnTile)
                {
                    Type t = obj.GetType();
                    if (t == typeof(StructureItem)) return false;
                    if (t == typeof(OverlayUnit))
                    {
                        PresentMisc p = (obj as OverlayUnit).SceneObject;
                        result = result && (p.IsTiberiumOverlay || p.IsRubble) && !p.IsMoveBlockingOverlay;
                    }
                }
                return result;
            }
        }
        public byte Height { get; set; }
        public int RealHeight { get { return originalHeight == 0 ? Height : originalHeight; } }
        public int TileIndex
        {
            get
            {
                if (tileIndex == -1) return 0;
                else return tileIndex;
            }
            set
            {
                if (value == -1) tileIndex = 0;
                else tileIndex = value;
            }
        }
        public byte SubIndex { get; set; }
        public byte IceGrowth { get; set; }
        public int Coord { get { return Misc.CoordInt(X, Y); } }
        public int ObjectCount { get { return objectsOnTile.Count; } }
        public PresentTile SceneObject { get; set; }
        public bool Selected { get; set; }
        public bool IsHyte { get; set; }
        public int TileTerrainType { get; set; }
        #endregion
    }
}
