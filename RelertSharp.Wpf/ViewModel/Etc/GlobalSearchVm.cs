using RelertSharp.Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RelertSharp.Wpf.ViewModel
{
    internal class GlobalSearchVm : BaseVm<object>
    {
        private MinimapSurface minimap;
        public GlobalSearchVm()
        {
            data = new object();
        }


        #region Minimap
        public void ResetMinimapSize(Rectangle mapSize)
        {
            minimap = new MinimapSurface(mapSize);
        }
        public void UpdateMinimap()
        {
            SetProperty(nameof(Minimap));
        }
        #endregion


        #region Calls
        public MinimapSurface Surface { get { return minimap; } }
        public ImageSource Minimap
        {
            get { return minimap?.Image.ToWpfImage(); }
        }
        #endregion
    }
}
