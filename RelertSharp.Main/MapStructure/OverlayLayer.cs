using RelertSharp.Common;
using RelertSharp.Encoding;
using RelertSharp.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        public OverlayUnit this[I2dLocateable pos]
        {
            get { return this[pos.X, pos.Y]; }
            set { this[pos.X, pos.Y] = value; }
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
        internal OverlayUnit()
        {

        }
        #endregion


        #region Public Methods - OverlayUnit
        public void MoveTo(I3dLocateable pos, int subcell = -1)
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
                SceneObject.ApplyTempColor(Vec4.Selector);
            }
        }

        public void CancelSelection()
        {
            if (Selected)
            {
                Selected = false;
                SceneObject.RemoveTempColor();
            }
        }
        public void Dispose()
        {
            Selected = false;
            SceneObject.RemoveTempColor();
            SceneObject?.Dispose();
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

        public int GetHeight(Map source = null)
        {
            if (source != null) return source.GetHeightFromTile(this);
            else if (GlobalVar.HasMap) return GlobalVar.GlobalMap.GetHeightFromTile(this);
            else return Constant.MapStructure.INVALID_HEIGHT;
        }

        public void ApplyConfig(IMapObjectBrushConfig config, IObjectBrushFilter filter, bool applyPosAndName = false)
        {
            if (applyPosAndName)
            {
                X = config.Pos.X;
                Y = config.Pos.Y;
                Index = config.OverlayIndex;
                Frame = config.OverlayFrame;
            }
        }
        public string[] ExtractParameter()
        {
            return new string[]
            {
                X.ToString(),
                Y.ToString(),
                Index.ToString(),
                Frame.ToString()
            };
        }
        public IMapObject ConstructFromParameter(string[] command)
        {
            ParameterReader reader = new ParameterReader(command);
            OverlayUnit o = new OverlayUnit()
            {
                X = reader.ReadInt(),
                Y = reader.ReadInt(),
                Index = reader.ReadByte(),
                Frame = reader.ReadByte()
            };
            return o;
        }
        #endregion


        #region Public Calls - OverlayUnit
        /// <summary>
        /// Overlay won't have id, returns null
        /// </summary>
        public string Id { get { return null; } }
        public bool IsHidden { get; private set; }
        public byte Index { get; set; }
        public byte Frame { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Coord { get { return Misc.CoordInt(X, Y); } }
        public bool Selected { get; set; }
        public string RegName { get; private set; }
        public MapObjectType ObjectType { get { return MapObjectType.Overlay; } }
        #endregion


        #region Drawing
        public ISceneOverlay SceneObject { get; set; }

        ISceneObject IMapObject.SceneObject { get { return SceneObject; } }
        #endregion
    }
}
