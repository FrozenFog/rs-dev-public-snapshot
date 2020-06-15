﻿using System;
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
        public BrushModel() { }


        public void Reload(string regname, MapObjectType type)
        {
            if (BrushObject != null && BrushObject.SceneObject != null)
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
            BrushObject.X = -1000;
            BrushObject.Y = -1000;
            if (BrushObject as ICombatObject != null)
            {
                (BrushObject as ICombatObject).OwnerHouse = GlobalVar.CurrentMapDocument.Map.Houses.First().Name;
            }
            GlobalVar.Engine.DrawBrushObject(BrushObject);
        }


        public IMapObject BrushObject { get; set; }
    }
}
