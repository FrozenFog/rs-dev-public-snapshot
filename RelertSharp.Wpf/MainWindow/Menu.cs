using RelertSharp.Wpf.Common;
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
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Filter = "Map File |*.map;*.mpr;*.yrm"
            };
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string name = dlg.FileName;
                EngineApi.DisposeMap();
                GlobalVar.LoadMapDocument(name);
            }
        }
        private void MenuSaveMap(object sender, RoutedEventArgs e)
        {
            SaveMap();
        }

        private void MenuSaveMapAs(object sender, RoutedEventArgs e)
        {
            SaveAs();
        }

        private void MenuNewStandardMap(object sender, RoutedEventArgs e)
        {
            bool cancel = false;
            if (GlobalVar.HasMap)
            {
                MapSaveFailsafe(out bool c);
                cancel = c;
            }
            if (!cancel)
            {
                EngineApi.DisposeMap();
                DlgCreateMap dlg = new DlgCreateMap();
                if (dlg.ShowDialog().Value)
                {
                    GlobalVar.CreateNewMap(dlg.Config);
                }
                else
                {
                    GlobalVar.DisposeMapDocument();
                }
            }
        }

        private void MenuExit(object sender, RoutedEventArgs e)
        {

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

        private void MenuInspectRules(object sender, RoutedEventArgs e)
        {
            DlgNameInput dlg = new DlgNameInput("Ini section name");
            if (dlg.ShowDialog().Value)
            {
                DlgIniInspector ins = new DlgIniInspector();
                ins.SetContent(dlg.ResultName);
                ins.ShowDialog();
            }
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
        private void MenuCliffSelected(object sender, RoutedEventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            RelertSharp.Common.Config.Model.TheaterCliffSet cliff = menu.DataContext as RelertSharp.Common.Config.Model.TheaterCliffSet;
            InteliBrush.CliffAlignType = cliff.Key;
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
        #region Brush
        private void MenuRandomBrushDlg(object sender, RoutedEventArgs e)
        {
            DlgRandomizeBrush dlg = new DlgRandomizeBrush();
            dlg.LoadBrush();
            dlg.ShowDialog();
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
        private void MenuEraseEverything(object sender, RoutedEventArgs e)
        {
            if (GuiUtil.YesNoWarning("Everything in map will be removed, include tiles.\nThis cannot be undone, are you sure?"))
            {
                DlgDangerousCommit dlg = new DlgDangerousCommit();
                List<string> contents = new List<string>();
                if (!GlobalVar.GlobalMap.Info.Author.IsNullOrEmpty()) contents.Add(string.Format("Map created by {0}", GlobalVar.GlobalMap.Info.Author));
                contents.Add(GlobalVar.GlobalMap.Info.MapName);
                dlg.SetContents(contents);
                if (dlg.ShowDialog().Value)
                {
                    EngineApi.InvokeLock();
                    EngineApi.DisposeAllObjects();
                    GlobalVar.GlobalMap.VoidOut();
                    GlobalVar.ForceReload();
                    EngineApi.InvokeRedraw();
                    EngineApi.InvokeUnlock();
                }
            }
        }
        private void MenuCallSetting(object sender, RoutedEventArgs e)
        {
            DlgSetting dlg = new DlgSetting();
            if (dlg.ShowDialog().Value)
            {
                MainWindowUtil.UpdateAutosaveTime();
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
                try
                {
                    dockMain.LoadLayoutFromXml(dlg.FileName, this);
                }
                catch (Exception ex)
                {
                    GuiUtil.Fatal("Invalid layout file, or version is not acceptable", ex);
                }
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
                try
                {
                    dockMain.SaveLayoutToXml(dlg.FileName);
                    GuiUtil.Asterisk("Save complete!");
                }
                catch (Exception ex)
                {
                    GuiUtil.Fatal("Cannot save current layout.", ex);
                }
            }
        }

        private void MenuDefaultLayout(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
