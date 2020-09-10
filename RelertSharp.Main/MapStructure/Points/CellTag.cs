using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.DrawingEngine.Presenting;

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


        #region Public Methods
        public override string ToString()
        {
            return string.Format("{0}, {1} - {2}", X, Y, TagID);
        }
        #endregion


        #region Public Calls - CellTagItem
        public string TagID { get; set; }
        public new PresentMisc SceneObject { get { return (PresentMisc)base.SceneObject; } set { base.SceneObject = value; } }
        IPresentBase IMapScenePresentable.SceneObject { get { return base.SceneObject; } set { base.SceneObject = value; } }
        #endregion
    }
}
