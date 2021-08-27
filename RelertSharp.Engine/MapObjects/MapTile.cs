using RelertSharp.Common;
using RelertSharp.Engine.DrawableBuffer;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Engine.MapObjects
{
    internal sealed class MapTile : MapObjectBase, ISceneTile
    {
        private bool isBodyHidden, isExtraHidden;
        private bool isFramework = false;
        private int Body { get { if (isFramework) return pFramework; else return pSelf; } }
        private int Extra { get { if (isFramework) return pExFramework; else return pExtra; } }


        #region Ctor - MapTile
        internal MapTile(int pself, int pextra, byte height, DrawableTile tile, int subtile, I2dLocateable pos, int frm, int frmex)
        {
            pSelf = pself;
            pExtra = pextra;
            Height = height;
            X = pos.X;
            Y = pos.Y;
            WaterPassable = tile[subtile].WaterPassable;
            Buildable = tile[subtile].Buildable;
            LandPassable = tile[subtile].LandPassable;
            RampType = tile[subtile].RampType;
            pFramework = frm;
            pExFramework = frmex;
            SetColor(pFramework, Vec4.HideCompletely);
            SetColor(pExFramework, Vec4.HideCompletely);
        }
        #endregion


        #region Public Methods - MapTile
        public void SwitchToFramework(bool enable)
        {
            isFramework = enable;
            if (isFramework)
            {
                SetColor(pSelf, Vec4.HideCompletely);
                SetColor(pExtra, Vec4.HideCompletely);
                SetColor(pFramework, ColorVector);
                SetColor(pExFramework, ColorVector);
            }
            else
            {
                SetColor(pSelf, ColorVector);
                SetColor(pExtra, ColorVector);
                SetColor(pFramework, Vec4.HideCompletely);
                SetColor(pExFramework, Vec4.HideCompletely);
            }
        }
        public void Dispose()
        {
            if (!Disposed)
            {
                RemoveProp(PresentFileTypeFlag.Tmp, pSelf);
                RemoveProp(PresentFileTypeFlag.Tmp, pExtra);
                RemoveProp(PresentFileTypeFlag.Tmp, pFramework);
                RemoveProp(PresentFileTypeFlag.Tmp, pExFramework);
                Disposed = true;
            }
        }
        public override void MoveTo(I3dLocateable cell, int subcell = -1)
        {
            Vec3 dest = Vec3.ToVec3Iso(cell) + Transformation.OffsetTo(Constant.DrawingEngine.Offset.Ground);
            SetLocation(dest, pSelf, pExtra);
            SetLocation(dest, pFramework, pExFramework);
            base.MoveTo(cell);
        }
        /// <summary>
        /// Shift tile will do nothing
        /// </summary>
        /// <param name="delta"></param>
        public override void ShiftBy(I3dLocateable delta)
        {

        }
        //public void Mark(Vec4 main, Vec4 extra, bool deSelect)
        //{
        //    if (deSelect)
        //    {
        //        main = ColorVector;
        //        extra = ColorVector;
        //    }
        //    SetColor(Body, main);
        //    SetColor(Extra, extra);
        //}
        /// <summary>
        /// bypass everything
        /// </summary>
        /// <param name="color"></param>
        /// <param name="deSelect"></param>
        public void MarkSelf(Vec4 color, bool deSelect = false)
        {
            if (deSelect) SetColor(Body, ColorVector);
            else SetColor(Body, color);
        }
        /// <summary>
        /// bypass everything
        /// </summary>
        /// <param name="color"></param>
        /// <param name="deSelect"></param>
        public void MarkExtra(Vec4 color, bool deSelect = false)
        {
            if (deSelect) SetColor(Extra, ColorVector);
            else SetColor(Extra, color);
        }
        public void HideSelf()
        {
            isBodyHidden = true;
            SetColor(Body, Vec4.HideCompletely);
        }
        public void HideExtra()
        {
            isExtraHidden = true;
            SetColor(Extra, Vec4.HideCompletely);
        }
        public void RevealSelf()
        {
            isBodyHidden = false;
            SetColor(Body, ColorVector);
        }
        public void RevealExtra()
        {
            isExtraHidden = false;
            SetColor(Extra, ColorVector);
        }
        public override void Hide()
        {
            HideSelf();
            SetColor(Extra, Vec4.HideCompletely);
        }
        public override void Reveal()
        {
            RevealSelf();
            if (!isExtraHidden) SetColor(Extra, ColorVector);
        }
        #endregion


        #region Private Methods - MapTile
        protected override void SetColorStrict(Vec4 color)
        {
            SetColor(Body, color);
            SetColor(Extra, color);
        }

        public void RedrawTile(Tile t)
        {
            EngineMain.DrawTile(t);
        }
        #endregion


        #region Public Calls - MapTile
        public override bool IsHidden 
        { 
            get => isBodyHidden || isExtraHidden;
            protected set { }
        }
        public byte Height { get; set; }
        public int pExtra { get; set; }
        public int pFramework { get; set; }
        public int pExFramework { get; set; }
        public bool IsValid { get { return pSelf != 0 || pExtra != 0; } }
        public bool Lamped { get; set; }
        public bool Buildable { get; set; }
        public bool WaterPassable { get; set; }
        public bool LandPassable { get; set; }
        public byte RampType { get; set; }
        #endregion
    }
}
