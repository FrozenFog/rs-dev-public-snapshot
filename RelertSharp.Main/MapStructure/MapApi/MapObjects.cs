using RelertSharp.Common;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.MapStructure
{
    public static partial class MapApi
    {
        #region Add
        private static void AddToTile(IMapObject obj)
        {
            switch (obj.ObjectType)
            {
                case MapObjectType.Building:
                    StructureItem bud = obj as StructureItem;
                    foreach (I2dLocateable pos in new Foundation2D(bud)) Map.TilesData.AddObjectOnTile(pos, obj);
                    break;
                case MapObjectType.Smudge:
                    SmudgeItem smg = obj as SmudgeItem;
                    foreach (I2dLocateable pos in new Square2D(smg, smg.SizeX, smg.SizeY)) Map.TilesData.AddObjectOnTile(pos, obj);
                    break;
                default:
                    Map.TilesData.AddObjectOnTile(obj);
                    break;
            }
        }
        public static StructureItem AddBuilding(IMapObjectBrushConfig config)
        {
            StructureItem bud = new StructureItem(config.RegName);
            bud.ApplyConfig(config);
            Map.Buildings.AddItem(bud);
            AddToTile(bud);
            return bud;
        }
        public static InfantryItem AddInfantry(IMapObjectBrushConfig config)
        {
            InfantryItem inf = new InfantryItem();
            inf.ApplyConfig(config);
            Map.Infantries.AddItem(inf);
            AddToTile(inf);
            return inf;
        }
        public static UnitItem AddUnit(IMapObjectBrushConfig config)
        {
            UnitItem unit = new UnitItem();
            unit.ApplyConfig(config);
            Map.Units.AddItem(unit);
            AddToTile(unit);
            return unit;
        }
        public static AircraftItem AddAircraft(IMapObjectBrushConfig config)
        {
            AircraftItem air = new AircraftItem();
            air.ApplyConfig(config);
            Map.Aircrafts.AddItem(air);
            AddToTile(air);
            return air;
        }
        public static SmudgeItem AddSmudge(IMapObjectBrushConfig config)
        {
            SmudgeItem smg = new SmudgeItem();
            smg.ApplyConfig(config);
            Map.Smudges.AddObject(smg);
            AddToTile(smg);
            return smg;
        }
        public static TerrainItem AddTerrain(IMapObjectBrushConfig config)
        {
            TerrainItem terr = new TerrainItem();
            terr.ApplyConfig(config);
            Map.Terrains.AddObject(terr);
            AddToTile(terr);
            return terr;
        }
        public static CellTagItem AddCellTag(IMapObjectBrushConfig config)
        {
            CellTagItem cell = new CellTagItem();
            cell.ApplyConfig(config);
            Map.Celltags.AddObject(cell);
            AddToTile(cell);
            return cell;
        }
        public static BaseNode AddBaseNode(IMapObjectBrushConfig config)
        {
            BaseNode node = Map.Houses[config.OwnerHouse].AddNewNode();
            node.ApplyConfig(config);
            AddToTile(node);
            return node;
        }
        public static WaypointItem AddWaypoint(IMapObjectBrushConfig config)
        {
            WaypointItem wp = new WaypointItem();
            wp.ApplyConfig(config);
            Map.Waypoints.AddObject(wp);
            AddToTile(wp);
            return wp;
        }
        public static OverlayUnit AddOverlay(IMapObjectBrushConfig config)
        {
            OverlayUnit o = new OverlayUnit();
            o.ApplyConfig(config);
            Map.Overlays[o] = o;
            AddToTile(o);
            return o;
        }
        #endregion


        #region Move
        private static bool isMoving = false;
        private static Dictionary<IMapObject, I2dLocateable> orgPos;
        public static void BeginMove(IEnumerable<IMapObject> targets)
        {
            isMoving = true;
            orgPos = new Dictionary<IMapObject, I2dLocateable>();
            foreach (IMapObject obj in targets)
            {
                orgPos[obj] = new Pnt(obj);
                EraseObjectRefInTile(obj);
            }
        }
        public static void ShiftObjectsBy(I2dLocateable delta)
        {
            if (isMoving)
            {
                foreach (var kv in orgPos)
                {
                    I2dLocateable destFlat = new Pnt(kv.Value.X + delta.X, kv.Value.Y + delta.Y);
                    if (Map.TilesData.HasTileOn(destFlat))
                    {
                        I3dLocateable dest = new Pnt3(destFlat, Map.GetHeightFromTile(destFlat));
                        kv.Key.MoveTo(dest);
                    }
                }
            }
        }
        public static void MoveObjectTo(IMapObject obj, I2dLocateable pos, int subcell)
        {
            if (Map.TilesData.HasTileOn(pos))
            {
                EraseObjectRefInTile(obj);
                I3dLocateable dest = new Pnt3(pos, Map.GetHeightFromTile(pos));
                obj.MoveTo(dest, subcell);
                AddToTile(obj);
            }
        }
        public static void EndMove()
        {
            isMoving = false;
            foreach (IMapObject obj in orgPos.Keys)
            {
                AddToTile(obj);
            }
            orgPos.Clear();
        }
        #endregion


        #region Remove
        private static void EraseObjectRefInTile(IMapObject obj)
        {
            switch (obj.ObjectType)
            {
                case MapObjectType.Building:
                    StructureItem bud = obj as StructureItem;
                    foreach (I2dLocateable pos in new Foundation2D(bud))
                    {
                        Map.TilesData.RemoveObjectOnTile(obj, pos);
                    }
                    break;
                case MapObjectType.Smudge:
                    SmudgeItem smg = obj as SmudgeItem;
                    foreach (I2dLocateable pos in new Square2D(smg, smg.SizeX, smg.SizeY))
                    {
                        Map.TilesData.RemoveObjectOnTile(obj, pos);
                    }
                    break;
                default:
                    Map.TilesData.RemoveObjectOnTile(obj);
                    break;
            }
        }
        /// <summary>
        /// Will dispose scene object if available
        /// </summary>
        /// <param name="obj"></param>
        public static void RemoveObject(IMapObject obj)
        {
            EraseObjectRefInTile(obj);
            obj.Dispose();
            switch (obj.ObjectType)
            {
                case MapObjectType.Aircraft:
                    Map.Aircrafts.RemoveByID(obj.Id);
                    break;
                case MapObjectType.BaseNode:
                    BaseNode node = obj as BaseNode;
                    node.Parent.RemoveNode(node);
                    break;
                case MapObjectType.Building:
                    Map.Buildings.RemoveByID(obj.Id);
                    break;
                case MapObjectType.Celltag:
                    Map.Celltags.RemoveObjectByID(obj as CellTagItem);
                    break;
                case MapObjectType.Infantry:
                    Map.Infantries.RemoveByID(obj.Id);
                    break;
                case MapObjectType.LightSource:
                    Map.LightSources.RemoveObjectByID(obj as LightSource);
                    break;
                case MapObjectType.Overlay:
                    Map.Overlays.RemoveByCoord(obj);
                    break;
                case MapObjectType.Smudge:
                    Map.Smudges.RemoveObjectByID(obj as SmudgeItem);
                    break;
                case MapObjectType.Terrain:
                    Map.Terrains.RemoveObjectByID(obj as TerrainItem);
                    break;
                case MapObjectType.Vehicle:
                    Map.Units.RemoveByID(obj.Id);
                    break;
                case MapObjectType.Waypoint:
                    Map.Waypoints.RemoveWaypoint(obj as WaypointItem);
                    break;
            }
        }
        #endregion
    }
}
