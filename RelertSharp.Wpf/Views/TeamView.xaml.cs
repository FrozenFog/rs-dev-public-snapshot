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
using RelertSharp.Common;
using RelertSharp.Wpf.ViewModel;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Wpf.Common;
using RelertSharp.Common.Config.Model;

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// TeamView.xaml 的交互逻辑
    /// </summary>
    public partial class TeamView : UserControl, IObjectReciver, IRsView
    {
        private const double ROW_DELTA = 28;
        private const double ROW_HEIGHT = 23;
        private ModConfig ModConfig { get { return GlobalVar.GlobalConfig.ModConfig; } }
        private TeamVm context;
        private TeamItem Team { get { return context.Data; } }

        public GuiViewType ViewType { get { return GuiViewType.Team; } }
        public AvalonDock.Layout.LayoutAnchorable ParentAncorable { get; set; }
        public AvalonDock.Layout.LayoutDocument ParentDocument { get; set; }

        private bool isLoading = false;

        public TeamView()
        {
            InitializeComponent();
            context = new TeamVm();
            GlobalVar.MapDocumentLoaded += MapLoadedHandler;
        }

        private bool initialized = false;
        private void MapLoadedHandler()
        {
            GlobalVar.Log.Info("Initializing Team Panel");
            if (!initialized)
            {
                InitializeTeamClasses();
                initialized = true;
            }
            RefreshControl();
            GlobalVar.Log.Info("Team Panel Complete");
        }

        private void InitializeTeamClasses()
        {
            foreach (AttributeClass cls in ModConfig.TeamClasses.Values)
            {
                FrameworkElement stack = CreateGroup(cls);
                stkMain.Children.Add(stack);
            }
        }
        private static AttributeItem GetFrmElemTagAtribute(object src)
        {
            FrameworkElement elem = src as FrameworkElement;
            return elem.DataContext as AttributeItem;
        }
        private FrameworkElement CreateGroup(AttributeClass cls)
        {
            #region Grids
            StackPanel container = new StackPanel();
            Button head = new Button()
            {
                Content = cls.Name
            };
            head.HorizontalContentAlignment = HorizontalAlignment.Left;
            Grid grd = new Grid();
            ColumnDefinition colLabel = new ColumnDefinition()
            {
                Width = new GridLength(0, GridUnitType.Auto)
            };
            ColumnDefinition colSeperator = new ColumnDefinition()
            {
                Width = new GridLength(3, GridUnitType.Pixel)
            };
            ColumnDefinition colMain = new ColumnDefinition()
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            grd.ColumnDefinitions.Add(colLabel);
            grd.ColumnDefinitions.Add(colSeperator);
            grd.ColumnDefinitions.Add(colMain);
            GridSplitter split = new GridSplitter()
            {
                Width = 3,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Stretch
            };
            split.SetColumn(1);
            grd.Children.Add(split);
            double y = 10d;
            #endregion
            foreach (AttributeItem item in ModConfig.TeamItems.Values.Where(x => x.Parent == cls.Id))
            {
                Label lbl = new Label()
                {
                    Content = item.Label,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Top
                };
                lbl.Margin = new Thickness(0, y, 10, 0);
                lbl.MouseDoubleClick += ClickLabelTrace;
                lbl.SetColumn(0);
                FrameworkElement control = AcquireControlFromAttribute(item);
                control.Height = ROW_HEIGHT;
                control.Margin = new Thickness(10, y, 10, 0);
                control.SetColumn(2);
                TraceHelper trace = new TraceHelper()
                {
                    SourceControl = control,
                    TraceType = item.Trace
                };
                if (item.Trace != TriggerInfoTraceType.None)
                {
                    lbl.Drop += DraggedItemDropped;
                    lbl.AllowDrop = true;
                }
                lbl.Tag = trace;
                grd.AddControls(lbl, control);
                y += ROW_DELTA;
            }
            Grid blank = new Grid()
            {
                Height = 10,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Bottom
            };
            head.Click += (o, e) =>
            {
                if (grd.Visibility == Visibility.Visible) grd.Visibility = Visibility.Collapsed;
                else grd.Visibility = Visibility.Visible;
            };
            container.AddControls(head, grd, blank);
            return container;
        }
        private void CkbUpdate(object sender, RoutedEventArgs e)
        {
            if (!isLoading)
            {
                Team[GetFrmElemTagAtribute(sender).Name].Value = (sender as CheckBox).IsChecked.Value.YesNo();
            }
        }
        private void CbbUpdate(object sender, SelectionChangedEventArgs e)
        {
            if (!isLoading)
            {
                Team[GetFrmElemTagAtribute(sender).Name].Value = ((sender as ComboBox).SelectedItem as IIndexableItem).Value;
            }
        }
        private void TxbUpdate(object sender, RoutedEventArgs e)
        {
            if (!isLoading)
            {
                string key = GetFrmElemTagAtribute(sender).Name;
                string text = (sender as TextBox).Text;
                if (key == Constant.KEY_NAME && text != Team[key].Value)
                {
                    Team[key].Value = text;
                    Team.OnNameUpdated();
                    GlobalCollectionVm.Teams.UpdateDesignatedName();
                }
                else
                {
                    Team[key].Value = text;
                }
            }
        }
        private FrameworkElement AcquireControlFromAttribute(AttributeItem src)
        {
            FrameworkElement r;
            switch (src.ValueType)
            {
                case Constant.Config.TYPE_BOOL:
                    CheckBox ckb = new CheckBox()
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left
                    };
                    ckb.Click += CkbUpdate;
                    r = ckb;
                    break;
                case Constant.Config.TYPE_INT:
                case Constant.Config.TYPE_STRING:
                    TextBox txb = new TextBox()
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Stretch
                    };
                    txb.TextChanged += TxbUpdate;
                    r = txb;
                    break;
                default:
                    ComboBox cbb = new ComboBox()
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Stretch
                    };
                    IEnumerable<IIndexableItem> items = ModConfig.GetCombo(src.ValueType);
                    cbb.ItemsSource = items;
                    cbb.SelectionChanged += CbbUpdate;
                    r = cbb;
                    break;
            }
            r.DataContext = src;
            return r;
        }
        private void RefreshControl()
        {
            isLoading = true;
            foreach (FrameworkElement elem in stkMain.Children)
            {
                if (elem is StackPanel subPanel)
                {
                    foreach (FrameworkElement stackSub in subPanel.Children)
                    {
                        if (stackSub is Grid g)
                        {
                            foreach (FrameworkElement c in g.Children)
                            {
                                string tag = GetFrmElemTagAtribute(c)?.Name;
                                if (c is CheckBox ckb)
                                {
                                    string sb = Team[tag].Value;
                                    ckb.IsChecked = sb.IniParseBool();
                                }
                                else if (c is TextBox txb)
                                {
                                    txb.Text = Team[tag].Value;
                                }
                                else if (c is ComboBox cbb)
                                {
                                    IEnumerable<IIndexableItem> items = GlobalVar.GlobalConfig.ModConfig.GetCombo((cbb.DataContext as AttributeItem).ValueType);
                                    IIndexableItem item = items.Where(x => x.Value == Team[tag].Value).FirstOrDefault();
                                    cbb.ItemsSource = items;
                                    if (item == null) cbb.SelectedIndex = 0;
                                    else cbb.SelectedItem = item;
                                }
                            }
                        }
                    }
                }
            }
            isLoading = false;
        }

        public void ReciveObject(object sender, object recived)
        {
            context = new TeamVm(recived as TeamItem);
            RefreshControl();
        }


        private void ClickLabelTrace(object sender, MouseButtonEventArgs e)
        {
            if (!isLoading)
            {
                Label lbl = sender as Label;
                TraceHelper bind = lbl.Tag as TraceHelper;
                string value = string.Empty;
                IIndexableItem target = new SimpleIndexableItem();
                if (bind.SourceControl is ComboBox cbb)
                {
                    target = cbb.SelectedItem as IIndexableItem;
                }
                else if (bind.SourceControl is TextBox txb)
                {
                    value = txb.Text;
                }
                NavigationHub.HandleTrace(bind.TraceType, value, target);
            }
        }
        private void DraggedItemDropped(object sender, DragEventArgs e)
        {
            IDataObject data = new DataObject();
            data = e.Data;
            string value = data.TryGetDroppedDataObjectId(out Type valueType);
            if (!value.IsNullOrEmpty()) TryWriteDroppedValue(sender as Label, value, valueType);
        }
        private void TryWriteDroppedValue(Label lbl, string value, Type valueType)
        {
            if (lbl != null && lbl.Tag is TraceHelper trace)
            {
                FrameworkElement elem = trace.SourceControl;
                if (elem is ComboBox cbb && cbb.Items.Count > 0)
                {
                    IEnumerable<IIndexableItem> items = GlobalVar.GlobalConfig.ModConfig.GetCombo((cbb.DataContext as AttributeItem).ValueType);
                    IIndexableItem item = items.Where(x => x.Id == value).FirstOrDefault();
                    cbb.ItemsSource = items.ToList();
                    if (item != null)
                    {
                        cbb.SelectedItem = item;
                    }
                }
            }
        }
    }
}
