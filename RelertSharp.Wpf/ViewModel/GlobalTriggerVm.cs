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

    internal class TriggerTreeItemVm
    {
        public TriggerTreeItemVm()
        {

        }
        public TriggerTreeItemVm(TriggerItem trg)
        {
            Data = trg;
        }


        public void AddItem(TriggerTreeItemVm item)
        {
            item.Ancestor = this;
            Items.Add(item);
        }
        public void AddItem(TriggerItem trg, string title)
        {
            TriggerTreeItemVm vm = new TriggerTreeItemVm(trg);
            vm.Title = title;
            AddItem(vm);
        }


        public TriggerTreeItemVm Ancestor { get; private set; }
        public TriggerItem Data { get; set; }
        public string Title { get; set; }
        public bool IsTree { get { return Items.Count > 0; } }

        public ObservableCollection<TriggerTreeItemVm> Items { get; set; } = new ObservableCollection<TriggerTreeItemVm>();
    }
}
