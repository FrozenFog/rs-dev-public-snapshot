namespace RelertSharp.Engine.DrawableBuffer
{
    internal class DrawableBase
    {
        #region Ctor - DrawableBase
        public DrawableBase() { }
        public DrawableBase(string nameid)
        {
            NameID = nameid;
        }
        #endregion


        #region Public Methods - DrawableBase
        #endregion


        #region Public Calls - DrawableBase
        public string NameID { get; set; }
        public int pSelf { get; set; }
        public int pShadow { get; set; }
        public int pPalCustom { get; set; }
        public uint RemapColor { get; set; }
        public short Framecount { get; set; }
        #endregion
    }


    internal interface IDrawableBase
    {
        string NameID { get; set; }
        int pSelf { get; set; }
        int pShadow { get; set; }
        int pPalCustom { get; set; }
        uint RemapColor { get; set; }
    }
}
