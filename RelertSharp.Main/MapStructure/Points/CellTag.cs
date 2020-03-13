using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.MapStructure.Points
{
    public class CellTagCollection : PointCollectionBase
    {
        public CellTagCollection() { }
    }


    public class CellTagItem : PointItemBase
    {
        public CellTagItem(string _coord, string _tagID) : base(_coord)
        {
            TagID = _tagID;
        }


        #region Public Calls - CellTagItem
        public string TagID { get; set; }
        #endregion
    }
}
