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


        public void Compile(string ancestorName = null)
        {
            if (data != null)
            {
                data.CompileName = ancestorName.IsNullOrEmpty() ? data.Name : string.Format("[{0}] {1}", ancestorName, data.Name);
            }
            else
            {
                foreach (TriggerTreeItemVm child in Items)
                {
                    string anc;
                    if (ancestorName.IsNullOrEmpty()) anc = Title;
                    else anc = string.Format("{0}.{1}", ancestorName, Title);
                    child.Compile(anc);
                }
            }
        }
        public void AddItem(TriggerItem trg, string title)
        {
            TriggerTreeItemVm vm = new TriggerTreeItemVm(trg);
            vm.SetTitle(title);
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
        public override string Title
        {
            get { if (Data != null) return Data.Name; return _title; }
        }
        public bool IsEqualWith(TriggerTreeItemVm target)
        {
            if (target.IsTree && IsTree) return target.Title == Title;
            else if (!target.IsTree && !IsTree)
            {
                return target.Data.ExtractParameter().Equals(Data.ExtractParameter());
            }
            else return false;
        }
    }
}