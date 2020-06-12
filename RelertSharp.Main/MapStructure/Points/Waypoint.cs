using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.DrawingEngine.Presenting;

namespace RelertSharp.MapStructure.Points
{
    public class WaypointCollection : PointCollectionBase<WaypointItem>
    {
        public WaypointCollection() { }

        
        public WaypointItem FindByID(string id)
        {
            foreach (WaypointItem item in this)
            {
                if (item.Num == id) return item;
            }
            return null;
        }
    }


    public class WaypointItem : PointItemBase, IMapMiscObject
    {
        public WaypointItem(string _coord, string _index) : base(_coord)
        {
            Num = _index;
        }


        #region Public Calls - WaypointItem
        public override string ToString()
        {
            return Num + " - " + "(" + X + "," + Y + ")";
        }
        public string Num { get; set; }
        public new PresentMisc SceneObject { get { return (PresentMisc)base.SceneObject; } set { base.SceneObject = value; } }
        IPresentBase IMapScenePresentable.SceneObject { get { return base.SceneObject; } set { base.SceneObject = value; } }
        #endregion
    }
}
