﻿using RelertSharp.Wpf.Common;
using RelertSharp.Common;
using RelertSharp.MapStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AvalonDock.Layout;
using AvalonDock.Layout.Serialization;
using System.Xml;
using System.IO;
using RelertSharp.Wpf.Views;
using RelertSharp.Wpf.MapEngine.Helper;
using RelertSharp.Engine.Api;
using RelertSharp.Wpf.Dialogs;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;
using System.Windows.Controls;

namespace RelertSharp.Wpf
{
    public partial class MainWindow : Window
    {
        #region File
        private void MenuOpenMap(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string name = dlg.FileName;
                Engine.Api.EngineApi.DisposeMap();
                GlobalVar.LoadMapDocument(name);
            }
        }
        #endregion
        #region Debug
        private const string mapname = "arele.map";

        private void DebugClick(object sender, RoutedEventArgs e)
        {
            GlobalVar.LoadMapDocument(mapname);
            //dockMain.LoadLayoutFromXml("layout.xml", this);
        }

        private void DebugClick2(object sender, RoutedEventArgs e)
        {

        }
        #endregion


        #region Edits
        #region Tiles
        private void MenuSetHeight(object sender, RoutedEventArgs e)
        {
            DlgNameInput dlg = new DlgNameInput(string.Format("Input height, range between 0 and {0}", Constant.DrawingEngine.MapMaxHeight));
            if (dlg.ShowDialog().Value)
            {
                string value = dlg.ResultName;
                if (int.TryParse(value, out int height) && RsMath.InRange(height, 0, Constant.DrawingEngine.MapMaxHeight))
                {
                    TileSelector.AllSetHeightTo(height);
                    EngineApi.InvokeRedraw();
                }
                else GuiUtil.Warning("Invalid height!");
            }
        }
        #endregion
        #region Universal
        private void MenuUndo(object sender, RoutedEventArgs e)
        {
            EngineApi.InvokeLock();
            if (UndoRedoHub.Undo()) EngineApi.InvokeRedraw();
            EngineApi.InvokeUnlock();
        }

        private void MenuRedo(object sender, RoutedEventArgs e)
        {
            EngineApi.InvokeLock();
            if (UndoRedoHub.Redo()) EngineApi.InvokeRedraw();
            EngineApi.InvokeUnlock();
        }

        private void MenuCopy(object sender, RoutedEventArgs e)
        {
            if (MouseState.IsState(PanelMouseState.TileSelecting)) MapClipboard.AddTileToClipboard();
            else if (MouseState.State == PanelMouseState.None) MapClipboard.AddObjectToClipboard();
        }

        private void MenuPaste(object sender, RoutedEventArgs e)
        {
            EngineApi.InvokeLock();
            if (MapClipboard.ObjectsCount > 0)
            {
                MapClipboard.LoadToObjectBrush();
                Selector.UnselectAll();
                MouseState.SetState(PanelMouseState.ObjectPasteBrush);
            }
            else if (MapClipboard.TilesCount > 0)
            {
                MapClipboard.LoadToTileBrush();
                TileSelector.UnselectAll();
                MouseState.SetState(PanelMouseState.TileSingleBrush);
            }
            EngineApi.InvokeUnlock();
        }

        private void MenuCut(object sender, RoutedEventArgs e)
        {
            EngineApi.InvokeLock();
            if (MouseState.IsState(PanelMouseState.TileSelecting))
            {
                MapClipboard.AddTileToClipboard();
            }
            else if (MouseState.State == PanelMouseState.None)
            {
                MapClipboard.AddObjectToClipboard();
                Selector.DeleteSelectedObjects();
                EngineApi.InvokeRedraw();
            }
            EngineApi.InvokeUnlock();
        }
        private void MenuCalcelSel(object sender, RoutedEventArgs e)
        {
            EngineApi.InvokeLock();
            TileSelector.UnselectAll();
            Selector.UnselectAll();
            EngineApi.InvokeRedraw();
            EngineApi.InvokeUnlock();
        }
        #endregion
        private void MenuSaveShot(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog()
            {
                Filter = "Jpeg File | *.jpg",
                FileName = GlobalVar.GlobalMap.Info.MapName,
                AddExtension = true
            };
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TilePaintBrush.SuspendBrush();
                PaintBrush.SuspendBrush();
                EngineApi.SuspendTileIndicator();
                bool success = pnlMain.SaveMapScreenshotAs(dlg.FileName);
                TilePaintBrush.ResumeBrush();
                PaintBrush.ResumeBrush();
                EngineApi.ResumeTileIndicator();
                if (!success)
                {
                    GuiUtil.Warning("Save failed, please try again.");
                }
            }
        }
        #endregion


        #region Layouts
        private void MenuLoadLayout(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Filter = "Xml layout files (*.xml)*.xml|*.xml",
                AddExtension = true,
            };
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dockMain.LoadLayoutFromXml(dlg.FileName, this);
            }
        }

        private void MenuSaveLayout(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog()
            {
                Filter = "Xml layout files (*.xml)|*.xml",
                AddExtension = true
            };
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dockMain.SaveLayoutToXml(dlg.FileName);
            }
        }

        private void MenuDefaultLayout(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
