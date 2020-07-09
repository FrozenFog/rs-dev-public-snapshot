using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Points;
using RelertSharp.MapStructure;

namespace RelertSharp.GUI.Model.BrushModel
{
    public class BrushModel
    {
        private string currentName;
        private I3dLocateable prevPos;


        public BrushModel() { }


        public void Reload(IMapObject src, MapObjectType type, bool reserveOriginal = false)
        {
            currentName = src.RegName;
            ObjectType = type;
            if (BrushObject != null && BrushObject.SceneObject != null && !reserveOriginal)
            {
                BrushObject.Dispose();
                GlobalVar.Engine.RemoveDisposedObjects();
            }
            switch (type)
            {
                case MapObjectType.Building:
                    BrushObject = new StructureItem(src as StructureItem);
                    break;
                case MapObjectType.Infantry:
                    BrushObject = new InfantryItem(src as InfantryItem);
                    break;
                case MapObjectType.Vehicle:
                    BrushObject = new UnitItem(src as UnitItem);
                    break;
                case MapObjectType.Aircraft:
                    BrushObject = new AircraftItem(src as AircraftItem);
                    break;
                case MapObjectType.Terrain:
                    BrushObject = new TerrainItem(src as TerrainItem);
                    break;
                case MapObjectType.Smudge:
                    BrushObject = new SmudgeItem(src as SmudgeItem);
                    break;
                case MapObjectType.Overlay:
                    BrushObject = new OverlayUnit(src as OverlayUnit);
                    break;
                case MapObjectType.Celltag:
                    BrushObject = new CellTagItem(src as CellTagItem);
                    break;
            }
            GlobalVar.Engine.DrawBrushObject(BrushObject);
            GlobalVar.Engine.SetObjectLightningStandalone(BrushObject.SceneObject);
            GlobalVar.Engine.UnmarkBuildingShape();
        }
        public void Reload(string regname, MapObjectType type, ICombatObject referance = null, bool reserveOriginal = false, bool usePrevPos = false)
        {
            currentName = regname;
            ObjectType = type;
            if (BrushObject != null && BrushObject.SceneObject != null && !reserveOriginal)
            {
                BrushObject.Dispose();
                GlobalVar.Engine.RemoveDisposedObjects();
            }
            switch (type)
            {
                case MapObjectType.Building:
                    BrushObject = new StructureItem(regname);
                    break;
                case MapObjectType.Infantry:
                    BrushObject = new InfantryItem(regname);
                    break;
                case MapObjectType.Vehicle:
                    BrushObject = new UnitItem(regname);
                    break;
                case MapObjectType.Aircraft:
                    BrushObject = new AircraftItem(regname);
                    break;
                case MapObjectType.Terrain:
                    BrushObject = new TerrainItem(regname);
                    break;
                case MapObjectType.Smudge:
                    BrushObject = new SmudgeItem(regname);
                    break;
            }
            if (referance != null) (BrushObject as ICombatObject).ApplyAttributeFrom(referance);
            GlobalVar.Engine.DrawBrushObject(BrushObject);
            GlobalVar.Engine.SetObjectLightningStandalone(BrushObject.SceneObject);
            GlobalVar.Engine.UnmarkBuildingShape();
        }
        public void MoveBrushObjectTo(I3dLocateable pos)
        {
            BrushObject.MoveTo(pos);
            prevPos = pos;
        }
        public void RedrawBrushObject()
        {
            if (BrushObject != null)
            {
                int fix = prevPos != null ? prevPos.Z : 0;
                BrushObject.SceneObject.Dispose();
                BrushObject.SceneObject = null;
                GlobalVar.Engine.RemoveDisposedObjects();
                GlobalVar.Engine.DrawBrushObject(BrushObject, fix);
                GlobalVar.Engine.SetObjectLightningStandalone(BrushObject.SceneObject);
            }
        }
        public IMapObject ReleaseObject()
        {
            IMapObject obj = BrushObject;
            Reload(currentName, ObjectType, obj as ICombatObject, true);
            return obj;
        }


        public IMapObject BrushObject { get; set; }
        public MapObjectType ObjectType { get; private set; }
        public bool IsInvalidItem { get { return BrushObject.X == -1000 || BrushObject.Y == -1000; } }
    }
}
