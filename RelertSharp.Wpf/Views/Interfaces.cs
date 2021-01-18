using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RelertSharp.Wpf.Views
{
    interface IListToolPageContent
    {
        void SortBy(bool ascending);
        void ShowingId(bool enable);
    }
}
