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
            if (GlobalVar.HasMap) return GlobalVar.GlobalMap.Triggers.GetEnumerator();
            return EmptyEnumerator;
        }
    }
}
