using RelertSharp.MapStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.ViewModel
{
    internal class TileSetVm : BaseTreeVm<TileSet>
    {
        public TileSetVm(string title)
        {
            Title = title;
        }
        public TileSetVm(TileSet set)
        {
            data = set;
        }


        #region Calls
        public string Title { get; private set; }
        #endregion
    }
}
