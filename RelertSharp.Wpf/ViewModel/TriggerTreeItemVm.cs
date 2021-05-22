using RelertSharp.MapStructure.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.ViewModel
{
    internal class TriggerTreeItemVm : BaseVm<TriggerItem>
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

        public void AddItem(TriggerTreeItemVm item)
        {
            item.Ancestor = this;
            if (item.IsTree) Items.Insert(0, item);
            else Items.Add(item);
        }
        public void AddItem(TriggerItem trg, string title)
        {
            TriggerTreeItemVm vm = new TriggerTreeItemVm(trg);
            vm.Title = title;
            AddItem(vm);
        }
        public void RemoveFromAncestor()
        {
            Ancestor?.RemoveItem(this);
            Ancestor = null;
        }
        public void RemoveItem(TriggerTreeItemVm target)
        {
            Items.Remove(target);
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
        public bool IsDescendantOf(TriggerTreeItemVm ancestor)
        {
            TriggerTreeItemVm back = this;
            while (back != null)
            {
                if (back.Ancestor == null) return false;
                if (back.Ancestor == ancestor) return true;
                back = this.Ancestor;
            }
            return false;
        }
        public void UnselectAllDescendant()
        {
            foreach (TriggerTreeItemVm item in Items)
            {
                item.UnselectAllDescendant();
            }
            IsSelected = false;
        }


        public TriggerTreeItemVm Ancestor { get; private set; }
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
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                SetProperty();
            }
        }
        private bool _isExpanded;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                SetProperty();
            }
        }
        public bool IsTree { get { return Data == null; } }
        public bool IsRoot { get { return Ancestor == null; } }

        public ObservableCollection<TriggerTreeItemVm> Items { get; set; } = new ObservableCollection<TriggerTreeItemVm>();
    }
}