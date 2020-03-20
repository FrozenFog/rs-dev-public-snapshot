using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.MapStructure.Points
{
    public class SmudgeLayer : PointCollectionBase<SmudgeItem>
    {
        public SmudgeLayer() { }
    }


    public class SmudgeItem : PointItemBase
    {
        public SmudgeItem(string _name, int x, int y, bool _ignore) : base(x, y)
        {
            Name = _name;
            IgnoreSmudge = _ignore;
        }


        #region Public Calls - SmudgeItem
        public string Name { get; set; }
        public bool IgnoreSmudge { get; set; }
        #endregion
    }
}
