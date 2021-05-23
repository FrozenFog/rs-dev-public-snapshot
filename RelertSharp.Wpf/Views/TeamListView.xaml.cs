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
using RelertSharp.MapStructure.Logic;
using RelertSharp.Wpf.ViewModel;
using RelertSharp.Common;
using RelertSharp.Wpf.Common;
using System.Windows.Threading;

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// TeamListView.xaml 的交互逻辑
    /// </summary>
    public partial class TeamListView : UserControl, IListContainer, IRsView
    {
        public GuiViewType ViewType { get { return GuiViewType.TeamList; } }

        public TeamListView()
        {
            InitializeComponent();
            DataContext = GlobalCollectionVm.Teams;
            _dragTimer = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 0, 0, DRAG_INTERVAL)
            };
            _dragTimer.Tick += DragTick;
        }

        public event ContentCarrierHandler ItemSelected;

        private void IdUnchecked(object sender, RoutedEventArgs e)
        {
            GlobalVar.CurrentMapDocument?.Map.Teams.ChangeDisplay(IndexableDisplayType.NameOnly);
            GlobalCollectionVm.Teams.UpdateAll();
        }

        private void IdChecked(object sender, RoutedEventArgs e)
        {
            GlobalVar.CurrentMapDocument?.Map.Teams.ChangeDisplay(IndexableDisplayType.IdAndName);
            GlobalCollectionVm.Teams.UpdateAll();
        }

        private void AscendingSort(object sender, RoutedEventArgs e)
        {
            GlobalVar.CurrentMapDocument?.Map.Teams.AscendingSort();
            GlobalCollectionVm.Teams.UpdateAll();
        }

        private void DescendingSort(object sender, RoutedEventArgs e)
        {
            GlobalVar.CurrentMapDocument?.Map.Teams.DescendingSort();
            GlobalCollectionVm.Teams.UpdateAll();
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
            ItemSelected?.Invoke(this, lbxMain.SelectedItem);
        }



        #region Drag Drop
        private DispatcherTimer _dragTimer;
        private const int DRAG_INTERVAL = 200;
        private Point prevMouseDown;
        private TeamItem dragItem;
        private bool isDraging = false;

        private void DragMouseMove(object sender, MouseEventArgs e)
        {
            if (isDraging && e.LeftButton == MouseButtonState.Pressed)
            {
                Point current = e.GetPosition(lbxMain);
                if (RsMath.ChebyshevDistance(current, prevMouseDown) > 10 && dragItem != null)
                {
                    DataObject obj = new DataObject(dragItem);
                    DragDropEffects effect = DragDrop.DoDragDrop(lbxMain, obj, DragDropEffects.Move);
                }
            }
        }
        private void TeamDragOver(object sender, DragEventArgs e)
        {
            Point current = e.GetPosition(lbxMain);
            if (RsMath.ChebyshevDistance(current, prevMouseDown) > 10)
            {
                e.Effects = DragDropEffects.Scroll;
            }
            e.Handled = true;
        }

        private void DragMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                _dragTimer.Stop();
                _dragTimer.Start();
                prevMouseDown = e.GetPosition(lbxMain);
                TextBlock blk = lbxMain.GetItemAtMouse<TextBlock>(e);
                if (blk != null)
                {
                    dragItem = blk.DataContext as TeamItem;
                }
            }
        }
        private void DragTick(object sender, EventArgs e)
        {
            _dragTimer.Stop();
            isDraging = true;
        }
        private void DragMouseLeave(object sender, MouseEventArgs e)
        {
            if (isDraging) isDraging = false;
        }
        #endregion
    }
}
