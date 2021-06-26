using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RelertSharp.Wpf.ViewModel;
using RelertSharp.Common;
using RelertSharp.Wpf.Common;
using System.Collections.ObjectModel;

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// ScriptListView.xaml 的交互逻辑
    /// </summary>
    public partial class ScriptListView : UserControl, IListContainer, IRsView
    {
        public GuiViewType ViewType { get { return GuiViewType.ScriptList; } }
        public AvalonDock.Layout.LayoutAnchorable ParentAncorable { get; set; }
        public AvalonDock.Layout.LayoutDocument ParentDocument { get; set; }

        public ScriptListView()
        {
            InitializeComponent();
            DataContext = GlobalCollectionVm.Scripts;
            GlobalVar.MapDocumentLoaded += MapReloadedHandler;
            NavigationHub.GoToScriptRequest += SelectItem;
            NavigationHub.BindScriptList(this);
        }

        private void MapReloadedHandler(object sender, EventArgs e)
        {
            lbxMain.Items.Clear();
            foreach (var script in GlobalVar.CurrentMapDocument.Map.Scripts)
            {
                lbxMain.Items.Add(new ScriptListVm(script));
            }
        }


        #region Private
        #endregion

        public event ContentCarrierHandler ItemSelected;

        private void IdUnchecked(object sender, RoutedEventArgs e)
        {
            foreach (ScriptListVm script in lbxMain.Items)
            {
                script.ChangeDisplay(IndexableDisplayType.NameOnly);
            }
            GlobalVar.CurrentMapDocument?.Map.Scripts.ChangeDisplay(IndexableDisplayType.NameOnly);
            lbxMain.Items.Refresh();
        }

        private void IdChecked(object sender, RoutedEventArgs e)
        {
            foreach (ScriptListVm script in lbxMain.Items)
            {
                script.ChangeDisplay(IndexableDisplayType.IdAndName);
            }
            GlobalVar.CurrentMapDocument?.Map.Scripts.ChangeDisplay(IndexableDisplayType.IdAndName);
            lbxMain.Items.Refresh();
        }

        private void AscendingSort(object sender, RoutedEventArgs e)
        {
            List<ScriptListVm> src = lbxMain.Items.Cast<ScriptListVm>().ToList();
            src = src.OrderBy(x => x.Title).ToList();
            lbxMain.Items.Clear();
            src.ForEach(x => lbxMain.Items.Add(x));
        }

        private void DescendingSort(object sender, RoutedEventArgs e)
        {
            List<ScriptListVm> src = lbxMain.Items.Cast<ScriptListVm>().ToList();
            src = src.OrderByDescending(x => x.Title).ToList();
            lbxMain.Items.Clear();
            src.ForEach(x => lbxMain.Items.Add(x));
        }


        public void SortBy(bool ascending)
        {
            if (ascending)
            {
                AscendingSort(null, null);
            }
            else
            {
                DescendingSort(null, null);
            }
        }

        public void ShowingId(bool enable)
        {
            if (enable)
            {
                IdChecked(null, null);
            }
            else
            {
                IdUnchecked(null, null);
            }
        }

        private void SelectedItemChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbxMain.SelectedItem is ScriptListVm script)
            {
                ItemSelected?.Invoke(this, script.Data);
            }
        }

        public void SelectItem(IIndexableItem item)
        {
            lbxMain.SelectItem(item, (a, b) =>
            {
                return a.Id == b.Id;
            });
        }

        public IIndexableItem GetSelectedItem()
        {
            return (lbxMain.SelectedItem as ScriptListVm).Data;
        }
    }
}
