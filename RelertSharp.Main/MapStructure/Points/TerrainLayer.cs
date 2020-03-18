using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.MapStructure.Points
{
    public class TerrainLayer : PointCollectionBase
    {
        public TerrainLayer() { }
    }


    public class TerrainItem : PointItemBase
    {
        public TerrainItem(string _coord, string _name) : base(_coord)
        {
            TerrainName = _name;
        }


        #region Public Calls - TerrainItem
        public string TerrainName { get; set; }
        #endregion
    }
}
