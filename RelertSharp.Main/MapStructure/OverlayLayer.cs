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
using RelertSharp.DrawingEngine.Presenting;

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
        public void RemoveByCoord(I2dLocateable pos)
        {
            int coord = (pos.Y << 9) + pos.X;
            data.Remove(coord);
        }
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
                int coord = (y << 9) + x;
                if (data.Keys.Contains(coord)) return data[coord];
                return null;
            }
            set
            {
                if (x + y > 512) throw new RSException.OverlayOutOfIndexException(x.ToString(), y.ToString());
                data[(y << 9) + x] = value;
            }
        }
    #endregion
    }


    public class OverlayUnit : IMapObject, IRegistable
    {



        #region Ctor - OverlayUnit
        public OverlayUnit(int _coord, byte _overlayIndex, byte _frameIndex)
        {
            X = Misc.CoordByteX(_coord);
            Y = Misc.CoordByteY(_coord);
            Index = _overlayIndex;
            Frame = _frameIndex;
        }
        public OverlayUnit(byte index, byte frame)
        {
            Index = index;
            Frame = frame;
            X = -1000;
            Y = -1000;
        }
        public OverlayUnit(OverlayUnit src)
        {
            X = src.X;
            Y = src.Y;
            Index = src.Index;
            Frame = src.Frame;
        }
        #endregion


        #region Public Methods - OverlayUnit
        public void MoveTo(I3dLocateable pos)
        {
            X = pos.X;
            Y = pos.Y;
            SceneObject.MoveTo(pos);
        }
        public void ShiftBy(I3dLocateable delta)
        {
            X += delta.X;
            Y += delta.Y;
            SceneObject.ShiftBy(delta);
        }

        public void Select()
        {
            if (!Selected)
            {
                Selected = true;
                SceneObject.MarkSelected();
            }
        }

        public void UnSelect()
        {
            if (Selected)
            {
                Selected = false;
                SceneObject.Unmark();
            }
        }
        public void Dispose()
        {
            Selected = false;
            SceneObject.Dispose();
        }
        public IMapObject CopyNew()
        {
            OverlayUnit o = new OverlayUnit(this);
            return o;
        }
        public void Hide()
        {
            if (!IsHidden)
            {
                SceneObject.Hide();
                IsHidden = true;
            }
        }
        public void Reveal()
        {
            if (IsHidden)
            {
                SceneObject.Reveal();
                IsHidden = false;
            }
        }
        #endregion


        #region Public Calls - OverlayUnit
        public bool IsHidden { get; private set; }
        public byte Index { get; set; }
        public byte Frame { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Coord { get { return Misc.CoordInt(X, Y); } }
        public bool Selected { get; set; }
        public string RegName { get; set; }
        #endregion


        #region Drawing
        public PresentMisc SceneObject { get; set; }
        IPresentBase IMapScenePresentable.SceneObject { get { return SceneObject; } set { SceneObject = (PresentMisc)value; } }
        #endregion
    }
}
