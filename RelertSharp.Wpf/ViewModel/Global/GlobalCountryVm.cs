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
    internal class GlobalCountryVm : BaseNotifyCollectionVm<CountryItem>, IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            if (GlobalVar.HasMap) return GlobalVar.GlobalMap.Countries.GetEnumerator();
            return EmptyEnumerator;
        }
    }
}