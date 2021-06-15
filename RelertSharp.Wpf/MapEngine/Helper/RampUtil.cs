using System;
using System.Collections.Generic;
using RelertSharp.MapStructure;
using RelertSharp.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.MapEngine.Helper
{
    internal static class RampUtil
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
        private static int RandomRampOffsetSameLevel
        {
            get
            {
                int rnd = -1;
                while (rnd < 0 || (rnd >= 13 || rnd <= 16))
                {
                    rnd = RandomRampOffset;
                }
                return rnd;
            }
        }
        #endregion


        #region ctor & listener
        static RampUtil()
        {
            Engine.Api.EngineApi.TheaterReloaded += TheaterReloadedHandler;
            r = new Random(1234);
        }

        private static void TheaterReloadedHandler(object sender, EventArgs e)
        {
            Reload();
        }
        #endregion


        #region Api
        public static void RunTest()
        {
            //List<Tile> targets = new List<Tile>();
            //Pnt p = new Pnt(87, 82);
            //int xMax = 1;
            //for (int y = 0; y < 30; y++)
            //{
            //    for (int x = 0; x < xMax; x++)
            //    {
            //        I2dLocateable pos = p + new Pnt(x, y);
            //        targets.Add(Map.TilesData[pos]);
            //    }
            //    xMax++;
            //}
            Algorithm.PerlinNoiseGeneratorConfig cfg = new Algorithm.PerlinNoiseGeneratorConfig()
            {
                Amplify = false,
                Width = 100,
                Height = 100,
                Iteration = 4,
                Scale = 5f,
                SmoothIteration = 4,
                Seed = 1234
            };
            double[] noise = RelertSharp.Algorithm.PerlinNoiseGenerator.Generate2DNoise(cfg);
            GenerateRandomHill(Map.TilesData, 100, 100, noise, 6, 0);
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
        public static void GenerateRandomHill(IEnumerable<Tile> targets, int width, int height, double[] randomNoise, int maxTileHeight, int baseHeight, bool canyon = false)
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
            Reload();
            HashSet<Tile> ramped = new HashSet<Tile>();
            HashSet<Tile> org = targets.ToHashSet();
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
                    for (int i = 0; i< dests.Length; i++)
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
            SmoothRampIn(targets);
        }
        public static void RoughRampIn(IEnumerable<Tile> targets, int iteration = 2)
        {
            if (targets.Count() == 0) return;
            Reload();
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
            HashSet<Tile> ramped = new HashSet<Tile>();
            HashSet<Tile> org = targets.ToHashSet();
            for (int lvl = 15; lvl >= 0; lvl--)
            {
                IEnumerable<Tile> currentLevel = targets.Where(x => x.Height == lvl);
                foreach (Tile center in currentLevel)
                {
                    int centerRamp;
                    RampData centerData,
                        ul = RampData.IgnoreRamp, ur = RampData.IgnoreRamp, dl = RampData.IgnoreRamp, dr = RampData.IgnoreRamp;
                    MapApi.GetAdjacentTileAround(center, out Tile[] dests, out WallDirection[] dirs);
                    centerData = GetRampData(center);
                    for (int i = 0; i< dests.Length; i++)
                    {
                        Tile referance = dests[i];
                        WallDirection dir = dirs[i];
                        int rampHeightFix = center.Height - referance.Height;
                        RampData ramp = GetRampData(referance);
                        ramp.HeightFix = rampHeightFix;
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
                }
            }
            //foreach (Tile center in targets)
            //{
            //    int centerRamp;
            //    RampData centerData,
            //        ul = RampData.IgnoreRamp, ur = RampData.IgnoreRamp, dl = RampData.IgnoreRamp, dr = RampData.IgnoreRamp;
            //    if (!heightSetted.Contains(center))
            //    {
            //        SetHeight(center, RsMath.Round(hCenter));
            //        heightSetted.Add(center);
            //    }
            //    MapApi.GetAdjacentTileAround(center, out Tile[] dests, out WallDirection[] dirs);
            //    for (int i = 0; i < dests.Length; i++)
            //    {
            //        Tile referance = dests[i];
            //        WallDirection dir = dirs[i];
            //        int rampHeightFix = referance.Height - center.Height;
            //        if (!heightSetted.Contains(referance))
            //        {
            //            double hReferance = getHeight(center);
            //            double hDelta = hReferance - hCenter;
            //            if (Math.Abs(hDelta) > 1)
            //            {
            //                rampHeightFix = (int)(hDelta / Math.Abs(hDelta));
            //                hReferance = hCenter + rampHeightFix;
            //            }
            //            else
            //            {
            //                hReferance = hCenter;
            //            }
            //            SetHeight(referance, (int)hReferance);
            //            heightSetted.Add(referance);
            //        }
            //        RampData ramp = GetRampData(referance);
            //        ramp.HeightFix = rampHeightFix;
            //        if (ramped.Contains(referance))
            //        {
            //            switch (dir)
            //            {
            //                case WallDirection.NW:
            //                    dr = ramp;
            //                    break;
            //                case WallDirection.NE:
            //                    dl = ramp;
            //                    break;
            //                case WallDirection.SW:
            //                    ur = ramp;
            //                    break;
            //                case WallDirection.SE:
            //                    ul = ramp;
            //                    break;
            //            }
            //        }
            //        if (!org.Contains(referance))
            //        {
            //            switch (dir)
            //            {
            //                case WallDirection.NW:
            //                    dr = RampData.FlatRamp;
            //                    break;
            //                case WallDirection.NE:
            //                    dl = RampData.FlatRamp;
            //                    break;
            //                case WallDirection.SW:
            //                    ur = RampData.FlatRamp;
            //                    break;
            //                case WallDirection.SE:
            //                    ul = RampData.FlatRamp;
            //                    break;
            //            }
            //        }
            //    }
            //    centerData = GetFixRampData(ul, ur, dr, dl);
            //    centerRamp = centerData.Offset;
            //    if (!centerData.IsFlat)
            //    {
            //        MapApi.SetTile(centerRamp + rampBaseIndex - 1, 0, center);
            //        ramped.Add(center);
            //    }
            //    else
            //    {
            //        MapApi.SetTile(0, 0, center);
            //        ramped.Add(center);
            //    }
            //    if (IsNotRamp(center))
            //    {
            //        centerRamp = RandomRampOffsetSameLevel;
            //        centerData = Data[centerRamp];
            //        MapApi.SetTile(centerRamp + rampBaseIndex, 0, center);
            //    }
            //    else
            //    {
            //        centerRamp = center.TileIndex - rampBaseIndex;
            //        centerData = Data[centerRamp];
            //    }
            //}
        }
        #endregion


        #region Private
        private static void GetAdjacentRampData(Tile center, out RampData ul, out RampData ur, out RampData dl, out RampData dr)
        {
            ul = RampData.IgnoreRamp; ur = RampData.IgnoreRamp; dl = RampData.IgnoreRamp; dr = RampData.IgnoreRamp;
            MapApi.GetAdjacentTileAround(center, out Tile[] dests, out WallDirection[] dirs);
            for (int i = 0; i < dests.Length; i++)
            {
                Tile referance = dests[i];
                WallDirection dir = dirs[i];
                int rampHeightFix = center.Height - referance.Height;
                RampData ramp = GetRampData(referance);
                ramp.HeightFix = rampHeightFix;
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
        }
        private static void ProcessNoise(ref double[] noise, int noiseWidth, int noiseHeight, int maxHeight, int baseHeight, bool ignoreNegative)
        {
            int delta = maxHeight - baseHeight;
            for (int i = 0; i< noise.Length; i++)
            {
                noise[i] = (noise[i] + 1) / 2;
            }
            if (ignoreNegative)
            {
                for (int i = 0; i< noise.Length; i++)
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
        private static void Reload()
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
        /// <summary>
        /// strickly use first available ramp
        /// </summary>
        /// <param name="ul"></param>
        /// <param name="ur"></param>
        /// <param name="dr"></param>
        /// <param name="dl"></param>
        /// <returns></returns>
        private static RampData GetFixRampData(RampData ul, RampData ur, RampData dr, RampData dl)
        {
            RampSideSection ulSide = ul.IsIgnore ? RampSideSection.Ignore : ul.Sections[2];
            RampSideSection urSide = ur.IsIgnore ? RampSideSection.Ignore : ur.Sections[3];
            RampSideSection drSide = dr.IsIgnore ? RampSideSection.Ignore : dr.Sections[0];
            RampSideSection dlSide = dl.IsIgnore ? RampSideSection.Ignore : dl.Sections[1];
            if (ulSide.IsNoBindFlat && urSide.IsNoBindFlat && drSide.IsNoBindFlat && dlSide.IsNoBindFlat) return RampData.FlatRamp;
            RampData[] results = Data.Where(x =>
            {
                return
                    (ulSide.IsIgnore || (x.Sections[0].SlopeType == ulSide.SlopeType && x.Sections[0].Height + ul.HeightFix == ulSide.Height)) &&
                    (urSide.IsIgnore || (x.Sections[1].SlopeType == urSide.SlopeType && x.Sections[1].Height + ur.HeightFix == urSide.Height)) &&
                    (drSide.IsIgnore || (x.Sections[2].SlopeType == drSide.SlopeType && x.Sections[2].Height + dr.HeightFix == drSide.Height)) &&
                    (dlSide.IsIgnore || (x.Sections[3].SlopeType == dlSide.SlopeType && x.Sections[3].Height + dl.HeightFix == dlSide.Height));
            }).ToArray();
            if (results.Length > 0)
            {
                return results.First();
            }
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
        private enum RampSlopeType
        {
            Ignore = -1,
            Flat = 0,
            Xz1 = 1, Xz2 = 2,
            Yz1 = 3, Yz2 = 4
        }
        private struct RampSideSection
        {
            public RampSlopeType SlopeType;
            public int Height;
            public RampSideSection(int height, int type)
            {
                SlopeType = (RampSlopeType)type;
                Height = height;
            }
            public bool IsIgnore { get { return SlopeType == RampSlopeType.Ignore; } }
            public bool IsNoBindFlat { get { return (SlopeType == RampSlopeType.Flat && Height == 0) || IsIgnore; } }
            public static bool operator==(RampSideSection a, RampSideSection b)
            {
                return a.SlopeType == b.SlopeType && a.Height == b.Height;
            }
            public static bool operator!=(RampSideSection a, RampSideSection b)
            {
                return a.SlopeType != b.SlopeType || a.Height != b.Height;
            }
            public static RampSideSection Flat
            {
                get { return new RampSideSection(0, 0); }
            }
            public static RampSideSection Ignore
            {
                get { return new RampSideSection(-1, -1); }
            }
        }
        private struct RampData
        {
            public RampSideSection[] Sections { get; private set; }
            public int Offset { get; private set; }
            public int HeightFix { get; set; }
            public RampData(string param, int offset)
            {
                List<RampSideSection> results = new List<RampSideSection>();
                RampSideSection func(char height, char type)
                {
                    RampSideSection r = new RampSideSection(int.Parse(height.ToString()), int.Parse(type.ToString()));
                    return r;
                }
                for (int i = 0; i< param.Length;)
                {
                    RampSideSection data = func(param[i++], param[i++]);
                    results.Add(data);
                }
                Sections = results.ToArray();
                Offset = offset;
                HeightFix = 0;
            }
            public bool IsFlat
            {
                get { return Offset == 0; }
            }
            public bool IsIgnore
            {
                get { return Offset == -1; }
            }
            public bool StretchHeight
            {
                get { return Sections.Any(x => x.Height == 2); }
            }
            public static RampData FlatRamp
            {
                get
                {
                    return new RampData("00000000", 0);
                }
            }
            public static RampData IgnoreRamp
            {
                get
                {
                    return new RampData("00000000", -1);
                }
            }
        }
        #endregion
    }
}
