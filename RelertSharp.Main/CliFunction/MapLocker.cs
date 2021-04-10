using RelertSharp.FileSystem;
using RelertSharp.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure;
using RelertSharp.Common;
using VectorDictionary = System.Collections.Generic.Dictionary<RelertSharp.Common.I2dLocateable, RelertSharp.Common.I2dLocateable>;
using RelertSharp.MapStructure.Logic;

namespace RelertSharp.CliFunction
{
    internal static class MapLocker
    {
        internal const string MODE_LOCK = "lock";
        internal const string MODE_UNLOCK = "unlock";
        internal const string PUBLICKEY = "public";
        private static Map map;

        internal static bool RunLocker(MapFile mf, string mode, string seed)
        {
            if (seed.IsNullOrEmpty()) seed = PUBLICKEY;
            int nSeed = seed.GetHashCode();
            map = mf.Map;
            VectorDictionary shuffleVector = GenerateVector(nSeed);
            if (mode == MODE_LOCK) return LockMap(shuffleVector);
            else if (mode == MODE_UNLOCK) return UnlockMap(shuffleVector);
            return false;
        }

        private static VectorDictionary GenerateVector(int seed)
        {
            VectorDictionary vector = new VectorDictionary();
            Pnt[] temp = new Pnt[map.TilesData.Count()];
            int p = 0;
            foreach (Tile t in map.TilesData) temp[p++] = new Pnt(t);
            Pnt[] shuffled = temp.Shuffle(null, seed).ToArray();
            p = 0;
            foreach (Tile t in map.TilesData)
            {
                Pnt src = new Pnt(t);
                Pnt dest = shuffled[p++];
                vector[src] = dest;
            }
            return vector;
        }
        private static bool LockMap(VectorDictionary shuffleVector)
        {
            Swap(map.TilesData, shuffleVector, (pos, v) => { Swap(pos, v[pos.AsPnt()]); });
            foreach (OverlayUnit o in map.Overlays.ToArray())
            {
                map.RemoveOverlay(o);
                Swap(o, shuffleVector[o.AsPnt()]);
                map.Overlays[o.X, o.Y] = o;
            }
            Swap(map.Buildings, shuffleVector, (pos, v) => { Swap(pos, v[pos.AsPnt()]); });
            Swap(map.Infantries, shuffleVector, (pos, v) => { Swap(pos, v[pos.AsPnt()]); });
            Swap(map.Units, shuffleVector, (pos, v) => { Swap(pos, v[pos.AsPnt()]); });
            Swap(map.Aircrafts, shuffleVector, (pos, v) => { Swap(pos, v[pos.AsPnt()]); });
            Swap(map.Terrains, shuffleVector, (pos, v) => { Swap(pos, v[pos.AsPnt()]); });
            Swap(map.Smudges, shuffleVector, (pos, v) => { Swap(pos, v[pos.AsPnt()]); });
            Swap(map.Waypoints, shuffleVector, (pos, v) => { Swap(pos, v[pos.AsPnt()]); });
            Swap(map.Celltags, shuffleVector, (pos, v) => { Swap(pos, v[pos.AsPnt()]); });
            foreach (HouseItem h in map.Houses) Swap(h.BaseNodes, shuffleVector, (pos, v) => { Swap(pos, v[pos.AsPnt()]); });
            return true;
        }
        private static bool UnlockMap(VectorDictionary shuffleVector)
        {
            shuffleVector = shuffleVector.ToDictionary(x => x.Value, x => x.Key);
            return LockMap(shuffleVector);
        }


        private static Pnt AsPnt(this I2dLocateable src)
        {
            return new Pnt(src);
        }
        private static void Swap(IEnumerable<I2dLocateable> src, VectorDictionary vector, Action<I2dLocateable, VectorDictionary> swapFunc)
        {
            foreach (I2dLocateable pos in src) swapFunc(pos, vector);
        }
        private static void Swap(I2dLocateable src, I2dLocateable dest)
        {
            src.X = dest.X;
            src.Y = dest.Y;
        }
    }
}
