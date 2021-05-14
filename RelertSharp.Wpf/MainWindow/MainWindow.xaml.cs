using AvalonDock.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.IO;
using System.Windows.Threading;
using RelertSharp.Wpf.ViewModel;
using RelertSharp.Wpf.Views;
using RelertSharp.Wpf.Common;
using RelertSharp.Wpf.MapEngine;
using RelertSharp.Common;
using RelertSharp.Wpf.ToolBoxes;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common.Config.Model;

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
        [RsViewComponent(GuiViewType.AiTrigger, nameof(aiTrigger))]
        private readonly AiTriggerView aiTrigger = new AiTriggerView();
        [RsViewComponent(GuiViewType.TeamList, nameof(teamList))]
        private readonly TeamListView teamList = new TeamListView();
        [RsViewComponent(GuiViewType.AiTriggerList, nameof(aiTriggerList))]
        private readonly AiTriggerListView aiTriggerList = new AiTriggerListView();
        [RsViewComponent(GuiViewType.ScriptList, nameof(scriptList))]
        private readonly ScriptListView scriptList = new ScriptListView();
        [RsViewComponent(GuiViewType.TaskforceList, nameof(taskforceList))]
        private readonly TaskforceListView taskforceList = new TaskforceListView();
        [RsViewComponent(GuiViewType.Team, nameof(team))]
        private readonly TeamView team = new TeamView();
        [RsViewComponent(GuiViewType.Script, nameof(script))]
        private readonly ScriptView script = new ScriptView();
        [RsViewComponent(GuiViewType.MainPanel, nameof(pnlMain))]
        private readonly MainPanel pnlMain = new MainPanel();
        [RsViewComponent(GuiViewType.Minimap, nameof(minimap))]
        private readonly MinimapPanel minimap = new MinimapPanel();
        [RsViewComponent(GuiViewType.LightningPanel, nameof(lightning))]
        private readonly LightningView lightning = new LightningView();
        [RsViewComponent(GuiViewType.AnimationPreview, nameof(animationPreview))]
        private readonly AnimationPreview animationPreview = new AnimationPreview();
        [RsViewComponent(GuiViewType.TriggerList, nameof(triggerList))]
        private readonly TriggerListView triggerList = new TriggerListView();
        [RsViewComponent(GuiViewType.Trigger, nameof(trigger))]
        private readonly TriggerView trigger = new TriggerView();
        [RsViewComponent(GuiViewType.Event, nameof(events))]
        private readonly EventView events = new EventView();
        [RsViewComponent(GuiViewType.Action, nameof(actions))]
        private readonly ActionView actions = new ActionView();
        #endregion
        #region Dispatcher
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            AddToolPage();
            AddReciveListener();
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
            //dockMain.Layout.AddToolToRight("Team List", teamList);
            //dockMain.Layout.AddToolToRight("Lightning", lightning);
            //dockMain.Layout.AddToolToRight("Ai Trigger List", aiTriggerList);
            //dockMain.AddToolToRight("Script List", scriptList);
            //dockMain.AddToolToRight("Taskforce List", taskforceList);
            //dockMain.Layout.AddToolToRight("Team", team);
            //dockMain.Layout.AddToolToRight("Animation", animationPreview);
            dockMain.Layout.AddToolToRight("Trigger List", triggerList);
            //dockMain.AddToolToRight("Script", script);
            //dockMain.Layout.AddToolToRight("Light", lightPanel);
            dockMain.AddCenterPage("Map", pnlMain);
            dockMain.Layout.AddToolToRight("Minimap", minimap, 1);
            dockMain.Layout.AddToolToRight("Trigger", trigger);
            dockMain.Layout.AddToolToRight("Events", events);
            dockMain.Layout.AddToolToRight("Actions", actions);
        }

        #region Reciver Logics
        private void AddReciveListener()
        {
            //BindListener(aiTrigger, aiTriggerList);
            //BindListener(script, scriptList);
            //BindListener(team, teamList);
            BindListener(trigger, triggerList);
            BindListener(events, triggerList);
            BindListener(actions, triggerList);
        }
        private void BindListener(IObjectReciver reciver, IListContainer sender)
        {
            sender.ItemSelected += reciver.ReciveObject;
        }
        private void RedrawListener()
        {
            lightning.LightningChangedRequest += RedrawRequestHandler;
        }
        private void OtherListener()
        {
            pnlMain.MousePosChanged += PnlMain_MousePosChanged;
            pnlMain.ScaleFactorChanged += PnlMain_ScaleFactorChanged;
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
            RedrawListener();
            OtherListener();
        }
        private void DebugClick()
        {
            tmrInit.Stop();
            triggerList.ReloadMapTrigger();
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

        private void DebugClick(object sender, RoutedEventArgs e)
        {
            DebugClick();
        }
    }
}
