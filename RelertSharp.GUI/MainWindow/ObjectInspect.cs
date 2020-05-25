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

        private void InspectItemAt(MouseEventArgs e)
        {
            Vec3 pos = GlobalVar.Engine.ClientPointToCellPos(e.Location, out int subcell);
            Tile t = map.TilesData[pos.To2dLocateable()];
            IEnumerable<IMapObject> objects = t.GetObjects();
            if (t != null && objects.Count() > 0)
            {
                foreach (IMapObject obj in objects)
                {
                    if (obj.GetType() == typeof(UnitItem))
                    {
                        if (unitForm == null) unitForm = new UnitAttributeForm(obj as UnitItem);
                        else unitForm.Reload(obj as UnitItem);
                        unitForm.ShowDialog();
                        UnitItem newitem = unitForm.Result;
                        map.UpdateUnit(newitem);
                        GlobalVar.Engine.UpdateUnitAttribute(newitem, map.GetHeightFromTile(newitem), map.GetHouseColor(newitem.OwnerHouse));
                        break;
                    }
                    else if (obj.GetType() == typeof(InfantryItem))
                    {
                        foreach (InfantryItem inf in objects)
                        {
                            if (inf.SubCells == subcell)
                            {
                                if (infForm == null) infForm = new InfantryAttributeForm(inf as InfantryItem);
                                else infForm.Reload(inf as InfantryItem);
                                infForm.ShowDialog();
                                InfantryItem newitem = infForm.Result;
                                map.UpdateInfantry(newitem);
                                GlobalVar.Engine.UpdateInfantryAttribute(newitem, map.GetHeightFromTile(newitem), map.GetHouseColor(newitem.OwnerHouse), subcell);
                                break;
                            }
                        }
                        break;
                    }
                }
            }
        }
    }
}
