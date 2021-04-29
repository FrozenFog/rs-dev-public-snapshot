using RelertSharp.Common.Config.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Common
{
    public partial class ModConfig
    {

        #region Public Calls
        public TriggerInfo TriggerInfo { get { return data.TriggerInfo; } }
        #endregion
    }
}
