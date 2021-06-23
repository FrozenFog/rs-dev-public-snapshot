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
using RelertSharp.IniSystem;
using RelertSharp.MapStructure;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Wpf.ViewModel;

namespace RelertSharp.Wpf.Views
{
    /// <summary>
    /// CountryHouseView.xaml 的交互逻辑
    /// </summary>
    public partial class CountryHouseView : UserControl
    {
        private Map Map { get { return GlobalVar.CurrentMapDocument?.Map; } }
        public CountryHouseView()
        {
            InitializeComponent();
            GlobalVar.MapDocumentLoaded += MapReloadedHandler;
        }

        private void MapReloadedHandler(object sender, EventArgs e)
        {
            //lbxHouse.ItemsSource = null;
            //lbxHouse.ItemsSource = GlobalCollectionVm.Houses;
            lbxHouse.Items.Clear();
            foreach (HouseItem house in Map.Houses)
            {
                lbxHouse.Items.Add(new HouseListVm(house));
            }

            cbbInherit.Items.Clear();
            INIEntity rulesHouses = GlobalVar.GlobalRules["Countries"];
            foreach (INIPair p in rulesHouses)
            {
                cbbInherit.Items.Add(new ComboItem(p.Value.ToString()));
            }

            cbbColor.Items.Clear();
            INIEntity rulesColors = GlobalVar.GlobalRules["Colors"];
            foreach (INIPair p in rulesColors)
            {
                cbbColor.Items.Add(new ComboItem(p.Name));
            }
        }



        #region Handler
        private void HouseSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbxHouse.SelectedItem is HouseListVm lsHouse)
            {
                if (Map?.Countries.GetCountry(lsHouse.Data.Country) is CountryItem country)
                {
                    DataContext = new HouseVm(lsHouse.Data, country);
                }
            }
        }
        #endregion
    }
}
