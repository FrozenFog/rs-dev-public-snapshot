using RelertSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.ViewModel
{
    internal class ConfigDialogVm : BaseVm<object>
    {
        public ConfigDialogVm()
        {

        }

        public void Update()
        {
            OnAllPropertyChanged();
        }


        #region Bind
        public bool IsConfigLoaded
        {
            get { return GlobalVar.GlobalConfig != null; }
        }
        public string GamePath
        {
            get
            {
                if (GlobalVar.GlobalConfig != null) return GlobalVar.GlobalConfig.GamePath;
                else return string.Empty;
            }
            set
            {
                if (GlobalVar.GlobalConfig != null) GlobalVar.GlobalConfig.GamePath = value;
                SetProperty();
            }
        }
        #endregion
    }
}
