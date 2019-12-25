using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static relert_sharp.Utils.Misc;

namespace relert_sharp.MapStructure.Points
{
    public class PointCollectionBase
    {
        private Dictionary<string, PointItemBase> data = new Dictionary<string, PointItemBase>();
        public PointCollectionBase() { }


        #region Public Calls - PointCollectionBase
        public PointItemBase this[int x, int y]
        {
            get
            {
                string coord = CoordString(x, y);
                if (data.Keys.Contains(coord)) return data[coord];
                return new PointItemBase();
            }
            set
            {
                data[CoordString(x, y)] = value;
            }
        }
        public PointItemBase this[string coord]
        {
            get
            {
                if (data.Keys.Contains(coord)) return data[coord];
                return new PointItemBase();
            }
            set
            {
                data[coord] = value;
            }
        }
        #endregion
    }



    public class PointItemBase
    {
        public PointItemBase() { }
        public PointItemBase(string _coord)
        {
            Coord = _coord;
        }


        #region Public Calls - PointItemBase
        public string Coord { get; set; }
        public int CoordX { get { return CoordByteX(int.Parse(Coord)); } }
        public int CoordY { get { return CoordByteY(int.Parse(Coord)); } }
        #endregion;
    }
}
