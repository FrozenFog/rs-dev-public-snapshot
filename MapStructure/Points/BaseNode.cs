using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace relert_sharp.MapStructure.Points
{
    public class BaseNode
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
