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
using System.Reflection;
using System.ComponentModel;

namespace RelertSharp.Wpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Timer
        #endregion
        #region Components
        [RsViewComponent(GuiViewType.AiTrigger, GuiViewSide.Right, "Ai Trigger Info")]
        private readonly AiTriggerView aiTrigger = new AiTriggerView();
        [RsViewComponent(GuiViewType.TeamList, GuiViewSide.Left, "Team List")]
        private readonly TeamListView teamList = new TeamListView();
        [RsViewComponent(GuiViewType.AiTriggerList, GuiViewSide.Left, "Ai Trigger List")]
        private readonly AiTriggerListView aiTriggerList = new AiTriggerListView();
        [RsViewComponent(GuiViewType.ScriptList, GuiViewSide.Left, "Script List")]
        private readonly ScriptListView scriptList = new ScriptListView();
        [RsViewComponent(GuiViewType.TaskforceList, GuiViewSide.Left, "Taskforce List")]
        private readonly TaskforceListView taskforceList = new TaskforceListView();
        [RsViewComponent(GuiViewType.Taskforce, GuiViewSide.Right, "Taskforce Info")]
        private readonly TaskforceView taskforce = new TaskforceView();
        [RsViewComponent(GuiViewType.Team, GuiViewSide.Right, "Team Info")]
        private readonly TeamView team = new TeamView();
        [RsViewComponent(GuiViewType.Script, GuiViewSide.Right, "Script Info")]
        private readonly ScriptView script = new ScriptView();
        [RsViewComponent(GuiViewType.MainPanel, GuiViewSide.Center, "Map")]
        private readonly MainPanel pnlMain = new MainPanel();
        [RsViewComponent(GuiViewType.Minimap, GuiViewSide.Top, "Minimap")]
        private readonly MinimapPanel minimap = new MinimapPanel();
        [RsViewComponent(GuiViewType.LightningPanel, GuiViewSide.Right, "Lightning")]
        private readonly LightningView lightning = new LightningView();
        [RsViewComponent(GuiViewType.AnimationPreview, GuiViewSide.Right, "Animation Preview")]
        private readonly AnimationPreview animationPreview = new AnimationPreview();
        [RsViewComponent(GuiViewType.TriggerList, GuiViewSide.Left, "Trigger List")]
        private readonly TriggerListView triggerList = new TriggerListView();
        [RsViewComponent(GuiViewType.Trigger, GuiViewSide.Top, "Trigger Info")]
        private readonly TriggerView trigger = new TriggerView();
        [RsViewComponent(GuiViewType.Event, GuiViewSide.Bottom, "Events")]
        private readonly TriggerLogicView events = new TriggerLogicView(true);
        [RsViewComponent(GuiViewType.Action, GuiViewSide.Bottom, "Actions")]
        private readonly TriggerLogicView actions = new TriggerLogicView(false);
        [RsViewComponent(GuiViewType.ObjctPanel, GuiViewSide.Left, "Object Brush")]
        private readonly MapObjectBrushView objectBrush = new MapObjectBrushView();
        [RsViewComponent(GuiViewType.HousePanel, GuiViewSide.Bottom, "House Info")]
        private readonly CountryHouseView housePanel = new CountryHouseView();
        [RsViewComponent(GuiViewType.TilePanel, GuiViewSide.Bottom, "Tiles")]
        private readonly TilePanelView tiles = new TilePanelView();
        #endregion
        #region Else
        private SoundManager soundManager = new SoundManager();
        #endregion
        #region Dispatcher
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            LoadAllTools();
            AddReciveListener();
            BindNavigation();
            InitializeGuiStatus();
        }

        #region Initialization
        private void InitializeGuiStatus()
        {
            if (GlobalVar.GlobalConfig.UserConfig.IsGuiValid())
            {
                var gui = GlobalVar.GlobalConfig.UserConfig.GuiStatus;
                Left = gui.PosX;
                Top = gui.PosY;
                Width = gui.Width;
                Height = gui.Height;
                if (gui.IsMaximized) WindowState = WindowState.Maximized;
            }
        }
        private void LoadAllTools()
        {
            FieldInfo[] fields = typeof(MainWindow).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo info in fields)
            {
                RsViewComponentAttribute attr = info.GetCustomAttribute<RsViewComponentAttribute>();
                if (attr != null)
                {
                    IRsView control = info.GetValue(this) as IRsView;
                    switch (attr.Side)
                    {
                        case GuiViewSide.Top:
                            dockMain.Layout.AddToolToTop(attr.Title, control);
                            break;
                        case GuiViewSide.Bottom:
                            dockMain.Layout.AddToolToBottom(attr.Title, control);
                            break;
                        case GuiViewSide.Left:
                            dockMain.Layout.AddToolToLeft(attr.Title, control);
                            break;
                        case GuiViewSide.Right:
                            dockMain.Layout.AddToolToRight(attr.Title, control);
                            break;
                        case GuiViewSide.Center:
                            dockMain.AddCenterPage(attr.Title, control);
                            break;
                    }
                }
            }
        }
        private void LoadTargetTool(GuiViewType type)
        {
            FieldInfo[] fields = typeof(MainWindow).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo info in fields)
            {
                RsViewComponentAttribute attr = info.GetCustomAttribute<RsViewComponentAttribute>();
                if (attr != null && attr.ViewType == type)
                {
                    IRsView control = info.GetValue(this) as IRsView;
                    switch (attr.Side)
                    {
                        case GuiViewSide.Top:
                            dockMain.Layout.AddToolToTop(attr.Title, control);
                            break;
                        case GuiViewSide.Bottom:
                            dockMain.Layout.AddToolToBottom(attr.Title, control);
                            break;
                        case GuiViewSide.Left:
                            dockMain.Layout.AddToolToLeft(attr.Title, control);
                            break;
                        case GuiViewSide.Right:
                            dockMain.Layout.AddToolToRight(attr.Title, control);
                            break;
                        case GuiViewSide.Center:
                            dockMain.AddCenterPage(attr.Title, control);
                            break;
                    }
                    return;
                }
            }
        }
        #endregion

        #region Reciver Logics
        private void BindNavigation()
        {
            NavigationHub.PlaySoundRequest += NavigateSound;
        }

        private async void NavigateSound(string regname, SoundType type)
        {
            if (soundManager.IsPlaying)
            {
                soundManager.Stop();
            }
            string name = soundManager.GetSoundName(regname, type);
            await Task.Run(() =>
            {
                soundManager.LoadWav(GlobalVar.GlobalSoundBank.GetSound(name));
            });
            soundManager.Play();
        }

        private void AddReciveListener()
        {
            BindListener(aiTrigger, aiTriggerList);
            BindListener(script, scriptList);
            BindListener(team, teamList);
            BindListener(trigger, triggerList);
            BindListener(events, triggerList);
            BindListener(actions, triggerList);
            BindListener(taskforce, taskforceList);
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

        private void RedrawRequestHandler(object sender, EventArgs e)
        {
            pnlMain.HandleRedrawRequest();
        }

        private void PnlMain_ScaleFactorChanged(object sender, EventArgs e)
        {
            RefreshStatus();
        }

        private I3dLocateable posMouse;
        private int subcell;
        private void PnlMain_MousePosChanged(I3dLocateable pos, int subcell)
        {
            posMouse = pos;
            this.subcell = subcell;
            RefreshStatus();
        }
        private void RefreshStatus()
        {
            position.Text = string.Format("X: {0} Y: {1} Z: {2} Subcell: {4}, Scale: {3}", posMouse.X, posMouse.Y, posMouse.Z, Engine.Api.EngineApi.ScaleFactor, subcell);
        }


        #region Handler
        #region Loaded & Closed
        private void WindowLoadedInitializer(object sender, RoutedEventArgs e)
        {
            GuiUtil.MonitorScale = this.GetScale();

            Engine.Api.EngineApi.SetBackgroundColor(GuiUtil.defBackColor);
            //pnlMain.DrawMap();
            //minimap.ResumeDrawing();
            RedrawListener();
            OtherListener();

            ObjectBrushConfig cfg = new ObjectBrushConfig();
            ObjectBrushFilter filter = new ObjectBrushFilter();
            PaintBrush.SetConfig(cfg, filter);
            objectBrush.BindBrushConfig(cfg, filter);
        }
        private void MainWindowClosed(object sender, EventArgs e)
        {
            /// finalize view
            FieldInfo[] fields = typeof(MainWindow).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo info in fields)
            {
                RsViewComponentAttribute attr = info.GetCustomAttribute<RsViewComponentAttribute>();
                if (attr != null && info.GetValue(this) is IFinalizeableView view)
                {
                    view.DoFinalization();
                }
            }

            /// save Gui
            GlobalVar.GlobalConfig.UserConfig.SetGuiStatus(Left, Top, Width, Height, WindowState == WindowState.Maximized);
        }
        #endregion

        #endregion
    }
}
