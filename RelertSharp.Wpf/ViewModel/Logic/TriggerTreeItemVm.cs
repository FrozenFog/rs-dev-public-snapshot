using RelertSharp.Common;
using RelertSharp.MapStructure.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RelertSharp.Wpf.ViewModel
{
    internal class TriggerTreeItemVm : BaseTreeVm<TriggerItem>
    {
        public TriggerTreeItemVm()
        {

        }
        public TriggerTreeItemVm(TriggerItem trg)
        {
            Data = trg;
            Data.NameUpdated += SetName;
        }

        private void SetName(object sender, EventArgs e)
        {
            SetProperty(nameof(Title));
        }

        public void AddItem(TriggerItem trg, string title)
        {
            TriggerTreeItemVm vm = new TriggerTreeItemVm(trg);
            vm.Title = title;
            base.AddItem(vm);
        }
        public void Sort(bool ascending)
        {
            List<TriggerTreeItemVm> trees = new List<TriggerTreeItemVm>();
            List<TriggerTreeItemVm> items = new List<TriggerTreeItemVm>();
            foreach (TriggerTreeItemVm item in Items)
            {
                if (item.IsTree)
                {
                    item.Sort(ascending);
                    trees.Add(item);
                }
                else items.Add(item);
            }
            Items.Clear();
            if (ascending)
            {
                trees = trees.OrderBy(x => x.Title).ToList();
                items = items.OrderBy(x => x.Title).ToList();
            }
            else
            {
                trees = trees.OrderByDescending(x => x.Title).ToList();
                items = items.OrderByDescending(x => x.Title).ToList();
            }
            trees.ForEach(x => Items.Add(x));
            items.ForEach(x => Items.Add(x));
        }
        public TriggerTreeItemVm Find(IIndexableItem target)
        {
            if (IsTree)
            {
                foreach (TriggerTreeItemVm sub in Items)
                {
                    TriggerTreeItemVm result = sub.Find(target);
                    if (result != null) return result;
                }
            }
            else
            {
                if (data.Id == target.Id) return this;
            }
            return null;
        }
        public override void RemoveItem(IBaseTreeVm<TriggerItem> item)
        {
            base.RemoveItem(item);
            SetProperty(nameof(HeadImg));
        }


        public TriggerItem Data
        {
            get { return data; }
            set { data = value; }
        }
        private string title;
        public string Title
        {
            get { if (Data != null) return Data.Name; return title; }
            set
            {
                title = value;
                SetProperty();
            }
        }
        public override bool IsExpanded
        {
            get { return base.IsExpanded; }
            set
            {
                base.IsExpanded = value;
                SetProperty(nameof(HeadImg));
            }
        }
        public ImageSource HeadImg
        {
            get
            {
                if (IsTree)
                {
                    if (IsExpanded)
                    {
                        if (Items.Count == 0) return Properties.Resources.iconTrgFold.ToWpfImage(true);
                        return Properties.Resources.iconTrgExpand.ToWpfImage(true);
                    }
                    else return Properties.Resources.iconTrgFold.ToWpfImage(true);
                }
                else return null;
            }
        }
    }
}