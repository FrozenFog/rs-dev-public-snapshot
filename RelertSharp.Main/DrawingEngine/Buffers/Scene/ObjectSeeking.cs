using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.DrawingEngine.Presenting;
using RelertSharp.Common;

namespace RelertSharp.DrawingEngine
{
    internal partial class BufferCollection
    {
        public partial class CScene
        {
            private T GetObjectByCoord<T>(I2dLocateable cell) where T : PresentBase, IPresentBase
            {
                int coord = cell.Coord;
                if (Tiles.Keys.Contains(coord))
                {
                    return Tiles[coord].GetFirstTileObject<T>();
                }
                return null;
            }
            private T GetObjectByCoord<T>(I2dLocateable cell, Predicate<T> predicate) where T : PresentBase, IPresentBase
            {
                int coord = cell.Coord;
                if (Tiles.Keys.Contains(coord))
                {
                    return Tiles[coord].GetFirstTileObject(predicate);
                }
                return null;
            }
            
        }
    }
}
