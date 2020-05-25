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
        private void InspectItemAt(MouseEventArgs e)
        {
            Vec3 pos = GlobalVar.Engine.ClientPointToCellPos(e.Location);
            Tile t = map.TilesData[pos.To2dLocateable()];
            if (t != null && t.GetObjects().Count() > 0)
            {
                IMapObject obj = t.GetObjects().Last();
                if (obj.GetType() == typeof(UnitItem))
                {
                    UnitAttributeForm fm = new UnitAttributeForm(obj as UnitItem, map.Houses, map.Tags);
                    fm.ShowDialog();
                    UnitItem newitem = fm.Result;
                    map.Units[newitem.ID] = newitem;
                    GlobalVar.Engine.UpdateUnitAttribute(newitem, map.GetHeightFromTile(newitem), map.GetHouseColor(newitem.OwnerHouse));
                }
            }
        }
    }
}
