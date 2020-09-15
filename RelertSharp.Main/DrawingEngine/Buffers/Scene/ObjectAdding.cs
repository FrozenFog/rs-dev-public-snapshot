using RelertSharp.DrawingEngine.Presenting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RelertSharp.DrawingEngine
{
    internal partial class BufferCollection
    {
        public partial class CScene
        {
            private int NewID<T>(Dictionary<int, T> referance, T obj) where T : PresentBase, IPresentBase
            {
                for (int i = 0; i < 99999; i++)
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
                int id = NewID(Units, unit);
            }
            public void AddInfantry(PresentInfantry inf)
            {
                int id = NewID(Infantries, inf);
            }
            public void AddBuilding(PresentStructure bud)
            {
                int id = NewID(Structures, bud);
            }
            public void AddOverlay(PresentMisc ov)
            {
                int id = NewID(Overlays, ov);
            }
            public void AddTerrain(PresentMisc terr)
            {
                int id = NewID(Terrains, terr);
            }
            public void AddSmudge(PresentMisc smg)
            {
                int id = NewID(Smudges, smg);
            }
        }
    }
}
