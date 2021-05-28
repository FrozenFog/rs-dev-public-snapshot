using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;

namespace RelertSharp.Engine
{
    public delegate void MapDrawingProgressEventHandler(int max, int progress, MapObjectType type);
    public delegate void MouseOnTileChangedEventHandler(I3dLocateable posNow, I3dLocateable posPrev);
    public delegate void MouseOnSubtileChangedEventHandler(int subtileNow, int subtilePrev);
    public delegate void I3dLocateableHandler(I3dLocateable pos);
    public delegate void I2dLocateableHandler(I2dLocateable pos);
}
