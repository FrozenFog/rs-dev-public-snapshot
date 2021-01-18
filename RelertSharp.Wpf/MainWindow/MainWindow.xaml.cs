﻿using AvalonDock.Layout;
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
using RelertSharp.Wpf.ViewModel;
using RelertSharp.Wpf.Views;

namespace RelertSharp.Wpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AiTriggerView aiTrigger = new AiTriggerView();
        private readonly TeamListView teamList = new TeamListView();
        private readonly AiTriggerListView aiTriggerList = new AiTriggerListView();
        private readonly ScriptListView scriptList = new ScriptListView();
        private readonly TaskforceListView taskforceList = new TaskforceListView();

        public MainWindow()
        {
            InitializeComponent();
            AddToolPage();
        }

        private void AddToolPage()
        {
            dockMain.AddToolToRight("Ai Trigger Edit", aiTrigger);
            dockMain.AddToolToRight("Team List", teamList);
            dockMain.AddToolToRight("Ai Trigger List", aiTriggerList);
            dockMain.AddToolToRight("Script List", scriptList);
            dockMain.AddToolToRight("Taskforce List", taskforceList);
        }
    }
}