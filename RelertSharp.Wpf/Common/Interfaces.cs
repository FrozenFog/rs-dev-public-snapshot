using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RelertSharp.Wpf.Views
{
    interface IListContainer
    {
        void SortBy(bool ascending);
        void ShowingId(bool enable);
        event ContentCarrierHandler ItemSelected;
    }

    interface IObjectReciver
    {
        void ReciveObject(object sender, object recived);
    }
}
