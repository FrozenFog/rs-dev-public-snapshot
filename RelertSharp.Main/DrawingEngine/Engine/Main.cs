using RelertSharp.Common;
using RelertSharp.MapStructure;
using System;
using System.Drawing;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.DrawingEngine
{
    public partial class Engine : IDisposable
    {
        private readonly float _30SQ2, _10SQ3, _15SQ2, _rad45;
        private readonly string[] _zeroLandType = new string[] { "Water", "Clear", "" };
        private float _width { get { return _30SQ2; } }
        private Vec3 _NormTileVec { get { return new Vec3(_15SQ2, _15SQ2, _10SQ3); } }
        private Map Map { get { return CurrentMapDocument.Map; } }
        private float _height { get { return _10SQ3; } }
        private const uint _colorIgnore = 0x000000FF;
        private const uint _white = 0xFFFFFFFF;
        private static Vec3 _generalOffset = new Vec3() { X = 1, Y = 1, Z = (float)Math.Sqrt(2f / 3f) };
        private BufferCollection Buffer = new BufferCollection();
        private int pPalIso = 0;
        private int pPalUnit = 0;
        private int pPalTheater = 0;
        private int pPalSystem = 0;


        #region Ctor - Engine
        public Engine()
        {
            Log.Write("Engine Begin");
            _30SQ2 = (float)(30F * Math.Sqrt(2));
            _10SQ3 = (float)(10F * Math.Sqrt(3));
            _15SQ2 = _30SQ2 / 2;
            _rad45 = (float)Math.PI / 4;
            Log.Write("Minimap Begin");
            minimap = new GdipSurface();
        }
        #endregion


        #region Public Methods - Engine
        public static bool FirstRun()
        {
            return CppExtern.Scene.SetUpScene(IntPtr.Zero);
        }
        public bool Initialize(IntPtr mainHandle, Size panelsize, Rectangle mapsize, TileLayer tileReferace)
        {
            _cellFindingReferance = tileReferace;

            Log.Write("Setup Scene...");
            if (CppExtern.Scene.SetUpScene(mainHandle)) Log.Write("Setup complete");
            else return false;

            Log.Write("Resetting Viewport...");
            if (CppExtern.Scene.ResetSceneView()) Log.Write("Resetting complete");
            else return false;

            Log.Write("Booting Minimap...");
            if (minimap.Initialize(panelsize, mapsize)) Log.Write("Minimap booted");
            else return false;

            Log.Write("Initializing Waypoint");
            if (InitWaypointNum()) Log.Write("Done.");
            else return false;

            return true;
        }
        public void SetTheater(TheaterType type)
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
        public void Refresh()
        {
            CppExtern.Scene.PresentAllObject();
        }
        public void SetBackgroundColor(Color c)
        {
            CppExtern.Scene.SetBackgroundColor(c.R, c.G, c.B);
        }
        public void Dispose()
        {
            CppExtern.Scene.ResetSceneView();
            CppExtern.Scene.PresentAllObject();
        }
        #endregion


        #region Private Methods - Engine


        #region Etc
        private bool InitWaypointNum()
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
        #endregion
        #endregion


        #region Public Calls - Engine
        #endregion
    }
}
