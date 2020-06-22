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
    public partial class Engine
    {
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
        private DrawableTile CreateDrawableTile(string filename, int subindex)
        {
            DrawableTile d;
            if (!Buffer.Buffers.Tiles.Keys.Contains(filename))
            {
                d = new DrawableTile();
                d.pSelf = CreateTileFile(filename);
                TmpFile tmp = new TmpFile(GlobalDir.GetRawByte(filename), filename);
                foreach (TmpImage img in tmp.Images)
                {
                    DrawableTile.SubTile subtile = new DrawableTile.SubTile();
                    subtile.RadarColor = new DrawableTile.ColorPair(img.ColorRadarLeft, img.ColorRadarRight);
                    d.SubTiles.Add(subtile);
                    subtile.WaterPassable = img.TerrainType == Constant.DrawingEngine.Tiles.Water;
                    subtile.LandPassable = Constant.DrawingEngine.Tiles.Passable.Contains(img.TerrainType);
                    subtile.Buildable = Constant.DrawingEngine.Tiles.Buildables.Contains(img.TerrainType) && img.RampType == 0;
                }
                Buffer.Buffers.Tiles[filename] = d;
            }
            else d = Buffer.Buffers.Tiles[filename];
            return d;
        }
        private DrawableInfantry CreateDrawableInfantry(ObjectItemBase inf, uint color, int pPal, int idxFrame)
        {
            DrawableInfantry d;
            string lookup = string.Format("{0}{1}.in{2}", inf.RegName, color, idxFrame);
            if (!Buffer.Buffers.Infantries.Keys.Contains(lookup))
            {
                d = new DrawableInfantry();
                string nameid = GlobalRules.GetObjectImgName(inf, out short frame);
                string customPalName = GlobalRules.GetCustomPaletteName(inf.RegName);
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
            string name = TileDictionary.NameAsTheater(terrain.RegName);
            if (!Buffer.Buffers.Miscs.Keys.Contains(name))
            {
                d = new DrawableMisc(MapObjectType.Terrain, name);
                d.Framecount = GlobalDir.GetShpFrameCount(name);
                d.RadarColor = ToColor(GlobalRules[terrain.RegName].ParseStringList("RadarColor"));
                bool isTibTree = ParseBool(GlobalRules[terrain.RegName]["SpawnsTiberium"]);
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
            string name = TileDictionary.NameAsTheater(smudge.RegName);
            if (!Buffer.Buffers.Miscs.Keys.Contains(name))
            {
                d = new DrawableMisc(MapObjectType.Smudge, name);
                d.pSelf = CreateFile(name, DrawableType.Shp);
                GlobalRules.GetSmudgeSizeData(smudge.RegName, out int w, out int h);
                d.SmudgeWidth = w;
                d.SmudgeHeight = h;
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
                string land = GlobalRules[name]["Land"];
                string[] colors = GlobalRules[name].ParseStringList("RadarColor");
                if (wall) GlobalRules.FixWallOverlayName(ref filename);
                d.RadarColor = ToColor(colors);
                d.IsMoveBlockingOverlay = land == "Rock";
                d.IsRubble = rubble;
                d.IsWall = wall;

                if (!string.IsNullOrEmpty(img) && name != img) filename = img;
                if (overrides)
                {
                    flat = false;
                    d.IsHiBridge = true;
                    if (GlobalConfig.BridgeOffsetFrames.Contains(overlay.Frame)) d.IsOffsetBridge = true;
                }
                if (!rubble)
                {
                    if (!_zeroLandType.Contains(land))
                    {
                        if (land == "Railroad") flat = true;
                        else flat = false;
                        d.IsZeroVec = false;
                    }
                }
                if (flat) d.FlatType = ShpFlatType.FlatGround;
                else d.FlatType = ShpFlatType.Vertical;
                if (wall) d.FlatType = ShpFlatType.Box1;

                if (GlobalDir.HasFile(filename + ".shp")) filename = filename.ToLower() + ".shp";
                else if (wall)
                {
                    filename = Replace(filename, 1, 'G').ToLower() + ".shp";
                }
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

                if (d.FlatType == ShpFlatType.FlatGround) d.IsFlatOnly = true;

                d.Framecount = GlobalDir.GetShpFrameCount(filename);
                d.pSelf = CreateFile(filename, DrawableType.Shp, overlay.Frame);
                if (wall || !rubble || !isTiberium) d.pShadow = CreateFile(filename, DrawableType.Shp, overlay.Frame + d.Framecount / 2);
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
                                                    out short nSelf, out short nAnim, out short nTurret, out short nBib, out short nIdle, out short nAnim2, out short nAnim3, out short nSuper,
                                                    out string alphaName);
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
                d.MinimapColor = ToColor(color);
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
                if (!string.IsNullOrEmpty(alphaName))
                {
                    d.pAlphaImg = CreateFile(alphaName, DrawableType.Shp);
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
    }
}
