﻿using RelertSharp.MapStructure.Logic;
using RelertSharp.Wpf.Common;
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
    /// ActionView.xaml 的交互逻辑
    /// </summary>
    public partial class ActionView : UserControl, IObjectReciver, IRsView
    {
        public ActionView()
        {
            InitializeComponent();
            DataContext = new TriggerLogicVm();
        }

        public GuiViewType ViewType => GuiViewType.Action;

        public void ReciveObject(object sender, object recived)
        {
            if (recived != null) DataContext = new TriggerLogicVm((recived as TriggerItem).Actions);
            else DataContext = new TriggerLogicVm();
        }
    }
}
