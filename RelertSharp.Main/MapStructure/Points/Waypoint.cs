using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace relert_sharp.MapStructure.Points
{
    public class WaypointCollection : PointCollectionBase
    {
        public WaypointCollection() { }
    }


    public class WaypointItem : PointItemBase
    {
        public WaypointItem(string _coord, string _index)
        {
            Num = _index;
        }


        #region Public Calls - WaypointItem
        public string Num { get; set; }
        #endregion
    }
}
