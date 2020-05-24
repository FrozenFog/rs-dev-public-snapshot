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
        public void RemoveEmptyTiles()
        {
            int[] keys = data.Keys.ToArray();
            foreach (int key in keys)
            {
                if (data[key].IsRemoveable)
                {
                    data.Remove(key);
                    indexs.Remove(key);
                }
            }
        }
        public string CompressToString()
        {
            Sort();
            byte[] preCompress = new byte[indexs.Count * 11];
            for (int i = 0; i < indexs.Count; i++)
            {
                byte[] tileData = data[indexs[i]].GetBytes();
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
        #endregion


        #region Public Methods - Tile
        public byte[] GetBytes()
        {
            byte[] result = new byte[11];
            Misc.WriteToArray(result, BitConverter.GetBytes(X), 0);
            Misc.WriteToArray(result, BitConverter.GetBytes(Y), 2);
            Misc.WriteToArray(result, BitConverter.GetBytes(tileIndex), 4);
            result[8] = SubIndex;
            result[9] = Height;
            result[10] = IceGrowth;
            return result;
        }
        public IEnumerable<IMapObject> GetObjects()
        {
            return objectsOnTile;
        }
        public void AddObject(IMapObject src)
        {
            objectsOnTile.Add(src);
        }
        public void RemoveObject(IMapObject src)
        {
            objectsOnTile.Remove(src);
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
        public byte Height { get; set; }
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
        #endregion
    }
}
