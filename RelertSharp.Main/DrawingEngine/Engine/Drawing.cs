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
        #region Draw - Public
        public bool DrawObject(InfantryItem inf, int height, uint color)
        {
            int frame = GlobalRules.GetFrameFromDirection(inf.Rotation, inf.RegName);
            DrawableInfantry src = CreateDrawableInfantry(inf, color, pPalUnit, frame);
            PresentInfantry dest = new PresentInfantry(inf, height);
            Vec3 pos = ToVec3Iso(dest, inf.SubCells);
            return DrawInfantry(src, pos, dest, frame, pPalUnit, inf.SubCells);
        }
        public bool DrawObject(UnitItem unit, int height, uint color)
        {
            Vec3 idx = VxlRotation(unit.RegName, unit.Rotation, false);
            DrawableUnit src = CreateDrawableUnit(unit.RegName, color, pPalUnit, (int)idx.X);
            PresentUnit dest = new PresentUnit(unit, height, src.IsVxl);
            Vec3 pos = ToVec3Iso(dest);
            Vec3 ro = VxlRotation(unit.RegName, unit.Rotation, src.IsVxl);
            return DrawUnit(src, dest, pos, ro, pPalUnit);
        }
        public bool DrawObject(AircraftItem air, int height, uint color)
        {
            DrawableUnit src = CreateDrawableUnit(air.RegName, color, pPalUnit, 0);
            PresentUnit dest = new PresentUnit(air, height);
            Vec3 pos = ToVec3Iso(dest);
            Vec3 ro = VxlRotation(air.RegName, air.Rotation, true);
            return DrawUnit(src, dest, pos, ro, pPalUnit);
        }
        public bool DrawObject(StructureItem structure, int height, uint color)
        {
            Vec3 idx = BuildingRotation(structure.RegName, structure.Rotation, false);
            DrawableStructure src = CreateDrawableStructure(structure.RegName, color, pPalUnit, (int)idx.X);
            PresentStructure dest = new PresentStructure(structure, height, src.VoxelTurret, src);
            Vec3 ro;
            if (src.pTurretAnim != 0) ro = BuildingRotation(structure.RegName, structure.Rotation, src.VoxelTurret);
            else ro = Vec3.Zero;
            Vec3 pos = ToVec3Zero(dest).Rise();
            return DrawStructure(src, pos, dest, ro, pPalUnit);
        }
        public bool DrawObject(BaseNode node, int height, uint color)
        {
            Vec3 idx = BuildingRotation(node.RegName, 0, false);
            DrawableStructure src = CreateDrawableStructure(node.RegName, color, pPalUnit, (int)idx.X, true);
            PresentStructure dest = new PresentStructure(node, height, src.VoxelTurret);
            Vec3 ro;
            if (src.pTurretAnim != 0) ro = BuildingRotation(node.RegName, 0, src.VoxelTurret);
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
        public bool DrawGeneralItem(Tile t, bool isFramework = false)
        {
            string name;
            int subindex;
            if (isFramework)
            {
                name = "framework.tem";
                subindex = 0;
            }
            else
            {
                name = TileDictionary[t.TileIndex];
                subindex = t.SubIndex;
            }
            DrawableTile src = CreateDrawableTile(name, subindex);
            Buffer.Scenes.DrawableTiles[t.Coord] = src;
            minimap.DrawTile(src, t, subindex);
            Vec3 pos = ToVec3Iso(t);
            if (DrawTile(src.pSelf, pos, subindex, pPalIso, out int pSelf, out int pExtra))
            {
                PresentTile pt = new PresentTile(pSelf, pExtra, t.Height, src, subindex, t);
                Color cl = src.SubTiles[subindex].RadarColor.Left;
                Color cr = src.SubTiles[subindex].RadarColor.Right;
                pt.RadarColor = new RadarColor(cl, cr);
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
                Buffer.Scenes.AddTerrainAt(terrain.Coord, dest);
                return true;
            }
            return false;
        }
        public bool DrawGeneralItem(SmudgeItem smg, int height)
        {
            DrawableMisc src = CreateDrawableMisc(smg);
            PresentMisc dest = new PresentMisc(MapObjectType.Smudge, smg, height);
            Vec3 pos = ToVec3Zero(smg, height);
            if (DrawMisc(src, dest, pos, pPalIso, 0, _white, ShpFlatType.FlatGround))
            {
                Buffer.Scenes.Smudges[smg.Coord] = dest;
                return true;
            }
            return false;
        }
        public bool DrawGeneralItem(OverlayUnit o, int height)
        {
            DrawableMisc src = CreateDrawableMisc(o, _white);
            PresentMisc dest = new PresentMisc(MapObjectType.Overlay, o, height);
            dest.IsTiberiumOverlay = src.IsTiberiumOverlay;
            dest.IsMoveBlockingOverlay = src.IsMoveBlockingOverlay;
            dest.IsRubble = src.IsRubble;
            dest.IsWall = src.IsWall;
            dest.IsHiBridge = src.IsHiBridge;
            Vec3 pos;
            if (src.IsZeroVec) pos = ToVec3Zero(o, height);
            else pos = ToVec3Iso(o, height);
            int pal = src.pPal;
            ShpFlatType type = src.FlatType;
            if (DrawMisc(src, dest, pos, src.pPal, o.Frame, _white, type, src.Framecount))
            {
                Buffer.Scenes.AddOverlayAt(o.Coord, dest);
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
            if (DrawMisc(src, dest, pos, pPalSystem, 0, _white, ShpFlatType.Vertical) && DrawWaypointNum(dest, waypoint.Num, pos))
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
            if (DrawMisc(src, dest, pos, pPalSystem, 0, _white, ShpFlatType.FlatGround))
            {
                Buffer.Scenes.Celltags[dest.Coord] = dest;
                return true;
            }
            return false;
        }
        #endregion


        #region Draw - Private
        private bool DrawWaypointNum(PresentMisc dest, string waypointNum, Vec3 pos)
        {
            int width = waypointNum.Count() * 12;
            pos = MoveVertical(MoveHorizontal(pos, -1 * (width / 2 - 2)), 10);
            bool result = false;
            foreach (char c in waypointNum)
            {
                int num = int.Parse(c.ToString());
                int id = RenderAndPresent(Buffer.WaypointNum[num], pos.Rise(), num, _white, pPalSystem, ShpFlatType.Vertical, Vec3.Zero);
                result = result | (id != 0);
                dest.WaypointNums.Add(id);
                pos = MoveHorizontal(pos, 12);
            }
            return result;
        }
        private bool DrawMisc(DrawableMisc src, PresentMisc dest, Vec3 pos, int pPal, int frame, uint color, ShpFlatType type = ShpFlatType.Vertical, short shadow = 0)
        {
            if (src.pSelf != 0)
            {
                Vec3 box = Vec3.DefaultBox;
                if (src.IsOffsetBridge) pos = ToVec3Zero(pos);
                if (src.IsHiBridge)
                {
                    type = ShpFlatType.Vertical;
                }
                if (type == ShpFlatType.FlatGround) pos = pos.Rise();
                dest.pSelf = RenderAndPresent(src.pSelf, pos, frame, color, pPal, type, box);
                if (!src.IsTiberiumOverlay &&
                    src.MiscType != MapObjectType.Smudge &&
                    src.MiscType != MapObjectType.Celltag &&
                    !src.IsFlatOnly) dest.pSelfShadow = RenderAndPresent(src.pShadow, pos.Rise(), frame + shadow / 2, color, pPal, ShpFlatType.FlatGround, box, true);
                //if (src.MiscType == MapObjectType.Waypoint) dest.pWpNum = CppExtern.ObjectUtils.CreateStringObjectAtScene(pos.MoveX(_15SQ2 * -1), 0x0000FFFF, src.NameID);
            }
            if (dest.IsValid)
            {
                minimap.DrawMisc(src, dest);
                dest.RadarColor = new RadarColor(src.RadarColor);
            }
            return dest.IsValid;
        }
        private bool DrawInfantry(DrawableInfantry src, Vec3 pos, PresentInfantry dest, int frame, int pPal, int subcell)
        {
            if (src.pPalCustom != 0) pPal = src.pPalCustom;
            if (src.pSelf != 0)
            {
                dest.pSelf = RenderAndPresent(src.pSelf, pos, frame, src.RemapColor, pPal, ShpFlatType.Vertical, Vec3.DefaultBox);
                dest.pSelfShadow = RenderAndPresent(src.pShadow, pos.Rise(), frame + src.Framecount / 2, src.RemapColor, pPal, ShpFlatType.FlatGround, Vec3.DefaultBox, true);
            }
            Buffer.Scenes.AddInfantryAt(dest.Coord, dest, subcell);
            if (dest.IsValid)
            {
                minimap.DrawObject(src, dest, out Color c);
                dest.RadarColor = new RadarColor(c);
            }
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
                dest.pActivateAnimShadow = RenderAndPresent(src, src.pShadowActivateAnim, pos.Rise() + _generalOffset, pPal, ShpFlatType.FlatGround, src.ActivateAnimCount, true);
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
                    dest.pTurretAnim = RenderAndPresent(src.pTurretAnim, pos + src.offsetTurret, (int)turRotation.X, src.RemapColor, pPal, ShpFlatType.Vertical, Vec3.DefaultBox);
                    dest.pTurretAnimShadow = RenderAndPresent(src.pShadowTurretAnim, pos.Rise() + src.offsetTurret, (int)turRotation.X + src.TurretAnimCount / 2, src.RemapColor, pPal, ShpFlatType.FlatGround, Vec3.DefaultBox, true);
                }
            }
            Buffer.Scenes.AddBuildingAt(dest.Coord, dest, dest.IsBaseNode);
            if (dest.IsValid)
            {
                minimap.DrawStructure(src, dest, dest.IsBaseNode);
                dest.RadarColor = new RadarColor(src.MinimapColor);
            }
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
                    dest.pSelf = RenderAndPresent(src.pSelf, pos, (int)rotation.X, src.RemapColor, pPal, ShpFlatType.Vertical, Vec3.DefaultBox);
                    dest.pSelfShadow = RenderAndPresent(src.pSelf, pos.Rise(), (int)rotation.X + src.Framecount / 2, src.RemapColor, pPal, ShpFlatType.FlatGround, Vec3.DefaultBox, true);
                }
            }
            Buffer.Scenes.AddUnitAt(dest.Coord, dest);
            if (dest.IsValid)
            {
                minimap.DrawObject(src, dest, out Color c);
                dest.RadarColor = new RadarColor(c);
            }
            return dest.IsValid;
        }
        private bool DrawTile(int idTmp, Vec3 pos, int subindex, int pPal, out int idSelf, out int idExtra)
        {
            idSelf = 0; idExtra = 0;
            return CppExtern.ObjectUtils.CreateTmpObjectAtScene(idTmp, pos, pPal, subindex, ref idSelf, ref idExtra);
        }
        private int RenderAndPresent(int shpID, Vec3 pos, int frame, uint color, int pPal, ShpFlatType flat, Vec3 boxsize, bool shadow = false)
        {
            if (flat == ShpFlatType.Box1)
            {
                pos.Z += 0.1F;
                return CppExtern.ObjectUtils.CreateShpObjectAtScene(shpID, pos, frame, pPal, color, (int)flat, (int)boxsize.X, (int)boxsize.Y, (int)boxsize.Z, shadow);
            }
            else return CppExtern.ObjectUtils.CreateShpObjectAtScene(shpID, pos, frame, pPal, color, (int)flat, 0, 0, 0, shadow);
        }
        private int RenderAndPresent(DrawableStructure src, int id, Vec3 pos, int pPal, ShpFlatType flat, short framecount = 0, bool shadow = false)
        {
            return CppExtern.ObjectUtils.CreateShpObjectAtScene(id, pos, framecount / 2, pPal, src.RemapColor, (int)flat, src.FoundationX, src.FoundationY, src.Height, shadow);
        }
        private int RenderAndPresent(int vxlID, Vec3 pos, Vec3 ro, uint color, int pPal)
        {
            return CppExtern.ObjectUtils.CreateVxlObjectAtScene(vxlID, pos, ro.X, ro.Y, ro.Z, pPal, color);
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
                int offset = walkFrame - 1;
                return new Vec3() { X = startFrame + direction * walkFrame + offset, Y = 0, Z = 0 };
            }
        }
        #endregion
    }
}
