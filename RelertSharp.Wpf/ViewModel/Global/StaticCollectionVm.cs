using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using static RelertSharp.Common.Constant.Config.DefaultComboType;

namespace RelertSharp.Wpf.ViewModel
{
    internal class StaticCollectionVm 
    {
        private const string TYPE_AITRG_COND = "aitcond";
        private const string TYPE_AITRG_OPER = "aitoper";
        private const string TYPE_AISIDE = "AiSideIndex";
        private Dictionary<string, IEnumerable<IIndexableItem>> data = new Dictionary<string, IEnumerable<IIndexableItem>>();

        private ModConfig config { get { return GlobalVar.GlobalConfig.ModConfig; } }
        public StaticCollectionVm()
        {
            data[TYPE_AITRG_COND] = config.GetCombo("AiTriggerType");
            data[TYPE_AITRG_OPER] = config.GetCombo("AiTriggerOperator");
            data[TYPE_TECHTYPE] = config.GetCombo(TYPE_TECHTYPE);
            data[TYPE_AISIDE] = config.GetCombo(TYPE_AISIDE);
        }

        public IEnumerable<IIndexableItem> AiTriggerConditions
        {
            get { return data[TYPE_AITRG_COND]; }
        }
        public IEnumerable<IIndexableItem> AiTriggerOperators
        {
            get { return data[TYPE_AITRG_OPER]; }
        }
        public IEnumerable<IIndexableItem> TechTypes
        {
            get { return data[TYPE_TECHTYPE]; }
        }
        public IEnumerable<IIndexableItem> AiSideIndexes
        {
            get { return data[TYPE_AISIDE]; }
        }
    }
}
