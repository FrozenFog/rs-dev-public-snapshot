using RelertSharp.Common;
using RelertSharp.FileSystem;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;
using RelertSharp.Engine.Common;
using System.Linq;
using System;
using static RelertSharp.Common.GlobalVar;
using static RelertSharp.Utils.Misc;
using static RelertSharp.Common.Constant;
using RelertSharp.Engine.DrawableBuffer;
using RelertSharp.IniSystem;

namespace RelertSharp.Engine
{
    internal static partial class EngineMain
    {
        public static void ReleaseAllFiles()
        {
            Buffer.ReleaseFiles();
        }
        #region Create File
        private static int CreateTileFile(string name)
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
        internal static DrawableTile CreateDrawableTile(string filename, int subindex)
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
                    subtile.WaterPassable = img.TerrainType == Constant.DrawingEngine.Tiles.Water;
                    subtile.LandPassable = Constant.DrawingEngine.Tiles.Passable.Contains(img.TerrainType);
                    subtile.Buildable = Constant.DrawingEngine.Tiles.Buildables.Contains(img.TerrainType) && img.RampType == 0;
                    subtile.TerrainType = img.TerrainType;
                    subtile.RampType = img.RampType;
                    d.SubTiles.Add(subtile);
                }
                Buffer.Buffers.Tiles[filename] = d;
                tmp.Dispose();
            }
            else d = Buffer.Buffers.Tiles[filename];
            return d;
        }
        internal static DrawableInfantry CreateDrawableInfantry(ObjectItemBase inf, uint color, int pPal, int idxFrame)
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

                if (d.IsEmpty) d.pSelf = CreateFile("unkInf.shp", DrawableType.Shp);
                Buffer.Buffers.Infantries[lookup] = d;
            }
            else d = Buffer.Buffers.Infantries[lookup];
            return d;
        }
        internal static DrawableUnit CreateDrawableUnit(string name, uint color, int pPal, int idxFrame)
        {
            DrawableUnit d;
            string lookup = string.Format("{0}{1}.in{2}", name, color, idxFrame);
            if (!Buffer.Buffers.Units.Keys.Contains(lookup))
            {
                d = new DrawableUnit(name);
                string self = "", turret = "", barl = "";
                bool vxl = true;
                self = GlobalRules.GetUnitInfo(name, ref turret, ref barl, ref vxl, out int turretOffset);
                if (!vxl) d.Framecount = GlobalDir.GetShpFrameCount(self, out bool b);
                d.IsVxl = vxl;
                d.RemapColor = color;
                d.TurretOffset = turretOffset;
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

                if (d.IsEmpty)
                {
                    d.pSelf = CreateFile("unkObj.shp", DrawableType.Shp);
                    d.IsVxl = false;
                }
                Buffer.Buffers.Units[lookup] = d;
            }
            else d = Buffer.Buffers.Units[lookup];
            return d;
        }
        internal static DrawableMisc CreateDrawableMisc(TerrainItem terrain)
        {
            DrawableMisc d;
            string name = TileDictionary.NameAsTheater(terrain.RegName);
            if (!Buffer.Buffers.Miscs.Keys.Contains(name))
            {
                d = new DrawableMisc(MapObjectType.Terrain, name);
                d.Framecount = GlobalDir.GetShpFrameCount(name, out bool b);
                d.RadarColor = ToColor(GlobalRules[terrain.RegName].ParseStringList("RadarColor"));
                bool isTibTree = IniParseBool(GlobalRules[terrain.RegName]["SpawnsTiberium"]);
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
        internal static DrawableMisc CreateDrawableMisc(SmudgeItem smudge)
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
        internal static DrawableMisc CreateDrawableMisc(OverlayUnit overlay, uint color)
        {
            DrawableMisc d;
            string name = GlobalRules.GetOverlayName(overlay.OverlayIndex);
            string lookup = string.Format("{0}{1}.in{2}", name, color, overlay.OverlayFrame);
            if (!Buffer.Buffers.Miscs.Keys.Contains(lookup))
            {
                d = new DrawableMisc(MapObjectType.Overlay, name);
                d.IsZeroVec = true;
                d.pPal = pPalUnit;
                string filename = name;
                bool flat = IniParseBool(GlobalRules[name]["DrawFlat"], true);
                bool overrides = IniParseBool(GlobalRules[name]["Overrides"]);
                bool isTiberium = IniParseBool(GlobalRules[name]["Tiberium"]);
                bool rubble = IniParseBool(GlobalRules[name]["IsRubble"]);
                bool wall = IniParseBool(GlobalRules[name]["Wall"]);
                string img = GlobalRules[name][KEY_IMAGE];
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
                    if (GlobalConfig.BridgeOffsetContains(overlay.OverlayFrame)) d.IsOffsetBridge = true;
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
                    filename = filename.Replace(1, 'G').ToLower() + ".shp";
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

                d.Framecount = GlobalDir.GetShpFrameCount(filename, out bool b);
                d.pSelf = CreateFile(filename, DrawableType.Shp, overlay.OverlayFrame);
                if (wall || !rubble || !isTiberium) d.pShadow = CreateFile(filename, DrawableType.Shp, overlay.OverlayFrame + d.Framecount / 2);
                Buffer.Buffers.Miscs[lookup] = d;
            }
            else d = Buffer.Buffers.Miscs[lookup];
            return d;
        }
        internal static DrawableStructure CreateDrawableStructure(string name, uint color, int pPal, int direction, bool isBaseNode = false)
        {
            DrawableStructure d;
            string lookup = string.Format("{0}{1}.in{2}", name, color, direction);
            if (isBaseNode) lookup += "n";
            if (!Buffer.Buffers.Structures.Keys.Contains(lookup))
            {
                var adjust = GlobalConfig.DrawingAdjust;
                d = new DrawableStructure(name);
                BuildingData data = GlobalRules.GetBuildingData(name);
                GlobalRules.GetBuildingShapeData(name, out int height, out int foundx, out int foundy);

                #region framecount
                d.Framecount = data.nSelf;
                d.ActivateAnimCount = data.nActivateAnim;
                d.ActivateAnim2Count = data.nActivateAnimTwo;
                d.ActivateAnim3Count = data.nActivateAnimThree;
                d.IdleAnimCount = data.nIdleAnim;
                d.BibCount = data.nBibAnim;
                d.SuperAnimCount = data.nSuperAnim;
                d.TurretAnimCount = data.nTurretAnim;
                #endregion

                d.Height = height; d.FoundationX = foundx; d.FoundationY = foundy;
                d.VoxelTurret = data.TurretAnimIsVoxel;
                d.TurretOffset = data.TurretOffset;
                d.RemapColor = color;
                d.MinimapColor = ToColor(color);

                #region ZAdjust
                d.TurretZAdjust = data.TurretAnimZAdjust;
                d.ActivateZAdjust = data.ActiveAnimZAdjust;
                d.Activate2ZAdjust = data.ActiveAnimTwoZAdjust;
                d.Activate3ZAdjust = data.ActiveAnimThreeZAdjust;
                d.IdleZAdjust = data.IdleAnimZAdjust;
                d.SuperZAdjust = data.SuperAnimZAdjust;
                d.Plug1ZAdjust = data.Plug1ZAdjust;
                d.offsetPlug1 = GlobalRules.GetPlugOffset(name, 1);
                d.Plug2ZAdjust = data.Plug2ZAdjust;
                d.offsetPlug2 = GlobalRules.GetPlugOffset(name, 2);
                d.Plug3ZAdjust = data.Plug3ZAdjust;
                d.offsetPlug3 = GlobalRules.GetPlugOffset(name, 3);
                #endregion

                string customPalName = GlobalRules.GetCustomPaletteName(name);
                if (!string.IsNullOrEmpty(customPalName))
                {
                    pPal = CreatePalette(customPalName);
                    d.pPalCustom = pPal;
                }
                if (!string.IsNullOrEmpty(data.SelfId))
                {
                    d.pSelf = CreateFile(data.SelfId, DrawableType.Shp);
                    if (!adjust.DeactivateShadow.Any(x => x.Name == name)) d.pShadow = CreateFile(data.SelfId, DrawableType.Shp, d.Framecount / 2);
                }
                if (!string.IsNullOrEmpty(data.ActivateAnim))
                {
                    d.pActivateAnim = CreateFile(data.ActivateAnim, DrawableType.Shp);
                    d.pShadowActivateAnim = CreateFile(data.ActivateAnim, DrawableType.Shp, d.ActivateAnimCount / 2);
                }
                if (!string.IsNullOrEmpty(data.ActivateAnimTwo))
                {
                    d.pActivateAnim2 = CreateFile(data.ActivateAnimTwo, DrawableType.Shp);
                    d.pShadowActivateAnim2 = CreateFile(data.ActivateAnimTwo, DrawableType.Shp, d.ActivateAnim2Count / 2);
                }
                if (!string.IsNullOrEmpty(data.ActivateAnimThree))
                {
                    d.pActivateAnim3 = CreateFile(data.ActivateAnimThree, DrawableType.Shp);
                    d.pShadowActivateAnim3 = CreateFile(data.ActivateAnimThree, DrawableType.Shp, d.ActivateAnim3Count / 2);
                }
                if (!string.IsNullOrEmpty(data.SuperAnim))
                {
                    d.pSuperAnim = CreateFile(data.SuperAnim, DrawableType.Shp);
                    d.pShadowSuperAnim = CreateFile(data.SuperAnim, DrawableType.Shp, d.SuperAnimCount / 2);
                }
                if (!string.IsNullOrEmpty(data.IdleAnim))
                {
                    d.pIdleAnim = CreateFile(data.IdleAnim, DrawableType.Shp);
                    d.pShadowIdleAnim = CreateFile(data.IdleAnim, DrawableType.Shp, d.IdleAnimCount / 2);
                }
                if (!string.IsNullOrEmpty(data.BibAnim))
                {
                    d.pBib = CreateFile(data.BibAnim, DrawableType.Shp);
                    d.pShadowBib = CreateFile(data.BibAnim, DrawableType.Shp, d.BibCount / 2);
                }
                if (!string.IsNullOrEmpty(data.AlphaImage))
                {
                    d.pAlphaImg = CreateFile(data.AlphaImage, DrawableType.Shp);
                }
                if (!string.IsNullOrEmpty(data.TurretAnim))
                {
                    d.offsetTurret = GlobalRules.GetVoxTurOffset(name);
                    if (d.VoxelTurret)
                    {
                        d.pTurretAnim = CreateFile(data.TurretAnim, DrawableType.Vxl);
                        if (!string.IsNullOrEmpty(data.TurretBarrel)) d.pTurretBarl = CreateFile(data.TurretBarrel, DrawableType.Vxl);
                    }
                    else
                    {
                        d.pTurretAnim = CreateFile(data.TurretAnim, DrawableType.Shp, direction);
                        d.pShadowTurretAnim = CreateFile(data.TurretAnim, DrawableType.Shp, direction + d.TurretAnimCount / 2);
                    }
                }

                if (data.IsEmpty) d.pSelf = CreateFile("unkObj.shp", DrawableType.Shp);
                Buffer.Buffers.Structures[lookup] = d;
            }
            else d = Buffer.Buffers.Structures[lookup];
            return d;
        }
        private static int CreateFile(string filename, DrawableType type, int shpframe = 0)
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
        private static int CreateTmp(string filename)
        {
            int id;
            if (!Buffer.Files.Tmp.Keys.Contains(filename))
            {
                if (!GlobalDir.HasFile(filename)) return 0;
                VFileInfo info = GetPtrFromGlobalDir(filename);
                id = CppExtern.Files.CreateTmpFileFromFileInMemory(info.ptr, info.size);
                CppExtern.Files.LoadTmpTextures(id);
                Buffer.Files.Tmp[filename] = id;
            }
            else id = Buffer.Files.Tmp[filename];
            return id;
        }
        private static int CreateShp(string filename, int shpframe)
        {
            int id;
            string lookup = string.Format(filename);
            if (!Buffer.Files.Shp.Keys.Contains(lookup))
            {
                if (!GlobalDir.HasFile(filename)) return 0;
                VFileInfo info = GetPtrFromGlobalDir(filename);
                id = CppExtern.Files.CreateShpFileFromFileInMemory(info.ptr, info.size);
                Buffer.Files.Shp[lookup] = id;
            }
            id = Buffer.Files.Shp[lookup];
            int frameLookup = id << 16 | shpframe;
            if (!Buffer.Files.ShpLoaded.Contains(frameLookup))
            {
                CppExtern.Files.LoadShpTextures(id, shpframe);
                Buffer.Files.ShpLoaded.Add(frameLookup);
            }
            return id;
        }
        private static int CreateVxl(string filename)
        {
            int id;
            if (!Buffer.Files.Vxl.Keys.Contains(filename))
            {
                if (!GlobalDir.HasFile(filename)) return 0;
                VFileInfo info = GetPtrFromGlobalDir(filename);
                VFileInfo hva = GetPtrFromGlobalDir(filename.Replace("vxl", "hva"));
                id = CppExtern.Files.CreateVxlFileFromFileInMemory(info.ptr, info.size, hva.ptr, hva.size);
                Buffer.Files.Vxl[filename] = id;
            }
            else id = Buffer.Files.Vxl[filename];
            return id;
        }
        private static int CreatePalette(string filename)
        {
            if (!GlobalDir.HasFile(filename)) return 0;
            int id;
            if (!Buffer.Files.Pal.Keys.Contains(filename))
            {
                VFileInfo info = GetPtrFromGlobalDir(filename);
                id = CppExtern.Files.CreatePaletteFromFileInBuffer(info.ptr);
                Buffer.Files.Pal[filename] = id;
            }
            else id = Buffer.Files.Pal[filename];
            return id;
        }
        private static VFileInfo GetPtrFromGlobalDir(string filename)
        {
            VFileInfo info = GlobalDir.GetFilePtr(filename);
            return info;
        }
        #endregion
    }
}
