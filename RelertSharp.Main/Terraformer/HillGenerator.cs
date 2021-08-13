using RelertSharp.Common;
using RelertSharp.MapStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RelertSharp.Terraformer.RampBase;
using static RelertSharp.Terraformer.RampCalc;

namespace RelertSharp.Terraformer
{
    public class HillGeneratorConfig
    {
        /// <summary>
        /// The max Z height of the generated hill
        /// </summary>
        public int MaxHeight { get; set; }
        /// <summary>
        /// The min Z height of the generated hill
        /// </summary>
        public int BaseHeight { get; set; }
        /// <summary>
        /// Will hill generator generate canyon
        /// currently useless
        /// </summary>
        public bool GenerateCanyon { get; set; }
        /// <summary>
        /// Ramp smooth iteration
        /// default 2, more iteration may not help
        /// </summary>
        public int Iteration { get; set; } = 2;
        /// <summary>
        /// The X range of the generated hill
        /// </summary>
        public int HillWidth { get; set; }
        /// <summary>
        /// The Y range of the generated hill
        /// </summary>
        public int HillHeight { get; set; }
        /// <summary>
        /// Will treat all border(not included in area) tile as flat?
        /// if use Rough/Smooth ramp in map, set to false
        /// because Rough/Smooth ramp will use border tile as referance to fix ramp
        /// </summary>
        public bool RampFixBorderTreatAsFlat { get; set; }
        /// <summary>
        /// Will unsolved tile be risen?
        /// set to false if used in Rough/Smooth ramp Api.
        /// Default true
        /// </summary>
        public bool RiseUnsolvedTile { get; set; } = true;
        /// <summary>
        /// Will border(not included in area) be affected?
        /// </summary>
        public bool RampBorderUnaffect { get; set; }
    }
    public static class HillGenerator
    {
        #region Components
        private static Random r;
        private static Map Map { get { return GlobalVar.GlobalMap; } }
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
            //Algorithm.PerlinNoiseGeneratorConfig cfg = new Algorithm.PerlinNoiseGeneratorConfig()
            //{
            //    Amplify = false,
            //    Width = 300,
            //    Height = 300,
            //    Iteration = 5,
            //    Scale = 50f,
            //    SmoothIteration = 4,
            //    Seed = 1234
            //};
            //double[] noise = Algorithm.PerlinNoiseGenerator.Generate2DNoise(cfg);
            //GenerateRandomHill(Map.TilesData, 300, 300, noise, 10, 0);
            //GlobalVar.CurrentMapDocument.SaveMapAs("D:\\", "111.map");
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
        public static void GenerateRandomHill(IEnumerable<Tile> targets, double[] randomNoise, HillGeneratorConfig cfg)
        {
            double getHeight(I2dLocateable pos)
            {
                return randomNoise[pos.Y * cfg.HillWidth + pos.X];
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
            ProcessNoise(ref randomNoise, cfg.HillWidth, cfg.HillHeight, cfg.MaxHeight, cfg.BaseHeight, !cfg.GenerateCanyon);

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
            while (cfg.Iteration-- > 0) SmoothRampIn(targets, cfg);
        }
        #endregion


        #region Private
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
        #endregion


        #region Calls

        #endregion
    }
}
