using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;

namespace RelertSharp.DrawingEngine.Presenting
{
    internal class PresentTile : PresentBase, IPresentBase
    {
        #region Ctor - PresentTile
        public PresentTile(int pself, int pextra, byte height, Drawables.DrawableTile tile, int subtile)
        {
            pSelf = pself;
            pExtra = pextra;
            Height = height;
            WaterPassable = tile[subtile].WaterPassable;
            Buildable = tile[subtile].Buildable;
            LandPassable = tile[subtile].LandPassable;
        }
        #endregion


        #region Public Methods - PresentTile
        public void Dispose()
        {
            RemoveProp(pSelf);
            RemoveProp(pExtra);
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
