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
        #region Bind Filter
        public bool IsSearchAircraft { get; set; }
        public bool IsSearchUnit { get; set; }
        public bool IsSearchInfantry { get; set; }
        public bool IsSearchBuilding { get; set; }
        public bool IsSearchBaseNode { get; set; }
        public bool IsSearchOverlay { get; set; }
        public bool IsSearchCelltag { get; set; }
        public bool IsSearchTerrain { get; set; }
        public bool IsSearchSmudge { get; set; }
        public bool IsSearchWaypoint { get; set; }
        public bool IsSearchTrigger { get; set; }
        public bool IsSearchTeam { get; set; }
        public bool IsSearchTaskforce { get; set; }
        public bool IsSearchScript { get; set; }
        public bool IsSearchTag { get; set; }
        public bool IsSearchCsf { get; set; }
        public bool IsSearchSound { get; set; }
        public bool IsSearchMusic { get; set; }
        public bool IsSearchEva { get; set; }
        #endregion
        #endregion
    }
}
