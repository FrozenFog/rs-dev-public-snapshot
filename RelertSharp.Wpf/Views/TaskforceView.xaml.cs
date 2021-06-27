using AvalonDock.Layout;
using RelertSharp.Common;
using RelertSharp.Wpf.Common;
using RelertSharp.Wpf.ViewModel;
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

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// TaskforceView.xaml 的交互逻辑
    /// </summary>
    public partial class TaskforceView : UserControl, IObjectReciver, IRsView
    {
        private bool initialized;
        private List<IIndexableItem> availableUnits = new List<IIndexableItem>();
        private TaskforceVm Context { get { return DataContext as TaskforceVm; } }
        public TaskforceView()
        {
            InitializeComponent();
            DataContext = new TaskforceVm();
            search = new DelayedAction(null, SearchInListBox, 500);
        }

        public GuiViewType ViewType => GuiViewType.Taskforce;

        public LayoutAnchorable ParentAncorable { get; set; }
        public LayoutDocument ParentDocument { get; set; }

        public void ReciveObject(object sender, object recived)
        {
            if (!initialized) Initialize();
            DataContext = new TaskforceVm(recived);
        }


        #region Private
        private void Initialize()
        {
            lbxUnits.Items.Clear();
            foreach (IIndexableItem unit in GlobalVar.GlobalRules.TaskforceUnitAvailableList)
            {
                ComboItem combo = new ComboItem(unit.Id, unit.Name);
                availableUnits.Add(combo);
                lbxUnits.Items.Add(combo);
            }
            initialized = true;
        }
        #endregion


        #region Handler
        #region Lbx
        private void ScrollSelection(object sender, SelectionChangedEventArgs e)
        {
            lbxUnits.ScrollIntoView(lbxUnits.SelectedItem);
        }
        private DelayedAction search;
        private void TextChangeSearch(object sender, TextChangedEventArgs e)
        {
            search.Restart();
        }
        private void SearchInListBox()
        {
            lbxUnits.Items.Clear();
            string search = txbSearch.Text;
            if (search.IsNullOrEmpty())
            {
                lbxUnits.Items.AddRange(availableUnits);
            }
            else
            {
                IEnumerable<IIndexableItem> searchResult = availableUnits.Where(x => x.Id.Contains(search) || x.Name.Contains(search));
                lbxUnits.Items.AddRange(searchResult);
            }
        }
        #endregion


        #region Misc
        private void AmountWheel(object sender, MouseWheelEventArgs e)
        {
            if (Context.SelectedItem != null && int.TryParse(txbAmount.Text, out int num))
            {
                num += e.Delta / Math.Abs(e.Delta);
                num = RsMath.ForcePositive(num);
                Context.SelectedItem.UnitNum = num;
            }
        }
        #endregion

        #endregion


        #region Menu
        private void Menu_Add(object sender, RoutedEventArgs e)
        {
            int idx = lvMain.SelectedIndex;
            if (idx < 0) idx = Context.Count;
            else idx++;
            string regname = lbxUnits.SelectedItem == null ? Constant.ITEM_NONE : (lbxUnits.SelectedItem as IIndexableItem).Id;
            Context.AddItemAt(idx, regname);
            lvMain.SelectedIndex = idx;
        }

        private void Menu_Delete(object sender, RoutedEventArgs e)
        {
            int idx = lvMain.SelectedIndex;
            if (idx < 0) return;
            Context.RemoveItemAt(idx);
        }

        private void Menu_Copy(object sender, RoutedEventArgs e)
        {
            int idx = lvMain.SelectedIndex;
            if (idx < 0) return;
            Context.CopyItem(idx);
            lvMain.SelectedIndex = idx;
        }

        private void Menu_RemoveAll(object sender, RoutedEventArgs e)
        {
            if (Context.Count > 0) Context.RemoveAllItem();
        }
        #endregion
    }
}
