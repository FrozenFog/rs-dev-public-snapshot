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

        #region Curd
        public void AddItemAt(int pos)
        {
            data.AddItemAt(pos);
            SetProperty(nameof(Items));
        }
        public void RemoveItemAt(int pos)
        {
            data.RemoveItemAt(pos);
            SetProperty(nameof(Items));
        }
        public void MoveItemTo(int from, int to)
        {
            data.MoveItemTo(from, to);
            SetProperty(nameof(Items));
        }
        public void CopyItem(int sourceIndex)
        {
            data.CopyItemAt(sourceIndex);
            SetProperty(nameof(Items));
        }
        public void RemoveAllItem()
        {
            data.RemoveAll();
            SetProperty(nameof(Items));
        }
        #region Command
        public void ApplyCommand(string commandLine)
        {
            data.ApplyCommandLine(commandLine);
            SetProperty(nameof(Items));
        }
        #endregion
        #endregion
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
        public int Count { get { return data.Count(); } }
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


    internal class TriggerLogicItemVm : BaseVm<LogicItem>
    {
        public TriggerLogicItemVm()
        {
            data = new LogicItem(TriggerSubType.Event, null);
            data.InfoUpdated += UpdateTitle;
        }

        private void UpdateTitle(object sender, EventArgs e)
        {
            SetProperty(nameof(Title));
            SetProperty(nameof(DetailInformation));
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
        }
        public LogicInfo CurrentItemType
        {
            get { return data.Info; }
            set
            {
                int id = value.Id;
                data.SetIdTo(id);
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
}
