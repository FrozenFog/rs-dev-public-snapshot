using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using RelertSharp.FileSystem;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Points;
using RelertSharp.MapStructure.Objects;
using RelertSharp.DrawingEngine.Drawables;
using RelertSharp.DrawingEngine.Presenting;
using RelertSharp.Common;
using static RelertSharp.Utils.Misc;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.DrawingEngine
{
    public partial class Engine : IDisposable
    {
        private readonly float _30SQ2, _10SQ3, _15SQ2, _rad45;
        private readonly string[] _zeroLandType = new string[] { "Water", "Clear", "" };
        private float _width { get { return _30SQ2; } }
        private Vec3 _NormTileVec { get { return new Vec3(_15SQ2, _15SQ2, _10SQ3); } }
        private Vec4 sceneColor { get; set; }
        private Vec4 sceneObjectColor { get; set; }
        private float _height { get { return _10SQ3; } }
        private const uint _colorIgnore = 0x000000FF;
        private const uint _white = 0xFFFFFFFF;
        private static Vec3 _generalOffset = new Vec3() { X = 1, Y = 1, Z = (float)Math.Sqrt(2f/3f) };
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
            sceneColor = Vec4.Unit4(1);
            Log.Write("Minimap Begin");
            minimap = new GdipSurface();
        }
        #endregion


        #region Public Methods - Engine
        public bool Initialize(IntPtr mainHandle, Size panelsize, Rectangle mapsize, TileLayer tileReferace)
        {
            _cellFindingReferance = tileReferace;
            return CppExtern.Scene.SetUpScene(mainHandle) && CppExtern.Scene.ResetSceneView() && minimap.Initialize(panelsize, mapsize) && InitWaypointNum();
        }
        public void SetTheater(TheaterType type)
        {
            TileDictionary = new MapTheaterTileSet(type);
            CurrentTheater = type;
            VFileInfo pal = GlobalDir.GetFilePtr(string.Format("iso{0}.pal", TileDictionary.TheaterSub));
            VFileInfo upal = GlobalDir.GetFilePtr(string.Format("unit{0}.pal", TileDictionary.TheaterSub));
            VFileInfo thpal = GlobalDir.GetFilePtr(string.Format("{0}.pal", GlobalConfig.GetTheaterPalName(type)));
            VFileInfo syspal = GlobalDir.GetFilePtr("rs.pal");
            if (pPalIso != 0) CppExtern.Files.RemovePalette(pPalIso);
            if (pPalUnit != 0) CppExtern.Files.RemovePalette(pPalUnit);
            if (pPalTheater != 0) CppExtern.Files.RemovePalette(pPalTheater);
            pPalIso = CppExtern.Files.CreatePaletteFromFileInBuffer(pal.ptr);
            pPalUnit = CppExtern.Files.CreatePaletteFromFileInBuffer(upal.ptr);
            pPalTheater = CppExtern.Files.CreatePaletteFromFileInBuffer(thpal.ptr);
            pPalSystem = CppExtern.Files.CreatePaletteFromFileInBuffer(syspal.ptr);

            Buffer.Files.CelltagBase = CreateFile("celltag.shp", DrawableType.Shp);
            Buffer.Files.WaypointBase = CreateFile("waypoint.shp", DrawableType.Shp);
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
            Buffer.RemoveSceneItem(BufferCollection.RemoveFlag.All);
            Buffer.ReleaseFiles();
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
