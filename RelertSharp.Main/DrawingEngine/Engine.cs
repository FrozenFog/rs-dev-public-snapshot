using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
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
    public class Engine : IDisposable
    {
        private readonly float _30SQ2, _10SQ3, _15SQ2, _rad45;
        private readonly string[] _zeroLandType = new string[] { "Water", "Clear", "" };
        private float _width { get { return _30SQ2; } }
        private Vec3 _NormTileVec { get { return new Vec3(_15SQ2, _15SQ2, _10SQ3); } }
        private Vec4 _TileColor { get; set; }
        private float _height { get { return _10SQ3; } }
        private const uint _colorIgnore = 0x000000FF;
        private const uint _white = 0xFFFFFFFF;
        private static Vec3 _generalOffset = new Vec3() { X = 1, Y = 1, Z = (float)Math.Sqrt(2f/3f) };
        private BufferCollection Buffer = new BufferCollection();
        private GdipSurface surface;
        private Vec3 previousTile = Vec3.Zero;
        private int pPalIso = 0;
        private int pPalUnit = 0;
        private int pPalTheater = 0;
        private int pPalSystem = 0;


        #region Ctor - Engine
        public Engine()
        {
            _30SQ2 = (float)(30F * Math.Sqrt(2));
            _10SQ3 = (float)(10F * Math.Sqrt(3));
            _15SQ2 = _30SQ2 / 2;
            _rad45 = (float)Math.PI / 4;
            surface = new GdipSurface(60, 54);
            _TileColor = Vec4.Unit4(1);
        }
        #endregion


        #region Public Methods - Engine
        public bool Initialize(IntPtr ptr, Font font)
        {
            return CppExtern.Scene.SetUpScene(ptr) && CppExtern.Scene.ResetSceneView() && CppExtern.Scene.SetSceneFont(font.Name, (int)font.Size);
        }
        #region Draw
        public bool DrawObject(InfantryItem inf, int height, uint color)
        {
            int frame = GlobalRules.GetFrameFromDirection(inf.Rotation, inf.NameID);
            DrawableInfantry src = CreateDrawableInfantry(inf, color, pPalUnit, frame);
            PresentInfantry dest = new PresentInfantry(inf, height);
            Vec3 pos = ToVec3Iso(dest, inf.SubCells);
            return DrawInfantry(src, pos, dest, frame, pPalUnit, inf.SubCells);
        }
        public bool DrawObject(UnitItem unit, int height, uint color)
        {
            Vec3 idx = VxlRotation(unit.NameID, unit.Rotation, false);
            DrawableUnit src = CreateDrawableUnit(unit.NameID, color, pPalUnit, (int)idx.X);
            PresentUnit dest = new PresentUnit(unit, height, src.IsVxl);
            Vec3 pos = ToVec3Iso(dest);
            Vec3 ro = VxlRotation(unit.NameID, unit.Rotation, src.IsVxl);
            return DrawUnit(src, dest, pos, ro, pPalUnit);
        }
        public bool DrawObject(AircraftItem air, int height, uint color)
        {
            DrawableUnit src = CreateDrawableUnit(air.NameID, color, pPalUnit, 0);
            PresentUnit dest = new PresentUnit(air, height);
            Vec3 pos = ToVec3Iso(dest);
            Vec3 ro = VxlRotation(air.NameID, air.Rotation, true);
            return DrawUnit(src, dest, pos, ro, pPalUnit);
        }
        public bool DrawObject(StructureItem structure, int height, uint color)
        {
            Vec3 idx = BuildingRotation(structure.NameID, structure.Rotation, false);
            DrawableStructure src = CreateDrawableStructure(structure.NameID, color, pPalUnit, (int)idx.X);
            PresentStructure dest = new PresentStructure(structure, height, src.VoxelTurret);
            Vec3 ro;
            if (src.pTurretAnim != 0) ro = BuildingRotation(structure.NameID, structure.Rotation, src.VoxelTurret);
            else ro = Vec3.Zero;
            Vec3 pos = ToVec3Zero(dest).Rise();
            return DrawStructure(src, pos, dest, ro, pPalUnit);
        }
        public bool DrawObject(BaseNode node, int height, uint color)
        {
            Vec3 idx = BuildingRotation(node.Name, 0, false);
            DrawableStructure src = CreateDrawableStructure(node.Name, color, pPalUnit, (int)idx.X , true);
            PresentStructure dest = new PresentStructure(node, height, src.VoxelTurret);
            Vec3 ro;
            if (src.pTurretAnim != 0) ro = BuildingRotation(node.Name, 0, src.VoxelTurret);
            else ro = Vec3.Zero;
            Vec3 pos = ToVec3Zero(dest);
            pos.Z += 0.2F;
            bool draw = DrawStructure(src, pos, dest, ro, pPalUnit);
            if (draw)
            {
                dest.SetTransparency(true);
                return true;
            }
            return false;
        }
        public bool DrawGeneralItem(Tile t)
        {
            string name = TileDictionary[t.TileIndex];
            Vec3 pos = ToVec3Iso(t);
            int tmp = CreateTileFile(name);
            if (DrawTile(tmp, pos, t.SubIndex, pPalIso, out int pSelf, out int pExtra))
            {
                PresentTile pt = new PresentTile(pSelf, pExtra);
                Buffer.Scenes.Tiles[t.Coord] = pt;
                return true;
            }
            return false;
        }
        public bool DrawGeneralItem(TerrainItem terrain, int height)
        {
            DrawableMisc src = CreateDrawableMisc(terrain);
            PresentMisc dest = new PresentMisc(MapObjectType.Terrain, terrain, height);
            Vec3 pos;
            if (src.IsZeroVec) pos = ToVec3Zero(terrain, height);
            else pos = ToVec3Iso(terrain, height);
            if (DrawMisc(src, dest, pos, src.pPal, 0, _white, ShpFlatType.Vertical, src.Framecount))
            {
                Buffer.Scenes.Terrains[terrain.CoordInt] = dest;
                return true;
            }
            return false;
        }
        public bool DrawGeneralItem(SmudgeItem smg, int height)
        {
            DrawableMisc src = CreateDrawableMisc(smg);
            PresentMisc dest = new PresentMisc(MapObjectType.Smudge, smg, height);
            Vec3 pos = ToVec3Zero(smg, height);
            pos.Z += 0.02F;
            if (DrawMisc(src, dest, pos, pPalIso, 0, _white, ShpFlatType.FlatGround))
            {
                Buffer.Scenes.Smudges[smg.CoordInt] = dest;
                return true;
            }
            return false;
        }
        public bool DrawGeneralItem(OverlayUnit o, int height)
        {
            DrawableMisc src = CreateDrawableMisc(o, _white);
            PresentMisc dest = new PresentMisc(MapObjectType.Overlay, o, height);
            dest.IsTiberiumOverlay = src.IsTiberiumOverlay;
            Vec3 pos;
            if (src.IsZeroVec) pos = ToVec3Zero(o, height);
            else pos = ToVec3Iso(o, height);
            int pal = src.pPal;
            ShpFlatType type = src.FlatType;
            if (DrawMisc(src, dest, pos, src.pPal, o.Frame, _white, type, src.Framecount))
            {
                Buffer.Scenes.Overlays[o.Coord] = dest;
                return true;
            }
            return false;
        }
        public bool DrawWaypoint(WaypointItem waypoint, int height)
        {
            DrawableMisc src = new DrawableMisc(MapObjectType.Waypoint, waypoint.Num);
            src.pSelf = Buffer.Files.WaypointBase;
            PresentMisc dest = new PresentMisc(MapObjectType.Waypoint, waypoint, height);
            Vec3 pos = ToVec3Iso(dest).Rise() + 100 * _generalOffset;
            if (DrawMisc(src, dest, pos, pPalSystem, 0, _white, ShpFlatType.Vertical))
            {
                Buffer.Scenes.Waypoints[dest.Coord] = dest;
                return true;
            }
            return false;
        }
        public bool DrawCelltag(CellTagItem cell, int height, bool topmost = false)
        {
            DrawableMisc src = new DrawableMisc(MapObjectType.Celltag, "");
            src.pSelf = Buffer.Files.CelltagBase;
            PresentMisc dest = new PresentMisc(MapObjectType.Celltag, cell, height);
            Vec3 pos = ToVec3Iso(dest).Rise();
            if (topmost) pos += 99 * _generalOffset;
            if (DrawMisc(src,dest,pos,pPalSystem, 0, _white, ShpFlatType.FlatGround))
            {
                Buffer.Scenes.Celltags[dest.Coord] = dest;
                return true;
            }
            return false;
        }
        #endregion
        public void SetObjectLightning(LightningItem light)
        {
            Vec4 color = new Vec4(light.Red, light.Green, light.Blue, 1);
            Vec4 amb = Vec4.Unit3(light.Ambient);
            color *= amb;
            _TileColor = color;
            foreach (PresentTile t in Buffer.Scenes.Tiles.Values) t.SetColor(color);
            foreach (IPresentBase obj in Buffer.Scenes.MapObjects) obj.SetColor(amb);
            foreach (PresentMisc misc in Buffer.Scenes.MapMiscs) misc.SetColor(color);
        }
        public Vec3 ClientPointToCellPos(Point src, TileLayer referance)
        {
            Pnt p = Pnt.FromPoint(src);
            Vec3 pos = new Vec3();
            CppExtern.Scene.ClientPositionToScenePosition(p, ref pos);
            pos += _NormTileVec * 12;
            for (int height = 0; height < 12; height++)
            {
                Vec3 tilepos = ScenePosToCoord(pos);
                if (referance.HasTileOn(tilepos)) return tilepos;
                pos -= _NormTileVec;
            }
            return Vec3.Zero;
        }
        /// <summary>
        /// Return true if pos changed
        /// </summary>
        /// <param name="newpos"></param>
        /// <returns></returns>
        public bool SelectTile(Vec3 newpos)
        {
            if (newpos != previousTile)
            {
                Buffer.Scenes.ColoringTile(newpos.ToCoord(), Vec4.TileIndicator, Vec4.TileExIndi);
                Buffer.Scenes.ColoringTile(previousTile.ToCoord(), _TileColor, _TileColor);
                previousTile = newpos;
                return true;
            }
            return false;
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
            Buffer.RemoveSceneItem(BufferCollection.RemoveFlag.All);
            CppExtern.Scene.ResetSceneView();
            CppExtern.Scene.PresentAllObject();
        }
        #endregion


        #region Private Methods - Engine
        #region Create File
        private int CreateTileFile(string name)
        {
            int id = 0;
            if (!Buffer.Files.Tmp.Keys.Contains(name))
            {
                id = CreateFile(name, DrawableType.Tmp);
                Buffer.Files.Tmp[name] = id;
            }
            else id = Buffer.Files.Tmp[name];
            return id;
        }
        private DrawableInfantry CreateDrawableInfantry(ObjectItemBase inf, uint color, int pPal, int idxFrame)
        {
            DrawableInfantry d;
            string lookup = string.Format("{0}{1}.in{2}", inf.NameID, color, idxFrame);
            if (!Buffer.Buffers.Infantries.Keys.Contains(lookup))
            {
                d = new DrawableInfantry();
                string nameid = GlobalRules.GetObjectImgName(inf, out short frame);
                string customPalName = GlobalRules.GetCustomPaletteName(inf.NameID);
                if (!string.IsNullOrEmpty(customPalName))
                {
                    pPal = CreatePalette(customPalName);
                    d.pPalCustom = pPal;
                }
                d.NameID = nameid;
                d.RemapColor = color;
                d.Framecount = frame;
                d.pSelf = CreateFile(nameid, DrawableType.Shp, idxFrame);
                d.pShadow = CreateFile(nameid, DrawableType.Shp, idxFrame + frame / 2);
                Buffer.Buffers.Infantries[lookup] = d;
            }
            else d = Buffer.Buffers.Infantries[lookup];
            return d;
        }
        private DrawableUnit CreateDrawableUnit(string name, uint color, int pPal, int idxFrame)
        {
            DrawableUnit d;
            string lookup = string.Format("{0}{1}.in{2}", name, color, idxFrame);
            if (!Buffer.Buffers.Units.Keys.Contains(lookup))
            {
                d = new DrawableUnit(name);
                string self = "", turret = "", barl = "";
                bool vxl = true;
                self = GlobalRules.GetUnitImgName(name, ref turret, ref barl, ref vxl);
                if (!vxl) d.Framecount = GlobalDir.GetShpFrameCount(self);
                d.IsVxl = vxl;
                d.RemapColor = color;
                string customPalName = GlobalRules.GetCustomPaletteName(name);
                if (!string.IsNullOrEmpty(customPalName))
                {
                    pPal = CreatePalette(customPalName);
                    d.pPalCustom = pPal;
                }
                DrawableType t;
                if (d.IsVxl) t = DrawableType.Vxl;
                else t = DrawableType.Shp;
                if (!string.IsNullOrEmpty(self)) d.pSelf = CreateFile(self, t, idxFrame);
                if (!string.IsNullOrEmpty(turret)) d.pTurret = CreateFile(turret, DrawableType.Vxl);
                if (!string.IsNullOrEmpty(barl)) d.pBarrel = CreateFile(barl, DrawableType.Vxl);
                Buffer.Buffers.Units[lookup] = d;
            }
            else d = Buffer.Buffers.Units[lookup];
            return d;
        }
        private DrawableMisc CreateDrawableMisc(TerrainItem terrain)
        {
            DrawableMisc d;
            string name = TileDictionary.NameAsTheater(terrain.TerrainName);
            if (!Buffer.Buffers.Miscs.Keys.Contains(name))
            {
                d = new DrawableMisc(MapObjectType.Terrain, name);
                d.Framecount = GlobalDir.GetShpFrameCount(name);
                bool isTibTree = ParseBool(GlobalRules[terrain.TerrainName]["SpawnsTiberium"]);
                d.pPal = pPalIso;
                d.IsZeroVec = false;
                if (isTibTree)
                {
                    d.pPal = pPalUnit;
                    d.IsZeroVec = true;
                }
                d.pSelf = CreateFile(name, DrawableType.Shp);
                d.pShadow = CreateFile(name, DrawableType.Shp, d.Framecount / 2);
                Buffer.Buffers.Miscs[name] = d;
            }
            else d = Buffer.Buffers.Miscs[name];
            return d;

        }
        private DrawableMisc CreateDrawableMisc(SmudgeItem smudge)
        {
            DrawableMisc d;
            string name = TileDictionary.NameAsTheater(smudge.Name);
            if (!Buffer.Buffers.Miscs.Keys.Contains(name))
            {
                d = new DrawableMisc(MapObjectType.Smudge, name);
                d.pSelf = CreateFile(name, DrawableType.Shp);
                Buffer.Buffers.Miscs[name] = d;
            }
            else d = Buffer.Buffers.Miscs[name];
            return d;
        }
        private DrawableMisc CreateDrawableMisc(OverlayUnit overlay, uint color)
        {
            DrawableMisc d;
            string name = GlobalRules.GetOverlayName(overlay.Index);
            string lookup = string.Format("{0}{1}.in{2}", name, color, overlay.Frame);
            if (!Buffer.Buffers.Miscs.Keys.Contains(lookup))
            {
                d = new DrawableMisc(MapObjectType.Overlay, name);
                d.IsZeroVec = true;
                d.pPal = pPalUnit;
                string filename = name;
                bool flat = ParseBool(GlobalRules[name]["DrawFlat"], true);
                bool overrides = ParseBool(GlobalRules[name]["Overrides"]);
                bool isTiberium = ParseBool(GlobalRules[name]["Tiberium"]);
                bool rubble = ParseBool(GlobalRules[name]["IsRubble"]);
                bool wall = ParseBool(GlobalRules[name]["Wall"]);
                string img = GlobalRules[name]["Image"];

                if (!string.IsNullOrEmpty(img) && name != img) filename = img;
                if (overrides) flat = false;
                if (!rubble)
                {
                    if (!_zeroLandType.Contains((string)GlobalRules[name]["Land"]))
                    {
                        flat = false;
                        d.IsZeroVec = false;
                    }
                }
                if (flat) d.FlatType = ShpFlatType.FlatGround;
                else d.FlatType = ShpFlatType.Vertical;
                if (wall) d.FlatType = ShpFlatType.Box1;

                if (GlobalDir.HasFile(filename + ".shp")) filename = filename.ToLower() + ".shp";
                else
                {
                    filename = string.Format("{0}.{1}", filename.ToLower(), TileDictionary.TheaterSub);
                    if (isTiberium)
                    {
                        d.pPal = pPalTheater;
                        d.IsTiberiumOverlay = true;
                    }
                    else d.pPal = pPalIso;
                }

                if (d.FlatType == ShpFlatType.FlatGround) d.IsTiberiumOverlay = true;

                d.Framecount = GlobalDir.GetShpFrameCount(filename);
                d.pSelf = CreateFile(filename, DrawableType.Shp, overlay.Frame);
                if (!d.IsTiberiumOverlay) d.pShadow = CreateFile(filename, DrawableType.Shp, overlay.Frame + d.Framecount / 2);
                Buffer.Buffers.Miscs[lookup] = d;
            }
            else d = Buffer.Buffers.Miscs[lookup];
            return d;
        }
        private DrawableStructure CreateDrawableStructure(string name, uint color, int pPal, int direction, bool isBaseNode = false)
        {
            DrawableStructure d;
            string lookup = string.Format("{0}{1}.in{2}", name, color, direction);
            if (isBaseNode) lookup += "n";
            if (!Buffer.Buffers.Structures.Keys.Contains(lookup))
            {
                d = new DrawableStructure(name);
                string self = "", turret = "", anim = "", bib = "", idle = "", anim2 = "", anim3 = "", barl = "", super = "";
                bool vox = false;
                self = GlobalRules.GetObjectImgName(name, ref anim, ref turret, ref bib, ref vox, ref idle, ref anim2, ref anim3, ref barl, ref super,
                                                    out short nSelf, out short nAnim, out short nTurret, out short nBib, out short nIdle, out short nAnim2, out short nAnim3, out short nSuper);
                GlobalRules.GetBuildingShapeData(name, out int height, out int foundx, out int foundy);
                #region framecount
                d.Framecount = nSelf;
                d.ActivateAnimCount = nAnim;
                d.ActivateAnim2Count = nAnim2;
                d.ActivateAnim3Count = nAnim3;
                d.IdleAnimCount = nIdle;
                d.BibCount = nBib;
                d.SuperAnimCount = nSuper;
                d.TurretAnimCount = nTurret;
                #endregion
                d.Height = height; d.FoundationX = foundx; d.FoundationY = foundy;
                d.VoxelTurret = vox;
                d.RemapColor = color;
                string customPalName = GlobalRules.GetCustomPaletteName(name);
                if (!string.IsNullOrEmpty(customPalName))
                {
                    pPal = CreatePalette(customPalName);
                    d.pPalCustom = pPal;
                }
                if (!string.IsNullOrEmpty(self))
                {
                    d.pSelf = CreateFile(self, DrawableType.Shp);
                    if (!GlobalConfig.DeactiveShadow.Contains(name)) d.pShadow = CreateFile(self, DrawableType.Shp, d.Framecount / 2);
                }
                if (!string.IsNullOrEmpty(anim))
                {
                    d.pActivateAnim = CreateFile(anim, DrawableType.Shp);
                    d.pShadowActivateAnim = CreateFile(anim, DrawableType.Shp, d.ActivateAnimCount / 2);
                }
                if (!string.IsNullOrEmpty(anim2))
                {
                    d.pActivateAnim2 = CreateFile(anim2, DrawableType.Shp);
                    d.pShadowActivateAnim2 = CreateFile(anim2, DrawableType.Shp, d.ActivateAnim2Count / 2);
                }
                if (!string.IsNullOrEmpty(anim3))
                {
                    d.pActivateAnim3 = CreateFile(anim3, DrawableType.Shp);
                    d.pShadowActivateAnim3 = CreateFile(anim3, DrawableType.Shp, d.ActivateAnim3Count / 2);
                }
                if (!string.IsNullOrEmpty(super))
                {
                    d.pSuperAnim = CreateFile(super, DrawableType.Shp);
                    d.pShadowSuperAnim = CreateFile(super, DrawableType.Shp, d.SuperAnimCount / 2);
                }
                if (!string.IsNullOrEmpty(idle))
                {
                    d.pIdleAnim = CreateFile(idle, DrawableType.Shp);
                    d.pShadowIdleAnim = CreateFile(idle, DrawableType.Shp, d.IdleAnimCount / 2);
                }
                if (!string.IsNullOrEmpty(bib))
                {
                    d.pBib = CreateFile(bib, DrawableType.Shp);
                    d.pShadowBib = CreateFile(bib, DrawableType.Shp, d.BibCount / 2);
                }
                if (!string.IsNullOrEmpty(turret))
                {
                    d.offsetTurret = GlobalRules.GetVoxTurOffset(name);
                    if (d.VoxelTurret)
                    {
                        d.pTurretAnim = CreateFile(turret, DrawableType.Vxl);
                        if (!string.IsNullOrEmpty(barl)) d.pTurretBarl = CreateFile(barl, DrawableType.Vxl);
                    }
                    else
                    {
                        d.pTurretAnim = CreateFile(turret, DrawableType.Shp, direction);
                        d.pShadowTurretAnim = CreateFile(turret, DrawableType.Shp, direction + d.TurretAnimCount / 2);
                    }
                }
                Buffer.Buffers.Structures[lookup] = d;
            }
            else d = Buffer.Buffers.Structures[lookup];
            return d;
        }
        private int CreateFile(string filename, DrawableType type, int shpframe = 0)
        {
            switch (type)
            {
                case DrawableType.Shp:
                    return CreateShp(filename, shpframe);
                case DrawableType.Tmp:
                    return CreateTmp(filename);
                case DrawableType.Vxl:
                    return CreateVxl(filename);
                default:
                    return 0;
            }
        }
        private int CreateTmp(string filename)
        {
            int id;
            if (!Buffer.Files.Tmp.Keys.Contains(filename))
            {
                if (!GlobalDir.HasFile(filename)) return 0;
                VFileInfo info = GlobalDir.GetFilePtr(filename);
                id = CppExtern.Files.CreateTmpFileFromFileInMemory(info.ptr, info.size);
                CppExtern.Files.LoadTmpTextures(id);
                Buffer.Files.Tmp[filename] = id;
            }
            else id = Buffer.Files.Tmp[filename];
            return id;
        }
        private int CreateShp(string filename, int shpframe)
        {
            int id;
            string lookup = string.Format("{0}.in{1}", filename, shpframe);
            if (!Buffer.Files.Shp.Keys.Contains(lookup))
            {
                if (!GlobalDir.HasFile(filename)) return 0;
                VFileInfo info = GlobalDir.GetFilePtr(filename);
                id = CppExtern.Files.CreateShpFileFromFileInMemory(info.ptr, info.size);
                CppExtern.Files.LoadShpTextures(id, shpframe);
                Buffer.Files.Shp[lookup] = id;
            }
            else id = Buffer.Files.Shp[lookup];
            return id;
        }
        private int CreateVxl(string filename)
        {
            int id;
            if (!Buffer.Files.Vxl.Keys.Contains(filename))
            {
                if (!GlobalDir.HasFile(filename)) return 0;
                VFileInfo info = GlobalDir.GetFilePtr(filename);
                VFileInfo hva = GlobalDir.GetFilePtr(filename.Replace("vxl", "hva"));
                id = CppExtern.Files.CreateVxlFileFromFileInMemory(info.ptr, info.size, hva.ptr, hva.size);
                Buffer.Files.Vxl[filename] = id;
            }
            else id = Buffer.Files.Vxl[filename];
            return id;
        }
        private int CreatePalette(string filename)
        {
            if (!GlobalDir.HasFile(filename)) return 0;
            int id;
            if (!Buffer.Files.Pal.Keys.Contains(filename))
            {
                VFileInfo info = GlobalDir.GetFilePtr(filename);
                id = CppExtern.Files.CreatePaletteFromFileInBuffer(info.ptr);
                Buffer.Files.Pal[filename] = id;
            }
            else id = Buffer.Files.Pal[filename];
            return id;
        }
        #endregion


        #region Drawing
        private bool DrawMisc(DrawableMisc src, PresentMisc dest, Vec3 pos, int pPal, int frame, uint color, ShpFlatType type = ShpFlatType.Vertical, short shadow = 0)
        {
            if (src.pSelf != 0)
            {
                dest.pSelf = RenderAndPresent(src.pSelf, pos, frame, color, pPal, type);
                if (!src.IsTiberiumOverlay && 
                    src.MiscType != MapObjectType.Smudge &&
                    src.MiscType != MapObjectType.Celltag) dest.pSelfShadow = RenderAndPresent(src.pShadow, pos.Rise(), frame + shadow / 2, color, pPal, ShpFlatType.FlatGround, true);
                if (src.MiscType == MapObjectType.Waypoint) dest.pWpNum = CppExtern.ObjectUtils.CreateStringObjectAtScene(pos.MoveX(_15SQ2 * -1), 0x0000FFFF, src.NameID);
            }

            return dest.IsValid;
        }
        private bool DrawInfantry(DrawableInfantry src, Vec3 pos, PresentInfantry dest, int frame, int pPal, int subcell)
        {
            if (src.pPalCustom != 0) pPal = src.pPalCustom;
            if (src.pSelf != 0)
            {
                dest.pSelf = RenderAndPresent(src.pSelf, pos, frame, src.RemapColor, pPal, ShpFlatType.Vertical);
                dest.pSelfShadow = RenderAndPresent(src.pShadow, pos.Rise(), frame + src.Framecount / 2, src.RemapColor, pPal, ShpFlatType.FlatGround, true);
            }

            Buffer.Scenes.Infantries[dest.Coord << 2 + subcell] = dest;
            return dest.IsValid;
        }
        private bool DrawStructure(DrawableStructure src, Vec3 pos, PresentStructure dest, Vec3 turRotation, int pPal)
        {
            ShpFlatType flat = ShpFlatType.Box1;
            if (src.pPalCustom != 0) pPal = src.pPalCustom;
            if (src.pSelf != 0)
            {
                dest.pSelf = RenderAndPresent(src, src.pSelf, pos, pPal, flat);
                dest.pSelfShadow = RenderAndPresent(src, src.pShadow, pos.Rise(), pPal, ShpFlatType.FlatGround, src.Framecount, true);
            }
            if (src.pActivateAnim != 0)
            {
                dest.pActivateAnim = RenderAndPresent(src, src.pActivateAnim, pos + _generalOffset, pPal, flat);
                dest.pActivateAnim2Shadow = RenderAndPresent(src, src.pShadowActivateAnim, pos.Rise() + _generalOffset, pPal, ShpFlatType.FlatGround, src.ActivateAnimCount, true);
            }
            if (src.pIdleAnim != 0)
            {
                dest.pIdleAnim = RenderAndPresent(src, src.pIdleAnim, _generalOffset + pos, pPal, flat);
                dest.pIdleAnimShadow = RenderAndPresent(src, src.pShadowIdleAnim, _generalOffset + pos.Rise(), pPal, ShpFlatType.FlatGround, src.IdleAnimCount, true);
            }
            if (src.pActivateAnim2 != 0)
            {
                dest.pActivateAnim2 = RenderAndPresent(src, src.pActivateAnim2, 2 * _generalOffset + pos, pPal, flat);
                dest.pActivateAnim2Shadow = RenderAndPresent(src, src.pShadowActivateAnim2, 2 * _generalOffset + pos.Rise(), pPal, ShpFlatType.FlatGround, src.ActivateAnim2Count, true);
            }
            if (src.pActivateAnim3 != 0)
            {
                dest.pActivateAnim3 = RenderAndPresent(src, src.pActivateAnim3, 3 * _generalOffset + pos, pPal, ShpFlatType.Vertical);
                dest.pActivateAnim3Shadow = RenderAndPresent(src, src.pShadowActivateAnim3, 3 * _generalOffset + pos.Rise(), pPal, ShpFlatType.FlatGround, src.ActivateAnim3Count, true);
            }
            if (src.pSuperAnim != 0)
            {
                dest.pSuperAnim = RenderAndPresent(src, src.pSuperAnim, 3 * _generalOffset + pos, pPal, flat);
                dest.pSuperAnimShadow = RenderAndPresent(src, src.pShadowSuperAnim, 3 * _generalOffset + pos.Rise(), pPal, ShpFlatType.FlatGround, src.SuperAnimCount, true);
            }
            if (src.pBib != 0)
            {
                dest.pBib = RenderAndPresent(src, src.pBib, pos, pPal, flat);
                dest.pBibShadow = RenderAndPresent(src, src.pShadowBib, pos.Rise(), pPal, ShpFlatType.FlatGround, src.BibCount, true);
            }
            if (src.pTurretAnim != 0)
            {
                if (src.VoxelTurret)
                {
                    dest.pTurretAnim = RenderAndPresent(src.pTurretAnim, pos + src.offsetTurret, turRotation, src.RemapColor, pPal);
                    if (src.pTurretBarl != 0) dest.pTurretBarl = RenderAndPresent(src.pTurretBarl, pos + src.offsetTurret, turRotation, src.RemapColor, pPal);
                }
                else
                {
                    dest.pTurretAnim = RenderAndPresent(src.pTurretAnim, pos + src.offsetTurret, (int)turRotation.X, src.RemapColor, pPal, ShpFlatType.Vertical);
                    dest.pTurretAnimShadow = RenderAndPresent(src.pShadowTurretAnim, pos.Rise() + src.offsetTurret, (int)turRotation.X + src.TurretAnimCount / 2, src.RemapColor, pPal, ShpFlatType.FlatGround, true);
                }
            }
            Buffer.Scenes.Structures[dest.Coord] = dest;
            return dest.IsValid;
        }
        private bool DrawUnit(DrawableUnit src, PresentUnit dest, Vec3 pos, Vec3 rotation, int pPal)
        {
            if (src.pPalCustom != 0) pPal = src.pPalCustom;
            if (src.IsVxl)
            {
                if (src.pSelf != 0) dest.pSelf = RenderAndPresent(src.pSelf, pos, rotation, src.RemapColor, pPal);
                if (src.pBarrel != 0) dest.pBarrel = RenderAndPresent(src.pBarrel, pos, rotation, src.RemapColor, pPal);
                if (src.pTurret != 0) dest.pTurret = RenderAndPresent(src.pTurret, pos, rotation, src.RemapColor, pPal);
            }
            else
            {
                if (src.pSelf != 0)
                {
                    dest.pSelf = RenderAndPresent(src.pSelf, pos, (int)rotation.X, src.RemapColor, pPal, ShpFlatType.Vertical);
                    dest.pSelfShadow = RenderAndPresent(src.pSelf, pos.Rise(), (int)rotation.X + src.Framecount / 2, src.RemapColor, pPal, ShpFlatType.FlatGround, true);
                }
            }
            Buffer.Scenes.Units[dest.Coord] = dest;
            return dest.IsValid;
        }
        private bool DrawTile(int idTmp, Vec3 pos, byte subindex, int pPal, out int idSelf, out int idExtra)
        {
            idSelf = 0;idExtra = 0;
            return CppExtern.ObjectUtils.CreateTmpObjectAtScene(idTmp, pos, pPal, subindex, ref idSelf, ref idExtra);
        }
        private int RenderAndPresent(int shpID, Vec3 pos, int frame, uint color, int pPal, ShpFlatType flat, bool shadow = false)
        {
            if (flat == ShpFlatType.Box1)
            {
                pos.Z += 0.1F;
                return CppExtern.ObjectUtils.CreateShpObjectAtScene(shpID, pos, frame, pPal, color, (int)flat, 1, 1, 5, shadow);
            }
            else return CppExtern.ObjectUtils.CreateShpObjectAtScene(shpID, pos, frame, pPal, color, (int)flat, 0, 0, 0, shadow);
        }
        private int RenderAndPresent(DrawableStructure src, int id, Vec3 pos, int pPal, ShpFlatType flat, short framecount = 0, bool shadow = false)
        {
            return CppExtern.ObjectUtils.CreateShpObjectAtScene(id, pos, framecount / 2 , pPal, src.RemapColor, (int)flat, src.FoundationX, src.FoundationY, src.Height, shadow);
        }
        private int RenderAndPresent(int vxlID, Vec3 pos, Vec3 ro, uint color, int pPal)
        {
            return CppExtern.ObjectUtils.CreateVxlObjectAtScene(vxlID, pos, ro.X, ro.Y, ro.Z, pPal, color);
        }
        #endregion


        #region Etc
        private Vec3 ScenePosToCoord(Vec3 px)
        {
            return new Vec3
                ((int)Math.Floor(px.X / _width),
                (int)Math.Floor(px.Y / _width),
                (int)Math.Floor(px.Z / _height));
        }
        private Vec3 BuildingRotation(string nameid, int facing, bool isVxl)
        {
            facing >>= 5;
            if (isVxl)
            {
                return new Vec3() { X = 0, Y = 0, Z = (facing - 2) * _rad45 };
            }
            else
            {
                if (facing == 7) facing = 0;
                else facing = 7 - facing;
                INIEntity turret = GlobalRules.GetBuildingTurret(nameid);
                int maxnum = turret.GetPair("LoopEnd").ParseInt() - turret.GetPair("LoopStart").ParseInt() + 1;
                if (maxnum < 8) return Vec3.Zero;
                int increse = maxnum >> 3;
                return new Vec3() { X = facing * increse, Y = 0, Z = 0 };
            }
        }
        private Vec3 VxlRotation(string nameID, int facing, bool isVxl)
        {
            if (isVxl)
            {
                facing = facing >> 5;
                return new Vec3() { X = 0, Y = 0, Z = (facing - 2) * _rad45 };
            }
            else
            {
                int facingBase = 256 / GlobalRules[nameID].GetPair("Facings").ParseInt(8);
                int direction = facing / facingBase;
                if (direction == 7) direction = 0;
                else direction++;
                int startFrame = GlobalRules[nameID].GetPair("StartWalkFrame").ParseInt(-1);
                int walkFrame = GlobalRules[nameID].GetPair("WalkFrames").ParseInt(12);
                int offset = walkFrame -1;
                return new Vec3() { X = startFrame + direction * walkFrame + offset, Y = 0, Z = 0 };
            }
        }
        private Vec3 ToVec3Iso(PresentInfantry inf, int subcell)
        {
            float x = inf.X + 0.25f, y = inf.Y + 0.25f, z = inf.Z;
            if (subcell == 2) x -= 0.5f;
            if (subcell == 3) y -= 0.5f;
            return ToVec3Iso(x, y, z);
        }
        private Vec3 ToVec3Iso(I3dLocateable d3)
        {
            return ToVec3Iso(d3.X, d3.Y, d3.Z);
        }
        private Vec3 ToVec3Zero(I3dLocateable d3)
        {
            return ToVec3Zero(d3.X, d3.Y, d3.Z);
        }
        private Vec3 ToVec3Iso(I2dLocateable d2, int height)
        {
            return ToVec3Iso(d2.X, d2.Y, height);
        }
        private Vec3 ToVec3Zero(I2dLocateable d2, int height)
        {
            return ToVec3Zero(d2.X, d2.Y, height);
        }
        private Vec3 ToVec3Iso(float x, float y, float z)
        {
            return new Vec3() { X = x * _30SQ2, Y = y * _30SQ2, Z = z * _10SQ3 };
        }
        private Vec3 ToVec3Iso(Vec3 zero)
        {
            return new Vec3() { X = zero.X + _15SQ2, Y = zero.Y + _15SQ2, Z = zero.Z };
        }
        private Vec3 ToVec3Zero(Vec3 iso)
        {
            return new Vec3() { X = iso.X - _15SQ2, Y = iso.Y - _15SQ2, Z = iso.Z };
        }
        private Vec3 ToVec3Zero(int x, int y, int z)
        {
            return new Vec3() { X = x * _30SQ2 - _15SQ2, Y = y * _30SQ2 - _15SQ2, Z = z * _10SQ3 };
        }
        #endregion
        #endregion
    }
}
