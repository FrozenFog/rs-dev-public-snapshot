using RelertSharp.Common;
using RelertSharp.MapStructure;
using RelertSharp.Terraformer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.MapEngine.Helper
{
    internal static class InteliBrush
    {
        private static int rampSelector = 0;
        private static List<Tile> inteliRampResult = new List<Tile>();
        private static I2dLocateable prevPos = new Pnt();


        #region Api
        /// <summary>
        /// Tile is not drawn
        /// return true if pos is changed
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static bool InteliRampAt(I2dLocateable cell)
        {
            bool changed = !RsMath.I2dEqual(cell, prevPos);
            if (changed)
            {
                inteliRampResult = RampCalc.LookupRampAt(cell);
                rampSelector = RsMath.TrimTo(rampSelector, 0, inteliRampResult.Count - 1);
                if (inteliRampResult.Count == 0) CurrentInteliRamp = Tile.EmptyTile;
                else CurrentInteliRamp = inteliRampResult[rampSelector];
            }
            prevPos = cell;
            return changed;
        }
        public static void IncreInteliRamp()
        {
            rampSelector++; rampSelector %= inteliRampResult.Count;
            CurrentInteliRamp = inteliRampResult[rampSelector];
        }
        public static void DecreInteliRamp()
        {
            rampSelector--;
            if (rampSelector < 0) rampSelector = inteliRampResult.Count - 1;
            CurrentInteliRamp = inteliRampResult[rampSelector];
        }
        #endregion


        #region Private
        #endregion


        #region Calls
        private static Tile _currentRamp = Tile.EmptyTile;
        public static Tile CurrentInteliRamp
        {
            get { return _currentRamp; }
            set
            {
                _currentRamp?.Dispose();
                _currentRamp = value;
            }
        }
        #endregion
    }
}
