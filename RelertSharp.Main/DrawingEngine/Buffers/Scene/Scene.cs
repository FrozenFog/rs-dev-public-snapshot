using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.DrawingEngine.Drawables;
using RelertSharp.DrawingEngine.Presenting;
using RelertSharp.Common;

namespace RelertSharp.DrawingEngine
{
    internal partial class BufferCollection
    {
        public partial class CScene
        {
            #region Ctor - Scene
            public CScene() { }
            #endregion


            #region Private Methods - CScene
            private void RemoveDisposedObject<T>(Dictionary<int, T> src) where T : PresentBase, IPresentBase
            {
                Dictionary<int, T> refer = new Dictionary<int, T>(src);
                foreach(int id in refer.Keys)
                {
                    if (refer[id].Disposed) src.Remove(id);
                }
            }
            #endregion


            #region Public Methods - CScene
            public void RemoveDisposedObject()
            {
                RemoveDisposedObject(Structures);
                RemoveDisposedObject(Units);
                RemoveDisposedObject(Infantries);
                RemoveDisposedObject(Overlays);
                RemoveDisposedObject(Terrains);
                RemoveDisposedObject(Smudges);
                RemoveDisposedObject(Celltags);
                RemoveDisposedObject(Waypoints);
            }

            #region Lightnings
            #endregion


            #region Remove all
            public void RemoveInfantries()
            {
                foreach (PresentInfantry infantry in Infantries.Values) infantry.Dispose();
                Infantries.Clear();
            }
            public void RemoveUnits()
            {
                foreach (PresentUnit unit in Units.Values) unit.Dispose();
                Units.Clear();
            }
            public void RemoveStructures()
            {
                foreach (PresentStructure structure in Structures.Values) structure.Dispose();
                Structures.Clear();
            }
            public void RemoveOverlays()
            {
                foreach (PresentMisc o in Overlays.Values) o.Dispose();
                Overlays.Clear();
            }
            public void RemoveTerrains()
            {
                foreach (PresentMisc terr in Terrains.Values) terr.Dispose();
                Terrains.Clear();
            }
            public void RemoveSmudges()
            {
                foreach (PresentMisc smg in Smudges.Values) smg.Dispose();
                Smudges.Clear();
            }
            public void RemoveCelltags()
            {
                foreach (PresentMisc ctg in Celltags.Values) ctg.Dispose();
                Celltags.Clear();
            }
            public void RemoveWaypoints()
            {
                foreach (PresentMisc wp in Waypoints.Values) wp.Dispose();
                Waypoints.Clear();
            }
            #endregion


            #region Mark

            #endregion


            #region ViewPort
            public void ResetSelectingRectangle()
            {
                foreach (int line in RectangleLines)
                {
                    if (line != 0) CppExtern.ObjectUtils.RemoveCommonFromScene(line);
                }
                RectangleLines.Clear();
            }
            #endregion
            #endregion


            #region Private Methods - Scene
            #endregion


            #region Public Calls - Scene
            public Dictionary<int, PresentStructure> Structures { get; private set; } = new Dictionary<int, PresentStructure>();
            public Dictionary<int, PresentUnit> Units { get; private set; } = new Dictionary<int, PresentUnit>();
            public Dictionary<int, PresentInfantry> Infantries { get; private set; } = new Dictionary<int, PresentInfantry>();
            public Dictionary<int, DrawableTile> DrawableTiles { get; private set; } = new Dictionary<int, DrawableTile>();
            public Dictionary<int, PresentMisc> Overlays { get; private set; } = new Dictionary<int, PresentMisc>();
            public Dictionary<int, PresentMisc> Terrains { get; private set; } = new Dictionary<int, PresentMisc>();
            public Dictionary<int, PresentMisc> Smudges { get; private set; } = new Dictionary<int, PresentMisc>();
            public Dictionary<int, PresentMisc> Celltags { get; private set; } = new Dictionary<int, PresentMisc>();
            public Dictionary<int, PresentMisc> Waypoints { get; private set; } = new Dictionary<int, PresentMisc>();
            public IEnumerable<IPresentBase> MapObjects { get { return Structures.Values.Concat<IPresentBase>(Units.Values).Concat(Infantries.Values); } }
            public IEnumerable<PresentMisc> MapMiscs { get { return Overlays.Values.Concat(Terrains.Values).Concat(Smudges.Values); } }
            public IEnumerable<PresentMisc> MiscWithoutSmudges { get { return Overlays.Values.Concat(Terrains.Values); } }
            public IEnumerable<IPresentBase> OneCellObjects { get { return Units.Values.Concat<IPresentBase>(Infantries.Values).Concat(Overlays.Values).Concat(Terrains.Values); } }
            public IEnumerable<PresentMisc> LogicObjects { get { return Celltags.Values.Concat(Waypoints.Values); } }
            public List<int> RectangleLines { get; private set; } = new List<int>();
            #endregion
        }
    }

}
