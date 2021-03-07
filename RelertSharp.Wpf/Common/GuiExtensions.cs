using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using AvalonDock;
using AvalonDock.Layout;

namespace System.Windows
{
    internal static class WpfWindowsExtensions
    {
        public static System.Windows.Point WpfPoint(this System.Drawing.Point src)
        {
            return new Point(src.X, src.Y);
        }
        public static System.Drawing.Point GdiPoint(this System.Windows.Point src)
        {
            return new Drawing.Point((int)src.X, (int)src.Y);
        }
    }
}

namespace RelertSharp.Wpf
{
    internal static class GuiExtensions
    {
        #region Generals
        public static double GetScale(this FrameworkElement src)
        {
            PresentationSource source = PresentationSource.FromVisual(src);
            if (source == null) return 1;
            return source.CompositionTarget.TransformToDevice.M11;
        }
        public static int ScaledWidth(this FrameworkElement src, double additionalFactor = 1.0)
        {
            PresentationSource target = PresentationSource.FromVisual(src);
            if (target == null) return 0;
            double scale = target.CompositionTarget.TransformToDevice.M11;
            return (int)(scale * src.ActualWidth * additionalFactor);
        }
        public static int ScaledHeight(this FrameworkElement src, double additionalFactor = 1.0)
        {
            PresentationSource target = PresentationSource.FromVisual(src);
            if (target == null) return 0;
            double scale = target.CompositionTarget.TransformToDevice.M11;
            return (int)(scale * src.ActualHeight * additionalFactor);
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
        private static void AddToolToLayout(Func<LayoutAnchorGroup> selector, Action<LayoutAnchorGroup> creator, string title, object content)
        {
            LayoutAnchorGroup group = selector.Invoke();
            if (group == null)
            {
                group = new LayoutAnchorGroup();
                creator?.Invoke(group);
            }
            LayoutAnchorable anchorable = new LayoutAnchorable()
            {
                Title = title,
                Content = content
            };
            group.Children.Add(anchorable);
        }
        public static void AddToolToTop(this LayoutRoot root, string title, object content)
        {
            AddToolToLayout(() => { return root.TopSide.Children.FirstOrDefault(); }, 
                            (g) => { root.TopSide.Children.Add(g); },
                            title, content);
        }
        public static void AddToolToRight(this LayoutRoot root, string title, object content)
        {
            AddToolToLayout(() => { return root.RightSide.Children.FirstOrDefault(); },
                            (g) => { root.RightSide.Children.Add(g); },
                            title, content);
        }
        public static bool AddToolToRight(this LayoutRoot root, string title, object content, int level)
        {
            var groups = root.RightSide.Children;
            LayoutAnchorGroup group;
            if (groups.Count < level) return false;
            if (groups.Count == level)
            {
                group = new LayoutAnchorGroup();
                root.RightSide.Children.Add(group);
            }
            else
            {
                group = groups[level];
            }
            LayoutAnchorable anc = new LayoutAnchorable()
            {
                Title = title,
                Content = content
            };
            group.Children.Add(anc);
            return true;
        }
        public static void AddToolToBottom(this LayoutRoot root, string title, object content)
        {
            AddToolToLayout(() => { return root.BottomSide.Children.FirstOrDefault(); },
                            (g) => { root.BottomSide.Children.Add(g); },
                            title, content);
        }
        public static void AddToolToLeft(this LayoutRoot root, string title, object content)
        {
            AddToolToLayout(() => { return root.LeftSide.Children.FirstOrDefault(); },
                            (g) => { root.LeftSide.Children.Add(g); },
                            title, content);
        }
        public static LayoutAnchorGroup SelectRight(this LayoutRoot root, bool createNewIfNotExist = true)
        {
            LayoutAnchorGroup group = root.RightSide.Children.FirstOrDefault();
            if (group == null && createNewIfNotExist)
            {
                group = new LayoutAnchorGroup();
                root.RightSide.Children.Add(group);
            }
            return group;
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
