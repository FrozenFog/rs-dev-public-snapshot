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


    public class CellTagItem : PointItemBase, IMapMiscObject
    {
        public CellTagItem(string _coord, string _tagID) : base(_coord)
        {
            TagID = _tagID;
        }


        #region Public Calls - CellTagItem
        public string TagID { get; set; }
        public new PresentMisc SceneObject { get { return (PresentMisc)base.SceneObject; } set { base.SceneObject = value; } }
        IPresentBase IMapScenePresentable.SceneObject { get { return base.SceneObject; } set { base.SceneObject = value; } }
        #endregion
    }
}
