using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelertSharp.Wpf.ViewModel
{

    internal abstract class BaseTreeVm<TData> : BaseVm<TData>, IBaseTreeVm<TData> where TData : class
    {
        public BaseTreeVm(object obj = null) : base(obj)
        {

        }


        #region Private

        #endregion


        #region Public Methods
        public virtual void AddItem(IBaseTreeVm<TData> iitem)
        {
            if (iitem is BaseTreeVm<TData> item)
            {
                item.Ancestor = this;
                Items.Add(item);
            }
        }
        public virtual void InsertItem(IBaseTreeVm<TData> iitem, int index = 0)
        {
            if (iitem is BaseTreeVm<TData> item)
            {
                item.Ancestor = this;
                Items.Insert(index, item);
            }
        }
        public virtual void RemoveFromAncestor()
        {
            Ancestor?.RemoveItem(this);
            Ancestor = null;
        }
        public virtual void RemoveItem(IBaseTreeVm<TData> item)
        {
            Items.Remove(item);
        }
        public virtual bool IsDescendantOf(IBaseTreeVm<TData> ancestor)
        {
            IBaseTreeVm<TData> back = this;
            while (back != null)
            {
                if (back.Ancestor == null) return false;
                if (back.Ancestor == ancestor) return true;
                back = this.Ancestor;
            }
            return false;
        }
        public virtual void UnselectAllDescendant()
        {
            foreach (IBaseTreeVm<TData> item in Items)
            {
                item.UnselectAllDescendant();
            }
            IsSelected = false;
        }
        public virtual void ExpandAllAncestor()
        {
            IBaseTreeVm<TData> ancestor = this.Ancestor;
            while (ancestor != null)
            {
                ancestor.IsExpanded = true;
                ancestor = ancestor.Ancestor;
            }
        }
        #endregion


        #region Calls
        public IBaseTreeVm<TData> Ancestor { get; private set; }
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
        public bool IsTree { get { return data == null; } }
        public bool IsRoot { get { return Ancestor == null; } }
        public ObservableCollection<IBaseTreeVm<TData>> Items { get; set; } = new ObservableCollection<IBaseTreeVm<TData>>();
        #endregion
    }


    internal interface IBaseTreeVm<TData> where TData : class
    {
        IBaseTreeVm<TData> Ancestor { get; }
        bool IsExpanded { get; set; }
        bool IsRoot { get; }
        bool IsSelected { get; set; }
        bool IsTree { get; }
        ObservableCollection<IBaseTreeVm<TData>> Items { get; set; }

        void AddItem(IBaseTreeVm<TData> item);
        void InsertItem(IBaseTreeVm<TData> item, int index = 0);
        void ExpandAllAncestor();
        bool IsDescendantOf(IBaseTreeVm<TData> ancestor);
        void RemoveFromAncestor();
        void RemoveItem(IBaseTreeVm<TData> item);
        void UnselectAllDescendant();
    }
}
