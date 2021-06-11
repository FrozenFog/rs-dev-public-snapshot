using RelertSharp.Common;

namespace RelertSharp.MapStructure.Points
{
    public class CellTagCollection : PointCollectionBase<CellTagItem>
    {
        public CellTagCollection() { }
    }


    public class CellTagItem : PointItemBase, IMapObject
    {
        public CellTagItem(string _coord, string _tagID) : base(_coord)
        {
            TagID = _tagID;
            ObjectType = MapObjectType.Celltag;
        }
        public CellTagItem(CellTagItem src) : base(src)
        {
            TagID = src.TagID;
            ObjectType = MapObjectType.Celltag;
        }
        public CellTagItem(string tagid)
        {
            TagID = tagid;
            ObjectType = MapObjectType.Celltag;
        }
        public CellTagItem(I2dLocateable pos, string tagID) : base(pos)
        {
            TagID = TagID;
            ObjectType = MapObjectType.Celltag;
        }
        internal CellTagItem()
        {
            ObjectType = MapObjectType.Celltag;
        }


        #region Public Methods
        public override void ApplyConfig(IMapObjectBrushConfig config, IObjectBrushFilter filter, bool applyPosAndName = false)
        {
            base.ApplyConfig(config, filter, applyPosAndName);
            if (filter.Tag) TagID = config.AttatchedTag;
        }
        public override string ToString()
        {
            return string.Format("{0}, {1} - {2}", X, Y, TagID);
        }
        #endregion


        #region Public Calls - CellTagItem
        public string TagID { get; set; }
        #endregion
    }
}
