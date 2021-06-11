using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;
using System.Collections;

namespace RelertSharp.Wpf.ViewModel
{
    internal class GlobalTagVm : BaseNotifyCollectionVm<TagItem>, IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            if (GlobalVar.CurrentMapDocument != null) return GlobalVar.CurrentMapDocument.Map.Tags.GetEnumerator();
            return EmptyEnumerator;
        }
    }
}
