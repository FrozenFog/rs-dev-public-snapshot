using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.MapStructure.Objects;

namespace RelertSharp.MapStructure
{
    public partial class Map
    {
        public void MoveObjectTo(IMapObject target, I3dLocateable pos, int subcell = -1)
        {
            EraseObjectTileData(target);
            if (target.GetType() == typeof(InfantryItem))
            {
                (target as InfantryItem).MoveTo(pos, subcell);
            }
            else target.MoveTo(pos);
            AddObjectInfoInTile(target);
        }
        public void ShiftObjectBy(IMapObject target, I3dLocateable delta)
        {
            EraseObjectTileData(target);
            target.ShiftBy(delta);
            AddObjectInfoInTile(target);
        }
    }
}
