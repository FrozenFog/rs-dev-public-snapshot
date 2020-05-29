using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.DrawingEngine;
using RelertSharp.Common;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Points;

namespace RelertSharp.DrawingEngine
{
    public partial class Engine
    {
        public void MoveObjectTo(IMapObject obj, I3dLocateable newcell, int subcell)
        {
            I3dLocateable org = _cellFindingReferance[obj];
            Type t = obj.GetType();
            if (t == typeof(InfantryItem))
            {
                InfantryItem inf = obj as InfantryItem;
                Buffer.Scenes.MoveInfantryTo(newcell, org, inf.SubCells, subcell);
            }
            else if (t == typeof(StructureItem))
            {
                Buffer.Scenes.MoveBuildingTo(newcell, org);
            }
            else if (t == typeof(UnitItem) || t == typeof(AircraftItem))
            {
                Buffer.Scenes.MoveUnitTo(newcell, org);
            }
            else if (t == typeof(OverlayUnit))
            {
                Buffer.Scenes.MoveOverlayTo(newcell, org);
            }
            else if (t == typeof(TerrainItem))
            {
                Buffer.Scenes.MoveTerrainTo(newcell, org);
            }
        }
        public void ShiftObjectBy(I3dLocateable delta, IMapObject obj)
        {
            I3dLocateable org = _cellFindingReferance[obj];
            Type t = obj.GetType();
            if (t == typeof(InfantryItem))
            {
                InfantryItem inf = obj as InfantryItem;
            }
        }
    }
}
