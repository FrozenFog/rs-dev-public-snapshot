using RelertSharp.Common;
using RelertSharp.Encoding;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;
using RelertSharp.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.MapStructure
{
    public class TileLayer : IEnumerable<Tile>
    {
        private Dictionary<int, Tile> data = new Dictionary<int, Tile>();
        private List<int> indexs = new List<int>();
        private byte bottomLevel = 255;
        private bool isFramework, isFlat;


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
        public TileLayer(Rectangle size)
        {

        }
        #endregion


        #region Private Methods - TileLayer
        private void LayTileWeb(int xmin, int ymax, int width, int height, int altitude = 0)
        {
            for (int i = 0; i < height; i++)
            {
                int x = xmin;
                int y = ymax;
                for (int j = 0; j < width; j++)
                {
                    if (this[x, y] == null) this[x, y] = Tile.EmptyTileAt(x, y, altitude);
                    x++;
                    y--;
                }
                xmin++;
                ymax++;
            }
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
        #region Tile finding
        public IEnumerable<Tile> GetNeighbor(I2dLocateable src)
        {
            List<Tile> result = new List<Tile>();
            Pnt pos = new Pnt(src);
            if (this[pos + Pnt.OneX] is Tile dr) result.Add(dr);
            if (this[pos - Pnt.OneX] is Tile ul) result.Add(ul);
            if (this[pos + Pnt.OneY] is Tile dl) result.Add(dl);
            if (this[pos - Pnt.OneY] is Tile ur) result.Add(ur);
            return result;
        }
        /// <summary>
        /// The direction returned is from result to referance center tile
        /// </summary>
        /// <param name="src"></param>
        /// <param name="directions"></param>
        /// <returns></returns>
        public List<Tile> GetNeighbor(I2dLocateable src, out List<WallDirection> directions)
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
        public List<Tile> GetDiagonalTile(I2dLocateable src, out List<WallDirection> directions)
        {
            List<Tile> result = new List<Tile>();
            directions = new List<WallDirection>();
            Pnt pos = new Pnt(src);
            if (this[pos + Pnt.One] is Tile down)
            {
                result.Add(down);
                directions.Add(WallDirection.CornerOfSouth);
            }
            if (this[pos - Pnt.One] is Tile up)
            {
                result.Add(up);
                directions.Add(WallDirection.CornerOfNorth);
            }
            if (this[pos + Pnt.Diag] is Tile right)
            {
                result.Add(right);
                directions.Add(WallDirection.CornerOfEast);
            }
            if (this[pos - Pnt.Diag] is Tile left)
            {
                result.Add(left);
                directions.Add(WallDirection.CornerOfWest);
            }
            return result;
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
        public bool HasTileOn(Vec3 pos, out Tile t)
        {
            t = this[pos.ToCoord()];
            if (t != null) return t.Z == pos.Z;
            return false;
        }
        #endregion
        #region Add / remove objects
        public void AddObjectOnTile(IMapObject src)
        {
            this[src]?.AddObject(src);
        }
        public void AddObjectOnTile(I2dLocateable pos, IMapObject src)
        {
            this[pos]?.AddObject(src);
        }
        public void RemoveObjectOnTile(IMapObject obj, I2dLocateable pos)
        {
            this[pos]?.RemoveObject(obj);
        }
        public void RemoveObjectOnTile(IMapObject obj)
        {
            this[obj]?.RemoveObject(obj);
        }
        #endregion
        #region Saving io
        public void FixEmptyTiles(int width, int height, int altitude = 0)
        {
            LayTileWeb(1, width, width, height, altitude);
            LayTileWeb(2, width, width - 1, height, altitude);
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
        #endregion
        #region Framework & Flat Ground
        internal void SwitchFramework(bool enable)
        {
            isFramework = enable;
            foreach (Tile t in this)
            {
                t.SwitchToFramework(enable);
            }
        }
        internal void SwitchFlatGround(bool enable)
        {
            isFlat = enable;
            foreach (Tile t in this) t.FlatToGround(enable);
        }
        #endregion
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
        public bool IsFrameworkOn { get { return isFramework; } }
        public bool IsFlatGroundOn { get { return isFlat; } }
        #endregion
    }


    public class Tile : BaseVisibleObject<ISceneTile>, I3dLocateable, ITile, IChecksum
    {
        private int tileIndex;
        private List<IMapObject> objectsOnTile = new List<IMapObject>();
        private bool isSelfHidden = false, isExtraHidden = false;
        private bool isFramework, isFlat;
        private bool marked = false;
        private int originalHeight;


        #region Ctor - Tile
        public Tile(short _x, short _y, int _TileIndex, byte _TileSubIndex, byte _Level, byte _IceGrowth)
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
            isSelected = src.isSelected;
            marked = src.marked;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="height"></param>
        /// <param name="isFlatMode">used by flat mode toggle</param>
        private void MoveTileTo(I2dLocateable cell, int height)
        {
            SceneObject.MoveTo(new Pnt3(cell, height));
            X = cell.X;
            Y = cell.Y;
            SetHeightTo(height);
            //if (isSelected)
            //{
            //    if (!isSelfHidden) SceneObject.MarkSelf(Vec4.Selector);
            //    if (!isExtraHidden) SceneObject.MarkExtra(Vec4.Selector);
            //}
        }
        private void HideExtraImg()
        {
            if (Disposed) return;
            SceneObject.HideExtra();
            isExtraHidden = true;
        }
        private void RevealExtraImg()
        {
            if (Disposed) return;
            SceneObject.RevealExtra();
            isExtraHidden = false;
        }
        #endregion


        #region Public Methods - Tile
        public static Tile CopyAsReferance(Tile src)
        {
            return new Tile(src.TileIndex, src.SubIndex, src.X, src.Y, src.Z);
        }
        public void Redraw()
        {
            Vec4 color = Vec4.Zero;
            if (SceneObject != null) color = SceneObject.ActualColor;
            SceneObject?.Dispose();
            SceneObject?.RedrawTile(this);
            if (color != Vec4.Zero) SceneObject.SetColor(color);
            if (isFlat)
            {
                FlatToGround(false);
                FlatToGround(true);
            }
            if (isFramework)
            {
                SwitchToFramework(false);
                SwitchToFramework(true);
            }
            if (isSelected)
            {
                CancelSelection();
                Select();
            }
            if (isPhased)
            {
                UnPhase();
                PhaseOut();
            }
        }
        public void SetHeightTo(int height, bool isFlatMode = false, bool moveObject = false)
        {
            if (height >= 0 && height <= Constant.DrawingEngine.MapMaxHeight)
            {
                if (isFlatMode)
                {
                    if (isFlat)
                    {
                        originalHeight = Height;
                        Height = (byte)height;
                    }
                    else
                    {
                        Height = (byte)originalHeight;
                        originalHeight = 0;
                    }
                    SceneObject?.MoveTo(new Pnt3(this, Height));
                    if (moveObject) MoveTileObjectsTo(this, Height);
                }
                else
                {
                    if (isFlat)
                    {
                        originalHeight = height;
                        SceneObject?.MoveTo(new Pnt3(this, 0));
                    }
                    else
                    {
                        bool changed = height != Height;
                        Height = (byte)height;
                        if (changed)
                        {
                            if (IsHyte)
                            {
                                SceneObject?.Dispose();
                                Redraw();
                                if (isFramework) SwitchToFramework(true);
                                if (moveObject) MoveTileObjectsTo(this, Height);
                            }
                            else
                            {
                                SceneObject?.MoveTo(new Pnt3(this, Height));
                                if (moveObject) MoveTileObjectsTo(this, Height);
                            }
                        }
                    }
                }
            }
        }
        public void Rise()
        {
            if (Height <= Constant.DrawingEngine.MapMaxHeight)
            {
                SetHeightTo(RealHeight + 1);
                if (!isFlat) MoveTileObjectsTo(this, Height);
            }
        }
        public void Sink()
        {
            if (RealHeight > 0)
            {
                SetHeightTo(RealHeight - 1);
                if (!isFlat) MoveTileObjectsTo(this, Height);
            }
        }
        public void Mark(bool marked)
        {
            if (Disposed) return;
            this.marked = marked;
            if (!isSelected && CanSelect)
            {
                if (marked)
                {
                    if (!isSelfHidden) SceneObject.MarkSelf(Vec4.TileIndicator);
                    if (!isExtraHidden) SceneObject.MarkExtra(Vec4.TileExIndi);
                }
                else
                {
                    if (!isSelfHidden) SceneObject.MarkSelf(SceneObject.ColorVector, true);
                    if (!isExtraHidden) SceneObject.MarkExtra(SceneObject.ColorVector, true);
                }
            }
        }
        public override void Hide()
        {
            if (!isSelfHidden)
            {
                SceneObject?.HideSelf();
                SceneObject?.HideExtra();
                isSelfHidden = true;
                status |= ObjectStatus.Hide;
            }
        }
        public override void Reveal()
        {
            if (isSelfHidden)
            {
                SceneObject?.RevealSelf();
                if (!isExtraHidden) SceneObject?.RevealExtra();
                isSelfHidden = false;
                status &= ~ObjectStatus.Hide;
            }
        }
        public void SwitchToFramework(bool enable)
        {
            isFramework = enable;
            SceneObject.SwitchToFramework(enable);
            if (isFlat)
            {
                FlatToGround(false);
                FlatToGround(true);
            }
        }
        public void FlatToGround(bool enable)
        {
            if (enable && !isFlat)
            {
                isFlat = true;
                MoveTileObjectsTo(this, 0);
                SetHeightTo(0, true);
                HideExtraImg();
            }
            else if (!enable && isFlat)
            {
                MoveTileObjectsTo(this, originalHeight);
                SetHeightTo(originalHeight, true);
                RevealExtraImg();
                isFlat = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="subcell"></param>
        public override void MoveTo(I3dLocateable pos, int subcell = -1)
        {
            X = pos.X;
            Y = pos.Y;
            MoveTileTo(pos, pos.Z);
        }
        /// <summary>
        /// do nothing for tile
        /// </summary>
        /// <param name="delta"></param>
        public override void ShiftBy(I3dLocateable delta)
        {
            
        }
        public byte[] GetBytes()
        {
            byte[] result = new byte[11];
            Misc.WriteToArray(result, BitConverter.GetBytes(X16), 0);
            Misc.WriteToArray(result, BitConverter.GetBytes(Y16), 2);
            Misc.WriteToArray(result, BitConverter.GetBytes(tileIndex), 4);
            result[8] = SubIndex;
            result[9] = (byte)RealHeight;
            result[10] = IceGrowth;
            return result;
        }
        public List<IMapObject> GetObjects()
        {
            return objectsOnTile;
        }
        public IMapObject GetObejct(Predicate<IMapObject> predicate)
        {
            foreach (IMapObject obj in objectsOnTile) if (predicate(obj)) return obj;
            return null;
        }
        public void AddObject(IMapObject src)
        {
            if (src.ObjectType == MapObjectType.Overlay && objectsOnTile.Any(x => x.ObjectType == MapObjectType.Overlay))
            {
                OverlayUnit o = objectsOnTile.First(x => x.ObjectType == MapObjectType.Overlay) as OverlayUnit;
                o.Dispose();
                bool b = objectsOnTile.Remove(o);
            }
            objectsOnTile.Add(src);
        }
        /// <summary>
        /// By type and RegName
        /// </summary>
        /// <param name="src"></param>
        public void RemoveObject(IMapObject src)
        {
            if (src is ICombatObject com)
            {
                RemoveObject(com);
                return;
            }
            int i = 0;
            for (; i < objectsOnTile.Count; i++)
            {
                if (objectsOnTile[i].GetType() == src.GetType() && objectsOnTile[i].RegName == src.RegName)
                {
                    objectsOnTile.RemoveAt(i);
                    break;
                }
            }
        }
        public void RemoveObject(ICombatObject src)
        {
            int i = 0;
            for (; i < objectsOnTile.Count; i++)
            {
                if (objectsOnTile[i] is ICombatObject target)
                {
                    if (target.RegName == src.RegName && target.Id == src.Id)
                    {
                        objectsOnTile.RemoveAt(i);
                        break;
                    }
                }
            }
        }
        public bool MarkForSimulating(bool waterBound = false)
        {
            bool b = waterBound ? WaterBuildable : Buildable;
            if (b) SceneObject.ApplyTempColor(Vec4.BuildableTile);
            else SceneObject.ApplyTempColor(Vec4.UnBuildableTile);
            return b;
        }
        public void UnMarkForSimulating()
        {
            SceneObject.RemoveTempColor();
        }
        public override int GetHeight(Map source = null)
        {
            return Z;
        }
        public int GetChecksum()
        {
            unchecked
            {
                int hash = Constant.BASE_HASH;
                hash = hash * 53 + X;
                hash = hash * 53 + Y;
                hash = hash * 53 + RealHeight;
                hash = hash * 53 + TileIndex;
                hash = hash * 53 + SubIndex;
                hash = hash * 53 + IceGrowth;
                return hash;
            }
        }
        #endregion


        #region Internal
        internal void ReplaceWithNewTileConfig(IMapTileBrushConfig config)
        {
            X = config.Pos.X;
            Y = config.Pos.Y;
            Z = config.Pos.Z;
            TileIndex = config.TileIndex;
            SubIndex = config.TileSubIndex;
            IceGrowth = config.IceGrowth;
        }
        internal void SetTileTo(int tileIndex, byte subindex)
        {
            TileIndex = tileIndex;
            SubIndex = subindex;
        }
        #endregion


        #region Public Calls - Tile
        public dynamic[] Attributes
        {
            get { return new dynamic[] { X16, Y16, tileIndex, SubIndex, Height, IceGrowth }; }
        }
        public bool IsDefault
        {
            get { return IsEmptyTile && RealHeight == 0; }
        }
        public bool IsZeroTile
        {
            get { return IsDefault && IceGrowth == 0; }
        }
        public bool IsRemoveable
        {
            get { return IsDefault && IceGrowth == 0; }
        }
        public override bool CanSelect
        {
            get { return !isPhased; }
        }
        public static Tile EmptyTileAt(int x, int y, int height = 0)
        {
            return new Tile((short)x, (short)y, 0, 0, (byte)height, 0);
        }
        public static Tile EmptyTile => new Tile(0, 0, 0, 0, 0);
        public short X16 { get; set; }
        public short Y16 { get; set; }
        public override int X { get { return X16; } set { X16 = (short)value; } }
        public override int Y { get { return Y16; } set { Y16 = (short)value; } }
        public int Z { get { return Height; } set { Height = (byte)value; } }
        public bool IsRamp
        {
            get
            {
                return SceneObject?.RampType != 0;
            }
        }
        public byte RampType
        {
            get { return SceneObject?.RampType ?? 0; }
        }
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
                        ISceneOverlay p = (obj as OverlayUnit).SceneObject;
                        result = result && (p.IsTiberiumOverlay || p.IsRubble) && !p.IsMoveBlockingOverlay;
                    }
                }
                return result;
            }
        }
        public byte Height { get; internal set; }
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
        public override int Coord { get { return Misc.CoordInt(X, Y); } }
        public int MinimapRenderableObjectCount { get { return objectsOnTile.Where(x => (x.ObjectType & MapObjectType.MinimapRenderable) != 0).Count(); } }
        public bool IsHyte { get; set; }
        public int TileTerrainType { get; set; }
        public bool Disposed { get; private set; }
        public bool IsLeagalTile { get; set; }
        public bool IsFrozen { get { return isPhased; } }
        public bool IsEmptyTile { get { return (tileIndex == 65535 || tileIndex == 0) && SubIndex == 0; } }
        #endregion
    }
}
