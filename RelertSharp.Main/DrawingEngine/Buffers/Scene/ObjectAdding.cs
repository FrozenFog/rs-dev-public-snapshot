using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.DrawingEngine.Presenting;
using RelertSharp.Common;

namespace RelertSharp.DrawingEngine
{
    internal partial class BufferCollection
    {
        public partial class CScene
        {
            private int NewID<T>(Dictionary<int, T> referance, T obj) where T : PresentBase, IPresentBase
            {
                for(int i = 0; i < 99999; i++)
                {
                    if (!referance.Keys.Contains(i))
                    {
                        obj.ID = i;
                        referance[i] = obj;
                        return i;
                    }
                }
                throw new OverflowException("Too much objects, index out of range");
            }

            public void AddUnit(PresentUnit unit)
            {
                int coord = unit.Coord;
                if (Tiles.Keys.Contains(coord))
                {
                    int id = NewID(Units, unit);
                }
            }
            public void AddInfantry(PresentInfantry inf)
            {
                int coord = inf.Coord;
                if (Tiles.Keys.Contains(coord))
                {
                    int id = NewID(Infantries, inf);
                }
            }
            public void AddBuilding(PresentStructure bud)
            {
                int coord = bud.Coord;
                if (Tiles.Keys.Contains(coord))
                {
                    int id = NewID(Structures, bud);
                }
            }
            public void AddOverlay(PresentMisc ov)
            {
                int coord = ov.Coord;
                if (Tiles.Keys.Contains(coord))
                {
                    int id = NewID(Overlays, ov);
                }
            }
            public void AddTerrain(PresentMisc terr)
            {
                int coord = terr.Coord;
                if (Tiles.Keys.Contains(coord))
                {
                    int id = NewID(Terrains, terr);
                }
            }
            public void AddSmudge(PresentMisc smg)
            {
                int coord = smg.Coord;
                if (Tiles.Keys.Contains(coord))
                {
                    int id = NewID(Smudges, smg);
                    foreach (I2dLocateable pos in new Square2D(smg, smg.SmgWidth, smg.SmgHeight))
                    {
                        int sub = pos.Coord;
                    }
                }
            }
        }
    }
}
