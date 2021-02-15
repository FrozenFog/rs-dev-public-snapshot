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
        #endregion
        #region Dispatcher
        private DispatcherProcessingDisabled dispatcher;
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
            pnlMain.Initialize();
            PanelListener();
            pnlMain.DrawMap();

            tmrInit.Stop();
        }

        private void AddToolPage()
        {
            dockMain.AddToolToRight("Ai Trigger Edit", aiTrigger);
            dockMain.AddToolToRight("Team List", teamList);
            dockMain.AddToolToRight("Ai Trigger List", aiTriggerList);
            //dockMain.AddToolToRight("Script List", scriptList);
            //dockMain.AddToolToRight("Taskforce List", taskforceList);
            dockMain.AddToolToRight("Team", team);
            //dockMain.AddToolToRight("Script", script);

            dockMain.AddCenterPage("Map", pnlMain);
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

        #region Panel Listener
        private void PanelListener()
        {
            pnlMain.RedrawBegin += PnlMain_RedrawBegin;
            pnlMain.RedrawEnd += PnlMain_RedrawEnd;
        }

        private void PnlMain_RedrawEnd(object sender, EventArgs e)
        {
            dispatcher.Dispose();
        }

        private void PnlMain_RedrawBegin(object sender, EventArgs e)
        {
            dispatcher = Dispatcher.DisableProcessing();
        }
        #endregion
    }
}
