using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelertSharp.MapStructure.Logic;
using RelertSharp.MapStructure;
using RelertSharp.Common;
using static RelertSharp.Common.Constant;
using System.Collections;

namespace RelertSharp.Wpf.ViewModel
{
    internal class TriggerLogicVm : BaseVm<LogicGroup>
    {
        public TriggerLogicVm()
        {
            data = new LogicGroup();
        }
        public TriggerLogicVm(object obj) : base(obj) { }


        public TriggerLogicCollectionVm Items
        {
            get { return new TriggerLogicCollectionVm(data); }
        }
    }


    internal class TriggerLogicCollectionVm : BaseNotifyCollectionVm<LogicItem>, IEnumerable
    {
        private List<LogicItem> data;
        public TriggerLogicCollectionVm()
        {
            data = new List<LogicItem>();
        }
        public TriggerLogicCollectionVm(IEnumerable<LogicItem> items)
        {
            data = items.ToList();
        }
        public IEnumerator GetEnumerator()
        {
            return data.GetEnumerator();
        }
        public List<LogicItem> Items { get { return data; } }
    }
}
