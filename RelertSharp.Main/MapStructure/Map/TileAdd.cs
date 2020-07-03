using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;

namespace RelertSharp.MapStructure
{
    public partial class Map
    {
        public void AddTile(Tile t)
        {
            if (Tiles[t] is Tile org)
            {
                org.Dispose();
                org.ReplaceWith(t);
                Tiles[t] = t;
            }
        }
    }
}
