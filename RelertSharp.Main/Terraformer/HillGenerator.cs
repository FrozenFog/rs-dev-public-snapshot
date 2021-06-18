using RelertSharp.Common;
using RelertSharp.MapStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RelertSharp.Terraformer.RampBase;

namespace RelertSharp.Terraformer
{
    public static class HillGenerator
    {
        #region Components
        private static readonly RampData[] Data = new RampData[20]
        {
            new RampData("00111011", 1),
            new RampData("13001310", 2),
            new RampData("10120012", 3),
            new RampData("14101400", 4),
            new RampData("00001311", 5),

            new RampData("13000012", 6),
            new RampData("14120000", 7),
            new RampData("00111400", 8),
            new RampData("13111010", 9),
            new RampData("10121310", 10),

            new RampData("10101412", 11),
            new RampData("14101011", 12),
            new RampData("13112321", 13),
            new RampData("23121322", 14),
            new RampData("24221412", 15),

            new RampData("14212411", 16),
            new RampData("13111412", 17),
            new RampData("14121311", 18),
            new RampData("13111412", 19),
            new RampData("14121311", 20)
        };
        private static int rampBaseIndex;
        private static Random r;
        private static Map Map { get { return GlobalVar.CurrentMapDocument.Map; } }
        private static int RandomRampOffset
        {
            get
            {
                return r.Next(0, 20);
            }
        }
        #endregion


        #region ctor & listener
        static HillGenerator()
        {
            r = new Random();
        }
        #endregion


        #region Api
        public static void RunTest()
        {
            Algorithm.PerlinNoiseGeneratorConfig cfg = new Algorithm.PerlinNoiseGeneratorConfig()
            {
                Amplify = false,
                Width = 300,
                Height = 300,
                Iteration = 5,
                Scale = 50f,
                SmoothIteration = 4,
                Seed = 1234
            };
            double[] noise = Algorithm.PerlinNoiseGenerator.Generate2DNoise(cfg);
            GenerateRandomHill(Map.TilesData, 300, 300, noise, 10, 0);
            GlobalVar.CurrentMapDocument.SaveMapAs("D:\\", "111.map");
        }
        /// <summary>
        /// require normalized noise array
        /// </summary>
        /// <param name="targets"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="randomNoise"></param>
        /// <param name="maxTileHeight"></param>
        /// <param name="baseHeight"></param>
        /// <param name="canyon"></param>
        public static void GenerateRandomHill(IEnumerable<Tile> targets, int width, int height, double[] randomNoise, int maxTileHeight, int baseHeight, bool canyon = false, int iteration = 2)
        {
            double getHeight(I2dLocateable pos)
            {
                return randomNoise[pos.Y * width + pos.X];
            }
            /// centerHeight = normalizedDelta + referanceHeight
            int normalizeDelta(double centerNoise, byte referanceHeight)
            {
                double delta = centerNoise - referanceHeight;
                if (Math.Round(Math.Abs(delta)) >= 1) return delta > 0 ? 1 : -1;
                else return 0;
            }
            if (targets.Count() == 0) return;
            ReloadBaseData();
            // pre-process noise
            ProcessNoise(ref randomNoise, width, height, maxTileHeight, baseHeight, !canyon);

            // set height first, then deal with ramp slope
            void applyNoiseHeight()
            {
                HashSet<Tile> heightSetted = new HashSet<Tile>();
                foreach (Tile center in targets)
                {
                    MapApi.GetAdjacentTileAround(center, out Tile[] dests, out WallDirection[] dirs);
                    bool freeSet = true;
                    double noiseValue = getHeight(center);
                    for (int i = 0; i < dests.Length; i++)
                    {
                        Tile referance = dests[i];
                        if (heightSetted.Contains(referance))
                        {
                            freeSet = false;
                            int delta = normalizeDelta(noiseValue, referance.Height);
                            SetHeight(center, delta + referance.Height);
                        }
                    }
                    if (freeSet)
                    {
                        SetHeight(center, RsMath.Round(noiseValue));
                        heightSetted.Add(center);
                    }
                }
            }

            applyNoiseHeight();
            GlobalVar.CurrentMapDocument.SaveMapAs("D:\\", "org.map");
            while (iteration-- > 0) SmoothRampIn(targets);
        }
        public static void RoughRampIn(IEnumerable<Tile> targets, int iteration = 2)
        {
            InitializeRampLookup();
            if (targets.Count() == 0) return;
            ReloadBaseData();
            HashSet<Tile> ramped = new HashSet<Tile>();
            HashSet<Tile> org = targets.ToHashSet();
            HashSet<Tile> reRamp = new HashSet<Tile>();
            foreach (Tile center in targets)
            {
                int centerRamp;
                RampData centerData,
                    ul = RampData.IgnoreRamp, ur = RampData.IgnoreRamp, dl = RampData.IgnoreRamp, dr = RampData.IgnoreRamp;
                MapApi.GetAdjacentTileAround(center, out Tile[] dests, out WallDirection[] dirs);
                for (int i = 0; i < dests.Length; i++)
                {
                    Tile referance = dests[i];
                    WallDirection dir = dirs[i];
                    RampData ramp = GetRampData(referance);
                    if (ramped.Contains(referance))
                    {
                        switch (dir)
                        {
                            case WallDirection.NW:
                                dr = ramp;
                                break;
                            case WallDirection.NE:
                                dl = ramp;
                                break;
                            case WallDirection.SW:
                                ur = ramp;
                                break;
                            case WallDirection.SE:
                                ul = ramp;
                                break;
                        }
                    }
                    if (!org.Contains(referance))
                    {
                        switch (dir)
                        {
                            case WallDirection.NW:
                                dr = RampData.FlatRamp;
                                break;
                            case WallDirection.NE:
                                dl = RampData.FlatRamp;
                                break;
                            case WallDirection.SW:
                                ur = RampData.FlatRamp;
                                break;
                            case WallDirection.SE:
                                ul = RampData.FlatRamp;
                                break;
                        }
                    }
                }
                centerData = GetValidRampData(ul, ur, dr, dl, true);
                centerRamp = centerData.Offset;
                if (!centerData.IsFlat)
                {
                    MapApi.SetTile(centerRamp + rampBaseIndex - 1, 0, center);
                    ramped.Add(center);
                }
                else
                {
                    MapApi.SetTile(0, 0, center);
                    ramped.Add(center);
                    reRamp.Add(center);
                }
                //if (IsNotRamp(center))
                //{
                //    centerRamp = RandomRampOffsetSameLevel;
                //    centerData = Data[centerRamp];
                //    MapApi.SetTile(centerRamp + rampBaseIndex, 0, center);
                //}
                //else
                //{
                //    centerRamp = center.TileIndex - rampBaseIndex;
                //    centerData = Data[centerRamp];
                //}
            }
            if (iteration > 0) RoughRampIn(reRamp, iteration - 1);
        }
        public static void SmoothRampIn(IEnumerable<Tile> targets)
        {
            InitializeRampLookup();
            targets = targets.OrderBy(x => x.X).ThenBy(x => x.Y);
            HashSet<Tile> ramped = new HashSet<Tile>();
            HashSet<Tile> org = targets.ToHashSet();
            for (int lvl = Constant.DrawingEngine.MapMaxHeightDrawing; lvl >= 0; lvl--)
            {
                IEnumerable<Tile> currentLevel = targets.Where(x => x.Height == lvl);
                foreach (Tile center in currentLevel)
                {
                    bool skipCenter(Tile t)
                    {
                        return ramped.Contains(t);
                    }
                    void callback(Tile t)
                    {
                        ramped.Add(t);
                    }
                    MapApi.GetAdjacentTileAround(center, out Tile[] adjs, out WallDirection[] _);
                    adjs.Foreach(x => SmoothRampAt(x, skipCenter, callback));
                    MapApi.GetDiagonalTileAround(center, out Tile[] diags, out WallDirection[] _);
                    diags.Foreach(x => SmoothRampAt(x, skipCenter, callback));
                }
            }
        }
        #endregion


        #region Private
        private static RampData LookupRampData(RampSideSection ul, RampSideSection ur, RampSideSection dr, RampSideSection dl, out bool failed)
        {
            failed = false;
            int code =
                (ul.IdentityCode << 12) |
                (ur.IdentityCode << 8) |
                (dr.IdentityCode << 4) |
                dl.IdentityCode;
            if (rampLookup.ContainsKey(code)) return rampLookup[code];
            failed = true;
            return RampData.IgnoreRamp;
        }
        private static Dictionary<int, RampData> rampLookup;
        private static bool isRampLookupInitialized = false;
        private static void InitializeRampLookup()
        {
            if (!isRampLookupInitialized)
            {
                rampLookup = new Dictionary<int, RampData>();
                for (int i = 0; i < 11; i++)
                {
                    RampSideSection ul = RampSideSection.FromIdentityCode(i);
                    for (int j = 0; j < 11; j++)
                    {
                        RampSideSection ur = RampSideSection.FromIdentityCode(j);
                        for (int k = 0; k < 11; k++)
                        {
                            RampSideSection dr = RampSideSection.FromIdentityCode(k);
                            for (int l = 0; l < 11; l++)
                            {
                                RampSideSection dl = RampSideSection.FromIdentityCode(l);
                                RampData ramp = GetFirstValidRamp(ul, ur, dr, dl, out bool failed);
                                if (failed) continue;
                                int code = (i << 12) | (j << 8) | (k << 4) | l;
                                rampLookup[code] = ramp;
                            }
                        }
                    }
                }
                isRampLookupInitialized = true;
            }
        }
        private static void SmoothRampAt(Tile center, Predicate<Tile> skipCenter, Action<Tile> callback)
        {
            RampSideSection ul = RampSideSection.Ignore, ur = RampSideSection.Ignore, dl = RampSideSection.Ignore, dr = RampSideSection.Ignore;
            if (skipCenter(center)) return;
            MapApi.GetAdjacentTileAround(center, out Tile[] adjs, out WallDirection[] dirs);
            for (int i = 0; i < adjs.Length; i++)
            {
                Tile referance = adjs[i];
                WallDirection dir = dirs[i];
                bool bindCondition = false;
                if (IsNotRamp(referance))
                {
                    // higher flat ground
                    if (referance.Height > center.Height) bindCondition = true;
                }
                else bindCondition = true;
                if (bindCondition)
                {
                    RampData ramp = GetRampData(referance);
                    switch (dir)
                    {
                        case WallDirection.NW:
                            dr = ramp.Sections[0].Copy();
                            dr.Height += referance.Height - center.Height;
                            break;
                        case WallDirection.NE:
                            dl = ramp.Sections[1].Copy();
                            dl.Height += referance.Height - center.Height;
                            break;
                        case WallDirection.SW:
                            ur = ramp.Sections[3].Copy();
                            ur.Height += referance.Height - center.Height;
                            break;
                        case WallDirection.SE:
                            ul = ramp.Sections[2].Copy();
                            ul.Height += referance.Height - center.Height;
                            break;
                    }
                }
            }
            RampData centerRamp = LookupRampData(ul, ur, dr, dl, out bool failed);
            if (failed)
            {
                MapApi.SetTile(0, 0, center);
                center.Rise();
            }
            else
            {
                if (!centerRamp.IsFlat) MapApi.SetTile(centerRamp.Offset + rampBaseIndex - 1, 0, center);
            }
            callback.Invoke(center);
        }
        private static void ProcessNoise(ref double[] noise, int noiseWidth, int noiseHeight, int maxHeight, int baseHeight, bool ignoreNegative)
        {
            int delta = maxHeight - baseHeight;
            for (int i = 0; i < noise.Length; i++)
            {
                noise[i] = (noise[i] + 1) / 2;
            }
            if (ignoreNegative)
            {
                for (int i = 0; i < noise.Length; i++)
                {
                    noise[i] *= delta;
                    if (noise[i] < baseHeight) noise[i] = baseHeight;
                }
            }
            else
            {
                for (int i = 0; i < noise.Length; i++)
                {
                    noise[i] *= delta;
                }
            }
        }
        private static void SetHeight(Tile centerTarget, int height)
        {
            centerTarget.SetHeightTo(height);
        }
        private static void ReloadBaseData()
        {
            TileSet ramp = GlobalVar.TileDictionary.GetRampSet();
            rampBaseIndex = ramp.Offset;
        }
        private static RampData GetRampData(Tile src)
        {
            if (IsNotRamp(src)) return RampData.FlatRamp;
            int offset = src.TileIndex - rampBaseIndex;
            return Data[offset];
        }
        private static bool IsNotRamp(Tile src)
        {
            return src.TileIndex < rampBaseIndex || src.TileIndex >= rampBaseIndex + 20;
        }
        private static RampData GetFirstValidRamp(RampSideSection ul, RampSideSection ur, RampSideSection dr, RampSideSection dl, out bool solutionFailed)
        {
            solutionFailed = false;
            if (ul.IsNoBindFlat && ur.IsNoBindFlat && dr.IsNoBindFlat && dl.IsNoBindFlat) return RampData.FlatRamp;
            IEnumerable<RampData> results = Data.Where(x =>
            {
                return
                    (ul.IsNoBindFlat || (x.Sections[0] == ul)) &&
                    (ur.IsNoBindFlat || (x.Sections[1] == ur)) &&
                    (dr.IsNoBindFlat || (x.Sections[2] == dr)) &&
                    (dl.IsNoBindFlat || (x.Sections[3] == dl));
            });
            if (results.Any()) return results.First();
            solutionFailed = true;
            return RampData.FlatRamp;
        }
        /// <summary>
        /// randomize pick any valid ramp
        /// </summary>
        /// <param name="ul"></param>
        /// <param name="ur"></param>
        /// <param name="dr"></param>
        /// <param name="dl"></param>
        /// <param name="sameLevel"></param>
        /// <returns></returns>
        private static RampData GetValidRampData(RampData ul, RampData ur, RampData dr, RampData dl, bool sameLevel = false)
        {
            RampSideSection ulSide = ul.IsIgnore ? RampSideSection.Ignore : ul.Sections[2];
            RampSideSection urSide = ur.IsIgnore ? RampSideSection.Ignore : ur.Sections[3];
            RampSideSection drSide = dr.IsIgnore ? RampSideSection.Ignore : dr.Sections[0];
            RampSideSection dlSide = dl.IsIgnore ? RampSideSection.Ignore : dl.Sections[1];
            RampData[] results = Data.Where(x =>
            {
                if (sameLevel)
                {
                    return
                        (ulSide.IsIgnore || x.Sections[0] == ulSide) &&
                        (urSide.IsIgnore || x.Sections[1] == urSide) &&
                        (drSide.IsIgnore || x.Sections[2] == drSide) &&
                        (dlSide.IsIgnore || x.Sections[3] == dlSide) &&
                        !x.StretchHeight;
                }
                else
                {
                    return
                        (ulSide.IsIgnore || x.Sections[0] == ulSide) &&
                        (urSide.IsIgnore || x.Sections[1] == urSide) &&
                        (drSide.IsIgnore || x.Sections[2] == drSide) &&
                        (dlSide.IsIgnore || x.Sections[3] == dlSide);
                }
            }).ToArray();
            if (results.Length > 0)
            {
                return results[r.Next(0, results.Length)];
            }
            return RampData.FlatRamp;
        }
        #endregion


        #region Calls

        #endregion


        #region Defines
        #endregion
    }
}
