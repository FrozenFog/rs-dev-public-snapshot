using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RelertSharp.DrawingEngine;
using RelertSharp.FileSystem;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Points;
using RelertSharp.MapStructure.Objects;
using RelertSharp.MapStructure.Logic;
using RelertSharp.IniSystem;
using RelertSharp.Common;
using RelertSharp.GUI.Model;

namespace RelertSharp.GUI
{
    public partial class MainWindowTest
    {
        private UnitAttributeForm unitForm;
        private InfantryAttributeForm infForm;
        private BuildingAttributeForm budForm;
        private AircraftAttributeForm airForm;

        private void InspectItemAt(MouseEventArgs e)
        {
            Vec3 pos = GlobalVar.Engine.ClientPointToCellPos(e.Location, out int subcell);
            //map.DumpStructures();
            Tile t = Map.TilesData[pos.To2dLocateable()];
            IEnumerable<IMapObject> objects = t.GetObjects();
            if (t != null && objects.Count() > 0)
            {
                foreach (IMapObject obj in objects)
                {
                    if (obj.ObjectType == MapObjectType.Unit)
                    {
                        if (unitForm == null) unitForm = new UnitAttributeForm(obj as UnitItem);
                        else unitForm.Reload(obj as UnitItem);
                        if (unitForm.ShowDialog() == DialogResult.OK)
                        {
                            Current.RemoveItemFromSelectList(obj);
                            UnitItem newitem = unitForm.Result;
                            Map.UpdateUnit(newitem);
                            GlobalVar.Engine.UpdateUnitAttribute(newitem, Map.GetHeightFromTile(newitem), Map.GetHouseColor(newitem.OwnerHouse));
                            if (newitem.Selected) Current.SelectUnitAt(newitem);
                        }
                        break;
                    }
                    else if (obj.ObjectType == MapObjectType.Infantry)
                    {
                        foreach (InfantryItem inf in objects)
                        {
                            if (inf.SubCells == subcell)
                            {
                                if (infForm == null) infForm = new InfantryAttributeForm(inf as InfantryItem);
                                else infForm.Reload(inf as InfantryItem);
                                if (infForm.ShowDialog() == DialogResult.OK)
                                {
                                    Current.RemoveItemFromSelectList(obj);
                                    InfantryItem newitem = infForm.Result;
                                    Map.UpdateInfantry(newitem);
                                    GlobalVar.Engine.UpdateInfantryAttribute(newitem, Map.GetHeightFromTile(newitem), Map.GetHouseColor(newitem.OwnerHouse), subcell);
                                    if (newitem.Selected) Current.SelectInfantryAt(newitem, newitem.SubCells);
                                }
                                break;
                            }
                        }
                        break;
                    }
                    else if (obj.ObjectType == MapObjectType.Building)
                    {
                        if (budForm == null) budForm = new BuildingAttributeForm(obj as StructureItem);
                        else budForm.Reload(obj as StructureItem);
                        if (budForm.ShowDialog() == DialogResult.OK)
                        {
                            Current.RemoveItemFromSelectList(obj);
                            StructureItem newitem = budForm.Result;
                            Map.UpdateBuilding(newitem);
                            GlobalVar.Engine.UpdateBuildingAttribute(newitem, Map.GetHeightFromTile(newitem), Map.GetHouseColor(newitem.OwnerHouse));
                            if (newitem.Selected) Current.SelectBuildingAt(newitem);
                        }
                        break;
                    }
                    else if (obj.ObjectType == MapObjectType.Aircraft)
                    {
                        if (airForm == null) airForm = new AircraftAttributeForm(obj as AircraftItem);
                        else airForm.Reload(obj as AircraftItem);
                        if (airForm.ShowDialog() == DialogResult.OK)
                        {
                            Current.RemoveItemFromSelectList(obj);
                            AircraftItem newitem = airForm.Result;
                            Map.UpdateAircraft(newitem);
                            GlobalVar.Engine.UpdateAircraftAttribute(newitem, Map.GetHeightFromTile(newitem), Map.GetHouseColor(newitem.OwnerHouse));
                            if (newitem.Selected) Current.SelectAircraftAt(newitem);
                        }
                        break;
                    }
                }
                RedrawMinimapAll();
            }
        }
    }
}
