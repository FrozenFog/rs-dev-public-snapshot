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
                InfantryItem inf = map.GetInfantry(pos, subcell);
                if (inf != null)
                {
                    inf.ApplyAttributeFrom(changer);
                    Engine.UpdateInfantryAttribute(inf, height, map.GetHouseColor(inf.OwnerHouse), subcell, false);
                }
            }
            if (rbPanelAttribute.ToUnits)
            {
                UnitItem unit = map.GetUnit(pos);
                if (unit != null)
                {
                    unit.ApplyAttributeFrom(changer);
                    Engine.UpdateUnitAttribute(unit, height, map.GetHouseColor(unit.OwnerHouse), false);
                }
            }
            if (rbPanelAttribute.ToBuildings)
            {
                StructureItem bud = map.GetBuilding(pos);
                if (bud != null)
                {
                    bud.ApplyAttributeFrom(changer);
                    Engine.UpdateBuildingAttribute(bud, height, map.GetHouseColor(bud.OwnerHouse), false);
                }
            }
            if (rbPanelAttribute.ToAircrafts)
            {
                AircraftItem air = map.GetAircraft(pos);
                if (air != null)
                {
                    air.ApplyAttributeFrom(changer);
                    Engine.UpdateAircraftAttribute(air, height, map.GetHouseColor(air.OwnerHouse), false);
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
                Engine.UpdateInfantryAttribute(inf, map.GetHeightFromTile(inf), map.GetHouseColor(inf.OwnerHouse), inf.SubCells, true);
            }
            foreach (UnitItem unit in Current.Units)
            {
                unit.ApplyAttributeFrom(changer);
                Engine.UpdateUnitAttribute(unit, map.GetHeightFromTile(unit), map.GetHouseColor(unit.OwnerHouse), true);
            }
            foreach (AircraftItem air in Current.Aircrafts)
            {
                air.ApplyAttributeFrom(changer);
                Engine.UpdateAircraftAttribute(air, map.GetHeightFromTile(air), map.GetHouseColor(air.OwnerHouse), true);
            }
            foreach (StructureItem bud in Current.Buildings)
            {
                bud.ApplyAttributeFrom(changer);
                Engine.UpdateBuildingAttribute(bud, map.GetHeightFromTile(bud), map.GetHouseColor(bud.OwnerHouse), true);
            }
            Engine.Refresh();
            Engine.RedrawMinimapAll();
        }
    }
}
