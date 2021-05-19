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
using RelertSharp.Common.Config.Model;

namespace RelertSharp.Wpf.ViewModel
{
    internal class TriggerLogicVm : BaseVm<LogicGroup>
    {

        public TriggerLogicVm()
        {
            data = new LogicGroup();
        }
        public TriggerLogicVm(LogicGroup src) : base(src)
        {

        }


        public TriggerLogicCollectionVm Items
        {
            get { return new TriggerLogicCollectionVm(data); }
        }
        private TriggerLogicItemVm selectedItem;
        public TriggerLogicItemVm SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                SetProperty();
            }
        }
    }

    internal class TriggerLogicItemVm : BaseVm<LogicItem>
    {
        public TriggerLogicItemVm()
        {
            data = new LogicItem(TriggerSubType.EventLogic);
            data.InfoUpdated += UpdateTitle;
        }

        private void UpdateTitle(object sender, EventArgs e)
        {
            SetProperty(nameof(Title));
        }

        public TriggerLogicItemVm(LogicItem src) : base(src)
        {
            data.InfoUpdated += UpdateTitle;
        }


        public string GetParameter(LogicInfoParameter param)
        {
            return data.GetParameter(param);
        }
        public void SetParameter(LogicInfoParameter param, string value)
        {
            if (value.IsNullOrEmpty()) value = Config.PARAM_DEF;
            data.SetParameter(param, value);
        }
        public void SetParameter(LogicInfoParameter param, bool value)
        {
            data.SetParameter(param, value);
        }
        public void SetParameter(LogicInfoParameter param, IIndexableItem value)
        {
            if (value != null) data.SetParameter(param, value.Id);
            else data.SetParameter(param, Config.PARAM_DEF);
        }


        public string Title
        {
            get { return data.ToString(); }
            set
            {
                SetProperty();
            }
        }
        public int CurrentItemTypeIndex
        {
            get { return data.ID; }
            set
            {
                data.SetIdTo(value);
                SetProperty();
            }
        }
        public string DetailInformation
        {
            get { return data.Info.Description; }
        }
        public LogicInfo Info
        {
            get { return data.Info; }
        }
    }


    internal class TriggerLogicCollectionVm : BaseNotifyCollectionVm<TriggerLogicItemVm>, IEnumerable
    {
        private List<TriggerLogicItemVm> data;
        public TriggerLogicCollectionVm()
        {
            data = new List<TriggerLogicItemVm>();
        }
        public TriggerLogicCollectionVm(IEnumerable<LogicItem> items)
        {
            data = new List<TriggerLogicItemVm>();
            items.Foreach(x =>
            {
                data.Add(new TriggerLogicItemVm(x));
            });
        }
        public IEnumerator GetEnumerator()
        {
            return data.GetEnumerator();
        }
        public List<TriggerLogicItemVm> Items { get { return data; } }
    }
}
