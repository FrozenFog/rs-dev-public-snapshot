using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.DrawingEngine.Drawables;

namespace RelertSharp.DrawingEngine
{
    internal partial class BufferCollection
    {
        public class CBuffer
        {
            #region Ctor - Buffer
            public CBuffer() { }
            #endregion


            #region Public Calls - CBuffer
            public int Celltag { get; set; }
            public int WaypointBase { get; set; }
            public Dictionary<string, DrawableStructure> Structures { get; private set; } = new Dictionary<string, DrawableStructure>();
            public Dictionary<string, DrawableUnit> Units { get; private set; } = new Dictionary<string, DrawableUnit>();
            public Dictionary<string, DrawableInfantry> Infantries { get; private set; } = new Dictionary<string, DrawableInfantry>();
            public Dictionary<string, DrawableMisc> Miscs { get; private set; } = new Dictionary<string, DrawableMisc>();
            public Dictionary<string, DrawableTile> Tiles { get; private set; } = new Dictionary<string, DrawableTile>();
            #endregion
        }
    }
}
