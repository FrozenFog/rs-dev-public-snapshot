using RelertSharp.Common;
using RelertSharp.Engine.Api;
using RelertSharp.MapStructure;
using RelertSharp.Wpf.MapEngine.Helper;
using RelertSharp.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.ViewModel
{
    internal class MainWindowVm : BaseVm<MainWindowVm.MainWindowModel>
    {
        private const string GROUP_MOUSE = "mouse";
        private const string GROUP_UNDO = "undoredo";
        private const string GROUP_NAVIGATE = "navi";
        private const string GROUP_COPYPASTE = "cv";
        private readonly TilePanelView tiles;
        public MainWindowVm()
        {
            data = new MainWindowModel();
        }
        public MainWindowVm(TilePanelView tile)
        {
            data = new MainWindowModel();
            tiles = tile;
            MouseState.MouseStateChanged += HandleMouseStateChanged;
            UndoRedoHub.CommandPushed += HandleUndoRedoCommand;
            UndoRedoHub.CommandExecuted += HandleUndoRedoCommand;
            NavigationHub.NavigatePushed += HandleNavigateCommand;
            Selector.SelectionChanged += HandleSelectionChanged;
            TileSelector.SelectedTileChanged += HandleSelectionChanged;
            MapClipboard.ObjectAdded += HandleClipboardChanged;
            MapClipboard.TileAdded += HandleClipboardChanged;
            EngineApi.MapDrawingComplete += HandleMapDraw;
            GlobalVar.MapLoadComplete += HandleMapLoadComplete;
        }

        private void HandleMapLoadComplete()
        {
            SetProperty(nameof(IsMapLoaded));
        }

        private void HandleMapDraw()
        {
            SetProperty(nameof(IsEngineDrawed));
        }

        private void HandleClipboardChanged()
        {
            SetProperty(nameof(IsClipboardHasAnything));
        }

        private void HandleSelectionChanged()
        {
            AutoUpdateAll(GROUP_COPYPASTE);
        }

        private void HandleNavigateCommand()
        {
            AutoUpdateAll(GROUP_NAVIGATE);
        }

        private void HandleUndoRedoCommand()
        {
            AutoUpdateAll(GROUP_UNDO);
        }
        private void HandleMouseStateChanged()
        {
            AutoUpdateAll(GROUP_MOUSE);
        }



        #region Calls
        #region Tiles
        #region TileSelect
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
            get { return MouseState.State == PanelMouseState.TileBucketFlood; }
            set
            {
                MouseState.SetState(PanelMouseState.TileBucketFlood);
            }
        }
        #endregion
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
        public bool IsFrameworkEnable
        {
            get { return data.FrameworkEnable; }
            set
            {
                data.FrameworkEnable = value;
                EngineApi.InvokeLock();
                MapApi.SetFramework(data.FrameworkEnable);
                TilePaintBrush.SwitchToFramework(data.FrameworkEnable);
                tiles.SetFramework(data.FrameworkEnable);
                EngineApi.InvokeUnlock();
                EngineApi.InvokeRedraw();
                SetProperty();
            }
        }
        public bool IsFlatEnable
        {
            get { return data.FlatEnable; }
            set
            {
                data.FlatEnable = value;
                EngineApi.InvokeLock();
                MapApi.SetFlatGround(data.FlatEnable);
                TilePaintBrush.SwitchToFlatGround(data.FlatEnable);
                EngineApi.InvokeUnlock();
                EngineApi.InvokeRedraw();
            }
        }
        #endregion
        #region ToolBar
        #region UndoRedo
        [AutoUpdate(GROUP_UNDO)]
        public bool IsUndoEnable
        {
            get { return UndoRedoHub.UndoCount > 0; }
        }
        [AutoUpdate(GROUP_UNDO)]
        public bool IsRedoEnable
        {
            get { return UndoRedoHub.RedoCount > 0; }
        }
        #endregion
        #region Trace
        [AutoUpdate(GROUP_NAVIGATE)]
        public bool IsNavigateForwardEnable
        {
            get { return NavigationHub.BackTraceCount > 0; }
        }
        [AutoUpdate(GROUP_NAVIGATE)]
        public bool IsNavigateBackwardEnable
        {
            get { return NavigationHub.NavitationCount > 0; }
        }
        #endregion
        #endregion
        #region Universal
        [AutoUpdate(GROUP_COPYPASTE)]
        public bool IsSelectorHasObject
        {
            get { return Selector.SelectedObjects.Count() > 0; }
        }
        [AutoUpdate(GROUP_COPYPASTE)]
        public bool IsSelectorHasTile
        {
            get { return TileSelector.SelectedTile.Count() > 0; }
        }
        [AutoUpdate(GROUP_COPYPASTE)]
        public bool IsSelectorHasAnything
        {
            get { return IsSelectorHasObject || IsSelectorHasTile; }
        }
        [AutoUpdate(GROUP_COPYPASTE)]
        public bool IsClipboardHasAnything
        {
            get { return MapClipboard.ObjectsCount > 0 || MapClipboard.TilesCount > 0; }
        }
        public bool IsMapLoaded
        {
            get { return GlobalVar.HasMap; }
        }
        public bool IsEngineDrawed
        {
            get { return EngineApi.MapDrawed; }
        }
        #endregion
        #region Mouse State
        [AutoUpdate(GROUP_MOUSE)]
        public bool IsMouseNone
        {
            get { return MouseState.State == PanelMouseState.None; }
            set
            {
                MouseState.SetState(PanelMouseState.None);
                SetProperty();
            }
        }
        [AutoUpdate(GROUP_MOUSE)]
        public bool IsMouseInteliRamp
        {
            get { return MouseState.State == PanelMouseState.InteliRampBrush; }
            set
            {
                MouseState.SetState(PanelMouseState.InteliRampBrush);
                SetProperty();
            }
        }
        [AutoUpdate(GROUP_MOUSE)]
        public bool IsMouseRiseSingle
        {
            get { return MouseState.State == PanelMouseState.TileSingleRising; }
            set
            {
                MouseState.SetState(PanelMouseState.TileSingleRising);
                SetProperty();
            }
        }
        [AutoUpdate(GROUP_MOUSE)]
        public bool IsMouseSinkSingle
        {
            get { return MouseState.State == PanelMouseState.TileSingleSinking; }
            set
            {
                MouseState.SetState(PanelMouseState.TileSingleSinking);
                SetProperty();
            }
        }
        [AutoUpdate(GROUP_MOUSE)]
        public bool IsMouseFlatRamp
        {
            get { return MouseState.State == PanelMouseState.TileFlatting; }
            set
            {
                MouseState.SetState(PanelMouseState.TileFlatting);
                SetProperty();
            }
        }
        [AutoUpdate(GROUP_MOUSE)]
        public bool IsTileBrushMode
        {
            get { return MouseState.IsState(PanelMouseState.TileBrush); }
        }
        [AutoUpdate(GROUP_MOUSE)]
        public bool IsSingleTileSelecting
        {
            get { return MouseState.State == PanelMouseState.TileSingleSelecting; }
            set
            {
                MouseState.SetState(PanelMouseState.TileSingleSelecting);
                SetProperty();
            }
        }
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
        #endregion
        #endregion


        #region Model
        public class MainWindowModel
        {
            public bool FrameworkEnable { get; set; }
            public bool FlatEnable { get; set; }
        }
        #endregion
    }
}
