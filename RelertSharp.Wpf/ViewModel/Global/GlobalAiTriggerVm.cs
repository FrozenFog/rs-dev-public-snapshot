using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;

namespace RelertSharp.Wpf.ViewModel
{
    internal class GlobalAiTriggerVm : BaseNotifyCollectionVm<AITriggerItem>, IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            if (GlobalVar.CurrentMapDocument != null) return GlobalVar.CurrentMapDocument.Map.AiTriggers.GetEnumerator();
            return EmptyEnumerator;
        }
    }
}
