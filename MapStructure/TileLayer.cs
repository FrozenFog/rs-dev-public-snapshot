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
        private Dictionary<string, Tile> data = new Dictionary<string, Tile>();
        public TileLayer(string stringPack, Rectangle Size)
        {
            byte[] fromBase64 = Convert.FromBase64String(stringPack);
            int tileNum = (Size.Width * 2 - 1) * Size.Height;
            byte[] tileData = PackEncoding.DecodePack(fromBase64);
            BinaryReader br = new BinaryReader(new MemoryStream(tileData));
            for (; tileNum > 0; tileNum--)
            {
                short x = br.ReadInt16();
                short y = br.ReadInt16();
                int tileIndex = br.ReadInt32();
                byte tileSubIndex = br.ReadByte();
                byte level = br.ReadByte();
                byte iceGrowth = br.ReadByte();
                data[Misc.CoordString(x, y)] = new Tile(x, y, tileIndex, level, iceGrowth);
            }
        }
        #region Public Calls - TileLayer
        public Tile this[int x, int y]
        {
            get
            {
                string coord = Misc.CoordString(x, y);
                if (data.Keys.Contains(coord)) return data[coord];
                return null;
            }
            set { data[Misc.CoordString(x, y)] = value; }
        }
        public Tile this[string coord]
        {
            get
            {
                if (data.Keys.Contains(coord)) return data[coord];
                return null;
            }
            set { data[coord] = value; }
        }
        #endregion
    }
    public class Tile
    {
        private short x, y;
        private int tileIndex;
        private byte level, iceGrowth;
        public Tile(short _x, short _y, int _TileIndex, byte _Level, byte _IceGrowth)
        {
            x = _x;
            y = _y;
            tileIndex = _TileIndex;
            level = _Level;
            iceGrowth = _IceGrowth;
        }
        #region Public Calls - Tile
        public bool IsDefault
        {
            get { return tileIndex == 0 && level == 0; }
        }
        public bool IsRemoveable
        {
            get { return IsDefault && iceGrowth == 0; }
        }
        public static Tile EmptyTile
        {
            get { return new Tile(0, 0, 0, 0, 0); }
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
