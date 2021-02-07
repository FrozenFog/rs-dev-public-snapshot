using RelertSharp.MapStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;

namespace RelertSharp.Engine.MapObjects
{
    internal sealed class MapOverlay : MapMisc, ISceneOverlay
    {
        public MapOverlay(I2dLocateable xy, int z) : base(MapObjectType.Overlay, xy, z)
        {
            
        }

    }
}
