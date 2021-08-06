using RelertSharp.Wpf.MapEngine.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.ViewModel
{
    internal class MainWindowVm : BaseVm<object>
    {
        private const string GROUP_MOUSE = "mouse";
        public MainWindowVm()
        {
            data = new object();
            MouseState.MouseStateChanged += HandleMouseStateChanged;
        }

        private void HandleMouseStateChanged()
        {
            AutoUpdateAll(typeof(MainWindowVm), GROUP_MOUSE);
        }



        #region Calls
        #region TileSelect
        [AutoUpdate(GROUP_MOUSE)]
        public bool IsBoxTileSelecting
        {
            get { return MouseState.State == PanelMouseState.TileBoxSelecting; }
            set
            {
                MouseState.SetState(PanelMouseState.TileBoxSelecting);
                SetProperty();
            }
        }
        [AutoUpdate(GROUP_MOUSE)]
        public bool IsBucketTileSelecting
        {
            get { return MouseState.State == PanelMouseState.TileBucketSelecting; }
            set
            {
                MouseState.SetState(PanelMouseState.TileBucketSelecting);
                SetProperty();
            }
        }
        [AutoUpdate(GROUP_MOUSE)]
        public bool IsLineTileSelecting
        {
            get { return MouseState.State == PanelMouseState.TileLineSelecting; }
            set
            {
                MouseState.SetState(PanelMouseState.TileLineSelecting);
                SetProperty();
            }
        }
        public bool IsTileSetFilter
        {
            get { return TileSelector.IsTileSetFilter; }
            set
            {
                TileSelector.BucketTilesetFilter(value);
                SetProperty();
            }
        }
        public bool IsHeightFilter
        {
            get { return TileSelector.IsHeightFilter; }
            set
            {
                TileSelector.BucketHeightFilter(value);
                SetProperty();
            }
        }
        #endregion
        #region TileBrush
        [AutoUpdate(GROUP_MOUSE)]
        public bool IsBrushSingle
        {
            get { return MouseState.State == PanelMouseState.TileSingleBrush; }
            set
            {
                MouseState.SetState(PanelMouseState.TileSingleBrush);
                SetProperty();
            }
        }
        [AutoUpdate(GROUP_MOUSE)]
        public bool IsBrushFill
        {
            get { return MouseState.State == PanelMouseState.TileBucketFill; }
            set
            {
                MouseState.SetState(PanelMouseState.TileBucketFill);
            }
        }
        #endregion
        #region Object Select
        public bool IsIsoSelect
        {
            get { return Selector.IsIsometric; }
            set
            {
                Selector.SetIsometricSelecting(value);
                TileSelector.SetIsometricSelecting(value);
                SetProperty();
            }
        }
        #endregion
        #endregion
    }
}
