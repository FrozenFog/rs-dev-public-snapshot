using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using RelertSharp.Common;

namespace RelertSharp.DrawingEngine.Presenting
{
    public class PresentTile : PresentBase, IPresentBase
    {
        #region Ctor - PresentTile
        internal PresentTile(int pself, int pextra, byte height, Drawables.DrawableTile tile, int subtile, I2dLocateable pos)
        {
            pSelf = pself;
            pExtra = pextra;
            Height = height;
            X = pos.X;
            Y = pos.Y;
            WaterPassable = tile[subtile].WaterPassable;
            Buildable = tile[subtile].Buildable;
            LandPassable = tile[subtile].LandPassable;
        }
        #endregion


        #region Public Methods - PresentTile
        public void Dispose()
        {
            if (!Disposed)
            {
                RemoveProp(pSelf);
                RemoveProp(pExtra);
                Disposed = true;
            }
        }
        /// <summary>
        /// Move tile will do nothing
        /// </summary>
        /// <param name="cell"></param>
        public override void MoveTo(I3dLocateable cell)
        {

        }
        /// <summary>
        /// Shift tile will do nothing
        /// </summary>
        /// <param name="delta"></param>
        public override void ShiftBy(I3dLocateable delta)
        {
            
        }
        public void Mark(Vec4 main, Vec4 extra, bool deSelect)
        {
            if (deSelect)
            {
                main = ColorVector;
                extra = ColorVector;
            }
            SetColor(pSelf, main);
            SetColor(pExtra, extra);
        }
        public void SetColor(Vec4 color)
        {
            ColorVector = color;
            if (!selected)
            {
                SetColorStrict(color);
            }
        }
        public void MultiplyColor(Vec4 color)
        {
            ColorVector *= color;
            SetColor(ColorVector);
        }
        public void DivColor(Vec4 color)
        {
            ColorVector /= color;
        }
        public void AddColor(Vec4 color)
        {
            ColorVector += color;
            SetColor(ColorVector);
        }
        public void MarkSelected()
        {
            SetColorStrict(Vec4.Selector);
            selected = true;
        }
        public void Unmark()
        {
            selected = false;
            SetColorStrict(ColorVector);
        }
        #endregion


        #region Private Methods - PresentTile
        private void SetColorStrict(Vec4 color)
        {
            SetColor(pSelf, color);
            SetColor(pExtra, color);
        }
        #endregion


        #region Public Calls - PresentTile
        public byte Height { get; set; }
        public int pExtra { get; set; }
        public bool IsValid { get { return pSelf != 0 || pExtra != 0; } }
        public bool Lamped { get; set; }
        public bool Buildable { get; set; }
        public bool WaterPassable { get; set; }
        public bool LandPassable { get; set; }
        #endregion
    }
}
