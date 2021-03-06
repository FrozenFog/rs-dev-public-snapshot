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
        internal static float _30SQ2, _10SQ3, _15SQ2, _rad45;
        private static string[] _zeroLandType = new string[] { "Water", "Clear", "" };
        private static float _width { get { return _30SQ2; } }
        private static Vec3 _NormTileVec { get { return new Vec3(_15SQ2, _15SQ2, _10SQ3); } }
        private static float _height { get { return _10SQ3; } }
        private const uint _colorIgnore = 0x000000FF;
        private const uint _white = 0xFFFFFFFF;
        internal static Vec3 _generalOffset = new Vec3() { X = 1, Y = 1, Z = (float)Math.Sqrt(2f / 3f) };
        private static int pPalIso = 0;
        private static int pPalUnit = 0;
        private static int pPalTheater = 0;
        private static int pPalSystem = 0;
        private static BufferCollection Buffer = new BufferCollection();
        internal static IntPtr Handle;



        internal static void EngineCtor(int width, int height)
        {
            Log.Info("Engine Begin");
            _30SQ2 = (float)(30F * Math.Sqrt(2));
            _10SQ3 = (float)(10F * Math.Sqrt(3));
            _15SQ2 = _30SQ2 / 2;
            _rad45 = (float)Math.PI / 4;
            Log.Info("Minimap Begin");
            minimap = new GdipSurface();
            Log.Info("Setup Scene...");
            CppExtern.Scene.SetUpScene(width, height);
            //Handle = CppExtern.Scene.SetSceneSize(1, 1);
            Log.Info("Setup complete");
            Log.Info("Initializing Waypoint");
            InitWaypointNum();
            //Log.Write("Resetting Viewport...");
            //CppExtern.Scene.ResetSceneView();
            //Log.Write("Resetting complete");


            Log.Asterisk("Engine initialized.");
        }
        internal static bool ResetMiniMap(Rectangle mapsize, Size panelSize, double scaleFactor = 1)
        {
            return minimap.SetupMinimap(panelSize, mapsize, scaleFactor);
        }
        internal static void SetTheater(TheaterType type)
        {
            //TileDictionary = new MapTheaterTileSet(type);
            CurrentTheater = type;
            VFileInfo pal = GetPtrFromGlobalDir(string.Format("iso{0}.pal", TileDictionary.TheaterSub));
            VFileInfo upal = GetPtrFromGlobalDir(string.Format("unit{0}.pal", TileDictionary.TheaterSub));
            VFileInfo thpal = GetPtrFromGlobalDir(string.Format("{0}.pal", GlobalConfig.GetTheaterPalName(type)));
            VFileInfo syspal = GetPtrFromGlobalDir("rs.pal");
            pPalSystem = CppExtern.Files.CreatePaletteFromFileInBuffer(syspal.ptr);
            if (pPalIso != 0) CppExtern.Files.RemovePalette(pPalIso);
            if (pPalUnit != 0) CppExtern.Files.RemovePalette(pPalUnit);
            if (pPalTheater != 0) CppExtern.Files.RemovePalette(pPalTheater);
            pPalIso = CppExtern.Files.CreatePaletteFromFileInBuffer(pal.ptr);
            pPalUnit = CppExtern.Files.CreatePaletteFromFileInBuffer(upal.ptr);
            pPalTheater = CppExtern.Files.CreatePaletteFromFileInBuffer(thpal.ptr);

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
