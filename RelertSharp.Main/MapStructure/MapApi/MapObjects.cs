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
        public static StructureItem AddBuilding(IMapObjectBrushConfig config, IObjectBrushFilter filter)
        {
            StructureItem bud = new StructureItem(config.RegName);
            bud.ApplyConfig(config, filter, true);
            Map.Buildings.AddItem(bud);
            AddToTile(bud);
            return bud;
        }
        public static InfantryItem AddInfantry(IMapObjectBrushConfig config, IObjectBrushFilter filter)
        {
            InfantryItem inf = new InfantryItem();
            inf.ApplyConfig(config, filter, true);
            Map.Infantries.AddItem(inf);
            AddToTile(inf);
            return inf;
        }
        public static UnitItem AddUnit(IMapObjectBrushConfig config, IObjectBrushFilter filter)
        {
            UnitItem unit = new UnitItem();
            unit.ApplyConfig(config, filter, true);
            Map.Units.AddItem(unit);
            AddToTile(unit);
            return unit;
        }
        public static AircraftItem AddAircraft(IMapObjectBrushConfig config, IObjectBrushFilter filter)
        {
            AircraftItem air = new AircraftItem();
            air.ApplyConfig(config, filter, true);
            Map.Aircrafts.AddItem(air);
            AddToTile(air);
            return air;
        }
        public static SmudgeItem AddSmudge(IMapObjectBrushConfig config, IObjectBrushFilter filter)
        {
            SmudgeItem smg = new SmudgeItem();
            smg.ApplyConfig(config, filter, true);
            Map.Smudges.AddObject(smg);
            AddToTile(smg);
            return smg;
        }
        public static TerrainItem AddTerrain(IMapObjectBrushConfig config, IObjectBrushFilter filter)
        {
            TerrainItem terr = new TerrainItem();
            terr.ApplyConfig(config, filter, true);
            Map.Terrains.AddObject(terr);
            AddToTile(terr);
            return terr;
        }
        public static CellTagItem AddCellTag(IMapObjectBrushConfig config, IObjectBrushFilter filter)
        {
            CellTagItem cell = new CellTagItem();
            cell.ApplyConfig(config, filter, true);
            Map.Celltags.AddObject(cell);
            AddToTile(cell);
            return cell;
        }
        public static BaseNode AddBaseNode(IMapObjectBrushConfig config, IObjectBrushFilter filter)
        {
            BaseNode node = Map.Houses.GetHouse(config.Owner).AddNewNode();
            node.ApplyConfig(config, filter, true);
            AddToTile(node);
            return node;
        }
        public static WaypointItem AddWaypoint(IMapObjectBrushConfig config, IObjectBrushFilter filter)
        {
            WaypointItem wp = new WaypointItem();
            wp.ApplyConfig(config, filter, true);
            Map.Waypoints.AddObject(wp);
            AddToTile(wp);
            return wp;
        }
        public static OverlayUnit AddOverlay(IMapObjectBrushConfig config, IObjectBrushFilter filter)
        {
            OverlayUnit o = new OverlayUnit();
            o.ApplyConfig(config, filter, true);
            Map.Overlays[o] = o;
            AddToTile(o);
            return o;
        }
        public static void AddObject(IMapObject obj)
        {
            switch (obj.ObjectType)
            {
                case MapObjectType.Aircraft:
                    Map.Aircrafts.AddItem(obj as AircraftItem);
                    break;
                case MapObjectType.Building:
                    Map.Buildings.AddItem(obj as StructureItem);
                    break;
                case MapObjectType.Vehicle:
                    Map.Units.AddItem(obj as UnitItem);
                    break;
                case MapObjectType.Infantry:
                    Map.Infantries.AddItem(obj as InfantryItem);
                    break;
                case MapObjectType.Smudge:
                    Map.Smudges.AddObject(obj as SmudgeItem);
                    break;
                case MapObjectType.Terrain:
                    Map.Terrains.AddObject(obj as TerrainItem);
                    break;
                case MapObjectType.Celltag:
                    Map.Celltags.AddObject(obj as CellTagItem);
                    break;
                case MapObjectType.Waypoint:
                    Map.Waypoints.AddObject(obj as WaypointItem);
                    break;
                case MapObjectType.Overlay:
                    Map.Overlays[obj] = obj as OverlayUnit;
                    break;
                case MapObjectType.LightSource:
                    Map.LightSources.AddObject(obj as LightSource);
                    break;
                case MapObjectType.BaseNode:
                    BaseNode node = obj as BaseNode;
                    node.Parent.AddNode(node);
                    break;
            }
            AddToTile(obj);
        }
        #endregion


        #region Move
        private static bool isMoving = false;
        private static Dictionary<IMapObject, I2dLocateable> orgPos;
        //public static void BeginMove(IEnumerable<IMapObject> targets)
        //{
        //    isMoving = true;
        //    orgPos = new Dictionary<IMapObject, I2dLocateable>();
        //    foreach (IMapObject obj in targets)
        //    {
        //        orgPos[obj] = new Pnt(obj);
        //        EraseObjectRefInTile(obj);
        //    }
        //}
        /// <summary>
        /// Erase all object information on original tile
        /// </summary>
        /// <param name="targets"></param>
        public static void BeginMove(IEnumerable<IMapObject> targets)
        {
            foreach (IMapObject obj in targets)
            {
                EraseObjectRefInTile(obj);
            }
        }
        /// <summary>
        /// Write all object information to current tile they're on
        /// </summary>
        /// <param name="targets"></param>
        public static void EndMove(IEnumerable<IMapObject> targets)
        {
            foreach (IMapObject obj in targets)
            {
                AddToTile(obj);
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
        /// <summary>
        /// Only use for single object moving, such as ONE infantry, or Object brush
        /// </summary>
        /// <param name="obj">Object that needs to move</param>
        /// <param name="pos">Target position</param>
        /// <param name="subcell">Target subcell</param>
        /// <param name="eraseMove">Will erase original object info on tile and write to destination tile</param>
        public static void MoveObjectTo(IMapObject obj, I2dLocateable pos, int subcell = -1, bool eraseMove = true)
        {
            if (Map.TilesData.HasTileOn(pos))
            {
                if (eraseMove) EraseObjectRefInTile(obj);
                I3dLocateable dest = new Pnt3(pos, Map.GetHeightFromTile(pos));
                obj.MoveTo(dest, subcell);
                if (eraseMove) AddToTile(obj);
            }
        }
        //public static void EndMove()
        //{
        //    isMoving = false;
        //    foreach (IMapObject obj in orgPos.Keys)
        //    {
        //        AddToTile(obj);
        //    }
        //    orgPos.Clear();
        //}
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
