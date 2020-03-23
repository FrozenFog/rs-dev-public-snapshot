using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Points;
using RelertSharp.MapStructure.Objects;
using RelertSharp.Common;
using static RelertSharp.Utils.Misc;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.DrawingEngine
{
    public class Engine : IDisposable
    {
        private enum DrawableType { Shp, Tmp, Vxl }
        private readonly float _30SQ2, _10SQ3, _15SQ2;
        private const uint _colorIgnore = 0x000000FF;
        private readonly Vec3 _generalOffset = new Vec3() { X = 1, Y = 1, Z = (float)Math.Sqrt(1.5) };
        private Dictionary<int, int> tilesData = new Dictionary<int, int>();
        private Dictionary<int, int> overlayData = new Dictionary<int, int>();
        private Dictionary<int, int> smudgeData = new Dictionary<int, int>();
        private Dictionary<int, int> terrainData = new Dictionary<int, int>();
        private Dictionary<int, int> infData = new Dictionary<int, int>();
        private Dictionary<int, int> vehData = new Dictionary<int, int>();
        private Dictionary<int, PresentStructure> buildingData = new Dictionary<int, PresentStructure>();
        private Dictionary<int, int> airData = new Dictionary<int, int>();
        private Dictionary<string, int> tmpBuffer = new Dictionary<string, int>();
        private Dictionary<string, int> shpbuffer = new Dictionary<string, int>();
        private Dictionary<string, int> vxlBuffer = new Dictionary<string, int>();
        private Dictionary<string, DrawableStructure> buildingBuffer = new Dictionary<string, DrawableStructure>();
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
            string name = TileDictionary[t.TileIndex];
            int id = 0;
            Vec3 pos = ToVec3Iso(t);
            return Draw(pos, name, pPalIso, tmpBuffer, tilesData, DrawableType.Tmp, t.SubIndex, _colorIgnore, t.Coord, ref id);
        }
        public bool DrawObject(InfantryItem inf, int height)
        {
            string name = GlobalRules.GetObjectImgName(inf);
            int id = 0;
            int frame = GlobalRules.GetFrameFromDirection(inf.Rotation, inf.NameID);
            Vec3 pos = ToVec3Iso(inf.X, inf.Y, height);
            if (GlobalRules.IsVxl(inf.NameID))
            {
                return Draw(pos, name, pPalUnit, vxlBuffer, infData, DrawableType.Vxl, 0, _colorIgnore, inf.CoordInt, ref id);
            }
            else
            {
                return Draw(pos, name, pPalUnit, shpbuffer, infData, DrawableType.Shp, frame, _colorIgnore, inf.CoordInt, ref id);
            }
        }
        public bool DrawObject(StructureItem structure, int height)
        {
            DrawableStructure ds = CreateDrawableStructure(structure.NameID, _colorIgnore);
            PresentStructure ps = new PresentStructure(structure, height);
            Vec3 pos = ToVec3Zero(ps.X, ps.Y, ps.Z);
            return DrawStructure(ds, _colorIgnore, pos, ps);
        }
        public bool DrawShp(TerrainItem terrain, int height)
        {
            string terrname = TileDictionary.NameAsTheater(terrain.TerrainName);
            Vec3 pos = ToVec3Iso(terrain.CoordX, terrain.CoordY, height);
            int pal = pPalIso;
            int id = 0;
            if (ParseBool(GlobalRules[terrain.TerrainName]["SpawnsTiberium"])) pal = pPalUnit;
            return Draw(pos, terrname, pal, shpbuffer, terrainData, DrawableType.Shp, 0, _colorIgnore, terrain.CoordInt, ref id, false);
        }
        public bool DrawShp(SmudgeItem smg, int height)
        {
            int id = 0;
            string smgname = TileDictionary.NameAsTheater(smg.Name);
            Vec3 pos = ToVec3Zero(smg.CoordX, smg.CoordY, height);
            return Draw(pos, smgname, pPalIso, shpbuffer, smudgeData, DrawableType.Shp, 0, _colorIgnore, smg.CoordInt, ref id, true);
        }
        public bool DrawShp(OverlayUnit o, int height)
        {
            string ovname = GlobalRules.GetOverlayName(o.Index);
            int pal = pPalUnit;
            Vec3 pos = ToVec3Zero(o.X, o.Y, height);
            bool flat = ParseBool(GlobalRules[ovname]["DrawFlat"], true);
            bool isTiberium = ParseBool(GlobalRules[ovname]["Tiberium"]);
            if (GlobalDir.HasFile(ovname + ".shp")) ovname = ovname.ToLower() + ".shp";
            else
            {
                ovname = string.Format("{0}.{1}", ovname.ToLower(), TileDictionary.TheaterSub);
                if (isTiberium) pal = pPalTheater;
                else pal = pPalIso;
            }
            int id = 0;
            return Draw(pos, ovname, pal, shpbuffer, overlayData, DrawableType.Shp, o.Frame, _colorIgnore, o.Coord, ref id, flat);
        }
        #endregion
        public void SetTheater(TheaterType type)
        {
            TileDictionary = new MapTheaterTileSet(type);
            CurrentTheater = type;
            VFileInfo pal = GlobalDir.GetFilePtr(string.Format("iso{0}.pal", TileDictionary.TheaterSub));
            VFileInfo upal = GlobalDir.GetFilePtr(string.Format("unit{0}.pal", TileDictionary.TheaterSub));
            VFileInfo thpal = GlobalDir.GetFilePtr(string.Format("{0}.pal", GlobalConfig.GetTheaterPalName(type)));
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
        public void Dispose()
        {
            foreach (int id in tmpBuffer.Values)
            {
                CppExtern.Files.RemoveTmpFile(id);
                CppExtern.ObjectUtils.RemoveObjectAtScene(id);
            }
            foreach (int id in shpbuffer.Values)
            {
                CppExtern.Files.RemoveShpFile(id);
                CppExtern.ObjectUtils.RemoveObjectAtScene(id);
            }
            foreach (int id in vxlBuffer.Values)
            {
                CppExtern.Files.RemoveVxlFile(id);
                CppExtern.ObjectUtils.RemoveObjectAtScene(id);
            }
            tmpBuffer.Clear();
            shpbuffer.Clear();
            vxlBuffer.Clear();
            overlayData.Clear();
            smudgeData.Clear();
            terrainData.Clear();
            tilesData.Clear();
            CppExtern.Scene.ResetSceneView();
            CppExtern.Scene.PresentAllObject();
        }
        #endregion


        #region Private Methods - Engine
        #region Create File
        private int CreateTmp(string name)
        {
            int id = 0;
            if (!tmpBuffer.Keys.Contains(name))
            {
                id = CreateFile(name, DrawableType.Tmp, pPalIso, 0);
            }
            else id = tmpBuffer[name];
            return id;
        }
        private int CreateShp(string name, int pal, uint color)
        {
            int id = 0;
            if (!shpbuffer.Keys.Contains(name))
            {
                id = CreateFile(name, DrawableType.Shp, pal, color);
            }
            else id = shpbuffer[name];
            return id;
        }
        private int CreateVxl(string name, int pal, uint color)
        {
            int id = 0;
            if (!vxlBuffer.Keys.Contains(name))
            {
                id = CreateFile(name, DrawableType.Vxl, pal, color);
            }
            else id = vxlBuffer[name];
            return id;
        }
        private DrawableStructure CreateDrawableStructure(string name, uint color)
        {
            DrawableStructure ds;
            if (!buildingBuffer.Keys.Contains(name))
            {
                ds = new DrawableStructure(name);
                string self = "", turret = "", anim = "", bib = "", idle = "", anim2 = "", anim3 = "";
                bool vox = false;
                self = GlobalRules.GetObjectImgName(name, ref anim, ref turret, ref bib, ref vox, ref idle, ref anim2, ref anim3);
                ds.VoxelTurret = vox;
                if (!string.IsNullOrEmpty(self)) ds.pSelf = CreateFile(self, DrawableType.Shp, pPalUnit, color);
                if (!string.IsNullOrEmpty(anim)) ds.pActivateAnim = CreateFile(anim, DrawableType.Shp, pPalUnit, color);
                if (!string.IsNullOrEmpty(idle)) ds.pIdleAnim = CreateFile(idle, DrawableType.Shp, pPalUnit, color);
                if (!string.IsNullOrEmpty(anim2)) ds.pActivateAnim2 = CreateFile(anim2, DrawableType.Shp, pPalUnit, color);
                if (!string.IsNullOrEmpty(anim3)) ds.pActivateAnim3 = CreateFile(anim3, DrawableType.Shp, pPalUnit, color);
                if (!string.IsNullOrEmpty(bib)) ds.pBib = CreateFile(bib, DrawableType.Shp, pPalUnit, color);
                if (!string.IsNullOrEmpty(turret))
                {
                    ds.offsetTurret = GlobalRules.GetVoxTurOffset(name);
                    if (ds.VoxelTurret)
                    {
                        ds.pTurretAnim = CreateFile(turret, DrawableType.Vxl, pPalUnit, color);
                    }
                    else ds.pTurretAnim = CreateFile(turret, DrawableType.Shp, pPalUnit, color);
                }
                buildingBuffer[name] = ds;
            }
            else ds = buildingBuffer[name];
            return ds;
        }
        private int CreateFile(string name, DrawableType type, int pal, uint color)
        {
            int result = 0;
            if (!GlobalDir.HasFile(name)) return 0;
            VFileInfo info = GlobalDir.GetFilePtr(name);
            switch (type)
            {
                case DrawableType.Shp:
                    result = CppExtern.Files.CreateShpFileFromFileInMemory(info.ptr, info.size);
                    CppExtern.Files.LoadShpTextures(result, pal, color);
                    return result;
                case DrawableType.Tmp:
                    result = CppExtern.Files.CreateTmpFileFromFilenMemory(info.ptr, info.size);
                    CppExtern.Files.LoadTmpTextures(result, pal);
                    return result;
                case DrawableType.Vxl:
                    VFileInfo hva = GlobalDir.GetFilePtr(name.Replace("vxl", "hva"));
                    result = CppExtern.Files.CreateVxlFileFromFileInMemory(info.ptr, info.size, hva.ptr, hva.size);
                    return result;
                default:
                    return 0;
            }
        }
        #endregion


        #region Drawing
        private bool DrawStructure(DrawableStructure src, uint color, Vec3 pos, PresentStructure dest)
        {
            if (src.pSelf != 0) dest.pSelf = CppExtern.ObjectUtils.CreateShpObjectAtScene(src.pSelf, pos, 0, pPalUnit, color, src.pTurretAnim != 0);
            if (src.pActivateAnim != 0) dest.pActivateAnim = CppExtern.ObjectUtils.CreateShpObjectAtScene(src.pActivateAnim, _generalOffset + pos, 0, pPalUnit, color, false);
            if (src.pIdleAnim != 0) dest.pIdleAnim = CppExtern.ObjectUtils.CreateShpObjectAtScene(src.pIdleAnim, _generalOffset + pos, 0, pPalUnit, color, false);
            if (src.pActivateAnim2 != 0) dest.pActivateAnim2 = CppExtern.ObjectUtils.CreateShpObjectAtScene(src.pActivateAnim2, 2 * _generalOffset + pos, 0, pPalUnit, color, false);
            if (src.pActivateAnim3 != 0) dest.pActivateAnim3 = CppExtern.ObjectUtils.CreateShpObjectAtScene(src.pActivateAnim3, 3 * _generalOffset + pos, 0, pPalUnit, color, false);
            if (src.pBib != 0) dest.pBib = CppExtern.ObjectUtils.CreateShpObjectAtScene(src.pBib, pos, 0, pPalUnit, color, false);
            if (src.pTurretAnim != 0)
            {
                if (src.VoxelTurret) dest.pTurretAnim = CppExtern.ObjectUtils.CreateVxlObjectAtScene(src.pTurretAnim, ToVec3Iso(pos) + src.offsetTurret, 0, 0, 0, pPalUnit, color);
                else dest.pTurretAnim = CppExtern.ObjectUtils.CreateShpObjectAtScene(src.pTurretAnim, pos + src.offsetTurret, 0, pPalUnit, color, false);
            }

            return !(dest.pActivateAnim == 0 && dest.pBib == 0 && dest.pSelf == 0 && dest.pTurretAnim == 0);
        }
        private bool Draw(Vec3 pos, string name, int pPal, Dictionary<string, int> lookup, Dictionary<int, int> dest,
DrawableType type, int subIndex, uint remapColor, int coordIndexer, ref int id, bool _isFlat = false)
        {
            if (!lookup.Keys.Contains(name))
            {
                id = CreateFile(name, type, pPal, remapColor);
                if (id == 0) return false;
                lookup[name] = id;
            }
            else id = lookup[name];
            bool bResult = false;
            int nResult = 0;
            switch (type)
            {
                case DrawableType.Shp:
                    nResult = CppExtern.ObjectUtils.CreateShpObjectAtScene(id, pos, subIndex, pPal, remapColor, _isFlat);
                    break;
                case DrawableType.Tmp:
                    int o1 = 0, o2 = 0;
                    bResult = CppExtern.ObjectUtils.CreateTmpObjectAtScene(id, pos, subIndex, ref o1, ref o2);
                    if (bResult)
                    {
                        dest[coordIndexer] = id;
                        return true;
                    }
                    return false;
                case DrawableType.Vxl:
                    nResult = CppExtern.ObjectUtils.CreateVxlObjectAtScene(id, pos, 0, 0, 0, pPal, remapColor);
                    break;
            }
            if (nResult != 0)
            {
                dest[coordIndexer] = nResult;
                return true;
            }
            return false;
        }
        #endregion


        private Vec3 ToVec3Iso(ILocateable loc)
        {
            return new Vec3() { X = loc.fX * _30SQ2, Y = loc.fY * _30SQ2, Z = loc.fZ * _10SQ3 };
        }
        private Vec3 ToVec3Iso(int x, int y, int z)
        {
            return new Vec3() { X = x * _30SQ2, Y = y * _30SQ2, Z = z * _10SQ3 };
        }
        private Vec3 ToVec3Iso(Vec3 zero)
        {
            return new Vec3() { X = zero.X + _15SQ2, Y = zero.Y + _15SQ2, Z = zero.Z };
        }
        private Vec3 ToVec3Zero(int x, int y, int z)
        {
            return new Vec3() { X = x * _30SQ2 - _15SQ2, Y = y * _30SQ2 - _15SQ2, Z = z * _10SQ3 };
        }
        #endregion
    }
}
