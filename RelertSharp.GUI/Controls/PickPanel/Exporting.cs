using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.MapStructure.Objects;

namespace RelertSharp.GUI.Controls
{
    public partial class PickPanel
    {
        public void MoveBurhObjectTo(I3dLocateable cell, int subcell = 1)
        {
            if (brush.BrushObject != null && brush.BrushObject.SceneObject !=null)
            {
                if (brush.BrushObject.GetType() == typeof(InfantryItem))
                {
                    (brush.BrushObject as InfantryItem).MoveTo(cell, subcell);
                }
                else
                {
                    brush.BrushObject.MoveTo(cell);
                }
            }
        }
    }
}
