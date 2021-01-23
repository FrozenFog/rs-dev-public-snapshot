using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        public static void SetColumn(this FrameworkElement src, int column)
        {
            src.SetValue(Grid.ColumnProperty, column);
        }
        public static void AddControls(this Panel src, params FrameworkElement[] elements)
        {
            foreach (FrameworkElement c in elements) src.Children.Add(c);
        }
        public static void SetStyle(this FrameworkElement src, FrameworkElement provider, string key)
        {
            src.Style = provider.FindResource(key) as Style;
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
