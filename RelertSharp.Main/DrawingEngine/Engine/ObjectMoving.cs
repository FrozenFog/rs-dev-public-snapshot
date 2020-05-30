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

using System.Threading;

namespace RelertSharp.DrawingEngine
{
    public partial class Engine
    {
        public void MoveObjectTo(IMapObject obj, I3dLocateable newcell, int subcell = -1)
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
            else if (t == typeof(SmudgeItem))
            {
                Buffer.Scenes.MoveSmudgeTo(newcell, org);
            }
        }
        public void ShiftObjectBy(I3dLocateable delta, IMapObject obj)
        {
            Type t = obj.GetType();
            if (t == typeof(InfantryItem))
            {
                Buffer.Scenes.ShiftInfantryBy(delta, obj, (obj as InfantryItem).SubCells);
            }
            else if (t == typeof(UnitItem) || t == typeof(AircraftItem))
            {
                Buffer.Scenes.ShiftUnitBy(delta, obj);
            }
            else if (t == typeof(StructureItem))
            {
                Buffer.Scenes.ShiftBuildingBy(delta, obj);
            }
            else if (t == typeof(TerrainItem))
            {
                Buffer.Scenes.ShiftTerrainBy(delta, obj);
            }
            else if (t == typeof(OverlayUnit))
            {
                Buffer.Scenes.ShiftOverlayBy(delta, obj);
            }
            else if (t == typeof(SmudgeItem))
            {
                Buffer.Scenes.ShiftSmudgeTo(delta, obj);
            }
        }
    }
}
