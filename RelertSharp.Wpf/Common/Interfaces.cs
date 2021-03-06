using AvalonDock.Layout;
using RelertSharp.Common;
using RelertSharp.Wpf.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RelertSharp.Wpf.Views
{
    interface IListContainer : IRsView
    {
        void SortBy(bool ascending);
        void ShowingId(bool enable);
        void SelectItem(IIndexableItem item);
        IIndexableItem GetSelectedItem();
        event ContentCarrierHandler ItemSelected;
    }

    interface IObjectReciver : IRsView
    {
        void ReciveObject(object sender, object recived);
    }

    interface IRsView
    {
        GuiViewType ViewType { get; }
        LayoutAnchorable ParentAncorable { get; set; }
        LayoutDocument ParentDocument { get; set; }
    }

    interface IFinalizeableView : IRsView
    {
        void DoFinalization();
    }
}
