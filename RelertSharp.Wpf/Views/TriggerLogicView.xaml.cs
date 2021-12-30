using RelertSharp.Common;
using RelertSharp.Common.Config.Model;
using RelertSharp.FileSystem;
using RelertSharp.MapStructure.Logic;
using RelertSharp.MapStructure.Points;
using RelertSharp.Wpf.Common;
using RelertSharp.Wpf.Dialogs;
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
using static RelertSharp.Wpf.Common.GuiConst;

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// TriggerLogicView.xaml 的交互逻辑
    /// </summary>
    public partial class TriggerLogicView : UserControl, IRsView, IObjectReciver
    {
        private bool initialized = false;
        private bool isLoadingParameter = false;
        private Dictionary<string, ComboBox> cbbBuffer = new Dictionary<string, ComboBox>();


        private ModConfig Config { get { return GlobalVar.GlobalConfig.ModConfig; } }
        private TriggerLogicVm Context { get { return DataContext as TriggerLogicVm; } }


        public TriggerLogicView()
        {
            InitializeComponent();
            SetDataContext(null);
        }
        public TriggerLogicView(bool isEvent)
        {
            InitializeComponent();
            if (!isEvent)
            {
                ViewType = GuiViewType.Action;
                lblType.Content = "Actions";
            }
            else
            {
                menuDisableNew.Visibility = Visibility.Collapsed;
                menuEnableNew.Visibility = Visibility.Collapsed;
            }
            SetDataContext(null);
        }

        public GuiViewType ViewType { get; } = GuiViewType.Event;
        public AvalonDock.Layout.LayoutAnchorable ParentAncorable { get; set; }
        public AvalonDock.Layout.LayoutDocument ParentDocument { get; set; }

        public void ReciveObject(object sender, object recived)
        {
            if (!initialized) Initialize();
            SetDataContext(recived);
        }

        private void SetDataContext(object recived)
        {
            if (recived == null)
            {
                DataContext = new TriggerLogicVm();
            }
            else
            {
                if (ViewType == GuiViewType.Event) DataContext = new TriggerLogicVm((recived as TriggerItem).Events);
                else DataContext = new TriggerLogicVm((recived as TriggerItem).Actions);
            }
            RefreshControl();
        }

        private void Initialize()
        {
            if (ViewType == GuiViewType.Action) cbbCurrent.ItemsSource = GlobalVar.GlobalConfig.ModConfig.TriggerInfo.TriggerActions;
            else cbbCurrent.ItemsSource = GlobalVar.GlobalConfig.ModConfig.TriggerInfo.TriggerEvents;
            initialized = true;
        }

        /// <summary>
        /// menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewRightDown(object sender, MouseButtonEventArgs e)
        {
            bool hasItem = lbxMain.SelectedItem != null;
            menuDelete.IsEnabled = hasItem;
            menuCopy.IsEnabled = hasItem;
            menuUp.IsEnabled = hasItem;
            menuDown.IsEnabled = hasItem;
        }
        #region curd
        private void Menu_Add(object sender, RoutedEventArgs e)
        {
            int idx = lbxMain.SelectedIndex;
            if (idx < 0) idx = Context.Count;
            else idx++;
            Context.AddItemAt(idx);
        }

        private void Menu_Delete(object sender, RoutedEventArgs e)
        {
            int idx = lbxMain.SelectedIndex;
            if (idx < 0) return;
            Context.RemoveItemAt(idx);
        }

        private void Menu_Copy(object sender, RoutedEventArgs e)
        {
            int idx = lbxMain.SelectedIndex;
            if (idx < 0) return;
            Context.CopyItem(idx);
        }

        private void Menu_MoveUp(object sender, RoutedEventArgs e)
        {
            int idx = lbxMain.SelectedIndex;
            if (idx <= 0) return;
            Context.MoveItemTo(idx, idx - 1);
            lbxMain.SelectedIndex = idx - 1;
        }

        private void Menu_MoveDown(object sender, RoutedEventArgs e)
        {
            int idx = lbxMain.SelectedIndex;
            if (idx >= Context.Count - 1) return;
            Context.MoveItemTo(idx, idx + 1);
            lbxMain.SelectedIndex = idx + 1;
        }
        private void Menu_Top(object sender, RoutedEventArgs e)
        {
            int idx = lbxMain.SelectedIndex;
            if (idx <= 0) return;
            Context.MoveItemTo(idx, 0);
            lbxMain.SelectedIndex = 0;
        }

        private void Menu_Bottom(object sender, RoutedEventArgs e)
        {
            int last = Context.Count - 1;
            int idx = lbxMain.SelectedIndex;
            if (idx >= last) return;
            Context.MoveItemTo(idx, last);
            lbxMain.SelectedIndex = last;
        }

        private void Menu_DisableNew(object sender, RoutedEventArgs e)
        {
            DlgNameInput dlg = new DlgNameInput("Trigger name");
            if (dlg.ShowDialog().Value)
            {
                int idx = lbxMain.SelectedIndex;
                if (idx < 0) idx = Context.Count;
                else idx++;
                TriggerItem newTrigger = GlobalVar.CurrentMapDocument.Map.AddTrigger(dlg.ResultName);
                Context.DisableNewTrigger(idx, newTrigger.Id);
                TriggerUtilHub.PushNewTriggerToList(newTrigger);
            }
        }

        private void Menu_EnableNew(object sender, RoutedEventArgs e)
        {
            DlgNameInput dlg = new DlgNameInput("Trigger name");
            if (dlg.ShowDialog().Value)
            {
                int idx = lbxMain.SelectedIndex;
                if (idx < 0) idx = Context.Count;
                else idx++;
                TriggerItem newTrigger = GlobalVar.CurrentMapDocument.Map.AddTrigger(dlg.ResultName);
                Context.EnableNewTrigger(idx, newTrigger.Id);
                TriggerUtilHub.PushNewTriggerToList(newTrigger);
            }
        }

        private void Menu_RemoveAll(object sender, RoutedEventArgs e)
        {
            if (Context.Count > 0) Context.RemoveAllItem();
        }

        private void Menu_Cmd(object sender, RoutedEventArgs e)
        {
            DlgCmdInput dlg = new DlgCmdInput();
            if (dlg.ShowDialog().Value)
            {
                string command = dlg.ResultCommand;
                Context.ApplyCommand(command);
            }
        }
        #endregion

        #region ParameterIO

        #region Common
        internal void RefreshControl()
        {
            LoadControl();
            LoadParameter();
        }
        private void LoadControl()
        {
            ClearStack();
            LoadStack();
        }
        private void LoadParameter()
        {
            if (Context.SelectedItem == null) return;
            isLoadingParameter = true;
            foreach (FrameworkElement elem in stkMain.Children)
            {
                if (elem is Grid grid)
                {
                    foreach (FrameworkElement c in grid.Children)
                    {
                        LogicInfoParameter info = GetInfoFromElem(c);
                        if (info != null)
                        {
                            string value = Context.SelectedItem.GetParameter(info);
                            if (c is CheckBox ckb)
                            {
                                ckb.IsChecked = value.IniParseBool();
                            }
                            else if (c is TextBox txb)
                            {
                                txb.Text = value;
                            }
                            else if (c is ComboBox cbb)
                            {
                                if (info.ValueType == Constant.Config.DefaultComboType.TYPE_CSF)
                                {
                                    CsfString csf = GlobalVar.GlobalCsf[value];
                                    cbb.SelectedItem = csf;
                                }
                                else
                                {
                                    IIndexableItem item = cbb.ItemsSource.OfType<IIndexableItem>().Where(x => x.Id == value).FirstOrDefault();
                                    if (item == null) cbb.SelectedIndex = 0;
                                    else cbb.SelectedItem = item;
                                }
                            }
                        }
                    }
                }
            }
            isLoadingParameter = false;
        }
        private void ClearStack()
        {
            stkMain.Children.Clear();
        }
        private void LoadStack()
        {
            if (Context.SelectedItem == null) return;
            Grid grd = new Grid();
            ColumnDefinition colLabel = new ColumnDefinition()
            {
                Width = new GridLength(0, GridUnitType.Auto)
            };
            ColumnDefinition colMain = new ColumnDefinition()
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            grd.ColumnDefinitions.Add(colLabel);
            grd.ColumnDefinitions.Add(colMain);
            double y = 10d;
            foreach (LogicInfoParameter param in Context.SelectedItem.Info.Parameters)
            {
                Label lbl = new Label()
                {
                    Content = param.Label,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top
                };
                lbl.Margin = new Thickness(10, y, 0, 0);
                lbl.SetColumn(0);
                lbl.MouseDoubleClick += ClickLabelTrace;
                FrameworkElement control = GenerateControl(param);
                TraceHelper binder = new TraceHelper()
                {
                    SourceControl = control,
                    Param = param
                };
                lbl.Tag = binder;
                control.Height = PARAM_ROW_HEIGHT;
                control.Margin = new Thickness(10, y, 10, 0);
                control.SetColumn(2);
                grd.AddControls(lbl, control);
                y += PARAM_ROW_DELTA;
            }
            Grid blank = new Grid()
            {
                Height = 10,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Bottom
            };
            stkMain.AddControls(grd, blank);
        }

        private FrameworkElement GenerateControl(LogicInfoParameter src)
        {
            FrameworkElement elem;
            switch (src.ValueType)
            {
                case Constant.Config.TYPE_BOOL:
                    CheckBox ckb = new CheckBox()
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left
                    };
                    ckb.Click += CkbUpdate;
                    elem = ckb;
                    break;
                case Constant.Config.TYPE_INT:
                case Constant.Config.TYPE_STRING:
                    TextBox txb = new TextBox()
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Stretch
                    };
                    txb.TextChanged += TxbUpdate;
                    elem = txb;
                    break;
                default:
                    if (cbbBuffer.ContainsKey(src.ValueType)) elem = cbbBuffer[src.ValueType];
                    else
                    {
                        ComboBox cbb = new ComboBox()
                        {
                            VerticalAlignment = VerticalAlignment.Top,
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            IsEditable = true
                        };
                        cbb.ItemsPanel = FindResource("VirtualPanel") as ItemsPanelTemplate;
                        IEnumerable<IIndexableItem> sources = Config.GetCombo(src.ValueType);
                        cbb.ItemsSource = sources;
                        cbb.SelectionChanged += CbbUpdate;
                        elem = cbb;
                        cbbBuffer[src.ValueType] = cbb;
                    }
                    break;
            }
            elem.DataContext = src;
            return elem;
        }


        private LogicInfoParameter GetInfoFromElem(FrameworkElement src)
        {
            return src.DataContext as LogicInfoParameter;
        }
        #endregion



        #region Listener
        private void CurrentTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshControl();
        }

        private void CurrentSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshControl();
        }
        #region Handler
        private void CkbUpdate(object sender, RoutedEventArgs e)
        {
            if (!isLoadingParameter)
            {
                CheckBox ckb = sender as CheckBox;
                LogicInfoParameter info = GetInfoFromElem(ckb);
                if (info != null)
                {
                    Context.SelectedItem.SetParameter(info, ckb.IsChecked.Value);
                }
            }
        }
        private void TxbUpdate(object sender, RoutedEventArgs e)
        {
            if (!isLoadingParameter)
            {
                TextBox txb = sender as TextBox;
                LogicInfoParameter info = GetInfoFromElem(txb);
                if (info != null)
                {
                    Context.SelectedItem.SetParameter(info, txb.Text);
                }
            }
        }
        private void CbbUpdate(object sender, RoutedEventArgs e)
        {
            if (!isLoadingParameter)
            {
                ComboBox cbb = sender as ComboBox;
                IIndexableItem item = cbb.SelectedItem as IIndexableItem;
                LogicInfoParameter info = GetInfoFromElem(cbb);
                if (info != null)
                {
                    Context.SelectedItem.SetParameter(info, item);
                }
            }
        }
        private void ClickLabelTrace(object sender, MouseButtonEventArgs e)
        {
            if (!isLoadingParameter)
            {
                Label lbl = sender as Label;
                TraceHelper binder = lbl.Tag as TraceHelper;
                string value = string.Empty;
                IIndexableItem target = new SimpleIndexableItem();
                if (binder.SourceControl is ComboBox cbb)
                {
                    target = cbb.SelectedItem as IIndexableItem;
                }
                else if (binder.SourceControl is TextBox txb)
                {
                    value = txb.Text;
                }
                NavigationHub.HandleTrace(binder.Param.TraceTarget, value, target);
            }
        }
        #endregion

        #endregion

        #region Drag Drop
        private void DraggedItemDropped(object sender, DragEventArgs e)
        {
            IDataObject data = new DataObject();
            data = e.Data;
            string value = data.TryGetDroppedDataObjectId(out Type valueType);
            if (!value.IsNullOrEmpty()) TryWriteDroppedValue(value, valueType);
        }

        private void TryWriteDroppedValue(string value, Type refType)
        {
            foreach (FrameworkElement elem in stkMain.Children)
            {
                if (elem is Grid grid)
                {
                    foreach (FrameworkElement c in grid.Children)
                    {
                        if (c is ComboBox cbb && cbb.Items.Count > 0)
                        {
                            object first = cbb.Items[0];
                            if (first.GetType() == refType)
                            {
                                IEnumerable<IIndexableItem> items = Config.GetCombo((cbb.DataContext as LogicInfoParameter).ValueType);
                                IIndexableItem item = items.Where(x => x.Id == value).FirstOrDefault();
                                cbb.ItemsSource = items.ToList();
                                if (item != null)
                                {
                                    cbb.SelectedItem = item;
                                    return;
                                }
                            }
                        }
                        else if (c is TextBox txb && txb.DataContext is LogicInfoParameter param)
                        {
                            if (param.ParamFormat == ParamFormatType.WpPos && refType == typeof(WaypointItem))
                            {
                                txb.Text = value;
                                return;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #endregion
    }
}
