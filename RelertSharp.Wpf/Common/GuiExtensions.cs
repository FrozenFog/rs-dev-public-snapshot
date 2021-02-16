using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using AvalonDock;
using AvalonDock.Layout;

namespace RelertSharp.Wpf
{
    internal static class GuiExtensions
    {
        #region Generals
        public static int ScaledWidth(this FrameworkElement src)
        {
            double scale = PresentationSource.FromVisual(src).CompositionTarget.TransformToDevice.M11;
            return (int)(scale * src.ActualWidth);
        }
        public static int ScaledHeight(this FrameworkElement src)
        {
            double scale = PresentationSource.FromVisual(src).CompositionTarget.TransformToDevice.M11;
            return (int)(scale * src.ActualHeight);
        }
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
        public static void AddToolToBottom(this DockingManager dock, string title, object content)
        {
            LayoutAnchorGroup group = dock.Layout.BottomSide.Children.FirstOrDefault();
            if (group == null)
            {
                group = new LayoutAnchorGroup();
                dock.Layout.BottomSide.Children.Add(group);
            }
            LayoutAnchorable anchorable = new LayoutAnchorable()
            {
                Title = title,
                Content = content
            };
            group.Children.Add(anchorable);
        }
        public static void AddToolToLeft(this DockingManager dock, string title, object content)
        {
            LayoutAnchorGroup group = dock.Layout.LeftSide.Children.FirstOrDefault();
            if (group == null)
            {
                group = new LayoutAnchorGroup();
                dock.Layout.LeftSide.Children.Add(group);
            }
            LayoutAnchorable anchorable = new LayoutAnchorable()
            {
                Title = title,
                Content = content
            };
            group.Children.Add(anchorable);
        }
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
        public static void AddCenterPage(this DockingManager dock, string title, object content)
        {
            LayoutDocumentPane pane = dock.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault();
            if (pane != null)
            {
                LayoutDocument doc = new LayoutDocument
                {
                    Title = title,
                    Content = content
                };
                pane.Children.Add(doc);
            }
        }
        public static void AddCenterPage(this DockingManager dock, string title, System.Windows.Forms.Control winformControl)
        {
            LayoutDocumentPane pane = dock.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault();
            if (pane != null)
            {
                WindowsFormsHost host = new WindowsFormsHost()
                {
                    Child = winformControl
                };
                LayoutDocument doc = new LayoutDocument
                {
                    Title = title,
                    Content = host
                };
                pane.Children.Add(doc);
            }
        }
        #endregion
    }
}
