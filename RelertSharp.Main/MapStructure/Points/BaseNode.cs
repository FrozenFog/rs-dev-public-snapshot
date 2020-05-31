using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;

namespace RelertSharp.MapStructure.Points
{
    public class BaseNode : IMapObject
    {
        public BaseNode(string _name, int _x, int _y)
        {
            RegName = _name;
            X = _x;
            Y = _y;
        }


        #region Public Methods - BaseNode
        public void MoveTo(I2dLocateable pos)
        {
            X = pos.X;
            Y = pos.Y;
        }
        public void ShiftBy(I2dLocateable delta)
        {
            X += delta.X;
            Y += delta.Y;
        }
        #endregion


        #region Public Calls - BaseNode
        public string RegName { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Coord { get { return Utils.Misc.CoordInt(this); } }
        public bool Selected { get; set; }
        #endregion
    }
}
