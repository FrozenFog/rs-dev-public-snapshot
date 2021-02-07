using RelertSharp.Common;
using RelertSharp.MapStructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.Engine
{
    internal static partial class EngineMain
    {
        private static float _30SQ2, _10SQ3, _15SQ2, _rad45;
        private static string[] _zeroLandType = new string[] { "Water", "Clear", "" };
        private static float _width { get { return _30SQ2; } }
        private static Vec3 _NormTileVec { get { return new Vec3(_15SQ2, _15SQ2, _10SQ3); } }
        private static Map Map { get { return CurrentMapDocument.Map; } }
        private static float _height { get { return _10SQ3; } }
        private const uint _colorIgnore = 0x000000FF;
        private const uint _white = 0xFFFFFFFF;
        private static Vec3 _generalOffset = new Vec3() { X = 1, Y = 1, Z = (float)Math.Sqrt(2f / 3f) };
        private static int pPalIso = 0;
        private static int pPalUnit = 0;
        private static int pPalTheater = 0;
        private static int pPalSystem = 0;
        private static BufferCollection Buffer = new BufferCollection();



        internal static void EngineCtor(IntPtr hwndMain)
        {
            Log.Write("Engine Begin");
            _30SQ2 = (float)(30F * Math.Sqrt(2));
            _10SQ3 = (float)(10F * Math.Sqrt(3));
            _15SQ2 = _30SQ2 / 2;
            _rad45 = (float)Math.PI / 4;
            Log.Write("Minimap Begin");
            minimap = new GdipSurface();
            Log.Write("Initializing Waypoint");
            InitWaypointNum();
            Log.Write("Setup Scene...");
            CppExtern.Scene.SetUpScene(hwndMain);
            Log.Write("Setup complete");
            Log.Write("Resetting Viewport...");
            CppExtern.Scene.ResetSceneView();
            Log.Write("Resetting complete");


            Log.Write("Done.");
        }
        internal static bool Initialize(Size panelsize, Rectangle mapsize, TileLayer tileReferace)
        {
            _cellFindingReferance = tileReferace;

            Log.Write("Booting Minimap...");
            if (minimap.SetupMinimap(panelsize, mapsize)) Log.Write("Minimap booted");
            else return false;

            return true;
        }
        internal static void SetTheater(TheaterType type)
        {
            TileDictionary = new MapTheaterTileSet(type);
            CurrentTheater = type;
            VFileInfo pal = GetPtrFromGlobalDir(string.Format("iso{0}.pal", TileDictionary.TheaterSub));
            VFileInfo upal = GetPtrFromGlobalDir(string.Format("unit{0}.pal", TileDictionary.TheaterSub));
            VFileInfo thpal = GetPtrFromGlobalDir(string.Format("{0}.pal", GlobalConfig.GetTheaterPalName(type)));
            VFileInfo syspal = GetPtrFromGlobalDir("rs.pal");
            if (pPalIso != 0) CppExtern.Files.RemovePalette(pPalIso);
            if (pPalUnit != 0) CppExtern.Files.RemovePalette(pPalUnit);
            if (pPalTheater != 0) CppExtern.Files.RemovePalette(pPalTheater);
            pPalIso = CppExtern.Files.CreatePaletteFromFileInBuffer(pal.ptr);
            pPalUnit = CppExtern.Files.CreatePaletteFromFileInBuffer(upal.ptr);
            pPalTheater = CppExtern.Files.CreatePaletteFromFileInBuffer(thpal.ptr);
            pPalSystem = CppExtern.Files.CreatePaletteFromFileInBuffer(syspal.ptr);

            Buffer.Files.CelltagBase = CreateFile("celltag.shp", DrawableType.Shp);
            Buffer.Files.WaypointBase = CreateFile("waypoint.shp", DrawableType.Shp);
            Buffer.Files.LightSourceBase = CreateFile("lightsource.shp", DrawableType.Shp);
        }
        internal static bool InitWaypointNum()
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    int numid = CreateFile("rsnum.shp", DrawableType.Shp, i);
                    Buffer.WaypointNum.Add(numid);
                }
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
