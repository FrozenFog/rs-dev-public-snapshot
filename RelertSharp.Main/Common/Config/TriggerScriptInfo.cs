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
        private void LoadTriggerLanguage()
        {
            TriggerLanguageCollection source = GlobalVar.Language.Data.Triggers;
            void load(IEnumerable<LogicInfo> dest, Dictionary<int, TriggerLanguageItem> src)
            {
                foreach (var item in dest)
                {
                    var language = src[item.Id];
                    item.Abstract = language.Abstract;
                    item.Description = language.Description;
                    item.FormatString = language.FormatString;
                }
            }
            load(TriggerInfo.TriggerEvents, source.Event);
            load(TriggerInfo.TriggerActions, source.Action);
            load(TriggerInfo.ScriptActions, source.Script);
        }

        #region Public Calls
        public TriggerInfo TriggerInfo { get { return data.TriggerInfo; } }
        #endregion
    }
}
