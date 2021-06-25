using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.Common;
using RelertSharp.MapStructure.Logic;

namespace RelertSharp.Wpf.ViewModel
{
    internal class GlobalTriggerVm : BaseNotifyCollectionVm<TriggerItem>, IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            if (GlobalVar.CurrentMapDocument != null) return GlobalVar.CurrentMapDocument.Map.Triggers.GetEnumerator();
            return EmptyEnumerator;
        }
    }
}
