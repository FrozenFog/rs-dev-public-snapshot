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


        public void Reload(string regname, MapObjectType type, bool reserveOriginal = false, bool usePrevPos = false)
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
            if (usePrevPos && prevPos != null)
            {
                BrushObject.X = prevPos.X;
                BrushObject.Y = prevPos.Y;
            }
            else
            {
                BrushObject.X = -1000;
                BrushObject.Y = -1000;
            }
            if (BrushObject as ICombatObject != null)
            {
                (BrushObject as ICombatObject).OwnerHouse = GlobalVar.CurrentMapDocument.Map.Houses.First().Name;
            }
            GlobalVar.Engine.DrawBrushObject(BrushObject);
            GlobalVar.Engine.SetObjectLightningStandalone(BrushObject.SceneObject);
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
            Reload(currentName, ObjectType, true);
            return obj;
        }


        public IMapObject BrushObject { get; set; }
        public MapObjectType ObjectType { get; private set; }
        public bool IsInvalidItem { get { return BrushObject.X == -1000 || BrushObject.Y == -1000; } }
    }
}
