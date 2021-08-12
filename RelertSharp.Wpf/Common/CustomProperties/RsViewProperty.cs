using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace RelertSharp.Wpf.Common.CustomProperties
{
    internal partial class Ex
    {
        public static readonly DependencyProperty RsViewProperty =
            DependencyProperty.RegisterAttached("RsView", typeof(GuiViewType), typeof(Ex),
                new PropertyMetadata(GuiViewType.Undefined, (o, e) =>
                {
                    RoutedEvent @event = null;
                    if (o is MenuItem) @event = MenuItem.ClickEvent;
                    else if (o is ButtonBase) @event = ButtonBase.ClickEvent;
                    if (o is UIElement menu && menu.GetValue(RsViewProperty) is GuiViewType type)
                    {
                        if (type != GuiViewType.Undefined)
                        {
                            GuiViewType oldValue = (GuiViewType)e.OldValue;
                            GuiViewType newValue = (GuiViewType)e.NewValue;
                            if (oldValue == GuiViewType.Undefined && newValue != GuiViewType.Undefined)
                            {
                                menu.AddHandler(@event, new RoutedEventHandler(MenuViewClick));
                            }
                            if (oldValue != GuiViewType.Undefined && newValue == GuiViewType.Undefined)
                            {
                                menu.RemoveHandler(@event, new RoutedEventHandler(MenuViewClick));
                            }
                        }
                    }
                }));
        public static void SetRsView(UIElement elem, GuiViewType type)
        {
            elem.SetValue(RsViewProperty, type);
        }
        public static GuiViewType GetRsView(UIElement elem)
        {
            return (GuiViewType)elem.GetValue(RsViewProperty);
        }


        private static void MenuViewClick(object sender, RoutedEventArgs e)
        {
            if (sender is UIElement menu && menu.GetValue(RsViewProperty) is GuiViewType type)
            {
                LayoutManagerHub.ShowView(type);
            }
        }
    }
}
