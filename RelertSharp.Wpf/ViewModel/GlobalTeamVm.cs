using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure.Logic;
using RelertSharp.Common;

namespace RelertSharp.Wpf.ViewModel
{
    internal class GlobalTeamVm : BaseNotifyCollectionVm<TeamItem>, IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            if (GlobalVar.CurrentMapDocument != null) return GlobalVar.CurrentMapDocument.Map.Teams.GetEnumerator();
            return EmptyEnumerator;
        }

        public void UpdateAll()
        {
            OnReset();
        }
    }
}
