using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using RelertSharp.Encoding;
using RelertSharp.Common;
using RelertSharp.Utils;
using System.Collections;

namespace RelertSharp.MapStructure
{
    public class OverlayLayer : IEnumerable<OverlayUnit>
    {
        private Dictionary<int, OverlayUnit> data = new Dictionary<int, OverlayUnit>();


        #region Ctor - OverlayLayer
        public OverlayLayer(string _overlayPackString, string _overlayDataPackString)
        {
            byte[] _ovlOut = new byte[262144];
            byte[] _ovldOut = new byte[262144];
            byte[] _frombase64O = Convert.FromBase64String(_overlayPackString);
            byte[] _frombase64D = Convert.FromBase64String(_overlayDataPackString);
            _ovlOut = PackEncoding.DecodePack(_frombase64O, 0, PackType.OverlayPack);
            _ovldOut = PackEncoding.DecodePack(_frombase64D, 0, PackType.OverlayPack);
            for (int i = 0; i < 262144; i++)
            {
                if (_ovlOut[i] != 0xFF) data[i] = new OverlayUnit(i, _ovlOut[i], _ovldOut[i]);
            }
        }
        #endregion


        #region Public Methods - OverlayLayer
        public string CompressIndex()
        {
            byte[] preCompress = new byte[262144];
            for (int i = 0; i < 262144; i++)
            {
                if (data.Keys.Contains(i)) preCompress[i] = data[i].Index;
                else preCompress[i] = 0xFF;
            }
            byte[] format80Pack = PackEncoding.EncodeToPack(preCompress, PackType.OverlayPack);
            return Convert.ToBase64String(format80Pack);
        }
        public string CompressFrame()
        {
            byte[] preCompress = new byte[262144];
            for (int i = 0; i < 262144; i++)
            {
                if (data.Keys.Contains(i)) preCompress[i] = data[i].Frame;
                else preCompress[i] = 0x00;
            }
            byte[] format80Pack = PackEncoding.EncodeToPack(preCompress, PackType.OverlayPack);
            return Convert.ToBase64String(format80Pack);
        }
        #region Enumerator
        public IEnumerator<OverlayUnit> GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.Values.GetEnumerator();
        }
        #endregion
        #endregion


        #region Public Calls - OverlayLayer
        public OverlayUnit this[int x, int y]
        {
            get
            {
                int coord = y * 512 + x;
                if (data.Keys.Contains(coord)) return data[coord];
                return null;
            }
            set
            {
                if (x + y > 512) throw new RSException.OverlayOutOfIndexException(x.ToString(), y.ToString());
                data[y * 512 + x] = value;
            }
        }
        public Dictionary<int, OverlayUnit> Data { get { return data; } }
    #endregion
    }


    public class OverlayUnit : ILocateable
    {



        #region Ctor - OverlayUnit
        public OverlayUnit(int _coord, byte _overlayIndex, byte _frameIndex)
        {
            X = Misc.CoordByteX(_coord);
            Y = Misc.CoordByteY(_coord);
            Index = _overlayIndex;
            Frame = _frameIndex;
        }
        #endregion


        #region Public Calls - OverlayUnit
        public byte Index { get; set; }
        public byte Frame { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public float fX { get { return X; } }
        public float fY { get { return Y; } }
        public float fZ { get; set; }
        public int Coord { get { return Misc.CoordInt(X, Y); } }
        #endregion
    }
}
