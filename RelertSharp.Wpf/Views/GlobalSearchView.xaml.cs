using AvalonDock.Layout;
using RelertSharp.Common;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Wpf.Common;
using RelertSharp.Wpf.ViewModel;
using RelertSharp.Engine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Color = System.Drawing.Color;
using WpfColor = System.Windows.Media.Color;
using RelertSharp.MapStructure.Objects;

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// GlobalSearchView.xaml 的交互逻辑
    /// </summary>
    public partial class GlobalSearchView : UserControl, IRsView
    {
        public GuiViewType ViewType => GuiViewType.GlobalSearch;

        public LayoutAnchorable ParentAncorable { get; set; }
        public LayoutDocument ParentDocument { get; set; }
        private SearchConditionVm Root = new SearchConditionVm(true);
        private SearchConditionVm SelectedCondition { get { return trvConditions.SelectedItem as SearchConditionVm; } }
        private List<object> SearchResult = new List<object>();
        private ObservableCollection<SearchResultVm> ResultVm = new ObservableCollection<SearchResultVm>();
        private MinimapSurface minimap;
        private GlobalSearchVm vm = new GlobalSearchVm();
        public GlobalSearchView()
        {
            InitializeComponent();
            dragCond = new DragDropHelper<SearchConditionVm.SearchConditionModel, SearchConditionVm>(trvConditions);
            lvResult.ItemsSource = ResultVm;
            trvConditions.Items.Add(Root);
            GlobalVar.MapDocumentLoaded += HandleMapLoaded;
            SearchHub.SearchResultPushed += AddRequestedResult;
            SearchHub.SearchClearRequested += RemoveResultHandler;
            DataContext = null;
            DataContext = vm;
        }

        private void HandleMapLoaded()
        {
            minimap = new MinimapSurface(GlobalVar.GlobalMap.Info.Size);
        }

        private void RemoveResultHandler(object sender, EventArgs e)
        {
            SearchResult.Clear();
            SetResult();
        }

        private void AddRequestedResult(IEnumerable<object> objects)
        {
            SearchResult.AddRange(objects);
            SearchResult = SearchResult.Distinct().ToList();
            SetResult();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var map = GlobalVar.GlobalMap;
            IEnumerable<object> results = new List<object>();
            if (vm.IsSearchAircraft) results = results.Concat(SearchIn(map.Aircrafts));
            if (vm.IsSearchBuilding) results = results.Concat(SearchIn(map.Buildings));
            if (vm.IsSearchBaseNode)
            {
                IEnumerable<IMapObject> nodes = map.Houses.SelectMany<HouseItem, IMapObject>(x => x.BaseNodes);
                results = results.Concat(SearchIn(nodes));
            }
            if (vm.IsSearchUnit) results = results.Concat(SearchIn(map.Units));
            if (vm.IsSearchInfantry) results = results.Concat(SearchIn(map.Infantries));
            if (vm.IsSearchOverlay) results = results.Concat(SearchIn(map.Overlays));
            if (vm.IsSearchTerrain) results = results.Concat(SearchIn(map.Terrains));
            if (vm.IsSearchSmudge) results = results.Concat(SearchIn(map.Smudges));
            if (vm.IsSearchCelltag) results = results.Concat(SearchIn(map.Celltags));
            if (vm.IsSearchWaypoint) results = results.Concat(SearchIn(map.Waypoints));
            if (vm.IsSearchTrigger) results = results.Concat(SearchIn(map.Triggers));
            if (vm.IsSearchTag) results = results.Concat(SearchIn(map.Tags));
            if (vm.IsSearchScript) results = results.Concat(SearchIn(map.Scripts));
            if (vm.IsSearchTaskforce) results = results.Concat(SearchIn(map.Taskforces));
            if (vm.IsSearchTeam) results = results.Concat(SearchIn(map.Teams));
            if (vm.IsSearchCsf) results = results.Concat(SearchIn(GlobalVar.GlobalCsf));
            if (vm.IsSearchSound) results = results.Concat(SearchIn(GlobalVar.GlobalSound.SoundList));
            if (vm.IsSearchMusic) results = results.Concat(SearchIn(GlobalVar.GlobalSound.ThemeList));
            if (vm.IsSearchEva) results = results.Concat(SearchIn(GlobalVar.GlobalSound.EvaList));
            SearchResult = results.ToList();
            SetResult();
        }
        private void btnSearchIn_Click(object sender, RoutedEventArgs e)
        {
            List<object> results = SearchIn(SearchResult);
            SearchResult = results;
            SetResult();
        }
        private void SearchSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            minimap?.ClearAllObjects();
            foreach (SearchResultVm vm in ResultVm)
            {
                if (vm.IsSelected) SetObjectColorInMinimap(vm.Data, Color.Red);
                else SetObjectColorInMinimap(vm.Data, Color.White);
            }
            imgSearchMinimap.Source = minimap.Image.ToWpfImage();
        }


        #region Search
        private void SetObjectColorInMinimap(object obj, Color c)
        {
            if (obj is I2dLocateable i2d)
            {
                if (i2d is StructureItem bud)
                {
                    foreach (I2dLocateable pos in new Foundation2D(bud))
                    {
                        minimap?.SetColor(pos, c);
                    }
                }
                else
                {
                    minimap?.SetColor(i2d, c);
                }
            }
        }
        private void SetResult()
        {
            ResultVm.Clear();
            minimap?.ClearAllObjects();
            foreach (object obj in SearchResult)
            {
                SearchResultVm vm = new SearchResultVm(obj);
                ResultVm.Add(vm);
                SetObjectColorInMinimap(obj, Color.White);
            }
            imgSearchMinimap.Source = minimap.Image.ToWpfImage();
        }
        private List<object> SearchIn(IEnumerable<object> src)
        {
            List<object> results = new List<object>();
            var map = GlobalVar.GlobalMap;
            foreach (object obj in src)
            {
                if (obj is TriggerItem trg)
                {
                    bool eventValid = false;
                    foreach (var evnt in trg.Events)
                    {
                        eventValid = eventValid || Root.IsValidObject(evnt);
                    }
                    bool actionValid = false;
                    foreach (var act in trg.Actions)
                    {
                        actionValid = actionValid || Root.IsValidObject(act);
                    }
                    if (eventValid || actionValid) results.Add(trg);
                }
                else if (obj is TeamScriptGroup script)
                {
                    bool scriptValid = false;
                    foreach (var item in script.Data)
                    {
                        scriptValid = scriptValid || Root.IsValidObject(item);
                    }
                    if (scriptValid) results.Add(script);
                }
                else
                {
                    if (Root.IsValidObject(obj)) results.Add(obj);
                }
            }
            return results;
        }
        #endregion
        #region Menu
        #region Conditions
        private void PreviewRightDown(object sender, MouseButtonEventArgs e)
        {
            if (SelectedCondition != null) SelectedCondition.IsSelected = false;
            SearchConditionVm item = trvConditions.GetItemAtMouse<SearchConditionVm, TextBlock>(e);
            if (item != null) item.IsSelected = true;
            PreviewCondMenu();
        }
        private void PreviewCondMenu()
        {
            if (SelectedCondition != null)
            {
                bool canDel = !SelectedCondition.IsRoot;
                menuDel.IsEnabled = canDel;
            }
            else
            {
                menuDel.IsEnabled = false;
            }
        }

        private void Menu_AddCond(object sender, RoutedEventArgs e)
        {
            var vm = new SearchConditionVm();
            if (SelectedCondition == null || SelectedCondition.IsRoot)
            {
                Root.AddItem(vm);
            }
            else
            {
                if (SelectedCondition.IsTree) SelectedCondition.AddItem(vm);
                else SelectedCondition.Ancestor.AddItem(vm);
            }
        }

        private void Menu_AddGroup(object sender, RoutedEventArgs e)
        {
            var vm = new SearchConditionVm(true);
            if (SelectedCondition == null || SelectedCondition.IsRoot) Root.AddItem(vm);
            else
            {
                if (SelectedCondition.IsTree) SelectedCondition.AddItem(vm);
                else SelectedCondition.Ancestor.AddItem(vm);
            }
        }

        private void Menu_RemoveCond(object sender, RoutedEventArgs e)
        {
            if (!SelectedCondition.IsRoot)
            {
                SelectedCondition.RemoveFromAncestor();
            }
        }

        private void Menu_ResetCond(object sender, RoutedEventArgs e)
        {
            Root.RemoveAllItem();
        }
        #endregion
        #region Results

        private void Menu_ResGenReport(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (SearchResultVm vm in ResultVm)
            {
                if (vm.IsSelected) sb.AppendLine(vm.GenerateReport());
            }
            Clipboard.SetText(sb.ToString());
        }
        private void ResMenuRightDown(object sender, MouseButtonEventArgs e)
        {
            bool hasItem = lvResult.SelectedItems.Count > 0;
            menuDiscardRes.IsEnabled = hasItem;
            menuSelectScene.IsEnabled = hasItem;
            menuReport.IsEnabled = hasItem;
        }
        private void Menu_ResDiscardSel(object sender, RoutedEventArgs e)
        {
            List<SearchResultVm> remove = new List<SearchResultVm>();
            List<object> removeObj = new List<object>();
            foreach (SearchResultVm vm in lvResult.SelectedItems)
            {
                remove.Add(vm);
                removeObj.Add(vm.Data);
            }
            removeObj.ForEach(x => SearchResult.Remove(x));
            remove.ForEach(x => ResultVm.Remove(x));
        }
        private void Menu_AddSelection(object sender, RoutedEventArgs e)
        {
            List<IMapObject> src = new List<IMapObject>();
            foreach (SearchResultVm vm in ResultVm)
            {
                if (vm.IsSelected && vm.Data is IMapObject obj)
                {
                    src.Add(obj);
                }
            }
            SearchHub.PushSelection(src);
        }
        #endregion
        #endregion

        #region DragDrop
        private DragDropHelper<SearchConditionVm.SearchConditionModel, SearchConditionVm> dragCond;
        private void CondDragMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                dragCond.BeginDrag(e.GetPosition(trvConditions));
                dragCond.SetReferanceVm(trvConditions.GetItemAtMouse<SearchConditionVm, StackPanel>(e));
                dragCond.SetDragItem(dragCond.ReferanceVm?.Data);
                e.Handled = true;
            }
        }

        private void CondDragMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                dragCond.EndDrag();
                if (dragCond.DragItem != null) dragCond.ReferanceVm.IsSelected = true;
            }
        }

        private void CondDragDrop(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
            e.Handled = true;
            var vm = dragCond.ReferanceVm;
            SearchConditionVm target = e.GetContext<SearchConditionVm, TextBlock>();
            if (vm != null)
            {
                if (target == null || target.Equals(Root))
                {
                    if (target.Equals(vm)) return;
                    vm.RemoveFromAncestor();
                    Root.AddItem(vm);
                }
                else if (target.IsDescendantOf(vm) || target.Equals(vm)) return;
                else if (target.IsTree)
                {
                    vm.RemoveFromAncestor();
                    target.AddItem(vm);
                }
                else if (!target.IsTree)
                {
                    vm.RemoveFromAncestor();
                    target.Ancestor.AddItem(vm);
                }
            }
        }

        private void CondDragMove(object sender, MouseEventArgs e)
        {
            if (dragCond.IsDraging && e.LeftButton== MouseButtonState.Pressed)
            {
                Point current = e.GetPosition(trvConditions);
                dragCond.MouseMoveDrag(current);
            }
        }

        private void CondDragLeave(object sender, MouseEventArgs e)
        {
            dragCond.EndDrag();
        }
        private void CondDragOver(object sender, DragEventArgs e)
        {
            Point current = e.GetPosition(trvConditions);
            dragCond.MouseMoveDrag(current);
        }
        #endregion

        #region Mouse
        private void ResTraceDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is SearchResultVm vm)
            {
                NavigationHub.AutoTrace(vm.Data);
            }
        }
        #endregion


    }
}
