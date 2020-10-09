using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.FileSystem;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Points;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Logic;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using RelertSharp.GUI.Model;
using RelertSharp.SubWindows.LogicEditor;
using RelertSharp.SubWindows.INIEditor;
using static RelertSharp.Common.GlobalVar;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest
    {
        private void ApplyAttributeToPrecise(MouseEventArgs e)
        {
            Vec3 v3 = Engine.ClientPointToCellPos(e.Location, out int subcell);
            I2dLocateable pos = v3.To2dLocateable();
            AttributeChanger changer = rbPanelAttribute.Changer;
            int height = (int)v3.Z;
            if (rbPanelAttribute.ToInfantries)
            {
                InfantryItem inf = Map.GetInfantry(pos, subcell);
                if (inf != null)
                {
                    inf.ApplyAttributeFrom(changer);
                    Engine.UpdateInfantryAttribute(inf, height, Map.GetHouseColor(inf.OwnerHouse), subcell);
                }
            }
            if (rbPanelAttribute.ToUnits)
            {
                UnitItem unit = Map.GetUnit(pos);
                if (unit != null)
                {
                    unit.ApplyAttributeFrom(changer);
                    Engine.UpdateUnitAttribute(unit, height, Map.GetHouseColor(unit.OwnerHouse));
                }
            }
            if (rbPanelAttribute.ToBuildings)
            {
                StructureItem bud = Map.GetBuilding(pos);
                if (bud != null)
                {
                    bud.ApplyAttributeFrom(changer);
                    Engine.UpdateBuildingAttribute(bud, height, Map.GetHouseColor(bud.OwnerHouse));
                }
            }
            if (rbPanelAttribute.ToAircrafts)
            {
                AircraftItem air = Map.GetAircraft(pos);
                if (air != null)
                {
                    air.ApplyAttributeFrom(changer);
                    Engine.UpdateAircraftAttribute(air, height, Map.GetHouseColor(air.OwnerHouse));
                }
            }
            Engine.Refresh();
            Engine.RedrawMinimapAll();
        }


        private void ApplyAttributeToSelected()
        {
            AttributeChanger changer = rbPanelAttribute.Changer;
            foreach (InfantryItem inf in Current.Infantries)
            {
                inf.ApplyAttributeFrom(changer);
                Engine.UpdateInfantryAttribute(inf, Map.GetHeightFromTile(inf), Map.GetHouseColor(inf.OwnerHouse), inf.SubCells);
            }
            foreach (UnitItem unit in Current.Units)
            {
                unit.ApplyAttributeFrom(changer);
                Engine.UpdateUnitAttribute(unit, Map.GetHeightFromTile(unit), Map.GetHouseColor(unit.OwnerHouse));
            }
            foreach (AircraftItem air in Current.Aircrafts)
            {
                air.ApplyAttributeFrom(changer);
                Engine.UpdateAircraftAttribute(air, Map.GetHeightFromTile(air), Map.GetHouseColor(air.OwnerHouse));
            }
            foreach (StructureItem bud in Current.Buildings)
            {
                bud.ApplyAttributeFrom(changer);
                Engine.UpdateBuildingAttribute(bud, Map.GetHeightFromTile(bud), Map.GetHouseColor(bud.OwnerHouse));
            }
            Engine.Refresh();
            Engine.RedrawMinimapAll();
        }
    }
}
