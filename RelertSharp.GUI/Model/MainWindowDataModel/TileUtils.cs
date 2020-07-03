using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Engine.Refresh();
        }
        public void DeSelectTile(Tile t)
        {
            t.DeSelect();
            selectedTile.Remove(t);
            Engine.Refresh();
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
    }
}
