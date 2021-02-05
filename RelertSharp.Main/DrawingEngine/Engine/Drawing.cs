﻿//using RelertSharp.Common;
//using RelertSharp.DrawingEngine.Drawables;
//using RelertSharp.DrawingEngine.Presenting;
//using RelertSharp.IniSystem;
//using RelertSharp.MapStructure;
//using RelertSharp.MapStructure.Objects;
//using RelertSharp.MapStructure.Points;
//using System.Drawing;
//using System.Linq;
//using static RelertSharp.Common.Constant.DrawingEngine;
//using static RelertSharp.Common.GlobalVar;
//using static RelertSharp.Utils.Misc;

//namespace RelertSharp.DrawingEngine
//{
//    public partial class Engine
//    {
//        #region Draw - Public
//        public bool DrawBrushObject(IMapObject obj, int heightFix = 0, string houseName = "")
//        {
//            switch (obj.ObjectType)
//            {
//                case MapObjectType.Infantry:
//                    return DrawObject(obj as InfantryItem, heightFix, CurrentMapDocument.Map.GetHouseColor(obj as ICombatObject));
//                case MapObjectType.Unit:
//                    return DrawObject(obj as UnitItem, heightFix, CurrentMapDocument.Map.GetHouseColor(obj as ICombatObject));
//                case MapObjectType.Building:
//                    return DrawObject(obj as StructureItem, heightFix, CurrentMapDocument.Map.GetHouseColor(obj as ICombatObject));
//                case MapObjectType.Aircraft:
//                    return DrawObject(obj as AircraftItem, heightFix, CurrentMapDocument.Map.GetHouseColor(obj as ICombatObject));
//                case MapObjectType.Terrain:
//                    return DrawGeneralItem(obj as TerrainItem, heightFix);
//                case MapObjectType.Smudge:
//                    return DrawGeneralItem(obj as SmudgeItem, heightFix);
//                case MapObjectType.Overlay:
//                    return DrawGeneralItem(obj as OverlayUnit, heightFix);
//                case MapObjectType.Celltag:
//                    return DrawCelltag(obj as CellTagItem, heightFix, true);
//                case MapObjectType.BaseNode:
//                    return DrawObject(obj as BaseNode, heightFix, CurrentMapDocument.Map.GetHouseColor(houseName));
//                case MapObjectType.Waypoint:
//                    return DrawWaypoint(obj as WaypointItem, heightFix);
//                case MapObjectType.LightSource:
//                    return DrawLightSource(obj as LightSource, heightFix);
//                default:
//                    return false;
//            }
//        }
//        public bool DrawObject(InfantryItem inf, int height, uint color)
//        {
//            int frame = GlobalRules.GetFrameFromDirection(inf.Rotation, inf.RegName);
//            DrawableInfantry src = CreateDrawableInfantry(inf, color, pPalUnit, frame);
//            PresentInfantry dest = new PresentInfantry(inf, height);
//            inf.SceneObject = dest;
//            Vec3 pos = ToVec3Iso(dest, inf.SubCells);
//            return DrawInfantry(src, pos, dest, frame, pPalUnit, inf.SubCells);
//        }
//        public bool DrawObject(UnitItem unit, int height, uint color)
//        {
//            Vec3 idx = VxlRotation(unit.RegName, unit.Rotation, false);
//            DrawableUnit src = CreateDrawableUnit(unit.RegName, color, pPalUnit, (int)idx.X);
//            PresentUnit dest = new PresentUnit(unit, height, src.IsVxl);
//            unit.SceneObject = dest;
//            Vec3 pos = ToVec3Iso(dest);
//            Vec3 ro = VxlRotation(unit.RegName, unit.Rotation, src.IsVxl);
//            return DrawUnit(src, dest, pos, ro, pPalUnit);
//        }
//        public bool DrawObject(AircraftItem air, int height, uint color)
//        {
//            DrawableUnit src = CreateDrawableUnit(air.RegName, color, pPalUnit, 0);
//            PresentUnit dest = new PresentUnit(air, height);
//            air.SceneObject = dest;
//            Vec3 pos = ToVec3Iso(dest);
//            Vec3 ro = VxlRotation(air.RegName, air.Rotation, true);
//            return DrawUnit(src, dest, pos, ro, pPalUnit);
//        }
//        public bool DrawObject(StructureItem structure, int height, uint color)
//        {
//            Vec3 idx = BuildingRotation(structure.RegName, structure.Rotation, false);
//            DrawableStructure upg1 = null, upg2 = null, upg3 = null;
//            DrawableStructure src = CreateDrawableStructure(structure.RegName, color, pPalUnit, (int)idx.X);
//            PresentStructure dest = new PresentStructure(structure, height, src.VoxelTurret, src);
//            structure.SceneObject = dest;
//            if (structure.Upgrade1 != "None") upg1 = CreateDrawableStructure(structure.Upgrade1, color, pPalUnit, (int)idx.X);
//            if (structure.Upgrade2 != "None") upg2 = CreateDrawableStructure(structure.Upgrade2, color, pPalUnit, (int)idx.X);
//            if (structure.Upgrade3 != "None") upg3 = CreateDrawableStructure(structure.Upgrade3, color, pPalUnit, (int)idx.X);
//            Vec3 ro;
//            if (src.pTurretAnim != 0) ro = BuildingRotation(structure.RegName, structure.Rotation, src.VoxelTurret);
//            else ro = Vec3.Zero;
//            Vec3 pos = ToVec3Zero(dest);
//            return DrawStructure(src, pos, dest, ro, pPalUnit, upg1, upg2, upg3);
//        }
//        public bool DrawObject(BaseNode node, int height, uint color)
//        {
//            Vec3 idx = BuildingRotation(node.RegName, 0, false);
//            DrawableStructure src = CreateDrawableStructure(node.RegName, color, pPalUnit, (int)idx.X, true);
//            PresentStructure dest = new PresentStructure(node, height, src.VoxelTurret);
//            node.SceneObject = dest;
//            Vec3 ro;
//            if (src.pTurretAnim != 0) ro = BuildingRotation(node.RegName, 0, src.VoxelTurret);
//            else ro = Vec3.Zero;
//            Vec3 pos = ToVec3Zero(dest) + OffsetTo(Offset.BaseNode);
//            bool draw = DrawStructure(src, pos, dest, ro, pPalUnit);
//            if (draw)
//            {
//                dest.SetTransparency(true);
//                dest.SetBasenodeZ();
//                return true;
//            }
//            return false;
//        }
//        public bool DrawGeneralItem(Tile t)
//        {
//            string name, framework;
//            int subindex = 0, frameworkIndex = 0;
//            Vec4 color = Vec4.Zero;
//            if (t.SceneObject != null)
//            {
//                color = t.SceneObject.ColorVector;
//            }
//            framework = TileDictionary.GetFrameworkFromTile(t, out bool isHyte);
//            t.IsHyte = isHyte;
//            name = TileDictionary[t.TileIndex];
//            subindex = TileDictionary.IsValidTile(t.TileIndex) ? t.SubIndex : 0;
//            if (isHyte) frameworkIndex = 0;
//            else frameworkIndex = TileDictionary.IsValidTile(t.TileIndex) ? t.SubIndex : 0;
//            DrawableTile src = CreateDrawableTile(name, subindex);
//            DrawableTile frm = CreateDrawableTile(framework, frameworkIndex);
//            //minimap.DrawTile(src, t, subindex);
//            Vec3 pos = ToVec3Iso(t);
//            bool success = DrawTile(src.pSelf, pos, subindex, pPalIso, out int pSelf, out int pExtra);
//            bool frmSuccess = DrawTile(frm.pSelf, pos, frameworkIndex, pPalIso, out int pfrm, out int pfrmEx);
//            PresentTile pt = new PresentTile(pSelf, pExtra, t.Height, src, subindex, t, pfrm, pfrmEx);
//            if (color != Vec4.Zero) pt.SetColor(color);
//            t.SceneObject = pt;
//            Color cl = src.SubTiles[subindex].RadarColor.Left;
//            Color cr = src.SubTiles[subindex].RadarColor.Right;
//            pt.RadarColor = new RadarColor(cl, cr);
//            t.TileTerrainType = src.SubTiles[subindex].TerrainType;
//            return success;
//        }
//        public bool DrawGeneralItem(TerrainItem terrain, int height)
//        {
//            DrawableMisc src = CreateDrawableMisc(terrain);
//            PresentMisc dest = new PresentMisc(MapObjectType.Terrain, terrain, height);
//            terrain.SceneObject = dest;
//            dest.IsZeroVec = src.IsZeroVec;
//            Vec3 pos;
//            if (src.IsZeroVec) pos = ToVec3Zero(terrain, height);
//            else pos = ToVec3Iso(terrain, height);
//            if (DrawMisc(src, dest, pos, src.pPal, 0, _white, ShpFlatType.Vertical, src.Framecount))
//            {
//                return true;
//            }
//            return false;
//        }
//        public bool DrawGeneralItem(SmudgeItem smg, int height)
//        {
//            DrawableMisc src = CreateDrawableMisc(smg);
//            PresentMisc dest = new PresentMisc(MapObjectType.Smudge, smg, height);
//            smg.SceneObject = dest;
//            dest.SmgWidth = src.SmudgeWidth;
//            dest.SmgHeight = src.SmudgeHeight;
//            Vec3 pos = ToVec3Zero(smg, height);
//            if (DrawMisc(src, dest, pos, pPalIso, 0, _white, ShpFlatType.FlatGround))
//            {
//                return true;
//            }
//            return false;
//        }
//        public bool DrawGeneralItem(OverlayUnit o, int height)
//        {
//            DrawableMisc src = CreateDrawableMisc(o, _white);
//            PresentMisc dest = new PresentMisc(MapObjectType.Overlay, o, height);
//            o.SceneObject = dest;
//            dest.IsTiberiumOverlay = src.IsTiberiumOverlay;
//            dest.IsMoveBlockingOverlay = src.IsMoveBlockingOverlay;
//            dest.IsRubble = src.IsRubble;
//            dest.IsWall = src.IsWall;
//            dest.IsHiBridge = src.IsHiBridge;
//            dest.IsZeroVec = src.IsZeroVec;
//            Vec3 pos;
//            if (src.IsZeroVec) pos = ToVec3Zero(o, height);
//            else pos = ToVec3Iso(o, height);
//            ShpFlatType type = src.FlatType;
//            if (DrawMisc(src, dest, pos, src.pPal, o.Frame, _white, type, src.Framecount))
//            {
//                return true;
//            }
//            return false;
//        }
//        public bool DrawWaypoint(WaypointItem waypoint, int height)
//        {
//            DrawableMisc src = new DrawableMisc(MapObjectType.Waypoint, waypoint.Num);
//            src.pSelf = Buffer.Files.WaypointBase;
//            PresentMisc dest = new PresentMisc(MapObjectType.Waypoint, waypoint, height);
//            waypoint.SceneObject = dest;
//            Vec3 pos = ToVec3Iso(dest).Rise() + WaypointHeightMultiplier * _generalOffset;
//            if (DrawMisc(src, dest, pos, pPalSystem, 0, _white, ShpFlatType.Vertical) && DrawWaypointNum(dest, waypoint.Num, pos))
//            {
//                CppExtern.ObjectUtils.SetObjectZAdjust(dest.pSelf, ZAdjust.Waypoint);
//                dest.WaypointNums.ForEach(x => CppExtern.ObjectUtils.SetObjectZAdjust(x, ZAdjust.Waypoint));
//                return true;
//            }
//            return false;
//        }
//        public bool DrawCelltag(CellTagItem cell, int height, bool topmost = false)
//        {
//            DrawableMisc src = new DrawableMisc(MapObjectType.Celltag, "");
//            src.pSelf = Buffer.Files.CelltagBase;
//            PresentMisc dest = new PresentMisc(MapObjectType.Celltag, cell, height);
//            cell.SceneObject = dest;
//            Vec3 pos = ToVec3Iso(dest);
//            if (topmost) pos += CelltagHeightMultiplier * _generalOffset;
//            if (DrawMisc(src, dest, pos, pPalSystem, 0, _white, ShpFlatType.FlatGround))
//            {
//                if (topmost) CppExtern.ObjectUtils.SetObjectZAdjust(dest.pSelf, ZAdjust.CellTag);
//                return true;
//            }
//            return false;
//        }
//        public bool DrawLightSource(LightSource light, int height)
//        {
//            DrawableMisc src = new DrawableMisc(MapObjectType.LightSource, "");
//            src.pSelf = Buffer.Files.LightSourceBase;
//            PresentMisc dest = new PresentMisc(MapObjectType.LightSource, light, height);
//            light.SceneObject = dest;
//            Vec3 pos = ToVec3Iso(dest) + LightSourceHeightMultiplier * _generalOffset;
//            return DrawMisc(src, dest, pos, pPalSystem, 0, _white, ShpFlatType.FlatGround);
//        }
//        #endregion


//        #region Draw - Private
//        private bool DrawWaypointNum(PresentMisc dest, string waypointNum, Vec3 pos)
//        {
//            int width = waypointNum.Count() * WaypointNumWidth;
//            pos = MoveVertical(MoveHorizontal(pos, -1 * (width / 2 - 2)), 10);
//            bool result = false;
//            foreach (char c in waypointNum)
//            {
//                int num = int.Parse(c.ToString());
//                int id = RenderAndPresent(Buffer.WaypointNum[num], pos.Rise(), num, _white, pPalSystem, ShpFlatType.Vertical, Vec3.Zero);
//                result |= (id != 0);
//                dest.WaypointNums.Add(id);
//                pos = MoveHorizontal(pos, WaypointNumWidth);
//            }
//            return result;
//        }
//        private bool DrawMisc(DrawableMisc src, PresentMisc dest, Vec3 pos, int pPal, int frame, uint color, ShpFlatType type = ShpFlatType.Vertical, short shadow = 0)
//        {
//            if (src.pSelf != 0)
//            {
//                Vec3 box = Vec3.DefaultBox;
//                if (src.IsOffsetBridge) pos = ToVec3Zero(pos);
//                if (src.IsHiBridge)
//                {
//                    type = ShpFlatType.FlatGround;
//                }
//                Vec3 selfPos = pos;
//                if (src.MiscType == MapObjectType.Smudge) selfPos += OffsetTo(Offset.Smudge);
//                else selfPos += OffsetTo(Offset.Self);
//                dest.pSelf = RenderAndPresent(src.pSelf, selfPos, frame, color, pPal, type, box);
//                if (!src.IsTiberiumOverlay &&
//                    src.MiscType != MapObjectType.Smudge &&
//                    src.MiscType != MapObjectType.Celltag &&
//                    !src.IsFlatOnly) dest.pSelfShadow = RenderAndPresent(src.pShadow, pos + OffsetTo(Offset.ShadowSelf), frame + shadow / 2, color, pPal, ShpFlatType.FlatGround, box, ShaderType.Shadow);
//                //if (src.MiscType == MapObjectType.Waypoint) dest.pWpNum = CppExtern.ObjectUtils.CreateStringObjectAtScene(pos.MoveX(_15SQ2 * -1), 0x0000FFFF, src.NameID);
//            }
//            if (dest.IsValid)
//            {
//                //minimap.DrawMisc(src, dest);
//                if (src.IsHiBridge) CppExtern.ObjectUtils.SetObjectZAdjust(dest.pSelf, ZAdjust.HiBridgeZAdjust);
//                CppExtern.ObjectUtils.SetObjectZAdjust(dest.pSelfShadow, ZAdjust.Shadow);
//                dest.RadarColor = new RadarColor(src.RadarColor);
//            }
//            return dest.IsValid;
//        }
//        private bool DrawInfantry(DrawableInfantry src, Vec3 pos, PresentInfantry dest, int frame, int pPal, int subcell)
//        {
//            if (src.pPalCustom != 0) pPal = src.pPalCustom;
//            if (src.pSelf != 0)
//            {
//                dest.pSelf = RenderAndPresent(src.pSelf, pos + OffsetTo(Offset.Self), frame, src.RemapColor, pPal, ShpFlatType.Vertical, Vec3.DefaultBox);
//                dest.pSelfShadow = RenderAndPresent(src.pShadow, pos + OffsetTo(Offset.ShadowSelf), frame + src.Framecount / 2, src.RemapColor, pPal, ShpFlatType.FlatGround, Vec3.DefaultBox, ShaderType.Shadow);
//            }
//            if (dest.IsValid)
//            {
//                //minimap.DrawObject(src, dest, out Color c);
//                dest.RadarColor = new RadarColor(ToColor(src.RemapColor));
//            }
//            return dest.IsValid;
//        }
//        private bool DrawStructure(DrawableStructure src, Vec3 pos, PresentStructure dest, Vec3 turRotation, int pPal, DrawableStructure upg1 = null, DrawableStructure upg2 = null, DrawableStructure upg3 = null)
//        {
//            ShpFlatType flat = ShpFlatType.Box1;
//            if (src.pPalCustom != 0) pPal = src.pPalCustom;
//            if (src.pSelf != 0)
//            {
//                dest.pSelf = RenderAndPresent(src, src.pSelf, pos + OffsetTo(Offset.Self), pPal, flat);
//                dest.pSelfShadow = RenderAndPresent(src, src.pShadow, pos + OffsetTo(Offset.ShadowSelf), pPal, ShpFlatType.FlatGround, src.Framecount, ShaderType.Shadow);
//            }
//            if (src.pActivateAnim != 0)
//            {
//                dest.pActivateAnim = RenderAndPresent(src, src.pActivateAnim, pos + OffsetTo(Offset.AAnim1), pPal, flat);
//                dest.pActivateAnimShadow = RenderAndPresent(src, src.pShadowActivateAnim, pos + OffsetTo(Offset.ShadowAAnim1), pPal, ShpFlatType.FlatGround, src.ActivateAnimCount, ShaderType.Shadow);
//            }
//            if (src.pIdleAnim != 0)
//            {
//                dest.pIdleAnim = RenderAndPresent(src, src.pIdleAnim, pos + OffsetTo(Offset.Idle), pPal, flat);
//                dest.pIdleAnimShadow = RenderAndPresent(src, src.pShadowIdleAnim, pos + OffsetTo(Offset.ShadowIdle), pPal, ShpFlatType.FlatGround, src.IdleAnimCount, ShaderType.Shadow);
//            }
//            if (src.pActivateAnim2 != 0)
//            {
//                dest.pActivateAnim2 = RenderAndPresent(src, src.pActivateAnim2, pos + OffsetTo(Offset.AAnim2), pPal, flat);
//                dest.pActivateAnim2Shadow = RenderAndPresent(src, src.pShadowActivateAnim2, pos + OffsetTo(Offset.ShadowAAnim2), pPal, ShpFlatType.FlatGround, src.ActivateAnim2Count, ShaderType.Shadow);
//            }
//            if (src.pActivateAnim3 != 0)
//            {
//                dest.pActivateAnim3 = RenderAndPresent(src, src.pActivateAnim3, pos + OffsetTo(Offset.AAnim3), pPal, ShpFlatType.Vertical);
//                dest.pActivateAnim3Shadow = RenderAndPresent(src, src.pShadowActivateAnim3, pos + OffsetTo(Offset.ShadowAAnim3), pPal, ShpFlatType.FlatGround, src.ActivateAnim3Count, ShaderType.Shadow);
//            }
//            if (src.pSuperAnim != 0)
//            {
//                dest.pSuperAnim = RenderAndPresent(src, src.pSuperAnim, pos + OffsetTo(Offset.Super), pPal, flat);
//                dest.pSuperAnimShadow = RenderAndPresent(src, src.pShadowSuperAnim, pos + OffsetTo(Offset.ShadowSuper), pPal, ShpFlatType.FlatGround, src.SuperAnimCount, ShaderType.Shadow);
//            }
//            if (src.pBib != 0)
//            {
//                dest.pBib = RenderAndPresent(src, src.pBib, pos + OffsetTo(Offset.Bib), pPal, flat);
//                dest.pBibShadow = RenderAndPresent(src, src.pShadowBib, pos + OffsetTo(Offset.ShadowBib), pPal, ShpFlatType.FlatGround, src.BibCount, ShaderType.Shadow);
//            }
//            if (src.pTurretAnim != 0)
//            {
//                if (src.VoxelTurret)
//                {
//                    Vec3 turret = pos + src.offsetTurret;
//                    RenderAndPresent(src.pTurretAnim, turret + OffsetTo(Offset.Turret), turRotation, src.RemapColor, pPal, out int vid, out int sid, turret + OffsetTo(Offset.ShadowTurret));
//                    dest.pTurretAnimShadow = sid;
//                    dest.pTurretAnim = vid;
//                    if (src.pTurretBarl != 0)
//                    {
//                        RenderAndPresent(src.pTurretBarl, turret + OffsetTo(Offset.Barrel), turRotation, src.RemapColor, pPal, out int turBarrel, out int turBarrShadow, turret + OffsetTo(Offset.ShadowBarrel));
//                        dest.pTurretBarl = turBarrel;
//                        dest.pTurretBarlShadow = turBarrShadow;
//                    }
//                    SetVxlZAdjust(0, 0, dest.pTurretAnim, dest.pTurretAnimShadow, dest.pTurretBarl, dest.pTurretBarlShadow);

//                }
//                else
//                {
//                    dest.pTurretAnim = RenderAndPresent(src.pTurretAnim, pos + src.offsetTurret + OffsetTo(Offset.Turret), (int)turRotation.X, src.RemapColor, pPal, ShpFlatType.Vertical, Vec3.DefaultBox);
//                    dest.pTurretAnimShadow = RenderAndPresent(src.pShadowTurretAnim, pos + src.offsetTurret + OffsetTo(Offset.ShadowTurret), (int)turRotation.X + src.TurretAnimCount / 2, src.RemapColor, pPal, ShpFlatType.FlatGround, Vec3.DefaultBox, ShaderType.Shadow);
//                }
//            }
//            if (src.pAlphaImg != 0)
//            {
//                Vec3 alphapos = ToVec3Zero(dest.X + src.FoundationX / 2f, dest.Y + src.FoundationY / 2f, (float)dest.Z);
//                dest.pAlphaImg = RenderAndPresent(src, src.pAlphaImg, alphapos, pPal, ShpFlatType.Vertical, 0, ShaderType.Alpha);
//            }
//            if (upg1 != null)
//            {
//                if (upg1.pSelf != 0)
//                {
//                    dest.pPlug1 = RenderAndPresent(src, upg1.pSelf, pos + OffsetTo(Offset.Plug1), pPal, flat);
//                    dest.pPlug1Shadow = RenderAndPresent(src, upg1.pShadow, pos + OffsetTo(Offset.ShadowPlug1), pPal, ShpFlatType.FlatGround, upg1.Framecount, ShaderType.Shadow);
//                }
//            }
//            if (upg2 != null)
//            {
//                if (upg2.pSelf != 0)
//                {
//                    dest.pPlug2 = RenderAndPresent(src, upg2.pSelf, pos + OffsetTo(Offset.Plug2), pPal, flat);
//                    dest.pPlug2Shadow = RenderAndPresent(src, upg2.pShadow, pos + OffsetTo(Offset.ShadowPlug2), pPal, ShpFlatType.FlatGround, upg2.Framecount, ShaderType.Shadow);
//                }
//            }
//            if (upg3 != null)
//            {
//                if (upg3.pSelf != 0)
//                {
//                    dest.pPlug3 = RenderAndPresent(src, upg3.pSelf, pos + OffsetTo(Offset.Plug3), pPal, flat);
//                    dest.pPlug3Shadow = RenderAndPresent(src, upg3.pShadow, pos + OffsetTo(Offset.ShadowPlug3), pPal, ShpFlatType.FlatGround, upg3.Framecount, ShaderType.Shadow);
//                }
//            }
//            if (dest.IsValid)
//            {
//                //minimap.DrawStructure(src, dest, dest.IsBaseNode);
//                dest.RadarColor = new RadarColor(src.MinimapColor);
//            }
//            SetBuildingZAdjust(dest, src);
//            //dest.SetZAdjust();
//            return dest.IsValid;
//        }
//        private bool DrawUnit(DrawableUnit src, PresentUnit dest, Vec3 pos, Vec3 rotation, int pPal)
//        {
//            if (src.pPalCustom != 0) pPal = src.pPalCustom;
//            if (src.IsVxl)
//            {
//                if (src.pSelf != 0)
//                {
//                    RenderAndPresent(src.pSelf, pos + OffsetTo(Offset.Self), rotation, src.RemapColor, pPal, out int selfId, out int selfShadow, pos + OffsetTo(Offset.ShadowSelf));
//                    dest.pSelf = selfId;
//                    dest.pSelfShadow = selfShadow;
//                }
//                if (src.pBarrel != 0)
//                {
//                    RenderAndPresent(src.pBarrel, pos + OffsetTo(Offset.Barrel), rotation, src.RemapColor, pPal, out int barlId, out int barlShadow, pos + OffsetTo(Offset.ShadowBarrel));
//                    dest.pBarrel = barlId;
//                    dest.pBarrelShadow = barlShadow;
//                }
//                if (src.pTurret != 0)
//                {
//                    RenderAndPresent(src.pTurret, pos + OffsetTo(Offset.Turret), rotation, src.RemapColor, pPal, out int turId, out int turShadow, pos + OffsetTo(Offset.ShadowTurret));
//                    dest.pTurret = turId;
//                    dest.pTurretShadow = turShadow;
//                }
//            }
//            else
//            {
//                if (src.pSelf != 0)
//                {
//                    dest.pSelf = RenderAndPresent(src.pSelf, pos + OffsetTo(Offset.Self), (int)rotation.X, src.RemapColor, pPal, ShpFlatType.Vertical, Vec3.DefaultBox);
//                    dest.pSelfShadow = RenderAndPresent(src.pSelf, pos + OffsetTo(Offset.ShadowSelf), (int)rotation.X + src.Framecount / 2, src.RemapColor, pPal, ShpFlatType.FlatGround, Vec3.DefaultBox, ShaderType.Shadow);
//                }
//            }
//            if (dest.IsValid)
//            {
//                //minimap.DrawObject(src, dest, out Color c);
//                dest.SetUnitShadowZAdjust();
//                dest.RadarColor = new RadarColor(ToColor(src.RemapColor));
//            }
//            return dest.IsValid;
//        }
//        private bool DrawTile(int idTmp, Vec3 pos, int subindex, int pPal, out int idSelf, out int idExtra)
//        {
//            idSelf = 0; idExtra = 0;
//            return CppExtern.ObjectUtils.CreateTmpObjectAtScene(idTmp, pos + OffsetTo(Offset.Ground), pPal, subindex, ref idSelf, ref idExtra);
//        }
//        private int RenderAndPresent(int shpID, Vec3 pos, int frame, uint color, int pPal, ShpFlatType flat, Vec3 boxsize, ShaderType shade = ShaderType.Normal)
//        {
//            if (flat == ShpFlatType.Box1)
//            {
//                pos.Z += 0.1F;
//                return CppExtern.ObjectUtils.CreateShpObjectAtScene(shpID, pos, frame, pPal, color, (int)flat, (int)boxsize.X, (int)boxsize.Y, (int)boxsize.Z, (byte)shade);
//            }
//            else return CppExtern.ObjectUtils.CreateShpObjectAtScene(shpID, pos, frame, pPal, color, (int)flat, 0, 0, 0, (byte)shade);
//        }
//        private int RenderAndPresent(DrawableStructure src, int id, Vec3 pos, int pPal, ShpFlatType flat, short framecount = 0, ShaderType shade = ShaderType.Normal)
//        {
//            return CppExtern.ObjectUtils.CreateShpObjectAtScene(id, pos, framecount / 2, pPal, src.RemapColor, (int)flat, src.FoundationX, src.FoundationY, src.Height, (byte)shade);
//        }
//        /// <summary>
//        /// obsolete
//        /// </summary>
//        /// <param name="vxlID"></param>
//        /// <param name="pos"></param>
//        /// <param name="ro"></param>
//        /// <param name="color"></param>
//        /// <param name="pPal"></param>
//        /// <returns></returns>
//        private int RenderAndPresent(int vxlID, Vec3 pos, Vec3 ro, uint color, int pPal)
//        {
//            return CppExtern.ObjectUtils.CreateVxlObjectAtScene(vxlID, pos, ro.X, ro.Y, ro.Z, pPal, color);
//        }
//        private void RenderAndPresent(int vxlFile, Vec3 pos, Vec3 rotation, uint color, int pPal, out int vxlId, out int shadowId, Vec3 shadowPos = default)
//        {
//            if (shadowPos == Vec3.Zero) shadowPos = pos;
//            vxlId = 0; shadowId = 0;
//            CppExtern.ObjectUtils.CreateVxlObjectCached(vxlFile, pos, shadowPos, rotation.Z, pPal, color, ref vxlId, ref shadowId);
//        }
//        private void SetVxlZAdjust(int self, int selfShadow, int turret = 0, int turrShadow = 0, int barl = 0, int barlShadow = 0)
//        {
//            //CppExtern.ObjectUtils.SetObjectZAdjust(self, -3);
//            //if (selfShadow != 0) CppExtern.ObjectUtils.SetObjectZAdjust(selfShadow, 5);
//            //if (turret != 0) CppExtern.ObjectUtils.SetObjectZAdjust(turret, -5);
//            //if (turrShadow != 0) CppExtern.ObjectUtils.SetObjectZAdjust(turrShadow, 10);
//            //if (barl != 0) CppExtern.ObjectUtils.SetObjectZAdjust(barl, -10);
//            //if (barlShadow != 0) CppExtern.ObjectUtils.SetObjectZAdjust(barlShadow, 15);
//        }
//        /// <summary>
//        /// Currently disable
//        /// </summary>
//        /// <param name="dest"></param>
//        /// <param name="src"></param>
//        private void SetBuildingZAdjust(PresentStructure dest, DrawableStructure src)
//        {
//            dest.SetShadowZAdjust();
//            if (dest.pTurretAnim != 0) CppExtern.ObjectUtils.SetObjectZAdjust(dest.pTurretAnim, src.TurretZAdjust);
//            if (dest.pTurretBarl != 0) CppExtern.ObjectUtils.SetObjectZAdjust(dest.pTurretBarl, src.TurretZAdjust);
//            if (dest.pActivateAnim != 0) CppExtern.ObjectUtils.SetObjectZAdjust(dest.pActivateAnim, src.ActivateZAdjust);
//            if (dest.pActivateAnim2 != 0) CppExtern.ObjectUtils.SetObjectZAdjust(dest.pActivateAnim2, src.Activate2ZAdjust);
//            if (dest.pActivateAnim3 != 0) CppExtern.ObjectUtils.SetObjectZAdjust(dest.pActivateAnim3, src.Activate3ZAdjust);
//            if (dest.pSuperAnim != 0) CppExtern.ObjectUtils.SetObjectZAdjust(dest.pSuperAnim, src.SuperZAdjust);
//            if (dest.pIdleAnim != 0) CppExtern.ObjectUtils.SetObjectZAdjust(dest.pIdleAnim, src.IdleZAdjust);
//        }
//        private Vec3 BuildingRotation(string nameid, int facing, bool isVxl)
//        {
//            facing >>= 5;
//            if (isVxl)
//            {
//                return new Vec3() { X = 0, Y = 0, Z = (facing - 2) * _rad45 };
//            }
//            else
//            {
//                if (facing == 7) facing = 0;
//                else facing = 7 - facing;
//                INIEntity turret = GlobalRules.GetBuildingTurret(nameid);
//                int maxnum = turret.ParseInt("LoopEnd") - turret.ParseInt("LoopStart") + 1;
//                if (maxnum < 8) return Vec3.Zero;
//                int increse = maxnum >> 3;
//                return new Vec3() { X = facing * increse, Y = 0, Z = 0 };
//            }
//        }
//        private Vec3 VxlRotation(string nameID, int facing, bool isVxl)
//        {
//            if (isVxl)
//            {
//                facing >>= 5;
//                return new Vec3() { X = 0, Y = 0, Z = (facing - 2) * _rad45 };
//            }
//            else
//            {
//                int facingBase = 256 / GlobalRules[nameID].ParseInt("Facings", 8);
//                int direction = facing / facingBase;
//                if (direction == 7) direction = 0;
//                else direction++;
//                int startFrame = GlobalRules[nameID].ParseInt("StartWalkFrame", -1);
//                int walkFrame = GlobalRules[nameID].ParseInt("WalkFrames", 12);
//                int offset = walkFrame - 1;
//                return new Vec3() { X = startFrame + direction * walkFrame + offset, Y = 0, Z = 0 };
//            }
//        }
//        private Vec3 OffsetTo(float multiply)
//        {
//            return _generalOffset * multiply;
//        }
//        #endregion
//    }
//}
