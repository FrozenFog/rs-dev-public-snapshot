using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;

namespace RelertSharp.MapStructure
{
    public static partial class MapApi
    {
        private static Map Map { get { return GlobalVar.CurrentMapDocument.Map; } }
        private static bool IsValid
        {
            get { return GlobalVar.CurrentMapDocument != null; }
        }
        static MapApi()
        {

        }
    }
}
