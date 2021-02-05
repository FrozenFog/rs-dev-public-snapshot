using RelertSharp.Common;
using RelertSharp.MapStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.Engine
{
    internal static partial class EngineMain
    {
        internal static float _30SQ2, _10SQ3, _15SQ2, _rad45;
        internal static string[] _zeroLandType = new string[] { "Water", "Clear", "" };
        internal static float _width { get { return _30SQ2; } }
        internal static Vec3 _NormTileVec { get { return new Vec3(_15SQ2, _15SQ2, _10SQ3); } }
        internal static Map Map { get { return CurrentMapDocument.Map; } }
        internal static float _height { get { return _10SQ3; } }
        internal const uint _colorIgnore = 0x000000FF;
        internal const uint _white = 0xFFFFFFFF;
        internal static Vec3 _generalOffset = new Vec3() { X = 1, Y = 1, Z = (float)Math.Sqrt(2f / 3f) };
        internal static int pPalIso = 0;
        internal static int pPalUnit = 0;
        internal static int pPalTheater = 0;
        internal static int pPalSystem = 0;
        internal static BufferCollection Buffer = new BufferCollection();



        internal static void InitializeEngine()
        {
            Log.Write("Engine Begin");
            _30SQ2 = (float)(30F * Math.Sqrt(2));
            _10SQ3 = (float)(10F * Math.Sqrt(3));
            _15SQ2 = _30SQ2 / 2;
            _rad45 = (float)Math.PI / 4;
            Log.Write("Minimap Begin");
        }
    }
}
