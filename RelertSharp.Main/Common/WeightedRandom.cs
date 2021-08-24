using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Common
{
    public class WeightedRandom
    {
        private Random r;
        private double prev;
        private readonly int seed;
        private readonly bool isSeeded;
        public WeightedRandom()
        {
            r = new Random();
        }
        public WeightedRandom(int seed)
        {
            this.seed = seed;
            isSeeded = true;
            r = new Random(seed);
        }


        #region Public Methods
        public int NoRepeatNext()
        {
            if (Floor == Ceil || Ceil - Floor == 1) return Floor;
            int result = r.Next(Floor, Ceil);
            while (result == prev) result = r.Next(Floor, Ceil);
            prev = result;
            return result;
        }
        public double NextDouble()
        {
            return r.NextDouble();
        }
        public void Reset()
        {
            if (isSeeded) r = new Random();
            else r = new Random(seed);
        }
        #endregion


        #region Call
        /// <summary>
        /// Exclude
        /// </summary>
        public int Ceil { get; set; } = 1;
        /// <summary>
        /// Include
        /// </summary>
        public int Floor { get; set; }
        public int Seed { get { return seed; } }
        #endregion
    }
}
