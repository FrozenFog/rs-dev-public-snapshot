using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using RelertSharp.MapStructure;
using RelertSharp.Common;
using static RelertSharp.Common.GlobalVar;


namespace RelertSharp.GUI.Model
{
    public partial class MainWindowDataModel
    {
        private List<Tile> selectedTile = new List<Tile>();
        public void SelectTile(Tile t)
        {
            t.Select();
            selectedTile.Add(t);
        }
        public void DeSelectTile(Tile t)
        {
            t.DeSelect();
            selectedTile.Remove(t);
        }
        public void DeSelectTileAll()
        {
            foreach (Tile t in selectedTile) t.DeSelect();
            selectedTile.Clear();
            Engine.Refresh();
        }


        public void RiseTileAll()
        {
            foreach (Tile t in selectedTile) t.Rise();
            Engine.Refresh();
        }
        public void SinkTileAll()
        {
            foreach (Tile t in selectedTile) t.Sink();
            Engine.Refresh();
        }
        public Rectangle GetSelectedRegionSize()
        {
            int xmin = 1000, ymin = 1000, xmax = -100, ymax = -100;
            foreach(Tile t in selectedTile)
            {
                xmin = Math.Min(xmin, t.X);
                ymin = Math.Min(ymin, t.Y);
                xmax = Math.Max(xmax, t.X);
                ymax = Math.Max(ymax, t.Y);
            }
            return new Rectangle(xmin, ymin, xmax - xmin + 1, ymax - ymin + 1);
        }
        public void FillTilesAt(I3dLocateable cell, TileSet set)
        {
            Rectangle area = GetSelectedRegionSize();
            IEnumerable<Tile> newtiles = Bucket.FillTilesAt(cell, area, set);
            selectedTile.Clear();
            selectedTile.AddRange(newtiles);
        }


        public TileBrush.TileBucket Bucket { get; private set; } = new TileBrush.TileBucket();
    }
}
