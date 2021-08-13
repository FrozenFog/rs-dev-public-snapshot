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
    public static class RampCalc
    {
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
        private static Random r = new Random();

        #region Api
        public static List<Tile> LookupRampAt(I2dLocateable cell)
        {
            List<Tile> result = new List<Tile>();
            if (GlobalVar.GlobalMap.TilesData[cell] is Tile t)
            {
                Tile center = new Tile(0, 0, t.X, t.Y, t.RealHeight);
                InitializeRampLookup();
                ReloadBaseData();
                result = LookupAllRampAt(center);
            }
            return result;
        }
        public static void SmoothRampIn(IEnumerable<Tile> targets, HillGeneratorConfig cfg)
        {
            InitializeRampLookup();
            ReloadBaseData();
            targets = targets.OrderBy(x => x.X).ThenBy(x => x.Y);
            HashSet<Tile> ramped = new HashSet<Tile>();
            HashSet<Tile> org = targets.ToHashSet();
            for (int lvl = Constant.DrawingEngine.MapMaxHeightDrawing; lvl >= 0; lvl--)
            {
                IEnumerable<Tile> currentLevel = targets.Where(x => x.Height == lvl);
                foreach (Tile center in currentLevel)
                {
                    bool skipCondition(Tile t)
                    {
                        if (ramped.Contains(t)) return true;
                        if (cfg.RampBorderUnaffect) return !targets.Contains(t);
                        return false;
                    }
                    void callback(Tile t)
                    {
                        ramped.Add(t);
                    }
                    SmoothRampAt(center, skipCondition, callback, cfg.RampFixBorderTreatAsFlat, cfg.RiseUnsolvedTile);
                    MapApi.GetAdjacentTileAround(center, out Tile[] adjs, out WallDirection[] _);
                    adjs.Foreach(x => SmoothRampAt(x, skipCondition, callback, cfg.RampFixBorderTreatAsFlat, cfg.RiseUnsolvedTile));
                    MapApi.GetDiagonalTileAround(center, out Tile[] diags, out WallDirection[] _);
                    diags.Foreach(x => SmoothRampAt(x, skipCondition, callback, cfg.RampFixBorderTreatAsFlat, cfg.RiseUnsolvedTile));
                }
            }
        }
        public static void RoughRampIn(IEnumerable<Tile> targets, HillGeneratorConfig cfg)
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
            if (cfg.Iteration-- > 0) RoughRampIn(reRamp, cfg);
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
        private static List<Tile> LookupAllRampAt(Tile center)
        {
            List<Tile> result = new List<Tile>();
            RampSideSection ul = RampSideSection.Ignore, ur = RampSideSection.Ignore, dl = RampSideSection.Ignore, dr = RampSideSection.Ignore;
            MapApi.GetAdjacentTileAround(center, out Tile[] adjs, out WallDirection[] dirs);
            for (int i = 0; i < adjs.Length; i++)
            {
                Tile referance = adjs[i];
                WallDirection dir = dirs[i];
                bool bindCondition = false;
                if (IsNotRamp(referance))
                {
                    // higher flat ground
                    if (referance.RealHeight > center.RealHeight) bindCondition = true;
                }
                else
                {
                    if (referance.RealHeight < center.RealHeight) bindCondition = false;
                    else bindCondition = true;
                }

                // if bind, this side will strictly use referance ramp as lookup condition
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
            IEnumerable<RampData> ramps = GetAllValidRamp(ul, ur, dr, dl, out bool failed);
            if (failed || (ramps.Count() == 1 && ramps.First().IsFlat))
            {
                result.Add(new Tile(0, 0, 0, 0, 0));
            }
            else
            {
                ramps.Foreach(x => result.Add(new Tile(x.Offset + rampBaseIndex - 1, 0, 0, 0, 0)));
            }
            return result;
        }
        private static void SmoothRampAt(Tile center, Predicate<Tile> skipCondition, Action<Tile> callback, bool borderFlat, bool riseUnsolved = true)
        {
            RampSideSection ul = RampSideSection.Ignore, ur = RampSideSection.Ignore, dl = RampSideSection.Ignore, dr = RampSideSection.Ignore;
            if (skipCondition != null && skipCondition(center)) return;
            MapApi.GetAdjacentTileAround(center, out Tile[] adjs, out WallDirection[] dirs);
            for (int i = 0; i < adjs.Length; i++)
            {
                Tile referance = adjs[i];
                WallDirection dir = dirs[i];
                bool bindCondition = false;
                if (IsNotRamp(referance))
                {
                    // higher flat ground
                    if (referance.RealHeight > center.RealHeight) bindCondition = true;
                }
                else
                {
                    if (referance.RealHeight < center.RealHeight) bindCondition = false;
                    else bindCondition = true;
                }

                // if bind, this side will strictly use referance ramp as lookup condition
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
                if (riseUnsolved) center.Rise();
            }
            else
            {
                if (!centerRamp.IsFlat) MapApi.SetTile(centerRamp.Offset + rampBaseIndex - 1, 0, center);
            }
            callback?.Invoke(center);
        }
        private static RampData GetRampData(Tile src)
        {
            if (IsNotRamp(src)) return RampData.FlatRamp;
            int offset = src.RampType - 1;
            //int offset = src.TileIndex - rampBaseIndex;
            return Data[offset];
        }
        private static bool IsNotRamp(Tile src)
        {
            return !src.IsRamp;
        }
        private static IEnumerable<RampData> GetAllValidRamp(RampSideSection ul, RampSideSection ur, RampSideSection dr, RampSideSection dl, out bool failed)
        {
            failed = false;
            List<RampData> result = new List<RampData>();
            if (ul.IsNoBindFlat && ur.IsNoBindFlat && dr.IsNoBindFlat && dl.IsNoBindFlat)
            {
                result.Add(RampData.FlatRamp);
                return result;
            }
            IEnumerable<RampData> lookup = Data.Where(x =>
            {
                return
                    (ul.IsNoBindFlat || (x.Sections[0] == ul)) &&
                    (ur.IsNoBindFlat || (x.Sections[1] == ur)) &&
                    (dr.IsNoBindFlat || (x.Sections[2] == dr)) &&
                    (dl.IsNoBindFlat || (x.Sections[3] == dl));
            });
            if (!lookup.Any())
            {
                failed = true;
                result.Add(RampData.FlatRamp);
                return result;
            }
            return lookup;
        }
        /// <summary>
        /// Will use minimum height tatic
        /// </summary>
        /// <param name="ul"></param>
        /// <param name="ur"></param>
        /// <param name="dr"></param>
        /// <param name="dl"></param>
        /// <param name="solutionFailed"></param>
        /// <returns></returns>
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
            if (results.Any())
            {
                int minumunHeight = results.Min(x => x.Sections.Sum(sec => sec.Height));
                return results.Where(x => x.Sections.Sum(sec => sec.Height) == minumunHeight).First();
            }
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
        internal static void ReloadBaseData()
        {
            TileSet ramp = GlobalVar.TileDictionary.GetRampSet();
            rampBaseIndex = ramp.Offset;
        }
        #endregion
    }
}
