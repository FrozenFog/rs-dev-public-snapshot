using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using relert_sharp.Encoding;
using System.Collections;
using relert_sharp.Utils;

namespace relert_sharp.MapStructure
{
    public class TileLayer
    {
        private Dictionary<int, Tile> data = new Dictionary<int, Tile>();
        private List<int> indexs = new List<int>();
        private byte bottomLevel = 255;
        public TileLayer(string stringPack, Rectangle Size)
        {
            byte[] fromBase64 = Convert.FromBase64String(stringPack);
            int tileNum = (Size.Width * 2 - 1) * Size.Height;
            byte[] tileData = PackEncoding.DecodePack(fromBase64, tileNum);
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


        #region Private Methods - TileLayer

        #endregion


        #region Public Methods - TileLayer
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
                        int height = data[coord].Height;
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
            byte[] lzoPack = PackEncoding.EncodeToPack(preCompress);
            return Convert.ToBase64String(lzoPack);
        }
        #endregion


        #region Public Calls - TileLayer
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
    public class Tile
    {
        private short x, y;
        private int tileIndex;
        private byte subIndex, level, iceGrowth;
        public Tile(short _x, short _y, int _TileIndex, byte _TileSubIndex,  byte _Level, byte _IceGrowth)
        {
            x = _x;
            y = _y;
            tileIndex = _TileIndex;
            subIndex = _TileSubIndex;
            level = _Level;
            iceGrowth = _IceGrowth;
        }
        public byte[] GetBytes()
        {
            byte[] result = new byte[11];
            Misc.WriteToArray(result, BitConverter.GetBytes(x), 0);
            Misc.WriteToArray(result, BitConverter.GetBytes(y), 2);
            Misc.WriteToArray(result, BitConverter.GetBytes(tileIndex), 4);
            result[8] = subIndex;
            result[9] = level;
            result[10] = iceGrowth;
            return result;
        }
        #region Public Calls - Tile
        public dynamic[] Attributes
        {
            get { return new dynamic[] { x, y, tileIndex, subIndex, level, iceGrowth }; }
        }
        public bool IsDefault
        {
            get { return (tileIndex == 65535 || tileIndex == 0) && level == 0 && subIndex == 0; }
        }
        public bool IsRemoveable
        {
            get { return IsDefault && iceGrowth == 0; }
        }
        public static Tile EmptyTile
        {
            get { return new Tile(0, 0, 65535,0 , 0, 0); }
        }
        public short X
        {
            get { return x; }
            set { x = value; }
        }
        public short Y
        {
            get { return y; }
            set { y = value; }
        }
        public int TileIndex
        {
            get { return tileIndex; }
            set { tileIndex = value; }
        }
        public byte SubIndex
        {
            get { return subIndex; }
            set { subIndex = value; }
        }
        public byte Height
        {
            get { return level; }
            set { level = value; }
        }
        public byte IceGrowth
        {
            get { return iceGrowth; }
            set { iceGrowth = value; }
        }
        #endregion
    }
}
