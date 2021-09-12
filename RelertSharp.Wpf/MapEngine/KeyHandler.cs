using RelertSharp.Wpf.Common;
using RelertSharp.Wpf.MapEngine.Helper;
using RelertSharp.Engine.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RelertSharp.Wpf.MapEngine
{
    public partial class MainPanel
    {
        private DelayedAction keyClickAction;
        private bool isActivate;
        #region Handler Firstpass
        private bool isLoaded = false;
        private void PanelLoaded(object sender, RoutedEventArgs e)
        {
            if (!isLoaded)
            {
                var parent = Window.GetWindow(this);
                parent.PreviewKeyDown += HandleKeyDown;
                parent.PreviewKeyUp += HandleKeyUp;
                isLoaded = true;
            }
        }
        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            if (drew && isActivate)
            {
                if (e.Key == Key.Z && GuiUtil.IsControlDown())
                {
                    EngineApi.InvokeLock();
                    if (UndoRedoHub.Undo()) EngineApi.InvokeRedraw();
                    EngineApi.InvokeUnlock();
                }
                else if (e.Key == Key.Y && GuiUtil.IsControlDown())
                {
                    EngineApi.InvokeLock();
                    if (UndoRedoHub.Redo()) EngineApi.InvokeRedraw();
                    EngineApi.InvokeUnlock();
                }
                else if (e.Key == Key.D && GuiUtil.IsControlDown())
                {
                    EngineApi.InvokeLock();
                    TileSelector.UnselectAll();
                    EngineApi.InvokeRedraw();
                    EngineApi.InvokeUnlock();
                }
                else if (MouseState.IsState(PanelMouseState.TileSelecting))
                {
                    bool affected = false;
                    if (e.Key == Key.PageUp)
                    {
                        TileSelector.RiseAllSelectedTile();
                        affected = true;
                    }
                    else if (e.Key == Key.PageDown)
                    {
                        TileSelector.SinkAllSelectedTile();
                        affected = true;
                    }
                    else if (e.Key == Key.C && GuiUtil.IsControlDown())
                    {
                        MapClipboard.AddTileToClipboard();
                    }
                    else if (e.Key == Key.V && GuiUtil.IsControlDown())
                    {
                        MapClipboard.LoadToTileBrush();
                        MouseState.SetState(PanelMouseState.TileSingleBrush);
                        affected = true;
                    }
                    if (affected)
                    {
                        e.Handled = true;
                        EngineApi.InvokeRedraw();
                    }
                }
                else
                {
                    switch (MouseState.State)
                    {
                        case PanelMouseState.TileSingleBrush:
                            TileBrushKeyHandler(e);
                            EngineApi.InvokeRedraw();
                            e.Handled = true;
                            break;
                        case PanelMouseState.InteliRampBrush:
                            InteliRampBrushHandler(e);
                            EngineApi.InvokeRedraw();
                            e.Handled = true;
                            break;
                        case PanelMouseState.None:
                            SelectorKeyHandler(e);
                            EngineApi.InvokeRedraw();
                            e.Handled = true;
                            break;
                    }
                }
            }
        }

        private void HandleKeyUp(object sender, KeyEventArgs e)
        {
            if (drew && isActivate)
            {

            }
        }

        private void KeyHoldToClick()
        {

        }
        #endregion


        #region State
        private void InteliRampBrushHandler(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.PageDown:
                    TileSelector.SinkTile(prevCell);
                    break;
                case Key.PageUp:
                    TileSelector.RiseTile(prevCell);
                    break;
            }
        }
        private void TileBrushKeyHandler(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    TilePaintBrush.MoveOffsetYAxis(-1);
                    break;
                case Key.Down:
                    TilePaintBrush.MoveOffsetYAxis(1);
                    break;
                case Key.Left:
                    TilePaintBrush.MoveOffsetXAxis(-1);
                    break;
                case Key.Right:
                    TilePaintBrush.MoveOffsetXAxis(1);
                    break;
                case Key.PageUp:
                    TilePaintBrush.MoveOffsetHeight(1);
                    break;
                case Key.PageDown:
                    TilePaintBrush.MoveOffsetHeight(-1);
                    break;
                case Key.Home:
                    TilePaintBrush.ResetOffset();
                    break;
            }
        }
        private void SelectorKeyHandler(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Delete:
                    Selector.DeleteSelectedObjects();
                    EngineApi.RedrawMinimapAll();
                    break;
                case Key.C:
                    if (GuiUtil.IsControlDown())
                    {
                        MapClipboard.AddObjectToClipboard();
                    }
                    break;
                case Key.V:
                    if (GuiUtil.IsControlDown())
                    {
                        MapClipboard.LoadToObjectBrush();
                        Selector.UnselectAll();
                        MouseState.SetState(PanelMouseState.ObjectPasteBrush);
                    }
                    break;
            }
        }
        #endregion
    }
}
