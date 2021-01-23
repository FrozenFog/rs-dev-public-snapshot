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

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// TeamView.xaml 的交互逻辑
    /// </summary>
    public partial class TeamView : UserControl
    {
        private const double ROW_DELTA = 28;
        private const double ROW_HEIGHT = 23;
        private ModConfig ModConfig { get { return GlobalVar.GlobalConfig.ModConfig; } }

        public TeamView()
        {
            InitializeComponent();
            InitializeTeamClasses();
        }

        private void InitializeTeamClasses()
        {
            foreach (AttributeClass cls in ModConfig.TeamClasses.Values)
            {
                FrameworkElement stack = CreateGroup(cls);
                stkMain.Children.Add(stack);
            }
        }

        private FrameworkElement CreateGroup(AttributeClass cls)
        {
            StackPanel container = new StackPanel();
            Button head = new Button()
            {
                Content = cls.Name
            };
            head.SetStyle(this, "btnDark");
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
            split.SetStyle(this, "gsplitDark");
            split.SetColumn(1);
            grd.Children.Add(split);
            double y = 10d;
            foreach (AttributeItem item in ModConfig.TeamItems.Values.Where(x => x.ClassId == cls.Id))
            {
                Label lbl = new Label()
                {
                    Content = item.Name,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Top
                };
                lbl.SetStyle(this, "lblDark");
                lbl.Margin = new Thickness(0, y, 10, 0);
                lbl.SetColumn(0);
                FrameworkElement control = AcquireControlFromAttribute(item);
                control.Height = ROW_HEIGHT;
                control.Margin = new Thickness(10, y, 10, 0);
                control.SetColumn(2);
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

        private FrameworkElement AcquireControlFromAttribute(AttributeItem src)
        {
            TextBox r = new TextBox()
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            r.SetStyle(this, "txbDark");
            return r;
        }
    }
}
