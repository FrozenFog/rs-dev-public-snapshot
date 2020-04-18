using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;

namespace RelertSharp.MapStructure.Points
{
    public class SmudgeLayer : PointCollectionBase<SmudgeItem>
    {
        public SmudgeLayer() { }
    }


    public class SmudgeItem : PointItemBase, IMapObject
    {
        public SmudgeItem(string _name, int x, int y, bool _ignore) : base(x, y)
        {
            RegName = _name;
            IgnoreSmudge = _ignore;
        }


        #region Public Calls - SmudgeItem
        public string RegName { get; set; }
        public bool IgnoreSmudge { get; set; }
        #endregion
    }
}
