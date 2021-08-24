using RelertSharp.Common;
using RelertSharp.Wpf.MapEngine.Helper;
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
using System.Windows.Shapes;

namespace RelertSharp.Wpf.Dialogs
{
    /// <summary>
    /// DlgRandomizeBrush.xaml 的交互逻辑
    /// </summary>
    public partial class DlgRandomizeBrush : Window
    {
        public DlgRandomizeBrush()
        {
            InitializeComponent();
        }



        #region Public
        public void LoadBrush()
        {
            lvMain.Items.Clear();
            foreach (var obj in RandomizeBrush.CurrentObjects)
            {
                var vm = new RandomObjectVm(obj);
                lvMain.Items.Add(vm);
            }
        }
        #endregion


        private void Menu_RemoveAll(object sender, RoutedEventArgs e)
        {
            RandomizeBrush.ClearRandomObject();
            lvMain.Items.Clear();
        }

        private void Menu_RemoveCurrent(object sender, RoutedEventArgs e)
        {
            if (lvMain.SelectedItem is RandomObjectVm vm)
            {
                RandomizeBrush.RemoveObject(vm.Data);
                lvMain.Items.Remove(vm);
            }
        }
        private void PreviewRightDown(object sender, MouseButtonEventArgs e)
        {
            bool canRemove = lvMain.SelectedItem != null;
            menuRemove.IsEnabled = canRemove;
        }



        internal class RandomObjectVm : BaseVm<IAbstractObjectDescriber>
        {
            public RandomObjectVm(IAbstractObjectDescriber obj)
            {
                data = obj;
            }

            public IAbstractObjectDescriber Data { get { return data; } }


            #region Bind
            public MapObjectType ObjectType { get { return data.ObjectType; } }
            public string RegName { get { return data.RegName; } }
            public string Name { get { return GlobalVar.GlobalRules.FormatTreeNodeName(RegName); } }
            public byte OverlayIndex { get { return (data as IOverlay).OverlayIndex; } }
            public byte OverlayFrame { get { return (data as IOverlay).OverlayFrame; } }
            #endregion
        }
    }
}
