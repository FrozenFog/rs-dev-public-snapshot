using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Points;
using RelertSharp.Common;
using static RelertSharp.Utils.Misc;

namespace RelertSharp.DrawingEngine
{
    public class Engine
    {
        private readonly float _30SQ2, _10SQ3, _15SQ2;
        private const uint _colorIgnore = 0x000000FF;
        private Dictionary<int, int> tilesData = new Dictionary<int, int>();
        private Dictionary<int, int> overlayData = new Dictionary<int, int>();
        private Dictionary<int, int> smudgeData = new Dictionary<int, int>();
        private Dictionary<int, int> terrainData = new Dictionary<int, int>();
        private Dictionary<string, int> tmpBuffer = new Dictionary<string, int>();
        private Dictionary<string, int> shpbuffer = new Dictionary<string, int>();
        private Dictionary<string, int> vxlBuffer = new Dictionary<string, int>();
        private int pPalIso = 0;
        private int pPalUnit = 0;
        private int pPalTheater = 0;


        #region Ctor - Engine
        public Engine()
        {
            _30SQ2 = (float)(30F * Math.Sqrt(2));
            _10SQ3 = (float)(10F * Math.Sqrt(3));
            _15SQ2 = _30SQ2 / 2;
        }
        #endregion


        #region Public Methods - Engine
        public bool Initialize(IntPtr ptr)
        {
            return CppExtern.Scene.SetUpScene(ptr) && CppExtern.Scene.ResetSceneView();
        }
        #region Draw
        public bool DrawTile(Tile t)
        {
            string name = GlobalVar.TileDictionary[t.TileIndex];
            int id = 0;
            if (!tmpBuffer.Keys.Contains(name))
            {
                VFileInfo info = GlobalVar.GlobalDir.GetFilePtr(name);
                id = CppExtern.Files.CreateTmpFileFromFilenMemory(info.ptr, info.size);
                CppExtern.Files.LoadTmpTextures(id, pPalIso);
                tmpBuffer[name] = id;
            }
            else id = tmpBuffer[name];
            Vec3 pos = ToVec3Iso(t);
            int o1 = 0, o2 = 0;
            if (CppExtern.ObjectUtils.CreateTmpObjectAtScene(id, pos, t.SubIndex, ref o1, ref o2))
            {
                tilesData[t.Coord] = id;
                return true;
            }
            return false;
        }
        public bool DrawShp(TerrainItem terrain, int height)
        {
            string terrname = GlobalVar.TileDictionary.NameAsTheater(terrain.TerrainName);
            int pal = pPalIso;
            if (ParseBool(GlobalVar.GlobalRules[terrain.TerrainName]["SpawnsTiberium"]))
                pal = pPalUnit;
            if (!GlobalVar.GlobalDir.HasFile(terrname)) return false;
            int id = 0;
            if (!shpbuffer.Keys.Contains(terrname))
            {
                VFileInfo info = GlobalVar.GlobalDir.GetFilePtr(terrname);
                id = CppExtern.Files.CreateShpFileFromFileInMemory(info.ptr, info.size);
                CppExtern.Files.LoadShpTextures(id, pal, _colorIgnore);
                shpbuffer[terrname] = id;
            }
            else id = shpbuffer[terrname];
            Vec3 pos = ToVec3Iso(terrain.CoordX, terrain.CoordY, height);
            if (CppExtern.ObjectUtils.CreateShpObjectAtScene(id, pos, 0, pal, _colorIgnore, false) != 0)
            {
                terrainData[terrain.CoordInt] = id;
                return true;
            }
            return false;
        }
        public bool DrawShp(SmudgeItem smg, int height)
        {
            string smgname = GlobalVar.TileDictionary.NameAsTheater(smg.Name);
            if (!GlobalVar.GlobalDir.HasFile(smgname)) return false;
            int id = 0;
            if (!shpbuffer.Keys.Contains(smgname))
            {
                VFileInfo info = GlobalVar.GlobalDir.GetFilePtr(smgname);
                id = CppExtern.Files.CreateShpFileFromFileInMemory(info.ptr, info.size);
                CppExtern.Files.LoadShpTextures(id, pPalIso, _colorIgnore);
                shpbuffer[smgname] = id;
            }
            else id = shpbuffer[smgname];
            Vec3 pos = ToVec3Zero(smg.CoordX, smg.CoordY, height);
            if (CppExtern.ObjectUtils.CreateShpObjectAtScene(id, pos, 0, pPalIso, _colorIgnore, true) != 0)
            {
                smudgeData[smg.CoordInt] = id;
                return true;
            }
            return false;
        }
        public bool DrawShp(OverlayUnit o, int height)
        {
            string ovname = GlobalVar.GlobalRules.GetOverlayName(o.Index);
            int pal = pPalUnit;
            bool flat = ParseBool(GlobalVar.GlobalRules[ovname]["DrawFlat"], true);
            bool isTiberium = ParseBool(GlobalVar.GlobalRules[ovname]["Tiberium"]);
            if (GlobalVar.GlobalDir.HasFile(ovname + ".shp")) ovname = ovname.ToLower() + ".shp";
            else
            {
                ovname = string.Format("{0}.{1}", ovname.ToLower(), GlobalVar.TileDictionary.TheaterSub);
                if (isTiberium) pal = pPalTheater;
                else pal = pPalIso;
                if (!GlobalVar.GlobalDir.HasFile(ovname)) return false;
            }
            int id = 0;
            if (!shpbuffer.Keys.Contains(ovname))
            {
                VFileInfo info = GlobalVar.GlobalDir.GetFilePtr(ovname);
                id = CppExtern.Files.CreateShpFileFromFileInMemory(info.ptr, info.size);
                CppExtern.Files.LoadShpTextures(id, pal, _colorIgnore);
                shpbuffer[ovname] = id;
            }
            else id = shpbuffer[ovname];
            Vec3 pos = ToVec3Zero(o.X, o.Y, height);
            if (CppExtern.ObjectUtils.CreateShpObjectAtScene(id, pos, o.Frame, pal, _colorIgnore, flat) != 0)
            {
                overlayData[o.Coord] = id;
                return true;
            }
            return false;
        }
        #endregion
        public void SetTheater(TheaterType type)
        {
            GlobalVar.TileDictionary = new MapTheaterTileSet(type);
            VFileInfo pal = GlobalVar.GlobalDir.GetFilePtr(string.Format("iso{0}.pal", GlobalVar.TileDictionary.TheaterSub));
            VFileInfo upal = GlobalVar.GlobalDir.GetFilePtr(string.Format("unit{0}.pal", GlobalVar.TileDictionary.TheaterSub));
            VFileInfo thpal = GlobalVar.GlobalDir.GetFilePtr(string.Format("{0}.pal", GlobalVar.GlobalConfig.GetTheaterPalName(type)));
            if (pPalIso != 0) CppExtern.Files.RemovePalette(pPalIso);
            if (pPalUnit != 0) CppExtern.Files.RemovePalette(pPalUnit);
            if (pPalTheater != 0) CppExtern.Files.RemovePalette(pPalTheater);
            pPalIso = CppExtern.Files.CreatePaletteFromFileInBuffer(pal.ptr);
            pPalUnit = CppExtern.Files.CreatePaletteFromFileInBuffer(upal.ptr);
            pPalTheater = CppExtern.Files.CreatePaletteFromFileInBuffer(thpal.ptr);
        }
        public void Refresh()
        {
            CppExtern.Scene.PresentAllObject();
        }
        public void MoveTo(int x, int y, int z = 0)
        {
            CppExtern.Scene.MoveFocusOnScene(ToVec3Iso(x, y, z));
        }
        public void ResetView()
        {
            CppExtern.Scene.ResetSceneView();
            Refresh();
        }
        public void ViewShift(Point delta)
        {
            CppExtern.Scene.MoveFocusOnScreen(delta.X, delta.Y);
            Refresh();
        }
        public void SetBackgroundColor(Color c)
        {
            CppExtern.Scene.SetBackgroundColor(c.R, c.G, c.B);
        }
        #endregion


        #region Private Methods - Engine
        private Vec3 ToVec3Iso(ILocateable loc)
        {
            return new Vec3() { X = loc.fX * _30SQ2, Y = loc.fY * _30SQ2, Z = loc.fZ * _10SQ3 };
        }
        private Vec3 ToVec3Iso(int x, int y, int z)
        {
            return new Vec3() { X = x * _30SQ2, Y = y * _30SQ2, Z = z * _10SQ3 };
        }
        private Vec3 ToVec3Zero(int x, int y, int z)
        {
            return new Vec3() { X = x * _30SQ2 - _15SQ2, Y = y * _30SQ2 - _15SQ2, Z = z * _10SQ3 };
        }
        #endregion
    }
}
