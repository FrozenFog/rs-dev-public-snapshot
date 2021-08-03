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
            MouseState.SetState(PanelMouseState.TileLineSelecting);
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
        private void MenuIsoSelect(object sender, RoutedEventArgs e)
        {
            bool enable = (sender as MenuItem).IsChecked;
            Selector.SetIsometricSelecting(enable);
        }

        private void MenuSwitchFramework(object sender, RoutedEventArgs e)
        {
            EngineApi.InvokeLock();
            bool enable = (sender as MenuItem).IsChecked;
            MapApi.SetFramework(enable);
            TilePaintBrush.SwitchToFramework(enable);
            tiles.SetFramework(enable);
            EngineApi.InvokeUnlock();
            EngineApi.InvokeRedraw();
        }

        private void MenuSwitchFlat(object sender, RoutedEventArgs e)
        {
            EngineApi.InvokeLock();
            bool enable = (sender as MenuItem).IsChecked;
            MapApi.SetFlatGround(enable);
            TilePaintBrush.SwitchToFlatGround(enable);
            EngineApi.InvokeUnlock();
            EngineApi.InvokeRedraw();
        }
        #region Tile Selecting
        private void MenuTileSelSingle(object sender, RoutedEventArgs e)
        {
            MouseState.SetState(PanelMouseState.TileSingleSelecting);
        }

        private void MenuTileSelBucket(object sender, RoutedEventArgs e)
        {
            MouseState.SetState(PanelMouseState.TileBucketSelecting);
        }

        private void MenuTileSelLine(object sender, RoutedEventArgs e)
        {
            MouseState.SetState(PanelMouseState.TileLineSelecting);
        }
        private void MenuTileSelFiltSet(object sender, RoutedEventArgs e)
        {
            TileSelector.BucketTilesetFilter((sender as MenuItem).IsChecked);
        }

        private void MenuTileSelFiltHeight(object sender, RoutedEventArgs e)
        {
            TileSelector.BucketHeightFilter((sender as MenuItem).IsChecked);
        }
        #endregion
        #region Tile Editing
        private void MenuTileBrsSingle(object sender, RoutedEventArgs e)
        {
            MouseState.SetState(PanelMouseState.TileSingleBrush);
        }

        private void MenuTileBrsBucket(object sender, RoutedEventArgs e)
        {
            MouseState.SetState(PanelMouseState.TileBucketFill);
        }
        #endregion
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


        #region Views
        #region Logics
        private void MenuShowEvent(object sender, RoutedEventArgs e)
        {
            LayoutManagerHub.ShowView(events);
        }

        private void MenuShowAction(object sender, RoutedEventArgs e)
        {
            LayoutManagerHub.ShowView(actions);
        }

        private void MenuShowTrigger(object sender, RoutedEventArgs e)
        {
            LayoutManagerHub.ShowView(trigger);
        }

        private void MenupenTriggerList(object sender, RoutedEventArgs e)
        {
            LayoutManagerHub.ShowView(triggerList);
        }

        private void MenuShowTeam(object sender, RoutedEventArgs e)
        {
            LayoutManagerHub.ShowView(team);
        }

        private void MenuShowTeamList(object sender, RoutedEventArgs e)
        {
            LayoutManagerHub.ShowView(teamList);
        }

        private void MenuShowTaskforce(object sender, RoutedEventArgs e)
        {
            LayoutManagerHub.ShowView(taskforce);
        }

        private void MenuShowTaskforceList(object sender, RoutedEventArgs e)
        {
            LayoutManagerHub.ShowView(taskforceList);
        }

        private void MenuShowScript(object sender, RoutedEventArgs e)
        {
            LayoutManagerHub.ShowView(script);
        }

        private void MenuShowScriptList(object sender, RoutedEventArgs e)
        {
            LayoutManagerHub.ShowView(scriptList);
        }

        private void MenuShowHouses(object sender, RoutedEventArgs e)
        {
            LayoutManagerHub.ShowView(housePanel);
        }

        private void MenuShowAiTrigger(object sender, RoutedEventArgs e)
        {
            LayoutManagerHub.ShowView(aiTrigger);
        }

        private void MenuShowAiTriggerList(object sender, RoutedEventArgs e)
        {
            LayoutManagerHub.ShowView(aiTriggerList);
        }

        private void MenuShowLocalVar(object sender, RoutedEventArgs e)
        {
            LayoutManagerHub.ShowView(localVar);
        }
        #endregion

        #region Map and Objects
        private void MenuShowMapPanel(object sender, RoutedEventArgs e)
        {
            LayoutManagerHub.ShowView(pnlMain);
        }

        private void MenuShowLightning(object sender, RoutedEventArgs e)
        {
            LayoutManagerHub.ShowView(lightning);
        }

        private void MenuShowLightSource(object sender, RoutedEventArgs e)
        {

        }
        private void MenuShowObjectBrush(object sender, RoutedEventArgs e)
        {
            LayoutManagerHub.ShowView(objectBrush);
        }

        private void MenuShowTileBrush(object sender, RoutedEventArgs e)
        {

        }

        private void MenuShowMinimap(object sender, RoutedEventArgs e)
        {
            LayoutManagerHub.ShowView(minimap);
        }

        private void MenuShowFmtBrush(object sender, RoutedEventArgs e)
        {

        }

        private void MenuShowLayerControl(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Tools
        private void MenuShowAnimPreview(object sender, RoutedEventArgs e)
        {
            LayoutManagerHub.ShowView(animationPreview);
        }

        private void MenuShowStatistics(object sender, RoutedEventArgs e)
        {

        }
        #endregion
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
