using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;

namespace RelertSharp.MapStructure.Points
{
    public class BaseNode : I2dLocateable
    {
        public BaseNode(string _name, int _x, int _y)
        {
            Name = _name;
            X = _x;
            Y = _y;
        }


        #region Public Calls - BaseNode
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        #endregion
    }
}
