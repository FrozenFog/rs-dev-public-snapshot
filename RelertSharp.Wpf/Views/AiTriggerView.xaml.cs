﻿using RelertSharp.Common;
using RelertSharp.Wpf.ViewModel;
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

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// AiTriggerView.xaml 的交互逻辑
    /// </summary>
    public partial class AiTriggerView : UserControl, IObjectReciver
    {
        private AiTriggerVm context { get { return DataContext as AiTriggerVm; } }
        private ModConfig config { get { return GlobalVar.GlobalConfig.ModConfig; } }
        public AiTriggerView()
        {
            InitializeComponent();
            DataContext = new AiTriggerVm();
        }

        public void ReciveObject(object sender, object recived)
        {
            DataContext = new AiTriggerVm(recived);
        }
    }
}
