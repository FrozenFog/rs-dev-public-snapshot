using RelertSharp.Common;
using RelertSharp.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.MapEngine.Helper
{
    internal static class RandomizeBrush
    {



        #region Api
        #region Randomize Object
        private static List<RandomObject> randomObjects = new List<RandomObject>();
        private static RandomObject currentObject;
        private static WeightedRandom rnd = new WeightedRandom();
        public static bool AddRandomObject(string regName, MapObjectType type)
        {
            if (!randomObjects.Any(x => x.RegName == regName && x.ObjectType == type))
            {
                randomObjects.Add(new RandomObject()
                {
                    RegName = regName,
                    ObjectType = type
                });
                rnd.Ceil = randomObjects.Count;
                return true;
            }
            return false;
        }
        public static bool AddRandomObject(string regname, byte overlayIndex, byte overlayFrame)
        {
            if (!randomObjects.Any(x => x.RegName == regname && x.OverlayIndex == overlayIndex && x.OverlayFrame == overlayFrame))
            {
                randomObjects.Add(new RandomObject()
                {
                    RegName = regname,
                    OverlayFrame = overlayFrame,
                    ObjectType = MapObjectType.Overlay,
                    OverlayIndex = overlayIndex
                });
                rnd.Ceil = randomObjects.Count;
                return true;
            }
            return false;
        }
        public static void ClearRandomObject()
        {
            randomObjects.Clear();
            rnd.Ceil = 0;
        }
        public static void RemoveObject(IAbstractObjectDescriber obj)
        {
            var target = randomObjects.Find(x => x.IsEqual(obj));
            randomObjects.Remove(target);
            rnd.Ceil = randomObjects.Count;
        }
        public static void NextRandomObject(bool loadObject = true)
        {
            if (randomObjects.Count > 0)
            {
                currentObject = randomObjects[rnd.NoRepeatNext()];
                if (loadObject) LoadRandomObject();
            }
        }
        public static void LoadRandomObject()
        {
            if (randomObjects.Count > 0)
            {
                using (var _ = new EngineRegion())
                {
                    if (currentObject == null) NextRandomObject(false);
                    if (currentObject.ObjectType == MapObjectType.Overlay)
                    {
                        PaintBrush.SetOverlayInfo(currentObject.OverlayIndex, currentObject.OverlayFrame);
                        PaintBrush.LoadBrushObject(currentObject.RegName, MapObjectType.Overlay);
                    }
                    else PaintBrush.LoadBrushObject(currentObject.RegName, currentObject.ObjectType);
                }
            }
        }
        #endregion
        #endregion



        #region Calls
        public static IEnumerable<IAbstractObjectDescriber> CurrentObjects { get { return randomObjects; } }
        public static bool HasItem { get { return randomObjects.Any(); } }
        #endregion
        private class RandomObject : IAbstractObjectDescriber, IOverlay
        {
            public string RegName { get; set; }
            public MapObjectType ObjectType { get; set; }
            public byte OverlayIndex { get; set; }
            public byte OverlayFrame { get; set; }

            public bool IsEqual(IAbstractObjectDescriber desc)
            {
                if (desc is IOverlay o)
                {
                    return RegName == desc.RegName && ObjectType == desc.ObjectType &&
                        OverlayIndex == o.OverlayIndex && OverlayFrame == o.OverlayFrame;
                }
                return RegName == desc.RegName && ObjectType == desc.ObjectType;
            }
        }
    }
}
