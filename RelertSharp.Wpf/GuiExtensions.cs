﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AvalonDock;
using AvalonDock.Layout;

namespace RelertSharp.Wpf
{
    internal static class GuiExtensions
    {
        #region Generals
        public static void RefreshContext(this FrameworkElement src, object context)
        {
            src.DataContext = null;
            src.DataContext = context;
        }
        #endregion


        #region DockingManager
        public static void AddToolToRight(this DockingManager dock, string title, object content)
        {
            LayoutAnchorGroup group = dock.Layout.RightSide.Children.FirstOrDefault();
            if (group == null)
            {
                group = new LayoutAnchorGroup();
                dock.Layout.RightSide.Children.Add(group);
            }
            LayoutAnchorable anchorable = new LayoutAnchorable()
            {
                Title = title,
                Content = content
            };
            group.Children.Add(anchorable);
        }
        #endregion
    }
}
