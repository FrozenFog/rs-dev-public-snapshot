using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;

namespace RelertSharp.DrawingEngine.Drawables
{
    internal class DrawableTile : DrawableBase, IDrawableBase
    {
        #region Ctor - DrawableTile
        public DrawableTile() { }
        #endregion


        #region Public Calls - DrawableTile
        public List<SubTile> SubTiles { get; private set; } = new List<SubTile>();
        public SubTile this[int subtile] { get { return SubTiles[subtile]; } }
        #endregion


        #region Public Methods - DrawableTile
        #region Enumerator

        #endregion
        #endregion


        public class SubTile
        {
            #region Ctor - SubTile
            public SubTile() { }
            #endregion


            #region Public Calls - SubTile
            public ColorPair RadarColor { get; set; }
            public bool Buildable { get; set; }
            public bool LandPassable { get; set; }
            public bool WaterPassable { get; set; }
            public int TerrainType { get; set; }
            #endregion
        }


        public class ColorPair
        {
            public ColorPair(Color left, Color right)
            {
                Left = left;
                Right = right;
            }
            public Color Left { get; private set; }
            public Color Right { get; private set; }
        }
    }
}
