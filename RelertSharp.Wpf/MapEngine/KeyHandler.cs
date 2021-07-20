﻿using RelertSharp.Wpf.Common;
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
            if (drew && ParentDocument.IsActive)
            {
                if (e.Key == Key.Z && GuiUtil.IsKeyDown(Key.LeftCtrl))
                {
                    EngineApi.InvokeLock();
                    if (UndoRedoHub.Undo()) EngineApi.InvokeRedraw();
                    EngineApi.InvokeUnlock();
                }
                else if (e.Key == Key.Y && GuiUtil.IsKeyDown(Key.LeftCtrl))
                {
                    EngineApi.InvokeLock();
                    if (UndoRedoHub.Redo()) EngineApi.InvokeRedraw();
                    EngineApi.InvokeUnlock();
                }
                else
                {
                    switch (MouseState.State)
                    {
                        case PanelMouseState.TileBrush:
                            TileBrushKeyHandler(e);
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
            if (drew && ParentDocument.IsActive)
            {

            }
        }

        private void KeyHoldToClick()
        {

        }
        #endregion


        #region State
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
                    break;
            }
        }
        #endregion
    }
}
