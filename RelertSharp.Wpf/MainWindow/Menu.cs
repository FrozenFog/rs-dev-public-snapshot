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
                pnlMain.SaveMapScreenshotAs(dlg.FileName);
                TilePaintBrush.ResumeBrush();
                PaintBrush.ResumeBrush();
                EngineApi.ResumeTileIndicator();
            }
        }
        #endregion


        #region Views
        private void ShowView(IRsView src)
        {
            if (src.ParentAncorable != null)
            {
                foreach (LayoutAnchorable anc in dockMain.Layout.Hidden)
                {
                    if (anc.ContentId == src.ViewType.ToString())
                    {
                        dockMain.Layout.Hidden.Remove(anc);
                        LoadTargetTool(src.ViewType);
                        break;
                    }
                }
            }
            if (src.ParentDocument != null && !dockMain.HasDocumentWithContentId(src.ViewType.ToString()))
            {
                dockMain.AddCenterPage(RsViewComponentAttribute.GetViewTitle(src), src);
            }
        }
        #region Logics
        private void MenuShowEvent(object sender, RoutedEventArgs e)
        {
            ShowView(events);
        }

        private void MenuShowAction(object sender, RoutedEventArgs e)
        {
            ShowView(actions);
        }

        private void MenuShowTrigger(object sender, RoutedEventArgs e)
        {
            ShowView(trigger);
        }

        private void MenupenTriggerList(object sender, RoutedEventArgs e)
        {
            ShowView(triggerList);
        }

        private void MenuShowTeam(object sender, RoutedEventArgs e)
        {
            ShowView(team);
        }

        private void MenuShowTeamList(object sender, RoutedEventArgs e)
        {
            ShowView(teamList);
        }

        private void MenuShowTaskforce(object sender, RoutedEventArgs e)
        {
            ShowView(taskforce);
        }

        private void MenuShowTaskforceList(object sender, RoutedEventArgs e)
        {
            ShowView(taskforceList);
        }

        private void MenuShowScript(object sender, RoutedEventArgs e)
        {
            ShowView(script);
        }

        private void MenuShowScriptList(object sender, RoutedEventArgs e)
        {
            ShowView(scriptList);
        }

        private void MenuShowHouses(object sender, RoutedEventArgs e)
        {
            ShowView(housePanel);
        }

        private void MenuShowAiTrigger(object sender, RoutedEventArgs e)
        {
            ShowView(aiTrigger);
        }

        private void MenuShowAiTriggerList(object sender, RoutedEventArgs e)
        {
            ShowView(aiTriggerList);
        }

        private void MenuShowLocalVar(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Map and Objects
        private void MenuShowMapPanel(object sender, RoutedEventArgs e)
        {
            ShowView(pnlMain);
        }

        private void MenuShowLightning(object sender, RoutedEventArgs e)
        {
            ShowView(lightning);
        }

        private void MenuShowLightSource(object sender, RoutedEventArgs e)
        {

        }
        private void MenuShowObjectBrush(object sender, RoutedEventArgs e)
        {
            ShowView(objectBrush);
        }

        private void MenuShowTileBrush(object sender, RoutedEventArgs e)
        {

        }

        private void MenuShowMinimap(object sender, RoutedEventArgs e)
        {
            ShowView(minimap);
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
            ShowView(animationPreview);
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
