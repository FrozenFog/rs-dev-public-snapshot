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
using RelertSharp.Engine.Api;

namespace RelertSharp.Wpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Else
        private SoundManager soundManager = new SoundManager();
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            LayoutManagerHub.BindManager(dockMain, this);
            LoadAllTools();
            AddReciveListener();
            BindNavigation();
            InitializeGuiStatus();
            DataContext = new MainWindowVm(tiles);
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
            dockMain.ActiveContent = reciver.ParentAncorable;
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
