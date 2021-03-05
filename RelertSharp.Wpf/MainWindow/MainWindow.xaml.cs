using AvalonDock.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using RelertSharp.Wpf.ViewModel;
using RelertSharp.Wpf.Views;
using RelertSharp.Wpf.MapEngine;
using System.Windows.Interop;
using RelertSharp.Common;

namespace RelertSharp.Wpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Timer
        DispatcherTimer tmrInit;
        #endregion
        #region Components
        private readonly AiTriggerView aiTrigger = new AiTriggerView();
        private readonly TeamListView teamList = new TeamListView();
        private readonly AiTriggerListView aiTriggerList = new AiTriggerListView();
        private readonly ScriptListView scriptList = new ScriptListView();
        private readonly TaskforceListView taskforceList = new TaskforceListView();
        private readonly TeamView team = new TeamView();
        private readonly ScriptView script = new ScriptView();
        private readonly MainPanel pnlMain = new MainPanel();
        private readonly MinimapPanel minimap = new MinimapPanel();
        private readonly LightningView lightning = new LightningView();
        //private readonly LightPanel lightPanel = new LightPanel();

        #endregion
        #region Dispatcher
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            AddToolPage();
            //AddReciveListener();
            SetTimer();
        }

        private void SetTimer()
        {
            tmrInit = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 5)
            };
            tmrInit.Tick += DelayedInitialize;
            tmrInit.Start();
        }

        private void DelayedInitialize(object sender, EventArgs e)
        {
            DebugInit();
        }

        private void AddToolPage()
        {
            //dockMain.Layout.AddToolToRight("Ai Trigger Edit", aiTrigger);
            dockMain.Layout.AddToolToRight("Team List", teamList);
            dockMain.Layout.AddToolToRight("Lightning", lightning);
            //dockMain.Layout.AddToolToRight("Ai Trigger List", aiTriggerList);
            //dockMain.AddToolToRight("Script List", scriptList);
            //dockMain.AddToolToRight("Taskforce List", taskforceList);
            //dockMain.Layout.AddToolToRight("Team", team);
            //dockMain.AddToolToRight("Script", script);
            //dockMain.Layout.AddToolToRight("Light", lightPanel);
            dockMain.AddCenterPage("Map", pnlMain);

            dockMain.Layout.RightSide.Root.Manager.Layout.AddToolToTop("Minimap", minimap);
        }

        #region Reciver Logics
        private void AddReciveListener()
        {
            BindListener(aiTrigger, aiTriggerList);
            BindListener(script, scriptList);
            BindListener(team, teamList);
        }
        private void BindListener(IObjectReciver reciver, IListContainer sender)
        {
            sender.ItemSelected += reciver.ReciveObject;
        }
        #endregion


        private void DebugInit()
        {
            GuiUtil.MonitorScale = this.GetScale();
            tmrInit.Stop();

            pnlMain.InitializePanel();
            minimap.Initialize();
            Engine.Api.EngineApi.SetBackgroundColor(GuiUtil.defBackColor);
            pnlMain.DrawMap();
            minimap.ResumeDrawing();
            pnlMain.MousePosChanged += PnlMain_MousePosChanged;
            pnlMain.ScaleFactorChanged += PnlMain_ScaleFactorChanged;
            lightning.LightningChangedRequest += RedrawRequestHandler;
        }

        private void RedrawRequestHandler(object sender, EventArgs e)
        {
            pnlMain.HandleRedrawRequest();
        }

        private void PnlMain_ScaleFactorChanged(object sender, EventArgs e)
        {
            RefreshStatus();
        }

        private I3dLocateable posMouse;
        private void PnlMain_MousePosChanged(object sender, I3dLocateable pos)
        {
            posMouse = pos;
            RefreshStatus();
        }
        private void RefreshStatus()
        {
            position.Text = string.Format("X: {0} Y: {1} Z: {2}, Scale: {3}", posMouse.X, posMouse.Y, posMouse.Z, Engine.Api.EngineApi.ScaleFactor);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DebugInit();
        }
    }
}
