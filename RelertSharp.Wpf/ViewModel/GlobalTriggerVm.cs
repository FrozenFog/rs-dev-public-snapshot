﻿using System;
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


        public TriggerTreeItemVm Ancestor { get; private set; }
        public TriggerItem Data { get; set; }
        public string Title { get; set; }
        public bool IsTree { get { return Data == null; } }
        public bool IsRoot { get { return Ancestor == null; } }

        public ObservableCollection<TriggerTreeItemVm> Items { get; set; } = new ObservableCollection<TriggerTreeItemVm>();
    }
}