using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    public class WaypointItem : PointItemBase
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
        #endregion
    }
}
