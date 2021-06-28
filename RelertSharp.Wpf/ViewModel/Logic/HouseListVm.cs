using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using RelertSharp.IniSystem;
using System.Windows;

namespace RelertSharp.Wpf.ViewModel
{
    internal class HouseListVm : BaseVm<HouseItem>
    {
        #region Ctor
        public HouseListVm()
        {
            data = new HouseItem();
        }
        public HouseListVm(HouseItem obj) : base(obj)
        {
            obj.NameUpdated += SetName;
            obj.ColorUpdated += SetColor;
        }

        private void SetColor(object sender, EventArgs e)
        {
            SetProperty(nameof(HouseColor));
        }

        private void SetName(object sender, EventArgs e)
        {
            SetProperty(nameof(Title));
        }
        #endregion


        #region Bind Call
        public string Title
        {
            get { return data.Name; }
        }
        public Brush HouseColor
        {
            get
            {
                SolidColorBrush b = new SolidColorBrush(Colors.White);
                if (GlobalVar.GlobalRules != null && !data.ColorName.IsNullOrEmpty())
                {
                    INIPair p = GlobalVar.GlobalRules.GetColorInfo(data.ColorName);
                    Color rgb = Utils.HSBColor.FromHSB(p.ParseStringList()).ToWpfColor();
                    b.Color = rgb;
                }
                return b;
            }
        }
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                SetProperty();
            }
        }
        #endregion
        public HouseItem Data
        {
            get { return data; }
        }
    }
}
