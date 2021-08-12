using AvalonDock;
using AvalonDock.Layout;
using RelertSharp.Wpf.Common;
using RelertSharp.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf
{
    internal static class LayoutManagerHub
    {
        private static DockingManager manager;
        private static MainWindow mwnd;
        #region Api
        public static void BindManager(DockingManager dock, MainWindow window)
        {
            manager = dock;
            mwnd = window;
        }
        public static void SwitchToActivateContent(IRsView view)
        {
            LayoutContent obj;
            if (view.ParentAncorable == null) obj = view.ParentDocument;
            else obj = view.ParentAncorable;
            manager.ActiveContent = obj;
        }
        public static IRsView GetView(GuiViewType type)
        {
            FieldInfo[] fields = typeof(MainWindow).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo info in fields)
            {
                RsViewComponentAttribute attr = info.GetCustomAttribute<RsViewComponentAttribute>();
                if (attr != null && attr.ViewType == type)
                {
                    IRsView control = info.GetValue(mwnd) as IRsView;
                    return control;
                }
            }
            return null;
        }
        public static void ShowView(IRsView src)
        {
            if (src.ParentAncorable != null)
            {
                bool loaded = false;
                foreach (LayoutAnchorable anc in manager.Layout.Hidden)
                {
                    if (anc.ContentId == src.ViewType.ToString())
                    {
                        manager.Layout.Hidden.Remove(anc);
                        LoadTargetTool(src.ViewType);
                        loaded = true;
                        break;
                    }
                }
                if (!loaded) LoadTargetTool(src.ViewType);
            }
            if (src.ParentDocument != null && !manager.HasDocumentWithContentId(src.ViewType.ToString()))
            {
                manager.AddCenterPage(RsViewComponentAttribute.GetViewTitle(src), src);
            }
        }
        public static void ShowView(GuiViewType type)
        {
            IRsView view = GetView(type);
            ShowView(view);
        }
        #endregion

        #region Private
        private static void LoadTargetTool(GuiViewType type)
        {
            FieldInfo[] fields = typeof(MainWindow).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo info in fields)
            {
                RsViewComponentAttribute attr = info.GetCustomAttribute<RsViewComponentAttribute>();
                if (attr != null && attr.ViewType == type)
                {
                    IRsView control = info.GetValue(mwnd) as IRsView;
                    switch (attr.Side)
                    {
                        case GuiViewSide.Top:
                            manager.Layout.AddToolToTop(attr.Title, control);
                            break;
                        case GuiViewSide.Bottom:
                            manager.Layout.AddToolToBottom(attr.Title, control);
                            break;
                        case GuiViewSide.Left:
                            manager.Layout.AddToolToLeft(attr.Title, control);
                            break;
                        case GuiViewSide.Right:
                            manager.Layout.AddToolToRight(attr.Title, control);
                            break;
                        case GuiViewSide.Center:
                            manager.AddCenterPage(attr.Title, control);
                            break;
                    }
                    return;
                }
            }
        }
        #endregion
    }
}
