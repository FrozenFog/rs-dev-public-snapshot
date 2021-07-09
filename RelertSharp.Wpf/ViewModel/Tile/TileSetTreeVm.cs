using RelertSharp.MapStructure;
using RelertSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.FileSystem;

namespace RelertSharp.Wpf.ViewModel
{
    internal class TileSetTreeVm : BaseTreeVm<TileSet>
    {
        private List<TileSetItemVm> subtiles = new List<TileSetItemVm>();
        private MapTheaterTileSet TileDictionary { get { return GlobalVar.TileDictionary; } }
        public TileSetTreeVm(string title)
        {
            Title = title;
        }
        public TileSetTreeVm(TileSet set)
        {
            data = set;
            Title = set.SetName;
            int idx = 0;
            foreach (string name in set.GetNames())
            {
                string filename = TileDictionary.GetFrameworkNameSafe(name);
                if (filename.IsNullOrEmpty()) continue;
                TmpFile file = new TmpFile(GlobalVar.GlobalDir.GetRawByte(filename), filename);
                file.LoadColor(GlobalVar.TilePalette);
                subtiles.Add(new TileSetItemVm(file, set.Offset + idx++));
            }
        }
        public TileSetTreeVm(TileSetTreeVm src)
        {
            data = src.data;
            Title = src.Title;
            int idx = 0;
            foreach (string name in data.GetNames())
            {
                string filename = TileDictionary.GetFrameworkNameSafe(name);
                if (filename.IsNullOrEmpty()) continue;
                TmpFile file = new TmpFile(GlobalVar.GlobalDir.GetRawByte(filename), filename);
                file.LoadColor(GlobalVar.TilePalette);
                subtiles.Add(new TileSetItemVm(file, data.Offset + idx++));
            }
        }


        #region Calls
        public override string Title { get; }
        public IEnumerable<TileSetItemVm> SubTileCollection
        {
            get { return subtiles; }
        }
        public int SetIndex { get { return data.SetIndex; } }
        public bool IsCustomRoot { get; set; }
        public bool IsCustom
        {
            get
            {
                IBaseTreeVm<TileSet> ancestor = this;
                while (ancestor.Ancestor != null)
                {
                    ancestor = ancestor.Ancestor;
                }
                return (ancestor as TileSetTreeVm).IsCustomRoot;
            }
        }
        #endregion
    }
}
