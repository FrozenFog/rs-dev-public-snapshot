using RelertSharp.Common;
using RelertSharp.MapStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.ViewModel
{
    internal class TileVm : BaseListVm<Tile>
    {
        private string fileName, setName;
        public TileVm(Tile t)
        {
            data = t;
            fileName = GlobalVar.TileDictionary[t.TileIndex];
            setName = GlobalVar.TileDictionary.GetTileSetFromTile(t).SetName;
        }


        #region Calls
        #region Bind
        public int TileIndex
        {
            get { return data.TileIndex; }
        }
        public int SubIndex
        {
            get { return data.SubIndex; }
        }
        public int X
        {
            get { return data.X; }
        }
        public int Y
        {
            get { return data.Y; }
        }
        public int Height
        {
            get { return data.RealHeight; }
        }
        public string FileName
        {
            get { return fileName; }
        }
        public string SetName
        {
            get { return setName; }
        }
        public string Passable
        {
            get { return data.Passable.YesNo(); }
        }
        public string Buildable
        {
            get { return data.Buildable.YesNo(); }
        }
        public int TerrainType
        {
            get { return data.TileTerrainType; }
        }
        public int RampType
        {
            get { return data.RampType; }
        }
        #endregion
        #endregion
    }
}
