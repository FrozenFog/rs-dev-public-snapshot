using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using RelertSharp.Common;
using RelertSharp.IniSystem;
using RelertSharp.MapStructure.Logic;


namespace RelertSharp.Wpf.ViewModel
{
    internal class HouseVm : BaseVm<IIndexableItem>
    {
        private HouseItem house;
        private CountryItem country;


        #region ctor
        public HouseVm()
        {
            // virtual, useless
            data = new ComboItem();
            house = new HouseItem();
            country = new CountryItem();
            house.AllInfoUpdate += UpdateAllHouseInfo;
            country.AllInfoUpdated += UpdateAllCountryInfo;
        }

        public HouseVm(HouseItem house, CountryItem country)
        {
            this.house = house;
            this.country = country;
            house.AllInfoUpdate += UpdateAllHouseInfo;
            country.AllInfoUpdated += UpdateAllCountryInfo;
        }
        public HouseVm(object obj) : base(obj) { }
        #endregion


        #region Private
        private void UpdateAllCountryInfo(object sender, EventArgs e)
        {
            SetProperty(nameof(HouseName));
            SetProperty(nameof(InheritFromRulesHouseItem));
            SetProperty(nameof(ExtraInfo));
        }
        private void UpdateAllHouseInfo(object sender, EventArgs e)
        {
            SetProperty(nameof(AlliesWith));
            SetProperty(nameof(HouseName));
            SetProperty(nameof(Credit));
        }
        private void FindAncestorCountryProps(string inheritFrom, out string prefix, out string suffix, out string side)
        {
            INIEntity ancestor = GlobalVar.GlobalRules[inheritFrom];
            suffix = ancestor["Suffix"];
            prefix = ancestor["Prefix"];
            side = ancestor["Side"];
        }
        #endregion


        #region Public
        public void AddAlly(string allyName)
        {
            if (!house.AlliesWith.Contains(allyName))
            {
                house.AlliesWith.Add(allyName);
                SetProperty(nameof(AlliesWith));
            }
        }
        #endregion


        #region Bind Call
        /// <summary>
        /// Without "House" in "XXX House"
        /// </summary>
        public string HouseName
        {
            get { return country.Name; }
            set
            {
                country.Name = value;
                house.Name = string.Format("{0} House", value);
                house.Country = value;
                house.OnNameUpdated();
                country.OnNameUpdated();
                SetProperty();
            }
        }
        public int Credit
        {
            get { return house.Credits; }
            set
            {
                house.Credits = value;
                SetProperty();
            }
        }
        public IIndexableItem InheritFromRulesHouseItem
        {
            get { return new ComboItem(country.ParentCountryName); }
            set
            {
                if (value == null) return;
                country.ParentCountryName = value.Id;
                FindAncestorCountryProps(value.Id, out string prefix, out string suffix, out string side);
                country.Prefix = prefix;
                country.Suffix = suffix;
                country.Side = side;
                SetProperty();
            }
        }
        public IIndexableItem ColorItem
        {
            get { return new ComboItem(house.ColorName); }
            set
            {
                if (value == null) return;
                string c = value.Id;
                house.ColorName = c;
                country.ColorName = c;
                SetProperty();
                SetProperty(nameof(ColorIndicator));
                var objects = GlobalVar.GlobalMap?.AllCombatObjects;
                if (objects != null)
                {
                    IEnumerable<IMapObject> targets = objects.Where(x => x.Owner == house.Name);
                    Engine.Api.EngineApi.UpdateHouseAllObjectColor(targets);
                    Engine.Api.EngineApi.UpdateHouseAllObjectColor(house.BaseNodes);
                }
            }
        }
        public Brush ColorIndicator
        {
            get
            {
                SolidColorBrush b = new SolidColorBrush(Colors.White);
                if (GlobalVar.GlobalRules != null && !house.ColorName.IsNullOrEmpty())
                {
                    INIPair p = GlobalVar.GlobalRules.GetColorInfo(house.ColorName);
                    Color rgb = Utils.HSBColor.FromHSB(p.ParseStringList()).ToWpfColor();
                    b.Color = rgb;
                }
                return b;
            }
        }
        public IIndexableItem EdgeItem
        {
            get { return new ComboItem(house.Edge.ToString()); }
            set
            {
                house.Edge = (HouseEdges)Enum.Parse(typeof(HouseEdges), value.Id);
                SetProperty();
            }
        }
        public double BuildActivity
        {
            get { return house.PercentBuilt; }
            set
            {
                house.PercentBuilt = value;
                SetProperty();
            }
        }
        public int TechLevel
        {
            get { return house.TechLevel; }
            set
            {
                house.TechLevel = value;
                SetProperty();
            }
        }
        public int IQ
        {
            get { return house.IQ; }
            set
            {
                house.IQ = value;
                SetProperty();
            }
        }
        public bool PlayerControl
        {
            get { return house.PlayerControl; }
            set
            {
                house.PlayerControl = value;
                SetProperty();
            }
        }
        public IEnumerable<string> AlliesWith
        {
            get { return house.AlliesWith; }
        }
        public string ExtraInfo
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                IEnumerable<INIPair> residual = country.Residual;
                foreach (INIPair p in residual)
                {
                    sb.AppendLine(string.Format("{0}={1}", p.Name, p.Value));
                }
                return sb.ToString();
            }
            set
            {
                string[] lines = value.Split('\n');
                List<INIPair> pairs = new List<INIPair>();
                foreach (string line in lines)
                {
                    if (line.StartsWith(";")) continue;
                    string[] data = line.Split('=');
                    if (data.Length != 2) continue;
                    string pValue = data[1];
                    if (pValue.Contains(";")) pValue = pValue.Split(';')[0];
                    INIPair p = new INIPair(data[0].Trim(), pValue.Trim());
                    pairs.Add(p);
                }
                country.Residual = pairs;
                SetProperty();
            }
        }
        #endregion

    }
}
