using RelertSharp.MapStructure;
using RelertSharp.Wpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.MapEngine.Helper
{
    internal static class TilesetLookup
    {
        private static List<TileSetTreeVm> vms = new List<TileSetTreeVm>();

        #region Api
        public static TileSetItemVm LookupTileSetInfo(int setIndex, int subindex)
        {
            var tree = vms.Find(x => x.SetIndex == setIndex);
            return tree.SubTileCollection.ElementAt(subindex);
        }
        public static void RegistAdd(TileSetTreeVm vm)
        {
            vms.Add(vm);
        }
        #endregion
    }
}
