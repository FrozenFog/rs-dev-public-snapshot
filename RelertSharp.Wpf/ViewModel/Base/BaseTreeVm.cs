using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RelertSharp.Wpf.ViewModel
{

    internal abstract class BaseTreeVm<TData> : BaseVm<TData>, IBaseTreeVm<TData> where TData : class
    {
        public BaseTreeVm(object obj = null) : base(obj)
        {

        }


        #region Protected
        protected virtual void SetName(object sender, EventArgs e)
        {
            SetProperty(nameof(Title));
        }
        #endregion


        #region Public Methods
        public virtual int IndexOf(IBaseTreeVm<TData> iitem)
        {
            return Items.IndexOf(iitem);
        }
        public virtual void AddItem(IBaseTreeVm<TData> iitem)
        {
            if (iitem is BaseTreeVm<TData> item)
            {
                item.Ancestor = this;
                Items.Add(item);
            }
        }
        public virtual void AddItems(params IBaseTreeVm<TData>[] items)
        {
            foreach (IBaseTreeVm<TData> item in items) AddItem(item);
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
        public virtual void RemoveAllItem()
        {
            Items.Clear();
        }
        public virtual bool IsDescendantOf(IBaseTreeVm<TData> ancestor)
        {
            if (!ancestor.IsTree) return false;
            IBaseTreeVm<TData> descendant = this;
            while (descendant != null)
            {
                if (descendant.Ancestor == null) return false;
                if (descendant.Ancestor.Equals(ancestor)) return true;
                descendant = descendant.Ancestor;
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
        public virtual void SetTitle(string title)
        {
            _title = title;
            SetProperty(nameof(Title));
        }
        public virtual void All(Action<IBaseTreeVm<TData>> action)
        {
            foreach (IBaseTreeVm<TData> item in Items)
            {
                action(item);
                item.All(action);
            }
        }
        #endregion


        #region Calls
        public IBaseTreeVm<TData> Ancestor { get; private set; }
        private bool _isSelected;
        public virtual bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                SetProperty();
            }
        }
        private bool _isExpanded;
        public virtual bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                SetProperty();
                SetProperty(nameof(HeadImg));
            }
        }
        public virtual ImageSource HeadImg
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
        public virtual bool IsTree { get { return data == null; } }
        public virtual bool IsNotTree { get { return !IsTree; } }
        public bool IsRoot { get { return Ancestor == null; } }
        public ObservableCollection<IBaseTreeVm<TData>> Items { get; set; } = new ObservableCollection<IBaseTreeVm<TData>>();
        protected string _title;
        public virtual string Title { get; }
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
        int IndexOf(IBaseTreeVm<TData> item);
        void ExpandAllAncestor();
        bool IsDescendantOf(IBaseTreeVm<TData> ancestor);
        void RemoveFromAncestor();
        void RemoveItem(IBaseTreeVm<TData> item);
        void UnselectAllDescendant();
        void SetTitle(string name);
        void All(Action<IBaseTreeVm<TData>> action);
    }
}
