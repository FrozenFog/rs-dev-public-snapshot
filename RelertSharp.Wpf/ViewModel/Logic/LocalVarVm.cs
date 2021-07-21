using RelertSharp.MapStructure.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.ViewModel
{
    internal class LocalVarVm : BaseListVm<LocalVarItem>
    {
        public LocalVarVm()
        {
            data = new LocalVarItem();
            data.NameUpdated += UpdateName;
        }
        public LocalVarVm(LocalVarItem src) : base(src)
        {
            src.NameUpdated += UpdateName;
        }

        private void UpdateName(object sender, EventArgs e)
        {
            SetProperty(nameof(Header));
        }



        #region Binding Call
        public string Name
        {
            get { return data.Name; }
            set
            {
                data.Name = value;
                data.OnNameUpdated();
                SetProperty();
            }
        }
        public bool IsEnabled
        {
            get { return data.InitState; }
            set
            {
                data.InitState = value;
                SetProperty();
            }
        }
        public string Header
        {
            get { return data.ToString(); }
        }
        #endregion
    }
}
