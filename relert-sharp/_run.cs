using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Reflection;
using relert_sharp.FileSystem;
using relert_sharp.MapStructure;
using System.IO;
using OpenGL;

namespace relert_sharp
{
    class _run
    {
        public static void M()
        {
            MapFile m = new MapFile(@"a21.mpr");
            m.SaveMap();
        }
    }
}
