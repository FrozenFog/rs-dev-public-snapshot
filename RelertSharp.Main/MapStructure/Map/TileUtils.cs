using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.MapStructure
{
    public partial class Map
    {
        public void SwitchToFramework(bool enable)
        {
            foreach (Tile t in TilesData)
            {
                t.SwitchToFramework(enable);
            }
            Engine.Refresh();
        }
        public void SwitchFlatGround(bool enable)
        {
            foreach (Tile t in TilesData)
            {
                t.FlatToGround(enable);
            }
            Engine.Refresh();
        }
    }
}
