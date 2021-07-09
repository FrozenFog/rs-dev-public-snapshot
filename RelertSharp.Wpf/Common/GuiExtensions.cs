using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.IO;
using System.Windows.Media;
using AvalonDock;
using AvalonDock.Layout;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using System.Windows.Input;
using RelertSharp.Wpf.Common;
using RelertSharp.Wpf.ViewModel;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;
using RelertSharp.Wpf.Views;
using RelertSharp.Wpf;

namespace System.Windows.Media
{
    internal static class WpfMediaExtensions
    {
        public static BitmapImage ToWpfImage(this System.Drawing.Image src, bool isPng = false)
        {
            MemoryStream ms = new MemoryStream();
            BitmapImage img = new BitmapImage();
            ImageFormat fmt;
            if (isPng) fmt = ImageFormat.Png;
            else fmt = ImageFormat.Bmp;
            src.Save(ms, fmt);
            img.BeginInit();
            img.CacheOption = BitmapCacheOption.OnLoad;
            img.StreamSource = ms;
            img.EndInit();
            return img;
        }
    }
}

namespace System.Windows.Controls
{
    internal static class WpfControlExtension
    {
        public static void AddRange(this ItemCollection target, IEnumerable<object> src)
        {
            foreach (object o in src) target.Add(o);
        }
        public static TItem GetItemAtMouse<TItem, TTemplateControl>(this ItemsControl src, MouseButtonEventArgs e) where TTemplateControl : FrameworkElement where TItem : class
        {
            TTemplateControl item = src.GetItemControlAtMouse<TTemplateControl>(e);
            if (item != null) return item.DataContext as TItem;
            return null;
        }
    }
}

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
        public static Drawing.Color ToGdiColor(this System.Windows.Media.Color src)
        {
            return Drawing.Color.FromArgb(src.A, src.R, src.G, src.B);
        }
        public static Color ToWpfColor(this System.Drawing.Color src)
        {
            return Color.FromArgb(src.A, src.R, src.G, src.B);
        }
        public static Color FromArgb(uint argb)
        {
            byte a = (byte)((argb & 0xff000000) >> 24);
            byte r = (byte)((argb & 0x00ff0000) >> 16);
            byte g = (byte)((argb & 0x0000ff00) >> 8);
            byte b = (byte)(argb & 0xff);
            return Color.FromArgb(a, r, g, b);
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
            foreach (FrameworkElement c in elements)
            {
                if (c.Parent != null)
                {
                    (c.Parent as Panel).Children.Remove(c);
                }
                src.Children.Add(c);
            }
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
                Content = content,
                ContentId = RsViewComponentAttribute.GetViewName(content)
            };
            IRsView con = content as IRsView;
            con.ParentAncorable = anchorable;
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
        public static void AddCenterPage(this DockingManager dock, string title, object content)
        {
            LayoutDocumentPane pane = dock.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault();
            if (pane != null)
            {
                LayoutDocument doc = new LayoutDocument
                {
                    Title = title,
                    Content = content,
                    ContentId = RsViewComponentAttribute.GetViewName(content)
                };
                IRsView con = content as IRsView;
                con.ParentDocument = doc;
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
        public static bool HasDocumentWithContentId(this DockingManager dock, string id)
        {
            bool find(ILayoutElement ob)
            {
                if (ob is LayoutPanel pnl)
                {
                    foreach (var sub in pnl.Children) if (find(sub)) return true;
                }
                else if (ob is LayoutDocumentPaneGroup group)
                {
                    foreach (var sub in group.Children) if (find(sub)) return true;
                }
                else if (ob is LayoutDocumentPane docPane)
                {
                    foreach (var sub in docPane.Children) if (find(sub)) return true;
                }
                else if (ob is LayoutDocument doc)
                {
                    if (doc.ContentId == id) return true;
                    return false;
                }
                return false;
            }
            foreach (ILayoutPanelElement elem in dock.Layout.RootPanel.Children)
            {
                if (find(elem)) return true;
            }
            return false;
        }
        #endregion


        #region TreeView
        public static TreeViewItem CastSelectedItem(this TreeView src)
        {
            return src.ItemContainerGenerator.ContainerFromItem(src.SelectedItem) as TreeViewItem;
        }
        #endregion


        #region Items container
        public static TCast GetItemControlAtMouse<TCast>(this ItemsControl src, MouseButtonEventArgs e) where TCast : class
        {
            Point p = e.GetPosition(src);
            TCast dest = null;
            UIElement elem = src.InputHitTest(p) as UIElement;
            while (elem != null)
            {
                if (elem == src) dest = null;
                object item = src.ItemContainerGenerator.ItemFromContainer(elem);
                if (!item.Equals(DependencyProperty.UnsetValue))
                {
                    dest = item as TCast;
                    break;
                }
                if (elem is TCast target)
                {
                    dest = target;
                    break;
                }
                elem = (UIElement)VisualTreeHelper.GetParent(elem);
            }
            return dest;
        }
        public static void SelectItem<TItem>(this ListBox src, TItem target, Func<TItem, TItem, bool> selector) where TItem : class
        {
            foreach (object o in src.Items)
            {
                if (o is TItem item)
                {
                    if (selector(item, target))
                    {
                        src.SelectedItem = o;
                        return;
                    }
                }
            }
        }
        #endregion


        #region Drag Drop - DataObject
        public static string TryGetDroppedDataObjectId(this IDataObject src, out Type dataType)
        {
            dataType = null;
            if (src.GetData(typeof(TriggerTreeItemVm)) is TriggerTreeItemVm trigger)
            {
                if (trigger.Data != null)
                {
                    dataType = typeof(TriggerItem);
                    return trigger.Data.Id;
                }
            }
            else if (src.GetData(typeof(TeamItem)) is TeamItem team)
            {
                dataType = typeof(TeamItem);
                return team.Id;
            }
            else if (src.GetData(typeof(TeamScriptGroup)) is TeamScriptGroup script)
            {
                dataType = typeof(TeamScriptGroup);
                return script.Id;
            }
            else if (src.GetData(typeof(TaskforceItem)) is TaskforceItem taskforce)
            {
                dataType = typeof(TaskforceItem);
                return taskforce.Id;
            }
            else if (src.GetData(typeof(HouseItem)) is HouseItem house)
            {
                dataType = typeof(ComboItem);
                return GlobalVar.CurrentMapDocument.Map.Houses.IndexOf(house.Name).ToString();
            }
            return string.Empty;
        }
        #endregion
    }
}
